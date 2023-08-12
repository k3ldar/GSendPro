using System.Text.Json.Serialization;

namespace GSendShared
{
    [JsonConverter(typeof(Converters.JsonConverterToolsModel))]

    public interface IToolProfile
    {
        long Id { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        DateTime UsageLastReset { get; set; }

        decimal ExpectedLifeMinutes { get; set; }
    }
}
