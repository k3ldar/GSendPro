using SimpleDB;

namespace GSendDB.Tables
{
    internal class JobProfileDataRowTriggers : ITableTriggers<JobProfileDataRow>
    {
        public int Position => 0;

        public TriggerType TriggerTypes => TriggerType.BeforeDelete;

        public void AfterDelete(List<JobProfileDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void AfterInsert(List<JobProfileDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void AfterUpdate(List<JobProfileDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void BeforeDelete(List<JobProfileDataRow> records)
        {
            if (records.Any(r => r.Id.Equals(0)))
                throw new InvalidDataRowException("", "", "Undable to delete default record");
        }

        public void BeforeInsert(List<JobProfileDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void BeforeUpdate(List<JobProfileDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void BeforeUpdate(JobProfileDataRow newRecord, JobProfileDataRow oldRecord)
        {
            // not used but declared as part of interface
        }
    }
}
