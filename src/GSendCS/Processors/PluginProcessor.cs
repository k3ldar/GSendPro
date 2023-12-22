using System.Text;
using System.Text.Json;

using CommandLinePlus;

using GSendShared;
using GSendShared.Plugins;

using static GSendCS.Internal.Constants;

namespace GSendCS.Processors
{
    internal sealed class PluginProcessor : BaseCommandLine, IDisposable
    {
        private readonly string _fileName;
        private FileStream _fileStream;

        internal PluginProcessor(ICommandLineArguments args, IDisplay display, string pluginFileName)
            : base (args, display)
        {
            if (String.IsNullOrEmpty(pluginFileName))
                throw new ArgumentNullException(nameof(pluginFileName));

            _fileName = pluginFileName;
            LoadFileStream();
            IsExclusive = _fileStream != null;
        }

        public PluginProcessor(string pluginFileName)
        {
            if (String.IsNullOrEmpty(pluginFileName))
                throw new ArgumentNullException(nameof(pluginFileName));

            _fileName = pluginFileName;
            LoadFileStream();
            IsExclusive = _fileStream != null;
        }

        public bool IsExclusive { get; }

        public override string Name => "Plugin";

        public override int SortOrder => 0;

        public override bool IsEnabled => true;

        public override void DisplayHelp()
        {

        }

        public override int Execute(string[] args)
        {
            Display.WriteLine("Invalid Options!");
            return -1;
        }

        [CmdLineDescription("Adds a new plugin")]
        public int Add(
            [CmdLineAbbreviation("p", "Name of plugin")] string pluginName,
            [CmdLineAbbreviation("a", "Assembly name for plugin")] string assemblyName,
            [CmdLineAbbreviation("u", "Hosts that can load the plugin")] PluginHosts usage,
            [CmdLineAbbreviation("m", "Type of machine the plugin is targeting.")] MachineType machineType,
            [CmdLineAbbreviation("f", "Firmware the plugin is targeting")] MachineFirmware machineFirmware,
            [CmdLineAbbreviation("e", "Set's enabled state")] bool enabled = true,
            [CmdLineAbbreviation("t", "Indicates that plugin contains tool bar items")] bool showToolbarItems = false,
            [CmdLineAbbreviation("d", "Description")]string description = null)
        {
            if (!ValidateExclusiveAccess())
                return ResponseExclusiveAccessDenied;

            List<GSendPluginSettings> settings = LoadPluginSettings();

            GSendPluginSettings plugin = settings.Find(p => p.Name.Equals(pluginName, StringComparison.OrdinalIgnoreCase));

            if (plugin == null)
            {
                plugin = new()
                {
                    Name = pluginName,
                    AssemblyName = assemblyName,
                    Usage = usage,
                    MachineFirmware = machineFirmware,
                    MachineType = machineType,
                    Description = description,
                    Enabled = enabled,
                    ShowToolbarItems = showToolbarItems
                };

                settings.Add(plugin);
                WriteFileStream(settings);

                return ResponseSuccess;
            }

            return ResponseExists;
        }

        [CmdLineDescription("Updates an existing plugin")]
        public int Update(
            [CmdLineAbbreviation("p", "Name of plugin")] string pluginName,
            [CmdLineAbbreviation("a", "Assembly name for plugin")] string assemblyName,
            [CmdLineAbbreviation("u", "Hosts that can load the plugin")] PluginHosts usage,
            [CmdLineAbbreviation("m", "Type of machine the plugin is targeting.")] MachineType machineType,
            [CmdLineAbbreviation("f", "Firmware the plugin is targeting")] MachineFirmware machineFirmware,
            [CmdLineAbbreviation("e", "Set's enabled state")] bool enabled = true,
            [CmdLineAbbreviation("t", "Indicates that plugin contains tool bar items")] bool showToolbarItems = false,
            [CmdLineAbbreviation("d", "Description")] string description = null)
        {
            if (!ValidateExclusiveAccess())
                return ResponseExclusiveAccessDenied;

            List<GSendPluginSettings> settings = LoadPluginSettings();

            GSendPluginSettings plugin = settings.Find(p => p.Name.Equals(pluginName, StringComparison.OrdinalIgnoreCase));

            if (plugin == null)
                return ResponseDoesNotExist;

            plugin.Name = pluginName;
            plugin.AssemblyName = assemblyName;
            plugin.Usage = usage;
            plugin.MachineFirmware = machineFirmware;
            plugin.MachineType = machineType;
            plugin.Description = description;
            plugin.Enabled = enabled;
            plugin.ShowToolbarItems = showToolbarItems;

            WriteFileStream(settings);

            return ResponseSuccess;
        }

        [CmdLineDescription("Deletes an existing plugin")]
        public int Delete(
            [CmdLineAbbreviation("p", "Name of plugin")] string pluginName)
        {
            if (!ValidateExclusiveAccess())
                return ResponseExclusiveAccessDenied;

            List<GSendPluginSettings> settings = LoadPluginSettings();

            GSendPluginSettings plugin = settings.Find(p => p.Name.Equals(pluginName, StringComparison.OrdinalIgnoreCase));

            if (plugin == null)
                return ResponseDoesNotExist;

            settings.Remove(plugin);

            WriteFileStream(settings);

            return ResponseSuccess;
        }

        [CmdLineDescription("Displays plugin information in tablular format")]
        public int Show()
        {
            if (!ValidateExclusiveAccess())
                return ResponseExclusiveAccessDenied;

            List<GSendPluginSettings> settings = LoadPluginSettings();

            Display.WriteLine(VerbosityLevel.Quiet, $"Name\tAssembly Name\tEnabled\tUsage\tMachine Type\tFirmware\tShow Toolbar Items\tDescription");

            foreach (GSendPluginSettings setting in settings)
            {
                Display.WriteLine(VerbosityLevel.Quiet, $"{setting.Name}\t{setting.AssemblyName}\t\t{setting.Enabled}\t{setting.Usage}\t{setting.MachineType}\t\t{setting.MachineFirmware}\t{setting.ShowToolbarItems}\t{setting.Description}");
            }
                
            return Int32.MaxValue;
        }

        private bool ValidateExclusiveAccess()
        {
            if (!IsExclusive)
            {
                Display.WriteLine("Unable to gain access to settings file!");
            }

            return IsExclusive;
        }

        private List<GSendPluginSettings> LoadPluginSettings()
        {
            try
            {
                _fileStream.Position = 0;
                return JsonSerializer.Deserialize<List<GSendPluginSettings>>(_fileStream, GSendShared.Constants.DefaultJsonSerializerOptions);
            }
            catch
            {
                return new();
            }
        }

        private void LoadFileStream()
        {
            try
            {
                _fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText(_fileName, "[]");
                _fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            }
            catch
            {
                _fileStream = null;
            }
        }

        private void WriteFileStream(List<GSendPluginSettings> settings)
        {
            if (_fileStream == null)
                throw new InvalidOperationException("File Access Denied");

            byte[] fileData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(settings));
            
            _fileStream.Position = 0;
            _fileStream.Write(fileData);
            _fileStream.SetLength(_fileStream.Position);
            _fileStream.Flush();
        }

        [CmdLineHidden]
        public void Dispose()
        {
            _fileStream?.Dispose();
        }
    }
}
