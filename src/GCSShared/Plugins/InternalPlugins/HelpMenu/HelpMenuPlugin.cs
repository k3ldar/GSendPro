namespace GSendShared.Plugins.InternalPlugins.HelpMenu
{
    public sealed class HelpMenuPlugin : IGSendPluginModule
    {
        public string Name => "Help Menu";

        public ushort Version => 1;

        public PluginHosts Host => PluginHosts.Any;

        public PluginOptions Options => PluginOptions.HasMenuItems;

        public IReadOnlyList<IPluginMenu> MenuItems
        {
            get
            {
                return new List<IPluginMenu>
                {
                    new HelpMenuItem(),
                    new SeperatorMenu(MenuParent.Help, 1),
                    new BugsAndIdeasMenu(),
                    new SeperatorMenu(MenuParent.Help, 3),
                    new HomePageMenu(),
                    new SeperatorMenu(MenuParent.Help, 5),
                };
            }
        }

        public IReadOnlyList<IPluginToolbarButton> ToolbarItems => null;

        public void ClientMessageReceived(IClientBaseMessage clientBaseMessage)
        {
            // from interface, not used in any context
        }
    }
}
