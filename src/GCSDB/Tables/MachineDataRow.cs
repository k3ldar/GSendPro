using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared;
using GSendShared.Models;

using SimpleDB;

namespace GSendDB.Tables
{
    [Table("Machines", CompressionType.Brotli, CachingStrategy.Memory, WriteStrategy.Forced)]
    public class MachineDataRow : TableRowDefinition
    {
        private string _name;
        private MachineType _machineType;
        private string _comPort;
        private MachineOptions _options;
        private byte _axisCount;
        private GrblSettings _settings;
        private DisplayUnits _displayUnits;
        private int _overrideSpeed;
        private int _overrideSpindle;
        private DateTime _configurationLastVerified;
        private string _probeCommand;
        private int _probeSpeed;
        private decimal _probeThickness;
        private int _jogUnits;
        private int _jogFeedrate;
        private SpindleType _spindleType;
        private int _softStartSeconds;

        // Update ConvertFromIMachineToMachineDataRow in machineprovider

        public MachineDataRow()
        {
            _settings = new GrblSettings();
        }

        [UniqueIndex]
        public string Name
        {
            get => _name;

            set
            {
                if (value == Name)
                    return;

                _name = value;
                Update();
            }
        }

        public MachineType MachineType
        {
            get => _machineType;

            set
            {
                if (value == _machineType)
                    return;

                _machineType = value;
                Update();
            }
        }

        public string ComPort
        {
            get => _comPort;

            set
            {
                if (value == _comPort)
                    return;

                _comPort = value;
                Update();
            }
        }

        public MachineOptions Options
        {
            get => _options;

            set
            {
                if (value == _options)
                    return;

                _options = value;
                Update();
            }
        }

        public byte AxisCount
        {
            get => _axisCount;

            set
            {
                if (value == _axisCount)
                    return;

                _axisCount = value;
                Update();
            }
        }

        public GrblSettings Settings
        {
            get => _settings;

            set
            {
                if (_settings == value)
                    return;

                if (value == null)
                    return;

                _settings = value;
                Update();
            }
        }

        public DisplayUnits DisplayUnits
        {
            get => _displayUnits;

            set
            {
                if (_displayUnits == value)
                    return;

                _displayUnits = value;
                Update();
            }
        }

        public int OverrideSpeed
        {
            get => _overrideSpeed;

            set
            {
                if (_overrideSpeed == value)
                    return;

                _overrideSpeed = value;
                Update();
            }
        }

        public int OverrideSpindle
        {
            get => _overrideSpindle;

            set
            {
                if (_overrideSpindle == value)
                    return;

                _overrideSpindle = value;
                Update();
            }
        }

        public DateTime ConfigurationLastVerified
        {
            get => _configurationLastVerified;

            set
            {
                if (_configurationLastVerified == value)
                    return;

                _configurationLastVerified = value;
                Update();
            }
        }

        public string ProbeCommand
        {
            get => _probeCommand;

            set
            {
                if (_probeCommand == value) 
                    return;

                _probeCommand = value;
                Update();
            }
        }

        public int ProbeSpeed
        {
            get => _probeSpeed;

            set
            {
                if (_probeSpeed == value)
                    return;

                _probeSpeed = value;
                Update();
            }
        }

        public decimal ProbeThickness
        {
            get => _probeThickness;

            set
            {
                if (_probeThickness == value)
                    return;

                _probeThickness = value;
                Update();
            }
        }

        public int JogUnits
        {
            get => _jogUnits;

            set
            {
                if (_jogUnits == value)
                    return;

                _jogUnits = value;
                Update();
            }
        }

        public int JogFeedRate
        {
            get => _jogFeedrate;

            set
            {
                if (_jogFeedrate == value)
                    return;

                _jogFeedrate = value;
                Update();
            }
        }

        public SpindleType SpindleType
        {
            get => _spindleType;

            set
            {
                if (_spindleType == value)
                    return;

                _spindleType = value;
                Update();
            }
        }

        public int SoftStartSeconds
        {
            get => _softStartSeconds;

            set
            {
                if (_softStartSeconds == value)
                    return;

                _softStartSeconds = value;
                Update();
            }
        }
    }
}
