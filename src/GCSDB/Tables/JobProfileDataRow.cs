using SimpleDB;

namespace GSendDB.Tables
{
    [Table("JobProfiles", CompressionType.Brotli, CachingStrategy.Memory, WriteStrategy.Forced)]
    internal class JobProfileDataRow : TableRowDefinition
    {
        private string _jobName;
        private string _jobDescription;
        private ulong _serialNumber;

        public string JobName
        {
            get => _jobName;

            set
            {
                if (_jobName == value)
                    return;

                _jobName = value;
                Update();
            }
        }

        public string JobDescription
        {
            get => _jobDescription;

            set
            {
                if (_jobDescription == value)
                    return;

                _jobDescription = value;
                Update();
            }
        }

        public ulong SerialNumber
        {
            get => _serialNumber;

            set
            {
                if (_serialNumber == value)
                    return;

                _serialNumber = value;
                Update();
            }
        }
    }
}
