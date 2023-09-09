using GSendShared.Plugins;

namespace GrblTuningWizard
{
    public sealed class GSendProTuningWizardPlugin : IPluginModule
    {
        public string Name => "GRBL Tuning Wizard";

        public ushort Version => 1;

        public PluginUsage Usage => PluginUsage.Sender;

        public PluginOptions Options => PluginOptions.HasToolbarButtons | PluginOptions.HasMenuItems;

        public IReadOnlyList<IPluginMenu> MenuItems
        {
            get
            {
                List<IPluginMenu> Result = new()
                {
                    new TuningWizardMenuItem()
                };

                return Result;
            }
        }
    }
}
