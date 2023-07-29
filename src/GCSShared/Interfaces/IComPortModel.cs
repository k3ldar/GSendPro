using System.IO.Ports;

namespace GSendShared.Abstractions
{
    public interface IComPortModel
    {
        int BaudRate { get; }
        int DataBits { get; }
        string Name { get; }
        Parity Parity { get; }
        StopBits StopBits { get; }
        int Timeout { get; }
    }
}