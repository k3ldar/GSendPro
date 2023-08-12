namespace GSendShared.Models
{
    public sealed class ToolUsageHistoryModel
    {
        public ToolUsageHistoryModel()
        {

        }

        public ToolUsageHistoryModel(DateTime lastChanged, double usageMinutes)
        {
            LastChanged = lastChanged;
            UsageMinutes = usageMinutes;
        }

        public DateTime LastChanged { get; set; }

        public double UsageMinutes { get; set; }
    }
}
