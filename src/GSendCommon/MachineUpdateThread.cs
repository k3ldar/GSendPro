using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

using GSendShared;
using GSendShared.Models;

using Shared.Classes;

namespace GSendCommon
{
    public sealed class MachineUpdateThread : ThreadManager
    {
        private const int OverrideUpdateTimeout = 250;
        private readonly GSendWebSocket _gSendWebSocket;
        private readonly IMachine _machine;
        private readonly IUiUpdate _uiUpdate;
        private DateTime _lastUiUpdate;
        private DateTime _lastOverrideUpdate;

        public MachineUpdateThread(TimeSpan runInterval, GSendWebSocket gSendWebSocket, IMachine machine, IUiUpdate uiUpdate)
            : base(null, runInterval)
        {
            ContinueIfGlobalException = true;
            ThreadSendCommandQueue = new();
            _gSendWebSocket = gSendWebSocket ?? throw new ArgumentNullException(nameof(gSendWebSocket));
            _machine = machine ?? throw new ArgumentNullException(nameof(machine));
            _uiUpdate = uiUpdate ?? throw new ArgumentNullException(nameof(uiUpdate));
            _lastUiUpdate = DateTime.UtcNow;
            _lastOverrideUpdate = DateTime.MaxValue;
            IsThreadRunning = false;
            Overrides = new();
            Overrides.ValueUpdated += Overrides_ValueUpdated;
        }

        private void Overrides_ValueUpdated(object sender, EventArgs e)
        {
            OverridesUpdated();
        }

        public bool IsThreadRunning { get; set; }

        public OverrideModel Overrides { get; }

        public void OverridesUpdated()
        {
            _lastOverrideUpdate = DateTime.UtcNow;
        }

        public ConcurrentQueue<string> ThreadSendCommandQueue { get; }

        protected override bool Run(object parameters)
        {
            while (ThreadSendCommandQueue.TryDequeue(out string sendCommand))
            {
                _gSendWebSocket.SendAsync(sendCommand).ConfigureAwait(false);
            }

            if (IsThreadRunning)
            {
                _gSendWebSocket.SendAsync(String.Format(Constants.MessageMachineStatus, _machine.Id)).ConfigureAwait(false);
            }

            TimeSpan overrideUpdateSpan = DateTime.UtcNow - _lastOverrideUpdate;

            if (overrideUpdateSpan.TotalMilliseconds > OverrideUpdateTimeout)
            {
                Trace.WriteLine("override send update");
                string overrideAsBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(Overrides, Constants.DefaultJsonSerializerOptions)));
                _gSendWebSocket.SendAsync(String.Format(Constants.MessageMachineUpdateOverrides, _machine.Id, overrideAsBase64)).ConfigureAwait(false);
                _lastOverrideUpdate = DateTime.MaxValue;
            }

            TimeSpan span = DateTime.UtcNow - _lastUiUpdate;

            if (span.TotalSeconds >= 60)
            {
                ThreadManager.ThreadStart(new RefreshServiceScheduleThread(_uiUpdate, this), $"{this.Name} - Service Schedule", ThreadPriority.BelowNormal);
                _lastUiUpdate = DateTime.UtcNow;
            }

            return !HasCancelled();
        }
    }
}
