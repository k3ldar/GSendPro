using GSendShared.Interfaces;

namespace GSendShared.Plugins
{
    public interface IPluginHelper
    {
        void InitializeAllPlugins(ISenderPluginHost pluginHost);

        void AddMenu(object parent, IPluginMenu menu, List<IShortcut> shortcuts);

        //void AddToolbarButton(object parent, IPluginToolbarButton toolbarButton);
    }
}
