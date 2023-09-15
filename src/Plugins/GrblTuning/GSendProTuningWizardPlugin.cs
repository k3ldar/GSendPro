﻿using GSendShared;
using GSendShared.Plugins;

namespace GrblTuningWizard
{
    public sealed class GSendProTuningWizardPlugin : IGSendPluginModule
    {
        public string Name => "GRBL Tuning Wizard";

        public ushort Version => 1;

        public PluginHosts Host => PluginHosts.Sender;

        public PluginOptions Options => PluginOptions.HasMenuItems;

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

        public IReadOnlyList<IPluginToolbarButton> ToolbarItems => null;

        public void ClientMessageReceived(IClientBaseMessage clientBaseMessage)
        {

        }
    }
}
