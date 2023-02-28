using System.IO.Ports;

using GSendShared.Interfaces;

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
