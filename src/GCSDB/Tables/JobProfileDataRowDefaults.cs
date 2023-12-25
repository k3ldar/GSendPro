using SimpleDB;

namespace GSendDB.Tables
{
    internal class JobProfileDataRowDefaults : ITableDefaults<JobProfileDataRow>
    {
        public long PrimarySequence => -1;

        public long SecondarySequence => 0;

        public ushort Version => 1;

        public List<JobProfileDataRow> InitialData(ushort version)
        {
            if (version > 1)
                return null;

            return
            [
                new() { JobName = "(Default)", JobDescription = "Default job" },
            ];
        }
    }
}
