using System;
using System.Collections.Generic;
using System.Drawing;

using GSendControls.Abstractions;

using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins
{
    public sealed class SeperatorMenu : IPluginMenu
    {
        public SeperatorMenu(IPluginMenu menuParent, int index)
        {
            ParentMenu = menuParent;
            Index = index;
        }

        public string Text => "Seperator Menu";

        public int Index { get; private set; }

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.Seperator;

        public IPluginMenu ParentMenu { get; }

        public bool ReceiveClientMessages => false;

        public void Clicked()
        {
            throw new NotImplementedException();
        }

        public bool GetShortcut(in List<int> defaultKeys, out string groupName, out string shortcutName)
        {
            groupName = null;
            shortcutName = null;
            return false;
        }

        public bool IsChecked() => false;

        public bool IsEnabled() => true;

        public bool IsVisible() => true;

        public void ClientMessageReceived(IClientBaseMessage clientMessage)
        {
            // from interface, not used in any context
        }

        public void UpdateHost<T>(T senderPluginHost)
        {
            // from interface, not used in any context
        }
    }
}
