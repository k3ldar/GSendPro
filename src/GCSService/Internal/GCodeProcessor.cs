using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

using GSendShared;

using Shared.Classes;

namespace GSendService.Internal
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
        private static TimeSpan DefaultTimeOut;

        private readonly List<IGCodeCommand> _commandsToSend = new();

        private readonly IMachine _machine;
        private readonly IComPort _port;
        private volatile bool _isRunning;
        private volatile bool _isPaused;
        private volatile bool _waitingForResponse;
        private int _currentBufferSize = 0;

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

            if (NextCommand < CommandCount)
            {
                IGCodeCommand commandToSend = _commandsToSend[NextCommand];

                if (commandToSend.Attributes.HasFlag(CommandAttributes.DoNotProcess))
                {
                    NextCommand++;
                    return;
                }

                string commandText = commandToSend.GetCommand();

                if (BufferSize + commandText.Length > MaxBufferSize)
                    return;

                if (ActiveCommand == null)
                    ActiveCommand = commandToSend;

                ActiveCommand.Status = CommandStatus.Sent;
                //todo overrides

                BufferSize += commandText.Length;

                WriteLine(commandText);
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

                string connectResponse = SendCommandWaitForResponse("$I");

                if (_port.IsOpen())
                    OnGrblError?.Invoke(this, GrblError.Locked);
            }
            catch (Exception)
            {
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
            OnStart?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool Pause()
        {
            if (!_isRunning)
                return false;

            _isPaused = true;
            OnPause?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool Resume()
        {
            if (!_isPaused)
                return false;

            _isPaused = false;
            OnResume?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool Stop()
        {
            if (!_isRunning)
                return false;

            _isRunning = false;
            _isPaused = false;
            OnStop?.Invoke(this, EventArgs.Empty);
            NextCommand = FirstCommand;
            ActiveCommand = null;
            return true;
        }

        public void Clear()
        {
            _commandsToSend.Clear();
            NextCommand = FirstCommand;
        }

        public void LoadGCode(IReadOnlyList<IGCodeCommand> commands)
        {
            if (commands == null)
                throw new InvalidOperationException(nameof(commands));

            Clear();

            foreach (IGCodeCommand command in commands)
                _commandsToSend.Add(command);
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
            OnCommandSent?.Invoke(this, CommandSent.Home);
        }

        public string Help()
        {
            return SendCommandWaitForResponse(CommandHelp);
        }

        public void Unlock()
        {
            SendCommandWaitForOKCommand(CommandUnlock);
            IsLocked = false;
            OnCommandSent?.Invoke(this, CommandSent.Unlock);
        }

        public Dictionary<int, decimal> Settings()
        {
            Dictionary<int, decimal> Result = new();

            string allSettings = SendCommandWaitForResponse(CommandSettings);

            string[] settings = allSettings.Trim().Split('\r');

            foreach (string s in settings)
            {
                string[] parts = s.Split('=', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

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
                SpindleSpeed = speed;
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
            if (!MistCoolantActive)
            {
                SendCommandWaitForOKCommand(CommandMistCoolantOn);
                MistCoolantActive = true;
                OnCommandSent?.Invoke(this, CommandSent.MistOn);
            }
        }

        public void TurnFloodCoolantOn()
        {
            if (!FloodCoolantActive)
            {
                SendCommandWaitForOKCommand(CommandFloodCoolantOn);
                FloodCoolantActive = true;
                OnCommandSent?.Invoke(this, CommandSent.FloodOn);
            }
        }

        public void CoolantOff()
        {
            SendCommandWaitForOKCommand(CommandCoolantOff);
            MistCoolantActive = false;
            FloodCoolantActive = false;
            OnCommandSent?.Invoke(this, CommandSent.CoolantOff);
        }

        #endregion IGCodeProcessor Methods

        #region IGCodeProcessor Properties

        public long Id => _machine.Id;

        public bool IsRunning => _isRunning;

        public bool IsPaused => _isPaused;

        public bool IsConnected => _port.IsOpen();

        public bool IsLocked { get; private set; }

        public int CommandCount => _commandsToSend.Count;

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

        public IGCodeCommand ActiveCommand { get; private set; }

        public TimeSpan TimeOut { get; set; } = DefaultTimeOut;

        public bool SpindleActive { get; private set; }

        public int SpindleSpeed { get; private set; }

        public bool MistCoolantActive { get; private set; }

        public bool FloodCoolantActive { get; private set; }

        #endregion IGCodeProcessor Properties

        #region IGCodeProcessor Events

        public event EventHandler OnConnect;

        public event EventHandler OnDisconnect;

        public event EventHandler OnStart;

        public event EventHandler OnStop;

        public event EventHandler OnPause;

        public event EventHandler OnResume;

        public event CommandSentHandler OnCommandSent;

        public event BufferSizeHandler OnBufferSizeChanged;

        public event EventHandler OnSerialError;

        public event EventHandler OnSerialPinChanged;

        public event GrblErrorHandler OnGrblError;

        public event EventHandler OnInvalidComPort;

        #endregion IGCodeProcessor Events

        #region ThreadManager

        protected override bool Run(object parameters)
        {
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
            if (response.Equals(ResultOk, StringComparison.InvariantCultureIgnoreCase))
            {
                if (_isRunning)
                {
                    ActiveCommand.Status = CommandStatus.Processed;
                    BufferSize -= ActiveCommand.GetCommand().Length;

                    NextCommand++;

                    if (NextCommand < CommandCount)
                    {
                        ActiveCommand = _commandsToSend[NextCommand];
                    }
                    else
                    {
                        Stop();
                    }
                }

                _waitingForResponse = false;

                return;
            }

            // is it a message
            if (response[0].Equals('['))
            {
                
            }

            if (response.StartsWith("error", StringComparison.InvariantCultureIgnoreCase))
            {
                Stop();

                if (ActiveCommand != null)
                    ActiveCommand.Status = CommandStatus.Failed;

                GrblError error = GrblError.Undefined;

                if (Int32.TryParse(response[6..], out int errorCode))
                {
                    error = (GrblError)errorCode;

                    OnGrblError?.Invoke(this, error);
                    return;
                }

                return;
            }

            throw new NotImplementedException();
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
                WriteLine(commandText);

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
            WriteLine(commandText);

            while (_waitingForResponse)
            {
                if (DateTime.UtcNow - sendTime > TimeOut)
                    throw new TimeoutException();

                Thread.Sleep(TimeSpan.Zero);
            }
        }

        private void WriteLine(string commandText)
        {
            _port.WriteLine(commandText);
        }

        #endregion Private Methods
    }
}
