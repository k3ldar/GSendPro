using GSendShared;
using GSendShared.Plugins;

namespace GrblTuningWizard
{
    public sealed class GSendProTuningWizardPlugin : IGSendPluginModule
    {
        public string Name => "GRBL Tuning Wizard";

        public ushort Version => 1;

        public PluginHosts Host => PluginHosts.Sender;

        public PluginOptions Options => PluginOptions.HasMenuItems | PluginOptions.MessageReceived;

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1168:Empty arrays and collections should be returned instead of null", Justification = "Expecting null if non available")]
        public IReadOnlyList<IPluginToolbarButton> ToolbarItems => null;

        public void ClientMessageReceived(IClientBaseMessage clientBaseMessage)
        {

        }
    }
}
