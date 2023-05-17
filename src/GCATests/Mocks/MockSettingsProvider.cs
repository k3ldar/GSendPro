using System;
using System.Diagnostics.CodeAnalysis;

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
