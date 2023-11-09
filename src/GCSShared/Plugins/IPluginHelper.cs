using GSendShared.Interfaces;

namespace GSendShared.Plugins
{
    public interface IPluginHelper
    {
        /// <summary>
        /// Initializes all plugins within a host
        /// </summary>
        /// <param name="pluginHost"></param>
        void InitializeAllPlugins(IPluginHost pluginHost);

        /// <summary>
        /// Adds a menu to a host
        /// </summary>
        /// <param name="pluginHost"></param>
        /// <param name="parent"></param>
        /// <param name="menu"></param>
        /// <param name="shortcuts"></param>
        void AddMenu(IPluginHost pluginHost, object parent, IPluginMenu menu, List<IShortcut> shortcuts);

        /// <summary>
        /// Adds a popup menu to a host
        /// </summary>
        /// <param name="pluginHost"></param>
        /// <param name="parent"></param>
        /// <param name="menu"></param>
        /// <param name="shortcuts"></param>
        void AddPopupMenu(IPluginHost pluginHost, object parent, IPluginMenu menu, List<IShortcut> shortcuts);

        /// <summary>
        /// Adds a toolbar button to a host
        /// </summary>
        /// <param name="pluginHost"></param>
        /// <param name="parent"></param>
        /// <param name="toolbarButton"></param>
        void AddToolbarButton(IPluginHost pluginHost, object parent, IPluginToolbarButton toolbarButton);
    }
}
