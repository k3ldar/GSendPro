using System;
using System.ComponentModel;
using System.Windows.Forms;

using GSendShared;

namespace GSendDesktop.Controls
{
    public partial class ProbingCommand : UserControl, IFeedRateUnitUpdate
    {
        public ProbingCommand()
        {
            InitializeComponent();
            lblProbeThickness.Text = GSend.Language.Resources.ProbeThickness;
            lblProbeCommand.Text = GSend.Language.Resources.ProbeCommand;
            btnSave.Text = GSend.Language.Resources.Save;
            btnGenerate.Text = GSend.Language.Resources.Generate;
        }

        public void InitializeProbingCommand(string existingCommand, int travelSpeed, decimal thickness)
        {
            txtProbeCommand.Text = existingCommand;
            trackBarTravelSpeed.Value = Math.Max(travelSpeed, trackBarTravelSpeed.Minimum);
            numericProbeThickness.Value = Math.Max(thickness, numericProbeThickness.Minimum);
            btnSave.Enabled = false;
        }

        public decimal Thickness => numericProbeThickness.Value;

        public int TravelSpeed => trackBarTravelSpeed.Value;

        public string ProbeCommand => txtProbeCommand.Text.Trim();

        public event EventHandler OnSave;

        public void UpdateFeedRateDisplay()
        {
            trackBarTravelSpeed_ValueChanged(this, EventArgs.Empty);
        }

        [Browsable(true)]
        public FeedRateDisplayUnits FeedRateDisplay { get; set; }

        private void trackBarTravelSpeed_ValueChanged(object sender, EventArgs e)
        {
            lblTravelSpeed.Text = String.Format(GSend.Language.Resources.ProbeTravelSpeed,
                HelperMethods.ConvertFeedRateForDisplay(FeedRateDisplay, trackBarTravelSpeed.Value),
                HelperMethods.TranslateDisplayUnit(FeedRateDisplay));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            txtProbeCommand.Text = String.Format(GSendShared.Constants.ProbeCommand, trackBarTravelSpeed.Value, numericProbeThickness.Value);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OnSave?.Invoke(this, EventArgs.Empty);
        }
    }
}
