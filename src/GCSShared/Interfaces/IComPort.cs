using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSendShared
{
    public interface IComPort
    {
        void Open();

        void Close();

        string ReadLine();

        void WriteLine(string line);

        bool IsOpen();

        event SerialErrorReceivedEventHandler ErrorReceived;

        event SerialPinChangedEventHandler PinChanged;

        event EventHandler DataReceived;
    }
}
