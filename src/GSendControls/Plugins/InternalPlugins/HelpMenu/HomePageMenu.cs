﻿using System;
using System.Collections.Generic;
using System.Drawing;

using GSendControls.Abstractions;

using GSendShared;
using GSendShared.Plugins;

using Shared.Classes;

namespace GSendControls.Plugins.InternalPlugins.HelpMenu
{
    internal sealed class HomePageMenu : IPluginMenu
    {
        private readonly IPluginMenu _parentMenu;
        private readonly IRunProgram _runProgram;

        public HomePageMenu(IPluginMenu parentMenu, IRunProgram runProgram)
        {
            _parentMenu = parentMenu ?? throw new ArgumentNullException(nameof(parentMenu));
            _runProgram = runProgram ?? throw new ArgumentNullException(nameof(runProgram));
        }

        public string Text => "Home Page";

        public int Index => 4;

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public IPluginMenu ParentMenu => _parentMenu;

        public bool ReceiveClientMessages => false;

        public void Clicked()
        {
            _runProgram.Run("https://www.gsend.pro/", null, true, false, 500);
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
