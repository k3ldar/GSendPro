using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

using GSendShared;

using Languages;

using PluginManager.Abstractions;

using Shared.Classes;

using GSendCommon;

namespace GSendCommon
{
    public class ProcessorMediator : IProcessorMediator, INotificationListener
    {
        private const int BufferSize = 8192;
        private readonly object _lockObject = new();
        private readonly ILogger _logger;
        private readonly IMachineProvider _machineProvider;
        private readonly IComPortFactory _comPortFactory;
        private readonly List<IGCodeProcessor> _machines = new();
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly GSendSettings _settings;

        public ProcessorMediator(ILogger logger, 
            IMachineProvider machineProvider,
            IComPortFactory comPortFactory, 
            INotificationService notificationService,
            ISettingsProvider settingsProvider)
        {
            if (notificationService == null)
                throw new ArgumentNullException(nameof(notificationService));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _machineProvider = machineProvider ?? throw new ArgumentNullException(nameof(machineProvider));
            _comPortFactory = comPortFactory ?? throw new ArgumentNullException(nameof(comPortFactory));
            _settings = settingsProvider.GetSettings<GSendSettings>(Constants.SettingsName);
            notificationService.RegisterListener(this);
            _cancellationTokenSource = new CancellationTokenSource();
        }

        #region IProcessorMediator Methods

        public void OpenProcessors()
        {
            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                _logger.AddToLog(PluginManager.LogLevel.Information, nameof(OpenProcessors));
                IReadOnlyList<IMachine> machines = _machineProvider.MachinesGet();

                foreach (IMachine machine in machines)
                {
                    IGCodeProcessor processor = new GCodeProcessor(machine, _comPortFactory);
                    _machines.Add(processor);
                    processor.TimeOut = TimeSpan.FromMilliseconds(_settings.SendTimeOut);
                    AddEventsToProcessor(processor);
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
                    m.Disconnect();
                    RemoveEventsFromProcessor(m);
                });

                _machines.Clear();

                ThreadManager.CancelAll();
            }
        }

        public async Task ProcessClientCommunications(WebSocket webSocket)
        {
            byte[] receiveBuffer = new byte[1024];
            WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(receiveBuffer), CancellationToken);

            while (!receiveResult.CloseStatus.HasValue)
            {

                string request = Encoding.UTF8.GetString(receiveBuffer);
                byte[] sendBuffer;

                using (TimedLock tl = TimedLock.Lock(_lockObject))
                {
                    sendBuffer = ProcessRequest(request[..receiveResult.Count]);
                }

                await webSocket.SendAsync(
                    new ArraySegment<byte>(sendBuffer, 0, sendBuffer.Length),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    CancellationToken);

                receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(receiveBuffer), CancellationToken);
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                CancellationToken);
        }

        #endregion IProcessorMediator Methods

        #region IProcessorMediator Properties

        public IReadOnlyList<IGCodeProcessor> Machines => _machines.AsReadOnly();

        public CancellationToken CancellationToken => _cancellationTokenSource.Token;

        #endregion IProcessorMediator Properties

        #region INotificationListener

        public bool EventRaised(in string eventId, in object param1, in object param2, ref object result)
        {
            return false;
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
                    RemoveMachine(machineId);
                    AddMachine(machineId);

                    break;
            }

        }

        private void AddMachine(long id)
        {
            if (_machines.Any(m => m.Id == id))
                return;

            IMachine newMachine = _machineProvider.MachineGet(id);

            if (newMachine != null)
            {
                IGCodeProcessor processor = new GCodeProcessor(newMachine, _comPortFactory);
                _machines.Add(processor);
            }
        }

        private void RemoveMachine(long machineId)
        {
            IGCodeProcessor machineToDelete = _machines.Where(m => m.Id.Equals(machineId)).FirstOrDefault();

            if (machineToDelete != null)
            {
                if (machineToDelete.IsConnected)
                    machineToDelete.Disconnect();

                _machines.Remove(machineToDelete);
            }
        }

        public List<string> GetEvents()
        {
            return new List<string>()
            {
                Constants.NotificationMachineAdd,
                Constants.NotificationMachineRemove,
                Constants.NotificationMachineUpdated
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
            processor.OnBufferSizeChanged += Processor_OnBufferSizeChanged;
            processor.OnSerialError += Processor_OnSerialError;
            processor.OnSerialPinChanged += Processor_OnSerialPinChanged;
            processor.OnGrblError += Processor_OnGrblError;
            processor.OnGrblAlarm += Processor_OnGrblAlarm;
            processor.OnInvalidComPort += Processor_OnInvalidComPort;
            processor.OnMachineStateChanged += Processor_OnMachineStateChanged;
            processor.OnMessageReceived += Processor_OnMessageReceived;
            processor.OnResponseReceived += Processor_OnResponseReceived;
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
            processor.OnBufferSizeChanged -= Processor_OnBufferSizeChanged;
            processor.OnSerialError -= Processor_OnSerialError;
            processor.OnSerialPinChanged -= Processor_OnSerialPinChanged;
            processor.OnGrblError -= Processor_OnGrblError;
            processor.OnGrblAlarm -= Processor_OnGrblAlarm;
            processor.OnInvalidComPort -= Processor_OnInvalidComPort;
            processor.OnMachineStateChanged -= Processor_OnMachineStateChanged;
            processor.OnMessageReceived -= Processor_OnMessageReceived;
            processor.OnResponseReceived -= Processor_OnResponseReceived;
        }

        private void Processor_OnResponseReceived(IGCodeProcessor sender, string response)
        {

        }

        private void Processor_OnMessageReceived(IGCodeProcessor sender, string message)
        {

        }

        private void Processor_OnMachineStateChanged(IGCodeProcessor sender, GSendShared.Models.MachineStateModel machineState)
        {

        }

        private void Processor_OnInvalidComPort(IGCodeProcessor sender, EventArgs e)
        {

        }

        private void Processor_OnGrblAlarm(IGCodeProcessor sender, GrblAlarm alarm)
        {

        }

        private void Processor_OnGrblError(IGCodeProcessor sender, GrblError errorCode)
        {

        }

        private void Processor_OnSerialPinChanged(IGCodeProcessor sender, EventArgs e)
        {

        }

        private void Processor_OnSerialError(IGCodeProcessor sender, EventArgs e)
        {

        }

        private void Processor_OnResume(IGCodeProcessor sender, EventArgs e)
        {

        }

        private void Processor_OnStop(IGCodeProcessor sender, EventArgs e)
        {

        }

        private void Processor_OnStart(IGCodeProcessor sender, EventArgs e)
        {

        }

        private void Processor_OnDisconnect(IGCodeProcessor sender, EventArgs e)
        {

        }

        private void Processor_OnPause(IGCodeProcessor sender, EventArgs e)
        {

        }

        private void Processor_OnConnect(IGCodeProcessor sender, EventArgs e)
        {

        }

        private void Processor_OnCommandSent(IGCodeProcessor sender, CommandSent e)
        {

        }

        private void Processor_OnBufferSizeChanged(IGCodeProcessor sender, int size)
        {

        }


        #endregion Processor Events

        #region Private Methods

        private byte[] ProcessRequest(string request)
        {
            ClientBaseMessage response = new()
            {
                request = request,
                ServerCpuStatus = ThreadManager.CpuUsage,
            };

            if (String.IsNullOrEmpty(request))
                request = "mStatus";

            long machineId = -1;
            string[] parts = request.Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            bool foundMachine = parts.Length > 1 ? Int64.TryParse(parts[1], out machineId) : false;
            IGCodeProcessor proc = null;
            
            if (foundMachine)
                proc = _machines.FirstOrDefault(m => m.Id.Equals(machineId));

            switch (parts[0])
            {
                case "mResumeAll":
                    foreach (IGCodeProcessor processor in _machines)
                    {
                        if (processor.IsConnected && processor.IsPaused)
                            processor.Resume();
                    }

                    break;

                case "mPauseAll":
                    foreach (IGCodeProcessor processor in _machines)
                    {
                        if (processor.IsConnected)
                            processor.Pause();
                    }

                    break;

                case "mClearAlm":
                    if (foundMachine && proc != null)
                    {
                        if (proc.StateModel.MachineState == MachineState.Alarm)
                            proc.Unlock();
                    }

                    break;

                case "mConnect":
                    if (foundMachine && proc != null)
                    {
                        if (proc.IsConnected)
                            proc.Disconnect();
                        else
                            proc.Connect();
                    }

                    break;

                case "mResume":
                    if (foundMachine && proc != null)
                    {
                        if (proc.StateModel.MachineState != MachineState.Alarm)
                            proc.Resume();
                    }

                    break;

                case "mHome":
                    if (foundMachine && proc != null)
                    {
                        if (proc.StateModel.MachineState != MachineState.Alarm)
                            proc.Home();
                    }

                    break;

                case "mStatus":
                    List<StatusResponseMessage> machineStates = new();

                    foreach (IGCodeProcessor processor in _machines)
                    {
                        machineStates.Add(new StatusResponseMessage()
                            {
                                Id = processor.Id,
                                Connected = processor.IsConnected,
                                State =  processor.StateModel.MachineState.ToString(),
                                CpuStatus = processor.Cpu,
                            });
                    }

                    response.message = machineStates;
                    response.success = true;
                    break;

                default:
                    response.message = "Invalid Request";
                    break;

            }

            byte[] json = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));

            if (json.Length > BufferSize)
                throw new InvalidOperationException();

            return json;
        }

        #endregion Private Methods
    }
}
