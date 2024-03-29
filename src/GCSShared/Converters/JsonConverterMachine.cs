﻿using System.Text.Json;
using System.Text.Json.Serialization;

using GSendShared.Models;


namespace GSendShared.Converters
{
    public sealed class JsonConverterMachine : JsonConverter<MachineModel>
    {
        public JsonConverterMachine()
        {
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(IMachine));
        }

        public override void Write(Utf8JsonWriter writer, MachineModel value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        public override MachineModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;


            using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);

            return JsonSerializer.Deserialize<MachineModel>(jsonDocument.RootElement.ToString());
        }
    }
}
