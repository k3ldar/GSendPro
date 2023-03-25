using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

using GSendCommon;

using GSendDesktop.Controls;

using GSendShared;
using GSendShared.Attributes;
using GSendShared.Models;

using Shared.Classes;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static GSendShared.Constants;

namespace GSendDesktop.Forms
{
    public partial class FrmMachine : Form
    {
        private readonly CancellationTokenRegistration _cancellationTokenRegistration;
        private readonly GSendWebSocket _clientWebSocket;
        private readonly IGSendContext _gSendContext;
        private readonly IMachine _machine;
        private MachineStateModel _machineStatusModel = null;
        private readonly object _lockObject = new();
        private volatile bool _threadRun = false;
        private bool _machineConnected = false;
        private bool _isPaused = false;
        private bool _canCancelJog = false;
        private bool _appliedSettingsChanged = false;

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

            _cancellationTokenRegistration = new();
            _clientWebSocket = new GSendWebSocket(_cancellationTokenRegistration.Token, _machine.Name);
            _clientWebSocket.ProcessMessage += ClientWebSocket_ProcessMessage;
            _clientWebSocket.ConnectionLost += ClientWebSocket_ConnectionLost;
            _clientWebSocket.Connected += ClientWebSocket_Connected;

            Thread updateThread = new Thread(new ThreadStart(UpdateThread))
            {
                IsBackground = true,
                Priority = ThreadPriority.Normal,
                Name = $"{machine.Name} Update Status"
            };
            updateThread.Start();

            propertyGridGrblSettings.SelectedObject = machine.Settings;

            jogControl.FeedMinimum = 0;
            jogControl.FeedMaximum = (int)machine.Settings.MaxFeedRateX;
            jogControl.StepValue = 7;

            trackBarPercent.Value = machine.OverrideSpeed;
            selectionOverrideSpindle.Value = machine.OverrideSpindle;
            cbOverridesDisable.Checked = true;

            lblPropertyHeader.Text = String.Empty;
            lblPropertyDesc.Text = String.Empty;

            UpdateDisplay();
            UpdateEnabledState();
        }

        #region Client Web Socket

        private void ClientWebSocket_Connected(object sender, EventArgs e)
        {
            _threadRun = true;
            _clientWebSocket.SendAsync(String.Format("mAddEvents:{0}", _machine.Id)).ConfigureAwait(false);
            toolStripStatusLabelServerConnect.Text = GSend.Language.Resources.ServerConnected;
            UpdateEnabledState();
        }

        private void ClientWebSocket_ConnectionLost(object sender, EventArgs e)
        {
            flowLayoutWarningErrors.Visible = false;
            _threadRun = false;
            toolStripStatusLabelServerConnect.Text = GSend.Language.Resources.ServerNotConnected;
            UpdateEnabledState();
        }

        private void ClientWebSocket_ProcessMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(ClientWebSocket_ProcessMessage, message);
                return;
            }

            if (String.IsNullOrWhiteSpace(message))
                return;

            ClientBaseMessage clientMessage = null;
            try
            {
                clientMessage = JsonSerializer.Deserialize<ClientBaseMessage>(message);
            }
            catch (JsonException)
            {
                return;
            }

            //Trace.WriteLine(String.Format("Machine {0} Socket Message: {1}", _machine.Name, clientMessage.success));

            if (clientMessage == null || !clientMessage.success)
                return;

            string serverCpu = String.Format(GSend.Language.Resources.ServerCpuStateConnected, clientMessage.ServerCpuStatus.ToString("N2"));

            switch (clientMessage.request)
            {
                case "mStatus":
                    _machineStatusModel = JsonSerializer.Deserialize<MachineStateModel>(clientMessage.message.ToString());

                    if (_machineStatusModel.IsConnected != clientMessage.IsConnected)
                        _machineStatusModel.IsConnected = clientMessage.IsConnected;

                    UpdateMachineStatus(_machineStatusModel);
                    UpdateEnabledState();
                    break;

                case "Connect":
                    _machineConnected = true;
                    UpdateEnabledState();
                    break;

                case "Disconnect":
                    _machineConnected = false;
                    txtGrblUpdates.Text = String.Empty;
                    _appliedSettingsChanged = false;
                    flowLayoutWarningErrors.Controls.Clear();
                    UpdateWarningStatusBarItem();
                    UpdateEnabledState();
                    break;

                case "Pause":
                    _isPaused = true;
                    UpdateEnabledState();
                    break;

                case "Resume":
                    _isPaused = false;
                    UpdateEnabledState();
                    break;

                case "ResponseReceived":
                case "MessageReceived":
                    //textBox2.Text += $"{clientMessage.message}\r\n";
                    break;

                case "StateChanged":
                    MachineStateModel model = clientMessage.message as MachineStateModel;

                    if (model != null)
                        UpdateMachineStatus(model);

                    break;
            }

        }

        private void UpdateEnabledState()
        {
            toolStripButtonConnect.Enabled = !_machineConnected;
            toolStripButtonDisconnect.Enabled = _machineConnected;
            toolStripButtonClearAlarm.Enabled = _machineConnected;
            toolStripButtonHome.Enabled = _machineConnected;
            toolStripButtonProbe.Enabled = _machineConnected;
            toolStripButtonResume.Enabled = _machineConnected && _isPaused;
            toolStripButtonPause.Enabled = _machineConnected && !_isPaused;
            toolStripButtonStop.Enabled = _machineConnected;
            jogControl.Enabled = _machineConnected;

            tabPageOverrides.Enabled = _machineConnected;

            tabPageMachineSettings.Enabled = _machineConnected && _machineStatusModel?.MachineState == MachineState.Idle;
            btnApplyGrblUpdates.Enabled = _machineConnected && !String.IsNullOrEmpty(txtGrblUpdates.Text);
        }

        private void UpdateMachineStatus(MachineStateModel status)
        {
            if (InvokeRequired)
            {
                Invoke(UpdateMachineStatus, new object[] { status });
                return;
            }

            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                _machineConnected = status.IsConnected;

                if (status.IsConnected)
                {
                    if (!_appliedSettingsChanged)
                    {
                        LoadAllStatusChangeWarnings(status);
                    }

                    machinePositionGeneral.UpdateMachinePosition(status.MachineX, status.MachineY, status.MachineZ);
                    machinePositionGeneral.UpdateWorkPosition(status.WorkX, status.WorkY, status.WorkZ);
                    machinePositionOverrides.UpdateMachinePosition(status.MachineX, status.MachineY, status.MachineZ);
                    machinePositionOverrides.UpdateWorkPosition(status.WorkX, status.WorkY, status.WorkZ);
                    machinePositionJog.UpdateMachinePosition(status.MachineX, status.MachineY, status.MachineZ);
                    machinePositionJog.UpdateWorkPosition(status.WorkX, status.WorkY, status.WorkZ);

                    if (!toolStripStatusLabelStatus.Text.Equals(HelperMethods.TranslateState(status.MachineState)))
                    {
                        Color backColor = SystemColors.Control;
                        Color foreColor = Color.Black;
                        string text = HelperMethods.TranslateState(status.MachineState);

                        switch (status.MachineState)
                        {
                            case MachineState.Undefined:
                                text = GSend.Language.Resources.StatePortOpen;
                                backColor = Color.Yellow;
                                foreColor = Color.Black;
                                break;

                            case MachineState.Idle:
                                // no special colors
                                break;

                            case MachineState.Run:
                                backColor = Color.Green;
                                foreColor = Color.White;
                                break;

                            case MachineState.Jog:
                                backColor = Color.Green;
                                foreColor = Color.Black;
                                break;

                            case MachineState.Alarm:
                            case MachineState.Door:
                            case MachineState.Check:
                                backColor = Color.Red;
                                foreColor = Color.White;
                                break;

                            case MachineState.Home:
                                backColor = Color.Aqua;
                                foreColor = Color.Black;
                                break;

                            case MachineState.Sleep:
                                backColor = Color.DarkGray;
                                foreColor = Color.White;
                                break;
                        }

                        if (!toolStripStatusLabelStatus.Text.Equals(text))
                            toolStripStatusLabelStatus.Text = text;

                        if (toolStripStatusLabelStatus.BackColor != backColor)
                            toolStripStatusLabelStatus.BackColor = backColor;

                        if (toolStripStatusLabelStatus.ForeColor != foreColor)
                            toolStripStatusLabelStatus.ForeColor = foreColor;
                    }
                }
                else
                {
                    machinePositionGeneral.ResetPositions();
                    machinePositionJog.ResetPositions();
                    machinePositionOverrides.ResetPositions();
                    flowLayoutWarningErrors.Visible = false;
                }
            }
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
            selectionOverrideSpindle.TickFrequency = (int)_machine.Settings.MaxSpindleSpeed / 100;
        }

        private void FrmMachine_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !_gSendContext.IsClosing;
            Hide();
        }

        private void ConfigureMachine()
        {
            selectionOverrideSpindle.Maximum = (int)_machine.Settings.MaxSpindleSpeed;
            selectionOverrideSpindle.Minimum = (int)_machine.Settings.MinSpindleSpeed;
            selectionOverrideX.Maximum = (int)_machine.Settings.MaxFeedRateX;
            selectionOverrideX.Minimum = 0;
            selectionOverrideY.Maximum = (int)_machine.Settings.MaxFeedRateY;
            selectionOverrideY.Minimum = 0;
            selectionOverrideZDown.Maximum = (int)_machine.Settings.MaxFeedRateZ;
            selectionOverrideZDown.Minimum = 0;
            selectionOverrideZUp.Maximum = (int)_machine.Settings.MaxFeedRateZ;
            selectionOverrideZUp.Minimum = 0;
            selectionOverrideSpindle.Minimum = (int)_machine.Settings.MinSpindleSpeed;
            selectionOverrideSpindle.Maximum = (int)_machine.Settings.MaxSpindleSpeed;

            jogControl.FeedMaximum = (int)_machine.Settings.MaxFeedRateX;
            jogControl.FeedMinimum = 0;
            jogControl.FeedRate = jogControl.FeedMaximum / 2;
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


            // menu items
            machineToolStripMenuItem.Text = GSend.Language.Resources.Machine;
            viewToolStripMenuItem.Text = GSend.Language.Resources.View;


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

        private void jogControl1_OnJogStart(JogDirection jogDirection, double stepSize, double feedRate)
        {
            _canCancelJog = stepSize == 0;
            _clientWebSocket.SendAsync(String.Format(MessageMachineJogStart, _machine.Id, jogDirection, stepSize, feedRate)).ConfigureAwait(false);
        }

        private void jogControl1_OnJogStop(object sender, EventArgs e)
        {
            machinePositionJog.Focus();

            if (_canCancelJog)
                _clientWebSocket.SendAsync(String.Format(MessageMachineJogStop, _machine.Id)).ConfigureAwait(false);
        }

        private void propertyGridGrblSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            PropertyInfo propertyInfo = _machine.Settings.GetType().GetProperty(e.ChangedItem.Label);

            if (propertyInfo == null)
                throw new InvalidOperationException();

            GrblSettingAttribute grblSettingAttribute = propertyInfo.GetCustomAttribute<GrblSettingAttribute>();

            if (grblSettingAttribute.IntValue == 10)
            {
                ReportType existingItems = (ReportType)e.OldValue;
                ReportType newValue = (ReportType)e.ChangedItem.Value;

                if (existingItems.HasFlag(newValue))
                    existingItems &= ~ newValue;
                else
                    existingItems |= newValue;

                _machine.Settings.StatusReport = existingItems;
            }

            string prefix = $"${grblSettingAttribute.IntValue}=";

            txtGrblUpdates.Lines = txtGrblUpdates.Lines.Where(l => !l.StartsWith(prefix)).ToArray();

            object newPropertyValue = propertyInfo.GetValue(_machine.Settings);

            if (newPropertyValue.GetType().BaseType.Name.Equals("Enum"))
                newPropertyValue = (int)newPropertyValue;
            else if (newPropertyValue.GetType() == typeof(bool))
                newPropertyValue = newPropertyValue.Equals(true) ? 1 : 0;

            txtGrblUpdates.Text = $"{prefix}{newPropertyValue}\r\n{txtGrblUpdates.Text}";
        }

        private void propertyGridGrblSettings_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            PropertyInfo propertyInfo = _machine.Settings.GetType().GetProperty(e.NewSelection.Label);

            if (propertyInfo == null)
                throw new InvalidOperationException();

            GrblSettingAttribute grblSettingAttribute = propertyInfo.GetCustomAttribute<GrblSettingAttribute>();

            if (grblSettingAttribute == null)
                throw new InvalidOperationException();

            string dollarValue = grblSettingAttribute.DollarValue;

            lblPropertyHeader.Text = $"{dollarValue} - {Shared.Utilities.SplitCamelCase(e.NewSelection.Label)}";
            lblPropertyDesc.Text = GSend.Language.Resources.ResourceManager.GetString($"SettingDescription{grblSettingAttribute.IntValue}");
        }

        private void txtGrblUpdates_TextChanged(object sender, EventArgs e)
        {
            btnApplyGrblUpdates.Enabled = !String.IsNullOrEmpty(txtGrblUpdates.Text);
        }

        private void btnApplyGrblUpdates_Click(object sender, EventArgs e)
        {
            foreach (string line in txtGrblUpdates.Lines)
            {
                string command = line;

                if (command.IndexOf(Constants.SemiColon) > -1)
                    command = command[..command.IndexOf(Constants.SemiColon)].Trim();

                if (String.IsNullOrEmpty(command))
                    continue;

                command = String.Format(Constants.MessageMachineUpdateSetting, _machine.Id, command);
                _clientWebSocket.SendAsync(command).ConfigureAwait(false);
            }
        }

        private void UpdateThread()
        {
            while (true)
            {
                if (_threadRun)
                {
                    _clientWebSocket.SendAsync(String.Format(MessageMachineStatus, _machine.Id)).ConfigureAwait(false);
                    Thread.Sleep(_gSendContext.Settings.UpdateMilliseconds);
                }
            }
        }

        private void flowLayoutWarningErrors_VisibleChanged(object sender, EventArgs e)
        {
           
            if (flowLayoutWarningErrors.Visible && flowLayoutWarningErrors.Controls.Count > 0)
            {
                if (flowLayoutWarningErrors.Controls.Count < 2)
                    flowLayoutWarningErrors.Height = 27;
                else
                    flowLayoutWarningErrors.Height = 48;

                tabControlMain.Top = flowLayoutWarningErrors.Top + flowLayoutWarningErrors.Height + 8;
                textBox2.Top = tabControlMain.Top + tabControlMain.Height + 8;
                textBox2.Height = statusStrip.Top - (textBox2.Top + 6);
            }
            else
            {
                tabControlMain.Top = 86;
                textBox2.Top = tabControlMain.Top + tabControlMain.Height + 8;
                textBox2.Height = statusStrip.Top - (textBox2.Top + 6);
            }
        }

        private void ResetLayoutWarningErrorSize()
        {
            int minusScrollBar = 0;

            if (flowLayoutWarningErrors.Controls.Count > 1)
                minusScrollBar = 12;

            foreach (Control control in flowLayoutWarningErrors.Controls)
            {
                control.Width = flowLayoutWarningErrors.ClientSize.Width - minusScrollBar;
            }
        }

        private void LoadAllStatusChangeWarnings(MachineStateModel status)
        {
            foreach (ChangedGrblSettings changedGrblSetting in status.UpdatedGrblConfiguration)
            {
                string setting = String.Format(GSend.Language.Resources.GrblValueUpdated,
                    changedGrblSetting.DollarValue,
                    changedGrblSetting.OldValue,
                    changedGrblSetting.NewValue,
                    changedGrblSetting.PropertyName);

                AddWarningPanel(setting);
            }

            _appliedSettingsChanged = true;
        }

        private void AddWarningPanel(string message)
        {
            WarningPanel warningPanel = new WarningPanel(message);
            warningPanel.Width = flowLayoutWarningErrors.ClientSize.Width - 10;
            warningPanel.WarningClose += WarningPanel_WarningClose;
            flowLayoutWarningErrors.Controls.Add(warningPanel);
            UpdateWarningStatusBarItem();
            flowLayoutWarningErrors.Visible = flowLayoutWarningErrors.Controls.Count > 0;
            ResetLayoutWarningErrorSize();
        }

        private void WarningPanel_WarningClose(object sender, EventArgs e)
        {
            if (sender is  WarningPanel warningPanel)
            {
                warningPanel.WarningClose -= WarningPanel_WarningClose;
                flowLayoutWarningErrors.Controls.Remove(warningPanel);
            }

            UpdateWarningStatusBarItem();
            flowLayoutWarningErrors.Visible = flowLayoutWarningErrors.Controls.Count > 0;
            ResetLayoutWarningErrorSize();
            flowLayoutWarningErrors_VisibleChanged(flowLayoutWarningErrors, EventArgs.Empty);
        }

        private void UpdateWarningStatusBarItem()
        {
            int count = 0;

            foreach (Control control in flowLayoutWarningErrors.Controls)
            {
                if (control is WarningPanel warningPanel)
                {
                    count++;
                }
            }

            toolStripStatusLabelWarnings.Text = count.ToString();
            toolStripStatusLabelWarnings.Visible = count > 0;
        }

        private void toolStripButtonConnect_Click(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(String.Format(MessageMachineConnect, _machine.Id)).ConfigureAwait(false);
        }

        private void toolStripButtonDisconnect_Click(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(String.Format(MessageMachineConnect, _machine.Id)).ConfigureAwait(false);
        }

        private void toolStripButtonClearAlarm_Click(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(String.Format(MessageMachineClearAlarm, _machine.Id)).ConfigureAwait(false);
        }

        private void toolStripButtonHome_Click(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(String.Format(MessageMachineHome, _machine.Id)).ConfigureAwait(false);
        }

        private void toolStripButtonProbe_Click(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(String.Format(MessageMachineProbe, _machine.Id)).ConfigureAwait(false);
        }

        private void toolStripButtonResume_Click(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(String.Format(MessageMachineResume, _machine.Id)).ConfigureAwait(false);
        }

        private void toolStripButtonPause_Click(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(String.Format(MessageMachinePause, _machine.Id)).ConfigureAwait(false);
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(String.Format(MessageMachineStop, _machine.Id)).ConfigureAwait(false);
        }
    }
}
