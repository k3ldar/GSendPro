﻿namespace GSendShared.Interfaces
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

        bool SendCommand { get; set; }

        /// <summary>
        /// Machine processing the GCode
        /// </summary>
        IGCodeProcessor Processor { get; }

        /// <summary>
        /// Com port used by processor
        /// </summary>
        IComPort ComPort { get; }

        IMachine Machine { get; }

        IStaticMethods StaticMethods { get; }

        bool HasCancelled { get; }

        void ProcessGCodeLine(IGCodeLine line);

        void Cancel();
    }
}