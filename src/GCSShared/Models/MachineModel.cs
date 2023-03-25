namespace GSendShared.Models
{
    public sealed class MachineModel : IMachine
    {
        public MachineModel()
        {

        }

        public MachineModel(long id, string name, MachineType machineType, string comPort, MachineOptions options, byte axisCount, 
            GrblSettings settings, DisplayUnits displayUnits, int overrideSpeed, int overrideSpindle, DateTime configurationLastVerified,
            string probeCommand)
            : this()
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Id = id;
            Name = name;
            MachineType = machineType;
            ComPort = comPort;
            Options = options;
            AxisCount = axisCount;
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
            DisplayUnits = displayUnits;
            OverrideSpeed = overrideSpeed;
            OverrideSpindle = overrideSpindle;
            ConfigurationLastVerified = configurationLastVerified;
            ProbeCommand = probeCommand;
        }

        public long Id { get; set; } = Int64.MinValue;

        public string Name { get; set; }

        public MachineType MachineType { get; set; }

        public string ComPort { get; set; }

        public MachineOptions Options { get; set; }

        public byte AxisCount { get; set; }

        public GrblSettings Settings { get; set; }

        public DisplayUnits DisplayUnits { get; set; }

        public int OverrideSpeed { get; set; }

        public int OverrideSpindle { get; set; }

        public DateTime ConfigurationLastVerified { get; set; }

        public string ProbeCommand { get; set; }
    }
}
