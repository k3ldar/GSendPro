using System;

using GSendAnalyzer.Internal;

using GSendShared;
using GSendShared.Abstractions;

using PluginManager.Abstractions;

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
                    return new GCodeParserFactory(new MockPluginClassesService(), new MockSubprograms());
                }
                else if (serviceType.Equals(typeof(IPluginClassesService)))
                {
                    return new MockPluginClassesService();
                }
            }

            return null;
        }
    }
}
