using System.Diagnostics;
using System.Drawing;

using GSendShared.Models;

namespace GSendShared.Plugins.InternalPlugins.HelpMenu
{
    internal sealed class HelpMenuItem : IPluginMenu
    {
        public string Text => "Help";

        public int Index => 0;

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public MenuParent ParentMenu => MenuParent.Help;

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

        public void MachineStatusChanged(MachineStateModel machineStateModel)
        {
            // from interface, not used in any context
        }

        public void UpdateHost<T>(T senderPluginHost)
        {
            // from interface, not used in any context
        }
    }
}
