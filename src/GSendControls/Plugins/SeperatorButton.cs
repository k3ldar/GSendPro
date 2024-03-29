﻿using System;
using System.Drawing;

using GSendControls.Abstractions;

using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins
{
    public sealed class SeperatorButton : IPluginToolbarButton
    {
        public SeperatorButton(int index)
        {
            Index = index;
        }

        public ButtonType ButtonType => ButtonType.Seperator;

        public Image Picture => null;

        public string Text => null;

        public int Index { get; private set; }

        public bool ReceiveClientMessages => false;

        public void Clicked()
        {
            throw new InvalidOperationException();
        }

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
