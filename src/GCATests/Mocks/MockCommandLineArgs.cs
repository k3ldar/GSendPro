using System;
using System.Diagnostics.CodeAnalysis;

using CommandLinePlus;

namespace GSendTests.Mocks
{
    [ExcludeFromCodeCoverage]
    internal class MockCommandLineArgs : ICommandLineArguments
    {
        public string PrimaryOption { get; set; }

        public string SubOption { get; set; }

        public string[] AllArguments()
        {
            return Array.Empty<string>();
        }

        public bool Contains(string name)
        {
            return false;
        }

        public T Get<T>(string name)
        {
            return default;
        }

        public T Get<T>(string name, T defaultValue)
        {
            return defaultValue;
        }
    }
}
