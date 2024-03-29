﻿using GSendShared;

using SimpleDB;

namespace GSendDB.Tables
{
    [Table("MachineService", CompressionType.Brotli, CachingStrategy.Memory, WriteStrategy.Forced)]
    internal sealed class MachineServiceDataRow : TableRowDefinition
    {
        private long _machineId;
        private DateTime _date;
        private ServiceType _serviceType;
        private long _spindleHours;
        private ObservableList<long> _items;

        public MachineServiceDataRow()
        {
            _items = [];
            _items.Changed += ObservableDataChanged;
        }

        [ForeignKey("Machines")]
        public long MachineId
        {
            get => _machineId;

            set
            {
                if (value == _machineId)
                    return;

                _machineId = value;
                Update();
            }
        }

        public DateTime ServiceDate
        {
            get => _date;

            set
            {
                if (_date == value)
                    return;

                _date = value;
                Update();
            }
        }

        public ServiceType ServiceType
        {
            get => _serviceType;

            set
            {
                if (value == _serviceType)
                    return;

                _serviceType = value;
                Update();
            }
        }

        public long SpindleHours
        {
            get => _spindleHours;

            set
            {
                if (value == _spindleHours)
                    return;

                _spindleHours = value;
                Update();
            }
        }

        public ObservableList<long> Items
        {
            get => _items;

            set
            {
                if (value == null)
                    throw new InvalidOperationException();

                if (_items != null)
                    _items.Changed -= ObservableDataChanged;

                _items = value;
                _items.Changed += ObservableDataChanged;
                Update();
            }
        }
    }
}
