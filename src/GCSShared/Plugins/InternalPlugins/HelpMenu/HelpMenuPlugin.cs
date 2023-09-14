namespace GSendShared.Plugins.InternalPlugins.HelpMenu
{
    public sealed class HelpMenuPlugin : IGSendPluginModule
    {
        public string Name => "Help Menu";

        public ushort Version => 1;

        public PluginUsage Usage => PluginUsage.Any;

        public PluginOptions Options => PluginOptions.HasMenuItems | PluginOptions.HasToolbarButtons;

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

        public IReadOnlyList<IPluginToolbarButton> ToolbarItems
        {
            get
            {
                return new List<IPluginToolbarButton>()
                {
                    new SeperatorButton(15),
                    new SeperatorButton(16),
                };
            }
        }

        public void ClientMessageReceived(IClientBaseMessage clientBaseMessage)
        {
            // from interface, not used in any context
        }
    }
}
