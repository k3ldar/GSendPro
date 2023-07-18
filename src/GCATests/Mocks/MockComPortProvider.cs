using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using GSendShared.Abstractions;

namespace GSendTests.Mocks
{
    [ExcludeFromCodeCoverage]
    internal class MockComPortProvider : IComPortProvider
    {
        private readonly string _port;
        private readonly byte[] _availablePorts;

        public MockComPortProvider()
            : this(new byte[] { 5, 6, 7 })
        {

        }

        public MockComPortProvider(string port)
            : this(new byte[] { })
        {
            _port = port;
        }

        public MockComPortProvider(byte[] availablePorts)
        {
            _availablePorts = availablePorts;
        }

        public string[] AvailablePorts()
        {
            List<string> Result = new();

            foreach (byte port in _availablePorts)
            {
                Result.Add($"COM{port}");
            }

            if (!string.IsNullOrWhiteSpace(_port))
                Result.Add(_port);

            return Result.ToArray();
        }
    }
}
