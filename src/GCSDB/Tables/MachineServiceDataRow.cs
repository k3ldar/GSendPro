using SimpleDB;

namespace GSendDB.Tables
{
    [Table("MachineService", CompressionType.Brotli, CachingStrategy.Memory, WriteStrategy.Forced)]
    public sealed class MachineServiceDataRow : TableRowDefinition
    {
        private long _machineId;
        private DateTime _date;

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
    }
}
