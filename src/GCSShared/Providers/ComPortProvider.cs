using System.IO.Ports;

using GSendShared.Abstractions;

namespace GSendShared.Providers
{
    public class ComPortProvider : IComPortProvider
    {
        public string[] AvailablePorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}
