using System;

using GSendShared;

namespace GSendTests.Mocks
{
    internal class MockServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            if (serviceType != null && serviceType.Equals(typeof(IGSendDataProvider)))
            {
                return new MockGSendDataProvider();
            }
                
            return null;
        }
    }
}
