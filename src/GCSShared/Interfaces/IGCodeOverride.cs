namespace GSendShared.Interfaces
{
    /// <summary>
    /// Interface used by different overrides
    /// </summary>
    public interface IGCodeOverride
    {
        /// <summary>
        /// Preferred sort order for override
        /// </summary>
        int SortOrder { get; }

        /// <summary>
        /// Processes a line of gcode
        /// </summary>
        /// <param name="overrideContext"></param>
        /// <returns>bool</returns>
        void Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken);
    }
}
