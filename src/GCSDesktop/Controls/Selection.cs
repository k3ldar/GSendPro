using System;
using System.ComponentModel;
using System.Windows.Forms;

using GSendShared;

namespace GSendDesktop.Controls
{
    public partial class Selection : UserControl, IFeedRateUnitUpdate
    {
        public Selection()
        {
            InitializeComponent();
            trackBarValue.MouseWheel += TrackBarValue_MouseWheel;
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
        public int SmallTickChange
        {
            get => trackBarValue.SmallChange;
            set => trackBarValue.SmallChange = value;
        }

        [Browsable(true)]
        public int LargeTickChange
        {
            get => trackBarValue.LargeChange;
            set => trackBarValue.LargeChange = value;
        }

        [Browsable(true)]
        public FeedRateDisplayUnits FeedRateDisplay { get; set; } = FeedRateDisplayUnits.MmPerMinute;

        [Browsable(true)]
        public bool HasDisplayUnits { get; set; } = false;

        [Browsable(true)]
        public event EventHandler ValueChanged;

        [Browsable(true)]
        public bool HandleMouseWheel { get; set; } = false;

        public void UpdateFeedRateDisplay()
        {
            trackBarValue_ValueChanged(this, EventArgs.Empty);
        }

        private void trackBarValue_ValueChanged(object sender, System.EventArgs e)
        {
            if (!DesignMode)
            {
                if (HasDisplayUnits)
                    LabelValue = String.Format(LabelFormat, HelperMethods.ConvertFeedRateForDisplay(FeedRateDisplay, trackBarValue.Value));
                else
                    LabelValue = String.Format(LabelFormat, trackBarValue.Value);

                ValueChanged?.Invoke(this, EventArgs.Empty);
                lblDescription.Text = LabelValue;
            }
        }

        private void TrackBarValue_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!HandleMouseWheel)
                return;

            ((HandledMouseEventArgs)e).Handled = true;

            if (e.Delta > 0)
            {
                if (trackBarValue.Value < trackBarValue.Maximum)
                    trackBarValue.Value++;
            }
            else
            {
                if (trackBarValue.Value > trackBarValue.Minimum)
                    trackBarValue.Value--;
            }
        }
    }
}
