using System.Text.Json;
using System.Text.Json.Serialization;

using GSendShared.Models;

namespace GSendShared.Converters
{
    internal class JsonConverterJobExecution : JsonConverter<JobExecutionModel>
    {
        public JsonConverterJobExecution()
        {
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(IJobExecution));
        }

        public override void Write(Utf8JsonWriter writer, JobExecutionModel value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        public override JobExecutionModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;


            using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);

            return JsonSerializer.Deserialize<JobExecutionModel>(jsonDocument.RootElement.ToString());
        }
    }
}
