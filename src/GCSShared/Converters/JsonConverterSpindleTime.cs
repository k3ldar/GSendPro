using System.Text.Json;
using System.Text.Json.Serialization;

using GSendShared.Models;

namespace GSendShared.Converters
{
    internal class JsonConverterSpindleTime : JsonConverter<SpindleTime>
    {
        public JsonConverterSpindleTime()
        {
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(ISpindleTime));
        }

        public override void Write(Utf8JsonWriter writer, SpindleTime value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        public override SpindleTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);

            return JsonSerializer.Deserialize<SpindleTime>(jsonDocument.RootElement.ToString());
        }
    }
}
