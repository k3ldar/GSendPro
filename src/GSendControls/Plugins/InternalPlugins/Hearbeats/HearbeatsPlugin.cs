using System.Collections.Generic;

using GSendControls.Abstractions;

using GSendShared;
using GSendShared.Plugins;

namespace GSendControls.Plugins.InternalPlugins.Hearbeats
{
    internal class HearbeatsPlugin : IGSendPluginModule
    {
        private List<IPluginControl> _pluginControls;

        public string Name => "Heartbeats";

        public ushort Version => 1;

        public PluginHosts Host => PluginHosts.Sender;

        public PluginOptions Options => PluginOptions.HasControls;

        public IReadOnlyList<IPluginMenu> MenuItems => null;

        public IReadOnlyList<IPluginToolbarButton> ToolbarItems => null;

        public IReadOnlyList<IPluginControl> ControlItems
        {
            get
            {
                _pluginControls ??= [
                        new HeartbeatControlItem(),
                ];

                return _pluginControls;
            }
        }

        public bool ReceiveClientMessages => false;

        public void ClientMessageReceived(IClientBaseMessage clientMessage)
        {
            // from interface, not used in this context
        }

        public void Initialize(IPluginHost pluginHost)
        {
            // from interface, not used in this context
        }
    }
}
