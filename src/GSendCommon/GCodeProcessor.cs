using System.Diagnostics;
using System.Reflection;
using System.Text;

using GSendAnalyser.Internal;

using GSendCommon;

using GSendShared;
using GSendShared.Attributes;
using GSendShared.Interfaces;
using GSendShared.Models;

using Shared.Classes;

namespace GSendCommon
{
    public class GCodeProcessor : ThreadManager, IGCodeProcessor
    {
        private const int QueueProcessMilliseconds = 20;
        private const int MaxBufferSize = 120;
        private const int FirstCommand = 0;

        private const string ResultOk = "ok";
        private const string CommandHome = "$H";
        private const string CommandHelp = "$";
        private const string CommandSettings = "$$";
        private const string CommandUnlock = "$X";
        private const string CommandSpindleStartClockWise = "S{0}M3";
        private const string CommandSpindleStartCounterClockWise = "S{0}M4";
        private const string CommandStopSpindle = "M5";
        private const string CommandMistCoolantOn = "M7";
        private const string CommandFloodCoolantOn = "M8";
        private const string CommandCoolantOff = "M9";
        private const string CommandZero = "G10 P{0} L20";
        private const string CommandZeroAxis = " {0}0";
        private const string StatusIdle = "Idle";
        private const string CommandGetStatus = "$I";
        private const string CommandStartResume = "~";
        private const string StatusMistEnabled = "M";
        private const string CommandPause = "!";
        private const char SeparatorEquals = '=';
        private const string CommandRequestStatus = "?";
        private const string SeparatorPipe = "|";
        private const string SeparatorColon = ":";
        private const string SeparatorComma = ",";
        private const string OptionBuffer = "bf";
        private const string OptionMachinePosition = "mpos";
        private const string OptionLineNumber = "ln";
        private const string OptionOffsets = "wco";
        private const string OptionWorkPosition = "wpos";
        private const string OptionFeedSpeed = "fs";
        private const string OptionFeedRate = "f";
        private const string OptionOverrides = "ov";
        private const string OptionAccessoryState = "a";
        private const string JogNumberFormat = "F4";
        private static readonly TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(30);

        private List<IGCodeLine> _commandsToSend = null;
        private readonly Queue<IGCodeLine> _sendQueue = new();

        private readonly IMachineProvider _machineProvider;
        private readonly IMachine _machine;
        private readonly IComPort _port;
        private readonly IGCodeParser _gcodeParser = new GCodeParser();
        private volatile bool _isRunning;
        private volatile bool _isPaused;
        private volatile bool _isAlarm;
        private volatile bool _waitingForResponse;
        private int _currentBufferSize = 0;
        private DateTime _lastInformationCheck = DateTime.MinValue;
        private readonly object _lockObject = new();
        private readonly MachineStateModel _machineStateModel = new();
        private readonly IGCodeOverrideContext _overrideContext;

        private int _lineCount = 0;
        private bool _initialising = false;


        #region Constructors

        public GCodeProcessor(IMachineProvider machineProvider, IMachine machine, 
            IComPortFactory comPortFactory, IServiceProvider serviceProvider)
            : base(machine, TimeSpan.FromMilliseconds(QueueProcessMilliseconds))
        {
            _machineProvider = machineProvider ?? throw new ArgumentNullException(nameof(machineProvider));

            if (comPortFactory == null)
                throw new ArgumentNullException(nameof(comPortFactory));

            base.HangTimeout = int.MaxValue;
            _machine = machine ?? throw new ArgumentNullException(nameof(machine));
            _port = comPortFactory.CreateComPort(_machine);
            _port.DataReceived += Port_DataReceived;
            _port.ErrorReceived += Port_ErrorReceived;
            _port.PinChanged += Port_PinChanged;
            _overrideContext = new GCodeOverrideContext(serviceProvider, new StaticMethods(), this, _machine, _port);
            ThreadManager.ThreadStart(this, $"{machine.Name} - {machine.ComPort}", ThreadPriority.Normal);
        }

        #endregion Constructors

        #region Public Methods

        public void ProcessNextCommand()
        {
            if (_waitingForResponse)
                return;

            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                if (NextCommand < CommandCount)
                {
                    IGCodeLine commandToSend = _commandsToSend[NextCommand];

                    string commandText = commandToSend.GetGCode();

                    if (String.IsNullOrEmpty(commandText))
                    {
                        NextCommand++;
                        return;
                    }

                    if (BufferSize + commandText.Length > MaxBufferSize)
                        return;

                    _sendQueue.Enqueue(commandToSend);
                    Trace.WriteLine($"Line {NextCommand} added to queue");

                    commandToSend.Status = LineStatus.Sent;

                    BufferSize += commandText.Length;

                    InternalWriteLine(commandToSend);

                    NextCommand++;
                }
                else if (!IsPaused && _sendQueue.Count == 0 && _machineStateModel.MachineState.Equals(StatusIdle))
                {
                    Stop();
                }
            }
        }

        #endregion Public Methods

        #region IGCodeProcessor Methods

        public ConnectResult Connect()
        {
            if (_port.IsOpen())
                return ConnectResult.AlreadyConnected;


            _initialising = true;
            try
            {
                _isAlarm = false;
                _port.Open();
                OnConnect?.Invoke(this, EventArgs.Empty);
                string connectMessage = SendCommandWaitForOKCommand(CommandGetStatus).Trim();

                if (connectMessage.Length > 2)
                    connectMessage = connectMessage[1..^1];

                ProcessMessageResponse(connectMessage);
                ValidateGrblSettings();
                Trace.WriteLine($"Connect: {_port.IsOpen()}");
                _machineStateModel.IsConnected = _port.IsOpen();
                return ConnectResult.Success;
            }
            catch (TimeoutException)
            {
                if (_port.IsOpen())
                    _port.Close();

                return ConnectResult.TimeOut;
            }
            catch (Exception err)
            {
                Trace.WriteLine($"Connect Error: {err.Message}");
                OnInvalidComPort?.Invoke(this, EventArgs.Empty);

                return ConnectResult.Error;
            }
            finally
            {
                _initialising = false;
            }
        }

        public bool Disconnect()
        {
            if (!_port.IsOpen())
                return true;

            _port.Close();
            OnDisconnect?.Invoke(this, EventArgs.Empty);
            _isAlarm = false;
            Trace.WriteLine("Disconnect");

            _machineStateModel.IsConnected = _port.IsOpen();
            _machineStateModel.UpdatedGrblConfiguration.Clear();

            return !_port.IsOpen();
        }

        public bool Start()
        {
            if (!IsConnected)
                return false;

            if (_isRunning)
                return true;

            NextCommand = FirstCommand;
            _isRunning = true;
            _isPaused = false;

            Trace.WriteLine("Start");
            _port.WriteLine(CommandStartResume);
            OnStart?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool Pause()
        {
            _isPaused = true;
            OnPause?.Invoke(this, EventArgs.Empty);

            Trace.WriteLine("Pause");
            _port.WriteLine(CommandPause);
            return true;
        }

        public bool Resume()
        {
            _isPaused = false;
            OnResume?.Invoke(this, EventArgs.Empty);

            Trace.WriteLine($"Resume");
            _port.WriteLine(CommandStartResume);
            return true;
        }

        public bool Stop()
        {
            Trace.WriteLine("Stop");
            InternalWriteByte(new byte[] { 0x85 });
            _overrideContext.Cancel();

            _isRunning = false;
            _isPaused = false;
            OnStop?.Invoke(this, EventArgs.Empty);
            NextCommand = FirstCommand;
            _sendQueue.Clear();


            return true;
        }

        public void Clear()
        {
            Trace.WriteLine("Clear");
            _commandsToSend?.Clear();
            NextCommand = FirstCommand;
        }

        public bool LoadGCode(IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new InvalidOperationException(nameof(gCodeAnalyses));

            Trace.WriteLine("Load GCode");

            Clear();

            _commandsToSend = gCodeAnalyses.Lines(out _lineCount);
            //_lineCount = 0;
            //GCodeLine currentLine = null;

            //foreach (IGCodeCommand command in gCodeAnalyses.Commands)
            //{
            //    if (command.LineNumber > _lineCount)
            //    {
            //        currentLine = new();
            //        _lineCount++;
            //        _commandsToSend.Add(currentLine);
            //    }

            //    currentLine.Commands.Add(command);
            //}

            return true;
        }

        public bool ZeroAxes(ZeroAxis axis, int coordinateSystem)
        {
            string zeroCommand = String.Format(CommandZero, coordinateSystem);

            if (axis == ZeroAxis.X || axis == ZeroAxis.All)
                zeroCommand += String.Format(CommandZeroAxis, ZeroAxis.X);

            if (axis == ZeroAxis.Y || axis == ZeroAxis.All)
                zeroCommand += String.Format(CommandZeroAxis, ZeroAxis.Y);

            if (axis == ZeroAxis.Z || axis == ZeroAxis.All)
                zeroCommand += String.Format(CommandZeroAxis, ZeroAxis.Z);

            if (zeroCommand.Equals(CommandZero))
                return false;

            SendCommandWaitForResponse(zeroCommand, TimeOut);
            OnCommandSent?.Invoke(this, CommandSent.ZeroAxis);
            return true;
        }

        public bool Home()
        {
            SendCommandWaitForResponse(CommandHome, HomingTimeout);
            Trace.WriteLine("Home");
            OnCommandSent?.Invoke(this, CommandSent.Home);
            return true;
        }

        public bool Probe()
        {
            if (String.IsNullOrEmpty(_machine.ProbeCommand))
                return false;

            IGCodeAnalyses gCodeAnalyses = _gcodeParser.Parse(_machine.ProbeCommand);
            LoadGCode(gCodeAnalyses);

            Start();

            return true;
        }

        public string Help()
        {
            return SendCommandWaitForOKCommand(CommandHelp);
        }

        public bool Unlock()
        {
            if (!_isAlarm)
                return false;

            SendCommandWaitForResponse(CommandUnlock, TimeOut);
            _isAlarm = false;
            Trace.WriteLine("Unlock");
            OnCommandSent?.Invoke(this, CommandSent.Unlock);
            return true;
        }

        public bool JogStart(JogDirection jogDirection, double stepSize, double feedRate)
        {
            StringBuilder jogCommand = new("$J=G20G91");
            decimal maxZTravel = Convert.ToDecimal(stepSize);
            decimal maxXTravel = Convert.ToDecimal(stepSize);
            decimal maxYTravel = Convert.ToDecimal(stepSize);

            if (stepSize < 0.01)
            {
                maxZTravel = _machine.Settings.MaxTravelX - Convert.ToDecimal(StateModel.MachineZ);
                maxXTravel = _machine.Settings.MaxTravelY - Convert.ToDecimal(StateModel.MachineX);
                maxYTravel = _machine.Settings.MaxTravelZ - Convert.ToDecimal(StateModel.MachineY);
            }

            switch (jogDirection)
            {
                case JogDirection.ZPlus:
                    jogCommand.AppendFormat("Z{0}", maxZTravel.ToString(JogNumberFormat));

                    break;

                case JogDirection.ZMinus:
                    jogCommand.AppendFormat("Z{0}", (0 - maxZTravel).ToString(JogNumberFormat));

                    break;

                case JogDirection.XPlus:
                    jogCommand.AppendFormat("X{0}", maxXTravel.ToString(JogNumberFormat));

                    break;

                case JogDirection.XMinus:
                    jogCommand.AppendFormat("X{0}", (0 - maxXTravel).ToString(JogNumberFormat));

                    break;

                case JogDirection.YPlus:
                    jogCommand.AppendFormat("Y{0}", maxYTravel.ToString(JogNumberFormat));

                    break;

                case JogDirection.YMinus:
                    jogCommand.AppendFormat("Y{0}", (0 - maxYTravel).ToString(JogNumberFormat));

                    break;

                case JogDirection.XPlusYMinus:
                    jogCommand.AppendFormat("X{0}", maxXTravel.ToString(JogNumberFormat));
                    jogCommand.AppendFormat("Y{0}", (0 - maxYTravel).ToString(JogNumberFormat));

                    break;

                case JogDirection.XPlusYPlus:
                    jogCommand.AppendFormat("X{0}", maxXTravel.ToString(JogNumberFormat));
                    jogCommand.AppendFormat("Y{0}", maxYTravel.ToString(JogNumberFormat));

                    break;

                case JogDirection.XMinusYMinus:
                    jogCommand.AppendFormat("X{0}", (0 - maxXTravel).ToString(JogNumberFormat));
                    jogCommand.AppendFormat("Y{0}", (0 - maxYTravel).ToString(JogNumberFormat));

                    break;

                case JogDirection.XMinusYPlus:
                    jogCommand.AppendFormat("X{0}", (0 - maxXTravel).ToString(JogNumberFormat));
                    jogCommand.AppendFormat("Y{0}", maxYTravel.ToString(JogNumberFormat));

                    break;
            }

            jogCommand.AppendFormat("F{0}", feedRate);

            _port.WriteLine(jogCommand.ToString());
            return true;
        }

        public bool JogStop()
        {
            InternalWriteByte(new byte[] { 0x85 });
            return false;
        }

        public bool WriteLine(string gCode)
        {
            if (IsConnected && !IsPaused && !IsRunning)
            {
                InternalWriteLine(gCode);
                return true;
            }

            return false;
        }

        public Dictionary<int, object> Settings()
        {
            Dictionary<int, object> Result = new();

            string allSettings = SendCommandWaitForOKCommand(CommandSettings);

            string[] settings = allSettings.Trim().Split('\r', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in settings)
            {
                string[] parts = s.Split(SeparatorEquals, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                if (!Int32.TryParse(parts[0][1..], out int settingValue))
                    throw new InvalidCastException("Setting not recognized");

                if (!Decimal.TryParse(parts[1], out decimal value))
                    throw new InvalidCastException("Setting value not recognized");

                Result[settingValue] = value;
            }


            return Result;
        }

        public UpdateSettingResult UpdateSetting(string updateCommand)
        {
            _initialising = true;
            try
            {
                using (TimedLock tl = TimedLock.Lock(_lockObject))
                {
                    string response = SendCommandWaitForOKCommand(updateCommand);

                    if (response.Trim().Equals(String.Empty))
                    {
                        object[] parts = updateCommand.Split(Constants.EqualsChar,
                            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                        if (Int32.TryParse(parts[0].ToString()[1..], out int settingValue))
                        {
                            PropertyInfo propertyInfo = GetPropertyWithAttributeValue(settingValue);

                            if (propertyInfo != null)
                            {
                                if (propertyInfo.PropertyType == typeof(bool))
                                {
                                    int intValue = Convert.ToInt32(parts[1]);
                                    bool boolValue = (int)intValue != 0;
                                    propertyInfo.SetValue(_machine.Settings, boolValue, null);
                                }
                                else if (propertyInfo.PropertyType.Equals(typeof(AxisConfiguration)))
                                {
                                    int intEnum = Convert.ToInt32(parts[1]);
                                    AxisConfiguration axisConfiguration = (AxisConfiguration)intEnum;
                                    propertyInfo.SetValue(_machine.Settings, axisConfiguration, null);
                                }
                                else if (propertyInfo.PropertyType.Equals(typeof(FeedbackUnit)))
                                {
                                    int intEnum = Convert.ToInt32(parts[1]);
                                    FeedbackUnit feedbackUnit = (FeedbackUnit)intEnum;
                                    propertyInfo.SetValue(_machine.Settings, feedbackUnit, null);
                                }
                                else if (propertyInfo.PropertyType.Equals(typeof(ReportType)))
                                {
                                    int intEnum = Convert.ToInt32(parts[1]);
                                    ReportType reportType = (ReportType)intEnum;
                                    propertyInfo.SetValue(_machine.Settings, reportType, null);
                                }
                                else if (propertyInfo.PropertyType.Equals(typeof(Pin)))
                                {
                                    int intEnum = Convert.ToInt32(parts[1]);
                                    Pin pin = (Pin)intEnum;
                                    propertyInfo.SetValue(_machine.Settings, pin, null);
                                }
                                else if (propertyInfo.PropertyType == typeof(decimal))
                                {
                                    decimal decimalValue = Convert.ToDecimal(parts[1]);
                                    propertyInfo.SetValue(_machine.Settings, decimalValue, null);
                                }
                                else if (propertyInfo.PropertyType == typeof(UInt32))
                                {
                                    uint uintValue = Convert.ToUInt32(parts[1]);
                                    propertyInfo.SetValue(_machine.Settings, uintValue, null);
                                }

                                _machineProvider.MachineUpdate(_machine);
                            }
                        }

                        return UpdateSettingResult.Success;
                    }
                    else
                        return UpdateSettingResult.Failed;
                }
            }
            catch (TimeoutException)
            {
                return UpdateSettingResult.Timeout;
            }
            catch (Exception)
            {
                return UpdateSettingResult.Error;
            }
            finally
            {
                _initialising = false;
            }
        }

        public bool UpdateSpindleSpeed(int speed, bool clockWise)
        {
            if (speed < 0)
                return false;

            if (speed > 0)
            {
                if (clockWise)
                    InternalWriteLine(String.Format(CommandSpindleStartClockWise, speed));
                else
                    InternalWriteLine(String.Format(CommandSpindleStartCounterClockWise, speed));

                OnCommandSent?.Invoke(this, CommandSent.SpindleSpeedSet);
            }
            else
            {
                _overrideContext.Cancel();
                InternalWriteLine(CommandStopSpindle);
                OnCommandSent?.Invoke(this, CommandSent.SpindleOff);
            }

            return true;
        }

        public bool TurnMistCoolantOn()
        {
            if (!_machineStateModel.MistEnabled)
            {
                InternalWriteLine(CommandMistCoolantOn);
                OnCommandSent?.Invoke(this, CommandSent.MistOn);
                return true;
            }

            return false;
        }

        public bool TurnFloodCoolantOn()
        {
            if (!FloodCoolantActive)
            {
                InternalWriteLine(CommandFloodCoolantOn);
                OnCommandSent?.Invoke(this, CommandSent.FloodOn);
                return true;
            }

            return false;
        }

        public bool CoolantOff()
        {
            InternalWriteLine(CommandCoolantOff);
            OnCommandSent?.Invoke(this, CommandSent.CoolantOff);
            return true;
        }

        #endregion IGCodeProcessor Methods

        #region IGCodeProcessor Properties

        public long Id => _machine.Id;

        public IMachine Machine => _machine;

        public string Cpu => $"{ProcessCpuUsage.ToString("n1")}%/{SystemCpuUsage.ToString("n1")}%";

        public bool IsRunning => _isRunning;

        public bool IsPaused => _isPaused;

        public bool IsConnected => _port.IsOpen();

        public bool IsLocked { get => _machineStateModel.IsLocked; }

        public int CommandCount => _commandsToSend.Count;

        public int LineCount => _lineCount;

        public int NextCommand { get; private set; }

        public int BufferSize
        {
            get => _currentBufferSize;

            private set
            {
                if (_currentBufferSize + value > MaxBufferSize)
                    throw new InvalidOperationException("Buffer size too large");

                _currentBufferSize = value;
                OnBufferSizeChanged?.Invoke(this, _currentBufferSize);
                Trace.WriteLine($"BufferSize: {BufferSize}");
            }
        }

        public TimeSpan TimeOut { get; set; } = DefaultTimeOut;

        public TimeSpan HomingTimeout { get; set; } = TimeSpan.FromSeconds(180);

        public bool SpindleActive { get => _machineStateModel.SpindleClockWise || _machineStateModel.SpindleCounterClockWise; }

        public int SpindleSpeed { get => Convert.ToInt32(_machineStateModel.SpindleSpeed); }

        public bool MistCoolantActive { get => _machineStateModel.MistEnabled; }

        public bool FloodCoolantActive { get => _machineStateModel.FloodEnabled; }

        public MachineStateModel StateModel => _machineStateModel;

        #endregion IGCodeProcessor Properties

        #region IGCodeProcessor Events

        public event GSendEventHandler OnConnect;

        public event GSendEventHandler OnDisconnect;

        public event GSendEventHandler OnStart;

        public event GSendEventHandler OnStop;

        public event GSendEventHandler OnPause;

        public event GSendEventHandler OnResume;

        public event CommandSentHandler OnCommandSent;

        public event BufferSizeHandler OnBufferSizeChanged;

        public event GSendEventHandler OnSerialError;

        public event GSendEventHandler OnSerialPinChanged;

        public event GrblErrorHandler OnGrblError;

        public event GrblAlarmHandler OnGrblAlarm;

        public event GSendEventHandler OnInvalidComPort;

        public event GSendEventHandler OnComPortTimeOut;

        public event MachineStateHandler OnMachineStateChanged;

        public event MessageHandler OnMessageReceived;

        public event ResponseReceivedHandler OnResponseReceived;

        #endregion IGCodeProcessor Events

        #region ThreadManager

        protected override bool Run(object parameters)
        {
            if (!_port.IsOpen() || _initialising)
                return !HasCancelled();

            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                TimeSpan span = DateTime.UtcNow - _lastInformationCheck;

                if (span.TotalMilliseconds > 250)
                {
                    _lastInformationCheck = DateTime.UtcNow;

                    //bypass internal send commands
                    _port.WriteLine(CommandRequestStatus);
                }

                if (_isRunning && !IsPaused)
                {
                    ProcessNextCommand();
                }

                return !HasCancelled();
            }
        }

        #endregion ThreadManager

        #region Com Port Events

        private void ProcessGrblResponse(string response)
        {
            //Trace.WriteLine($"Response: {response}");
            if (String.IsNullOrWhiteSpace(response))
                return;

            OnResponseReceived?.Invoke(this, response);

            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {

                if (response.Equals(ResultOk, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (_isRunning)
                    {
                        if (_sendQueue.TryDequeue(out IGCodeLine activeCommand))
                        {
                            activeCommand.Status = LineStatus.Processed;
                            BufferSize -= activeCommand.GetGCode().Length;
                        }
                    }

                    _waitingForResponse = false;

                    return;
                }
                else if (response.StartsWith('['))
                {
                    ProcessMessageResponse(response.Trim()[1..^1]);
                    return;
                }
                else if (response.StartsWith("error", StringComparison.InvariantCultureIgnoreCase))
                {
                    Trace.WriteLine($"Response: {response}");
                    ProcessErrorResponse(response);
                    return;
                }
                else if (response.StartsWith('<'))
                {
                    ProcessInformationResponse(response[1..^1]);
                    return;
                }
                else if (response.StartsWith("alarm", StringComparison.InvariantCultureIgnoreCase))
                {
                    Trace.WriteLine($"Response: {response}");
                    ProcessAlarmResponse(response);
                    return;
                }
            }

#if DEBUG
            //Trace.WriteLine($"Junk Data: {response}");
#endif
        }

        private void ProcessMessageResponse(string response)
        {
            string[] parts = response.Split(SeparatorColon, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 1)
            {
                OnMessageReceived?.Invoke(this, parts[1]);

                if (parts[1].Equals("Pgm End"))
                    Stop();
            }
        }

        private void ProcessInformationResponse(string response)
        {
            string message = response;
            string[] parts = message.Split(SeparatorPipe, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 0)
            {
                string[] status = parts[0].Split(SeparatorPipe, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                string statusDesc = status[0];
                int statusSep = statusDesc.IndexOf(":");

                if (statusSep > -1)
                {
                    statusDesc = statusDesc[..statusSep];
                }

                if (Enum.TryParse<MachineState>(statusDesc, true, out MachineState machineState))
                    _machineStateModel.MachineState = machineState;
                else
                    _machineStateModel.MachineState = MachineState.Undefined;

                if (status.Length > 1)
                    _machineStateModel.SubState = Convert.ToInt32(status[1]);
                else
                    _machineStateModel.SubState = -1;

                if (machineState.Equals(MachineState.Alarm) && !_isAlarm)
                {
                    _isAlarm = true;
                    OnGrblAlarm(this, GrblAlarm.Undefined);
                }
            }

            for (int i = 1; i < parts.Length; i++)
            {
                string[] subParts = parts[i].Split(SeparatorColon, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                if (subParts.Length < 2)
                    continue;

                string[] values = subParts[1].Split(SeparatorComma, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                switch (subParts[0].ToLower())
                {
                    case OptionOffsets:
                        _machineStateModel.OffsetX = Convert.ToDouble(values[0]);
                        _machineStateModel.OffsetY = Convert.ToDouble(values[1]);
                        _machineStateModel.OffsetZ = Convert.ToDouble(values[2]);
                        break;

                    case OptionWorkPosition:
                        _machineStateModel.WorkX = Convert.ToDouble(values[0]);
                        _machineStateModel.WorkY = Convert.ToDouble(values[1]);
                        _machineStateModel.WorkZ = Convert.ToDouble(values[2]);
                        break;

                    case OptionMachinePosition:
                        _machineStateModel.MachineX = Convert.ToDouble(values[0]);
                        _machineStateModel.MachineY = Convert.ToDouble(values[1]);
                        _machineStateModel.MachineZ = Convert.ToDouble(values[2]);
                        break;

                    case OptionBuffer:
                        _machineStateModel.BufferAvailableBlocks = Convert.ToInt32(values[0]);
                        _machineStateModel.AvailableRXbytes = Convert.ToInt32(values[1]);
                        break;

                    case OptionLineNumber:
                        _machineStateModel.LineNumber = Convert.ToInt32(values[0]);
                        break;

                    case OptionFeedSpeed:
                        _machineStateModel.FeedRate = Convert.ToDouble(values[0]);
                        _machineStateModel.SpindleSpeed = Convert.ToDouble(values[1]);
                        break;

                    case OptionFeedRate:
                        _machineStateModel.FeedRate = Convert.ToDouble(values[0]);
                        break;

                    case OptionOverrides:
                        _machineStateModel.OverrideFeeds = Convert.ToByte(values[0]);
                        _machineStateModel.OverrideRapids = Convert.ToByte(values[1]);
                        _machineStateModel.OverrideSpindleSpeed = Convert.ToByte(values[2]);
                        break;

                    case OptionAccessoryState:
                        _machineStateModel.SpindleClockWise = values[0].Contains("S");
                        _machineStateModel.SpindleCounterClockWise = values[0].Contains("C");
                        _machineStateModel.FloodEnabled = values[0].Contains("F");
                        _machineStateModel.MistEnabled = values[0].Contains(StatusMistEnabled);
                        break;
                }
            }

            _machineStateModel.IsPaused = _isPaused;
            _machineStateModel.IsRunning = _isRunning;

            if (_machineStateModel.Updated)
            {
                OnMachineStateChanged?.Invoke(this, _machineStateModel);

                _machineStateModel.ResetUpdated();
            }
        }

        private void ProcessErrorResponse(string response)
        {
            if (_sendQueue.TryDequeue(out IGCodeLine activeCommand))
            {
                activeCommand.Status = LineStatus.Failed;
                BufferSize -= activeCommand.GetGCode().Length;
            }

            Stop();

            GrblError error = GrblError.Undefined;

            if (Int32.TryParse(response[6..], out int errorCode))
            {
                error = (GrblError)errorCode;

                OnGrblError?.Invoke(this, error);
            }
            else
            {
                OnGrblError?.Invoke(this, error);
            }

            _overrideContext.ProcessError(error);
        }

        private void ProcessAlarmResponse(string response)
        {
            if (_sendQueue.TryDequeue(out IGCodeLine activeCommand))
            {
                activeCommand.Status = LineStatus.Failed;
                BufferSize -= activeCommand.GetGCode().Length;
            }

            GrblAlarm alarm = GrblAlarm.Undefined;

            Stop();

            if (Int32.TryParse(response[6..], out int alarmCode))
            {
                alarm = (GrblAlarm)alarmCode;

                OnGrblAlarm?.Invoke(this, alarm);
            }
            else
            {
                OnGrblAlarm?.Invoke(this, alarm);
            }

            _overrideContext.ProcessAlarm(alarm);
        }

        private void Port_PinChanged(object sender, System.IO.Ports.SerialPinChangedEventArgs e)
        {
            DisconnectForSerialError();
            OnSerialPinChanged?.Invoke(this, e);
        }

        private void Port_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            DisconnectForSerialError();
            OnSerialError?.Invoke(this, e);
        }

        private void Port_DataReceived(object sender, EventArgs e)
        {
            try
            {
                ProcessGrblResponse(_port.ReadLine().Trim());
            }
            catch (TimeoutException)
            {
                OnComPortTimeOut?.Invoke(this, e);
            }
        }

        #endregion Com Port Events

        #region Private Methods

        private void DisconnectForSerialError()
        {
            if (IsRunning)
                Stop();

            if (IsConnected)
                Disconnect();

            _machineStateModel.IsConnected = _port.IsOpen();
        }

        public string SendCommandWaitForOKCommand(string commandText)
        {
            StringBuilder Result = new(1024);

            _port.DataReceived -= Port_DataReceived;
            try
            {
                using (TimedLock tl = TimedLock.Lock(_lockObject))
                {
                    _ = _port.ReadLine();
                    _port.WriteLine(commandText);
                    DateTime sendTime = DateTime.UtcNow;

                    while (true)
                    {
                        string line = _port.ReadLine();

                        if (line.Trim().Equals(ResultOk))
                            break;

                        Result.AppendLine(line);

                        if (DateTime.UtcNow - sendTime > TimeOut)
                            throw new TimeoutException();

                        Thread.Sleep(TimeSpan.Zero);
                    }
                }
            }
            finally
            {
                _port.DataReceived += Port_DataReceived;
            }

            return Result.ToString();
        }

        private void SendCommandWaitForResponse(string commandText, TimeSpan timeout)
        {
            _waitingForResponse = true;
            DateTime sendTime = DateTime.UtcNow;
            _port.WriteLine(commandText);

            while (_waitingForResponse)
            {
                if (DateTime.UtcNow - sendTime > timeout)
                    throw new TimeoutException();

                Thread.Sleep(TimeSpan.Zero);
            }
        }

        private void InternalWriteLine(string commandText)
        {
            if (!String.IsNullOrEmpty(commandText) && commandText.Length > 0 && commandText[0] == '$')
            {
                _port.WriteLine(commandText);
            }
            else
            {
                IGCodeAnalyses gCode = _gcodeParser.Parse(commandText);

                IList<IGCodeLine> lines = gCode.Lines(out int lineNumber);

                Debug.Assert(lineNumber == 1);
                InternalWriteLine(lines[0]);
            }
        }

        private void InternalWriteLine(IGCodeLine commandLine)
        {
            _overrideContext.ProcessGCodeLine(commandLine);

            if (_overrideContext.SendCommand)
            {
                string gcodeLine = commandLine.GetGCode();

                _port.WriteLine(gcodeLine);
            }
        }

        private void InternalWriteByte(byte[] buffer)
        {
            _port.Write(buffer, 0, buffer.Length);
        }

        private void ValidateGrblSettings()
        {
            Dictionary<int, object> settings = Settings();

            foreach (int key in settings.Keys)
            {
                PropertyInfo propertyInfo = GetPropertyWithAttributeValue(key);

                if (propertyInfo == null)
                    continue;

                object newPropertyValue = propertyInfo.GetValue(_machine.Settings);

                if (newPropertyValue.GetType().BaseType.Name.Equals("Enum"))
                    newPropertyValue = (int)newPropertyValue;
                else if (newPropertyValue.GetType().Equals(typeof(bool)))
                {
                    newPropertyValue = newPropertyValue.Equals(true) ? 1 : 0;
                }

                if (!newPropertyValue.ToString().Equals(settings[key].ToString()))
                {
                    // property has changed
                    _machineStateModel.UpdatedGrblConfiguration.Add(
                        new ChangedGrblSettings(propertyInfo.Name, key, newPropertyValue.ToString(), settings[key].ToString()));
                }
            }

            _machine.ConfigurationLastVerified = DateTime.UtcNow;
        }

        private PropertyInfo GetPropertyWithAttributeValue(int key)
        {
            foreach (PropertyInfo propertyInfo in _machine.Settings.GetType().GetProperties())
            {
                GrblSettingAttribute grblSettingAttribute = propertyInfo.GetCustomAttribute<GrblSettingAttribute>();

                if (grblSettingAttribute != null && grblSettingAttribute.IntValue.Equals(key))
                {
                    return propertyInfo;
                }
            }

            return null;
        }

        #endregion Private Methods
    }
}
