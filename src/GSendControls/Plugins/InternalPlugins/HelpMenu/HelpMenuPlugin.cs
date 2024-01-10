using System;
using System.Collections.Generic;
using GSendControls.Abstractions;
using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins.InternalPlugins.HelpMenu
{
    public sealed class HelpMenuPlugin : IGSendPluginModule
    {
        private List<IPluginMenu> _pluginMenus;
        private IPluginHost _pluginHost;

        public string Name => "Help Menu";

        public ushort Version => 1;

        public PluginHosts Host => PluginHosts.Any;

        public PluginOptions Options => PluginOptions.HasMenuItems;

        public IReadOnlyList<IPluginMenu> MenuItems
        {
            get
            {
                if (_pluginMenus == null)
                {
                    IPluginMenu parentHelpMenu = _pluginHost.GetMenu(MenuParent.Help);

                    _pluginMenus =
                    [
                        new SeperatorMenu(parentHelpMenu, 0),
                        new SeperatorMenu(parentHelpMenu, 0),
                        new SeperatorMenu(parentHelpMenu, 0),
                        new HelpMenuItem(parentHelpMenu),
                        new BugsAndIdeasMenu(parentHelpMenu),
                        new HomePageMenu(parentHelpMenu),
                    ];
                }

                return _pluginMenus;
            }
        }

        public IReadOnlyList<IPluginToolbarButton> ToolbarItems => null;

        public IReadOnlyList<IPluginControl> ControlItems => null;

        public void ClientMessageReceived(IClientBaseMessage clientBaseMessage)
        {
            // from interface, not used in any context
        }

        public void Initialize(IPluginHost pluginHost)
        {
            _pluginHost = pluginHost ?? throw new ArgumentNullException(nameof(pluginHost));
        }
    }
}
