
namespace GSendShared.Models
{
    public sealed class ToolProfileModel : IToolProfile
    {
        public ToolProfileModel()
            : this(new())
        {
            History = new List<ToolUsageHistoryModel>();
        }

        public ToolProfileModel(List<ToolUsageHistoryModel> history)
        {
            History = history;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime UsageLastReset { get; set; }

        public double ExpectedLifeMinutes { get; set; }

        public List<ToolUsageHistoryModel> History { get; set; }
    }
}
