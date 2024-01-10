using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Abstractions
{
    public interface IPluginHost
    {
        PluginHosts Host { get; }

        int MaximumMenuIndex { get; }

        IGSendContext GSendContext { get; }

        IPluginMenu GetMenu(MenuParent menuParent);

        void AddMenu(IPluginMenu pluginMenu);

        void AddToolbar(IPluginToolbarButton toolbarButton);

        void AddControl(IPluginControl pluginControl);

        void AddMessage(InformationType informationType, string message);

        void AddPlugin(IGSendPluginModule pluginModule);
    }
}
