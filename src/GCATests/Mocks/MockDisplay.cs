using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using CommandLinePlus;

namespace GSendTests.Mocks
{
    [ExcludeFromCodeCoverage]
    internal sealed class MockDisplay : IDisplay
    {
        public VerbosityLevel Verbosity { get; set; } = VerbosityLevel.Full;

        public void Write(Exception exception)
        {
            Lines.Add($"Error: {exception}");
        }

        public void WriteLine(VerbosityLevel verbosityLevel, string message)
        {
            Lines.Add(String.Format("{0}, {1}", verbosityLevel, message));
        }

        public void WriteLine(VerbosityLevel verbosityLevel, string message, params object[] args)
        {
            Lines.Add(String.Format("{0}, {1}", verbosityLevel, String.Format(message, args)));
        }

        public void WriteLine(string message)
        {
            Lines.Add(message);
        }

        public void WriteLine(string message, params object[] args)
        {
            Lines.Add(String.Format("Error {0}", String.Format(message, args)));
        }

        public List<string> Lines { get; } = new();
    }
}
