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
            AxisX = new OverrideValue();
            AxisY = new OverrideValue();
            AxisZDown = new OverrideValue();
            AxisZUp = new OverrideValue();
            Spindle = new OverrideValue();
        }

        public IOverriddenValue AxisX { get; }

        public IOverriddenValue AxisY { get; }

        public IOverriddenValue AxisZUp { get; }

        public IOverriddenValue AxisZDown { get; }

        public IOverriddenValue Spindle { get; }

        public bool OverrideX { get; set; }

        public bool OverrideY { get; set; }

        public bool OverrideZUp { get; set; }

        public bool OverrideZDown { get; set; }

        public bool OverrideSpindle { get; }

        public bool OverridesEnabled { get; }
    }
}
