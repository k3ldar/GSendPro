using System;
using System.Collections.Generic;

using GSendControls.Abstractions;

using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins.InternalPlugins.SearchMenu
{
    public sealed class SearchMenuPlugin : IGSendPluginModule
    {
        private IEditorPluginHost _pluginHost;
        private List<IPluginMenu> _pluginMenus;

        public string Name => "Search Menu";

        public ushort Version => 1;

        public PluginHosts Host => PluginHosts.Editor;

        public PluginOptions Options => PluginOptions.HasMenuItems;

        public IReadOnlyList<IPluginMenu> MenuItems
        {
            get
            {
                if (_pluginMenus == null)
                {
                    SearchMenuItem searchMenuItem = new();

                    _pluginMenus = [
                        searchMenuItem,
                        new FindMenuItem(searchMenuItem, _pluginHost.Editor),
                        new ReplaceMenuItem(searchMenuItem, _pluginHost.Editor),
                        new SeperatorMenu(searchMenuItem, 19),
                        new GotoMenuItem(searchMenuItem, _pluginHost.Editor)
                    ];
                }

                return _pluginMenus;
            }
        }

        public IReadOnlyList<IPluginToolbarButton> ToolbarItems => null;

        public IReadOnlyList<IPluginControl> ControlItems => null;

        public void ClientMessageReceived(IClientBaseMessage clientBaseMessage)
        {
            // from interface, not used in this context
        }

        public void Initialize(IPluginHost pluginHost)
        {
            _pluginHost = pluginHost as IEditorPluginHost ?? throw new ArgumentNullException(nameof(pluginHost));
        }
    }
}
