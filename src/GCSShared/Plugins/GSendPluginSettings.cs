namespace GSendShared.Plugins
{
    public class GSendPluginSettings
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string AssemblyName { get; set; }

        public PluginHosts Usage { get; set; }

        public MachineType MachineType { get; set; }

        public MachineFirmware MachineFirmware { get; set; }

        public bool Enabled { get; set; }

        public bool ShowToolbarItems { get; set; }
    }
}
