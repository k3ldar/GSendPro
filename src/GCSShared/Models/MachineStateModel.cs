namespace GSendShared.Models
{
    public sealed class MachineStateModel
    {
        private MachineState _machineState = MachineState.Undefined;
        private int _subState;
        private double _feedRate;
        private double _spindleSpeed;
        private double _machineX;
        private double _machineY;
        private double _machineZ;
        private double _workX;
        private double _workY;
        private double _workZ;
        private double _offsetX;
        private double _offsetY;
        private double _offsetZ;
        private int _bufferAvailableBlocks;
        private int _availableRXbytes;
        private int _lineNumber;
        private int _totalLines;
        private bool _floodEnabled;
        private bool _mistEnabled;
        private bool _spindleClockWise;
        private bool _spindleCounterClockWise;
        private bool _isRunning;
        private bool _isPaused;
        private bool _isHoming;
        private int _bufferSize;
        private int _queueSize;
        private int _commandQueueSize;
        private CoordinateSystem _coordinateSystem;
        private MachineStateOptions _machineStateOptions;
        private TimeSpan _totalJobTime;
        private readonly OverrideModel _overrideModel;

        public MachineStateModel()
        {
            _overrideModel = new OverrideModel();
            _overrideModel.ValueUpdated += OverrideModel_ValueUpdated;
            _machineStateOptions = MachineStateOptions.None;
        }

        public MachineStateOptions MachineStateOptions { get => _machineStateOptions; }

        public byte MachineOverrideFeeds { get; set; }

        public byte MachineOverrideSpindle { get; set; }

        public byte MachineOverrideRapids { get; set; }

        public OverrideModel Overrides
        {
            get => _overrideModel;

            set
            {
                bool hasUpdated = false;

                if (value.Rapids != _overrideModel.Rapids)
                {
                    _overrideModel.Rapids = value.Rapids;
                    hasUpdated = true;
                }

                if (value.AxisXY.NewValue != _overrideModel.AxisXY.NewValue)
                {
                    _overrideModel.AxisXY = value.AxisXY;
                    hasUpdated = true;
                }

                if (value.AxisZUp.NewValue != _overrideModel.AxisZUp.NewValue)
                {
                    _overrideModel.AxisZUp = value.AxisZUp;
                    hasUpdated = true;
                }

                if (value.AxisZDown.NewValue != _overrideModel.AxisZDown.NewValue)
                {
                    _overrideModel.AxisZDown = value.AxisZDown;
                    hasUpdated = true;
                }

                if (value.Spindle.NewValue != _overrideModel.Spindle.NewValue)
                {
                    _overrideModel.Spindle = value.Spindle;
                    hasUpdated = true;
                }

                if (value.OverrideRapids != _overrideModel.OverrideRapids)
                {
                    _overrideModel.OverrideRapids = value.OverrideRapids;
                    hasUpdated = true;
                }

                if (value.OverrideXY != _overrideModel.OverrideXY)
                {
                    _overrideModel.OverrideXY = value.OverrideXY;
                    hasUpdated = true;
                }

                if (value.OverrideZUp != _overrideModel.OverrideZUp)
                {
                    _overrideModel.OverrideZUp = value.OverrideZUp;
                    hasUpdated = true;
                }

                if (value.OverrideZDown != _overrideModel.OverrideZDown)
                {
                    _overrideModel.OverrideZDown = value.OverrideZDown;
                    hasUpdated = true;
                }

                if (value.OverrideSpindle != _overrideModel.OverrideSpindle)
                {
                    _overrideModel.OverrideSpindle = value.OverrideSpindle;
                    hasUpdated = true;
                }

                if (value.OverridesDisabled != _overrideModel.OverridesDisabled)
                {
                    _overrideModel.OverridesDisabled = value.OverridesDisabled;
                    hasUpdated = true;
                }

                if (hasUpdated)
                    Updated = true;
            }
        }

        public MachineState MachineState
        {
            get => _machineState;

            set
            {
                if (_machineState.Equals(value))
                    return;

                _machineState = value;
                Updated = true;
            }
        }

        public int SubState
        {
            get => _subState;

            set
            {
                if (_subState.Equals(value))
                    return;

                _subState = value;
                Updated = true;
            }
        }

        public CoordinateSystem CoordinateSystem
        {
            get => _coordinateSystem;

            set
            {
                if (_coordinateSystem.Equals(value))
                    return;

                _coordinateSystem = value;
                Updated = true;
            }
        }

        public double FeedRate
        {
            get => _feedRate;

            set
            {
                if (_feedRate.Equals(value))
                    return;

                _feedRate = value;
                Updated = true;
            }
        }

        public double SpindleSpeed
        {
            get => _spindleSpeed;

            set
            {
                if (_spindleSpeed.Equals(value))
                    return;

                _spindleSpeed = value;
                Updated = true;
            }
        }

        public double MachineX
        {
            get => _machineX;

            set
            {
                if (_machineX.Equals(value))
                    return;

                _machineX = value;
                Updated = true;
                _workX = _machineX - _offsetX;
            }
        }

        public double MachineY
        {
            get => _machineY;

            set
            {
                if (_machineY.Equals(value))
                    return;

                _machineY = value;
                Updated = true;
                _workY = _machineY - _offsetY;
            }
        }

        public double MachineZ
        {
            get => _machineZ;

            set
            {
                if (_machineZ.Equals(value))
                    return;

                _machineZ = value;
                Updated = true;
                _workZ = _machineZ - _offsetZ;
            }
        }

        public double WorkX
        {
            get => _workX;

            set
            {
                if (_workX.Equals(value))
                    return;

                _workX = value;
                Updated = true;
            }
        }

        public double WorkY
        {
            get => _workY;

            set
            {
                if (_workY.Equals(value))
                    return;

                _workY = value;
                Updated = true;
            }
        }

        public double WorkZ
        {
            get => _workZ;

            set
            {
                if (_workZ.Equals(value))
                    return;

                _workZ = value;
                Updated = true;
            }
        }

        public double OffsetX
        {
            get => _offsetX;

            set
            {
                if (_offsetX.Equals(value))
                    return;

                _offsetX = value;
                Updated = true;

                _workX = _machineX - _offsetX;
            }
        }

        public double OffsetY
        {
            get => _offsetY;

            set
            {
                if (_offsetY.Equals(value))
                    return;

                _offsetY = value;
                Updated = true;

                _workY = _machineY - _offsetY;
            }
        }

        public double OffsetZ
        {
            get => _offsetZ;

            set
            {
                if (_offsetZ.Equals(value))
                    return;

                _offsetZ = value;
                Updated = true;

                _workZ = _machineZ - _offsetZ;
            }
        }

        public int BufferAvailableBlocks
        {
            get => _bufferAvailableBlocks;

            set
            {
                if (_bufferAvailableBlocks.Equals(value))
                    return;

                _bufferAvailableBlocks = value;
                Updated = true;
            }
        }

        public int AvailableRXbytes
        {
            get => _availableRXbytes;

            set
            {
                if (_availableRXbytes.Equals(value))
                    return;

                _availableRXbytes = value;
                Updated = true;
            }
        }

        public int LineNumber
        {
            get => _lineNumber;

            set
            {
                if (_lineNumber.Equals(value))
                    return;

                _lineNumber = value;
                Updated = true;
            }
        }

        public int TotalLines
        {
            get => _totalLines;

            set
            {
                if (_totalLines.Equals(value))
                    return;

                _totalLines = value;
                Updated = true;
            }
        }

        public bool FloodEnabled
        {
            get => _floodEnabled;

            set
            {
                if (_floodEnabled.Equals(value))
                    return;

                _floodEnabled = value;
                Updated = true;
            }
        }

        public bool MistEnabled
        {
            get => _mistEnabled;

            set
            {
                if (_mistEnabled.Equals(value))
                    return;

                _mistEnabled = value;
                Updated = true;
            }
        }


        public bool SpindleClockWise
        {
            get => _spindleClockWise;

            set
            {
                if (_spindleClockWise.Equals(value))
                    return;

                _spindleClockWise = value;
                Updated = true;
            }
        }

        public bool SpindleCounterClockWise
        {
            get => _spindleCounterClockWise;

            set
            {
                if (_spindleCounterClockWise.Equals(value))
                    return;

                _spindleCounterClockWise = value;
                Updated = true;
            }
        }

        public bool Updated { get; private set; }

        public bool IsLocked
        {
            get
            {
                return _machineState == MachineState.Alarm;
            }
        }

        public bool IsPaused
        {
            get => _isPaused;

            set
            {
                if (_isPaused.Equals(value))
                    return;

                _isPaused = value;
                Updated = true;
            }
        }

        public bool IsRunning
        {
            get => _isRunning;

            set
            {
                if (_isRunning.Equals(value))
                    return;

                _isRunning = value;
                Updated = true;
            }
        }

        public bool IsHoming
        {
            get => _isHoming;

            set
            {
                if (_isHoming.Equals(value))
                    return;

                _isHoming = value;
                Updated = true;
            }
        }

        public TimeSpan JobTime
        {
            get => _totalJobTime;

            set
            {
                if (_totalJobTime.Ticks.Equals(value.Ticks))
                    return;

                _totalJobTime = value;
                Updated = true;
            }
        }

        public bool IsConnected { get; set; }

        public int BufferSize
        {
            get => _bufferSize;

            set
            {
                if (_bufferSize == value)
                    return;

                _bufferSize = value;
                Updated = true;
            }
        }

        public int QueueSize
        {
            get => _queueSize;

            set
            {
                if (_queueSize == value)
                    return;

                _queueSize = value;
                Updated = true;
            }
        }

        public int CommandQueueSize
        {
            get => _commandQueueSize;

            set
            {
                if (_commandQueueSize == value)
                    return;

                _commandQueueSize = value;
                Updated = true;
            }
        }

        public List<ChangedGrblSettings> UpdatedGrblConfiguration { get; set; } = new List<ChangedGrblSettings>();

        public void ResetUpdated()
        {
            Updated = false;
        }

        public void OptionAdd(MachineStateOptions machineStateOptions)
        {
            _machineStateOptions |= machineStateOptions;
            Updated = true;
        }

        public void OptionRemove(MachineStateOptions machineStateOptions)
        {
            _machineStateOptions &= ~machineStateOptions;
            Updated = true;
        }

        private void OverrideModel_ValueUpdated(object sender, EventArgs e)
        {
            Updated = true;
        }
    }
}
