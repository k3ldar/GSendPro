using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GSendShared;

namespace GSendDesktop.Controls
{
    public partial class MachinePosition : UserControl
    {
        public MachinePosition()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        public DisplayUnits DisplayMeasurements { get; set; }

        public void UpdateWorkPosition(double x, double y, double z)
        {
            lblWorkX.Text = HelperMethods.ConvertMeasurementForDisplay(DisplayMeasurements, x);
            lblWorkY.Text = HelperMethods.ConvertMeasurementForDisplay(DisplayMeasurements, y);
            lblWorkZ.Text = HelperMethods.ConvertMeasurementForDisplay(DisplayMeasurements, z);
        }

        public void UpdateMachinePosition(double x, double y, double z)
        {
            lblMachineX.Text = HelperMethods.ConvertMeasurementForDisplay(DisplayMeasurements, x);
            lblMachineY.Text = HelperMethods.ConvertMeasurementForDisplay(DisplayMeasurements, y);
            lblMachineZ.Text = HelperMethods.ConvertMeasurementForDisplay(DisplayMeasurements, z);
        }
    }
}
