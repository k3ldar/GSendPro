using GSendShared.Interfaces;

namespace GSendShared.Plugins
{
    public interface IPluginHelper
    {
        void InitializeAllPlugins(ISenderPluginHost pluginHost);

        void AddMenu(object parent, IPluginMenu menu);

        void AddShortcut(List<IShortcut> shortcuts, IShortcut shortcut);

        //void AddToolbarButton(object parent, IPluginToolbarButton toolbarButton);
    }
}
