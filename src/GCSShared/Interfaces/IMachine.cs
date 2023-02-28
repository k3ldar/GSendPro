using System.Text.Json.Serialization;

namespace GSendShared
{
    [JsonConverter(typeof(Converters.JsonConverterMachine))]
    public interface IMachine
    {
        long Id { get; }

        string Name { get; set; }

        MachineType MachineType { get; set; }

        string ComPort { get; set; }

        MachineOptions Options { get; set; }

        byte AxisCount { get; set; }

        Dictionary<uint, decimal> Settings { get; set; }
    }
}
