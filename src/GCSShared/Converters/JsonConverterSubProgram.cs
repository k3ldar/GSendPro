using System.Text.Json;
using System.Text.Json.Serialization;

using GSendCommon;


namespace GSendShared.Converters
{
    public sealed class JsonConverterSubProgram : JsonConverter<SubprogramModel>
    {
        public JsonConverterSubProgram()
        {
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(ISubprogram));
        }

        public override void Write(Utf8JsonWriter writer, SubprogramModel value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        public override SubprogramModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;


            using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);

            return JsonSerializer.Deserialize<SubprogramModel>(jsonDocument.RootElement.ToString());
        }
    }
}
