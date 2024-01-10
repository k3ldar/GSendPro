using System.Collections.Generic;
using System.Drawing;

using GSendShared.Plugins;

namespace GSendControls.Abstractions
{
    /// <summary>
    /// Interface created by a plugin to add menu items to the host
    /// </summary>
    public interface IPluginMenu : IPluginItemBase
    {
        /// <summary>
        /// Image to be displayed with menu
        /// </summary>
        Image MenuImage { get; }

        /// <summary>
        /// Type of menu
        /// </summary>
        MenuType MenuType { get; }

        /// <summary>
        /// Parent menu item
        /// </summary>
        IPluginMenu ParentMenu { get; }

        /// <summary>
        /// Determines whether the menu is checked or not
        /// </summary>
        /// <returns></returns>
        bool IsChecked();

        /// <summary>
        /// Determines whether the menu is visible or not
        /// </summary>
        /// <returns></returns>
        bool IsVisible();

        /// <summary>
        /// Shortcut that can be used by the menu
        /// </summary>
        /// <param name="defaultKeys">default keys for shortcut, if available</param>
        /// <param name="groupName"></param>
        /// <param name="shortcutName"></param>
        /// <returns></returns>
        bool GetShortcut(in List<int> defaultKeys, out string groupName, out string shortcutName);
    }
}
