using System;
using System.Drawing;

using GSendControls.Abstractions;

using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins.InternalPlugins.ServerMenu
{
    public sealed class ServerSelectMenuItem : IPluginMenu
    {
        private Uri _uri = null;

        public ServerSelectMenuItem(IPluginMenu parentMenu, int index)
        {
            ParentMenu = parentMenu ?? throw new ArgumentNullException(nameof(parentMenu));
            Index = index;
        }

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public IPluginMenu ParentMenu { get; }

        public string Text => _uri == null ? String.Empty : $"{_uri.Host}:{_uri.Port}";

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
            return IsVisible();
        }

        public bool IsVisible()
        {
            return _uri != null;
        }

        public void UpdateHost<T>(T senderPluginHost)
        {

        }

        internal void UpdateServerAddress(Uri uri)
        {
            _uri = uri;
        }
    }
}
