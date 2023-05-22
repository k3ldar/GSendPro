using SimpleDB;

namespace GSendDB.Tables
{
    public sealed class ToolDatabaseDefaults : ITableDefaults<ToolDatabaseDataRow>
    {
        public long PrimarySequence => -1;

        public long SecondarySequence => 0;

        public ushort Version => 1;

        public List<ToolDatabaseDataRow> InitialData(ushort version)
        {
            if (version == 1)
            {
                return new List<ToolDatabaseDataRow>()
                {
                    new ToolDatabaseDataRow() { ToolName = "(Default)", Description = "Default profile for no tool selected"}
                };
            }

            return new List<ToolDatabaseDataRow>();
        }
    }
}
