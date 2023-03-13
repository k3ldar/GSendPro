using System.Diagnostics;
using System.Text;

using GSendShared;
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
        private const string CommandSpindleStart = "S{0}M3";
        private const string CommandStopSpindle = "M5";
        private const string CommandMistCoolantOn = "M7";
        private const string CommandFloodCoolantOn = "M8";
        private const string CommandCoolantOff = "M9";
        private const string CommandZero = "G92";
        private const string CommandZeroAxis = "{0}0";
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
        private static TimeSpan DefaultTimeOut;

        private readonly List<IGCodeLine> _commandsToSend = new();
        private readonly Queue<IGCodeLine> _sendQueue = new();

        private readonly IMachine _machine;
        private readonly IComPort _port;
        private volatile bool _isRunning;
        private volatile bool _isPaused;
        private volatile bool _waitingForResponse;
        private int _currentBufferSize = 0;
        private DateTime _lastInformationCheck = DateTime.MinValue;
        private readonly object _lockObject = new();
        private readonly MachineStateModel _machineStateModel = new();
        private int _lineCount = 0;

        #region Constructors

        public GCodeProcessor(IMachine machine, IComPortFactory comPortFactory)
            : base(machine, TimeSpan.FromMilliseconds(QueueProcessMilliseconds))
        {
            if (comPortFactory == null)
                throw new ArgumentNullException(nameof(comPortFactory));

            _machine = machine ?? throw new ArgumentNullException(nameof(machine));
            _port = comPortFactory.CreateComPort(_machine);
            _port.DataReceived += Port_DataReceived;
            _port.ErrorReceived += Port_ErrorReceived;
            _port.PinChanged += Port_PinChanged;
            DefaultTimeOut = TimeSpan.FromSeconds(30);
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
                    //todo overrides

                    BufferSize += commandText.Length;

                    InternalWriteLine(commandText);

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

        public bool Connect()
        {
            if (_port.IsOpen())
                return true;

            try
            {
                _port.Open();
                OnConnect?.Invoke(this, EventArgs.Empty);
                ThreadManager.ThreadStart(this, $"{_machine.Name} - {_machine.ComPort}", ThreadPriority.Normal);
                InternalWriteLine(CommandGetStatus);

                Trace.WriteLine($"Connect: {_port.IsOpen()}");
            }
            catch (Exception)
            {
                Trace.WriteLine("Connect Error");
                OnInvalidComPort?.Invoke(this, EventArgs.Empty);
            }

            return _port.IsOpen();
        }

        public bool Disconnect()
        {
            if (!_port.IsOpen())
                return true;

            _port.Close();
            OnDisconnect?.Invoke(this, EventArgs.Empty);
            ThreadManager.Cancel($"{_machine.Name} - {_machine.ComPort}");
            Trace.WriteLine("Disconnect");

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
            InternalWriteLine(CommandStartResume);
            OnStart?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool Pause()
        {
            if (!_isRunning)
                return false;

            _isPaused = true;
            OnPause?.Invoke(this, EventArgs.Empty);

            Trace.WriteLine("Pause");
            InternalWriteLine(CommandPause);
            return true;
        }

        public bool Resume()
        {
            if (!_isPaused)
                return false;

            _isPaused = false;
            OnResume?.Invoke(this, EventArgs.Empty);

            Trace.WriteLine($"Resume");
            InternalWriteLine(CommandStartResume);
            return true;
        }

        public bool Stop()
        {
            if (!_isRunning)
                return false;

            Trace.WriteLine("Stop");
            InternalWriteByte(new byte[] { 0x85 });

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
            _commandsToSend.Clear();
            NextCommand = FirstCommand;
        }

        public void LoadGCode(IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new InvalidOperationException(nameof(gCodeAnalyses));

            Trace.WriteLine("Load GCode");

            Clear();

            _lineCount = 0;
            GCodeLine currentLine = null;

            foreach (IGCodeCommand command in gCodeAnalyses.Commands)
            {
                if (command.LineNumber > _lineCount)
                {
                    currentLine = new();
                    _lineCount++;
                    _commandsToSend.Add(currentLine);
                }

                currentLine.Commands.Add(command);
            }
        }

        public void ZeroAxis(Axis axis)
        {
            string zeroCommand = CommandZero;

            if (axis.HasFlag(Axis.X))
                zeroCommand += String.Format(CommandZeroAxis, Axis.X);

            if (axis.HasFlag(Axis.Y))
                zeroCommand += String.Format(CommandZeroAxis, Axis.Y);

            if (axis.HasFlag(Axis.Z))
                zeroCommand += String.Format(CommandZeroAxis, Axis.Z);

            SendCommandWaitForOKCommand(zeroCommand);
            OnCommandSent?.Invoke(this, CommandSent.ZeroAxis);
        }

        public void Home()
        {
            SendCommandWaitForOKCommand(CommandHome);
            Trace.WriteLine("Home");
            OnCommandSent?.Invoke(this, CommandSent.Home);
        }

        public string Help()
        {
            return SendCommandWaitForResponse(CommandHelp);
        }

        public void Unlock()
        {
            SendCommandWaitForOKCommand(CommandUnlock);
            Trace.WriteLine("Unlock");
            OnCommandSent?.Invoke(this, CommandSent.Unlock);
        }

        public void JogStart(JogDirection jogDirection, double stepSize, double feedRate)
        {
            StringBuilder jogCommand = new("$J=G21G91");
            decimal maxZTravel = Convert.ToDecimal(stepSize);
            decimal maxXTravel = Convert.ToDecimal(stepSize);
            decimal maxYTravel = Convert.ToDecimal(stepSize);

            if (stepSize < 0.01)
            {
                maxZTravel = _machine.Settings[132] - Convert.ToDecimal(StateModel.MachineZ);
                maxXTravel = _machine.Settings[130] - Convert.ToDecimal(StateModel.MachineX);
                maxYTravel = _machine.Settings[131] - Convert.ToDecimal(StateModel.MachineY);
            }

            switch (jogDirection)
            {
                case JogDirection.ZPlus:
                    jogCommand.AppendFormat("Z{0}", maxZTravel);

                    break;

                case JogDirection.ZMinus:
                    jogCommand.AppendFormat("Z{0}", 0 - maxZTravel);

                    break;

                case JogDirection.XPlus:
                    jogCommand.AppendFormat("X{0}", maxXTravel);

                    break;

                case JogDirection.XMinus:
                    jogCommand.AppendFormat("X{0}", 0 - maxXTravel);

                    break;

                case JogDirection.YPlus:
                    jogCommand.AppendFormat("Y{0}", maxYTravel);

                    break;

                case JogDirection.YMinus:
                    jogCommand.AppendFormat("Y{0}", 0 - maxYTravel);

                    break;

                case JogDirection.XPlusYMinus:
                    jogCommand.AppendFormat("X{0}", maxXTravel);
                    jogCommand.AppendFormat("Y{0}", 0 - maxYTravel);

                    break;

                case JogDirection.XPlusYPlus:
                    jogCommand.AppendFormat("X{0}", maxXTravel);
                    jogCommand.AppendFormat("Y{0}", maxYTravel);

                    break;

                case JogDirection.XMinusYMinus:
                    jogCommand.AppendFormat("X{0}", 0 - maxXTravel);
                    jogCommand.AppendFormat("Y{0}", 0 - maxYTravel);

                    break;

                case JogDirection.XMinusYPlus:
                    jogCommand.AppendFormat("X{0}", 0 - maxXTravel);
                    jogCommand.AppendFormat("Y{0}", maxYTravel);

                    break;
            }

            jogCommand.AppendFormat("F{0}", feedRate);

            InternalWriteLine(jogCommand.ToString());
        }

        public void JogStop()
        {
            InternalWriteByte(new byte[] { 0x85 });
        }

        public void WriteLine(string gCode)
        {
            if (IsConnected && !IsPaused && !IsRunning)
                InternalWriteLine(gCode);
        }

        public Dictionary<int, decimal> Settings()
        {
            Dictionary<int, decimal> Result = new();

            string allSettings = SendCommandWaitForResponse(CommandSettings);

            string[] settings = allSettings.Trim().Split('\r');

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

        public void UpdateSpindleSpeed(int speed)
        {
            if (speed < 0)
                throw new ArgumentOutOfRangeException(nameof(speed));

            if (speed > 0)
            {
                SendCommandWaitForOKCommand(String.Format(CommandSpindleStart, speed));
                OnCommandSent?.Invoke(this, CommandSent.SpindleSpeedSet);
            }
            else
            {
                SendCommandWaitForOKCommand(CommandStopSpindle);
                OnCommandSent?.Invoke(this, CommandSent.SpindleOff);
            }
        }

        public void TurnMistCoolantOn()
        {
            if (!_machineStateModel.MistEnabled)
            {
                SendCommandWaitForOKCommand(CommandMistCoolantOn);
                OnCommandSent?.Invoke(this, CommandSent.MistOn);
            }
        }

        public void TurnFloodCoolantOn()
        {
            if (!FloodCoolantActive)
            {
                SendCommandWaitForOKCommand(CommandFloodCoolantOn);
                OnCommandSent?.Invoke(this, CommandSent.FloodOn);
            }
        }

        public void CoolantOff()
        {
            SendCommandWaitForOKCommand(CommandCoolantOff);
            OnCommandSent?.Invoke(this, CommandSent.CoolantOff);
        }

        #endregion IGCodeProcessor Methods

        #region IGCodeProcessor Properties

        public long Id => _machine.Id;

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

        public event MachineStateHandler OnMachineStateChanged;

        public event MessageHandler OnMessageReceived;

        public event ResponseReceivedHandler OnResponseReceived;

        #endregion IGCodeProcessor Events

        #region ThreadManager

        protected override bool Run(object parameters)
        {
            TimeSpan span = DateTime.UtcNow - _lastInformationCheck;

            if (span.TotalMilliseconds > 300)
            {
                _lastInformationCheck = DateTime.UtcNow;
                InternalWriteLine(CommandRequestStatus);
            }

            if (_isRunning && !IsPaused)
            {
                ProcessNextCommand();
            }

            return !HasCancelled();
        }

        #endregion ThreadManager

        #region Com Port Events

        private void ProcessGrblResponse(string response)
        {
            if (String.IsNullOrWhiteSpace(response))
                return;

            OnResponseReceived?.Invoke(this, response);

            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                Trace.WriteLine($"Response: {response}");

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
                    ProcessMessageResponse(response[1..^1]);
                    return;
                }
                else if (response.StartsWith("error", StringComparison.InvariantCultureIgnoreCase))
                {
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
                    ProcessAlarmResponse(response);
                    return;
                }
            }

#if DEBUG
            throw new NotImplementedException();
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

                if (Enum.TryParse<MachineState>(status[0], true, out MachineState machineState))
                    _machineStateModel.MachineState = machineState;
                else
                    _machineStateModel.MachineState = MachineState.Undefined;

                if (status.Length > 1)
                    _machineStateModel.SubState = Convert.ToInt32(status[1]);
                else
                    _machineStateModel.SubState = -1;
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
        }

        private void ProcessAlarmResponse(string response)
        {
            if (_sendQueue.TryDequeue(out IGCodeLine activeCommand))
            {
                activeCommand.Status = LineStatus.Failed;
                BufferSize -= activeCommand.GetGCode().Length;
            }

            GrblAlarm error = GrblAlarm.Undefined;

            Stop();

            if (Int32.TryParse(response[6..], out int errorCode))
            {
                error = (GrblAlarm)errorCode;

                OnGrblAlarm?.Invoke(this, error);
            }
            else
            {
                OnGrblAlarm?.Invoke(this, error);
            }
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
            ProcessGrblResponse(_port.ReadLine().Trim());
        }

        #endregion Com Port Events

        #region Private Methods

        private void DisconnectForSerialError()
        {
            if (IsRunning)
                Stop();

            if (IsConnected)
                Disconnect();
        }

        private string SendCommandWaitForResponse(string commandText)
        {
            StringBuilder Result = new(1024);

            _port.DataReceived -= Port_DataReceived;
            try
            {
                DateTime sendTime = DateTime.UtcNow;
                InternalWriteLine(commandText);

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
            finally
            {
                _port.DataReceived += Port_DataReceived;
            }

            return Result.ToString();
        }

        private void SendCommandWaitForOKCommand(string commandText)
        {
            _waitingForResponse = true;
            DateTime sendTime = DateTime.UtcNow;
            InternalWriteLine(commandText);

            while (_waitingForResponse)
            {
                if (DateTime.UtcNow - sendTime > TimeOut)
                    throw new TimeoutException();

                Thread.Sleep(TimeSpan.Zero);
            }
        }

        private void InternalWriteLine(string commandText)
        {
            _port.WriteLine(commandText);
        }

        private void InternalWriteByte(byte[] buffer)
        {
            _port.Write(buffer, 0, buffer.Length);
        }

        #endregion Private Methods
    }
}
