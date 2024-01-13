using System.Globalization;

using GSendControls.Controls;

using GSendShared.Models;

namespace GSendControls.Plugins.InternalPlugins.Hearbeats
{
    public partial class HeartbeatControls : PluginControl
    {
        public HeartbeatControls()
        {
            InitializeComponent();
        }

        public override void LanguageChanged(CultureInfo culture)
        {
            base.LanguageChanged(culture);
            heartbeatPanelBufferSize.GraphName = GSend.Language.Resources.GraphBufferSize;
            heartbeatPanelCommandQueue.GraphName = GSend.Language.Resources.GraphCommandQueue;
            heartbeatPanelFeed.GraphName = GSend.Language.Resources.GraphFeedRate;
            heartbeatPanelQueueSize.GraphName = GSend.Language.Resources.GraphQueueSize;
            heartbeatPanelSpindle.GraphName = GSend.Language.Resources.GraphSpindleSpeed;
            heartbeatPanelAvailableBlocks.GraphName = GSend.Language.Resources.GraphAvailableBlocks;
            heartbeatPanelAvailableRXBytes.GraphName = GSend.Language.Resources.GraphAvailableRXBytes;
        }

        public void UpdateMachineStatus(MachineStateModel status)
        {
            if (InvokeRequired)
            {
                Invoke(UpdateMachineStatus, status);
                return;
            }

            if (status.IsConnected)
            {
                heartbeatPanelBufferSize.AddPoint(status.BufferSize);
                heartbeatPanelCommandQueue.AddPoint(status.CommandQueueSize);
                heartbeatPanelFeed.AddPoint((int)status.FeedRate);
                heartbeatPanelQueueSize.AddPoint(status.QueueSize);
                heartbeatPanelSpindle.AddPoint((int)status.SpindleSpeed);

                if (heartbeatPanelAvailableRXBytes.MaximumPoints == 0 && status.AvailableRXbytes > 0)
                {
                    heartbeatPanelAvailableRXBytes.MaximumPoints = status.AvailableRXbytes;
                }

                if (heartbeatPanelAvailableBlocks.MaximumPoints == 0 && status.BufferAvailableBlocks > 0)
                {
                    heartbeatPanelAvailableBlocks.MaximumPoints = status.BufferAvailableBlocks;
                }

                heartbeatPanelAvailableRXBytes.AddPoint(status.AvailableRXbytes);
                heartbeatPanelAvailableBlocks.AddPoint(status.BufferAvailableBlocks);
            }
        }
    }
}
