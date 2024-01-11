using System.Text.Json;

using GSendControls.Abstractions;
using GSendControls.Controls;

using GSendShared;
using GSendShared.Models;
using GSendShared.Plugins;

namespace GSendControls.Plugins.InternalPlugins.Hearbeats
{
    internal class HeartbeatControlItem : IPluginControl
    {
        private readonly HeartbeatControls _heartbeates;

        public HeartbeatControlItem()
        {
            _heartbeates = new();
        }

        public string Name => GSend.Language.Resources.Graphs;

        public PluginControl Control => _heartbeates;

        public ControlLocation Location => ControlLocation.Secondary;

        public string Text => "Heartbeat Graphs";

        public int Index => 20;

        public bool ReceiveClientMessages => true;

        public void ClientMessageReceived(IClientBaseMessage clientMessage)
        {
            switch (clientMessage.request)
            {
                case Constants.MessageMachineStatusServer:
                case Constants.StateChanged:
                    JsonElement element = (JsonElement)clientMessage.message;
                    MachineStateModel machineStatusModel = element.Deserialize<MachineStateModel>();

                    if (machineStatusModel != null)
                    {
                        _heartbeates.UpdateMachineStatus(machineStatusModel);
                    }

                    break;
            }
        }

        public bool IsEnabled()
        {
            return true;
        }

        public void UpdateHost<T>(T senderPluginHost)
        {
            //not used in this context
        }
    }
}
