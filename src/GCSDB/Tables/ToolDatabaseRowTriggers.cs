using SimpleDB;

namespace GSendDB.Tables
{
    internal class ToolDatabaseRowTriggers : ITableTriggers<ToolDatabaseDataRow>
    {
        public int Position => 0;

        public TriggerType TriggerTypes => TriggerType.BeforeDelete | TriggerType.BeforeInsert;

        public void AfterDelete(List<ToolDatabaseDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void AfterInsert(List<ToolDatabaseDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void AfterUpdate(List<ToolDatabaseDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void BeforeDelete(List<ToolDatabaseDataRow> records)
        {
            if (records.Any(r => r.Id.Equals(0)))
                throw new InvalidDataRowException("", "", "Undable to delete default record");
        }

        public void BeforeInsert(List<ToolDatabaseDataRow> records)
        {
            records.ForEach(r => r.UsageLastReset = DateTime.UtcNow);
        }

        public void BeforeUpdate(List<ToolDatabaseDataRow> records)
        {
            // not used but declared as part of interface
        }

        public void BeforeUpdate(ToolDatabaseDataRow newRecord, ToolDatabaseDataRow oldRecord)
        {
            // not used but declared as part of interface
        }
    }
}
