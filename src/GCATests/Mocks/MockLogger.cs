using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PluginManager;
using PluginManager.Abstractions;

namespace GSendTests.Mocks
{
    internal class MockLogger : ILogger
    {
        public List<string> LogItems { get; } = new List<string>();

        public void AddToLog(in LogLevel logLevel, in string data)
        {
            LogItems.Add($"{logLevel} - {data}");
        }

        public void AddToLog(in LogLevel logLevel, in Exception exception)
        {
            throw new NotImplementedException();
        }

        public void AddToLog(in LogLevel logLevel, in Exception exception, string data)
        {
            throw new NotImplementedException();
        }

        public void AddToLog(in LogLevel logLevel, in string moduleName, in string data)
        {
            throw new NotImplementedException();
        }

        public void AddToLog(in LogLevel logLevel, in string moduleName, in Exception exception)
        {
            throw new NotImplementedException();
        }

        public void AddToLog(in LogLevel logLevel, in string moduleName, in Exception exception, string data)
        {
            throw new NotImplementedException();
        }
    }
}
