namespace GSendDB.Tables
{
    public sealed class ToolUsageHistory
    {
        public ToolUsageHistory()
        {
        
        }

        public ToolUsageHistory(DateTime lastChanged, double usageMinutes)
        {
            LastChanged = lastChanged;
            UsageMinutes = usageMinutes;
        }

        public DateTime LastChanged { get; set; }

        public double UsageMinutes { get; set; }
    }
}
