using System.Collections.Concurrent;

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

        MachineStateModel MachineStateModel { get; }

        ConcurrentQueue<IGCodeLine> CommandQueue { get; }

        IJobExecution JobExecution { get; }

        IReadOnlyDictionary<ushort, IGCodeVariable> Variables { get; }

        bool ProcessGCodeOverrides(IGCodeLine line);

        bool ProcessMCodeOverrides(IGCodeLine line);

        void ProcessAlarm(GrblAlarm alarm);

        void ProcessError(GrblError error);

        void ProcessError(Exception error);

        void SendInformationUpdate(InformationType informationType, string message);

        void Cancel();
    }
}
