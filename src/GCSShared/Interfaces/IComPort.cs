﻿using System.IO.Ports;

namespace GSendShared
{
    public interface IComPort
    {
        string Name { get; }

        void Open();

        void Close();

        bool CanReadLine();

        string ReadLine();

        void WriteLine(string line);

        void Write(byte[] buffer, int offset, int count);

        bool IsOpen();

        event SerialErrorReceivedEventHandler ErrorReceived;

        event SerialPinChangedEventHandler PinChanged;

        event EventHandler DataReceived;
    }
}
