using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using GSendShared.Abstractions;

namespace GSendTests.Mocks
{
    [ExcludeFromCodeCoverage]
    internal class MockComPortProvider : IComPortProvider
    {
        private readonly byte[] _availablePorts;

        public MockComPortProvider()
            : this(new byte[] { 5, 6, 7 })
        {

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

            return Result.ToArray();
        }
    }
}
