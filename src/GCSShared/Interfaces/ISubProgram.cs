using System.Text.Json.Serialization;

using GSendShared.Abstractions;

namespace GSendShared
{
    [JsonConverter(typeof(Converters.JsonConverterSubProgram))]

    public interface ISubprogram
    {
        string Name { get; set; }

        string Description { get; set; }

        string Contents { get; set; }

        List<IGCodeVariable> Variables { get; set; }
    }
}
