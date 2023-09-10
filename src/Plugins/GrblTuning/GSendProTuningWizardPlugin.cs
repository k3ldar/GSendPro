using GSendShared;
using GSendShared.Interfaces;
using GSendShared.Plugins;

namespace GrblTuningWizard
{
    public sealed class GSendProTuningWizardPlugin : IGSendPluginModule
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

        public IReadOnlyList<IShortcut> Shortcuts => new List<IShortcut>();

        public void ClientMessageReceived(IClientBaseMessage clientBaseMessage)
        {

        }
    }
}
