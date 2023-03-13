using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using GSendApi;

using GSendDesktop.Abstractions;
using GSendDesktop.Forms;
using GSendDesktop.Internal;

using GSendShared;
using GSendShared.Models;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json.Linq;

using Shared.Classes;

namespace GSendDesktop
{
    public partial class FormMain : Form
    {
        private readonly MachineApiWrapper _machineApiWrapper;
        private readonly IMessageNotifier _messageNotifier;
        private readonly ICommandProcessor _processCommand;
        private readonly ClientWebSocket _clientWebSocket;
        private readonly object _lockObject = new();

        public FormMain(MachineApiWrapper machineApiWrapper, IMessageNotifier messageNotifier, ICommandProcessor processCommand)
        {
            InitializeComponent();
            _machineApiWrapper = machineApiWrapper ?? throw new ArgumentNullException(nameof(machineApiWrapper));
            _messageNotifier = messageNotifier ?? throw new ArgumentNullException(nameof(messageNotifier));
            _processCommand = processCommand ?? throw new ArgumentNullException(nameof(processCommand));
            _clientWebSocket = SetupWebSocket();


            //Receive(_clientWebSocket);

            

        }

        private ClientWebSocket SetupWebSocket()
        {
            ClientWebSocket result = new ClientWebSocket();
            
            result.Options.KeepAliveInterval = TimeSpan.FromMinutes(5);

            //Set desired headers
            //wsClient.Options.SetRequestHeader("Host", _host);

            //Add sub protocol if it's needed
            //result.Options.AddSubProtocol("application/json");
            

            result.ConnectAsync(new Uri("wss://localhost:7154/client"), CancellationToken.None).ConfigureAwait(false);
            return result;
        }

        private async Task Receive(ClientWebSocket webSocket)
        {
            byte[] buffer = new byte[1024];
            while (webSocket.State == WebSocketState.Open || webSocket.State == WebSocketState.Connecting)
            {
                if (webSocket.State == WebSocketState.Connecting)
                    continue;

                WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None).ConfigureAwait(false);
                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                   // webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None).ConfigureAwait(true);
                }
                else
                {
                    string request = Encoding.UTF8.GetString(buffer);

                    using (TimedLock tl = TimedLock.Lock(_lockObject))
                    {
                        ProcessRequest(request[..receiveResult.Count]);
                    }
                }
            }
        }

        private void ProcessRequest(string buffer)
        {
            if (buffer == "")
                return;
        }

        private void toolStripButtonGetMachines_Click(object sender, EventArgs e)
        {
            _processCommand.ProcessCommand(() =>
            {
                using (MouseControl mouseControl = MouseControl.ShowWaitCursor(this))
                {
                    listView1.Items.Clear();

                    List<IMachine> machines = _machineApiWrapper.MachinesGet();

                    foreach (IMachine machine in machines)
                    {
                        ListViewItem newNode = listView1.Items.Add(machine.Name);
                        newNode.SubItems.Add(machine.ComPort);
                        newNode.SubItems.Add(machine.MachineType.ToString());

                        if (machine.MachineType == MachineType.Printer)
                            newNode.ImageIndex = 0;
                        else
                            newNode.ImageIndex = 1;

                    }
                }
            });
        }

        private void toolStripButtonAddMachine_Click(object sender, EventArgs e)
        {
            using FrmAddMachine frmAddMachine = Program.Services.GetService<FrmAddMachine>();
                
            if (frmAddMachine.ShowDialog(this) == DialogResult.OK)
            {
                toolStripButtonGetMachines_Click(sender, e);
            }
        }

    }
}