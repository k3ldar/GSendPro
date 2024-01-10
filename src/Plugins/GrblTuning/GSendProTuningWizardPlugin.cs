﻿using GSendControls.Abstractions;
using GSendShared;
using GSendShared.Plugins;

namespace GrblTuningWizard
{
    public sealed class GSendProTuningWizardPlugin : IGSendPluginModule
    {
        private List<IPluginMenu> _menuItems;
        private IPluginHost _pluginHost;

        public string Name => "GRBL Tuning Wizard";

        public ushort Version => 1;

        public PluginHosts Host => PluginHosts.Sender;

        public PluginOptions Options => PluginOptions.HasMenuItems | PluginOptions.MessageReceived;

        public IReadOnlyList<IPluginMenu> MenuItems
        {
            get
            {
                _menuItems ??=
                    [
                        new TuningWizardMenuItem(_pluginHost.GetMenu(MenuParent.Tools))
                    ];

                return _menuItems;
            }
        }

        public IReadOnlyList<IPluginToolbarButton> ToolbarItems => null;

        public IReadOnlyList<IPluginControl> ControlItems => null;

        public void ClientMessageReceived(IClientBaseMessage clientBaseMessage)
        {

        }

        public void Initialize(IPluginHost pluginHost)
        {
            _pluginHost = pluginHost ?? throw new ArgumentNullException(nameof(pluginHost));
        }
    }
}
