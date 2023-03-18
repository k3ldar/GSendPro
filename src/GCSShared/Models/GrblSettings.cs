using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSendShared.Models
{
    public sealed class GrblSettings
    {
        /// <summary>
        /// $0
        /// </summary>
        [Browsable(true)]
        [DefaultValue(10)]
        [Category("Steps")]
        public uint StepPulseTime { get; set; } = 10;

        /// <summary>
        /// $1
        /// </summary>
        [Browsable(true)]
        [DefaultValue(10)]
        [Category("Steps")]
        public uint StepIdleDelay { get; set; } = 10;

        /// <summary>
        /// $2
        /// </summary>
        [Browsable(true)]
        [DefaultValue(AxisConfiguration.None)]
        [Category("Steps")]
        public AxisConfiguration StepPulseConfiguration { get; set; } = AxisConfiguration.None;

        /// <summary>
        /// $3
        /// </summary>
        [Browsable(true)]
        [DefaultValue(AxisConfiguration.None)]
        [Category("Steps")]
        public AxisConfiguration AxisDirection { get; set; } = AxisConfiguration.None;

        /// <summary>
        /// $4
        /// </summary>
        [Browsable(true)]
        [DefaultValue(AxisConfiguration.None)]
        [Category("Step")]
        public Pin StepEnableInvert { get; set; } = Pin.High;

        /// <summary>
        /// $5
        /// </summary>
        [Browsable(true)]
        [DefaultValue(Pin.High)]
        [Category("General")]
        public Pin LimitPinInvert { get; set; } = Pin.High;

        /// <summary>
        /// $6
        /// </summary>
        [Browsable(true)]
        [DefaultValue(Pin.High)]
        [Category("General")]
        public Pin ProbePinInvert { get; set; } = Pin.High;

        /// <summary>
        /// $10
        /// </summary>
        [Browsable(true)]
        [DefaultValue(ReportType.MachinePosition | ReportType.WorkPosition)]
        [Category("General")]
        public ReportType StatusReport { get; set; } = ReportType.MachinePosition | ReportType.WorkPosition;

        /// <summary>
        /// $11
        /// </summary>
        [Browsable(true)]
        [DefaultValue(0.002)]
        [Category("Curves")]
        public decimal JunctionDeviation { get; set; } = 0.002m;

        /// <summary>
        /// $12
        /// </summary>
        [Browsable(true)]
        [DefaultValue(0.002)]
        [Category("Curves")]
        public decimal ArcTolerance { get; set; } = 0.002m;

        /// <summary>
        /// $13
        /// </summary>
        [Browsable(true)]
        [DefaultValue(FeedbackUnit.Mm)]
        [Category("Feedback")]
        public FeedbackUnit FeedbackUnit { get; set; } = FeedbackUnit.Mm;

        /// <summary>
        /// $20
        /// </summary>
        [Browsable(true)]
        [DefaultValue(false)]
        [Category("Limits")]
        public bool SoftLimits { get; set; } = false;

        /// <summary>
        /// $21
        /// </summary>
        [Browsable(true)]
        [DefaultValue(false)]
        [Category("Limits")]
        public bool HardLimits { get; set; } = false;

        /// <summary>
        /// $22
        /// </summary>
        [Browsable(true)]
        [DefaultValue(true)]
        [Category("Homing")]
        public bool HomingCycle { get; set; } = true;

        /// <summary>
        /// $23
        /// </summary>
        [Browsable(true)]
        [DefaultValue(AxisConfiguration.ReverseX)]
        [Category("Homing")]
        public AxisConfiguration HomingCycleDirection { get; set; } = AxisConfiguration.ReverseX;

        /// <summary>
        /// $24
        /// </summary>
        [Browsable(true)]
        [DefaultValue(50)]
        [Category("Homing")]
        public decimal HomingFeedRate { get; set; } = 50;

        /// <summary>
        /// $25
        /// </summary>
        [Browsable(true)]
        [DefaultValue(635)]
        [Category("Homing")]
        public decimal HomingSeekRate { get; set; } = 635;

        /// <summary>
        /// $26
        /// </summary>
        [Browsable(true)]
        [DefaultValue(250)]
        [Category("Homing")]
        public uint HomingDebounce { get; set; } = 250;

        /// <summary>
        /// $27
        /// </summary>
        [Browsable(true)]
        [DefaultValue(1)]
        [Category("Homing")]
        public decimal HomingPullOff { get; set; } = 1;

        /// <summary>
        /// $100
        /// </summary>
        [Browsable(true)]
        [DefaultValue(250)]
        [Category("StepperMotor")]
        public decimal StepsPerMmX { get; set; } = 250;

        /// <summary>
        /// $101
        /// </summary>
        [Browsable(true)]
        [DefaultValue(250)]
        [Category("StepperMotor")]
        public decimal StepsPerMmY { get; set; } = 250;

        /// <summary>
        /// $102
        /// </summary>
        [Browsable(true)]
        [DefaultValue(250)]
        [Category("StepperMotor")]
        public decimal StepsPerMmZ { get; set; } = 250;

        /// <summary>
        /// $110
        /// </summary>
        [Browsable(true)]
        [DefaultValue(2000)]
        [Category("StepperMotor")]
        public decimal MaxFeedRateX { get; set; } = 2000;

        /// <summary>
        /// $111
        /// </summary>
        [Browsable(true)]
        [DefaultValue(2000)]
        [Category("StepperMotor")]
        public decimal MaxFeedRateY { get; set; } = 2000;

        /// <summary>
        /// $112
        /// </summary>
        [Browsable(true)]
        [DefaultValue(1000)]
        [Category("StepperMotor")]
        public decimal MaxFeedRateZ { get; set; } = 1000;

        /// <summary>
        /// $120
        /// </summary>
        [Browsable(true)]
        [DefaultValue(50)]
        [Category("StepperMotor")]
        public decimal MaxAccelerationX { get; set; } = 50;

        /// <summary>
        /// $121
        /// </summary>
        [Browsable(true)]
        [DefaultValue(50)]
        [Category("StepperMotor")]
        public decimal MaxAccelerationY { get; set; } = 50;

        /// <summary>
        /// $122
        /// </summary>
        [Browsable(true)]
        [DefaultValue(50)]
        [Category("StepperMotor")]
        public decimal MaxAccelerationZ { get; set; } = 50;

        /// <summary>
        /// $130
        /// </summary>
        [Browsable(true)]
        [DefaultValue(300)]
        [Category("Limits")]
        public decimal MaxTravelX { get; set; } = 300;

        /// <summary>
        /// $131
        /// </summary>
        [Browsable(true)]
        [DefaultValue(400)]
        [Category("Limits")]
        public decimal MaxTravelY { get; set; } = 400;

        /// <summary>
        /// $132
        /// </summary>
        [Browsable(true)]
        [DefaultValue(100)]
        [Category("Limits")]
        public decimal MaxTravelZ { get; set; } = 100;
    }
}
