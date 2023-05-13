using SimpleDB;

namespace GSendDB.Tables
{
    internal class JobProfileDataRowDefaults : ITableDefaults<JobProfileDataRow>
    {
        public long PrimarySequence => 0;

        public long SecondarySequence => 0;

        public ushort Version => 1;

        public List<JobProfileDataRow> InitialData(ushort version)
        {
            return new List<JobProfileDataRow>()
            {
                new JobProfileDataRow() { JobName = "(Default)", JobDescription = "Default job" },
            };
        }
    }
}
