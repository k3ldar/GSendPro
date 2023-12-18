using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Interfaces;
using GSendShared.Models;

namespace GSendTests.Mocks
{
    internal class MockOverrideContext : IGCodeOverrideContext
    {
        public MockOverrideContext(MachineStateModel machineStateModel)
        {
            MachineStateModel = machineStateModel ?? throw new ArgumentNullException(nameof(machineStateModel));
        }

        public IGCodeLine GCode { get; set; }

        public IGCodeLine OverriddenGCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Overridden => throw new NotImplementedException();

        public bool SendCommand { get; set; } = true;

        public IGCodeProcessor Processor => throw new NotImplementedException();

        public IMachine Machine { get; } = new MockMachine();

        public ICommonUtils CommonUtils => throw new NotImplementedException();

        public MachineStateModel MachineStateModel { get; set; }

        public ConcurrentQueue<IGCodeLine> CommandQueue { get; } = new();

        public IJobExecution JobExecution => throw new NotImplementedException();

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public void ProcessAlarm(GrblAlarm alarm)
        {
            throw new NotImplementedException();
        }

        public void ProcessError(GrblError error)
        {
            throw new NotImplementedException();
        }

        public void ProcessError(Exception error)
        {
            ExceptionsRaised.Add(error);
        }

        public bool ProcessGCodeOverrides(IGCodeLine line)
        {
            throw new NotImplementedException();
        }

        public bool ProcessMCodeOverrides(IGCodeLine line)
        {
            throw new NotImplementedException();
        }

        public void SendInformationUpdate(InformationType informationType, string message)
        {
            SendInformation.Add($"{informationType} - {message}");
        }

        public List<Exception> ExceptionsRaised { get; } = new();

        public IReadOnlyDictionary<ushort, IGCodeVariable> Variables { get; set; }

        public List<string> SendInformation = new();
    }
}
