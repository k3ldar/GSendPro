using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

using GSendApi;

using GSendCommon;

using GSendDesktop.Abstractions;
using GSendDesktop.Forms;
using GSendDesktop.Internal;

using GSendShared;

using Microsoft.Extensions.DependencyInjection;

using Shared.Classes;

using static GSendShared.Constants;

namespace GSendDesktop
{
    public partial class FormMain : Form
    {
        private readonly IGSendContext _context;

        private readonly MachineApiWrapper _machineApiWrapper;
        private readonly IMessageNotifier _messageNotifier;
        private readonly ICommandProcessor _processCommand;
        private readonly GSendWebSocket _clientWebSocket;
        private readonly object _lockObject = new();
        private readonly CancellationTokenRegistration _cancellationTokenRegistration;
        private readonly Dictionary<long, ListViewItem> _machines = new();
        private readonly ConcurrentDictionary<long, bool> _machineStateModel = new();
        private IMachine _selectedMachine = null;

        public FormMain(IGSendContext context, MachineApiWrapper machineApiWrapper,
            IMessageNotifier messageNotifier, ICommandProcessor processCommand, GSendSettings settings)
        {
            InitializeComponent();

            _context = context ?? throw new ArgumentNullException(nameof(context));
            _machineApiWrapper = machineApiWrapper ?? throw new ArgumentNullException(nameof(machineApiWrapper));
            _messageNotifier = messageNotifier ?? throw new ArgumentNullException(nameof(messageNotifier));
            _processCommand = processCommand ?? throw new ArgumentNullException(nameof(processCommand));

            _cancellationTokenRegistration = new();
            _clientWebSocket = new GSendWebSocket(_cancellationTokenRegistration.Token, nameof(FormMain));
            _clientWebSocket.ProcessMessage += ClientWebSocket_ProcessMessage;
            _clientWebSocket.ConnectionLost += ClientWebSocket_ConnectionLost;
            _clientWebSocket.Connected += ClientWebSocket_Connected;
            timerUpdateStatus.Interval = settings.UpdateMilliseconds;
        }

        #region Client Web Socket

        private void ClientWebSocket_Connected(object sender, EventArgs e)
        {

        }

        private void ClientWebSocket_ConnectionLost(object sender, EventArgs e)
        {

        }

        private void ClientWebSocket_ProcessMessage(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            ClientBaseMessage clientMessage = JsonSerializer.Deserialize<ClientBaseMessage>(message);

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


            if (clientMessage.request.Equals(MessageMachineStatusAll))
            {
                List<StatusResponseMessage> statuses = JsonSerializer.Deserialize<List<StatusResponseMessage>>(clientMessage.message.ToString());

                UpdateMachineStatus(statuses);
            }

            UpdateEnabledState();
        }

        private void ProcessClientConnectMessage(ConnectResult connectResult)
        {
            if (InvokeRequired)
            {
                Invoke(ProcessClientConnectMessage, connectResult);
                return;
            }


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
                Invoke(UpdateMachineStatus, new object[] { statuses });
                return;
            }

            if (statuses.Count != _machines.Count)
                UpdateMachines();

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

                    Color backLineColor = Color.White;
                    Color foreLineColor = Color.Black;
                    string configUpdate = String.Empty;
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
            using FrmAddMachine frmAddMachine = _context.ServiceProvider.GetService<FrmAddMachine>();

            if (frmAddMachine.ShowDialog(this) == DialogResult.OK)
            {
                UpdateMachines();
            }
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

        private void UpdateEnabledState()
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateResources();
            UpdateEnabledState();
        }

        private void UpdateResources()
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

            subProgramsToolStripMenuItem.Text = GSend.Language.Resources.SubPrograms;
            viewSubProgramToolStripMenuItem.Text = GSend.Language.Resources.View;

            helpToolStripMenuItem.Text = GSend.Language.Resources.Help;
            viewHelpToolStripMenuItem.Text = GSend.Language.Resources.HelpView;
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

        #endregion Menu Items
    }
}