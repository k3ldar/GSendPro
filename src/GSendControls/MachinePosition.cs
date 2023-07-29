using System.ComponentModel;
using System.Windows.Forms;

using GSendShared;

namespace GSendControls
{
    public partial class MachinePosition : UserControl
    {
        public MachinePosition()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        public FeedbackUnit DisplayFeedbackUnit { get; set; }

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
            UpdateLabel(lblWorkX, HelperMethods.ConvertMeasurementForDisplay(DisplayFeedbackUnit, x));
            UpdateLabel(lblWorkY, HelperMethods.ConvertMeasurementForDisplay(DisplayFeedbackUnit, y));
            UpdateLabel(lblWorkZ, HelperMethods.ConvertMeasurementForDisplay(DisplayFeedbackUnit, z));
        }

        public void UpdateMachinePosition(double x, double y, double z)
        {
            UpdateLabel(lblMachineX, HelperMethods.ConvertMeasurementForDisplay(DisplayFeedbackUnit, x));
            UpdateLabel(lblMachineY, HelperMethods.ConvertMeasurementForDisplay(DisplayFeedbackUnit, y));
            UpdateLabel(lblMachineZ, HelperMethods.ConvertMeasurementForDisplay(DisplayFeedbackUnit, z));
        }

        private static void UpdateLabel(Label label, string newText)
        {
            if (!label.Text.Equals(newText))
                label.Text = newText;
        }
    }
}
