﻿using System.Drawing;

namespace GSendShared.Plugins
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
        MenuParent ParentMenu { get; }

        /// <summary>
        /// Determines whether the menu is checked or not
        /// </summary>
        /// <returns></returns>
        bool IsChecked();

        /// <summary>
        /// Shortcut that can be used by the menu
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="shortcutName"></param>
        /// <returns></returns>
        bool GetShortcut(out string groupName, out string shortcutName);
    }
}
