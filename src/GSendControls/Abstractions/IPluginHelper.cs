using System.Collections.Generic;
using System.Windows.Forms;

using GSendShared.Interfaces;

namespace GSendControls.Abstractions
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
        void AddMenu(IPluginHost pluginHost, MenuStrip parent, IPluginMenu menu, List<IShortcut> shortcuts);

        /// <summary>
        /// Adds a popup menu to a host
        /// </summary>
        /// <param name="pluginHost"></param>
        /// <param name="parent"></param>
        /// <param name="menu"></param>
        /// <param name="shortcuts"></param>
        void AddPopupMenu(IPluginHost pluginHost, ContextMenuStrip parent, IPluginMenu menu, List<IShortcut> shortcuts);

        /// <summary>
        /// Adds a toolbar button to a host
        /// </summary>
        /// <param name="pluginHost"></param>
        /// <param name="parent"></param>
        /// <param name="toolbarButton"></param>
        void AddToolbarButton(IPluginHost pluginHost, ToolStrip parent, IPluginToolbarButton toolbarButton);
    }
}
