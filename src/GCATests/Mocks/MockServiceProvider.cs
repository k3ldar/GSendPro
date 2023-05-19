using System;

using GSendAnalyser.Internal;

using GSendShared;
using GSendShared.Abstractions;

namespace GSendTests.Mocks
{
    internal class MockServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            if (serviceType != null)
            {
                if (serviceType.Equals(typeof(IGSendDataProvider)))
                {
                    return new MockGSendDataProvider();
                }
                else if (serviceType.Equals(typeof(IGCodeParserFactory)))
                {
                    return new GCodeParserFactory(new MockPluginClassesService());
                }
            }
                
            return null;
        }
    }
}
