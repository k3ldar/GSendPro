using GSendShared;
using GSendShared.Models;

using SimpleDB;

namespace GSendDB.Tables
{
    [Table("Machines", CompressionType.Brotli, CachingStrategy.Memory, WriteStrategy.Forced)]
    public sealed class MachineDataRow : TableRowDefinition
    {
        private string _name;
        private MachineType _machineType;
        private MachineFirmware _machineFirmware;
        private string _comPort;
        private MachineOptions _options;
        private byte _axisCount;
        private GrblSettings _settings;
        private FeedRateDisplayUnits _displayUnits;
        private FeedbackUnit _feedbackUnits;
        private int _overrideSpeed;
        private int _overrideSpindle;
        private int _overrideZDown;
        private int _overrideZUp;
        private DateTime _configurationLastVerified;
        private string _probeCommand;
        private int _probeSpeed;
        private decimal _probeThickness;
        private int _jogUnits;
        private int _jogFeedrate;
        private SpindleType _spindleType;
        private int _softStartSeconds;
        private int _serviceWeeks;
        private int _serviceSpindleHours;
        private decimal _layerHeightWarning;


        // Update ConvertFromIMachineToMachineDataRow in gSendDataProvider

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

        public MachineFirmware MachineFirmware
        {
            get => _machineFirmware;

            set
            {
                if (value == _machineFirmware)
                    return;

                _machineFirmware = value;
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

        public FeedRateDisplayUnits DisplayUnits
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

        public FeedbackUnit FeedbackUnits
        {
            get => _feedbackUnits;

            set
            {
                if (_feedbackUnits == value)
                    return;

                _feedbackUnits = value;
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

        public int OverrideZUp
        {
            get => _overrideZUp;

            set
            {
                if (_overrideZUp == value)
                    return;

                _overrideZUp = value;
                Update();
            }
        }

        public int OverrideZDown
        {
            get => _overrideZDown;

            set
            {
                if (_overrideZDown == value)
                    return;

                _overrideZDown = value;
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

        public int ServiceWeeks
        {
            get => _serviceWeeks;

            set
            {
                if (_serviceWeeks == value)
                    return;

                _serviceWeeks = value;
                Update();
            }
        }

        public int ServiceSpindleHours
        {
            get => _serviceSpindleHours;

            set
            {
                if (_serviceSpindleHours == value)
                    return;

                _serviceSpindleHours = value;
                Update();
            }
        }

        public decimal LayerHeightWarning
        {
            get => _layerHeightWarning;

            set
            {
                if (_layerHeightWarning == value)
                    return;

                _layerHeightWarning = value;
                Update();
            }
        }
    }
}
