using System.Text.Json;
using System.Text.Json.Serialization;

using GSendCommon;

using GSendShared.Models;


namespace GSendShared.Converters
{
    public sealed class JsonConverterSubProgram : JsonConverter<SubProgramModel>
    {
        public JsonConverterSubProgram()
        {
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(ISubProgram));
        }

        public override void Write(Utf8JsonWriter writer, SubProgramModel value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        public override SubProgramModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;


            using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);

            return JsonSerializer.Deserialize(jsonDocument.RootElement.ToString(), typeToConvert, options) as SubProgramModel;
        }
    }
}
