namespace GSendShared.Abstractions
{
    /// <summary>
    /// Interface used by different overrides
    /// </summary>
    public interface IGCodeOverride
    {
        /// <summary>
        /// Type of machine the override can be used with
        /// </summary>
        MachineType MachineType { get; }

        /// <summary>
        /// Preferred sort order for override
        /// </summary>
        int SortOrder { get; }

        /// <summary>
        /// Processes a line of gcode
        /// </summary>
        /// <param name="overrideContext"></param>
        /// <returns>bool</returns>
        bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken);

        /// <summary>
        /// Process a grbl alarm
        /// </summary>
        /// <param name="alarm"></param>
        void Process(GrblAlarm alarm);

        /// <summary>
        /// Process a grbl error
        /// </summary>
        /// <param name="error"></param>
        void Process(GrblError error);

        /// <summary>
        /// Processes an exception
        /// </summary>
        /// <param name="error"></param>
        void Process(Exception error);
    }
}
