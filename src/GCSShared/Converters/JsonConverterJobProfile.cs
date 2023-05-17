using System.Text.Json;
using System.Text.Json.Serialization;

using GSendShared.Models;

namespace GSendShared.Converters
{
    internal class JsonConverterJobProfile : JsonConverter<JobProfileModel>
    {
        public JsonConverterJobProfile()
        {
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(IJobProfile));
        }

        public override void Write(Utf8JsonWriter writer, JobProfileModel value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        public override JobProfileModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;


            using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);

            return JsonSerializer.Deserialize(jsonDocument.RootElement.ToString(), typeToConvert, options) as JobProfileModel;
        }
    }
}
