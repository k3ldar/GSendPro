using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSendApi;

using GSendShared;
using GSendShared.Models;

namespace GSendDesktop.Forms
{
    public partial class FrmRegisterService : Form
    {
        private readonly MachineApiWrapper _machineApiWrapper;
        private readonly long _machineId;

        public FrmRegisterService()
        {
            InitializeComponent();

            dateTimeServiceDate.Value = DateTime.UtcNow;
            dateTimeServiceDate.CustomFormat = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.FullDateTimePattern;
            LoadResources();
        }

        public FrmRegisterService(long machineId, MachineApiWrapper machineApiWrapper)
            : this()
        {
            _machineApiWrapper = machineApiWrapper ?? throw new ArgumentNullException(nameof(machineApiWrapper));
            _machineId = machineId;
        }

        public void LoadResources()
        {
            Text = GSend.Language.Resources.RegisterService;

            lblServiceDate.Text = GSend.Language.Resources.ServiceDate;
            grpServiceType.Text = GSend.Language.Resources.ServiceType;
            rbDaily.Text = GSend.Language.Resources.ServiceTypeDaily;
            rbMinor.Text = GSend.Language.Resources.ServiceTypeMinor;
            rbMajor.Text = GSend.Language.Resources.ServiceTypeMajor;
            btnCancel.Text = GSend.Language.Resources.Cancel;
            btnOk.Text = GSend.Language.Resources.OK;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ServiceType serviceType = ServiceType.Daily;

            if (rbMajor.Checked)
                serviceType = ServiceType.Major;
            else if (rbMinor.Checked)
                serviceType = ServiceType.Minor;

            List<MachineServiceModel> services = _machineApiWrapper.MachineServices(_machineId);
            DateTime latestService = services.Max(s => s.ServiceDate);

            List<SpindleHoursModel> spindleHours = _machineApiWrapper.GetSpindleTime(_machineId, latestService);

            long totalTicks = spindleHours.Where(tt => tt.TotalTime.Ticks > 0).Sum(sh => sh.TotalTime.Ticks);

            _machineApiWrapper.MachineServiceAdd(_machineId, dateTimeServiceDate.Value, serviceType, totalTicks);
            DialogResult = DialogResult.OK;
        }
    }
}
