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

        public void ResetPositions()
        {
            UpdateLabel(lblWorkX, "-");
            UpdateLabel(lblWorkY, "-");
            UpdateLabel(lblWorkZ, "-");
            UpdateLabel(lblMachineX, "-");
            UpdateLabel(lblMachineY, "-");
            UpdateLabel(lblMachineZ, "-");
        }

        public void UpdateWorkPosition(double x, double y, double z)
        {
            UpdateLabel(lblWorkX, HelperMethods.ConvertMeasurementForDisplay(DisplayMeasurements, x));
            UpdateLabel(lblWorkY, HelperMethods.ConvertMeasurementForDisplay(DisplayMeasurements, y));
            UpdateLabel(lblWorkZ, HelperMethods.ConvertMeasurementForDisplay(DisplayMeasurements, z));
        }

        public void UpdateMachinePosition(double x, double y, double z)
        {
            UpdateLabel(lblMachineX, HelperMethods.ConvertMeasurementForDisplay(DisplayMeasurements, x));
            UpdateLabel(lblMachineY, HelperMethods.ConvertMeasurementForDisplay(DisplayMeasurements, y));
            UpdateLabel(lblMachineZ, HelperMethods.ConvertMeasurementForDisplay(DisplayMeasurements, z));
        }

        private void UpdateLabel(Label label, string newText)
        {
            if (!label.Text.Equals(newText))
                label.Text = newText;
        }
    }
}
