namespace GSendShared.Models
{
    public sealed class MachineModel : IMachine
    {
        public MachineModel()
        {

        }

        public MachineModel(long id, string name, MachineType machineType, MachineFirmware machineFirmware, string comPort, MachineOptions options, byte axisCount,
            GrblSettings settings, FeedRateDisplayUnits displayUnits, FeedbackUnit feedbackUnit, int overrideSpeed, int overrideSpindle,
            int overrideZDown, int overrideZUp, DateTime configurationLastVerified, decimal layerHeightWarning,
            string probeCommand, int probeSpeed, decimal probeThickness, int jogFeedRate, int jogUnits, SpindleType spindleType,
            int softStartSeconds, int serviceWeeks, int serviceSpindleHours)
            : this()
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Id = id;
            Name = name;
            MachineType = machineType;
            MachineFirmware = machineFirmware;
            ComPort = comPort;
            Options = options;
            AxisCount = axisCount;
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            DisplayUnits = displayUnits;
            FeedbackUnit = feedbackUnit;
            OverrideSpeed = overrideSpeed;
            OverrideSpindle = overrideSpindle;
            OverrideZDownSpeed = overrideZDown;
            OverrideZUpSpeed = overrideZUp;
            ConfigurationLastVerified = configurationLastVerified;
            LayerHeightWarning = layerHeightWarning;
            ProbeCommand = probeCommand;
            ProbeSpeed = probeSpeed;
            ProbeThickness = probeThickness;
            JogFeedrate = jogFeedRate;
            JogUnits = jogUnits;
            SpindleType = spindleType;
            SoftStartSeconds = softStartSeconds;
            ServiceWeeks = serviceWeeks;
            ServiceSpindleHours = serviceSpindleHours;
        }

        public long Id { get; set; } = Int64.MinValue;

        public string Name { get; set; }

        public MachineType MachineType { get; set; }

        public MachineFirmware MachineFirmware { get; set; }

        public string ComPort { get; set; }

        public MachineOptions Options { get; set; }

        public byte AxisCount { get; set; }

        public GrblSettings Settings { get; set; }

        public FeedRateDisplayUnits DisplayUnits { get; set; }

        public FeedbackUnit FeedbackUnit { get; set; }

        public int OverrideSpeed { get; set; }

        public int OverrideSpindle { get; set; }

        public int OverrideZDownSpeed { get; set; }

        public int OverrideZUpSpeed { get; set; }

        public DateTime ConfigurationLastVerified { get; set; }

        public string ProbeCommand { get; set; }

        public int ProbeSpeed { get; set; }

        public decimal ProbeThickness { get; set; }

        public int JogUnits { get; set; }

        public int JogFeedrate { get; set; }

        public SpindleType SpindleType { get; set; }

        public int SoftStartSeconds { get; set; }

        public bool SoftStart => Options.HasFlag(MachineOptions.SoftStart);

        public int ServiceWeeks { get; set; }

        public int ServiceSpindleHours { get; set; }

        public decimal LayerHeightWarning { get; set; }

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
