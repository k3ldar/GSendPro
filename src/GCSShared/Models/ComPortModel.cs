using System.IO.Ports;

using GSendShared.Abstractions;

namespace GSendShared.Models
{
    public sealed class ComPortModel : IComPortModel
    {
        public ComPortModel(string name, int timeout, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            Name = name;
            Timeout = timeout;
            BaudRate = baudRate;
            Parity = parity;
            DataBits = dataBits;
            StopBits = stopBits;
        }

        public string Name { get; }

        public int Timeout { get; }

        public int BaudRate { get; }

        public Parity Parity { get; }

        public int DataBits { get; }

        public StopBits StopBits { get; }
    }
}
