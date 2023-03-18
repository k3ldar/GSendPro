namespace GSendShared.Models
{
    public sealed class MachineModel : IMachine
    {
        public MachineModel()
        {
        }

        public MachineModel(long id, string name, MachineType machineType, string comPort, MachineOptions options, byte axisCount, 
            Dictionary<uint, decimal> settings)
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
            Settings = settings;

            AddDefaultSettingsIfNotPresent();
        }

        public long Id { get; set; } = Int64.MinValue;

        public string Name { get; set; }

        public MachineType MachineType { get; set; }

        public string ComPort { get; set; }

        public MachineOptions Options { get; set; }

        public byte AxisCount { get; set; }

        public Dictionary<uint, decimal> Settings { get; set; }

        public DisplayUnits DisplayUnits { get; set; }

        public int OverrideSpeed { get; set; }

        public int OverrideSpindle { get; set; }

        private void AddDefaultSettingsIfNotPresent()
        {
            AddValue(0, 10);
            AddValue(1, 25);
            AddValue(2, 0);
            AddValue(3, 0);
            AddValue(4, 0);
            AddValue(5, 0);
            AddValue(6, 0);
            AddValue(10, 1);
            AddValue(11, 0.010m);
            AddValue(12, 0.002m);
            AddValue(13, 0);
            AddValue(20, 0);
            AddValue(21, 0);
            AddValue(22, 1);
            AddValue(23, 0);
            AddValue(24, 25);
            AddValue(25, 500);
            AddValue(26, 250);
            AddValue(27, 1);
            AddValue(30, 1000);
            AddValue(31, 0);
            AddValue(32, 0);
            AddValue(100, 250);
            AddValue(101, 250);
            AddValue(102, 250);
            AddValue(110, 2000);
            AddValue(111, 2000);
            AddValue(112, 100);
            AddValue(120, 10);
            AddValue(121, 10);
            AddValue(122, 10);
            AddValue(130, 200);
            AddValue(131, 200);
            AddValue(132, 100);
        }

        private void AddValue(uint option, decimal value)
        {
            if (!Settings.ContainsKey(option))
                Settings.Add(option, value);
        }
    }
}
