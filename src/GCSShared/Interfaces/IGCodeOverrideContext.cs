using GSendShared.Models;

namespace GSendShared.Abstractions
{
    /// <summary>
    /// GCode override context
    /// </summary>
    public interface IGCodeOverrideContext
    {
        /// <summary>
        /// GCode line being processed
        /// </summary>
        IGCodeLine GCode { get; }

        /// <summary>
        /// Overridden GCode
        /// </summary>
        IGCodeLine OverriddenGCode { get; set; }

        /// <summary>
        /// Indicates the gcode has been overridden 
        /// </summary>
        bool Overridden { get; }

        /// <summary>
        /// Indicates the command will be sent to Grbl after processing
        /// </summary>
        bool SendCommand { get; set; }

        /// <summary>
        /// Machine processing the GCode
        /// </summary>
        IGCodeProcessor Processor { get; }

        IMachine Machine { get; }

        IStaticMethods StaticMethods { get; }

        OverrideModel Overrides { get; }

        MachineStateModel MachineStateModel { get; }

        bool ProcessGCodeOverrides(IGCodeLine line);

        bool ProcessMCodeOverrides(IGCodeLine line);

        void ProcessAlarm(GrblAlarm alarm);

        void ProcessError(GrblError error);

        void Cancel();
    }
}
