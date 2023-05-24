using System.Text.Json.Serialization;

namespace GSendShared
{
    [JsonConverter(typeof(Converters.JsonConverterJobProfile))]
    public interface IJobProfile
    {
        long Id { get; }

        string Name { get; set; }

        string Description { get; set; }

        ulong SerialNumber { get; }

        long ToolProfileId { get; set; }
    }
}
