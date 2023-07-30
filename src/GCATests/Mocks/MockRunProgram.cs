using Shared.Classes;
using System;

namespace GSendTests.Mocks
{
    internal class MockRunProgram : IRunProgram
    {
        public int Run(string programName, string parameters, bool useShellExecute, bool waitForFinish, int timeoutMilliseconds)
        {
            ProgramName = programName;
            Parameters = parameters;
            UseShellExecute = useShellExecute;
            WaitForFinish = waitForFinish;
            TimeoutMilliseconds = timeoutMilliseconds;
            return ReturnValue;
        }

        public int ReturnValue { get; set; } = Int32.MinValue;

        public string ProgramName { get; set; }

        public string Parameters { get; set; }

        public bool UseShellExecute { get; set; }

        public bool WaitForFinish { get; set; }

        public int TimeoutMilliseconds { get; set; }
    }
}
