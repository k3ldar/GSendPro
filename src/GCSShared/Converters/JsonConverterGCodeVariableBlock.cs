using System.Text.Json;
using System.Text.Json.Serialization;

using GSendShared.Models;


namespace GSendShared.Converters
{
    public sealed class JsonConverterGCodeVariableBlock : JsonConverter<GCodeVariableBlockModel>
    {
        public JsonConverterGCodeVariableBlock()
        {
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(IGCodeVariableBlock));
        }

        public override void Write(Utf8JsonWriter writer, GCodeVariableBlockModel value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        public override GCodeVariableBlockModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);

            return JsonSerializer.Deserialize<GCodeVariableBlockModel>(jsonDocument.RootElement.ToString());
        }
    }
}
