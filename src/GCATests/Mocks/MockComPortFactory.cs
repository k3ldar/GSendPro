using System.Diagnostics.CodeAnalysis;

using GSendShared;

namespace GSendTests.Mocks
{
    [ExcludeFromCodeCoverage]
    internal sealed class MockComPortFactory : IComPortFactory
    {
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
            MockComPort = MockComPort ?? new MockComPort(machine);
            return MockComPort;
        }
    }
}
