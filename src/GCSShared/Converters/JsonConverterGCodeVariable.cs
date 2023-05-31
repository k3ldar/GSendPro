using System.Text.Json;
using System.Text.Json.Serialization;

using GSendShared.Abstractions;
using GSendShared.Models;


namespace GSendShared.Converters
{
    public sealed class JsonConverterGCodeVariable : JsonConverter<GCodeVariableModel>
    {
        public JsonConverterGCodeVariable()
        {
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(IGCodeVariable));
        }

        public override void Write(Utf8JsonWriter writer, GCodeVariableModel value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        public override GCodeVariableModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);

            return JsonSerializer.Deserialize(jsonDocument.RootElement.ToString(), typeToConvert, options) as GCodeVariableModel;
        }
    }
}
