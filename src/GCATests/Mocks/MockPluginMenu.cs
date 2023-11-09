using System;
using System.Drawing;

using GSendShared;
using GSendShared.Models;
using GSendShared.Plugins;

namespace GSendTests.Mocks
{
    internal sealed class MockPluginMenu : IPluginMenu
    {
        private ISenderPluginHost _senderPluginHost;

        public MockPluginMenu(string name, int index, MenuType menuType, IPluginMenu menuParent)
        {
            Text = name;
            Index = index;
            MenuType = menuType;
            ParentMenu = menuParent;
        }

        public MockPluginMenu(string name, MenuType menuType, IPluginMenu menuParent)
            : this(name, -1, menuType, menuParent)
        {

        }

        public string Text { get; set; }

        public int Index { get; set; }

        public Image MenuImage => null;

        public MenuType MenuType { get; set; }

        public IPluginMenu ParentMenu { get; set; }

        public bool ReceiveClientMessages => false;

        public bool IsEnabled()
        {
            return true;
        }

        public bool IsChecked()
        {
            return false;
        }

        public bool IsVisible() => true;

        public void Clicked()
        {
            throw new NotImplementedException();
        }

        public void ClientMessageReceived(IClientBaseMessage clientMessage)
        {
            throw new NotImplementedException();
        }

        public void UpdateHost<T>(T senderPluginHost)
        {
            _senderPluginHost = senderPluginHost as ISenderPluginHost;
        }

        public bool GetShortcut(out string groupName, out string shortcutName)
        {
            throw new NotImplementedException();
        }

        public ISenderPluginHost PluginHost => _senderPluginHost;
    }
}
