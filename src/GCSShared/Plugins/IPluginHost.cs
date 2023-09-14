using GSendShared.Interfaces;

namespace GSendShared.Plugins
{
    public interface IPluginHost
    {
        PluginUsage Usage { get; }

        void AddMenu(IPluginMenu pluginMenu);

        void AddToolbar(IPluginToolbarButton toolbarButton);

        void AddMessage(InformationType informationType, string message);

        void AddPlugin(IGSendPluginModule pluginModule);
    }
}
