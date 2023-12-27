using System;
using System.Drawing;

using FastColoredTextBoxNS;

using GSendControls.Abstractions;

using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins.InternalPlugins.SearchMenu
{
    public sealed class GotoMenuItem : IPluginMenu
    {
        private readonly ITextEditor _textEditor;

        public GotoMenuItem(IPluginMenu parentMenu, ITextEditor textBox)
        {
            ParentMenu = parentMenu ?? throw new ArgumentNullException(nameof(parentMenu));
            _textEditor = textBox ?? throw new ArgumentNullException(nameof(textBox));
        }

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public IPluginMenu ParentMenu { get; private set; }

        public string Text => GSend.Language.Resources.GotoLineNumberMenu;

        public int Index => 0;

        public bool ReceiveClientMessages => false;

        public void Clicked()
        {
            _textEditor?.ShowGoToDialog();
        }

        public void ClientMessageReceived(IClientBaseMessage clientMessage)
        {
            throw new NotImplementedException();
        }

        public bool GetShortcut(out string groupName, out string shortcutName)
        {
            groupName = null;
            shortcutName = null;
            return false;
        }

        public bool IsChecked()
        {
            return false;
        }

        public bool IsEnabled()
        {
            return _textEditor?.LineCount > 0;
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
