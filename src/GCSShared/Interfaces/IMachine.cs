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

        MachineFirmware MachineFirmware { get; set; }

        string ComPort { get; set; }

        MachineOptions Options { get; set; }

        byte AxisCount { get; set; }

        FeedRateDisplayUnits DisplayUnits { get; set; }

        FeedbackUnit FeedbackUnit { get; set; }

        int OverrideSpeed { get; set; }

        int OverrideSpindle { get; set; }

        int OverrideZDownSpeed { get; set; }

        int OverrideZUpSpeed { get; set; }

        GrblSettings Settings { get; set; }

        DateTime ConfigurationLastVerified { get; set; }

        string ProbeCommand { get; set; }

        int ProbeSpeed { get; set; }

        decimal ProbeThickness { get; set; }

        int JogUnits { get; set; }

        int JogFeedrate { get; set; }

        SpindleType SpindleType { get; set; }

        int SoftStartSeconds { get; set; }

        bool SoftStart { get; }

        int ServiceSpindleHours { get; set; }

        int ServiceWeeks { get; set; }

        decimal LayerHeightWarning { get; set; }

        void AddOptions(MachineOptions options);

        void RemoveOptions(MachineOptions options);
    }
}
