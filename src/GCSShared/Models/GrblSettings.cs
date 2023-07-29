using System.ComponentModel;

using GSendShared.Attributes;

namespace GSendShared.Models
{
    public sealed class GrblSettings
    {
        [Browsable(true)]
        [DefaultValue(10)]
        [Category("Steps")]
        [GrblSetting("$0")]
        public uint StepPulseTime { get; set; } = 10;

        [Browsable(true)]
        [DefaultValue(10)]
        [Category("Steps")]
        [GrblSetting("$1")]
        public uint StepIdleDelay { get; set; } = 255;

        [Browsable(true)]
        [DefaultValue(AxisConfiguration.None)]
        [Category("Steps")]
        [GrblSetting("$2")]
        public AxisConfiguration StepPulseConfiguration { get; set; } = AxisConfiguration.None;

        [Browsable(true)]
        [DefaultValue(AxisConfiguration.None)]
        [Category("Steps")]
        [GrblSetting("$3")]
        public AxisConfiguration AxisDirection { get; set; } = AxisConfiguration.None;

        [Browsable(true)]
        [DefaultValue(AxisConfiguration.None)]
        [Category("Step")]
        [GrblSetting("$4")]
        public Pin StepEnableInvert { get; set; } = Pin.High;

        [Browsable(true)]
        [DefaultValue(Pin.High)]
        [Category("General")]
        [GrblSetting("$5")]
        public Pin LimitPinInvert { get; set; } = Pin.High;

        [Browsable(true)]
        [DefaultValue(Pin.High)]
        [Category("General")]
        [GrblSetting("$6")]
        public Pin ProbePinInvert { get; set; } = Pin.High;

        [Browsable(true)]
        [DefaultValue(ReportType.MachinePosition | ReportType.WorkPosition)]
        [Category("General")]
        [GrblSetting("$10")]
        public ReportType StatusReport { get; set; } = ReportType.MachinePosition | ReportType.WorkPosition;

        [Browsable(true)]
        [DefaultValue(0.002)]
        [Category("Curves")]
        [GrblSetting("$11")]
        public decimal JunctionDeviation { get; set; } = 0.010m;

        [Browsable(true)]
        [DefaultValue(0.002)]
        [Category("Curves")]
        [GrblSetting("$12")]
        public decimal ArcTolerance { get; set; } = 0.002m;

        [Browsable(true)]
        [DefaultValue(FeedbackUnit.Mm)]
        [Category("Feedback")]
        [GrblSetting("$13")]
        public FeedbackUnit FeedbackUnit { get; set; } = FeedbackUnit.Mm;

        [Browsable(true)]
        [DefaultValue(false)]
        [Category("Limits")]
        [GrblSetting("$20")]
        public bool SoftLimits { get; set; } = false;

        [Browsable(true)]
        [DefaultValue(false)]
        [Category("Limits")]
        [GrblSetting("$21")]
        public bool HardLimits { get; set; } = true;

        [Browsable(true)]
        [DefaultValue(true)]
        [Category("Homing")]
        [GrblSetting("$22")]
        public bool HomingCycle { get; set; } = true;

        [Browsable(true)]
        [DefaultValue(AxisConfiguration.ReverseXandY)]
        [Category("Homing")]
        [GrblSetting("$23")]
        public AxisConfiguration HomingCycleDirection { get; set; } = AxisConfiguration.ReverseXandY;

        [Browsable(true)]
        [DefaultValue(25)]
        [Category("Homing")]
        [GrblSetting("$24")]
        public decimal HomingFeedRate { get; set; } = 25;

        [Browsable(true)]
        [DefaultValue(635)]
        [Category("Homing")]
        [GrblSetting("$25")]
        public decimal HomingSeekRate { get; set; } = 635;

        [Browsable(true)]
        [DefaultValue(250)]
        [Category("Homing")]
        [GrblSetting("$26")]
        public uint HomingDebounce { get; set; } = 250;

        [Browsable(true)]
        [DefaultValue(1)]
        [Category("Homing")]
        [GrblSetting("$27")]
        public decimal HomingPullOff { get; set; } = 1;

        [Browsable(true)]
        [DefaultValue(10000)]
        [Category("Spindle")]
        [GrblSetting("$30")]
        public uint MaxSpindleSpeed { get; set; } = 10000;

        [Browsable(true)]
        [DefaultValue(0)]
        [Category("Spindle")]
        [GrblSetting("$31")]
        public uint MinSpindleSpeed { get; set; } = 0;

        [Browsable(true)]
        [DefaultValue(false)]
        [Category("Laser")]
        [GrblSetting("$32")]
        public bool LaserModeEnabled { get; set; } = false;

        [Browsable(true)]
        [DefaultValue(80)]
        [Category("StepperMotor")]
        [GrblSetting("$100")]
        public decimal StepsPerMmX { get; set; } = 80;

        [Browsable(true)]
        [DefaultValue(80)]
        [Category("StepperMotor")]
        [GrblSetting("$101")]
        public decimal StepsPerMmY { get; set; } = 80;

        [Browsable(true)]
        [DefaultValue(250)]
        [Category("StepperMotor")]
        [GrblSetting("$102")]
        public decimal StepsPerMmZ { get; set; } = 400;

        [Browsable(true)]
        [DefaultValue(2000)]
        [Category("StepperMotor")]
        [GrblSetting("$110")]
        public decimal MaxFeedRateX { get; set; } = 2000;

        [Browsable(true)]
        [DefaultValue(2000)]
        [Category("StepperMotor")]
        [GrblSetting("$111")]
        public decimal MaxFeedRateY { get; set; } = 2000;

        [Browsable(true)]
        [DefaultValue(1200)]
        [Category("StepperMotor")]
        [GrblSetting("$112")]
        public decimal MaxFeedRateZ { get; set; } = 1200;

        [Browsable(true)]
        [DefaultValue(50)]
        [Category("StepperMotor")]
        [GrblSetting("$120")]
        public decimal MaxAccelerationX { get; set; } = 300;

        [Browsable(true)]
        [DefaultValue(50)]
        [Category("StepperMotor")]
        [GrblSetting("$121")]
        public decimal MaxAccelerationY { get; set; } = 300;

        [Browsable(true)]
        [DefaultValue(50)]
        [Category("StepperMotor")]
        [GrblSetting("$122")]
        public decimal MaxAccelerationZ { get; set; } = 300;

        [Browsable(true)]
        [DefaultValue(300)]
        [Category("Limits")]
        [GrblSetting("$130")]
        public decimal MaxTravelX { get; set; } = 300;

        [Browsable(true)]
        [DefaultValue(400)]
        [Category("Limits")]
        [GrblSetting("$131")]
        public decimal MaxTravelY { get; set; } = 400;

        [Browsable(true)]
        [DefaultValue(100)]
        [Category("Limits")]
        [GrblSetting("$132")]
        public decimal MaxTravelZ { get; set; } = 100;
    }
}
