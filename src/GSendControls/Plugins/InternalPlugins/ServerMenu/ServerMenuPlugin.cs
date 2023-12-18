using System;
using System.Collections.Generic;
using System.Windows.Forms;

using GSendControls.Abstractions;
using GSendControls.Threads;

using GSendShared;
using GSendShared.Interfaces;
using GSendShared.Plugins;

using Shared.Classes;

namespace GSendControls.Plugins.InternalPlugins.ServerMenu
{
    /// <summary>
    /// Generic menu for configuring servers
    /// </summary>
    public sealed class ServerMenuPlugin : IGSendPluginModule
    {
        private readonly ICommonUtils _commonUtils;
        private const int MaximumServerNameMenuItems = 10;
        private List<IPluginMenu> _pluginMenus;
        private IPluginHost _pluginHost;
        private readonly IRunProgram _runProgram;

        public ServerMenuPlugin(IServerConfigurationUpdated serverConfigurationUpdated, ICommonUtils commonUtils, IRunProgram runProgram)
        {
            _commonUtils = commonUtils ?? throw new ArgumentNullException(nameof(commonUtils));
            _runProgram = runProgram ?? throw new ArgumentNullException(nameof(runProgram));

            if (serverConfigurationUpdated == null) 
                throw new ArgumentNullException(nameof(serverConfigurationUpdated));

            serverConfigurationUpdated.OnServerConfigurationUpdated += ServerConfigurationUpdated_OnServerConfigurationUpdated;
        }

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

                    for (int i = 0; i < MaximumServerNameMenuItems; i++)
                    {
                        _pluginMenus.Add(new ServerSelectMenuItem(parentServerMenu, i + 2));
                    }

                    UpdateServerMenuItems();
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

        private void ServerConfigurationUpdated_OnServerConfigurationUpdated(object sender, EventArgs e)
        {
            _pluginMenus.ForEach(pi =>
            {
                if (pi is ServerSelectMenuItem menu)
                    menu.UpdateServerAddress(null);
            });

            UpdateServerMenuItems();
        }

        private void UpdateServerMenuItems()
        {
            if (_commonUtils.GetGSendCS(out string gSendCS))
            {
                string[] servers = _runProgram.Run(gSendCS, "Server Show").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                for (int i = 2; i < servers.Length; i++)
                {
                    string line = servers[i];

                    if (Uri.TryCreate(line, UriKind.Absolute, out Uri uri) && _pluginMenus[i -2] is ServerSelectMenuItem serverMenuItem)
                    {
                        serverMenuItem.UpdateServerAddress(uri);
                    }
                }
            }
        }
    }
}
