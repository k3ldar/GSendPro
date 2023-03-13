using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
        private byte _overrideRapids;
        private byte _overrideFeeds;
        private byte _overrideSpindleSpeed;
        private bool _floodEnabled;
        private bool _mistEnabled;
        private bool _spindleClockWise;
        private bool _spindleCounterClockWise;

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

        public byte OverrideRapids
        {
            get => _overrideRapids;

            set
            {
                if (_overrideRapids.Equals(value))
                    return;

                _overrideRapids = value;
                Updated = true;
            }
        }

        public byte OverrideFeeds
        {
            get => _overrideFeeds;

            set
            {
                if (_overrideFeeds.Equals(value))
                    return;

                _overrideFeeds = value;
                Updated = true;
            }
        }

        public byte OverrideSpindleSpeed
        {
            get => _overrideSpindleSpeed;

            set
            {
                if (_overrideSpindleSpeed.Equals(value))
                    return;

                _overrideSpindleSpeed = value;
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

        public void ResetUpdated()
        {
            Updated = false;
        }
    }
}
