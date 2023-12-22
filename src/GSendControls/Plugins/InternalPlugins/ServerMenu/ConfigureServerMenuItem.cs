using System;
using System.Drawing;
using GSendControls.Abstractions;
using GSendControls.Forms;

using GSendShared;
using GSendShared.Plugins;

using Microsoft.Extensions.DependencyInjection;

namespace GSendControls.Plugins.InternalPlugins.ServerMenu
{
    public sealed class ConfigureServerMenuItem : IPluginMenu
    {
        private IPluginHost _pluginHost;

        public ConfigureServerMenuItem(IPluginMenu parentMenu)
        {
            ParentMenu = parentMenu ?? throw new ArgumentNullException(nameof(parentMenu));
        }

        public Image MenuImage => null;

        public MenuType MenuType => MenuType.MenuItem;

        public IPluginMenu ParentMenu { get; }

        public string Text => "Configure";

        public int Index => 0;

        public bool ReceiveClientMessages => false;

        public void Clicked()
        {
            using FrmConfigureServer frmConfigureServer = _pluginHost.GSendContext.ServiceProvider.GetRequiredService<FrmConfigureServer>();
            frmConfigureServer.ShowDialog();
        }

        public void ClientMessageReceived(IClientBaseMessage clientMessage)
        {

        }

        public bool GetShortcut(out string groupName, out string shortcutName)
        {
            groupName = String.Empty;
            shortcutName = String.Empty;
            return false;
        }

        public bool IsChecked()
        {
            return false;
        }

        public bool IsEnabled()
        {
            return true;
        }

        public bool IsVisible()
        {
            return true;
        }

        public void UpdateHost<T>(T senderPluginHost)
        {
            _pluginHost = senderPluginHost as IPluginHost;
        }
    }
}
