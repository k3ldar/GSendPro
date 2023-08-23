using SimpleDB;

namespace GSendDB.Tables
{
    [Table("ToolDatabase", CompressionType.Brotli, CachingStrategy.Memory, WriteStrategy.Forced)]
    internal sealed class ToolDatabaseDataRow : TableRowDefinition
    {
        private string _toolName;
        private string _description;
        private DateTime _resetUsage;
        private double _expectedLifeMinutes;
        private double _lengthInMillimetres;
        private ObservableList<ToolUsageHistory> _toolHistory;

        public ToolDatabaseDataRow()
        {
            _toolHistory = new ObservableList<ToolUsageHistory>();
            _toolHistory.Changed += ObservableDataChanged;
        }

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

        public double LengthInMillimetres
        {
            get => _lengthInMillimetres;

            set
            {
                if (value == _lengthInMillimetres)
                    return;

                _lengthInMillimetres = value;
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
        
        public double ExpectedLifeMinutes
        {
            get => _expectedLifeMinutes;

            set
            {
                if (value == _expectedLifeMinutes)
                    return;

                _expectedLifeMinutes = value;
                Update();
            }
        }

        public ObservableList<ToolUsageHistory> ToolHistory
        {
            get => _toolHistory;

            set
            {
                if (value == null)
                    throw new InvalidOperationException();

                if (_toolHistory != null)
                    _toolHistory.Changed -= ObservableDataChanged;

                _toolHistory = value;
                _toolHistory.Changed += ObservableDataChanged;
                Update();
            }
        }
    }
}
