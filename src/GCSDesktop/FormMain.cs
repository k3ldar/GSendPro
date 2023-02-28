using System;
using System.Collections.Generic;
using System.Windows.Forms;

using GSendApi;

using GSendDesktop.Abstractions;
using GSendDesktop.Forms;
using GSendDesktop.Internal;

using GSendShared;
using GSendShared.Models;

using Microsoft.Extensions.DependencyInjection;

namespace GSendDesktop
{
    public partial class FormMain : Form
    {
        private readonly MachineApiWrapper _machineApiWrapper;
        private readonly IMessageNotifier _messageNotifier;
        private readonly ICommandProcessor _processCommand;

        public FormMain(MachineApiWrapper machineApiWrapper, IMessageNotifier messageNotifier, ICommandProcessor processCommand)
        {
            InitializeComponent();
            _machineApiWrapper = machineApiWrapper ?? throw new ArgumentNullException(nameof(machineApiWrapper));
            _messageNotifier = messageNotifier ?? throw new ArgumentNullException(nameof(messageNotifier));
            _processCommand = processCommand ?? throw new ArgumentNullException(nameof(processCommand));
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