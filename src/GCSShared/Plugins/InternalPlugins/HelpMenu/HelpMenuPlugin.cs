﻿namespace GSendShared.Plugins.InternalPlugins.HelpMenu
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

                    _pluginMenus = new List<IPluginMenu>
                    {
                        new HelpMenuItem(parentHelpMenu),
                        new SeperatorMenu(parentHelpMenu, 1),
                        new BugsAndIdeasMenu(parentHelpMenu),
                        new SeperatorMenu(parentHelpMenu, 3),
                        new HomePageMenu(parentHelpMenu),
                        new SeperatorMenu(parentHelpMenu, 5)
                    };
                }

                return _pluginMenus;
            }
        }

        public IReadOnlyList<IPluginToolbarButton> ToolbarItems => null;

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