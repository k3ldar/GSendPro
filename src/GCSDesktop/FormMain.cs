using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

using GSendApi;

using GSendCommon;
using GSendCommon.Settings;

using GSendControls;

using GSendDesktop.Abstractions;
using GSendDesktop.Internal;

using GSendShared;
using GSendShared.Interfaces;
using GSendShared.Models;
using GSendShared.Plugins;

using Microsoft.Extensions.DependencyInjection;

using Shared.Classes;

using static GSendShared.Constants;

namespace GSendDesktop
{
    public partial class FormMain : BaseForm, IPluginHost
    {
        private readonly IGSendContext _context;

        private readonly IGSendApiWrapper _machineApiWrapper;
        private readonly ICommandProcessor _processCommand;
        private readonly GSendWebSocket _clientWebSocket;
        private readonly object _lockObject = new();
        private readonly CancellationTokenRegistration _cancellationTokenRegistration;
        private readonly Dictionary<long, ListViewItem> _machines = new();
        private IMachine _selectedMachine = null;
        private readonly IPluginHelper _pluginHelper;
        private long _machineHashCombined = 0;

        public FormMain(IGSendContext context, IGSendApiWrapper machineApiWrapper,
            ICommandProcessor processCommand, GSendSettings settings)
        {
            InitializeComponent();

            _context = context ?? throw new ArgumentNullException(nameof(context));
            _machineApiWrapper = machineApiWrapper ?? throw new ArgumentNullException(nameof(machineApiWrapper));
            _processCommand = processCommand ?? throw new ArgumentNullException(nameof(processCommand));
            _pluginHelper = context.ServiceProvider.GetRequiredService<IPluginHelper>() ?? throw new InvalidOperationException();

            _cancellationTokenRegistration = new();
            ApiSettings apiSettings = context.ServiceProvider.GetRequiredService<ApiSettings>();
            _clientWebSocket = new GSendWebSocket(apiSettings.RootAddress, nameof(FormMain), _cancellationTokenRegistration.Token);
            _clientWebSocket.ProcessMessage += ClientWebSocket_ProcessMessage;
            _clientWebSocket.ConnectionLost += ClientWebSocket_ConnectionLost;
            _clientWebSocket.Connected += ClientWebSocket_Connected;
            timerUpdateStatus.Interval = settings.UpdateMilliseconds;

            // initialize menu's for plugins
            machineToolStripMenuItem.Tag = new InternalPluginMenu(machineToolStripMenuItem);
            helpToolStripMenuItem.Tag = new InternalPluginMenu(helpToolStripMenuItem);
            subprogramsToolStripMenuItem.Tag = new InternalPluginMenu(subprogramsToolStripMenuItem);

            _pluginHelper.InitializeAllPlugins(this);
        }

        protected override string SectionName => nameof(GSendDesktop);

        #region Client Web Socket

        private void ClientWebSocket_Connected(object sender, EventArgs e)
        {
            RaiseServerConnected();
        }

        private void ClientWebSocket_ConnectionLost(object sender, EventArgs e)
        {
            ValidateServerConnection();
        }

        private void ClientWebSocket_ProcessMessage(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            ClientBaseMessage clientMessage = JsonSerializer.Deserialize<ClientBaseMessage>(message, Constants.DefaultJsonSerializerOptions);

            if (clientMessage.request.Equals(Constants.MessageMachineConnectServer))
            {
                JsonElement element = (JsonElement)clientMessage.message;

                ConnectResult connectResult = (ConnectResult)element.GetInt32();

                ProcessClientConnectMessage(connectResult);

                return;
            }

            if (!clientMessage.success)
            {
                return;
            }

            string serverCpu = String.Format(GSend.Language.Resources.ServerCpuStateConnected, clientMessage.ServerCpuStatus.ToString("N2"));

            if (!toolStripStatusCpu.Text.Equals(serverCpu))
                toolStripStatusCpu.Text = serverCpu;

            if (clientMessage.CombinedHash != _machineHashCombined)
            {
                _machineHashCombined = clientMessage.CombinedHash;
                UpdateMachines();
            }

            if (clientMessage.request.Equals(MessageMachineStatusAll))
            {
                List<StatusResponseMessage> statuses = JsonSerializer.Deserialize<List<StatusResponseMessage>>(clientMessage.message.ToString(), Constants.DefaultJsonSerializerOptions);
                UpdateMachineStatus(statuses);
            }

            UpdateEnabledState();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "Left in for now")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Ignore for now")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Ignore for now")]
        private void ProcessClientConnectMessage(ConnectResult connectResult)
        {
            //if (InvokeRequired)
            //{
            //    Invoke(ProcessClientConnectMessage, connectResult);
            //    return;
            //}


            //switch (connectResult)
            //{
            //    case ConnectResult.Success:
            //    case ConnectResult.AlreadyConnected:
            //        break;

            //    case ConnectResult.TimeOut:

            //        break;

            //    case ConnectResult.Error:

            //        break;
            //}
        }

        private void UpdateMachineStatus(List<StatusResponseMessage> statuses)
        {
            if (InvokeRequired)
            {
                Invoke(UpdateMachineStatus, statuses);
                return;
            }

            if (statuses.Count != _machines.Count)
                UpdateMachines();

            //error checking
            foreach (StatusResponseMessage status in statuses)
            {
                if (!_machines.ContainsKey(status.Id))
                    continue;

                using (TimedLock tl = TimedLock.Lock(_lockObject))
                {
                    ListViewItem machineItem = _machines[status.Id];

                    if (machineItem == null)
                        continue;

                    string connectedText = status.Connected ? GSend.Language.Resources.Yes : GSend.Language.Resources.No;

                    if (status.Connected)
                    {

                        if (status.UpdatedConfiguration)
                        {
                            if (machineItem.SubItems[0].BackColor != Color.Orange)
                                machineItem.SubItems[0].BackColor = Color.Orange;
                        }
                        else
                        {
                            if (machineItem.SubItems[0].BackColor != Color.White)
                                machineItem.SubItems[0].BackColor = Color.White;
                        }

                        if (!machineItem.SubItems[3].Text.Equals(connectedText))
                            machineItem.SubItems[3].Text = connectedText;

                        if (!machineItem.SubItems[4].Text.Equals(status.State))
                        {
                            Color backColor = Color.White;
                            Color foreColor = Color.Black;
                            string text = HelperMethods.TranslateState(status.State);

                            switch (status.State)
                            {
                                case StateUndefined:
                                    text = GSend.Language.Resources.StatePortOpen;
                                    backColor = Color.Yellow;
                                    foreColor = Color.Black;
                                    break;

                                case StateIdle:
                                    backColor = Color.Blue;
                                    foreColor = Color.White;
                                    break;

                                case StateJog:
                                case StateRun:
                                    backColor = Color.Green;
                                    foreColor = Color.White;
                                    break;

                                case StateAlarm:
                                case StateDoor:
                                case StateCheck:
                                    backColor = Color.Red;
                                    foreColor = Color.White;
                                    break;

                                case StateHome:
                                    backColor = Color.Aqua;
                                    foreColor = Color.Black;
                                    break;

                                case StateSleep:
                                    backColor = Color.DarkGray;
                                    foreColor = Color.White;
                                    break;
                            }

                            if (!machineItem.SubItems[4].Text.Equals(text))
                                machineItem.SubItems[4].Text = text;

                            if (machineItem.SubItems[4].BackColor != backColor)
                                machineItem.SubItems[4].BackColor = backColor;

                            if (machineItem.SubItems[4].ForeColor != foreColor)
                                machineItem.SubItems[4].ForeColor = foreColor;

                            if (machineItem.SubItems[5].Text != status.CpuStatus)
                                machineItem.SubItems[5].Text = status.CpuStatus;
                        }
                    }
                    else
                    {
                        if (!machineItem.SubItems[3].Text.Equals(connectedText))
                            machineItem.SubItems[3].Text = connectedText;

                        if (!machineItem.SubItems[4].Text.Equals(String.Empty))
                        {
                            machineItem.SubItems[4].BackColor = Color.White;
                            machineItem.SubItems[4].ForeColor = Color.Black;
                            machineItem.SubItems[4].Text = String.Empty;
                        }
                    }
                }
            }
        }

        #endregion Client Web Socket

        #region ToolBar

        private void toolStripButtonGetMachines_Click(object sender, EventArgs e)
        {
            UpdateMachines();
        }

        private void toolStripButtonAddMachine_Click(object sender, EventArgs e)
        {
            ApiSettings apiSettings = _context.ServiceProvider.GetRequiredService<ApiSettings>();
            IRunProgram runProgram = _context.ServiceProvider.GetRequiredService<IRunProgram>();

            runProgram.Run($"{apiSettings.RootAddress}Machines/Add/", null, true, false, apiSettings.Timeout);
        }

        private void toolStripButtonPauseAll_Click(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(MessageMachinePauseAll).ConfigureAwait(false);
        }

        private void toolStripButtonResumeAll_Click(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(MessageMachineResumeAll).ConfigureAwait(false);
        }

        private void toolStripButtonConnect_Click(object sender, EventArgs e)
        {
            if (_selectedMachine != null)
            {
                _clientWebSocket.SendAsync(String.Format(MessageMachineConnect, _selectedMachine.Id)).ConfigureAwait(false);
            }
        }

        private void toolStripButtonClearAlarm_Click(object sender, EventArgs e)
        {
            if (_selectedMachine != null)
            {
                _clientWebSocket.SendAsync(String.Format(MessageMachineClearAlarm, _selectedMachine.Id)).ConfigureAwait(false);
            }
        }

        private void toolStripButtonResume_Click(object sender, EventArgs e)
        {
            if (_selectedMachine != null)
            {
                _clientWebSocket.SendAsync(String.Format(MessageMachineResume, _selectedMachine.Id)).ConfigureAwait(false);
            }
        }

        private void toolStripButtonHome_Click(object sender, EventArgs e)
        {
            if (_selectedMachine != null)
            {
                _clientWebSocket.SendAsync(String.Format(MessageMachineHome, _selectedMachine.Id)).ConfigureAwait(false);
            }
        }

        #endregion ToolBar

        #region Form Methods

        private void UpdateMachines()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { UpdateMachines(); }));
                return;
            }

            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                _processCommand.ProcessCommand(() =>
                {
                    using (MouseControl mouseControl = MouseControl.ShowWaitCursor(this))
                    {
                        listViewMachines.Items.Clear();
                        _machines.Clear();

                        listViewMachines.BeginUpdate();
                        try
                        {
                            List<IMachine> machines = _machineApiWrapper.MachinesGet();

                            foreach (IMachine machine in machines)
                            {
                                ListViewItem newNode = listViewMachines.Items.Add(machine.Name);
                                newNode.Tag = machine;
                                newNode.SubItems.Add(machine.ComPort);
                                newNode.SubItems.Add(machine.MachineType.ToString());
                                newNode.SubItems.Add(GSend.Language.Resources.No);
                                newNode.SubItems.Add(String.Empty); // status
                                newNode.SubItems.Add(String.Empty); // cpu

                                if (machine.MachineType == MachineType.Printer)
                                    newNode.ImageIndex = 0;
                                else
                                    newNode.ImageIndex = 1;

                                newNode.UseItemStyleForSubItems = false;

                                _machines.Add(machine.Id, newNode);
                            }
                        }
                        finally
                        {
                            listViewMachines.EndUpdate();
                        }
                    }
                });
            }
        }

        private void timerUpdateStatus_Tick(object sender, EventArgs e)
        {
            _clientWebSocket.SendAsync(MessageMachineStatusAll).ConfigureAwait(false);

            string connectedText = _clientWebSocket.State == WebSocketState.Open ?
                GSend.Language.Resources.ServerConnected :
                GSend.Language.Resources.ServerNotConnected;

            if (!toolStripStatusConnected.Text.Equals(connectedText))
            {
                toolStripStatusConnected.Text = connectedText;

                if (_clientWebSocket.State == WebSocketState.Open)
                {
                    UpdateMachines();
                }
            }
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            timerUpdateStatus.Enabled = true;
        }

        private void listViewMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedMachine = listViewMachines.SelectedItems.Count == 0 ? null : listViewMachines.SelectedItems[0].Tag as IMachine;
            UpdateEnabledState();
        }

        protected override void UpdateEnabledState()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(UpdateEnabledState));
                return;
            }

            ListViewItem selectedItem = GetSelectedMachine();
            bool machineSelected = selectedItem != null;

            bool isConnected = selectedItem?.SubItems[3].Text.Equals(GSend.Language.Resources.Yes) ?? false;
            bool clearAlarm = selectedItem?.SubItems[4].BackColor == Color.Red;
            bool isIdle = selectedItem?.SubItems[4].Text.Equals(GSend.Language.Resources.StateIdle) ?? false;

            toolStripButtonPauseAll.Enabled = listViewMachines.Items.Count > 0;
            toolStripButtonResumeAll.Enabled = listViewMachines.Items.Count > 0;

            toolStripButtonConnect.Enabled = machineSelected && !isConnected;
            toolStripButtonDisconnect.Enabled = machineSelected && isConnected;

            toolStripButtonClearAlarm.Enabled = machineSelected && isConnected && clearAlarm;
            toolStripButtonResume.Enabled = machineSelected && isConnected && !clearAlarm;
            toolStripButtonHome.Enabled = machineSelected && isConnected && !clearAlarm && isIdle;
        }

        private ListViewItem GetSelectedMachine()
        {
            return listViewMachines.SelectedItems.Count == 0 ? null : listViewMachines.SelectedItems[0];
        }

        protected override void LoadSettings()
        {
            base.LoadSettings();
            LoadSettings(listViewMachines);
        }

        protected override void SaveSettings()
        {
            base.SaveSettings();
            SaveSettings(listViewMachines);
        }

        protected override void LoadResources()
        {
            // list view headers
            columnHeaderComPort.Text = GSend.Language.Resources.Port;
            columnHeaderConnected.Text = GSend.Language.Resources.Connected;
            columnHeaderCpu.Text = GSend.Language.Resources.CPUUsage;
            columnHeaderMachineType.Text = GSend.Language.Resources.Type;
            columnHeaderName.Text = GSend.Language.Resources.Name;
            columnHeaderStatus.Text = GSend.Language.Resources.Status;

            // toolbar buttons
            toolStripButtonGetMachines.Text = GSend.Language.Resources.MachineRefresh;
            toolStripButtonGetMachines.ToolTipText = GSend.Language.Resources.MachineRefresh;
            toolStripButtonAddMachine.Text = GSend.Language.Resources.MachineAdd;
            toolStripButtonAddMachine.ToolTipText = GSend.Language.Resources.MachineAdd;
            toolStripButtonRemoeMachine.Text = GSend.Language.Resources.MachineRemove;
            toolStripButtonRemoeMachine.ToolTipText = GSend.Language.Resources.MachineRemove;
            toolStripButtonPauseAll.Text = GSend.Language.Resources.PauseAll;
            toolStripButtonPauseAll.ToolTipText = GSend.Language.Resources.PauseAll;
            toolStripButtonResumeAll.Text = GSend.Language.Resources.ResumeAll;
            toolStripButtonResumeAll.ToolTipText = GSend.Language.Resources.ResumeAll;
            toolStripButtonConnect.Text = GSend.Language.Resources.Connect;
            toolStripButtonConnect.ToolTipText = GSend.Language.Resources.Connect;
            toolStripButtonDisconnect.Text = GSend.Language.Resources.Disconnect;
            toolStripButtonDisconnect.ToolTipText = GSend.Language.Resources.Disconnect;
            toolStripButtonClearAlarm.Text = GSend.Language.Resources.ClearAlarm;
            toolStripButtonClearAlarm.ToolTipText = GSend.Language.Resources.ClearAlarm;
            toolStripButtonResume.Text = GSend.Language.Resources.Resume;
            toolStripButtonResume.ToolTipText = GSend.Language.Resources.Resume;
            toolStripButtonHome.Text = GSend.Language.Resources.Home;
            toolStripButtonHome.ToolTipText = GSend.Language.Resources.Home;


            // status strip 
            toolStripStatusCpu.Text = GSend.Language.Resources.ServerCpuStateDisconnected;


            // menu items
            machineToolStripMenuItem.Text = GSend.Language.Resources.Machine;
            refreshToolStripMenuItem.Text = GSend.Language.Resources.MachineRefresh;
            addMachineToolStripMenuItem.Text = GSend.Language.Resources.MachineAdd;
            removeMachineToolStripMenuItem.Text = GSend.Language.Resources.MachineRemove;

            viewToolStripMenuItem.Text = GSend.Language.Resources.View;
            mnuViewLargeIcons.Text = GSend.Language.Resources.LargeIcons;
            mnuViewDetails.Text = GSend.Language.Resources.Details;

            subprogramsToolStripMenuItem.Text = GSend.Language.Resources.SubPrograms;
            viewSubProgramToolStripMenuItem.Text = GSend.Language.Resources.View;

            helpToolStripMenuItem.Text = GSend.Language.Resources.Help;
            aboutToolStripMenuItem.Text = GSend.Language.Resources.HelpAbout;
        }

        private void listViewMachines_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem machineListItem = GetSelectedMachine();

            if (machineListItem == null)
                return;

            IMachine machine = machineListItem.Tag as IMachine;

            if (machine == null)
                return;

            _context.ShowMachine(machine);
        }

        #endregion Form Methods

        #region Menu Items

        private void mnuViewLargeIcons_Click(object sender, EventArgs e)
        {
            listViewMachines.View = View.LargeIcon;
        }

        private void mnuViewSmallIcons_Click(object sender, EventArgs e)
        {
            listViewMachines.View = View.Details;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox.ShowAboutBox(GSend.Language.Resources.AppNameSender, this.Icon);
        }

        #endregion Menu Items

        private void FormMain_Activated(object sender, EventArgs e)
        {
            ValidateServerConnection();
        }

        private void ValidateServerConnection()
        {
            IGSendApiWrapper apiWrapper = _context.ServiceProvider.GetRequiredService<IGSendApiWrapper>();

            FrmServerValidation.ValidateServer(this, apiWrapper);
        }

        #region ISenderPluginHost

        public PluginHosts Host => PluginHosts.SenderHost;

        public void AddPlugin(IGSendPluginModule pluginModule)
        {
            // nothing special to do for this host
        }

        public IPluginMenu GetMenu(MenuParent menuParent)
        {
            switch (menuParent)
            {
                case MenuParent.Machine:
                    return machineToolStripMenuItem.Tag as IPluginMenu;

                case MenuParent.Subprograms:
                    return subprogramsToolStripMenuItem.Tag as IPluginMenu;

                case MenuParent.Help:
                    return helpToolStripMenuItem.Tag as IPluginMenu;
            }

            return null;
        }

        public void AddMenu(IPluginMenu pluginMenu)
        {
            pluginMenu.UpdateHost(this as IPluginHost);
            _pluginHelper.AddMenu(menuStripMain, pluginMenu, null);
        }

        public void AddToolbar(IPluginToolbarButton toolbarButton)
        {
            toolbarButton.UpdateHost(this as IEditorPluginHost);
            _pluginHelper.AddToolbarButton(toolStripMain, toolbarButton);
        }

        public void AddMessage(InformationType informationType, string message)
        {
            throw new InvalidOperationException();
        }

        #endregion ISenderPluginHost
    }
}