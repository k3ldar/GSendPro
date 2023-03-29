using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSendDesktop.Controls
{
    public partial class ProbingCommand : UserControl
    {
        public ProbingCommand()
        {
            InitializeComponent();
            lblProbeThickness.Text = GSend.Language.Resources.ProbeThickness;
            lblTravelSpeed.Text = String.Format(GSend.Language.Resources.ProbeTravelSpeed, trackBarTravelSpeed.Value);
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

        private void trackBarTravelSpeed_ValueChanged(object sender, EventArgs e)
        {
            lblTravelSpeed.Text = String.Format(GSend.Language.Resources.ProbeTravelSpeed, trackBarTravelSpeed.Value);
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
