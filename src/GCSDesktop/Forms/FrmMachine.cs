using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

using GSendCommon;

using GSendShared;

using Shared.Classes;

using static GSendShared.Constants;

namespace GSendDesktop.Forms
{
    public partial class FrmMachine : Form
    {
        private readonly CancellationTokenRegistration _cancellationTokenRegistration;
        private readonly GSendWebSocket _clientWebSocket;
        private readonly IGSendContext _gSendContext;
        private readonly IMachine _machine;


        public FrmMachine()
        {
            InitializeComponent();

            LoadResources();
        }

        public FrmMachine(IGSendContext gSendContext, IMachine machine)
            : this()
        {
            _gSendContext = gSendContext ?? throw new ArgumentNullException(nameof(gSendContext));
            _machine = machine ?? throw new ArgumentNullException(nameof(machine));

            Text = String.Format(GSend.Language.Resources.MachineTitle, machine.MachineType, machine.Name);

            if (machine.MachineType == MachineType.Printer)
                tabControlMain.TabPages.Remove(tabPageOverrides);

            ConfigureMachine();

            trackBarPercent.Value = machine.OverrideSpeed;
            selectionOverrideSpindle.Value = machine.OverrideSpindle;
            cbOverridesDisable.Checked = true;

            _cancellationTokenRegistration = new();
            _clientWebSocket = new GSendWebSocket(_cancellationTokenRegistration.Token);
            _clientWebSocket.ProcessMessage += ClientWebSocket_ProcessMessage;
            _clientWebSocket.ConnectionLost += ClientWebSocket_ConnectionLost;
            _clientWebSocket.Connected += ClientWebSocket_Connected;
            UpdateDisplay();
        }

        #region Client Web Socket

        private void ClientWebSocket_Connected(object sender, EventArgs e)
        {
            toolStripStatusLabelServerConnect.Text = GSend.Language.Resources.ServerConnected;
            UpdateEnabledState();
        }

        private void ClientWebSocket_ConnectionLost(object sender, EventArgs e)
        {
            toolStripStatusLabelServerConnect.Text = GSend.Language.Resources.ServerNotConnected;
            UpdateEnabledState();
        }

        private void ClientWebSocket_ProcessMessage(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            ClientBaseMessage clientMessage = JsonSerializer.Deserialize<ClientBaseMessage>(message);

            if (!clientMessage.success)
                return;

            string serverCpu = String.Format(GSend.Language.Resources.ServerCpuStateConnected, clientMessage.ServerCpuStatus.ToString("N2"));

            if (clientMessage.request.Equals(MessageMachineStatus))
            {
                List<StatusResponseMessage> statuses = JsonSerializer.Deserialize<List<StatusResponseMessage>>(clientMessage.message.ToString());

                UpdateMachineStatus(statuses);
            }

            UpdateEnabledState();
        }

        private void UpdateEnabledState()
        { }

        private void UpdateMachineStatus(List<StatusResponseMessage> statuses)
        {
            if (InvokeRequired)
            {
                Invoke(UpdateMachineStatus, new object[] { statuses });
                return;
            }

            //if (statuses.Count != _machines.Count)
            //    UpdateMachines();

            //foreach (StatusResponseMessage status in statuses)
            //{
            //    if (!_machines.ContainsKey(status.Id))
            //        continue;

            //    using (TimedLock tl = TimedLock.Lock(_lockObject))
            //    {
            //        ListViewItem machineItem = _machines[status.Id];

            //        if (machineItem == null)
            //            continue;

            //        string connectedText = status.Connected ? GSend.Language.Resources.Yes : GSend.Language.Resources.No;

            //        if (status.Connected)
            //        {
            //            if (!machineItem.SubItems[3].Text.Equals(connectedText))
            //                machineItem.SubItems[3].Text = connectedText;

            //            if (!machineItem.SubItems[4].Text.Equals(status.State))
            //            {
            //                Color backColor = Color.White;
            //                Color foreColor = Color.Black;
            //                string text = TranslateState(status.State);

            //                switch (status.State)
            //                {
            //                    case StateUndefined:
            //                        text = GSend.Language.Resources.StatePortOpen;
            //                        backColor = Color.Yellow;
            //                        foreColor = Color.Black;
            //                        break;

            //                    case StateIdle:
            //                        backColor = Color.Blue;
            //                        foreColor = Color.White;
            //                        break;

            //                    case StateRun:
            //                        backColor = Color.Green;
            //                        foreColor = Color.White;
            //                        break;

            //                    case StateJog:
            //                        backColor = Color.Yellow;
            //                        foreColor = Color.Black;
            //                        break;

            //                    case StateAlarm:
            //                    case StateDoor:
            //                    case StateCheck:
            //                        backColor = Color.Red;
            //                        foreColor = Color.White;
            //                        break;

            //                    case StateHome:
            //                        backColor = Color.Aqua;
            //                        foreColor = Color.Black;
            //                        break;

            //                    case StateSleep:
            //                        backColor = Color.DarkGray;
            //                        foreColor = Color.White;
            //                        break;
            //                }

            //                if (!machineItem.SubItems[4].Text.Equals(text))
            //                    machineItem.SubItems[4].Text = text;

            //                if (machineItem.SubItems[4].BackColor != backColor)
            //                    machineItem.SubItems[4].BackColor = backColor;

            //                if (machineItem.SubItems[4].ForeColor != foreColor)
            //                    machineItem.SubItems[4].ForeColor = foreColor;

            //                if (machineItem.SubItems[5].Text != status.CpuStatus)
            //                    machineItem.SubItems[5].Text = status.CpuStatus;
            //            }
            //        }
            //        else
            //        {
            //            if (!machineItem.SubItems[3].Text.Equals(connectedText))
            //                machineItem.SubItems[3].Text = connectedText;

            //            if (!machineItem.SubItems[4].Text.Equals(String.Empty))
            //            {
            //                machineItem.SubItems[4].BackColor = Color.White;
            //                machineItem.SubItems[4].ForeColor = Color.Black;
            //                machineItem.SubItems[4].Text = String.Empty;
            //            }
            //        }
            //    }
            //}
        }

        #endregion Client Web Socket

        private void UpdateDisplay()
        {
            if (InvokeRequired) 
            {
                Invoke(UpdateDisplay);
                return;
            }

            string labelFormat = GSend.Language.Resources.DisplayMmMinute;

            switch (_machine.DisplayUnits)
            {
                case DisplayUnits.MmPerMinute:
                    labelFormat = GSend.Language.Resources.DisplayMmMinute;
                    break;
                case DisplayUnits.MmPerSecond:
                    labelFormat = GSend.Language.Resources.DisplayMmSec;
                    break;
                case DisplayUnits.InchPerMinute:
                    labelFormat = GSend.Language.Resources.DisplayInchMinute;
                    break;
                case DisplayUnits.InchPerSecond:
                    labelFormat = GSend.Language.Resources.DisplayInchSecond;
                    break;
            }

            toolStripStatusLabelDisplayMeasurements.Text = labelFormat;
            selectionOverrideSpindle.TickFrequency = (int)_machine.Settings[30] / 100;
        }

        private void FrmMachine_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !_gSendContext.IsClosing;
            Hide();
        }

        private void ConfigureMachine()
        {
            selectionOverrideSpindle.Maximum = (int)_machine.Settings[30];
            selectionOverrideSpindle.Minimum = (int)_machine.Settings[31];
            selectionOverrideX.Maximum = (int)_machine.Settings[110];
            selectionOverrideX.Minimum = 0;
            selectionOverrideY.Maximum = (int)_machine.Settings[111];
            selectionOverrideY.Minimum = 0;
            selectionOverrideZDown.Maximum = (int)_machine.Settings[112];
            selectionOverrideZDown.Minimum = 0;
            selectionOverrideZUp.Maximum = (int)_machine.Settings[112];
            selectionOverrideZUp.Minimum = 0;
            selectionOverrideSpindle.Minimum = (int)_machine.Settings[31];
            selectionOverrideSpindle.Maximum = (int)_machine.Settings[30];

            jogControl1.FeedMaximum = (int)_machine.Settings[110];
            jogControl1.FeedMinimum = 0;
            jogControl1.StepsMaximum = 6;
            jogControl1.StepsMinimum = 0;


        }

        private void LoadResources()
        {
            //toolbar
            toolStripButtonConnect.Text = GSend.Language.Resources.Connect;
            toolStripButtonConnect.ToolTipText = GSend.Language.Resources.Connect;
            toolStripButtonDisconnect.Text = GSend.Language.Resources.Disconnect;
            toolStripButtonDisconnect.ToolTipText = GSend.Language.Resources.Disconnect;
            toolStripButtonClearAlarm.Text = GSend.Language.Resources.ClearAlarm;
            toolStripButtonClearAlarm.ToolTipText = GSend.Language.Resources.ClearAlarm;
            toolStripButtonHome.Text = GSend.Language.Resources.Home;
            toolStripButtonHome.ToolTipText = GSend.Language.Resources.Home;
            toolStripButtonProbe.Text = GSend.Language.Resources.Probe;
            toolStripButtonProbe.ToolTipText = GSend.Language.Resources.Probe;
            toolStripButtonResume.Text = GSend.Language.Resources.Resume;
            toolStripButtonResume.ToolTipText = GSend.Language.Resources.Resume;
            toolStripButtonPause.Text = GSend.Language.Resources.Pause;
            toolStripButtonPause.ToolTipText = GSend.Language.Resources.Pause;
            toolStripButtonStop.Text = GSend.Language.Resources.Stop;
            toolStripButtonStop.ToolTipText = GSend.Language.Resources.Stop;

            //tab pages
            tabPageMain.Text = GSend.Language.Resources.General;
            tabPageOverrides.Text = GSend.Language.Resources.Overrides;
            tabPageServiceSchedule.Text = GSend.Language.Resources.ServiceSchedule;
            tabPageMachineSettings.Text = GSend.Language.Resources.GrblSettings;
            tabPageSpindle.Text = GSend.Language.Resources.Spindle;
            tabPageUsage.Text = GSend.Language.Resources.Usage;
            tabPageSettings.Text = GSend.Language.Resources.Settings;
            selectionOverrideSpindle.LabelFormat = GSend.Language.Resources.OverrideRpm;

            //General tab


            // Override tab
            cbOverridesDisable.Text = GSend.Language.Resources.DisableOverrides;

        }

        private void trackBarPercent_ValueChanged(object sender, EventArgs e)
        {
            UpdateOverrides();
        }

        private void UpdateOverrides()
        { 

            selectionOverrideX.Value = selectionOverrideX.Maximum / 100 * trackBarPercent.Value;
            selectionOverrideY.Value = selectionOverrideY.Maximum / 100 * trackBarPercent.Value;
            selectionOverrideZDown.Value = selectionOverrideZDown.Maximum / 100 * trackBarPercent.Value;
            selectionOverrideZUp.Value = selectionOverrideZUp.Maximum / 100 * trackBarPercent.Value;
            labelSpeedPercent.Text = String.Format(GSend.Language.Resources.SpeedPercent, trackBarPercent.Value);
        }

        private void cbOverridesDisable_CheckedChanged(object sender, EventArgs e)
        {
            trackBarPercent.Enabled = !cbOverridesDisable.Checked;
            selectionOverrideSpindle.Enabled = !cbOverridesDisable.Checked;
            selectionOverrideX.Enabled = !cbOverridesDisable.Checked;
            selectionOverrideY.Enabled = !cbOverridesDisable.Checked;
            selectionOverrideZDown.Enabled = !cbOverridesDisable.Checked;
            selectionOverrideZUp.Enabled = !cbOverridesDisable.Checked;
        }
    }
}
