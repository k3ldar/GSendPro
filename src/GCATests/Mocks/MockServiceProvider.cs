using System;

using GSendShared;

namespace GSendTests.Mocks
{
    internal class MockServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            if (serviceType != null)
            {
                if (serviceType.Equals(typeof(IMachineProvider)))
                    return new MockMachineProvider();
            }
                
            return null;
        }
    }
}
