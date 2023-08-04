using System.Diagnostics;
using System.Net.WebSockets;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;

using GSendCommon.Settings;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Models;

using Microsoft.Extensions.DependencyInjection;

using PluginManager.Abstractions;

using Shared.Classes;

namespace GSendCommon
{
    public class ProcessorMediator : IProcessorMediator, INotificationListener
    {
        internal static bool _isLicensed = true;
        private static DateTime _lastLicenseCheck = DateTime.MinValue;
        private static readonly List<IGCodeProcessor> _machines = new();
        private const int BufferSize = 8192;
        private const int DelayBetweenUpdatesSent = 100;
        private const int LicenseValidationCheckInterval = 250;
        private DateTime _lastSendStatus = DateTime.MinValue;
        private readonly object _lockObject = new();
        private readonly ILogger _logger;
        private readonly IGSendDataProvider _gSendDataProvider;
        private readonly IComPortFactory _comPortFactory;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly GSendSettings _settings;
        private readonly IServiceProvider _serviceProvider;
        private readonly IGCodeParserFactory _gCodeParserFactory;
        private System.Net.WebSockets.WebSocket _webSocket;
        private bool _processEvents = false;
        private ulong _messageId = 0;
        private string _clientId = null;

        public ProcessorMediator(IServiceProvider serviceProvider,
            ILogger logger,
            IGSendDataProvider gSendDataProvider,
            IComPortFactory comPortFactory,
            INotificationService notificationService,
            ISettingsProvider settingsProvider,
            IGCodeParserFactory gCodeParserFactory)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            if (notificationService == null)
                throw new ArgumentNullException(nameof(notificationService));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _gSendDataProvider = gSendDataProvider ?? throw new ArgumentNullException(nameof(gSendDataProvider));
            _comPortFactory = comPortFactory ?? throw new ArgumentNullException(nameof(comPortFactory));
            _settings = settingsProvider.GetSettings<GSendSettings>(Constants.SettingsName);
            _gCodeParserFactory = gCodeParserFactory ?? throw new ArgumentNullException(nameof(gCodeParserFactory));

            notificationService.RegisterListener(this);
            _cancellationTokenSource = new CancellationTokenSource();
        }

        #region IProcessorMediator Methods

        public void OpenProcessors()
        {
            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                _machines.Clear();
                _logger.AddToLog(PluginManager.LogLevel.Information, nameof(OpenProcessors));
                IReadOnlyList<IMachine> machines = _gSendDataProvider.MachinesGet();

                foreach (IMachine machine in machines)
                {
                    IGCodeProcessor processor = new GCodeProcessor(_gSendDataProvider, machine, _comPortFactory, _serviceProvider, _settings);
                    _machines.Add(processor);
                    processor.TimeOut = TimeSpan.FromMilliseconds(_settings.ConnectTimeOut);
                }
            }
        }

        public void CloseProcessors()
        {
            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                _logger.AddToLog(PluginManager.LogLevel.Information, nameof(CloseProcessors));
                _machines.ForEach(m =>
                {
                    RemoveEventsFromProcessor(m);
                    m.Disconnect();
                });

                _machines.Clear();

                ThreadManager.CancelAll();
            }
        }

        public async Task ProcessClientCommunications(WebSocket webSocket, string clientId)
        {
            _clientId = clientId ?? "Unknwon";
            _machines.ForEach(m => AddEventsToProcessor(m));
            _webSocket = webSocket;
            byte[] receiveBuffer = new byte[1024];
            WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(receiveBuffer), CancellationToken);

            try
            {
                while (!receiveResult.CloseStatus.HasValue)
                {

                    string request = Encoding.UTF8.GetString(receiveBuffer);
                    byte[] sendBuffer;

                    sendBuffer = ProcessRequest(request[..receiveResult.Count]);

                    await webSocket.SendAsync(
                        new ArraySegment<byte>(sendBuffer, 0, sendBuffer.Length),
                        receiveResult.MessageType,
                        receiveResult.EndOfMessage,
                        CancellationToken);

                    receiveResult = await webSocket.ReceiveAsync(
                        new ArraySegment<byte>(receiveBuffer), CancellationToken);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(String.Format("Error Closing Client Comms: {0}", ex.Message));
            }
            finally
            {
                _machines.ForEach(m => RemoveEventsFromProcessor(m));
            }

            if (receiveResult.CloseStatus != null || receiveResult.CloseStatusDescription != null)
            {
                await webSocket.CloseOutputAsync(
                    receiveResult.CloseStatus.Value,
                    receiveResult.CloseStatusDescription,
                    CancellationToken);
            }

            _cancellationTokenSource.Cancel();

            _webSocket = null;
        }

        #endregion IProcessorMediator Methods

        #region IProcessorMediator Properties

        public IReadOnlyList<IGCodeProcessor> Machines => _machines.AsReadOnly();

        public CancellationToken CancellationToken => _cancellationTokenSource.Token;

        #endregion IProcessorMediator Properties

        #region INotificationListener

        public bool EventRaised(in string eventId, in object param1, in object param2, ref object result)
        {
            switch (eventId)
            {
                case Constants.NotificationMachineConnected:
                    int machineId = Convert.ToInt32(param1);
                    IGCodeProcessor processor = _machines.FirstOrDefault(m => m.Id.Equals(machineId));

                    if (processor == null)
                        result = false;
                    else
                        result = processor.IsConnected;

                    return true;

                default:
                    return false;
            }
        }

        public void EventRaised(in string eventId, in object param1, in object param2)
        {
            if (param1 == null || String.IsNullOrEmpty(eventId))
                return;

            long machineId = (long)param1;

            switch (eventId)
            {
                case Constants.NotificationMachineRemove:
                    RemoveMachine(machineId);

                    break;

                case Constants.NotificationMachineAdd:
                    AddMachine(machineId);

                    break;

                case Constants.NotificationMachineUpdated:
                    UpdateMachine(machineId);

                    break;
            }

        }

        private void AddMachine(long id)
        {
            if (_machines.Any(m => m.Id == id))
                return;

            IMachine newMachine = _gSendDataProvider.MachineGet(id);

            if (newMachine != null)
            {
                IGCodeProcessor processor = new GCodeProcessor(_gSendDataProvider, newMachine, _comPortFactory, _serviceProvider, _settings);
                _machines.Add(processor);
            }
        }

        private static void RemoveMachine(long machineId)
        {
            IGCodeProcessor machineToDelete = _machines.FirstOrDefault(m => m.Id.Equals(machineId));

            if (machineToDelete != null)
            {
                if (machineToDelete.IsConnected)
                    machineToDelete.Disconnect();

                _machines.Remove(machineToDelete);
            }
        }

        private void UpdateMachine(long machineId)
        {
            IGCodeProcessor processor = _machines.FirstOrDefault(m => m.Id.Equals(machineId));

            if (processor == null)
                return;

            IMachine updateMachine = _gSendDataProvider.MachineGet(machineId);

            if (updateMachine == null)
                return;

            processor.Machine.Options = updateMachine.Options;
            processor.Machine.Name = updateMachine.Name;
            processor.Machine.ComPort = updateMachine.ComPort;
            processor.Machine.ProbeCommand = updateMachine.ProbeCommand;
            processor.Machine.ProbeSpeed = updateMachine.ProbeSpeed;
            processor.Machine.ProbeThickness = updateMachine.ProbeThickness;
            processor.Machine.JogFeedrate = updateMachine.JogFeedrate;
            processor.Machine.JogUnits = updateMachine.JogUnits;
            processor.Machine.SpindleType = updateMachine.SpindleType;
            processor.Machine.SoftStartSeconds = updateMachine.SoftStartSeconds;
        }

        public List<string> GetEvents()
        {
            return new List<string>()
            {
                Constants.NotificationMachineAdd,
                Constants.NotificationMachineRemove,
                Constants.NotificationMachineUpdated,
                Constants.NotificationMachineConnected,
            };
        }

        #endregion INotificationListener

        #region Processor Events

        private void AddEventsToProcessor(IGCodeProcessor processor)
        {
            processor.OnConnect += Processor_OnConnect;
            processor.OnDisconnect += Processor_OnDisconnect;
            processor.OnStart += Processor_OnStart;
            processor.OnStop += Processor_OnStop;
            processor.OnPause += Processor_OnPause;
            processor.OnResume += Processor_OnResume;
            processor.OnCommandSent += Processor_OnCommandSent;
            processor.OnSerialError += Processor_OnSerialError;
            processor.OnSerialPinChanged += Processor_OnSerialPinChanged;
            processor.OnGrblError += Processor_OnGrblError;
            processor.OnGrblAlarm += Processor_OnGrblAlarm;
            processor.OnInvalidComPort += Processor_OnInvalidComPort;
            processor.OnComPortTimeOut += Processor_OnComPortTimeOut;
            processor.OnMachineStateChanged += Processor_OnMachineStateChanged;
            processor.OnMessageReceived += Processor_OnMessageReceived;
            processor.OnResponseReceived += Processor_OnResponseReceived;
            processor.OnLineStatusUpdated += Processor_OnLineStatusUpdated;
            processor.OnInformationUpdate += Processor_OnInformationUpdate;
        }

        private void RemoveEventsFromProcessor(IGCodeProcessor processor)
        {
            processor.OnConnect -= Processor_OnConnect;
            processor.OnDisconnect -= Processor_OnDisconnect;
            processor.OnStart -= Processor_OnStart;
            processor.OnStop -= Processor_OnStop;
            processor.OnPause -= Processor_OnPause;
            processor.OnResume -= Processor_OnResume;
            processor.OnCommandSent -= Processor_OnCommandSent;
            processor.OnSerialError -= Processor_OnSerialError;
            processor.OnSerialPinChanged -= Processor_OnSerialPinChanged;
            processor.OnGrblError -= Processor_OnGrblError;
            processor.OnGrblAlarm -= Processor_OnGrblAlarm;
            processor.OnInvalidComPort -= Processor_OnInvalidComPort;
            processor.OnComPortTimeOut -= Processor_OnComPortTimeOut;
            processor.OnMachineStateChanged -= Processor_OnMachineStateChanged;
            processor.OnMessageReceived -= Processor_OnMessageReceived;
            processor.OnResponseReceived -= Processor_OnResponseReceived;
            processor.OnLineStatusUpdated -= Processor_OnLineStatusUpdated;
            processor.OnInformationUpdate -= Processor_OnInformationUpdate;
        }

        private void Processor_OnComPortTimeOut(IGCodeProcessor sender, EventArgs e)
        {
            SendMessage(new ClientBaseMessage(Constants.ComPortTimeOut));
        }

        private void Processor_OnResponseReceived(IGCodeProcessor sender, string response)
        {
            SendMessage(new ClientBaseMessage("ResponseReceived", response));
        }

        private void Processor_OnMessageReceived(IGCodeProcessor sender, string message)
        {
            SendMessage(new ClientBaseMessage("MessageReceived", message));
        }

        private void Processor_OnMachineStateChanged(IGCodeProcessor sender, GSendShared.Models.MachineStateModel machineState)
        {
            TimeSpan span = DateTime.UtcNow - _lastSendStatus;

            if (span.TotalMilliseconds > DelayBetweenUpdatesSent)
            {
                SendMessage(new ClientBaseMessage(Constants.StateChanged, machineState));
                _lastSendStatus = DateTime.UtcNow;
            }
        }

        private void Processor_OnInvalidComPort(IGCodeProcessor sender, EventArgs e)
        {
            SendMessage(new ClientBaseMessage("InvalidComPort"));
        }

        private void Processor_OnGrblAlarm(IGCodeProcessor sender, GrblAlarm alarm)
        {
            SendMessage(new ClientBaseMessage("Alarm", alarm));
        }

        private void Processor_OnGrblError(IGCodeProcessor sender, GrblError errorCode)
        {
            SendMessage(new ClientBaseMessage("GrblError", errorCode));
        }

        private void Processor_OnSerialPinChanged(IGCodeProcessor sender, EventArgs e)
        {
            SendMessage(new ClientBaseMessage("SerialPinChanged"));
        }

        private void Processor_OnSerialError(IGCodeProcessor sender, EventArgs e)
        {
            SendMessage(new ClientBaseMessage("SerialError"));
        }

        private void Processor_OnResume(IGCodeProcessor sender, EventArgs e)
        {
            SendMessage(new ClientBaseMessage("Resume"));
        }

        private void Processor_OnStop(IGCodeProcessor sender, EventArgs e)
        {
            SendMessage(new ClientBaseMessage("Stop"));
        }

        private void Processor_OnStart(IGCodeProcessor sender, EventArgs e)
        {
            SendMessage(new ClientBaseMessage("Start"));
        }

        private void Processor_OnDisconnect(IGCodeProcessor sender, EventArgs e)
        {
            SendMessage(new ClientBaseMessage("Disconnect"));
        }

        private void Processor_OnPause(IGCodeProcessor sender, EventArgs e)
        {
            SendMessage(new ClientBaseMessage("Pause"));
        }

        private void Processor_OnConnect(IGCodeProcessor sender, EventArgs e)
        {
            SendMessage(new ClientBaseMessage("Connect"));
        }

        private void Processor_OnCommandSent(IGCodeProcessor sender, CommandSent e)
        {
            SendMessage(new ClientBaseMessage("CommandSent"));
        }

        private void Processor_OnLineStatusUpdated(int lineNumber, int masterLineNumber, LineStatus lineStatus)
        {
            SendMessage(new ClientBaseMessage(Constants.MessageLineStatusUpdated, new LineStatusUpdateModel(lineNumber, masterLineNumber, lineStatus)));
        }

        private void Processor_OnInformationUpdate(InformationType informationType, string message)
        {
            SendMessage(new ClientBaseMessage(Constants.MessageInformationUpdate, new InformationMessageModel(informationType, message)));
        }

        #endregion Processor Events

        #region Private Methods

        private void SendMessage(ClientBaseMessage clientBaseMessage)
        {
            if (!_processEvents)
                return;

            clientBaseMessage.ServerCpuStatus = ThreadManager.CpuUsage;

            byte[] json = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(clientBaseMessage, Constants.DefaultJsonSerializerOptions));

            _webSocket.SendAsync(new ArraySegment<byte>(json, 0, json.Length), WebSocketMessageType.Binary, true, CancellationToken).ConfigureAwait(false);
        }

        private byte[] ProcessRequest(string request)
        {
            if (_messageId > ulong.MaxValue - 50)
                _messageId = 0;

            ClientBaseMessage response = new()
            {
                request = request,
                ServerCpuStatus = ThreadManager.CpuUsage,
                Identifier = $"{_clientId}:{_messageId++}",
                IsLicensed = _isLicensed,
            };

            TimeSpan timeFromLastLicenseCheck = DateTime.UtcNow - _lastLicenseCheck;

            if (timeFromLastLicenseCheck.TotalMinutes > 5)
            {
                ILicenseFactory licenseFactory = _serviceProvider.GetRequiredService<ILicenseFactory>();

                ILicense license = licenseFactory.GetActiveLicense();

                _isLicensed = license != null && licenseFactory.LicenseIsValid(license);

                _lastLicenseCheck = DateTime.UtcNow;
            }

            if (_isLicensed)
            {
                if (String.IsNullOrEmpty(request))
                    request = Constants.MessageMachineStatusAll;

                long machineId = -1;
                string[] parts = request.Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                bool foundMachine = parts.Length > 1 && Int64.TryParse(parts[1], out machineId);
                IGCodeProcessor proc = null;

                if (foundMachine)
                    proc = _machines.FirstOrDefault(m => m.Id.Equals(machineId));

                switch (parts[0])
                {
                    case Constants.MessageMachineJogStartServer:
                        if (foundMachine && proc != null && parts.Length == 5)
                        {
                            if (Enum.TryParse(parts[2], true, out JogDirection jogDirection) &&
                                Double.TryParse(parts[3], out double stepSize) &&
                                Double.TryParse(parts[4], out double feedRate))
                            {
                                proc.JogStart(jogDirection, stepSize, feedRate);
                            }
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageMachineJogStopServer:
                        if (foundMachine && proc != null)
                        {
                            proc.JogStop();
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageMachineResumeAll:
                        foreach (IGCodeProcessor processor in _machines)
                        {
                            if (processor.IsConnected && processor.IsPaused)
                                processor.Resume();
                        }

                        break;

                    case Constants.MessageMachinePauseAll:
                        foreach (IGCodeProcessor processor in _machines)
                        {
                            if (processor.IsConnected)
                                processor.Pause();
                        }

                        break;

                    case Constants.MessageMachineClearAlarmServer:
                        if (foundMachine && proc != null)
                        {
                            if (proc.StateModel.MachineState == MachineState.Alarm)
                                proc.Unlock();
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageMachineConnectServer:
                        response.request = Constants.MessageMachineConnectServer;

                        if (foundMachine && proc != null)
                        {
                            if (_isLicensed && !proc.IsConnected)
                            {
                                response.message = (int)proc.Connect();
                                response.success = true;
                            }
                            else
                            {
                                response.success = false;
                            }
                        }
                        else
                        {
                            response.success = false;
                            response.message = (int)ConnectResult.Error;
                        }

                        break;
                    case Constants.MessageMachineDisconnectServer:
                        response.request = Constants.MessageMachineConnectServer;

                        if (foundMachine && proc != null)
                        {
                            if (proc.IsConnected)
                            {
                                response.message = proc.Disconnect() ? (int)ConnectResult.Success : (int)ConnectResult.Error;
                                response.success = true;
                            }
                            else
                            {
                                response.success = false;
                            }
                        }
                        else
                        {
                            response.success = false;
                            response.message = (int)ConnectResult.Error;
                        }

                        break;

                    case Constants.MessageMachineResumeServer:
                        if (foundMachine && proc != null)
                        {
                            if (proc.StateModel.MachineState != MachineState.Alarm)
                                proc.Resume();
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageMachineHomeServer:
                        if (foundMachine && proc != null)
                        {
                            if (proc.StateModel.MachineState != MachineState.Alarm)
                                proc.Home();
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageMachineProbeServer:
                        if (foundMachine && proc != null)
                        {
                            response.success = proc.Probe();
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageMachinePauseServer:
                        if (foundMachine && proc != null)
                        {
                            response.success = proc.Pause();
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageMachineStopServer:
                        if (foundMachine && proc != null)
                        {
                            response.success = proc.Stop();
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageMachineWriteLineServer:
                        if (foundMachine && proc != null)
                        {
                            response.success = proc.WriteLine(parts[2].Replace("\t", ":"));
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageMachineWriteLineServerR:
                        response.request = Constants.MessageMachineWriteLineServerR;
                        if (foundMachine && proc != null)
                        {
                            response.message = proc.SendCommandWaitForOKCommand(parts[2]);
                            response.success = true;
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageMachineUpdateOverridesAdmin:
                        response.request = Constants.MessageMachineUpdateOverridesAdmin;
                        if (foundMachine && proc != null)
                        {
                            proc.MachineOverrides = JsonSerializer.Deserialize<OverrideModel>(Encoding.UTF8.GetString(Convert.FromBase64String(parts[2])), Constants.DefaultJsonSerializerOptions);
                            response.success = true;
                        }
                        else
                        {
                            response.success = false;
                        }
                        break;

                    case Constants.MessageMachineSpindleAdmin:
                        if (foundMachine && proc != null && parts.Length == 4)
                        {
                            if (Int32.TryParse(parts[2], out int speed))
                            {
                                if (Boolean.TryParse(parts[3], out bool clockWise))
                                {
                                    response.success = proc.UpdateSpindleSpeed(speed, !clockWise);
                                }
                                else
                                {
                                    response.success = false;
                                }
                            }
                            else
                            {
                                response.success = false;
                            }
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case "mAddEvents":
                        if (foundMachine && proc != null)
                        {
                            _processEvents = true;
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case "mRemoveEvents":
                        if (foundMachine && proc != null)
                        {
                            _processEvents = false;
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageMachineSetZeroServer:
                        if (foundMachine && proc != null && parts.Length == 4 &&
                            Int32.TryParse(parts[2], out int zeroEnumInt) &&
                            Int32.TryParse(parts[3], out int coordinateSystem))
                        {
                            ZeroAxis zeroAxis = (ZeroAxis)zeroEnumInt;
                            response.success = proc.ZeroAxes(zeroAxis, coordinateSystem);
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageToggleSimulationServer:
                        response.request = Constants.MessageToggleSimulation;

                        if (foundMachine && proc != null)
                        {
                            response.success = proc.ToggleSimulation();
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;


                    case Constants.MessageMachineUpdateSettingServer:

                        response.request = Constants.MessageMachineUpdateSettingServer;

                        if (foundMachine && proc != null)
                        {
                            response.message = (int)proc.UpdateSetting(parts[2]);
                            response.success = true;
                        }
                        else
                        {
                            response.success = false;
                            response.message = (int)UpdateSettingResult.Error;
                        }

                        break;

                    case Constants.MessageMachineStatusServer:
                        if (foundMachine && proc != null)
                        {
                            response.message = proc.StateModel;
                            response.request = Constants.MessageMachineStatusServer;
                            response.success = true;
                            response.IsConnected = proc.IsConnected;
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageMachineStatusAll:
                        List<StatusResponseMessage> machineStates = new();

                        foreach (IGCodeProcessor processor in _machines)
                        {
                            machineStates.Add(new StatusResponseMessage()
                            {
                                Id = processor.Id,
                                Connected = processor.IsConnected,
                                State = processor.StateModel.MachineState.ToString(),
                                CpuStatus = processor.Cpu,
                                UpdatedConfiguration = processor.StateModel.UpdatedGrblConfiguration.Count > 0,
                            });
                        }

                        response.message = machineStates;
                        response.success = true;
                        break;

                    case Constants.MessageLoadGCodeAdmin:

                        response.request = Constants.MessageLoadGCodeAdmin;

                        string fileName = Encoding.UTF8.GetString(Convert.FromBase64String(parts[2]));

                        if (foundMachine && proc != null && File.Exists(fileName))
                        {
                            IGCodeParser gCodeParser = _gCodeParserFactory.CreateParser();
                            IGCodeAnalyses gCodeAnalyses = gCodeParser.Parse(File.ReadAllText(fileName));
                            gCodeAnalyses.Analyse();
                            response.message = proc.LoadGCode(gCodeAnalyses);

                            response.success = true;
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageUnloadGCodeAdmin:

                        response.request = Constants.MessageUnloadGCodeAdmin;

                        if (foundMachine && proc != null)
                        {
                            proc.Clear();
                            response.success = true;
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    case Constants.MessageRunGCodeAdmin:

                        response.request = Constants.MessageRunGCodeAdmin;

                        if (foundMachine && proc != null && parts.Length == 4)
                        {
                            long toolProfileId = Convert.ToInt64(parts[2]);
                            long jobProfileId = Convert.ToInt64(parts[3]);

                            IGSendDataProvider gSendDataProvider = _serviceProvider.GetRequiredService<IGSendDataProvider>();
                            IJobExecution jobExecution = gSendDataProvider.JobExecutionCreate(machineId, toolProfileId, jobProfileId);

                            proc.Start(jobExecution);
                            response.success = true;
                        }
                        else
                        {
                            response.success = false;
                        }

                        break;

                    default:
                        response.message = "Invalid Request";
                        break;

                }
            }
            else
            {
                response.success = false;
            }

            byte[] json = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response, Constants.DefaultJsonSerializerOptions));

            if (json.Length > BufferSize)
                throw new InvalidOperationException();

            return json;
        }

        #endregion Private Methods
    }
}
