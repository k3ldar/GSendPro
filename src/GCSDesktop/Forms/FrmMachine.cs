using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

using GSendApi;

using GSendCommon;

using GSendDesktop.Controls;

using GSendShared;
using GSendShared.Attributes;
using GSendShared.Models;

using Microsoft.Extensions.DependencyInjection;

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
        private bool _isRunning = false;
        private bool _isJogging = false;
        private bool _isAlarm = false;
        private bool _canCancelJog = false;
        private bool _isProbing = false;
        private bool _appliedSettingsChanged = false;
        private bool _configurationChanges = false;

        public FrmMachine()
        {
            InitializeComponent();

        }

        public FrmMachine(IGSendContext gSendContext, IMachine machine)
            : this()
        {
            _gSendContext = gSendContext ?? throw new ArgumentNullException(nameof(gSendContext));
            _machine = machine ?? throw new ArgumentNullException(nameof(machine));

            Text = String.Format(GSend.Language.Resources.MachineTitle, machine.MachineType, machine.Name);

            if (machine.MachineType == MachineType.Printer)
                tabControlMain.TabPages.Remove(tabPageOverrides);

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

            cbOverridesDisable.Checked = true;

            lblPropertyHeader.Text = String.Empty;
            lblPropertyDesc.Text = String.Empty;

            btnZeroX.Tag = ZeroAxis.X;
            btnZeroY.Tag = ZeroAxis.Y;
            btnZeroZ.Tag = ZeroAxis.Z;
            btnZeroAll.Tag = ZeroAxis.All;

            tabControlMain.SelectedTab = tabPageMain;

            cmbSpindleType.Items.Add(SpindleType.Integrated);
            cmbSpindleType.Items.Add(SpindleType.VFD);
            cmbSpindleType.Items.Add(SpindleType.External);

            UpdateDisplay();
            UpdateEnabledState();
            probingCommand1.InitializeProbingCommand(_machine.ProbeCommand, _machine.ProbeSpeed, _machine.ProbeThickness);
            probingCommand1.OnSave += ProbingCommand1_OnSave;

            ConfigureMachine();

            _configurationChanges = false;

            LoadResources();
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
            StopJogging();
            warningsAndErrors.Visible = false;
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
            Trace.WriteLine(message);
            if (clientMessage == null)
                return;

            if (!clientMessage.success)
            {
                ProcessFailedMessage(clientMessage);
            }

            string serverCpu = String.Format(GSend.Language.Resources.ServerCpuStateConnected, clientMessage.ServerCpuStatus.ToString("N2"));

            switch (clientMessage.request)
            {
                case "mProbe":
                    _isProbing = false;
                    break;

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
                    ConfigureMachine();
                    _configurationChanges = false;
                    break;

                case "Disconnect":
                    _machineConnected = false;
                    txtGrblUpdates.Text = String.Empty;
                    _appliedSettingsChanged = false;
                    warningsAndErrors.Clear();
                    warningsAndErrors_OnUpdate(warningsAndErrors, EventArgs.Empty);
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
                case "Alarm":
                    ProcessAlarmResponse(clientMessage);
                    break;
            }
        }

        private void UpdateEnabledState()
        {
            toolStripButtonSave.Enabled = _configurationChanges;
            toolStripButtonConnect.Enabled = !_machineConnected;
            toolStripButtonDisconnect.Enabled = _machineConnected && !_isJogging && !_isProbing;
            toolStripButtonClearAlarm.Enabled = _machineConnected && _isAlarm;
            toolStripButtonHome.Enabled = _machineConnected && !_isAlarm && !_isJogging && !_isPaused && !_isRunning && !_isProbing;
            toolStripButtonProbe.Enabled = _machineConnected && !_isAlarm && !_isJogging && !_isPaused && !_isRunning && !_isProbing;
            toolStripButtonResume.Enabled = _machineConnected && _isPaused;
            toolStripButtonPause.Enabled = _machineConnected & (_isPaused || _isRunning);
            toolStripButtonStop.Enabled = _machineConnected && !_isProbing && (_isRunning || _isJogging);
            jogControl.Enabled = _machineConnected && !_isProbing && !_isAlarm && !_isRunning;
            btnZeroAll.Enabled = toolStripButtonProbe.Enabled;
            btnZeroX.Enabled = toolStripButtonProbe.Enabled;
            btnZeroY.Enabled = toolStripButtonProbe.Enabled;
            btnZeroZ.Enabled = toolStripButtonProbe.Enabled;

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
                    toolStripStatusLabelBuffer.Text = $"Available bytes/blocks:{status.AvailableRXbytes}/{status.BufferAvailableBlocks}";

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
                    _isPaused = status.IsPaused;
                    _isRunning = status.IsRunning;
                    _isJogging = status.MachineState == MachineState.Jog;
                    _isAlarm = status.IsLocked;

                    if (!toolStripStatusLabelStatus.Text.Equals(HelperMethods.TranslateState(status.MachineState)))
                    {
                        Color backColor = SystemColors.Control;
                        Color foreColor = SystemColors.ControlText;

                        string text = HelperMethods.TranslateState(status.MachineState);

                        switch (status.MachineState)
                        {
                            case MachineState.Undefined:
                                text = status.IsPaused ? GSend.Language.Resources.StatePaused : GSend.Language.Resources.StatePortOpen;
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

                        UpdateMachineStatusLabel(backColor, foreColor, text);
                    }
                }
                else
                {
                    toolStripStatusLabelBuffer.Text = String.Empty;
                    toolStripStatusLabelBuffer.Text = String.Empty;
                    UpdateMachineStatusLabel(SystemColors.Control, SystemColors.ControlText, String.Empty);
                    machinePositionGeneral.ResetPositions();
                    machinePositionJog.ResetPositions();
                    machinePositionOverrides.ResetPositions();
                    warningsAndErrors.Visible = false;
                    _isRunning = false;
                    _isJogging = false;
                    _isPaused = false;
                    _isAlarm = false;
                }
            }
        }

        private void UpdateMachineStatusLabel(Color backColor, Color foreColor, string text)
        {
            if (!toolStripStatusLabelStatus.Text.Equals(text))
                toolStripStatusLabelStatus.Text = text;

            if (toolStripStatusLabelStatus.BackColor != backColor)
                toolStripStatusLabelStatus.BackColor = backColor;

            if (toolStripStatusLabelStatus.ForeColor != foreColor)
                toolStripStatusLabelStatus.ForeColor = foreColor;
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

            jogControl.FeedMaximum = (int)_machine.Settings.MaxFeedRateX;
            jogControl.FeedMinimum = 0;
            jogControl.FeedRate = jogControl.FeedMaximum / 2;
            jogControl.FeedMinimum = 0;
            jogControl.StepValue = 7;
            jogControl.FeedRate = _machine.JogFeedrate;
            trackBarPercent.Value = _machine.OverrideSpeed;
            selectionOverrideSpindle.Value = _machine.OverrideSpindle;
            cmbSpindleType.SelectedItem = _machine.SpindleType;
            cbSoftStart.Checked = _machine.SoftStart;
            trackBarDelaySpindle.Value = _machine.SoftStartSeconds;
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


            //Spindle tab
            lblSpindleType.Text = GSend.Language.Resources.SpindleType;
            cbSoftStart.Text = GSend.Language.Resources.SpindleSoftStart;

            if (_machine.SpindleType == SpindleType.Integrated)
                lblDelaySpindleStart.Text = String.Format(GSend.Language.Resources.SpindleSoftStartSeconds, trackBarDelaySpindle.Value);
            else
                lblDelaySpindleStart.Text = String.Format(GSend.Language.Resources.SpindleDelayStartVFD, trackBarDelaySpindle.Value);

            // menu items
            machineToolStripMenuItem.Text = GSend.Language.Resources.Machine;
            viewToolStripMenuItem.Text = GSend.Language.Resources.View;


            // Override tab
            cbOverridesDisable.Text = GSend.Language.Resources.DisableOverrides;
        }

        private void trackBarPercent_ValueChanged(object sender, EventArgs e)
        {
            UpdateOverrides();
            _machine.OverrideSpeed = trackBarPercent.Value;
            _configurationChanges = true;
        }

        private void selectionOverrideSpindle_ValueChanged(object sender, EventArgs e)
        {
            _machine.OverrideSpindle = selectionOverrideSpindle.Value;
            _configurationChanges = true;
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

        private void jogControl_OnJogStart(JogDirection jogDirection, double stepSize, double feedRate)
        {
            _canCancelJog = stepSize == 0;
            _clientWebSocket.SendAsync(String.Format(MessageMachineJogStart, _machine.Id, jogDirection, stepSize, feedRate)).ConfigureAwait(false);
        }

        private void jogControl_OnJogStop(object sender, EventArgs e)
        {
            machinePositionJog.Focus();

            if (_canCancelJog)
                StopJogging();
        }

        private void StopJogging()
        {
            _clientWebSocket.SendAsync(String.Format(MessageMachineJogStop, _machine.Id)).ConfigureAwait(false);
        }

        #region Grbl Settings

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

            SaveChanges(true);
            ConfigureMachine();
        }

        #endregion Grbl Settings

        #region Update thread

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

        #endregion Update thread

        #region Warnings and Error Handling

        private void LoadAllStatusChangeWarnings(MachineStateModel status)
        {
            foreach (ChangedGrblSettings changedGrblSetting in status.UpdatedGrblConfiguration)
            {
                string setting = String.Format(GSend.Language.Resources.GrblValueUpdated,
                    changedGrblSetting.DollarValue,
                    changedGrblSetting.OldValue,
                    changedGrblSetting.NewValue,
                    changedGrblSetting.PropertyName);

                warningsAndErrors.AddWarningPanel(InformationType.Warning, setting);
            }

            _appliedSettingsChanged = true;
        }

        private void WarningContainer_VisibleChanged(object sender, EventArgs e)
        {
            if (warningsAndErrors.Visible)
            {
                tabControlMain.Top = warningsAndErrors.Top + warningsAndErrors.Height + 8;
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

        private void warningsAndErrors_OnUpdate(object sender, EventArgs e)
        {
            int warningCount = warningsAndErrors.WarningCount();
            toolStripStatusLabelWarnings.Text = warningCount.ToString();
            toolStripStatusLabelWarnings.Visible = warningCount > 0;
        }

        private void ProcessAlarmResponse(ClientBaseMessage clientMessage)
        {
            _isProbing = false;
            JsonElement element = (JsonElement)clientMessage.message;
            GrblAlarm alarm = (GrblAlarm)element.GetInt32();
            string alarmDescription = GSend.Language.Resources.ResourceManager.GetString($"Alarm{(int)alarm}");
            warningsAndErrors.AddWarningPanel(InformationType.Alarm, alarmDescription);
        }

        private void ProcessFailedMessage(ClientBaseMessage clientMessage)
        {
            string[] message = clientMessage.request.Split(Constants.ColonChar, 
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            if (message.Length == 0)
                return;

            switch (message[0])
            {
                case Constants.MessageMachineProbeServer:
                    if (_isProbing)
                    {
                        _isProbing = false;
                        warningsAndErrors.AddWarningPanel(InformationType.Warning, GSend.Language.Resources.ProbingFailed);
                    }

                    break;

                default:
                    break;
            }
        }

        #endregion Warnings and Error Handling

        #region Toolbar Buttons

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            SaveChanges(false);
        }

        private void SaveChanges(bool forceOverride)
        { 
            if (_configurationChanges || forceOverride)
            {
                MachineApiWrapper machineApiWrapper = _gSendContext.ServiceProvider.GetRequiredService<MachineApiWrapper>();

                machineApiWrapper.MachineUpdate(_machine);

                _configurationChanges = false;
            }
        }

        private void toolStripButtonConnect_Click(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(String.Format(MessageMachineConnect, _machine.Id)).ConfigureAwait(false);
        }

        private void toolStripButtonDisconnect_Click(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(String.Format(MessageMachineDisconnect, _machine.Id)).ConfigureAwait(false);
        }

        private void toolStripButtonClearAlarm_Click(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(String.Format(MessageMachineClearAlarm, _machine.Id)).ConfigureAwait(false);
            warningsAndErrors.ClearAlarm();
        }

        private void toolStripButtonHome_Click(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(String.Format(MessageMachineHome, _machine.Id)).ConfigureAwait(false);
        }

        private void toolStripButtonProbe_Click(object sender, EventArgs e)
        {
            _isProbing = true;
            _clientWebSocket.SendAsync(String.Format(MessageMachineProbe, _machine.Id)).ConfigureAwait(false);
            UpdateEnabledState();
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
            if (_isJogging)
                StopJogging();
            else
                _clientWebSocket.SendAsync(String.Format(MessageMachineStop, _machine.Id)).ConfigureAwait(false);
        }

        #endregion Toolbar Buttons

        #region Zeroing

        public void SetZeroForAxes(object sender, EventArgs e)
        {
            ZeroAxis zeroAxis = (ZeroAxis)((System.Windows.Forms.Button)sender).Tag;
            _clientWebSocket.SendAsync(String.Format(MessageMachineSetZero, _machine.Id, (int)zeroAxis, 0)).ConfigureAwait(false);
            tabPageJog.Focus();
        }

        #endregion Zeroing

        #region Probing

        private void ProbingCommand1_OnSave(object sender, EventArgs e)
        {
            _machine.ProbeThickness = probingCommand1.Thickness;
            _machine.ProbeSpeed = probingCommand1.TravelSpeed;
            _machine.ProbeCommand = probingCommand1.ProbeCommand;

            _configurationChanges = true;
            UpdateEnabledState();

            ConfigureMachine();
        }

        #endregion Probing

        private void jogControl_OnUpdate(object sender, EventArgs e)
        {
            _machine.JogUnits = jogControl.StepValue;
            _machine.JogFeedrate = jogControl.FeedRate;
            _configurationChanges = true;
        }

        private void cmbSpindleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _machine.SpindleType = (SpindleType)cmbSpindleType.SelectedItem;
            _configurationChanges = true;
            cbSoftStart.Visible = _machine.SpindleType == SpindleType.Integrated;
            trackBarDelaySpindle.Visible = _machine.SpindleType != SpindleType.External;
            lblDelaySpindleStart.Visible = _machine.SpindleType != SpindleType.External;
            trackBarDelaySpindle_ValueChanged(sender, e);
        }

        private void trackBarDelaySpindle_ValueChanged(object sender, EventArgs e)
        {
            _machine.SoftStartSeconds = trackBarDelaySpindle.Value;

            if (_machine.SpindleType == SpindleType.Integrated)
                lblDelaySpindleStart.Text = String.Format(GSend.Language.Resources.SpindleSoftStartSeconds, trackBarDelaySpindle.Value);
            else
                lblDelaySpindleStart.Text = String.Format(GSend.Language.Resources.SpindleDelayStartVFD, trackBarDelaySpindle.Value);

            _configurationChanges = true;
        }

        private void cbSoftStart_CheckedChanged(object sender, EventArgs e)
        {
            _machine.SoftStart = cbSoftStart.Checked;
            _configurationChanges = true;
        }
    }
}
