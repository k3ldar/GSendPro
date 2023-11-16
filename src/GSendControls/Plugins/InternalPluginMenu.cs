using System;
using System.Drawing;
using System.Windows.Forms;
using GSendControls.Abstractions;
using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins
{
    public class InternalPluginMenu : IPluginMenu
    {
        private readonly ToolStripMenuItem _menuItem;

        public InternalPluginMenu(ToolStripMenuItem menuItem)
        {
            _menuItem = menuItem ?? throw new ArgumentNullException(nameof(menuItem));
        }

        public ToolStripMenuItem MenuItem => _menuItem;

        public Image MenuImage => _menuItem.Image;

        public MenuType MenuType => MenuType.MenuItem;

        public IPluginMenu ParentMenu => null;

        public string Text => _menuItem.Text;

        public int Index => _menuItem.MergeIndex;

        public bool ReceiveClientMessages => false;

        public void Clicked()
        {
            // only required by interface
        }

        public void ClientMessageReceived(IClientBaseMessage clientMessage)
        {
            // only required by interface
        }

        public bool GetShortcut(out string groupName, out string shortcutName)
        {
            groupName = String.Empty;
            shortcutName = String.Empty;
            return false;
        }

        public bool IsChecked()
        {
            return _menuItem.Checked;
        }

        public bool IsEnabled()
        {
            return _menuItem.Enabled;
        }

        public bool IsVisible()
        {
            return _menuItem.Visible;
        }

        public void UpdateHost<T>(T senderPluginHost)
        {
            // only required by interface
        }
    }
}
