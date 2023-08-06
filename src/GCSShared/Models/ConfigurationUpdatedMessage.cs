namespace GSendShared.Models
{
    public sealed class ConfigurationUpdatedMessage
    {
        public string Name { get; set; }

        public string Comport { get; set; }

        public MachineFirmware MachineFirmware { get; set; }

        public MachineType MachineType { get; set; }
    }
}
