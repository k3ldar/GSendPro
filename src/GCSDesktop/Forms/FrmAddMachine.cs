using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using GSendApi;

using GSendDesktop.Abstractions;

using GSendShared;
using GSendShared.Interfaces;
using GSendShared.Models;

namespace GSendDesktop.Forms
{
    public partial class FrmAddMachine : Form
    {
        private readonly MachineApiWrapper _machineApiWrapper;
        private readonly IComPortProvider _portProvider;
        private readonly IMessageNotifier _messageNotifier;

        public FrmAddMachine(IComPortProvider comPortProvider, MachineApiWrapper machineApiWrapper, IMessageNotifier messageNotifier)
        {
            InitializeComponent();
            _portProvider = comPortProvider ?? throw new ArgumentNullException(nameof(comPortProvider));
            _machineApiWrapper = machineApiWrapper ?? throw new ArgumentNullException(nameof(machineApiWrapper));
            _messageNotifier = messageNotifier ?? throw new ArgumentNullException(nameof(messageNotifier));

            UpdateAvailableComports();
        }

        private void UpdateAvailableComports()
        {
            cmbComPort.Items.Clear();
            string[] ports = _portProvider.AvailablePorts();

            for (int i = 0; i < ports.Length; i++)
            {
                cmbComPort.Items.Add(ports[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateAvailableComports();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IMachine machine = new MachineModel()
            {
                ComPort = cmbComPort.Text,
                MachineType = (MachineType)Enum.Parse(typeof(MachineType), cmbMachineType.Text),
                Name = txtName.Text,
            };

            try
            {
                _machineApiWrapper.MachineAdd(machine);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                _messageNotifier.ShowMessage(ex.Message);
            }
        }
    }
}
