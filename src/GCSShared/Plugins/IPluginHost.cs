namespace GSendShared.Plugins
{
    public interface IPluginHost
    {
        PluginHosts Host { get; }

        int MaximumMenuIndex { get; }

        IPluginMenu GetMenu(MenuParent menuParent);

        void AddMenu(IPluginMenu pluginMenu);

        void AddToolbar(IPluginToolbarButton toolbarButton);

        void AddMessage(InformationType informationType, string message);

        void AddPlugin(IGSendPluginModule pluginModule);
    }
}
