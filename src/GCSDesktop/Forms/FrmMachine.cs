using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

using GSendAnalyser.Internal;

using GSendApi;

using GSendCommon;

using GSendDesktop.Controls;

using GSendShared;
using GSendShared.Attributes;
using GSendShared.Models;

using Microsoft.Extensions.DependencyInjection;

using Shared.Classes;

using static GSendShared.Constants;

namespace GSendDesktop.Forms
{
    public partial class FrmMachine : Form, IUiUpdate
    {
        private const int WarningStatusWidth = 40;
        #region Private Fields

        private readonly CancellationTokenRegistration _cancellationTokenRegistration;
        private readonly GSendWebSocket _clientWebSocket;
        private readonly IGSendContext _gSendContext;
        private readonly IMachine _machine;
        private MachineStateModel _machineStatusModel = null;
        private MachineUpdateThread _machineUpdateThread;
        private IGCodeAnalyses _gCodeAnalyses = null;
        private readonly object _lockObject = new();
        private readonly Pen _borderPen = new Pen(SystemColors.ButtonShadow);
        private bool _machineConnected = false;
        private bool _isPaused = false;
        private bool _isRunning = false;
        private bool _isJogging = false;
        private bool _isAlarm = false;
        private bool _canCancelJog = false;
        private bool _isProbing = false;
        private bool _appliedSettingsChanged = false;
        private bool _configurationChanges = false;
        private bool _lastMessageWasHiddenCommand = false;
        private bool _updatingRapidOverride = false;

        #endregion Private Fields

        #region Constructors

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
            _machineUpdateThread = new MachineUpdateThread(new TimeSpan(0, 0, 0, 0,
                _gSendContext.Settings.UpdateMilliseconds), _clientWebSocket,
                _machine, this);
            ThreadManager.ThreadStart(_machineUpdateThread, $"{machine.Name} Update Status", ThreadPriority.Normal);

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

            ConfigureMachine();
            toolStripStatusLabelSpindle.Visible = false;
            toolStripStatusLabelFeedRate.Visible = false;
            toolStripStatusLabelBuffer.Visible = false;
            toolStripStatusLabelStatus.Visible = false;
            warningsAndErrors_OnUpdate(this, EventArgs.Empty);
            _configurationChanges = false;
            HookUpEvents();
            UpdateMachineStatus(new MachineStateModel());

            LoadResources();
            WarningContainer_VisibleChanged(this, EventArgs.Empty);
        }

        #endregion Constructors

        #region Client Web Socket

        private void SendMessage(string message)
        {
            _clientWebSocket.SendAsync(message).ConfigureAwait(false);
        }

        private void ClientWebSocket_Connected(object sender, EventArgs e)
        {
            _machineUpdateThread.IsThreadRunning = true;
            SendMessage(String.Format("mAddEvents:{0}", _machine.Id));
            toolStripStatusLabelServerConnect.Text = GSend.Language.Resources.ServerConnected;
            UpdateEnabledState();
            btnServiceRefresh_Click(sender, e);
        }

        private void ClientWebSocket_ConnectionLost(object sender, EventArgs e)
        {
            StopJogging();
            warningsAndErrors.Visible = warningsAndErrors.TotalCount() > 0;
            _machineUpdateThread.IsThreadRunning = false;
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
            //Trace.WriteLine(message);

            if (clientMessage == null)
                return;

            if (!clientMessage.success)
            {
                ProcessFailedMessage(clientMessage);
            }

            string serverCpu = String.Format(GSend.Language.Resources.ServerCpuStateConnected, clientMessage.ServerCpuStatus.ToString("N2"));

            switch (clientMessage.request)
            {
                case "ComPortTimeout":
                    warningsAndErrors.AddWarningPanel(InformationType.Warning, "Com port time out");
                    break;

                case "InvalidComPort":
                    warningsAndErrors.AddWarningPanel(InformationType.ErrorKeep, "Com port bad");
                    break;

                case Constants.MessageMachineProbeServer:
                    _isProbing = false;
                    break;

                case Constants.MessageMachineStatusServer:
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
                    toolStripStatusLabelSpindle.Visible = _machine.MachineType == MachineType.CNC;
                    toolStripStatusLabelFeedRate.Visible = true;
                    toolStripStatusLabelBuffer.Visible = true;
                    toolStripStatusLabelStatus.Visible = true;

                    break;

                case "Disconnect":
                    _machineConnected = false;
                    txtGrblUpdates.Text = String.Empty;
                    _appliedSettingsChanged = false;
                    warningsAndErrors.Clear(false);
                    warningsAndErrors_OnUpdate(warningsAndErrors, EventArgs.Empty);
                    toolStripStatusLabelSpindle.Visible = false;
                    toolStripStatusLabelFeedRate.Visible = false;
                    toolStripStatusLabelBuffer.Visible = false;
                    toolStripStatusLabelStatus.Visible = false;
                    _isPaused = false;
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
                    if (clientMessage.message.ToString().StartsWith("<") || _isJogging)
                    {
                        _lastMessageWasHiddenCommand = true;
                    }
                    else if (_lastMessageWasHiddenCommand && clientMessage.message.ToString().Equals("ok"))
                    {
                        _lastMessageWasHiddenCommand = false;
                    }
                    else if (clientMessage.message.ToString().Equals("ok") && textBoxConsoleText.Text.EndsWith("ok\r\n"))
                    {

                    }
                    else
                    {
                        AddMessageToConsole(clientMessage.message.ToString());
                    }

                    break;

                case "StateChanged":
                    JsonElement element = (JsonElement)clientMessage.message;
                    MachineStateModel model = element.Deserialize<MachineStateModel>();

                    if (model != null)
                        UpdateMachineStatus(model);

                    break;

                case "GrblError":
                    ProcessErrorResponse(clientMessage);
                    break;

                case Constants.StateAlarm:
                    ProcessAlarmResponse(clientMessage);
                    break;

                case Constants.MessageMachineWriteLineServerR:
                    string response = clientMessage.message.ToString();
                    textBoxConsoleText.AppendText(response);
                    break;

                case Constants.MessageMachineUpdateRapidOverridesAdmin:
                    _updatingRapidOverride = false;
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
            toolStripButtonStop.Enabled = _machineConnected && !_isProbing && (_isRunning || _isJogging || _isPaused);
            toolStripDropDownButtonCoordinateSystem.Enabled = _machineConnected && !_isProbing && (!_isRunning || !_isJogging || !_isPaused);

            jogControl.Enabled = _machineConnected && !_isProbing && !_isAlarm && !_isRunning;
            btnZeroAll.Enabled = toolStripButtonProbe.Enabled;
            btnZeroX.Enabled = toolStripButtonProbe.Enabled;
            btnZeroY.Enabled = toolStripButtonProbe.Enabled;
            btnZeroZ.Enabled = toolStripButtonProbe.Enabled;

            tabPageOverrides.Enabled = _machineConnected;

            tabPageMachineSettings.Enabled = _machineConnected && _machineStatusModel?.MachineState == MachineState.Idle;
            btnApplyGrblUpdates.Enabled = _machineConnected && !String.IsNullOrEmpty(txtGrblUpdates.Text);
            tabPageConsole.Enabled = _machineConnected && !_isRunning && !_isPaused && !_isProbing && !_isAlarm;
            grpBoxSpindleSpeed.Enabled = _machineConnected;

            loadToolStripMenuItem.Enabled = _gCodeAnalyses == null;
            clearToolStripMenuItem.Enabled = _gCodeAnalyses != null;
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

                if (toolStripDropDownButtonCoordinateSystem.Text != status.CoordinateSystem.ToString())
                {
                    toolStripDropDownButtonCoordinateSystem.Text = status.CoordinateSystem.ToString();
                    DeselectCoordinateMenuItems();

                    switch (status.CoordinateSystem)
                    {
                        case CoordinateSystem.G54:
                            g54ToolStripMenuItem.Checked = true;
                            break;
                        case CoordinateSystem.G55:
                            g55ToolStripMenuItem.Checked = true;
                            break;
                        case CoordinateSystem.G56:
                            g56ToolStripMenuItem.Checked = true;
                            break;
                        case CoordinateSystem.G57:
                            g57ToolStripMenuItem.Checked = true;
                            break;
                        case CoordinateSystem.G58:
                            g58ToolStripMenuItem.Checked = true;
                            break;
                        case CoordinateSystem.G59:
                            g59ToolStripMenuItem.Checked = true;
                            break;
                    }
                }

                if (!_updatingRapidOverride && selectionOverrideRapids.Value != (int)status.RapidSpeed)
                {
                    selectionOverrideRapids.ValueChanged -= SelectionOverrideRapids_ValueChanged;
                    selectionOverrideRapids.Value = (int)status.RapidSpeed;
                    selectionOverrideRapids.LabelValue = HelperMethods.TranslateRapidOverride(status.RapidSpeed);
                    selectionOverrideRapids.ValueChanged += SelectionOverrideRapids_ValueChanged;
                }

                if (status.IsConnected)
                {
                    toolStripStatusLabelBuffer.Text = $"{status.AvailableRXbytes}/{status.BufferAvailableBlocks}";

                    if (!_appliedSettingsChanged)
                    {
                        LoadAllStatusChangeWarnings(status);
                    }

                    if (_machine.SpindleType.Equals(SpindleType.Integrated))
                    {
                        if (status.SpindleClockWise && status.SpindleSpeed > 0)
                            toolStripStatusLabelSpindle.Text = String.Format(GSend.Language.Resources.SpindleClockwise, status.SpindleSpeed);
                        else if (status.SpindleCounterClockWise && status.SpindleSpeed > 0)
                            toolStripStatusLabelSpindle.Text = String.Format(GSend.Language.Resources.SpindleCounterClockwise, status.SpindleSpeed);
                        else
                            toolStripStatusLabelSpindle.Text = GSend.Language.Resources.SpindleInactive;
                    }
                    else
                    {
                        if (status.SpindleSpeed > 0 && (status.SpindleClockWise || status.SpindleCounterClockWise))
                            toolStripStatusLabelSpindle.Text = GSend.Language.Resources.SpindleActive;
                        else
                            toolStripStatusLabelSpindle.Text = GSend.Language.Resources.SpindleInactive;
                    }

                    toolStripStatusLabelFeedRate.Text = HelperMethods.ConvertFeedRateForDisplay(_machine.DisplayUnits, status.FeedRate);

                    btnSpindleStart.Enabled = status.SpindleSpeed == 0;
                    btnSpindleStop.Enabled = status.SpindleSpeed > 0;

                    machinePositionGeneral.UpdateMachinePosition(status.MachineX, status.MachineY, status.MachineZ);
                    machinePositionGeneral.UpdateWorkPosition(status.WorkX, status.WorkY, status.WorkZ);
                    machinePositionOverrides.UpdateMachinePosition(status.MachineX, status.MachineY, status.MachineZ);
                    machinePositionOverrides.UpdateWorkPosition(status.WorkX, status.WorkY, status.WorkZ);
                    machinePositionJog.UpdateMachinePosition(status.MachineX, status.MachineY, status.MachineZ);
                    machinePositionJog.UpdateWorkPosition(status.WorkX, status.WorkY, status.WorkZ);
                    _isPaused = status.IsPaused || status.MachineState == MachineState.Hold;
                    _isRunning = status.IsRunning || status.MachineState == MachineState.Run;
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
                    toolStripStatusLabelStatus.Text = String.Empty;
                    UpdateMachineStatusLabel(SystemColors.Control, SystemColors.ControlText, String.Empty);
                    machinePositionGeneral.ResetPositions();
                    machinePositionJog.ResetPositions();
                    machinePositionOverrides.ResetPositions();
                    warningsAndErrors.Visible = warningsAndErrors.TotalCount() > 0;
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

        #region Thread Send

        private void SendByThread(string message)
        {
            if (!ThreadManager.Exists($"{_machine.Name} Update Status"))
            {
                _machineUpdateThread = new MachineUpdateThread(new TimeSpan(0, 0, 0, 0,
                    _gSendContext.Settings.UpdateMilliseconds), _clientWebSocket, _machine, this);
                ThreadManager.ThreadStart(_machineUpdateThread, $"{_machine.Name} Update Status", ThreadPriority.Normal);
            }

            _machineUpdateThread.ThreadSendCommandQueue.Enqueue(message);

        }

        #endregion Thread Send

        #region Service

        private void TrackBarServiceSpindleHours_ValueChanged(object sender, EventArgs e)
        {
            _machine.ServiceSpindleHours = trackBarServiceSpindleHours.Value;
            lblSpindleHours.Text = String.Format(GSend.Language.Resources.ServiceSpindleHours, trackBarServiceSpindleHours.Value);
            UpdateConfigurationChanged();
        }

        private void TrackBarServiceWeeks_ValueChanged(object sender, EventArgs e)
        {
            _machine.ServiceWeeks = trackBarServiceWeeks.Value;
            lblServiceSchedule.Text = String.Format(GSend.Language.Resources.ServiceWeeks, trackBarServiceWeeks.Value);
            UpdateConfigurationChanged();
        }

        private void CbMaintainServiceSchedule_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMaintainServiceSchedule.Checked)
                _machine.AddOptions(MachineOptions.ServiceSchedule);
            else
                _machine.RemoveOptions(MachineOptions.ServiceSchedule);

            trackBarServiceSpindleHours.Enabled = cbMaintainServiceSchedule.Checked;
            trackBarServiceWeeks.Enabled = cbMaintainServiceSchedule.Checked;
            lblSpindleHours.Enabled = cbMaintainServiceSchedule.Checked;
            lblServiceSchedule.Enabled = cbMaintainServiceSchedule.Checked;
            lblNextService.Enabled = cbMaintainServiceSchedule.Checked;
            lblServiceDate.Enabled = cbMaintainServiceSchedule.Checked;
            lblSpindleHoursRemaining.Enabled = cbMaintainServiceSchedule.Checked;
            btnServiceRefresh.Enabled = cbMaintainServiceSchedule.Checked;
            btnServiceReset.Enabled = cbMaintainServiceSchedule.Checked;

            UpdateConfigurationChanged();
        }

        public void RefreshServiceSchedule()
        {
            if (InvokeRequired)
            {
                Invoke(RefreshServiceSchedule);
                return;
            }

            btnServiceRefresh_Click(this, EventArgs.Empty);
        }

        #endregion Service

        #region Settings

        private void CbAutoSelectFeedbackUnit_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoSelectFeedbackUnit.Checked)
                _machine.AddOptions(MachineOptions.AutoUpdateDisplayFromFile);
            else
                _machine.RemoveOptions(MachineOptions.AutoUpdateDisplayFromFile);

            UpdateConfigurationChanged();
        }

        private void CbLayerHeightWarning_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLayerHeightWarning.Checked)
                _machine.AddOptions(MachineOptions.LayerHeightWarning);
            else
                _machine.RemoveOptions(MachineOptions.LayerHeightWarning);

            UpdateConfigurationChanged();
        }

        private void NumericLayerHeight_CheckedChanged(object sender, EventArgs e)
        {
            _machine.LayerHeightWarning = numericLayerHeight.Value;

            UpdateConfigurationChanged();
        }

        private void CbLimitSwitches_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLimitSwitches.Checked)
                _machine.AddOptions(MachineOptions.LimitSwitches);
            else
                _machine.RemoveOptions(MachineOptions.LimitSwitches);

            UpdateConfigurationChanged();
        }

        private void CbToolChanger_CheckedChanged(object sender, EventArgs e)
        {
            if (cbToolChanger.Checked)
                _machine.AddOptions(MachineOptions.ToolChanger);
            else
                _machine.RemoveOptions(MachineOptions.ToolChanger);

            UpdateConfigurationChanged();
        }

        private void CbMistCoolant_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMistCoolant.Checked)
                _machine.AddOptions(MachineOptions.MistCoolant);
            else
                _machine.RemoveOptions(MachineOptions.MistCoolant);

            UpdateConfigurationChanged();
        }

        private void CbFloodCoolant_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFloodCoolant.Checked)
                _machine.AddOptions(MachineOptions.FloodCoolant);
            else
                _machine.RemoveOptions(MachineOptions.FloodCoolant);

            UpdateConfigurationChanged();
        }

        private void CbCorrectMode_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCorrectMode.Checked)
                _machine.AddOptions(MachineOptions.AutoCorrectLaserSpindleMode);
            else
                _machine.RemoveOptions(MachineOptions.AutoCorrectLaserSpindleMode);

            UpdateConfigurationChanged();
        }

        private void RbFeedDisplay_CheckedChanged(object sender, EventArgs e)
        {
            rbFeedDisplayInchMin.CheckedChanged -= RbFeedDisplay_CheckedChanged;
            rbFeedDisplayInchSec.CheckedChanged -= RbFeedDisplay_CheckedChanged;
            rbFeedDisplayMmMin.CheckedChanged -= RbFeedDisplay_CheckedChanged;
            rbFeedDisplayMmSec.CheckedChanged -= RbFeedDisplay_CheckedChanged;

            if (rbFeedDisplayInchMin.Checked)
                _machine.DisplayUnits = FeedRateDisplayUnits.InchPerMinute;
            else if (rbFeedDisplayInchSec.Checked)
                _machine.DisplayUnits = FeedRateDisplayUnits.InchPerSecond;
            else if (rbFeedDisplayMmMin.Checked)
                _machine.DisplayUnits = FeedRateDisplayUnits.MmPerMinute;
            else if (rbFeedDisplayMmSec.Checked)
                _machine.DisplayUnits = FeedRateDisplayUnits.MmPerSecond;

            UpdateConfigurationChanged();
            UpdateDisplay();

            rbFeedDisplayInchMin.CheckedChanged += RbFeedDisplay_CheckedChanged;
            rbFeedDisplayInchSec.CheckedChanged += RbFeedDisplay_CheckedChanged;
            rbFeedDisplayMmMin.CheckedChanged += RbFeedDisplay_CheckedChanged;
            rbFeedDisplayMmSec.CheckedChanged += RbFeedDisplay_CheckedChanged;
        }

        private void RbFeedback_CheckedChanged(object sender, EventArgs e)
        {
            rbFeedbackInch.CheckedChanged -= RbFeedback_CheckedChanged;
            rbFeedbackMm.CheckedChanged -= RbFeedback_CheckedChanged;

            if (rbFeedbackInch.Checked)
                _machine.FeedbackUnit = FeedbackUnit.Inch;
            else
                _machine.FeedbackUnit = FeedbackUnit.Mm;

            UpdateConfigurationChanged();
            UpdateDisplay();

            rbFeedbackInch.CheckedChanged += RbFeedback_CheckedChanged;
            rbFeedbackMm.CheckedChanged += RbFeedback_CheckedChanged;
        }

        #endregion Settings

        #region Jog

        private void jogControl_OnJogStart(JogDirection jogDirection, double stepSize, double feedRate)
        {
            _canCancelJog = stepSize == 0;
            SendMessage(String.Format(MessageMachineJogStart, _machine.Id, jogDirection, stepSize, feedRate));
        }

        private void jogControl_OnJogStop(object sender, EventArgs e)
        {
            machinePositionJog.Focus();

            if (_canCancelJog)
                StopJogging();
        }

        private void StopJogging()
        {
            SendByThread(String.Format(MessageMachineJogStop, _machine.Id));
        }

        private void jogControl_OnUpdate(object sender, EventArgs e)
        {
            _machine.JogUnits = jogControl.StepValue;
            _machine.JogFeedrate = jogControl.FeedRate;
            UpdateConfigurationChanged();
        }

        #endregion Jog

        #region Overrides

        private void SelectionOverrideRapids_ValueChanged(object sender, EventArgs e)
        {
            if (cbOverrideLinkRapids.Checked && !cbOverridesDisable.Checked)
            {
                _updatingRapidOverride = true;
                _machineUpdateThread.RapidsOverride = (RapidsOverride)selectionOverrideRapids.Value;
            }

            selectionOverrideRapids.LabelValue = HelperMethods.TranslateRapidOverride((RapidsOverride)selectionOverrideRapids.Value);
        }

        private void SelectionOverride_ValueChanged(object sender, EventArgs e)
        {
            _machineUpdateThread.Overrides.Spindle.NewValue = selectionOverrideSpindle.Value;
            _machineUpdateThread.Overrides.AxisXY.NewValue = selectionOverrideXY.Value;
            _machineUpdateThread.Overrides.AxisZUp.NewValue = selectionOverrideZUp.Value;
            _machineUpdateThread.Overrides.AxisZDown.NewValue = selectionOverrideZDown.Value;

            _machineUpdateThread.Overrides.OverrideSpindle = cbOverrideLinkSpindle.Checked;
            _machineUpdateThread.Overrides.OverrideXY = cbOverrideLinkXY.Checked;
            _machineUpdateThread.Overrides.OverrideZUp = cbOverrideLinkZUp.Checked;
            _machineUpdateThread.Overrides.OverrideZDown = cbOverrideLinkZDown.Checked;

            _machineUpdateThread.Overrides.OverridesEnabled = !cbOverridesDisable.Checked;

            _machineUpdateThread.OverridesUpdated();

            _machine.OverrideSpeed = trackBarPercent.Value;
            _machine.OverrideSpindle = selectionOverrideSpindle.Value;
            _machine.OverrideZDownSpeed = selectionOverrideZDown.Value;
            _machine.OverrideZUpSpeed = selectionOverrideZUp.Value;
            UpdateConfigurationChanged();
        }

        private void trackBarPercent_ValueChanged(object sender, EventArgs e)
        {
            UpdateOverrides();
            _machine.OverrideSpeed = trackBarPercent.Value;
            UpdateConfigurationChanged();
        }

        private void cbOverridesDisable_CheckedChanged(object sender, EventArgs e)
        {
            trackBarPercent.Enabled = !cbOverridesDisable.Checked;
            selectionOverrideSpindle.Enabled = !cbOverridesDisable.Checked;
            selectionOverrideRapids.Enabled = !cbOverridesDisable.Checked;
            selectionOverrideXY.Enabled = !cbOverridesDisable.Checked;
            selectionOverrideZDown.Enabled = !cbOverridesDisable.Checked;
            selectionOverrideZUp.Enabled = !cbOverridesDisable.Checked;
            cbOverrideLinkRapids.Enabled = !cbOverridesDisable.Checked;
            cbOverrideLinkXY.Enabled = !cbOverridesDisable.Checked;
            cbOverrideLinkZDown.Enabled = !cbOverridesDisable.Checked;
            cbOverrideLinkZUp.Enabled = !cbOverridesDisable.Checked;
            cbOverrideLinkSpindle.Enabled = !cbOverridesDisable.Checked;
        }

        private void selectionOverrideSpindle_ValueChanged(object sender, EventArgs e)
        {
            _machine.OverrideSpindle = selectionOverrideSpindle.Value;
            UpdateConfigurationChanged();
        }

        private void UpdateOverrides()
        {
            selectionOverrideXY.ValueChanged -= SelectionOverride_ValueChanged;

            selectionOverrideXY.Value = selectionOverrideXY.Maximum / 100 * trackBarPercent.Value;

            labelSpeedPercent.Text = String.Format(GSend.Language.Resources.SpeedPercent, trackBarPercent.Value);

            selectionOverrideXY.ValueChanged += SelectionOverride_ValueChanged;
        }

        private void OverrideAxis_Checked(object sender, EventArgs e)
        {
            trackBarPercent_ValueChanged(sender, e);

            if (cbOverrideLinkRapids.Checked)
                _machine.AddOptions(MachineOptions.OverrideRapids);
            else
                _machine.RemoveOptions(MachineOptions.OverrideRapids);

            if (cbOverrideLinkXY.Checked)
                _machine.AddOptions(MachineOptions.OverrideXY);
            else
                _machine.RemoveOptions(MachineOptions.OverrideXY);

            if (cbOverrideLinkZUp.Checked)
                _machine.AddOptions(MachineOptions.OverrideZUp);
            else
                _machine.RemoveOptions(MachineOptions.OverrideZUp);

            if (cbOverrideLinkZDown.Checked)
                _machine.AddOptions(MachineOptions.OverrideZDown);
            else
                _machine.RemoveOptions(MachineOptions.OverrideZDown);

            if (cbOverrideLinkSpindle.Checked)
                _machine.AddOptions(MachineOptions.OverrideSpindle);
            else
                _machine.RemoveOptions(MachineOptions.OverrideSpindle);

            _machine.OverrideZUpSpeed = selectionOverrideZUp.Value;
            _machine.OverrideZDownSpeed = selectionOverrideZDown.Value;
            _machine.OverrideSpindle = selectionOverrideSpindle.Value;

            UpdateConfigurationChanged();
        }

        #endregion Overrides

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
                    existingItems &= ~newValue;
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
                SendMessage(command);
            }

            SaveChanges(true);
            ConfigureMachine();
        }

        #endregion Grbl Settings

        #region Warnings and Error Handling

        private void LoadAllStatusChangeWarnings(MachineStateModel status)
        {
            ValidateLaserSpindleMode(status);

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

        private void ValidateLaserSpindleMode(MachineStateModel status)
        {
            if (_machine.Options.HasFlag(MachineOptions.AutoCorrectLaserSpindleMode))
            {
                switch (_machine.MachineType)
                {
                    case MachineType.CNC:
                        if (status.UpdatedGrblConfiguration.Any(s => s.DollarValue.Equals(32) && s.OldValue.Equals("0") && s.NewValue.Equals("1")))
                        {
                            warningsAndErrors.AddWarningPanel(InformationType.Information, GSend.Language.Resources.AutomaticallySelectedSpindleMode);
                            _machine.Settings.LaserModeEnabled = false;
                            SendMessage(String.Format(Constants.MessageMachineUpdateSetting, _machine.Id, "$32=0"));
                            SaveChanges(true);
                            ConfigureMachine();
                        }

                        break;

                    case MachineType.Laser:
                        if (status.UpdatedGrblConfiguration.Any(s => s.DollarValue.Equals(32) && s.OldValue.Equals("1") && s.NewValue.Equals("2")))
                        {
                            warningsAndErrors.AddWarningPanel(InformationType.Information, GSend.Language.Resources.AutomaticallySelectedLaserMode);
                            _machine.Settings.LaserModeEnabled = true;
                            SendMessage(String.Format(Constants.MessageMachineUpdateSetting, _machine.Id, "$32=1"));
                            SaveChanges(true);
                            ConfigureMachine();
                        }

                        break;
                }
            }
        }

        private void WarningContainer_VisibleChanged(object sender, EventArgs e)
        {
            if (warningsAndErrors.Visible)
            {
                tabControlMain.Top = warningsAndErrors.Top + warningsAndErrors.Height + 8;
                tabControlSecondary.Top = tabControlMain.Top + tabControlMain.Height + 8;
                tabControlSecondary.Height = statusStrip.Top - (tabControlSecondary.Top + 6);
            }
            else
            {
                tabControlMain.Top = 86;
                tabControlSecondary.Top = tabControlMain.Top + tabControlMain.Height + 8;
                tabControlSecondary.Height = statusStrip.Top - (tabControlSecondary.Top + 6);
            }
        }

        private void warningsAndErrors_OnUpdate(object sender, EventArgs e)
        {
            int width = 0;

            if (warningsAndErrors.WarningCount() > 0)
                width += WarningStatusWidth;

            if (warningsAndErrors.ErrorCount() > 0)
                width += WarningStatusWidth;
            
            if (warningsAndErrors.InformationCount() > 0)
                width += WarningStatusWidth;

            toolStripStatusLabelWarnings.Visible = warningsAndErrors.TotalCount() > 0;
            toolStripStatusLabelWarnings.Width = width;

            WarningContainer_VisibleChanged(sender, e);
            toolStripStatusLabelWarnings.Invalidate();
        }

        private void ProcessAlarmResponse(ClientBaseMessage clientMessage)
        {
            _isProbing = false;
            JsonElement element = (JsonElement)clientMessage.message;
            GrblAlarm alarm = (GrblAlarm)element.GetInt32();
            string alarmDescription = GSend.Language.Resources.ResourceManager.GetString($"Alarm{(int)alarm}");
            warningsAndErrors.AddWarningPanel(InformationType.Alarm, alarmDescription);
            UpdateEnabledState();
        }

        private void ProcessErrorResponse(ClientBaseMessage clientMessage)
        {
            _isProbing = false;
            JsonElement element = (JsonElement)clientMessage.message;
            GrblError error = (GrblError)element.GetInt32();
            string alarmDescription = GSend.Language.Resources.ResourceManager.GetString($"Error{(int)error}");
            warningsAndErrors.AddWarningPanel(InformationType.Error, alarmDescription);
            UpdateEnabledState();
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

            UpdateEnabledState();
        }

        #endregion Warnings and Error Handling

        #region Zeroing

        public void SetZeroForAxes(object sender, EventArgs e)
        {
            ZeroAxis zeroAxis = (ZeroAxis)((System.Windows.Forms.Button)sender).Tag;
            SendMessage(String.Format(MessageMachineSetZero, _machine.Id, (int)zeroAxis, 0));
            tabPageJog.Focus();
        }

        #endregion Zeroing

        #region Probing

        private void ProbingCommand1_OnSave(object sender, EventArgs e)
        {
            _machine.ProbeThickness = probingCommand1.Thickness;
            _machine.ProbeSpeed = probingCommand1.TravelSpeed;
            _machine.ProbeCommand = probingCommand1.ProbeCommand;

            UpdateConfigurationChanged();
            UpdateEnabledState();

            ConfigureMachine();
        }

        #endregion Probing

        #region Console

        private void btnGrblCommandClear_Click(object sender, EventArgs e)
        {
            textBoxConsoleText.Text = String.Empty;
            txtUserGrblCommand.Focus();
        }

        private void btnGrblCommandSend_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtUserGrblCommand.Text))
                return;

            AddMessageToConsole(txtUserGrblCommand.Text);

            string command = txtUserGrblCommand.Text.Trim();

            if (txtUserGrblCommand.Text.StartsWith("$") || txtUserGrblCommand.Text == "?")
                SendMessage(String.Format(Constants.MessageMachineWriteLineR, _machine.Id, command));
            else
                SendMessage(String.Format(Constants.MessageMachineWriteLine, _machine.Id, command));

            txtUserGrblCommand.Text = String.Empty;
            txtUserGrblCommand.Focus();
        }

        private void AddMessageToConsole(string message)
        {
            textBoxConsoleText.AppendText($"{message}\r\n");
            textBoxConsoleText.ScrollToCaret();
        }

        private void txtUserGrblCommand_TextChanged(object sender, EventArgs e)
        {
            btnGrblCommandSend.Enabled = txtUserGrblCommand.Text.Length > 0;
        }

        private void txtUserGrblCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGrblCommandSend_Click(sender, EventArgs.Empty);
            }
        }

        #endregion Console

        #region Spindle Control

        private void CbSpindleClockwise_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSpindleCounterClockwise.Checked)
                _machine.AddOptions(MachineOptions.SpindleCounterClockWise);
            else
                _machine.RemoveOptions(MachineOptions.SpindleCounterClockWise);

            UpdateConfigurationChanged();
        }

        private void cmbSpindleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _machine.SpindleType = (SpindleType)cmbSpindleType.SelectedItem;
            UpdateConfigurationChanged();
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

            UpdateConfigurationChanged();
        }

        private void cbSoftStart_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSoftStart.Checked)
                _machine.AddOptions(MachineOptions.SoftStart);
            else
                _machine.RemoveOptions(MachineOptions.SoftStart);

            UpdateConfigurationChanged();
        }

        private void trackBarSpindleSpeed_ValueChanged(object sender, EventArgs e)
        {
            lblSpindleSpeed.Text = String.Format(GSend.Language.Resources.SpeedRpm, trackBarSpindleSpeed.Value);
        }

        private void btnSpindleStart_Click(object sender, EventArgs e)
        {
            SendMessage(String.Format(MessageMachineSpindle, _machine.Id, trackBarSpindleSpeed.Value, cbSpindleCounterClockwise.Checked));
        }

        private void btnSpindleStop_Click(object sender, EventArgs e)
        {
            SendByThread(String.Format(MessageMachineSpindle, _machine.Id, 0, cbSpindleCounterClockwise.Checked));
        }

        #endregion Spindle Control

        #region Service

        private void btnServiceReset_Click(object sender, EventArgs e)
        {
            MachineApiWrapper machineApiWrapper = _gSendContext.ServiceProvider.GetRequiredService<MachineApiWrapper>();

            using (FrmRegisterService frmRegisterService = new FrmRegisterService(_machine.Id, machineApiWrapper))
            {
                if (frmRegisterService.ShowDialog(this) == DialogResult.OK)
                {
                    btnServiceRefresh_Click(sender, e);
                }
            }
        }

        private bool ServiceListViewItemExists(MachineServiceModel machineServiceModel)
        {
            foreach (ListViewItem listViewItem in lvServices.Items)
            {
                if (listViewItem.Tag is MachineServiceModel serviceMachineServiceModel)
                {
                    if (serviceMachineServiceModel.Id.Equals(machineServiceModel.Id))
                        return true;
                }
            }

            return false;
        }

        private void btnServiceRefresh_Click(object sender, EventArgs e)
        {
            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                MachineApiWrapper machineApiWrapper = _gSendContext.ServiceProvider.GetRequiredService<MachineApiWrapper>();

                List<MachineServiceModel> services = machineApiWrapper.MachineServices(_machine.Id);

                foreach (MachineServiceModel service in services.OrderBy(s => s.ServiceDate))
                {
                    if (ServiceListViewItemExists(service))
                        continue;

                    TimeSpan spanSpindleHours = new TimeSpan(service.SpindleHours);
                    ListViewItem serviceItem = new ListViewItem(service.ServiceDate.ToString(Thread.CurrentThread.CurrentUICulture.DateTimeFormat.FullDateTimePattern));
                    serviceItem.Tag = service;

                    string serviceType = GSend.Language.Resources.ServiceTypeDaily;

                    if (service.ServiceType.Equals(ServiceType.Major))
                        serviceType = GSend.Language.Resources.ServiceTypeMajor;
                    else if (service.ServiceType.Equals(ServiceType.Minor))
                        serviceType = GSend.Language.Resources.ServiceTypeMinor;

                    serviceItem.SubItems.Add(serviceType);
                    serviceItem.SubItems.Add($"{(int)spanSpindleHours.TotalHours} {GSend.Language.Resources.Hours} and {spanSpindleHours.Minutes} {GSend.Language.Resources.Minutes}");
                    lvServices.Items.Insert(0, serviceItem);
                }

                DateTime latestService = services.Max(s => s.ServiceDate);
                DateTime nextService = latestService.AddDays(_machine.ServiceWeeks * 7);
                TimeSpan span = nextService - DateTime.UtcNow;

                lblServiceDate.Text = $"{nextService:g} ({(int)span.TotalDays} days)";

                List<SpindleHoursModel> spindleHours = machineApiWrapper.GetSpindleTime(_machine.Id, latestService);

                long totalTicks = spindleHours.Where(tt => tt.TotalTime.Ticks > 0).Sum(sh => sh.TotalTime.Ticks);
                TimeSpan remaining = new TimeSpan((trackBarServiceSpindleHours.Value * TimeSpan.TicksPerHour) - totalTicks);
                lblSpindleHoursRemaining.Text = String.Format(GSend.Language.Resources.StatusServiceSpindleTime,
                    (int)remaining.TotalHours, remaining.Minutes);

                if (cbMaintainServiceSchedule.Checked)
                {
                    if ((span.TotalDays < 0 || remaining.TotalHours < 0))
                    {
                        if (!warningsAndErrors.Contains(InformationType.Warning, GSend.Language.Resources.ServiceOverdue))
                            warningsAndErrors.AddWarningPanel(InformationType.Warning, GSend.Language.Resources.ServiceOverdue);
                    }
                    else if ((span.TotalDays < 2 || remaining.TotalHours < 4) && !warningsAndErrors.Contains(InformationType.Information, GSend.Language.Resources.ServiceRequired))
                    {
                        warningsAndErrors.AddWarningPanel(InformationType.Information, GSend.Language.Resources.ServiceRequired);
                    }
                }
            }
        }

        #endregion Service

        #region Form Methods

        private void ConfigureMachine()
        {
            selectionOverrideSpindle.Maximum = (int)_machine.Settings.MaxSpindleSpeed;
            selectionOverrideSpindle.Minimum = (int)_machine.Settings.MinSpindleSpeed;
            selectionOverrideRapids.Maximum = 2;
            selectionOverrideRapids.Minimum = 0;
            selectionOverrideXY.Maximum = (int)_machine.Settings.MaxFeedRateY;
            selectionOverrideXY.Minimum = 0;
            selectionOverrideZDown.Maximum = (int)_machine.Settings.MaxFeedRateZ;
            selectionOverrideZDown.Minimum = 0;
            selectionOverrideZUp.Maximum = (int)_machine.Settings.MaxFeedRateZ;
            selectionOverrideZUp.Minimum = 0;

            cbOverrideLinkRapids.CheckedChanged -= OverrideAxis_Checked;
            cbOverrideLinkXY.CheckedChanged -= OverrideAxis_Checked;
            cbOverrideLinkZUp.CheckedChanged -= OverrideAxis_Checked;
            cbOverrideLinkZDown.CheckedChanged -= OverrideAxis_Checked;
            cbOverrideLinkSpindle.CheckedChanged -= OverrideAxis_Checked;

            cbOverrideLinkRapids.Checked = _machine.Options.HasFlag(MachineOptions.OverrideRapids);
            cbOverrideLinkXY.Checked = _machine.Options.HasFlag(MachineOptions.OverrideXY);
            cbOverrideLinkZUp.Checked = _machine.Options.HasFlag(MachineOptions.OverrideZUp);
            cbOverrideLinkZDown.Checked = _machine.Options.HasFlag(MachineOptions.OverrideZDown);

            cbOverrideLinkRapids.CheckedChanged += OverrideAxis_Checked;
            cbOverrideLinkXY.CheckedChanged += OverrideAxis_Checked;
            cbOverrideLinkZUp.CheckedChanged += OverrideAxis_Checked;
            cbOverrideLinkZDown.CheckedChanged += OverrideAxis_Checked;
            cbOverrideLinkSpindle.CheckedChanged += OverrideAxis_Checked;

            //trackBarPercent.ValueChanged -= trackBarPercent_ValueChanged;
            selectionOverrideRapids.ValueChanged -= SelectionOverrideRapids_ValueChanged;
            selectionOverrideXY.ValueChanged -= SelectionOverride_ValueChanged;
            selectionOverrideZDown.ValueChanged -= SelectionOverride_ValueChanged;
            selectionOverrideZUp.ValueChanged -= SelectionOverride_ValueChanged;
            selectionOverrideSpindle.ValueChanged -= SelectionOverride_ValueChanged;


            jogControl.FeedMaximum = (int)_machine.Settings.MaxFeedRateX;
            jogControl.FeedMinimum = 0;
            jogControl.FeedRate = jogControl.FeedMaximum / 2;
            jogControl.FeedMinimum = 0;
            jogControl.StepValue = 7;
            jogControl.FeedRate = _machine.JogFeedrate;
            trackBarPercent.Value = _machine.OverrideSpeed;
            selectionOverrideSpindle.Value = _machine.OverrideSpindle;
            selectionOverrideZDown.Value = _machine.OverrideZDownSpeed;
            selectionOverrideZUp.Value = _machine.OverrideZUpSpeed;
            cbSoftStart.Checked = _machine.SoftStart;
            trackBarDelaySpindle.Value = _machine.SoftStartSeconds;

            //trackBarPercent.ValueChanged += trackBarPercent_ValueChanged;
            selectionOverrideRapids.ValueChanged += SelectionOverrideRapids_ValueChanged;
            selectionOverrideXY.ValueChanged += SelectionOverride_ValueChanged;
            selectionOverrideZDown.ValueChanged += SelectionOverride_ValueChanged;
            selectionOverrideZUp.ValueChanged += SelectionOverride_ValueChanged;
            selectionOverrideSpindle.ValueChanged += SelectionOverride_ValueChanged;

            // spindle
            trackBarSpindleSpeed.Maximum = (int)_machine.Settings.MaxSpindleSpeed;
            trackBarSpindleSpeed.Minimum = (int)_machine.Settings.MinSpindleSpeed;
            //trackBarSpindleSpeed.TickFrequency = 
            trackBarSpindleSpeed.Value = trackBarSpindleSpeed.Maximum;
            cbSpindleCounterClockwise.Checked = _machine.Options.HasFlag(MachineOptions.SpindleCounterClockWise);
            cmbSpindleType.SelectedItem = _machine.SpindleType;

            // settings
            cbLimitSwitches.Checked = _machine.Options.HasFlag(MachineOptions.LimitSwitches);
            cbToolChanger.Checked = _machine.Options.HasFlag(MachineOptions.ToolChanger);
            cbToolChanger.Enabled = _machine.MachineType.Equals(MachineType.CNC);
            cbFloodCoolant.Checked = _machine.Options.HasFlag(MachineOptions.FloodCoolant);
            cbFloodCoolant.Enabled = _machine.MachineType.Equals(MachineType.CNC);
            cbMistCoolant.Checked = _machine.Options.HasFlag(MachineOptions.MistCoolant);
            cbMistCoolant.Enabled = _machine.MachineType.Equals(MachineType.CNC);
            cbCorrectMode.Checked = _machine.Options.HasFlag(MachineOptions.AutoCorrectLaserSpindleMode);
            cbCorrectMode.Enabled = _machine.MachineType.Equals(MachineType.CNC) || _machine.MachineType.Equals(MachineType.Laser);
            cbLayerHeightWarning.Checked = _machine.Options.HasFlag(MachineOptions.LayerHeightWarning);
            cbLayerHeightWarning.Enabled = true;
            numericLayerHeight.Value = _machine.LayerHeightWarning;
            cbAutoSelectFeedbackUnit.Checked = _machine.Options.HasFlag(MachineOptions.AutoUpdateDisplayFromFile);


            // service schedule
            cbMaintainServiceSchedule.Checked = _machine.Options.HasFlag(MachineOptions.ServiceSchedule);
            trackBarServiceWeeks.Value = _machine.ServiceWeeks;
            trackBarServiceSpindleHours.Value = _machine.ServiceSpindleHours;
            lblServiceSchedule.Text = String.Format(GSend.Language.Resources.ServiceWeeks, trackBarServiceWeeks.Value);
            lblSpindleHours.Text = String.Format(GSend.Language.Resources.ServiceSpindleHours, trackBarServiceSpindleHours.Value);
            btnServiceReset.Text = GSend.Language.Resources.AddService;
            lblNextService.Text = GSend.Language.Resources.NextService;
            columnServiceHeaderDateTime.Text = GSend.Language.Resources.ServiceDate;
            columnServiceHeaderServiceType.Text = GSend.Language.Resources.ServiceType;
            columnServiceHeaderSpindleHours.Text = GSend.Language.Resources.SpindleHours;

            trackBarServiceSpindleHours.Enabled = cbMaintainServiceSchedule.Checked;
            trackBarServiceWeeks.Enabled = cbMaintainServiceSchedule.Checked;
            lblSpindleHours.Enabled = cbMaintainServiceSchedule.Checked;
            lblServiceSchedule.Enabled = cbMaintainServiceSchedule.Checked;
            lblNextService.Enabled = cbMaintainServiceSchedule.Checked;
            lblServiceDate.Enabled = cbMaintainServiceSchedule.Checked;
            lblSpindleHoursRemaining.Enabled = cbMaintainServiceSchedule.Checked;
            btnServiceRefresh.Enabled = cbMaintainServiceSchedule.Checked;
            btnServiceReset.Enabled = cbMaintainServiceSchedule.Checked;
        }

        private void HookUpEvents()
        {
            probingCommand1.OnSave += ProbingCommand1_OnSave;
            cbSoftStart.CheckedChanged += new System.EventHandler(this.cbSoftStart_CheckedChanged);
            cbSpindleCounterClockwise.CheckedChanged += CbSpindleClockwise_CheckedChanged;
            trackBarDelaySpindle.ValueChanged += trackBarDelaySpindle_ValueChanged;

            if (_machine.SpindleType == SpindleType.Integrated)
                lblDelaySpindleStart.Text = String.Format(GSend.Language.Resources.SpindleSoftStartSeconds, trackBarDelaySpindle.Value);
            else
                lblDelaySpindleStart.Text = String.Format(GSend.Language.Resources.SpindleDelayStartVFD, trackBarDelaySpindle.Value);

            cbToolChanger.CheckedChanged += CbToolChanger_CheckedChanged;
            cbLimitSwitches.CheckedChanged += CbLimitSwitches_CheckedChanged;
            cbCorrectMode.CheckedChanged += CbCorrectMode_CheckedChanged;
            cbFloodCoolant.CheckedChanged += CbFloodCoolant_CheckedChanged;
            cbMistCoolant.CheckedChanged += CbMistCoolant_CheckedChanged;
            cbMaintainServiceSchedule.CheckedChanged += CbMaintainServiceSchedule_CheckedChanged;
            cbLayerHeightWarning.CheckedChanged += CbLayerHeightWarning_CheckedChanged;
            numericLayerHeight.ValueChanged += NumericLayerHeight_CheckedChanged;
            cbAutoSelectFeedbackUnit.CheckedChanged += CbAutoSelectFeedbackUnit_CheckedChanged;
            trackBarServiceWeeks.ValueChanged += TrackBarServiceWeeks_ValueChanged;
            trackBarServiceSpindleHours.ValueChanged += TrackBarServiceSpindleHours_ValueChanged;

            cbOverrideLinkRapids.CheckedChanged += OverrideAxis_Checked;
            cbOverrideLinkXY.CheckedChanged += OverrideAxis_Checked;
            cbOverrideLinkZDown.CheckedChanged += OverrideAxis_Checked;
            cbOverrideLinkZUp.CheckedChanged += OverrideAxis_Checked;
            cbOverrideLinkSpindle.CheckedChanged += OverrideAxis_Checked;
            cbOverridesDisable.CheckedChanged += OverrideAxis_Checked;

            cbOverrideLinkRapids.CheckedChanged += SelectionOverrideRapids_ValueChanged;
            cbOverrideLinkXY.CheckedChanged += SelectionOverride_ValueChanged;
            cbOverrideLinkZDown.CheckedChanged += SelectionOverride_ValueChanged;
            cbOverrideLinkZUp.CheckedChanged += SelectionOverride_ValueChanged;
            cbOverrideLinkSpindle.CheckedChanged += SelectionOverride_ValueChanged;
            cbOverridesDisable.CheckedChanged += SelectionOverride_ValueChanged;

            trackBarPercent.ValueChanged += SelectionOverride_ValueChanged;

            btnGrblCommandClear.Click += btnGrblCommandClear_Click;
            btnGrblCommandSend.Click += btnGrblCommandSend_Click;

            //menu
            generalToolStripMenuItem.Tag = tabPageMain;
            generalToolStripMenuItem.Click += SelectTabControlMainTab;
            overridesToolStripMenuItem.Tag = tabPageOverrides;
            overridesToolStripMenuItem.Click += SelectTabControlMainTab;
            jogToolStripMenuItem.Tag = tabPageJog;
            jogToolStripMenuItem.Click += SelectTabControlMainTab;
            spindleToolStripMenuItem.Tag = tabPageSpindle;
            spindleToolStripMenuItem.Click += SelectTabControlMainTab;
            serviceScheduleToolStripMenuItem.Tag = tabPageServiceSchedule;
            serviceScheduleToolStripMenuItem.Click += SelectTabControlMainTab;
            machineSettingsToolStripMenuItem.Tag = tabPageMachineSettings;
            machineSettingsToolStripMenuItem.Click += SelectTabControlMainTab;
            settingsToolStripMenuItem.Tag = tabPageSettings;
            settingsToolStripMenuItem.Click += SelectTabControlMainTab;
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
            toolStripStatusLabelSpindle.ToolTipText = GSend.Language.Resources.SpindleHint;
            toolStripStatusLabelFeedRate.ToolTipText = GSend.Language.Resources.CurrentFeedRate;
            toolStripDropDownButtonCoordinateSystem.ToolTipText = GSend.Language.Resources.CoordinateSystem;
            toolStripStatusLabelBuffer.ToolTipText = GSend.Language.Resources.AvailableBytesBlocks;
            toolStripStatusLabelWarnings.ToolTipText = GSend.Language.Resources.WarningsAndInformation;
            toolStripStatusLabelStatus.ToolTipText = GSend.Language.Resources.MachineStatus;


            //tab pages
            tabPageMain.Text = GSend.Language.Resources.General;
            tabPageOverrides.Text = GSend.Language.Resources.Overrides;
            tabPageServiceSchedule.Text = GSend.Language.Resources.ServiceSchedule;
            tabPageMachineSettings.Text = GSend.Language.Resources.GrblSettings;
            tabPageSpindle.Text = GSend.Language.Resources.Spindle;
            tabPageSettings.Text = GSend.Language.Resources.Settings;
            tabPageConsole.Text = GSend.Language.Resources.Console;
            selectionOverrideSpindle.LabelFormat = GSend.Language.Resources.OverrideRpm;

            //General tab


            //Spindle tab
            lblSpindleType.Text = GSend.Language.Resources.SpindleType;
            cbSoftStart.Text = GSend.Language.Resources.SpindleSoftStart;

            if (_machine.SpindleType == SpindleType.Integrated)
                lblDelaySpindleStart.Text = String.Format(GSend.Language.Resources.SpindleSoftStartSeconds, trackBarDelaySpindle.Value);
            else
                lblDelaySpindleStart.Text = String.Format(GSend.Language.Resources.SpindleDelayStartVFD, trackBarDelaySpindle.Value);

            btnSpindleStart.Text = GSend.Language.Resources.SpindleStart;
            btnSpindleStop.Text = GSend.Language.Resources.SpindleStop;
            grpBoxSpindleSpeed.Text = GSend.Language.Resources.SpindleControl;
            cbSpindleCounterClockwise.Text = GSend.Language.Resources.SpindleDirectionCounterClockwise;

            // settings tab
            cbLimitSwitches.Text = GSend.Language.Resources.MachineOptionLimitSwitches;
            cbToolChanger.Text = GSend.Language.Resources.MachineOptionToolChanger;
            cbFloodCoolant.Text = GSend.Language.Resources.FloodCoolant;
            cbMistCoolant.Text = GSend.Language.Resources.MistCoolant;
            cbCorrectMode.Text = _machine.MachineType == MachineType.Laser ? GSend.Language.Resources.AutoUpdateLaserMode : GSend.Language.Resources.AutoUpdateSpindleMode;
            cbLayerHeightWarning.Text = GSend.Language.Resources.WarnLayerHeight;
            grpDisplayUnits.Text = GSend.Language.Resources.DisplayUnits;
            rbFeedbackMm.Text = GSend.Language.Resources.DisplayMm;
            rbFeedbackInch.Text = GSend.Language.Resources.DisplayInch;
            cbAutoSelectFeedbackUnit.Text = GSend.Language.Resources.DisplayUpdateFromFile;
            grpFeedDisplay.Text = GSend.Language.Resources.FeedDisplay;
            rbFeedDisplayMmMin.Text = GSend.Language.Resources.DisplayMmMinute;
            rbFeedDisplayMmSec.Text = GSend.Language.Resources.DisplayMmSec;
            rbFeedDisplayInchMin.Text = GSend.Language.Resources.DisplayInchMinute;
            rbFeedDisplayInchSec.Text = GSend.Language.Resources.DisplayInchSecond;
            lblLayerHeightMeasure.Text = GSend.Language.Resources.DisplayMmAbreviated;

            // service schedule
            cbMaintainServiceSchedule.Text = GSend.Language.Resources.MaintainServiceSchedule;
            btnServiceRefresh.Text = GSend.Language.Resources.Refresh;
            columnServiceHeaderDateTime.Text = GSend.Language.Resources.ServiceDate;
            columnServiceHeaderServiceType.Text = GSend.Language.Resources.ServiceType;
            columnServiceHeaderSpindleHours.Text = GSend.Language.Resources.SpindleHours;

            // menu items
            machineToolStripMenuItem.Text = GSend.Language.Resources.Machine;
            viewToolStripMenuItem.Text = GSend.Language.Resources.View;



            // Override tab
            cbOverridesDisable.Text = GSend.Language.Resources.DisableOverrides;
            cbOverrideLinkRapids.Text = GSend.Language.Resources.Rapids;
            cbOverrideLinkXY.Text = GSend.Language.Resources.OverrideXY;
            cbOverrideLinkZUp.Text = GSend.Language.Resources.OverrideZUp;
            cbOverrideLinkZDown.Text = GSend.Language.Resources.OverrideZDown;
            cbOverrideLinkSpindle.Text = GSend.Language.Resources.Spindle;
            selectionOverrideRapids.LabelValue = GSend.Language.Resources.RapidRateHigh;

            // Console
            tabPageConsole.Text = GSend.Language.Resources.Console;
            btnGrblCommandSend.Text = GSend.Language.Resources.Send;
            btnGrblCommandClear.Text = GSend.Language.Resources.Clear;


            // menu
            openFileDialog1.Filter = GSend.Language.Resources.FileFilter;

            //Machine
            loadToolStripMenuItem.Text = GSend.Language.Resources.LoadGCode;
            clearToolStripMenuItem.Text = GSend.Language.Resources.ClearGCode;
            closeToolStripMenuItem.Text = GSend.Language.Resources.Close;

            //view
            generalToolStripMenuItem.Text = GSend.Language.Resources.General;
            overridesToolStripMenuItem.Text = GSend.Language.Resources.Overrides;
            jogToolStripMenuItem.Text = GSend.Language.Resources.Jog;
            spindleToolStripMenuItem.Text = GSend.Language.Resources.Spindle;
            serviceScheduleToolStripMenuItem.Text = GSend.Language.Resources.ServiceSchedule;
            machineSettingsToolStripMenuItem.Text = GSend.Language.Resources.MachineSettings;
            settingsToolStripMenuItem.Text = GSend.Language.Resources.Settings;
            consoleToolStripMenuItem.Text = GSend.Language.Resources.Console;

            // action
            saveConfigurationToolStripMenuItem.Text = GSend.Language.Resources.SaveConfiguration;
            connectToolStripMenuItem.Text = GSend.Language.Resources.Connect;
            disconnectToolStripMenuItem.Text = GSend.Language.Resources.Disconnect;
            clearAlarmToolStripMenuItem.Text = GSend.Language.Resources.ClearAlarm;
            homeToolStripMenuItem.Text = GSend.Language.Resources.Home;
            probeToolStripMenuItem.Text = GSend.Language.Resources.Probe;
            runToolStripMenuItem.Text = GSend.Language.Resources.Resume;
            pauseToolStripMenuItem.Text = GSend.Language.Resources.Pause;
            stopToolStripMenuItem.Text = GSend.Language.Resources.Stop;
        }

        private void UpdateDisplay()
        {
            if (InvokeRequired)
            {
                Invoke(UpdateDisplay);
                return;
            }

            rbFeedbackInch.CheckedChanged -= RbFeedback_CheckedChanged;
            rbFeedbackMm.CheckedChanged -= RbFeedback_CheckedChanged;

            switch (_machine.FeedbackUnit)
            {
                case FeedbackUnit.Mm:
                    rbFeedbackMm.Checked = true;
                    break;

                case FeedbackUnit.Inch:
                    rbFeedbackInch.Checked = true;
                    break;
            }

            machinePositionGeneral.DisplayFeedbackUnit = _machine.FeedbackUnit;
            machinePositionJog.DisplayFeedbackUnit = _machine.FeedbackUnit;
            machinePositionOverrides.DisplayFeedbackUnit = _machine.FeedbackUnit;
            selectionOverrideSpindle.TickFrequency = (int)_machine.Settings.MaxSpindleSpeed / 100;
            rbFeedbackInch.CheckedChanged += RbFeedback_CheckedChanged;
            rbFeedbackMm.CheckedChanged += RbFeedback_CheckedChanged;

            selectionOverrideSpindle.ValueChanged -= SelectionOverride_ValueChanged;
            selectionOverrideRapids.ValueChanged -= SelectionOverrideRapids_ValueChanged;
            selectionOverrideXY.ValueChanged -= SelectionOverride_ValueChanged;
            selectionOverrideZDown.ValueChanged -= SelectionOverride_ValueChanged;
            selectionOverrideZUp.ValueChanged -= SelectionOverride_ValueChanged;
            rbFeedDisplayInchMin.CheckedChanged -= RbFeedDisplay_CheckedChanged;
            rbFeedDisplayInchSec.CheckedChanged -= RbFeedDisplay_CheckedChanged;
            rbFeedDisplayMmMin.CheckedChanged -= RbFeedDisplay_CheckedChanged;
            rbFeedDisplayMmSec.CheckedChanged -= RbFeedDisplay_CheckedChanged;

            switch (_machine.DisplayUnits)
            {
                case FeedRateDisplayUnits.InchPerMinute:
                    rbFeedDisplayInchMin.Checked = true;
                    toolStripStatusLabelDisplayUnit.Text = GSend.Language.Resources.DisplayInchMinute;
                    break;

                case FeedRateDisplayUnits.InchPerSecond:
                    rbFeedDisplayInchSec.Checked = true;
                    toolStripStatusLabelDisplayUnit.Text = GSend.Language.Resources.DisplayInchSecond;
                    break;

                case FeedRateDisplayUnits.MmPerSecond:
                    rbFeedDisplayMmSec.Checked = true;
                    toolStripStatusLabelDisplayUnit.Text = GSend.Language.Resources.DisplayMmSec;
                    break;

                case FeedRateDisplayUnits.MmPerMinute:
                    rbFeedDisplayMmMin.Checked = true;
                    toolStripStatusLabelDisplayUnit.Text = GSend.Language.Resources.DisplayMmMinute;
                    break;
            }

            probingCommand1.FeedRateDisplay = _machine.DisplayUnits;
            probingCommand1.UpdateFeedRateDisplay();

            jogControl.FeedRateDisplay = _machine.DisplayUnits;
            jogControl.UpdateFeedRateDisplay();

            selectionOverrideXY.FeedRateDisplay = _machine.DisplayUnits;
            selectionOverrideXY.UpdateFeedRateDisplay();
            selectionOverrideZDown.FeedRateDisplay = _machine.DisplayUnits;
            selectionOverrideZDown.UpdateFeedRateDisplay();
            selectionOverrideZUp.FeedRateDisplay = _machine.DisplayUnits;
            selectionOverrideZUp.UpdateFeedRateDisplay();

            rbFeedDisplayInchMin.CheckedChanged += RbFeedDisplay_CheckedChanged;
            rbFeedDisplayInchSec.CheckedChanged += RbFeedDisplay_CheckedChanged;
            rbFeedDisplayMmMin.CheckedChanged += RbFeedDisplay_CheckedChanged;
            rbFeedDisplayMmSec.CheckedChanged += RbFeedDisplay_CheckedChanged;
            selectionOverrideSpindle.ValueChanged += SelectionOverride_ValueChanged;
            selectionOverrideRapids.ValueChanged += SelectionOverrideRapids_ValueChanged;
            selectionOverrideXY.ValueChanged += SelectionOverride_ValueChanged;
            selectionOverrideZDown.ValueChanged += SelectionOverride_ValueChanged;
            selectionOverrideZUp.ValueChanged += SelectionOverride_ValueChanged;
        }

        private void FrmMachine_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !_gSendContext.IsClosing;
            Hide();
        }

        private void UpdateConfigurationChanged()
        {
            _configurationChanges = true;
            UpdateEnabledState();
        }

        private void toolStripStatusLabelWarnings_Paint(object sender, PaintEventArgs e)
        {
            using (WarningPanel warningPanel = new WarningPanel())
            {
                e.Graphics.FillRectangle(new SolidBrush(toolStripStatusLabelWarnings.BackColor), e.ClipRectangle);
                int count = warningsAndErrors.ErrorCount();
                int leftPos = 2;

                if (count > 0)
                {
                    e.Graphics.DrawImage(warningPanel.GetImageForInformationType(InformationType.Error), new Point(leftPos, 1));
                    e.Graphics.DrawString($"{count}", toolStripStatusLabelWarnings.Font,
                        new SolidBrush(toolStripStatusLabelWarnings.ForeColor), new Point(leftPos + 18, 1));
                    leftPos += WarningStatusWidth;
                }

                count = warningsAndErrors.WarningCount();

                if (count > 0)
                {
                    e.Graphics.DrawImage(warningPanel.GetImageForInformationType(InformationType.Warning), new Point(leftPos, 1));
                    e.Graphics.DrawString($"{count}", toolStripStatusLabelWarnings.Font,
                        new SolidBrush(toolStripStatusLabelWarnings.ForeColor), new Point(leftPos + 18, 1));
                    leftPos += WarningStatusWidth;
                }

                count = warningsAndErrors.InformationCount();

                if (count > 0)
                {
                    e.Graphics.DrawImage(warningPanel.GetImageForInformationType(InformationType.Information), new Point(leftPos, 1));
                    e.Graphics.DrawString($"{count}", toolStripStatusLabelWarnings.Font,
                        new SolidBrush(toolStripStatusLabelWarnings.ForeColor), new Point(leftPos + 18, 1));
                }

                e.Graphics.DrawLine(_borderPen, e.ClipRectangle.Right -1, e.ClipRectangle.Top, e.ClipRectangle.Right -1, e.ClipRectangle.Bottom - 2);
            }

        }

        private void FrmMachine_Shown(object sender, EventArgs e)
        {
            warningsAndErrors.ResetLayoutWarningErrorSize();
        }

        #endregion Form Methods

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
            SendMessage(String.Format(MessageMachineConnect, _machine.Id));
        }

        private void toolStripButtonDisconnect_Click(object sender, EventArgs e)
        {
            SendMessage(String.Format(MessageMachineDisconnect, _machine.Id));
        }

        private void toolStripButtonClearAlarm_Click(object sender, EventArgs e)
        {
            SendMessage(String.Format(MessageMachineClearAlarm, _machine.Id));
            warningsAndErrors.ClearAlarm();
        }

        private void toolStripButtonHome_Click(object sender, EventArgs e)
        {
            SendMessage(String.Format(MessageMachineHome, _machine.Id));
        }

        private void toolStripButtonProbe_Click(object sender, EventArgs e)
        {
            _isProbing = true;
            SendMessage(String.Format(MessageMachineProbe, _machine.Id));
            UpdateEnabledState();
        }

        private void toolStripButtonResume_Click(object sender, EventArgs e)
        {
            SendMessage(String.Format(MessageMachineResume, _machine.Id));
        }

        private void toolStripButtonPause_Click(object sender, EventArgs e)
        {
            SendMessage(String.Format(MessageMachinePause, _machine.Id));
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            if (_isJogging)
                StopJogging();
            else
                SendByThread(String.Format(MessageMachineStop, _machine.Id));
        }

        private void ToolstripButtonCoordinates_Click(object sender, EventArgs e)
        {
            DeselectCoordinateMenuItems();

            ToolStripMenuItem selected = sender as ToolStripMenuItem;
            selected.Checked = true;
            toolStripDropDownButtonCoordinateSystem.Text = selected.Text;
            SendMessage(String.Format(Constants.MessageMachineWriteLineR, _machine.Id, selected.Text));
            SendMessage(String.Format(Constants.MessageMachineWriteLine, _machine.Id, "$G"));
        }

        #endregion Toolbar Buttons

        #region Menu

        private void SelectTabControlMainTab(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;

            if (sender == null)
                return;

            TabPage tabPage = menu.Tag as TabPage;

            if (tabPage == null)
                return;

            tabControlMain.SelectedTab = tabPage;
        }
        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlSecondary.SelectedTab = tabPageConsole;
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = String.Empty;

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                LoadGCode(openFileDialog1.FileName);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnloadGCode();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeselectCoordinateMenuItems()
        {
            g54ToolStripMenuItem.Checked = false;
            g55ToolStripMenuItem.Checked = false;
            g56ToolStripMenuItem.Checked = false;
            g57ToolStripMenuItem.Checked = false;
            g58ToolStripMenuItem.Checked = false;
            g59ToolStripMenuItem.Checked = false;
        }

        #endregion Menu

        #region G Code

        private void LoadGCode(string fileName)
        {
            UnloadGCode();

            try
            {
                GCodeParser gCodeParser = new GCodeParser();

                _gCodeAnalyses = gCodeParser.Parse(File.ReadAllText(fileName));
                _gCodeAnalyses.Analyse(fileName);

                if (cbAutoSelectFeedbackUnit.Checked && (int)_gCodeAnalyses.UnitOfMeasurement != (int)_machine.FeedbackUnit)
                {
                    warningsAndErrors.AddWarningPanel(InformationType.Warning,
                        String.Format(GSend.Language.Resources.UpdatingUnitOfMeasureFromGCode,
                        _gCodeAnalyses.UnitOfMeasurement,
                        _machine.FeedbackUnit));

                    _machine.FeedbackUnit = (FeedbackUnit)(int)_gCodeAnalyses.UnitOfMeasurement;

                    _machine.DisplayUnits = _gCodeAnalyses.UnitOfMeasurement == UnitOfMeasurement.Mm ? FeedRateDisplayUnits.MmPerMinute : FeedRateDisplayUnits.InchPerMinute;

                    UpdateDisplay();
                }

                switch (_gCodeAnalyses.UnitOfMeasurement)
                {
                    case UnitOfMeasurement.None:
                    case UnitOfMeasurement.Error:
                        warningsAndErrors.AddWarningPanel(InformationType.Error, GSend.Language.Resources.GCodeUnitOfMeasureError);
                        break;
                }

                if ((_gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.UsesMistCoolant) || _gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.UsesFloodCoolant)) && 
                    !_gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.TurnsOffCoolant))
                {
                    warningsAndErrors.AddWarningPanel(InformationType.Warning, GSend.Language.Resources.ErrorCoolantNotTurnedOff);
                }

                if (_gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.UsesMistCoolant) && !_machine.Options.HasFlag(MachineOptions.MistCoolant))
                {
                    warningsAndErrors.AddWarningPanel(InformationType.Warning, GSend.Language.Resources.WarningContainsMistCoolantOption);
                }

                if (_gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.UsesFloodCoolant) && !_machine.Options.HasFlag(MachineOptions.FloodCoolant))
                {
                    warningsAndErrors.AddWarningPanel(InformationType.Warning, GSend.Language.Resources.WarningContainsFloodCoolantOption);
                }

                if (_gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.ContainsToolChanges) && !_machine.Options.HasFlag(MachineOptions.ToolChanger))
                {
                    warningsAndErrors.AddWarningPanel(InformationType.Warning, GSend.Language.Resources.WarningContainsToolChangeOption);
                }

                if (_gCodeAnalyses.MaxLayerDepth > _machine.LayerHeightWarning && _machine.Options.HasFlag(MachineOptions.LayerHeightWarning))
                {
                    warningsAndErrors.AddWarningPanel(InformationType.Warning, String.Format(GSend.Language.Resources.WarningLayerHeightTooMuch,
                        _gCodeAnalyses.MaxLayerDepth, _machine.LayerHeightWarning));
                }

                if (_gCodeAnalyses.SubProgramCount > 0)
                {
                    foreach (IGCodeCommand item in _gCodeAnalyses.Commands.Where(c => c.Command.Equals('O')))
                    {
                        string subProgram = $"{item}";

                        if (!String.IsNullOrEmpty(item.Comment))
                            subProgram += $" {item.Comment}";

                        warningsAndErrors.AddWarningPanel(InformationType.Error, String.Format(GSend.Language.Resources.ErrorSubProgramMissing,
                            subProgram, item.LineNumber));
                    }
                }

                gCodeAnalysesDetails.LoadAnalyser(fileName, _gCodeAnalyses);
            }
            catch (Exception ex)
            {
                warningsAndErrors.AddWarningPanel(InformationType.Error, ex.Message);
            }

            UpdateEnabledState();
        }

        private void UnloadGCode()
        {
            if (_gCodeAnalyses != null)
                _gCodeAnalyses = null;


            gCodeAnalysesDetails.LoadAnalyser(_gCodeAnalyses);

            UpdateEnabledState();
        }

        #endregion G Code
    }
}
