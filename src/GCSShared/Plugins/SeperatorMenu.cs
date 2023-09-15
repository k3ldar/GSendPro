using System.Drawing;

using GSendShared.Models;

namespace GSendShared.Plugins
{
    public sealed class SeperatorMenu : IPluginMenu
    {
        public SeperatorMenu(MenuParent menuParent, int index)
        {
            ParentMenu = menuParent;
            Index = index;
        }

        public string Text => "Seperator Menu";

        public int Index { get; private set; }

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.Seperator;

        public MenuParent ParentMenu { get; private set; }

        public void Clicked()
        {
            throw new NotImplementedException();
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
