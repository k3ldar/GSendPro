﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

using GSendControls.Abstractions;

using GSendShared;
using GSendShared.Plugins;

using Shared.Classes;

namespace GSendControls.Plugins.InternalPlugins.HelpMenu
{
    internal sealed class HelpMenuItem : IPluginMenu
    {
        private readonly IRunProgram _runProgram;

        public HelpMenuItem(IPluginMenu parentMenu, IRunProgram runProgram)
        {
            ParentMenu = parentMenu ?? throw new ArgumentNullException(nameof(parentMenu));
            _runProgram = runProgram ?? throw new ArgumentNullException(nameof(runProgram));
        }

        public string Text => "Help";

        public int Index => 0;

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public IPluginMenu ParentMenu { get; }

        public bool ReceiveClientMessages => false;

        public void Clicked()
        {
            _runProgram.Run(Constants.HelpWebsite, null, true, false, 500);
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
