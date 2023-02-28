using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared;

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
        private ObservableDictionary<uint, decimal> _settings;

        public MachineDataRow()
        {
            _settings = new ObservableDictionary<uint, decimal>();
            _settings.Changed += ObservableDataChanged;
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

        public ObservableDictionary<uint, decimal> Settings
        {
            get => _settings;

            set
            {
                if (_settings == value)
                    return;

                if (value == null)
                    return;

                _settings.Changed -= ObservableItem_Changed;
                _settings = value;
                _settings.Changed += ObservableItem_Changed;
                Update();
            }
        }

        private void ObservableItem_Changed(object sender, EventArgs e)
        {
            Update();
        }
    }
}
