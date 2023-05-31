using System.Text.Json.Serialization;

namespace GSendShared.Abstractions
{
    [JsonConverter(typeof(Converters.JsonConverterGCodeVariable))]
    public interface IGCodeVariable
    {
        bool IsBoolean { get; }

        bool IsDecimal { get; }

        int LineNumber { get; }

        object Value { get; }

        ushort VariableId { get; }
    }
}