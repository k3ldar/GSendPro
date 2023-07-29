using SimpleDB;

namespace GSendDB.Tables
{
    internal class JobExecutionDataRowTriggers : ITableTriggers<JobExecutionDataRow>
    {
        public int Position => 0;

        public TriggerType TriggerTypes => TriggerType.BeforeDelete;

        public void AfterDelete(List<JobExecutionDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void AfterInsert(List<JobExecutionDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void AfterUpdate(List<JobExecutionDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void BeforeDelete(List<JobExecutionDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void BeforeInsert(List<JobExecutionDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void BeforeUpdate(List<JobExecutionDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void BeforeUpdate(JobExecutionDataRow newRecord, JobExecutionDataRow oldRecord)
        {
            // not used but declared as part of interface
        }
    }
}
