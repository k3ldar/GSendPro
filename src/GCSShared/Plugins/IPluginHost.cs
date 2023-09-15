using GSendShared.Interfaces;

namespace GSendShared.Plugins
{
    public interface IPluginHost
    {
        PluginHosts Host { get; }

        void AddMenu(IPluginMenu pluginMenu);

        void AddToolbar(IPluginToolbarButton toolbarButton);

        void AddMessage(InformationType informationType, string message);

        void AddPlugin(IGSendPluginModule pluginModule);
    }
}
