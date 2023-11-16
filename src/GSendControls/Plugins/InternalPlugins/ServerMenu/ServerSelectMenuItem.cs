using System;
using System.Drawing;
using GSendControls.Abstractions;
using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins.InternalPlugins.ServerMenu
{
    public sealed class ServerSelectMenuItem : IPluginMenu
    {
        public ServerSelectMenuItem(IPluginMenu parentMenu, int index)
        {
            ParentMenu = parentMenu ?? throw new ArgumentNullException(nameof(parentMenu));
            Index = index;
        }

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public IPluginMenu ParentMenu { get; }

        public string Text => "127.0.0.1:7050";

        public int Index { get; }

        public bool ReceiveClientMessages => false;

        public void Clicked()
        {

        }

        public void ClientMessageReceived(IClientBaseMessage clientMessage)
        {

        }

        public bool GetShortcut(out string groupName, out string shortcutName)
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
            return Index % 2 == 0;
        }

        public void UpdateHost<T>(T senderPluginHost)
        {

        }
    }
}
