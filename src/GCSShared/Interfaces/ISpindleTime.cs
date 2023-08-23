using System.Text.Json.Serialization;

namespace GSendShared
{
    [JsonConverter(typeof(Converters.JsonConverterSpindleTime))]
    public interface ISpindleTime
    {
        long MachineId { get; }

        long ToolProfileId { get; }

        int MaxRpm { get; }

        DateTime StartTime { get; }

        DateTime FinishTime { get; }
    }
}
