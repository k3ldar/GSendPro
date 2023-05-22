using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared;
using GSendShared.Models;

namespace GSendTests.Mocks
{
    internal class MockMachine : IMachine
    {
        public long Id => throw new NotImplementedException();

        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MachineType MachineType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ComPort { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MachineOptions Options { get; set; }
        public byte AxisCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public FeedRateDisplayUnits DisplayUnits { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public FeedbackUnit FeedbackUnit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int OverrideSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int OverrideSpindle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int OverrideZDownSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int OverrideZUpSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public GrblSettings Settings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime ConfigurationLastVerified { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ProbeCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ProbeSpeed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal ProbeThickness { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int JogUnits { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int JogFeedrate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SpindleType SpindleType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SoftStartSeconds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool SoftStart => throw new NotImplementedException();

        public int ServiceSpindleHours { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ServiceWeeks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal LayerHeightWarning { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddOptions(MachineOptions options)
        {
            Options |= options;
        }

        public void RemoveOptions(MachineOptions options)
        {
            Options &= ~options;
        }
    }
}
