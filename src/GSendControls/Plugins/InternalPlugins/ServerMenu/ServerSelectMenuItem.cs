using System;
using System.Collections.Generic;
using System.Drawing;

using GSendApi;

using GSendControls.Abstractions;

using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins.InternalPlugins.ServerMenu
{
    public sealed class ServerSelectMenuItem : IPluginMenu
    {
        private readonly IGSendApiWrapper _gSendApiWrapper;
        private Uri _uri = null;

        public ServerSelectMenuItem(IPluginMenu parentMenu, int index, IGSendApiWrapper gSendApiWrapper)
        {
            _gSendApiWrapper = gSendApiWrapper ?? throw new ArgumentNullException(nameof(gSendApiWrapper));
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
            _gSendApiWrapper.ServerAddress = _uri ?? throw new InvalidOperationException("Invalid Server Address");
        }

        public void ClientMessageReceived(IClientBaseMessage clientMessage)
        {
            // from interface, not used in this context
        }

        public bool GetShortcut(in List<int> defaultKeys, out string groupName, out string shortcutName)
        {
            groupName = String.Empty;
            shortcutName = String.Empty;
            return false;
        }

        public bool IsChecked()
        {
            if (_uri == null)
                return false;

            return _uri.Equals(_gSendApiWrapper.ServerAddress);
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
