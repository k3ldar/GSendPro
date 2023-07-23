using System.Text.Json.Serialization;

namespace GSendShared.Abstractions
{
    [JsonConverter(typeof(Converters.JsonConverterGCodeVariable))]
    public interface IGCodeVariable
    {
        bool IsBoolean { get; set; }

        bool IsDecimal { get; set; }

        int LineNumber { get; set; }

        object Value { get; set; }

        decimal DecimalValue { get; }

        int IntValue { get; }

        ushort VariableId { get; set; }
    }
}