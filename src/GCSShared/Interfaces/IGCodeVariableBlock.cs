using System.Text.Json.Serialization;

namespace GSendShared
{
    [JsonConverter(typeof(Converters.JsonConverterGCodeVariableBlock))]
    public interface IGCodeVariableBlock
    {
        string VariableBlock { get; }

        List<string> Variables { get; }

        List<ushort> VariableIds { get; }

        int LineNumber { get; }
    }
}
