﻿using System.Text.Json;
using System.Text.Json.Serialization;

using GSendShared.Models;

namespace GSendShared.Converters
{
    internal class JsonConverterToolsModel : JsonConverter<ToolProfileModel>
    {
        public JsonConverterToolsModel()
        {
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(IToolProfile));
        }

        public override void Write(Utf8JsonWriter writer, ToolProfileModel value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        public override ToolProfileModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;


            using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);

            return JsonSerializer.Deserialize<ToolProfileModel>(jsonDocument.RootElement.ToString());
        }
    }
}
