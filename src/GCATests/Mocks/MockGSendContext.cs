using System;

using GSendShared;
using GSendShared.Abstractions;

namespace GSendTests.Mocks
{
    internal class MockGSendContext : IGSendContext
    {
        public MockGSendContext()
        {
        }

        public MockGSendContext(IServiceProvider serviceProvider)
            : this()
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IServiceProvider ServiceProvider { get; set; }

        public bool IsClosing => throw new NotImplementedException();

        public IGSendSettings Settings => throw new NotImplementedException();

        public void CloseContext()
        {
            throw new NotImplementedException();
        }

        public void ShowMachine(IMachine machine)
        {
            throw new NotImplementedException();
        }
    }
}
