using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Interfaces;

namespace GSendCommon
{
    public sealed class Overrides : IOverrides
    {
        public Overrides()
        {
            Rapids = new OverrideValue();
            AxisXY = new OverrideValue();
            AxisZDown = new OverrideValue();
            AxisZUp = new OverrideValue();
            Spindle = new OverrideValue();
        }

        public IOverriddenValue Rapids { get; }

        public IOverriddenValue AxisXY { get; }

        public IOverriddenValue AxisZUp { get; }

        public IOverriddenValue AxisZDown { get; }

        public IOverriddenValue Spindle { get; }

        public bool OverrideRapids { get; set; }

        public bool OverrideXY { get; set; }

        public bool OverrideZUp { get; set; }

        public bool OverrideZDown { get; set; }

        public bool OverrideSpindle { get; set; }

        public bool OverridesEnabled { get; set; }
    }
}
