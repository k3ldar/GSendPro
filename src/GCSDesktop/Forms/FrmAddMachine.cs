using System;
using System.Windows.Forms;

using GSendApi;

using GSendDesktop.Abstractions;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Models;

using GSendControls;

namespace GSendDesktop.Forms
{
    public partial class FrmAddMachine : BaseForm
    {
        private readonly GSendApiWrapper _machineApiWrapper;
        private readonly IComPortProvider _portProvider;
        private readonly IMessageNotifier _messageNotifier;

        public FrmAddMachine(IComPortProvider comPortProvider, GSendApiWrapper machineApiWrapper, IMessageNotifier messageNotifier)
        {
            InitializeComponent();
            _portProvider = comPortProvider ?? throw new ArgumentNullException(nameof(comPortProvider));
            _machineApiWrapper = machineApiWrapper ?? throw new ArgumentNullException(nameof(machineApiWrapper));
            _messageNotifier = messageNotifier ?? throw new ArgumentNullException(nameof(messageNotifier));

            UpdateAvailableComports();
        }

        protected override string SectionName => nameof(FrmAddMachine);

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
                if (_machineApiWrapper.MachineNameExists(txtName.Text))
                {
                    _messageNotifier.ShowMessage(GSend.Language.Resources.MachineNameAlreadyExists);
                    return;
                }

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
