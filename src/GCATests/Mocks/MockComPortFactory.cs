using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using GSendShared;
using GSendShared.Abstractions;

using Shared.Classes;

using static GSend.Language.Resources;

namespace GSendTests.Mocks
{
    [ExcludeFromCodeCoverage]
    internal sealed class MockComPortFactory : IComPortFactory
    {
        private readonly object _lockObject = new();
        private readonly Dictionary<string, IComPort> _comPorts = new();

        public MockComPortFactory()
        {
        }

        public MockComPortFactory(MockComPort mockComPort)
        {
            MockComPort = mockComPort;
        }

        public MockComPort MockComPort { get; private set; }

        public IComPort CreateComPort(IMachine machine)
        {
            MockComPort ??= new MockComPort(machine);
            return MockComPort;
        }

        public IComPort CreateComPort(IComPortModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {

                if (_comPorts.ContainsKey(model.Name))
                    throw new InvalidOperationException(String.Format(ErrorComPortOpen, model.Name));

                MockComPort comPort = MockComPort ?? new MockComPort(model);

                _comPorts.Add(model.Name, comPort);

                return comPort;
            }
        }

        public void DeleteComPort(IComPort comPort)
        {
            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                MockComPort?.Close();
            }
        }

        public IComPort GetComPort(string comPort)
        {
            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                return MockComPort ?? throw new InvalidOperationException();
            }
        }
    }
}
