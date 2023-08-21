using System.Text.Json.Serialization;

using GSendShared.Models;

namespace GSendShared
{
    [JsonConverter(typeof(Converters.JsonConverterToolsModel))]

    public interface IToolProfile
    {
        long Id { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        double LengthInMillimetres { get; set; }

        DateTime UsageLastReset { get; set; }

        double ExpectedLifeMinutes { get; set; }

        List<ToolUsageHistoryModel> History {get; set;}
    }
}
