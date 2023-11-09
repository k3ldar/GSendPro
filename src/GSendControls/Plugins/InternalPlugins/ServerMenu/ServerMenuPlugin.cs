using System;
using System.Collections.Generic;

using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins.InternalPlugins.ServerMenu
{
    /// <summary>
    /// Generic menu for configuring servers
    /// </summary>
    public sealed class ServerMenuPlugin : IGSendPluginModule
    {
        private List<IPluginMenu> _pluginMenus;
        private IPluginHost _pluginHost;

        public string Name => "Server Menu";

        public ushort Version => 1;

        public PluginHosts Host => PluginHosts.Editor | PluginHosts.SenderHost;

        public PluginOptions Options => PluginOptions.HasMenuItems;

        public IReadOnlyList<IPluginMenu> MenuItems
        {
            get
            {
                if (_pluginMenus == null)
                {
                    IPluginMenu parentServerMenu = new ServerRootMenuItem();
                    _pluginHost.AddMenu(parentServerMenu);

                    _pluginMenus = new List<IPluginMenu>
                    {
                        new ConfigureServerMenuItem(parentServerMenu),
                        new SeperatorMenu(parentServerMenu, 1),
                    };

                    for (int i = 0; i < 10; i++)
                    {
                        _pluginMenus.Add(new ServerSelectMenuItem(parentServerMenu, i + 2));
                    }
                }

                return _pluginMenus;
            }
        }

        public IReadOnlyList<IPluginToolbarButton> ToolbarItems => null;

        public void ClientMessageReceived(IClientBaseMessage clientBaseMessage)
        {
            // from interface, not used in this context
        }

        public void Initialize(IPluginHost pluginHost)
        {
            _pluginHost = pluginHost ?? throw new ArgumentNullException(nameof(pluginHost));
        }
    }
}
