using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly ClientWebSocket _clientWebSocket;
        private readonly object _lockObject = new();
        private readonly CancellationTokenRegistration _cancellationTokenRegistration;
        private readonly Dictionary<long, ListViewItem> _machines = new();
        private IMachine _selectedMachine = null;

        public FormMain(IGSendContext context, MachineApiWrapper machineApiWrapper, 
            IMessageNotifier messageNotifier, ICommandProcessor processCommand)
        {
            InitializeComponent();

            _context = context ?? throw new ArgumentNullException(nameof(context));
            _machineApiWrapper = machineApiWrapper ?? throw new ArgumentNullException(nameof(machineApiWrapper));
            _messageNotifier = messageNotifier ?? throw new ArgumentNullException(nameof(messageNotifier));
            _processCommand = processCommand ?? throw new ArgumentNullException(nameof(processCommand));

            _cancellationTokenRegistration = new();
            _clientWebSocket = SetupWebSocket();
            Task.Run(() => ReceiveMessageAsync(_cancellationTokenRegistration.Token)).ConfigureAwait(false);
        }

        #region Client Web Socket

        private ClientWebSocket SetupWebSocket()
        {
            ClientWebSocket result = new ClientWebSocket();

            result.Options.KeepAliveInterval = TimeSpan.FromMinutes(5);
            ConnectToWebSocket(result);
            return result;
        }

        private static void ConnectToWebSocket(ClientWebSocket result)
        {
            result.ConnectAsync(new Uri(ServerUri), CancellationToken.None).ConfigureAwait(false);
        }

        public async Task SendAsync(string message, CancellationToken token)
        {
            if (_clientWebSocket.State != WebSocketState.Open)
                return;

            byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
            await _clientWebSocket.SendAsync(new ArraySegment<byte>(messageBuffer), WebSocketMessageType.Text, true, token).ConfigureAwait(false);
        }

        private async Task ReceiveMessageAsync(CancellationToken token)
        {
            byte[] buffer = new byte[ReceiveBufferSize];

            while (true)
            {
                if (_clientWebSocket.State != WebSocketState.Open)
                    continue;

                try
                {
                    WebSocketReceiveResult result = await _clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), token);

                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    ProcessMessage(message[..result.Count]);
                    Trace.WriteLine($"Message Received: {message[..result.Count]}");
                }
                catch (IOException)
                {
                    Trace.WriteLine("IO Exception");
                }
                catch (InvalidOperationException)
                {
                    Trace.WriteLine("Invalid operation");
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Error in receiving messages: {err}", ex.Message);
                    break;
                }
            }

            toolStripStatusCpu.Text = GSend.Language.Resources.ServerCpuStateDisconnected;
        }

        private void ProcessMessage(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            ClientBaseMessage clientMessage = JsonSerializer.Deserialize<ClientBaseMessage>(message);

            if (!clientMessage.success)
                return;

            string serverCpu = String.Format(GSend.Language.Resources.ServerCpuStateConnected, clientMessage.ServerCpuStatus.ToString("N2"));

            if (!toolStripStatusCpu.Text.Equals(serverCpu))
                toolStripStatusCpu.Text = serverCpu;


            if (clientMessage.request.Equals(MessageMachineStatus))
            {
                List<StatusResponseMessage> statuses = JsonSerializer.Deserialize<List<StatusResponseMessage>>(clientMessage.message.ToString());

                UpdateMachineStatus(statuses);
            }

            UpdateEnabledState();
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
                        if (!machineItem.SubItems[3].Text.Equals(connectedText))
                            machineItem.SubItems[3].Text = connectedText;

                        if (!machineItem.SubItems[4].Text.Equals(status.State))
                        {
                            Color backColor = Color.White;
                            Color foreColor = Color.Black;
                            string text = TranslateState(status.State);

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

                                case StateRun:
                                    backColor = Color.Green;
                                    foreColor = Color.White;
                                    break;

                                case StateJog:
                                    backColor = Color.Yellow;
                                    foreColor = Color.Black;
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
            using FrmAddMachine frmAddMachine = _context.ServiceProvider.GetService<FrmAddMachine>();

            if (frmAddMachine.ShowDialog(this) == DialogResult.OK)
            {
                UpdateMachines();
            }
        }

        private void toolStripButtonPauseAll_Click(object sender, EventArgs e)
        {
            SendAsync(MessageMachinePauseAll, _cancellationTokenRegistration.Token).ConfigureAwait(false);
        }

        private void toolStripButtonResumeAll_Click(object sender, EventArgs e)
        {
            SendAsync(MessageMachineResumeAll, _cancellationTokenRegistration.Token).ConfigureAwait(false);
        }

        private void toolStripButtonConnect_Click(object sender, EventArgs e)
        {
            if (_selectedMachine != null)
            {
                SendAsync(String.Format(MessageMachineConnect, _selectedMachine.Id), _cancellationTokenRegistration.Token).ConfigureAwait(false);
            }
        }

        private void toolStripButtonClearAlarm_Click(object sender, EventArgs e)
        {
            if (_selectedMachine != null)
            {
                SendAsync(String.Format(MessageMachineClearAlarm, _selectedMachine.Id), _cancellationTokenRegistration.Token).ConfigureAwait(false);
            }
        }

        private void toolStripButtonResume_Click(object sender, EventArgs e)
        {
            if (_selectedMachine != null)
            {
                SendAsync(String.Format(MessageMachineResume, _selectedMachine.Id), _cancellationTokenRegistration.Token).ConfigureAwait(false);
            }
        }

        private void toolStripButtonHome_Click(object sender, EventArgs e)
        {
            if (_selectedMachine != null)
            {
                SendAsync(String.Format(MessageMachineHome, _selectedMachine.Id), _cancellationTokenRegistration.Token).ConfigureAwait(false);
            }
        }

        #endregion ToolBar

        #region Form Methods

        private string TranslateState(string state)
        {
            switch (state)
            {
                case StateUndefined:
                    return GSend.Language.Resources.StatePortOpen;
                case StateIdle:
                    return GSend.Language.Resources.StateIdle;
                case StateRun:
                    return GSend.Language.Resources.StateRun;
                case StateJog:
                    return GSend.Language.Resources.StateJog;
                case StateAlarm:
                    return GSend.Language.Resources.StateAlarm;
                case StateDoor:
                    return GSend.Language.Resources.StateDoor;
                case StateCheck:
                    return GSend.Language.Resources.StateCheck;
                case StateHome:
                    return GSend.Language.Resources.StateHome;
                case StateSleep:
                    return GSend.Language.Resources.StateSleep;
                default:
                    return GSend.Language.Resources.StateUnknown;
            }
        }

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
            switch (_clientWebSocket.State)
            {
                case WebSocketState.Aborted:
                case WebSocketState.Closed:
                    ConnectToWebSocket(_clientWebSocket);
                    break;
            }

            SendAsync(MessageMachineStatus, _cancellationTokenRegistration.Token).ConfigureAwait(false);

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
            columnHeaderComPort.Text = GSend.Language.Resources.Port;
            columnHeaderConnected.Text = GSend.Language.Resources.Connected;
            columnHeaderCpu.Text = GSend.Language.Resources.CPUUsage;
            columnHeaderMachineType.Text = GSend.Language.Resources.Type;
            columnHeaderName.Text = GSend.Language.Resources.Name;
            columnHeaderStatus.Text = GSend.Language.Resources.Status;
           

            toolStripButtonGetMachines.Text = GSend.Language.Resources.MachineRefresh;
            toolStripButtonGetMachines.ToolTipText = GSend.Language.Resources.MachineRefresh;
            toolStripButtonAddMachine.Text = GSend.Language.Resources.MachineAdd;
            toolStripButtonAddMachine.ToolTipText = GSend.Language.Resources.MachineAdd;
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


            
            toolStripStatusCpu.Text = GSend.Language.Resources.ServerCpuStateDisconnected;
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
    }
}