using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSendShared.Plugins
{
    /// <summary>
    /// Interface created by a plugin to add menu items to the host
    /// </summary>
    public interface IPluginMenu
    {
        /// <summary>
        /// Menu name/text
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Preferred index of item in relation to other items in the menu
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Type of menu
        /// </summary>
        MenuType MenuType { get; }

        /// <summary>
        /// Parent menu item
        /// </summary>
        MenuParent ParentMenu { get; }

        /// <summary>
        /// Click event when menu clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Clicked ();
    }
}
