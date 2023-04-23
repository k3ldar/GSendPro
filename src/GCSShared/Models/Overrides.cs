namespace GSendShared.Models
{
    public sealed class OverrideModel
    {
        public OverrideModel()
        {
            AxisXY = new OverrideValue();
            AxisZDown = new OverrideValue();
            AxisZUp = new OverrideValue();
            Spindle = new OverrideValue();
        }

        public RapidsOverride Rapids { get; set; }

        public OverrideValue AxisXY { get; set; }

        public OverrideValue AxisZUp { get; set; }

        public OverrideValue AxisZDown { get; set; }

        public OverrideValue Spindle { get; set; }

        public bool OverrideRapids { get; set; }

        public bool OverrideXY { get; set; }

        public bool OverrideZUp { get; set; }

        public bool OverrideZDown { get; set; }

        public bool OverrideSpindle { get; set; }

        public bool OverridesEnabled { get; set; }
    }
}
