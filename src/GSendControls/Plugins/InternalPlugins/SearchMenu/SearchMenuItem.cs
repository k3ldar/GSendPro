using System.Collections.Generic;
using System.Drawing;

using GSendControls.Abstractions;

using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins.InternalPlugins.SearchMenu
{
    public sealed class SearchMenuItem : IPluginMenu
    {
        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public IPluginMenu ParentMenu => null;

        public string Text => GSend.Language.Resources.SearchMenu;

        public int Index => 3;

        public bool ReceiveClientMessages => false;

        public void Clicked()
        {
            // from interface, not used in this context
        }

        public void ClientMessageReceived(IClientBaseMessage clientMessage)
        {
            // from interface, not used in this context
        }

        public bool GetShortcut(in List<int> defaultKeys, out string groupName, out string shortcutName)
        {
            groupName = null;
            shortcutName = null;
            return false;
        }

        public bool IsChecked()
        {
            return false;
        }

        public bool IsEnabled()
        {
            return true;
        }

        public bool IsVisible()
        {
            return true;
        }

        public void UpdateHost<T>(T senderPluginHost)
        {
            // from interface, not used in this context
        }
    }
}
