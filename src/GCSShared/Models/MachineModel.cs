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
        }

        public long Id { get; set; } = Int64.MinValue;

        public string Name { get; set; }

        public MachineType MachineType { get; set; }

        public string ComPort { get; set; }

        public MachineOptions Options { get; set; }

        public byte AxisCount { get; set; }

        public Dictionary<uint, decimal> Settings { get; set; }
    }
}
