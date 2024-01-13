using System;
using System.Collections.Generic;

using GSendControls.Abstractions;

using GSendShared;
using GSendShared.Plugins;

using Microsoft.Extensions.DependencyInjection;

using Shared.Classes;

namespace GSendControls.Plugins.InternalPlugins.HelpMenu
{
    public sealed class HelpMenuPlugin : IGSendPluginModule
    {
        private List<IPluginMenu> _pluginMenus;
        private IPluginHost _pluginHost;
        private IRunProgram _runProgram;

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
                        new HelpMenuItem(parentHelpMenu, _runProgram),
                        new BugsAndIdeasMenu(parentHelpMenu, _runProgram),
                        new HomePageMenu(parentHelpMenu, _runProgram),
                    ];
                }

                return _pluginMenus;
            }
        }

        public IReadOnlyList<IPluginToolbarButton> ToolbarItems => null;

        public IReadOnlyList<IPluginControl> ControlItems => null;

        public bool ReceiveClientMessages => false;

        public void ClientMessageReceived(IClientBaseMessage clientMessage)
        {
            // from interface, not used in this context
        }

        public void Initialize(IPluginHost pluginHost)
        {
            _pluginHost = pluginHost ?? throw new ArgumentNullException(nameof(pluginHost));
            _runProgram = pluginHost.GSendContext.ServiceProvider.GetService<IRunProgram>() ?? throw new InvalidOperationException("IRunProgram not registered");
        }
    }
}
