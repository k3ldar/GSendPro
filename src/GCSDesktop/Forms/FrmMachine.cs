using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

using GSendApi;

using GSendCommon;

using GSendControls;

using GSendDesktop.Internal;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Attributes;
using GSendShared.Interfaces;
using GSendShared.Models;

using Microsoft.Extensions.DependencyInjection;

using Shared.Classes;

using static GSendShared.Constants;

//using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace GSendDesktop.Forms
{
    public partial class FrmMachine : BaseForm, IUiUpdate, IShortcutImplementation
    {
        #region Private Fields

        private readonly CancellationTokenRegistration _cancellationTokenRegistration;
        private readonly GSendWebSocket _clientWebSocket;
        private readonly IGSendContext _gSendContext;
        private readonly IMachine _machine;
        private readonly IServiceProvider _serviceProvider;
        private MachineStateModel _machineStatusModel = null;
        private MachineUpdateThread _machineUpdateThread;
        private IGCodeAnalyses _gCodeAnalyses = null;
        private readonly object _lockObject = new();
        private readonly Pen _borderPen = new(SystemColors.ButtonShadow);
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
        private bool _canConnectMachine = true;
        private int _totalLines;
        private List<IGCodeLine> _gcodeLines;
        private IToolProfile _toolProfile;
        private List<IShortcut> _shortcuts;
        private readonly ShortcutHandler _shortcutHandler;

        #endregion Private Fields

        #region Constructors

        public FrmMachine()
        {
            InitializeComponent();
            KeyPreview = true;

            _shortcutHandler = new()
            {
                RegisterKeyCombo = false
            };
            _shortcutHandler.OnKeyComboDown += ShortcutHandler_OnKeyComboDown;
            _shortcutHandler.OnKeyComboUp += ShortcutHandler_OnKeyComboUp;
        }

        public FrmMachine(IGSendContext gSendContext, IMachine machine, IServiceProvider serviceProvider)
            : this()
        {
            _gSendContext = gSendContext ?? throw new ArgumentNullException(nameof(gSendContext));
            _machine = machine ?? throw new ArgumentNullException(nameof(machine));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            Text = String.Format(GSend.Language.Resources.MachineTitle, machine.MachineType, machine.Name);

            if (machine.MachineType == MachineType.Printer)
                tabControlMain.TabPages.Remove(tabPageOverrides);

            _cancellationTokenRegistration = new();
            ApiSettings apiSettings = gSendContext.ServiceProvider.GetRequiredService<ApiSettings>();
            _clientWebSocket = new GSendWebSocket(apiSettings.RootAddress, _machine.Name, _cancellationTokenRegistration.Token);
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
            lblTotalLines.Text = String.Empty;
            toolStripProgressBarJob.Visible = false;

            btnZeroX.Tag = ZeroAxis.X;
            btnZeroY.Tag = ZeroAxis.Y;
            btnZeroZ.Tag = ZeroAxis.Z;
            btnZeroAll.Tag = ZeroAxis.All;

            tabControlMain.SelectedTab = tabPageMain;

            cmbSpindleType.Items.Add(SpindleType.Integrated);
            cmbSpindleType.Items.Add(SpindleType.VFD);
            cmbSpindleType.Items.Add(SpindleType.External);

            UpdateDisplay();
            probingCommand1.InitializeProbingCommand(_machine.ProbeCommand, _machine.ProbeSpeed, _machine.ProbeThickness);

            ConfigureMachine();
            toolStripStatusLabelSpindle.Visible = false;
            toolStripStatusLabelFeedRate.Visible = false;
            toolStripStatusLabelStatus.Visible = false;
            warningsAndErrors_OnUpdate(this, EventArgs.Empty);
            _configurationChanges = false;
            HookUpEvents();
            UpdateMachineStatus(new MachineStateModel());

            WarningContainer_VisibleChanged(this, EventArgs.Empty);
            tabControlSecondary.TabPages.Remove(tabPageGCode);

            if (_machine.MachineType != MachineType.CNC)
            {
                tabControlMain.TabPages.Remove(tabPageSpindle);
            }

            _shortcuts = RetrieveAvailableShortcuts();
        }

        #endregion Constructors

        #region Client Web Socket

        private void SendMessage(string message)
        {
            _clientWebSocket.SendAsync(message).ConfigureAwait(false);
        }

        private void ClientWebSocket_Connected(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => ClientWebSocket_Connected(sender, e));
                return;
            }

            _machineUpdateThread.IsThreadRunning = true;
            SendMessage(String.Format("mAddEvents:{0}", _machine.Id));
            toolStripStatusLabelServerConnect.Text = GSend.Language.Resources.ServerConnected;
            UpdateEnabledState();
            btnServiceRefresh_Click(sender, e);
        }

        private void ClientWebSocket_ConnectionLost(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => ClientWebSocket_ConnectionLost(sender, e));
                return;
            }

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

            //Trace.WriteLine($"Client: {message}");

            ClientBaseMessage clientMessage = null;
            try
            {
                clientMessage = JsonSerializer.Deserialize<ClientBaseMessage>(message, Constants.DefaultJsonSerializerOptions);
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
                case "Stop":
                    if (_machineStatusModel.MachineStateOptions.HasFlag(MachineStateOptions.SimulationMode))
                        SendMessage(String.Format(Constants.MessageToggleSimulation, _machine.Id));

                    break;

                case Constants.ComPortTimeOut:
                    warningsAndErrors.AddWarningPanel(InformationType.Warning, GSend.Language.Resources.ServerErrorComTimeOut);
                    break;

                case Constants.InvalidComPort:
                    warningsAndErrors.AddWarningPanel(InformationType.ErrorKeep, GSend.Language.Resources.ServerErrorInvalidComPort);
                    break;

                case Constants.MessageMachineProbeServer:
                    _isProbing = false;
                    break;

                case Constants.MessageMachineStatusServer:
                    _machineStatusModel = JsonSerializer.Deserialize<MachineStateModel>(clientMessage.message.ToString(), Constants.DefaultJsonSerializerOptions);

                    if (_machineStatusModel.IsConnected != clientMessage.IsConnected)
                        _machineStatusModel.IsConnected = clientMessage.IsConnected;

                    UpdateMachineStatus(_machineStatusModel);
                    UpdateEnabledState();
                    break;

                case Constants.StateChanged:
                    JsonElement element = (JsonElement)clientMessage.message;
                    _machineStatusModel = element.Deserialize<MachineStateModel>();

                    if (_machineStatusModel != null)
                    {
                        UpdateMachineStatus(_machineStatusModel);

                        if (_machineConnected != _machineStatusModel.IsConnected)
                        {
                            _machineConnected = _machineStatusModel.IsConnected;
                            UpdateEnabledState();
                        }
                    }

                    break;

                case "Connect":
                    _machineConnected = true;
                    UpdateEnabledState();
                    ConfigureMachine();
                    _configurationChanges = false;
                    toolStripStatusLabelFeedRate.Visible = true;
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
                        // not used in this context
                    }
                    else
                    {
                        AddMessageToConsole(clientMessage.message.ToString());
                    }

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

                case Constants.MessageLoadGCodeAdmin:

                    break;

                case Constants.MessageMachineConnectServer:
                    ConnectResult connectResponse = (ConnectResult)JsonSerializer.Deserialize<int>(clientMessage.message.ToString(), Constants.DefaultJsonSerializerOptions);

                    switch (connectResponse)
                    {
                        case ConnectResult.TimeOut:
                            warningsAndErrors.AddWarningPanel(InformationType.Error, GSend.Language.Resources.TimeoutConnectingToMachine);
                            break;

                        case ConnectResult.Error:
                            warningsAndErrors.AddWarningPanel(InformationType.Error, GSend.Language.Resources.ErrorConnectingToMachine);
                            break;
                    }

                    break;

                case Constants.MessageLineStatusUpdated:
                    LineStatusUpdateModel lineStatusUpdateModel = (LineStatusUpdateModel)JsonSerializer.Deserialize<LineStatusUpdateModel>(clientMessage.message.ToString(), Constants.DefaultJsonSerializerOptions);

                    if (lineStatusUpdateModel != null)
                    {
                        if (lineStatusUpdateModel.LineNumber == -1 && lineStatusUpdateModel.Status.Equals(LineStatus.Undefined))
                        {
                            _gcodeLines.ForEach(gcl => gcl.Status = LineStatus.Undefined);
                        }
                        else
                        {
                            IGCodeLine updateLine = _gcodeLines.FirstOrDefault(gcl => gcl.LineNumber.Equals(lineStatusUpdateModel.LineNumber) && gcl.MasterLineNumber.Equals(lineStatusUpdateModel.MasterLineNumber));

                            if (updateLine != null)
                            {
                                updateLine.Status = lineStatusUpdateModel.Status;
                            }
                        }

                        listViewGCode.Invalidate();
                    }

                    break;

                case Constants.MessageInformationUpdate:
                    InformationMessageModel informationMessageModel = (InformationMessageModel)JsonSerializer.Deserialize<InformationMessageModel>(clientMessage.message.ToString(), Constants.DefaultJsonSerializerOptions);

                    if (informationMessageModel != null)
                    {
                        if (_isRunning || informationMessageModel.InformationType != InformationType.Information)
                            warningsAndErrors.AddWarningPanel(informationMessageModel.InformationType, informationMessageModel.Message);
                        else
                            AddMessageToConsole($"{informationMessageModel.Message}");
                    }

                    break;

                case Constants.MessageConfigurationUpdated:
                    ConfigurationUpdatedMessage updateMessage = (ConfigurationUpdatedMessage)JsonSerializer.Deserialize<ConfigurationUpdatedMessage>(clientMessage.message.ToString(), Constants.DefaultJsonSerializerOptions);

                    if (_machine.Name != updateMessage.Name)
                    {
                        _machine.Name = updateMessage.Name;
                        Text = String.Format(GSend.Language.Resources.MachineTitle, _machine.MachineType, _machine.Name);
                    }

                    if (_machine.ComPort != updateMessage.Comport || _machine.MachineFirmware != updateMessage.MachineFirmware || _machine.MachineType != updateMessage.MachineType)
                    {
                        _canConnectMachine = false;
                        warningsAndErrors.AddWarningPanel(InformationType.ErrorKeep, GSend.Language.Resources.ConfigurationUpdatedRestart);
                        UpdateEnabledState();
                    }

                    break;
            }
        }

        protected override void UpdateEnabledState()
        {
            bool isConnected = (_machineConnected && _machineStatusModel?.IsConnected == true);
            toolStripButtonSave.Enabled = _configurationChanges;
            mnuActionSaveConfig.Enabled = _configurationChanges;

            toolStripButtonConnect.Enabled = _canConnectMachine && !_machineConnected;
            mnuActionConnect.Enabled = toolStripButtonConnect.Enabled;

            toolStripButtonDisconnect.Enabled = _machineConnected && !_isJogging && !_isProbing;
            mnuActionDisconnect.Enabled = toolStripButtonDisconnect.Enabled;

            toolStripButtonClearAlarm.Enabled = _machineConnected && _isAlarm;
            mnuActionClearAlarm.Enabled = toolStripButtonClearAlarm.Enabled;

            toolStripButtonHome.Enabled = isConnected && !_isAlarm && !_isJogging && !_isPaused && !_isRunning && !_isProbing;
            mnuActionHome.Enabled = toolStripButtonHome.Enabled;

            toolStripButtonProbe.Enabled = isConnected && !_isAlarm && !_isJogging && !_isPaused && !_isRunning && !_isProbing;
            mnuActionProbe.Enabled = toolStripButtonProbe.Enabled;

            toolStripButtonResume.Enabled = _isPaused || !_isRunning && isConnected && (_isPaused || _machineStatusModel?.TotalLines > 0);
            mnuActionRun.Enabled = toolStripButtonResume.Enabled;
            mnuMachineRename.Enabled = !_isRunning;

            toolStripButtonPause.Enabled = !_isPaused && _machineConnected && (_isPaused || _isRunning);
            mnuActionPause.Enabled = toolStripButtonPause.Enabled;

            toolStripButtonStop.Enabled = _machineConnected && !_isProbing && (_isRunning || _isJogging || _isPaused);
            mnuActionStop.Enabled = toolStripButtonStop.Enabled;

            toolStripDropDownButtonCoordinateSystem.Enabled = !_isRunning && isConnected && !_isProbing && (!_isRunning || !_isJogging || !_isPaused);

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

            mnuMachineLoadGCode.Enabled = _machineStatusModel?.IsRunning == false;
            mnuMachineClearGCode.Enabled = _machineStatusModel?.IsRunning == false && _gCodeAnalyses != null;

            tabPageServiceSchedule.Enabled = _machineConnected && _machineStatusModel?.IsRunning == false;
            tabPageSpindle.Enabled = _machineConnected && _machineStatusModel?.IsRunning == false;
        }

        private void UpdateMachineStatus(MachineStateModel status)
        {
            if (InvokeRequired)
            {
                Invoke(UpdateMachineStatus, status);
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

                //if (!_updatingRapidOverride && selectionOverrideRapids.Value != (int)status.RapidSpeed)
                //{
                //    selectionOverrideRapids.ValueChanged -= SelectionOverrideRapids_ValueChanged;
                //    selectionOverrideRapids.Value = (int)status.RapidSpeed;
                //    selectionOverrideRapids.LabelValue = HelperMethods.TranslateRapidOverride(status.RapidSpeed);
                //    selectionOverrideRapids.ValueChanged += SelectionOverrideRapids_ValueChanged;
                //}

                if (status.IsConnected)
                {
                    machine2dView1.XPosition = (float)status.WorkX;
                    machine2dView1.YPosition = (float)status.WorkY;

                    UpdateLabelText(lblBufferSize, String.Format(GSend.Language.Resources.BufferSize, status.BufferSize));
                    UpdateLabelText(lblQueueSize, String.Format(GSend.Language.Resources.QueueSize, status.QueueSize));
                    UpdateLabelText(lblCommandQueueSize, String.Format(GSend.Language.Resources.CommandQueueSize, status.CommandQueueSize));

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
                    toolStripProgressBarJob.Visible = status == null ? false : status.IsConnected && status.IsRunning;

                    toolStripProgressBarJob.Value = status.LineNumber;

                    UpdateLabelText(lblJobTime, String.Format(GSend.Language.Resources.TotalJobTime, GSendShared.HelperMethods.TimeSpanToTime(status.JobTime)));


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
                    toolStripStatusLabelStatus.Text = String.Empty;
                    UpdateLabelText(lblJobTime, String.Format(GSend.Language.Resources.TotalJobTime, "-"));
                    UpdateMachineStatusLabel(SystemColors.Control, SystemColors.ControlText, String.Empty);
                    machinePositionGeneral.ResetPositions();
                    machinePositionJog.ResetPositions();
                    machinePositionOverrides.ResetPositions();
                    warningsAndErrors.Visible = warningsAndErrors.TotalCount() > 0;
                    _isRunning = false;
                    _isJogging = false;
                    _isPaused = false;
                    _isAlarm = false;
                    toolStripProgressBarJob.Visible = false;
                    UpdateLabelText(lblBufferSize, String.Format(GSend.Language.Resources.BufferSize, 0));
                    UpdateLabelText(lblQueueSize, String.Format(GSend.Language.Resources.QueueSize, 0));
                    UpdateLabelText(lblCommandQueueSize, String.Format(GSend.Language.Resources.CommandQueueSize, 0));
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

        private static void UpdateLabelText(Label label, string text)
        {
            if (label.Text != text)
                label.Text = text;
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

        protected override string SectionName => $"{nameof(FrmMachine)}{_machine.Name}";

        protected override void SaveSettings()
        {
            base.SaveSettings();
            SaveSettings(tabControlMain);
            SaveSettings(tabControlSecondary);
            SaveSettings(lvServices);
            SaveSettings(gCodeAnalysesDetails.listViewAnalyses);
            SaveSettings(listViewGCode);
        }

        protected override void LoadSettings()
        {
            base.LoadSettings();
            LoadSettings(tabControlMain);
            LoadSettings(tabControlSecondary);
            LoadSettings(lvServices);
            LoadSettings(gCodeAnalysesDetails.listViewAnalyses);
            LoadSettings(listViewGCode);
        }

        private void SelectionOverrideRapids_ValueChanged(object sender, EventArgs e)
        {
            if (selectionOverrideRapids.Checked)
            {
                //_updatingRapidOverride = true;
                _machineUpdateThread.Overrides.Rapids = (RapidsOverride)selectionOverrideRapids.Value;
            }
            else
            {
                _machineUpdateThread.Overrides.Rapids = RapidsOverride.High;
            }

            _machineUpdateThread.Overrides.OverrideRapids = selectionOverrideRapids.Checked;
            selectionOverrideRapids.LabelValue = HelperMethods.TranslateRapidOverride((RapidsOverride)selectionOverrideRapids.Value);
        }

        private void SelectionOverride_ValueChanged(object sender, EventArgs e)
        {
            _machineUpdateThread.Overrides.Spindle.NewValue = selectionOverrideSpindle.Value;
            _machineUpdateThread.Overrides.AxisXY.NewValue = selectionOverrideXY.Value;
            _machineUpdateThread.Overrides.AxisZUp.NewValue = selectionOverrideZUp.Value;
            _machineUpdateThread.Overrides.AxisZDown.NewValue = selectionOverrideZDown.Value;

            _machineUpdateThread.Overrides.OverrideSpindle = selectionOverrideSpindle.Checked;
            _machineUpdateThread.Overrides.OverrideXY = selectionOverrideXY.Checked;
            _machineUpdateThread.Overrides.OverrideZUp = selectionOverrideZUp.Checked;
            _machineUpdateThread.Overrides.OverrideZDown = selectionOverrideZDown.Checked;

            _machineUpdateThread.Overrides.OverridesDisabled = cbOverridesDisable.Checked;

            _machineUpdateThread.OverridesUpdated();

            _machine.OverrideSpeed = trackBarPercent.Value;
            _machine.OverrideSpindle = selectionOverrideSpindle.Value;
            _machine.OverrideZDownSpeed = selectionOverrideZDown.Value;
            _machine.OverrideZUpSpeed = selectionOverrideZUp.Value;
            UpdateConfigurationChanged();
        }

        private void trackBarPercent_ValueChanged(object sender, EventArgs e)
        {
            _machine.OverrideSpeed = trackBarPercent.Value;
            UpdateOverrides();
            UpdateConfigurationChanged();
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
            if (!_machineStatusModel.IsRunning)
                trackBarPercent_ValueChanged(sender, e);

            if (selectionOverrideRapids.Checked)
                _machine.AddOptions(MachineOptions.OverrideRapids);
            else
                _machine.RemoveOptions(MachineOptions.OverrideRapids);

            if (selectionOverrideXY.Checked)
                _machine.AddOptions(MachineOptions.OverrideXY);
            else
                _machine.RemoveOptions(MachineOptions.OverrideXY);

            if (selectionOverrideZUp.Checked)
                _machine.AddOptions(MachineOptions.OverrideZUp);
            else
                _machine.RemoveOptions(MachineOptions.OverrideZUp);

            if (selectionOverrideZDown.Checked)
                _machine.AddOptions(MachineOptions.OverrideZDown);
            else
                _machine.RemoveOptions(MachineOptions.OverrideZDown);

            if (selectionOverrideSpindle.Checked)
                _machine.AddOptions(MachineOptions.OverrideSpindle);
            else
                _machine.RemoveOptions(MachineOptions.OverrideSpindle);

            _machine.OverrideZUpSpeed = selectionOverrideZUp.Value;
            _machine.OverrideZDownSpeed = selectionOverrideZDown.Value;
            _machine.OverrideSpindle = selectionOverrideSpindle.Value;

            UpdateConfigurationChanged();
        }

        protected override void WndProc(ref Message m)
        {
            //_shortcutHandler.WndProc(ref m);

            base.WndProc(ref m);
        }

        protected override void DefWndProc(ref Message m)
        {
            base.DefWndProc(ref m);
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
            txtGrblUpdates.Text = String.Empty;

            foreach (ChangedGrblSettings changedGrblSetting in status.UpdatedGrblConfiguration)
            {
                string setting = String.Format(GSend.Language.Resources.GrblValueUpdated,
                    changedGrblSetting.DollarValue,
                    changedGrblSetting.OldValue,
                    changedGrblSetting.NewValue,
                    changedGrblSetting.PropertyName);

                PropertyInfo propertyInfo = _machine.Settings.GetType().GetProperty(changedGrblSetting.PropertyName);
                propertyInfo.SetValue(_machine.Settings, Convert.ChangeType(changedGrblSetting.NewValue, propertyInfo.PropertyType));
                txtGrblUpdates.Text += $"${changedGrblSetting.DollarValue}={changedGrblSetting.OldValue}\r\n";
                warningsAndErrors.AddWarningPanel(InformationType.Warning, setting);
            }

            _appliedSettingsChanged = true;
            SaveChanges(true);
            ConfigureMachine();
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
                width += GSendShared.Constants.WarningStatusWidth;

            if (warningsAndErrors.ErrorCount() > 0)
                width += GSendShared.Constants.WarningStatusWidth;

            if (warningsAndErrors.InformationCount() > 0)
                width += GSendShared.Constants.WarningStatusWidth;

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
            if (_machineStatusModel.MachineState == MachineState.Alarm)
                return;

            _isProbing = false;
            JsonElement element = (JsonElement)clientMessage.message;
            GrblError error = (GrblError)element.GetInt32();
            string alarmDescription = GSend.Language.Resources.ResourceManager.GetString($"Error{(int)error}");

            if (!warningsAndErrors.Contains(InformationType.Error, alarmDescription))
            {
                warningsAndErrors.AddWarningPanel(InformationType.Error, alarmDescription);
            }

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
            ZeroAxis zeroAxis = (ZeroAxis)((Button)sender).Tag;
            SendMessage(String.Format(MessageMachineSetZero, _machine.Id, (int)zeroAxis, GetCoordinateSystemForZero()));
            tabPageJog.Focus();
        }

        private int GetCoordinateSystemForZero()
        {
            switch (_machineStatusModel.CoordinateSystem)
            {
                case CoordinateSystem.G54:
                    return 1;

                case CoordinateSystem.G55:
                    return 2;

                case CoordinateSystem.G56:
                    return 3;

                case CoordinateSystem.G57:
                    return 4;

                case CoordinateSystem.G58:
                    return 5;

                case CoordinateSystem.G59:
                    return 6;

                default:
                    return 0;
            }
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

            string command = txtUserGrblCommand.Text.Replace(":", "\t").Trim();

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

            if (_machineStatusModel?.SpindleSpeed > 0)
                btnSpindleStart_Click(sender, e);
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
            IGSendApiWrapper machineApiWrapper = _gSendContext.ServiceProvider.GetRequiredService<IGSendApiWrapper>();

            using (FrmRegisterService frmRegisterService = new(_machine.Id, machineApiWrapper))
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
                if (listViewItem.Tag is MachineServiceModel serviceMachineServiceModel &&
                    serviceMachineServiceModel.Id.Equals(machineServiceModel.Id))
                {
                    return true;
                }
            }

            return false;
        }

        private void btnServiceRefresh_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => btnServiceRefresh_Click(sender, e));
                return;
            }

            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                IGSendApiWrapper machineApiWrapper = _gSendContext.ServiceProvider.GetRequiredService<IGSendApiWrapper>();

                List<MachineServiceModel> services = machineApiWrapper.MachineServices(_machine.Id);

                foreach (MachineServiceModel service in services.OrderBy(s => s.ServiceDate))
                {
                    if (ServiceListViewItemExists(service))
                        continue;

                    TimeSpan spanSpindleHours = new(service.SpindleHours);
                    ListViewItem serviceItem = new(service.ServiceDate.ToString(Thread.CurrentThread.CurrentUICulture.DateTimeFormat.FullDateTimePattern))
                    {
                        Tag = service
                    };

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
                TimeSpan remaining = new((trackBarServiceSpindleHours.Value * TimeSpan.TicksPerHour) - totalTicks);
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
            toolStripStatusLabelSpindle.Visible = _machine.MachineType == MachineType.CNC;

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

            selectionOverrideRapids.EnabledChanged -= OverrideAxis_Checked;
            selectionOverrideXY.EnabledChanged -= OverrideAxis_Checked;
            selectionOverrideZUp.EnabledChanged -= OverrideAxis_Checked;
            selectionOverrideZDown.EnabledChanged -= OverrideAxis_Checked;
            selectionOverrideSpindle.EnabledChanged -= OverrideAxis_Checked;

            selectionOverrideRapids.Checked = _machine.Options.HasFlag(MachineOptions.OverrideRapids);
            selectionOverrideXY.Checked = _machine.Options.HasFlag(MachineOptions.OverrideXY);
            selectionOverrideZUp.Checked = _machine.Options.HasFlag(MachineOptions.OverrideZUp);
            selectionOverrideZDown.Checked = _machine.Options.HasFlag(MachineOptions.OverrideZDown);

            selectionOverrideRapids.EnabledChanged += OverrideAxis_Checked;
            selectionOverrideXY.EnabledChanged += OverrideAxis_Checked;
            selectionOverrideZUp.EnabledChanged += OverrideAxis_Checked;
            selectionOverrideZDown.EnabledChanged += OverrideAxis_Checked;
            selectionOverrideSpindle.EnabledChanged += OverrideAxis_Checked;

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
            trackBarPercent.Value = Shared.Utilities.CheckMinMax(_machine.OverrideSpeed, trackBarPercent.Minimum, trackBarPercent.Maximum);
            selectionOverrideSpindle.Value = Shared.Utilities.CheckMinMax(_machine.OverrideSpindle, selectionOverrideSpindle.Minimum, selectionOverrideSpindle.Maximum);
            selectionOverrideZDown.Value = Shared.Utilities.CheckMinMax(_machine.OverrideZDownSpeed, selectionOverrideZDown.Minimum, selectionOverrideZDown.Maximum);
            selectionOverrideZUp.Value = Shared.Utilities.CheckMinMax(_machine.OverrideZUpSpeed, selectionOverrideZUp.Minimum, selectionOverrideZUp.Maximum);
            cbSoftStart.Checked = _machine.SoftStart;
            trackBarDelaySpindle.Value = Shared.Utilities.CheckMinMax(_machine.SoftStartSeconds, trackBarDelaySpindle.Minimum, trackBarDelaySpindle.Maximum);

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
            numericLayerHeight.Value = Shared.Utilities.CheckMinMax(_machine.LayerHeightWarning, numericLayerHeight.Minimum, numericLayerHeight.Maximum);
            cbAutoSelectFeedbackUnit.Checked = _machine.Options.HasFlag(MachineOptions.AutoUpdateDisplayFromFile);


            // service schedule
            cbMaintainServiceSchedule.Checked = _machine.Options.HasFlag(MachineOptions.ServiceSchedule);
            trackBarServiceWeeks.Value = Shared.Utilities.CheckMinMax(_machine.ServiceWeeks, trackBarServiceWeeks.Minimum, trackBarServiceWeeks.Maximum);
            trackBarServiceSpindleHours.Value = Shared.Utilities.CheckMinMax(_machine.ServiceSpindleHours, trackBarServiceSpindleHours.Minimum, trackBarServiceSpindleHours.Maximum);
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

            // 2d view
            machine2dView1.MachineSize = new Rectangle(0, 0, (int)_machine.Settings.MaxTravelX, (int)_machine.Settings.MaxTravelY);
            machine2dView1.Configuration = _machine.Settings.HomingCycleDirection;
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

            selectionOverrideRapids.EnabledChanged += OverrideAxis_Checked;
            selectionOverrideXY.EnabledChanged += OverrideAxis_Checked;
            selectionOverrideZDown.EnabledChanged += OverrideAxis_Checked;
            selectionOverrideZUp.EnabledChanged += OverrideAxis_Checked;
            selectionOverrideSpindle.EnabledChanged += OverrideAxis_Checked;
            cbOverridesDisable.CheckedChanged += OverrideAxis_Checked;

            selectionOverrideRapids.EnabledChanged += SelectionOverrideRapids_ValueChanged;
            selectionOverrideXY.EnabledChanged += SelectionOverride_ValueChanged;
            selectionOverrideZDown.EnabledChanged += SelectionOverride_ValueChanged;
            selectionOverrideZUp.EnabledChanged += SelectionOverride_ValueChanged;
            selectionOverrideSpindle.EnabledChanged += SelectionOverride_ValueChanged;
            cbOverridesDisable.CheckedChanged += SelectionOverride_ValueChanged;

            trackBarPercent.ValueChanged += SelectionOverride_ValueChanged;

            btnGrblCommandClear.Click += btnGrblCommandClear_Click;
            btnGrblCommandSend.Click += btnGrblCommandSend_Click;

            //menu
            mnuViewGeneral.Tag = tabPageMain;
            mnuViewGeneral.Click += SelectTabControlMainTab;
            mnuViewOverrides.Tag = tabPageOverrides;
            mnuViewOverrides.Click += SelectTabControlMainTab;
            mnuViewJog.Tag = tabPageJog;
            mnuViewJog.Click += SelectTabControlMainTab;
            mnuViewSpindle.Tag = tabPageSpindle;
            mnuViewSpindle.Click += SelectTabControlMainTab;
            mnuViewServiceSchedule.Tag = tabPageServiceSchedule;
            mnuViewServiceSchedule.Click += SelectTabControlMainTab;
            mnuViewMachineSettings.Tag = tabPageMachineSettings;
            mnuViewMachineSettings.Click += SelectTabControlMainTab;
            mnuViewSettings.Tag = tabPageSettings;
            mnuViewSettings.Click += SelectTabControlMainTab;
        }

        protected override void LoadResources()
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
            toolStripButtonResume.Text = GSend.Language.Resources.StartOrResume;
            toolStripButtonResume.ToolTipText = GSend.Language.Resources.StartOrResume;
            toolStripButtonPause.Text = GSend.Language.Resources.Pause;
            toolStripButtonPause.ToolTipText = GSend.Language.Resources.Pause;
            toolStripButtonStop.Text = GSend.Language.Resources.Stop;
            toolStripButtonStop.ToolTipText = GSend.Language.Resources.Stop;
            toolStripStatusLabelSpindle.ToolTipText = GSend.Language.Resources.SpindleHint;
            toolStripStatusLabelFeedRate.ToolTipText = GSend.Language.Resources.CurrentFeedRate;
            toolStripDropDownButtonCoordinateSystem.ToolTipText = GSend.Language.Resources.CoordinateSystem;
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



            // Override tab
            cbOverridesDisable.Text = GSend.Language.Resources.DisableOverrides;
            selectionOverrideRapids.GroupName = GSend.Language.Resources.Rapids;
            selectionOverrideXY.GroupName = GSend.Language.Resources.OverrideXY;
            selectionOverrideZUp.GroupName = GSend.Language.Resources.OverrideZUp;
            selectionOverrideZDown.GroupName = GSend.Language.Resources.OverrideZDown;
            selectionOverrideSpindle.GroupName = GSend.Language.Resources.Spindle;
            selectionOverrideRapids.LabelValue = GSend.Language.Resources.RapidRateHigh;

            // Console tab
            tabPageConsole.Text = GSend.Language.Resources.Console;
            btnGrblCommandSend.Text = GSend.Language.Resources.Send;
            btnGrblCommandClear.Text = GSend.Language.Resources.Clear;

            // gcode tab
            tabPageGCode.Text = GSend.Language.Resources.GCode;
            columnHeaderLine.Text = GSend.Language.Resources.Line;
            columnHeaderAttributes.Text = GSend.Language.Resources.Attributes;
            columnHeaderComments.Text = GSend.Language.Resources.GCodeComments;
            columnHeaderFeed.Text = GSend.Language.Resources.FeedRate;
            columnHeaderGCode.Text = GSend.Language.Resources.GCode;
            columnHeaderSpindleSpeed.Text = GSend.Language.Resources.Spindle;
            columnHeaderStatus.Text = GSend.Language.Resources.Status;

            // 2d view
            tabPage2DView.Text = GSend.Language.Resources.View2D;

            // heartbeat tab
            tabPageHeartbeat.Text = GSend.Language.Resources.Graphs;
            heartbeatPanelBufferSize.GraphName = GSend.Language.Resources.GraphBufferSize;
            heartbeatPanelCommandQueue.GraphName = GSend.Language.Resources.GraphCommandQueue;
            heartbeatPanelFeed.GraphName = GSend.Language.Resources.GraphFeedRate;
            heartbeatPanelQueueSize.GraphName = GSend.Language.Resources.GraphQueueSize;
            heartbeatPanelSpindle.GraphName = GSend.Language.Resources.GraphSpindleSpeed;
            heartbeatPanelAvailableBlocks.GraphName = GSend.Language.Resources.GraphAvailableBlocks;
            heartbeatPanelAvailableRXBytes.GraphName = GSend.Language.Resources.GraphAvailableRXBytes;




            // menu items

            openFileDialog1.Filter = _gSendContext.Settings.FileFilter;

            //Machine
            mnuMachine.Text = GSend.Language.Resources.Machine;
            mnuMachineLoadGCode.Text = GSend.Language.Resources.LoadGCode;
            mnuMachineClearGCode.Text = GSend.Language.Resources.ClearGCode;
            mnuMachineRename.Text = GSend.Language.Resources.RenameMachine;
            mnuMachineClose.Text = GSend.Language.Resources.Close;

            //view
            mnuView.Text = GSend.Language.Resources.View;
            mnuViewGeneral.Text = GSend.Language.Resources.General;
            mnuViewOverrides.Text = GSend.Language.Resources.Overrides;
            mnuViewJog.Text = GSend.Language.Resources.Jog;
            mnuViewSpindle.Text = GSend.Language.Resources.Spindle;
            mnuViewServiceSchedule.Text = GSend.Language.Resources.ServiceSchedule;
            mnuViewMachineSettings.Text = GSend.Language.Resources.MachineSettings;
            mnuViewSettings.Text = GSend.Language.Resources.Settings;
            mnuViewConsole.Text = GSend.Language.Resources.Console;

            // action
            mnuActionSaveConfig.Text = GSend.Language.Resources.SaveConfiguration;
            mnuActionConnect.Text = GSend.Language.Resources.Connect;
            mnuActionDisconnect.Text = GSend.Language.Resources.Disconnect;
            mnuActionClearAlarm.Text = GSend.Language.Resources.ClearAlarm;
            mnuActionHome.Text = GSend.Language.Resources.Home;
            mnuActionProbe.Text = GSend.Language.Resources.Probe;
            mnuActionRun.Text = GSend.Language.Resources.Resume;
            mnuActionPause.Text = GSend.Language.Resources.Pause;
            mnuActionStop.Text = GSend.Language.Resources.Stop;

            // options
            mnuOptions.Text = GSend.Language.Resources.AppMenuOptions;
            mnuOptionsShortcutKeys.Text = GSend.Language.Resources.AppMenuShortcuts;
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
            using (WarningPanel warningPanel = new())
            {
                e.Graphics.FillRectangle(new SolidBrush(toolStripStatusLabelWarnings.BackColor), e.ClipRectangle);
                int count = warningsAndErrors.ErrorCount();
                int leftPos = 2;

                if (count > 0)
                {
                    e.Graphics.DrawImage(warningPanel.GetImageForInformationType(InformationType.Error), new Point(leftPos, 1));
                    e.Graphics.DrawString($"{count}", toolStripStatusLabelWarnings.Font,
                        new SolidBrush(toolStripStatusLabelWarnings.ForeColor), new Point(leftPos + 18, 1));
                    leftPos += GSendShared.Constants.WarningStatusWidth;
                }

                count = warningsAndErrors.WarningCount();

                if (count > 0)
                {
                    e.Graphics.DrawImage(warningPanel.GetImageForInformationType(InformationType.Warning), new Point(leftPos, 1));
                    e.Graphics.DrawString($"{count}", toolStripStatusLabelWarnings.Font,
                        new SolidBrush(toolStripStatusLabelWarnings.ForeColor), new Point(leftPos + 18, 1));
                    leftPos += GSendShared.Constants.WarningStatusWidth;
                }

                count = warningsAndErrors.InformationCount();

                if (count > 0)
                {
                    e.Graphics.DrawImage(warningPanel.GetImageForInformationType(InformationType.Information), new Point(leftPos, 1));
                    e.Graphics.DrawString($"{count}", toolStripStatusLabelWarnings.Font,
                        new SolidBrush(toolStripStatusLabelWarnings.ForeColor), new Point(leftPos + 18, 1));
                }

                e.Graphics.DrawLine(_borderPen, e.ClipRectangle.Right - 1, e.ClipRectangle.Top, e.ClipRectangle.Right - 1, e.ClipRectangle.Bottom - 2);
            }

        }

        private void FrmMachine_Shown(object sender, EventArgs e)
        {
            warningsAndErrors.ResetLayoutWarningErrorSize();
        }

        private void dataGridGCode_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            string rowIdx = (e.RowIndex + 1).ToString();

            StringFormat centerFormat = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            Rectangle headerBounds = new(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void tabControlMain_Resize(object sender, EventArgs e)
        {
            tabControlSecondary.Width = tabControlMain.Width;
        }

        private void FrmMachine_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = _shortcutHandler.KeyDown(e);

        }

        private void FrmMachine_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = _shortcutHandler.KeyUp(e);
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
                IGSendApiWrapper machineApiWrapper = _gSendContext.ServiceProvider.GetRequiredService<IGSendApiWrapper>();

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
            if (_machineConnected)
            {
                if (_isPaused)
                {
                    SendMessage(String.Format(MessageMachineResume, _machine.Id));
                }
                else if (_machineStatusModel.TotalLines > 0 && !_machineStatusModel.IsRunning)
                {
                    IGSendApiWrapper apiWrapper = _serviceProvider.GetRequiredService<IGSendApiWrapper>();

                    using (StartJobWizard startJobWizard = new(_machineStatusModel, _gCodeAnalyses, apiWrapper))
                    {
                        if (startJobWizard.ShowDialog() == DialogResult.OK)
                        {
                            _toolProfile = startJobWizard.ToolProfile;
                            IJobProfile jobProfile = startJobWizard.JobProfile;

                            if (startJobWizard.IsSimulation)
                                SendMessage(String.Format(Constants.MessageToggleSimulation, _machine.Id));

                            SendMessage(String.Format(Constants.MessageRunGCode, _machine.Id, _toolProfile.Id, jobProfile.Id));
                        }
                    }
                }
            }
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

        private void mnuMachineLoadGCode_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = String.Empty;

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                LoadGCode(openFileDialog1.FileName);
            }
        }

        private void mnuMachineClearGCode_Click(object sender, EventArgs e)
        {
            UnloadGCode();
        }

        private string validation(string newName)
        {
            if (String.IsNullOrEmpty(newName))
                return GSend.Language.Resources.MachineNameEmpty;

            IGSendApiWrapper machineApiWrapper = _gSendContext.ServiceProvider.GetRequiredService<IGSendApiWrapper>();

            if (machineApiWrapper.MachineNameExists(newName))
                return GSend.Language.Resources.MachineNameAlreadyExists;

            return null;
        }

        private void mnuMachineRename_Click(object sender, EventArgs e)
        {
            ApiSettings apiSettings = _gSendContext.ServiceProvider.GetRequiredService<ApiSettings>();
            IRunProgram runProgram = _gSendContext.ServiceProvider.GetRequiredService<IRunProgram>();

            runProgram.Run($"{apiSettings.RootAddress}Machines/Edit/{_machine.Id}/", null, true, false, apiSettings.Timeout);
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

        private void mnuOptionsShortcutKeys_Click(object sender, EventArgs e)
        {
            if (ShortcutEditor.ShowDialog(this, ref _shortcuts))
            {
                UpdateShortcutKeyValues(_shortcuts);

                foreach (IShortcut shortcut in _shortcuts)
                {
                    DesktopSettings.WriteValue("Shortcut Keys", shortcut.Name, String.Join(';', shortcut.DefaultKeys));
                }
            }
        }


        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            AboutBox.ShowAboutBox(GSend.Language.Resources.AppNameEditor, this.Icon);
        }

        #endregion Menu

        #region G Code

        private void LoadGCode(string fileName)
        {
            using (MouseControl mouseControl = MouseControl.ShowWaitCursor(this))
            {
                UnloadGCode();

                try
                {
                    IGCodeParserFactory gCodeParserFactory = _serviceProvider.GetService<IGCodeParserFactory>();
                    IGCodeParser gCodeParser = gCodeParserFactory.CreateParser();
                    string fileContents = File.ReadAllText(fileName);
                    _gCodeAnalyses = gCodeParser.Parse(fileContents);
                    _gCodeAnalyses.Analyse(fileName);

                    _gcodeLines = _gCodeAnalyses.Lines(out _totalLines);
                    listViewGCode.VirtualListSize = _totalLines;

                    selectionOverrideXY.Value = (int)_gCodeAnalyses.FeedX;
                    selectionOverrideZDown.Value = (int)_gCodeAnalyses.FeedZ;
                    selectionOverrideZUp.Value = (int)_gCodeAnalyses.FeedZ;

                    // set current spindle speed
                    List<IGCodeCommand> sCommands = _gCodeAnalyses.Commands.Where(c => c.Command.Equals('S')).ToList();
                    selectionOverrideSpindle.Value = sCommands.Count > 0 ? (int)sCommands.Max(c => c.CommandValue) : 0;

                    trackBarPercent.ValueChanged -= trackBarPercent_ValueChanged;
                    trackBarPercent.Value = Shared.Utilities.CheckMinMax((int)_gCodeAnalyses.FeedX / (selectionOverrideXY.Maximum / 100), 1, 100);
                    labelSpeedPercent.Text = String.Format(GSend.Language.Resources.SpeedPercent, trackBarPercent.Value);
                    trackBarPercent.ValueChanged += trackBarPercent_ValueChanged;

                    machine2dView1.LoadGCode(_gCodeAnalyses);

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

                    AnalyzeWarningAndErrors analyzeWarningAndErrors = new(_serviceProvider.GetRequiredService<ISubprograms>());
                    analyzeWarningAndErrors.ViewAndAnalyseWarningsAndErrors(warningsAndErrors, null, _gCodeAnalyses);

                    if (_gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.UsesMistCoolant) && !_machine.Options.HasFlag(MachineOptions.MistCoolant))
                    {
                        warningsAndErrors.AddWarningPanel(InformationType.Warning, GSend.Language.Resources.AnalysesWarningContainsMistCoolantOption);
                    }

                    if (_gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.UsesFloodCoolant) && !_machine.Options.HasFlag(MachineOptions.FloodCoolant))
                    {
                        warningsAndErrors.AddWarningPanel(InformationType.Warning, GSend.Language.Resources.AnalysesWarningContainsFloodCoolantOption);
                    }

                    if (_gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.ContainsToolChanges) && !_machine.Options.HasFlag(MachineOptions.ToolChanger))
                    {
                        warningsAndErrors.AddWarningPanel(InformationType.Warning, GSend.Language.Resources.AnalysesWarningContainsToolChangeOption);
                    }

                    if (_gCodeAnalyses.MaxLayerDepth > _machine.LayerHeightWarning && _machine.Options.HasFlag(MachineOptions.LayerHeightWarning))
                    {
                        warningsAndErrors.AddWarningPanel(InformationType.Warning, String.Format(GSend.Language.Resources.AnalysesWarningLayerHeightTooMuch,
                            _gCodeAnalyses.MaxLayerDepth, _machine.LayerHeightWarning));
                    }

                    _ = _gCodeAnalyses.AllLines(out int _totalAllLines);
                    UpdateLabelText(lblTotalLines, String.Format(GSend.Language.Resources.TotalLines, _totalLines));
                    lblTotalLines.Visible = true;


                    toolStripProgressBarJob.Maximum = _totalAllLines;

                    gCodeAnalysesDetails.LoadAnalyser(fileName, _gCodeAnalyses);
                    string fileNameAsBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(fileName));
                    SendMessage(String.Format(Constants.MessageLoadGCode, _machine.Id, fileNameAsBase64));
                }
                catch (Exception ex)
                {
                    warningsAndErrors.AddWarningPanel(InformationType.Error, ex.Message);
                }

                UpdateEnabledState();
                tabControlSecondary.TabPages.Insert(1, tabPageGCode);
            }
        }


        private static string FixEmptyValue(decimal value)
        {
            return value == 0 ? String.Empty : value.ToString();
        }

        private static string FixEmptyValue(CommandAttributes value)
        {
            CommandAttributes copy = value;

            copy &= ~CommandAttributes.AllowSpeedOverride;

            return copy == CommandAttributes.None ? String.Empty : Shared.Utilities.SplitCamelCase(copy.ToString().Replace("Movement", ""));
        }

        private void UnloadGCode()
        {
            listViewGCode.VirtualListSize = 0;

            if (_gCodeAnalyses != null)
                _gCodeAnalyses = null;

            if (_gcodeLines != null)
                _gcodeLines = null;

            machine2dView1.UnloadGCode();
            gCodeAnalysesDetails.LoadAnalyser(_gCodeAnalyses);
            SendMessage(String.Format(Constants.MessageUnloadGCode, _machine.Id));
            tabControlSecondary.TabPages.Remove(tabPageGCode);
            lblTotalLines.Visible = false;
            UpdateEnabledState();
        }

        private void ListViewGCode_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (_gcodeLines == null || _gcodeLines.Count < e.ItemIndex)
                return;

            IGCodeLine gcodeLine = _gcodeLines[e.ItemIndex];
            IGCodeLineInfo gCodeLineInfo = gcodeLine.GetGCodeInfo();

            int line = e.ItemIndex + 1;
            ListViewItem item = new(line.ToString());
            item.SubItems.Add(gCodeLineInfo.GCode);
            item.SubItems.Add(gCodeLineInfo.Comments);
            item.SubItems.Add(FixEmptyValue(gCodeLineInfo.FeedRate));
            item.SubItems.Add(FixEmptyValue(gCodeLineInfo.SpindleSpeed));
            item.SubItems.Add(FixEmptyValue(gCodeLineInfo.Attributes));
            item.SubItems.Add(gcodeLine.Status.ToString());
            item.Tag = gcodeLine;
            e.Item = item;
        }

        #endregion G Code

        #region Shortcuts

        private void ShortcutHandler_OnKeyComboDown(object sender, ShortcutArgs e)
        {
            IShortcut shortcut = _shortcuts.FirstOrDefault(s => s.Name.Equals(e.Name));

            if (shortcut != null)
            {
                shortcut.Trigger(true);
            }
        }

        private void ShortcutHandler_OnKeyComboUp(object sender, ShortcutArgs e)
        {
            IShortcut shortcut = _shortcuts.FirstOrDefault(s => s.Name.Equals(e.Name));

            if (shortcut != null)
            {
                shortcut.Trigger(false);
            }
        }

        private List<IShortcut> RetrieveAvailableShortcuts()
        {
            List<IShortcut> Result = new();
            RecursivelyRetrieveAllShortcutClasses(this, Result, 0);

            UpdateShortcutKeyValues(Result);
            return Result;
        }

        private void UpdateShortcutKeyValues(List<IShortcut> Result)
        {
            foreach (IShortcut shortcut in Result)
            {
                Debug.Assert(!_shortcutHandler.IsKeyComboRegistered(shortcut.DefaultKeys));

                _shortcutHandler.AddKeyCombo(shortcut.Name, shortcut.DefaultKeys);

                string keyArray = String.Join(';', shortcut.DefaultKeys);

                // is it overridden?
                string shortcutValue = DesktopSettings.ReadValue<string>("Shortcut Keys", shortcut.Name, keyArray);

                if (!String.IsNullOrEmpty(shortcutValue) && keyArray != shortcutValue)
                {
                    shortcut.DefaultKeys.Clear();

                    string[] keyItems = shortcutValue.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                    foreach (string item in keyItems)
                    {
                        if (Int32.TryParse(item, out int keyValue))
                            shortcut.DefaultKeys.Add(keyValue);
                    }
                }

                if (shortcut != null && shortcut.KeysUpdated != null)
                    shortcut.KeysUpdated(shortcut.DefaultKeys);
            }
        }

        private void RecursivelyRetrieveAllShortcutClasses(Control control, List<IShortcut> shortcuts, int depth)
        {
            if (depth > 25)
            {

            }

            if (control is IShortcutImplementation shortcutImpl)
            {
                shortcuts.AddRange(shortcutImpl.GetShortcuts());
            }

            foreach (Control childControl in control.Controls)
            {
                RecursivelyRetrieveAllShortcutClasses(childControl, shortcuts, depth + 1);
            }
        }

        private static void UpdateMenuShortCut(ToolStripMenuItem menu, List<int> keys)
        {
            if (menu == null || keys == null || keys.Count == 0)
                return;

            Keys key = Keys.None;

            foreach (int intKeyValue in keys)
            {
                key |= (Keys)intKeyValue;
            }

            menu.ShortcutKeys = key;
        }

        public List<IShortcut> GetShortcuts()
        {
            string groupName = GSend.Language.Resources.ShortcutMenuMachine;
            string groupNameViewMenu = GSend.Language.Resources.ShortcutMenuView;
            string groupNameActionMenu = GSend.Language.Resources.ShortcutMenuAction;
            string groupNameOverrides = GSend.Language.Resources.ShortcutGroupOverrides;
            string groupNameSettings = GSend.Language.Resources.ShortcutSettings;
            string groupNameCoordinates = GSend.Language.Resources.ShortcutCoordinates;

            return new()
            {
                // machine menu
                new ShortcutModel(groupName, GSend.Language.Resources.LoadGCode,
                    new List<int>() { (int)Keys.Control, (int)Keys.L},
                    (bool isKeyDown) => { if (isKeyDown && mnuMachineLoadGCode.Enabled) mnuMachineLoadGCode_Click(this, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuMachineLoadGCode, keys)),
                new ShortcutModel(groupName, GSend.Language.Resources.ClearGCode,
                    new List<int>() { (int)Keys.Control, (int)Keys.Delete},
                    (bool isKeyDown) => { if (isKeyDown && mnuMachineClearGCode.Enabled) mnuMachineClearGCode_Click(this, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuMachineClearGCode, keys)),
                new ShortcutModel(groupName, GSend.Language.Resources.Close,
                    new List<int>(),
                    (bool isKeyDown) => { if (isKeyDown && mnuMachineClose.Enabled) closeToolStripMenuItem_Click(this, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuMachineClose, keys)),

                // view menu
                new ShortcutModel(groupNameViewMenu, GSend.Language.Resources.ShortcutTabGeneral,
                    new List<int>() { (int)Keys.Control, (int)Keys.G },
                    (bool isKeyDown) => { if (isKeyDown && mnuViewGeneral.Enabled) SelectTabControlMainTab(mnuViewGeneral, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuViewGeneral, keys)),
                new ShortcutModel(groupNameViewMenu, GSend.Language.Resources.ShortcutTabOverrides,
                    new List<int>() { (int)Keys.Control, (int)Keys.O },
                    (bool isKeyDown) => { if (isKeyDown && mnuViewOverrides.Enabled) SelectTabControlMainTab(mnuViewOverrides, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuViewOverrides, keys)),
                new ShortcutModel(groupNameViewMenu, GSend.Language.Resources.ShortcutTabJog,
                    new List<int>() { (int)Keys.Control, (int)Keys.J },
                    (bool isKeyDown) => { if (isKeyDown && mnuViewJog.Enabled) SelectTabControlMainTab(mnuViewJog, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuViewJog, keys)),
                new ShortcutModel(groupNameViewMenu, GSend.Language.Resources.ShortcutTabSpindle,
                    new List<int>() { (int)Keys.Control, (int)Keys.I },
                    (bool isKeyDown) => { if (isKeyDown && mnuViewSpindle.Enabled) SelectTabControlMainTab(mnuViewSpindle, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuViewSpindle, keys)),
                new ShortcutModel(groupNameViewMenu, GSend.Language.Resources.ShortcutTabServiceSchedule,
                    new List<int>() { (int)Keys.Control, (int)Keys.E },
                    (bool isKeyDown) => { if (isKeyDown && mnuViewServiceSchedule.Enabled) SelectTabControlMainTab(mnuViewServiceSchedule, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuViewServiceSchedule, keys)),
                new ShortcutModel(groupNameViewMenu, GSend.Language.Resources.ShortcutTabMachineSettings,
                    new List<int>() { (int)Keys.Control, (int)Keys.M },
                    (bool isKeyDown) => { if (isKeyDown && mnuViewMachineSettings.Enabled) SelectTabControlMainTab(mnuViewMachineSettings, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuViewMachineSettings, keys)),
                new ShortcutModel(groupNameViewMenu, GSend.Language.Resources.ShortcutTabSettings,
                    new List<int>() { (int)Keys.Control, (int)Keys.T },
                    (bool isKeyDown) => { if (isKeyDown && mnuViewSettings.Enabled) SelectTabControlMainTab(mnuViewSettings, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuViewSettings, keys)),
                new ShortcutModel(groupNameViewMenu, GSend.Language.Resources.ShortcutTabConsole,
                    new List<int>() { (int)Keys.F4 },
                    (bool isKeyDown) => { if (isKeyDown && mnuViewConsole.Enabled) SelectTabControlMainTab(mnuViewConsole, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuViewConsole, keys)),


                // action menu
                new ShortcutModel(groupNameActionMenu, GSend.Language.Resources.ShortcutSaveConfig,
                    new List<int>() { (int)Keys.Control, (int)Keys.S },
                    (bool isKeyDown) => { if (isKeyDown && mnuActionSaveConfig.Enabled) toolStripButtonSave_Click(mnuActionSaveConfig, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuActionSaveConfig, keys)),
                new ShortcutModel(groupNameActionMenu, GSend.Language.Resources.ShortcutConnect,
                    new List<int>() { (int)Keys.F2 },
                    (bool isKeyDown) => { if (isKeyDown && mnuActionConnect.Enabled) toolStripButtonConnect_Click(mnuActionConnect, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuActionConnect, keys)),
                new ShortcutModel(groupNameActionMenu, GSend.Language.Resources.ShortcutDisconnect,
                    new List<int>() { (int)Keys.F3 },
                    (bool isKeyDown) => { if (isKeyDown && mnuActionDisconnect.Enabled) toolStripButtonDisconnect_Click(mnuActionDisconnect, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuActionDisconnect, keys)),
                new ShortcutModel(groupNameActionMenu, GSend.Language.Resources.ShortcutClearAlarm,
                    new List<int>() { (int)Keys.Control, (int)Keys.A },
                    (bool isKeyDown) => { if (isKeyDown && mnuActionClearAlarm.Enabled) toolStripButtonClearAlarm_Click(mnuActionClearAlarm, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuActionClearAlarm, keys)),
                new ShortcutModel(groupNameActionMenu, GSend.Language.Resources.ShortcutHome,
                    new List<int>() { (int)Keys.Control, (int)Keys.H },
                    (bool isKeyDown) => { if (isKeyDown && mnuActionHome.Enabled) toolStripButtonHome_Click(mnuActionHome, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuActionHome, keys)),
                new ShortcutModel(groupNameActionMenu, GSend.Language.Resources.ShortcutProbe,
                    new List<int>() { (int)Keys.Control, (int)Keys.B },
                    (bool isKeyDown) => { if (isKeyDown && mnuActionProbe.Enabled) toolStripButtonProbe_Click(mnuActionProbe, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuActionProbe, keys)),
                new ShortcutModel(groupNameActionMenu, GSend.Language.Resources.ShortcutRun,
                    new List<int>() { (int)Keys.F9 },
                    (bool isKeyDown) => { if (isKeyDown && mnuActionRun.Enabled) toolStripButtonResume_Click(mnuActionRun, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuActionRun, keys)),
                new ShortcutModel(groupNameActionMenu, GSend.Language.Resources.ShortcutPause,
                    new List<int>() { (int)Keys.F10 },
                    (bool isKeyDown) => { if (isKeyDown && mnuActionPause.Enabled) toolStripButtonPause_Click(mnuActionPause, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuActionPause, keys)),
                new ShortcutModel(groupNameActionMenu, GSend.Language.Resources.ShortcutStop,
                    new List<int>() { (int)Keys.F11 },
                    (bool isKeyDown) => { if (isKeyDown && mnuActionStop.Enabled) toolStripButtonStop_Click(mnuActionStop, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuActionStop, keys)),


                // overrides
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutToggleOverrideEnabled,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && tabPageOverrides.Enabled) cbOverridesDisable.Checked = !cbOverridesDisable.Checked; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutToggleRapidsOverrideEnabled,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && tabPageOverrides.Enabled) selectionOverrideRapids.Checked = !selectionOverrideRapids.Checked; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutToggleRapidsOverrideXYEnabled,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && tabPageOverrides.Enabled) selectionOverrideXY.Checked = !selectionOverrideXY.Checked; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutToggleRapidsOverrideZUpEnabled,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && tabPageOverrides.Enabled) selectionOverrideZUp.Checked = !selectionOverrideZUp.Checked; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutToggleRapidsOverrideZDownEnabled,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && tabPageOverrides.Enabled) selectionOverrideZDown.Checked = !selectionOverrideZDown.Checked; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutToggleRapidsOverrideSpindleEnabled,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && tabPageOverrides.Enabled) selectionOverrideSpindle.Checked = !selectionOverrideSpindle.Checked; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutIncreaseRapidsSpeed,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && selectionOverrideRapids.Enabled) selectionOverrideRapids.Value++; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutDecreaseRapidsSpeed,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && selectionOverrideRapids.Enabled) selectionOverrideRapids.Value--; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutIncreaseXYSpeed,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && selectionOverrideXY.Enabled) selectionOverrideXY.Value += 100; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutDecreaseXYSpeed,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && selectionOverrideXY.Enabled) selectionOverrideXY.Value -= 100; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutIncreaseZUpSpeed,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && selectionOverrideZUp.Enabled) selectionOverrideZUp.Value += 100; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutDecreaseZUpSpeed,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && selectionOverrideZUp.Enabled) selectionOverrideZUp.Value -= 100; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutIncreaseZDownSpeed,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && selectionOverrideZDown.Enabled) selectionOverrideZDown.Value += 100; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutDecreaseZDownSpeed,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && selectionOverrideZDown.Enabled) selectionOverrideZDown.Value -= 100; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutIncreaseSpindleSpeed,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && selectionOverrideSpindle.Enabled) selectionOverrideSpindle.Value += 100; },
                    null),
                new ShortcutModel(groupNameOverrides, GSend.Language.Resources.ShortcutDecreaseSpindleSpeed,
                    new List<int>() {  },
                    (bool isKeyDown) => { if (isKeyDown && selectionOverrideSpindle.Enabled) selectionOverrideSpindle.Value -= 100; },
                    null),

                // settings
                new ShortcutModel(groupNameSettings, GSend.Language.Resources.ShortcutToggleMmInch,
                    new List<int>() { (int)Keys.Control, (int)Keys.Home },
                    (bool isKeyDown) => { if (isKeyDown && rbFeedbackInch.Checked) rbFeedbackMm.Checked = true; else rbFeedbackInch.Checked = true; },
                    null),

                // coordinates 
                new ShortcutModel(groupNameCoordinates, GSend.Language.Resources.ShortcutSelectG54,
                    new List<int>() { (int)Keys.Control, (int)Keys.Shift, (int)Keys.D1 },
                    (bool isKeyDown) => { if (isKeyDown && toolStripDropDownButtonCoordinateSystem.Enabled) ToolstripButtonCoordinates_Click(g54ToolStripMenuItem, EventArgs.Empty); },
                    null),
                new ShortcutModel(groupNameCoordinates, GSend.Language.Resources.ShortcutSelectG55,
                    new List<int>() { (int)Keys.Control, (int)Keys.Shift, (int)Keys.D2 },
                    (bool isKeyDown) => { if (isKeyDown && toolStripDropDownButtonCoordinateSystem.Enabled) ToolstripButtonCoordinates_Click(g55ToolStripMenuItem, EventArgs.Empty); },
                    null),
                new ShortcutModel(groupNameCoordinates, GSend.Language.Resources.ShortcutSelectG56,
                    new List<int>() { (int)Keys.Control, (int)Keys.Shift, (int)Keys.D3 },
                    (bool isKeyDown) => { if (isKeyDown && toolStripDropDownButtonCoordinateSystem.Enabled) ToolstripButtonCoordinates_Click(g56ToolStripMenuItem, EventArgs.Empty); },
                    null),
                new ShortcutModel(groupNameCoordinates, GSend.Language.Resources.ShortcutSelectG57,
                    new List<int>() { (int)Keys.Control, (int)Keys.Shift, (int)Keys.D4 },
                    (bool isKeyDown) => { if (isKeyDown && toolStripDropDownButtonCoordinateSystem.Enabled) ToolstripButtonCoordinates_Click(g57ToolStripMenuItem, EventArgs.Empty); },
                    null),
                new ShortcutModel(groupNameCoordinates, GSend.Language.Resources.ShortcutSelectG58,
                    new List<int>() { (int)Keys.Control, (int)Keys.Shift, (int)Keys.D5 },
                    (bool isKeyDown) => { if (isKeyDown && toolStripDropDownButtonCoordinateSystem.Enabled) ToolstripButtonCoordinates_Click(g58ToolStripMenuItem, EventArgs.Empty); },
                    null),
                new ShortcutModel(groupNameCoordinates, GSend.Language.Resources.ShortcutSelectG59,
                    new List<int>() { (int)Keys.Control, (int)Keys.Shift, (int)Keys.D6 },
                    (bool isKeyDown) => { if (isKeyDown && toolStripDropDownButtonCoordinateSystem.Enabled) ToolstripButtonCoordinates_Click(g59ToolStripMenuItem, EventArgs.Empty); },
                    null),
            };
        }

        #endregion Shortcuts
    }
}
