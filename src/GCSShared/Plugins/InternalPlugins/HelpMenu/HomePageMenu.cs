using System.Diagnostics;
using System.Drawing;

using GSendShared.Models;

namespace GSendShared.Plugins.InternalPlugins.HelpMenu
{
    internal sealed class HomePageMenu : IPluginMenu
    {
        private readonly IPluginMenu _parentMenu;

        public HomePageMenu(IPluginMenu parentMenu)
        {
            _parentMenu = parentMenu ?? throw new ArgumentNullException(nameof(parentMenu));
        }

        public string Text => "Home Page";

        public int Index => 4;

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public IPluginMenu ParentMenu => _parentMenu;

        public bool ReceiveClientMessages => false;

        public void Clicked()
        {
            ProcessStartInfo psi = new()
            {
                FileName = "https://www.gsend.pro/",
                UseShellExecute = true
            };

            Process.Start(psi);
        }

        public bool GetShortcut(out string groupName, out string shortcutName)
        {
            groupName = null;
            shortcutName = null;
            return false;
        }

        public bool IsChecked() => false;

        public bool IsEnabled() => true;

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
