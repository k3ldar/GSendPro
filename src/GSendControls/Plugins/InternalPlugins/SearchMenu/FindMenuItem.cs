using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using GSendControls.Abstractions;

using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins.InternalPlugins.SearchMenu
{
    internal class FindMenuItem : IPluginMenu
    {
        private readonly ITextEditor _textEditor;

        public FindMenuItem(IPluginMenu parentMenu, ITextEditor textBox)
        {
            ParentMenu = parentMenu ?? throw new ArgumentNullException(nameof(parentMenu));
            _textEditor = textBox ?? throw new ArgumentNullException(nameof(textBox));
        }

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public IPluginMenu ParentMenu { get; private set; }

        public string Text => GSend.Language.Resources.FindMenu;

        public int Index => 0;

        public bool ReceiveClientMessages => false;

        public void Clicked()
        {
            _textEditor.ShowFindDialog();
        }

        public void ClientMessageReceived(IClientBaseMessage clientMessage)
        {
            throw new NotImplementedException();
        }

        public bool GetShortcut(in List<int> defaultKeys, out string groupName, out string shortcutName)
        {
            groupName = "Search Menu";
            shortcutName = "Find";
            defaultKeys.Add((int)Keys.Control);
            defaultKeys.Add((int)Keys.F);
            return true;
        }

        public bool IsChecked()
        {
            return false;
        }

        public bool IsEnabled()
        {
            return _textEditor.Text.Length > 0;
        }

        public bool IsVisible()
        {
            return true;
        }

        public void UpdateHost<T>(T senderPluginHost)
        {
            // from interface, not used in this context
        }
    }
}
