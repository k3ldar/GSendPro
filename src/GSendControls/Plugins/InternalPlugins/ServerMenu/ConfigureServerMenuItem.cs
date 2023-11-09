using System;
using System.Drawing;

using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins.InternalPlugins.ServerMenu
{
    public sealed class ConfigureServerMenuItem : IPluginMenu
    {
        public ConfigureServerMenuItem(IPluginMenu parentMenu)
        {
            ParentMenu = parentMenu ?? throw new ArgumentNullException(nameof(parentMenu));
        }

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public IPluginMenu ParentMenu { get; }

        public string Text => "Configure";

        public int Index => 0;

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
            return true;
        }

        public void UpdateHost<T>(T senderPluginHost)
        {

        }
    }
}
