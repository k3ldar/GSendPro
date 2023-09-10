using System;

using GSendShared.Models;
using GSendShared.Plugins;

namespace GSendTests.Mocks
{
    internal sealed class MockPluginMenu : IPluginMenu
    {
        public MockPluginMenu(string name, int index, MenuType menuType, MenuParent menuParent)
        {
            Name = name;
            Index = index;
            MenuType = menuType;
            ParentMenu = menuParent;
        }

        public MockPluginMenu(string name, MenuType menuType, MenuParent menuParent)
            : this(name, -1, menuType, menuParent)
        {

        }

        public string Name { get; set; }

        public int Index { get; set; }

        public MenuType MenuType { get; set; }

        public MenuParent ParentMenu { get; set; }

        public void Clicked()
        {
            throw new NotImplementedException();
        }

        public void MachineStatusChanged(MachineStateModel machineStateModel)
        {
            throw new NotImplementedException();
        }
    }
}
