using System.Diagnostics;
using System.Drawing;

using GSendShared.Models;

namespace GSendShared.Plugins.InternalPlugins.HelpMenu
{
    internal sealed class HelpMenuItem : IPluginMenu
    {
        public HelpMenuItem(IPluginMenu parentMenu)
        {
            ParentMenu = parentMenu ?? throw new ArgumentNullException(nameof(parentMenu));
        }

        public string Text => "Help";

        public int Index => 0;

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public IPluginMenu ParentMenu { get; }

        public bool ReceiveClientMessages => false;

        public void Clicked()
        {
            ProcessStartInfo psi = new()
            {
                FileName = Constants.HelpWebsite,
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
