using System.Text.Json.Serialization;

using GSendShared.Models;

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

        DisplayUnits DisplayUnits { get; set; }

        int OverrideSpeed { get; set; }

        int OverrideSpindle { get; set; }

        GrblSettings Settings { get; set; }

        DateTime ConfigurationLastVerified { get; set; }

        string ProbeCommand { get; set; }
    }
}
