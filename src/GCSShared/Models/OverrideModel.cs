namespace GSendShared.Models
{
    public sealed class OverrideModel
    {
        private bool _overrideRapids;
        private bool _overrideXY;
        private bool _overrideZUp;
        private bool _overrideZDown;
        private bool _overrideSpindle;
        private bool _overrideDisabled;
        private OverrideValue _spindle;
        private OverrideValue _axisZDown;
        private OverrideValue _axisZUp;
        private OverrideValue _axisXY;
        private RapidsOverride _rapids;
        private readonly bool _constructing = true;

        public OverrideModel()
        {

            AxisXY = new OverrideValue();
            AxisZDown = new OverrideValue();
            AxisZUp = new OverrideValue();
            Spindle = new OverrideValue();
            OverridesDisabled = true;
            _constructing = false;
        }

        public RapidsOverride Rapids
        {
            get => _rapids;

            set
            {
                if (value == _rapids)
                    return;

                _rapids = value;
                RaiseValueChanged();
            }
        }

        public OverrideValue AxisXY
        {
            get => _axisXY;

            set
            {
                if (value == null)
                    return;

                if (_axisXY != null)
                    _axisXY.ValueChanged -= ValueUpdated;

                _axisXY = value;
                _axisXY.ValueChanged += ValueUpdated;

                RaiseValueChanged();
            }
        }

        public OverrideValue AxisZUp
        {
            get => _axisZUp;

            set
            {
                if (value == null)
                    return;

                if (_axisZUp != null)
                    _axisZUp.ValueChanged -= ValueUpdated;

                _axisZUp = value;
                _axisZUp.ValueChanged += ValueUpdated;

                RaiseValueChanged();
            }
        }

        public OverrideValue AxisZDown
        {
            get => _axisZDown;

            set
            {
                if (value == null)
                    return;

                if (_axisZDown != null)
                    _axisZDown.ValueChanged -= ValueUpdated;

                _axisZDown = value;
                _axisZDown.ValueChanged += ValueUpdated;

                RaiseValueChanged();
            }
        }

        public OverrideValue Spindle
        {
            get => _spindle;

            set
            {
                if (value == null)
                    return;

                if (_spindle != null)
                    _spindle.ValueChanged -= ValueUpdated;

                _spindle = value;
                _spindle.ValueChanged += ValueUpdated;

                RaiseValueChanged();
            }
        }

        public bool OverrideRapids
        {
            get => _overrideRapids;

            set
            {
                if (value == _overrideRapids)
                    return;

                _overrideRapids = value;
                RaiseValueChanged();
            }
        }

        public bool OverrideXY
        {
            get => _overrideXY;

            set
            {
                if (value == _overrideXY)
                    return;

                _overrideXY = value;
                RaiseValueChanged();
            }
        }

        public bool OverrideZUp
        {
            get => _overrideZUp;

            set
            {
                if (value == _overrideZUp)
                    return;

                _overrideZUp = value;
                RaiseValueChanged();
            }
        }

        public bool OverrideZDown
        {
            get => _overrideZDown;

            set
            {
                if (value == _overrideZDown)
                    return;

                _overrideZDown = value;
                RaiseValueChanged();
            }
        }

        public bool OverrideSpindle
        {
            get => _overrideSpindle;

            set
            {
                if (value == _overrideSpindle)
                    return;

                _overrideSpindle = value;
                RaiseValueChanged();
            }
        }

        public bool OverridesDisabled
        {
            get => _overrideDisabled;

            set
            {
                if (value == _overrideDisabled)
                    return;

                _overrideDisabled = value;
                RaiseValueChanged();
            }
        }

        public event EventHandler ValueUpdated;

        private void RaiseValueChanged()
        {
            if (_constructing)
                return;

            ValueUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
