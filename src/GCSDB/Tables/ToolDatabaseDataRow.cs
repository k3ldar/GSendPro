using SimpleDB;

namespace GSendDB.Tables
{
    [Table("ToolDatabase", CompressionType.Brotli, CachingStrategy.Memory, WriteStrategy.Forced)]
    public sealed class ToolDatabaseDataRow : TableRowDefinition
    {
        private string _toolName;
        private string _description;
        private DateTime _resetUsage;

        public string ToolName
        {
            get => _toolName;

            set
            {
                if (value == _toolName)
                    return;

                _toolName = value;
                Update();
            }
        }

        public string Description
        {
            get => _description;

            set
            {
                if (value == _description)
                    return;

                _description = value;
                Update();
            }
        }

        public DateTime UsageLastReset
        {
            get => _resetUsage;

            set
            {
                if (value == _resetUsage)
                    return;

                _resetUsage = value;
                Update();
            }
        }
    }
}
