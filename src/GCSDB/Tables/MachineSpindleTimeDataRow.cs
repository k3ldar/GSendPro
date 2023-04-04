using SimpleDB;

namespace GSendDB.Tables
{
    [Table("MachineSpindleTime", CompressionType.None, CachingStrategy.Memory, WriteStrategy.Forced)]
    public class MachineSpindleTimeDataRow : TableRowDefinition
    {
        private long _machineId;
        private int _maxRpm;
        private DateTime _startTime;
        private DateTime _finishTime;

        [ForeignKey("Machines", ForeignKeyAttributes.DefaultValue)]
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

        public int MaxRpm
        {
            get => _maxRpm;

            set
            {
                if (value == _maxRpm)
                    return;

                _maxRpm = value;
                Update();
            }
        }

        public DateTime StartTime
        {
            get => _startTime;

            set
            {
                if (_startTime == value)
                    return;

                _startTime = value;
                Update();
            }
        }

        public DateTime FinishTime
        {
            get => _finishTime;

            set
            {
                if (_finishTime == value)
                    return;

                _finishTime = value;
                Update();
            }
        }
    }
}
