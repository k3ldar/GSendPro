using System;
using System.Collections.Concurrent;

using GSendShared;
using GSendShared.Abstractions;
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

        public IMachine Machine => throw new NotImplementedException();

        public IStaticMethods StaticMethods => throw new NotImplementedException();

        public MachineStateModel MachineStateModel { get; set; }

        public ConcurrentQueue<IGCodeLine> CommandQueue { get; } = new();

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

        public bool ProcessGCodeOverrides(IGCodeLine line)
        {
            throw new NotImplementedException();
        }

        public bool ProcessMCodeOverrides(IGCodeLine line)
        {
            throw new NotImplementedException();
        }
    }
}
