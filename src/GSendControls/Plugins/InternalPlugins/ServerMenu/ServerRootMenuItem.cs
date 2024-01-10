using System;
using System.Collections.Generic;
using System.Drawing;

using GSendControls.Abstractions;

using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins.InternalPlugins.ServerMenu
{
    public sealed class ServerRootMenuItem : IPluginMenu
    {
        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public IPluginMenu ParentMenu => null;

        public string Text => "Server";

        public int Index => 5;

        public bool ReceiveClientMessages => false;

        public void Clicked()
        {

        }

        public void ClientMessageReceived(IClientBaseMessage clientMessage)
        {

        }

        public bool GetShortcut(in List<int> defaultKeys, out string groupName, out string shortcutName)
        {
            groupName = String.Empty;
            shortcutName = String.Empty;
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

        }
    }
}
