using System.IO.Ports;

using GSendShared;

namespace GSendService.Internal
{
    public sealed class WindowsComPort : IComPort
    {
        private readonly SerialPort _serialPort;
        private readonly GSendSettings _settings;

        public WindowsComPort(IMachine machine, GSendSettings settings)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            if (!Enum.TryParse<Parity>(_settings.Parity, out Parity parity))
                parity = Parity.None;

            if (!Enum.TryParse<StopBits>(_settings.StopBits, out StopBits stopBits))
                stopBits = StopBits.One;

            _serialPort = new SerialPort(machine.ComPort, settings.BaudRate,
                parity, settings.DataBits, stopBits);
            _serialPort.ReadTimeout = settings.ReadTimeout;
            _serialPort.WriteTimeout = settings.WriteTimeout;
            _serialPort.DataReceived += SerialPort_DataReceived;
            _serialPort.ErrorReceived += SerialPort_ErrorReceived;
            _serialPort.PinChanged += SerialPort_PinChanged;
        }

        public void Close()
        {
            if (IsOpen())
            {
                _serialPort.Close();
            }
        }

        public bool IsOpen()
        {
            return _serialPort.IsOpen;
        }

        public void Open()
        {
            if (!IsOpen())
            {
                _serialPort.Open();
            }
        }

        public string ReadLine()
        {
            if (IsOpen() && _serialPort.BytesToRead > 0)
            {
                return _serialPort.ReadLine();
            }

            return String.Empty;
        }

        public void WriteLine(string line)
        {
            if (IsOpen() && !String.IsNullOrEmpty(line))
            {
                _serialPort.WriteLine(line);
            }
        }

        public event SerialErrorReceivedEventHandler ErrorReceived;

        public event SerialPinChangedEventHandler PinChanged;

        public event SerialDataReceivedEventHandler DataReceived;

        private void SerialPort_PinChanged(object sender, SerialPinChangedEventArgs e) => PinChanged?.Invoke(sender, e);

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e) => ErrorReceived?.Invoke(sender, e);

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) => DataReceived?.Invoke(sender, e);
    }
}
