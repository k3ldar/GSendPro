using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PluginManager.Abstractions;

namespace GSendTests.Mocks
{
    [ExcludeFromCodeCoverage]
    internal class MockSettingsProvider : ISettingsProvider
    {
        public T GetSettings<T>(in string storage, in string sectionName)
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        public T GetSettings<T>(in string sectionName)
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
