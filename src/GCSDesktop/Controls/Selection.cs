using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace GSendDesktop.Controls
{
    public partial class Selection : UserControl
    {
        public Selection()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        public string GroupName
        {
            get => groupBoxMain.Text;

            set => groupBoxMain.Text = value;
        }

        [Browsable(true)]
        public string LabelFormat { get; set; } = "{0}";

        [Browsable(true)]
        public int Maximum
        {
            get => trackBarValue.Maximum;
            set => trackBarValue.Maximum = value;
        }

        [Browsable(true)]
        public int Minimum
        {
            get => trackBarValue.Minimum;
            set => trackBarValue.Minimum = value;
        }

        [Browsable(true)]
        public int TickFrequency
        {
            get => trackBarValue.TickFrequency;
            set => trackBarValue.TickFrequency = value;
        }

        [Browsable(true)]
        public int Value
        {
            get => trackBarValue.Value;
            set => trackBarValue.Value = value; 
        }

        public string LabelValue { get; set; }

        [Browsable(true)]
        public event EventHandler ValueChanged;

        private void trackBarValue_ValueChanged(object sender, System.EventArgs e)
        {
            if (!DesignMode)
            {
                LabelValue = String.Format(LabelFormat, trackBarValue.Value);
                ValueChanged?.Invoke(this, EventArgs.Empty);
                lblDescription.Text = LabelValue;
            }
        }
    }
}
