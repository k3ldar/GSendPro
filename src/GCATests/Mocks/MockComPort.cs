using System;
using System.Diagnostics.CodeAnalysis;
using System.IO.Ports;

using GSendShared;

namespace GSendTests.Mocks
{
    [ExcludeFromCodeCoverage]
    internal sealed class MockComPort : IComPort
    {
        private readonly IMachine _machine;
        private bool _isOpen = false;

        public MockComPort(IMachine machine)
        {
            _machine = machine ?? throw new ArgumentNullException(nameof(machine));
        }

#pragma warning disable CS0067
        public event SerialErrorReceivedEventHandler ErrorReceived;
        public event SerialPinChangedEventHandler PinChanged;
        public event SerialDataReceivedEventHandler DataReceived;
#pragma warning restore CS0067

        public void Close()
        {
            if (!_isOpen)
                throw new InvalidOperationException("Should not close when not open");

            _isOpen = false;
        }

        public bool IsOpen()
        {
            return _isOpen;
        }

        public void Open()
        {
            if (_isOpen)
                throw new InvalidOperationException("Should not open when already open");

            _isOpen = true;
        }

        public string ReadLine()
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string line)
        {
            throw new NotImplementedException();
        }
    }
}
