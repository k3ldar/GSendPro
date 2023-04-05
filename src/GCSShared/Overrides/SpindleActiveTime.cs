using GSendShared.Interfaces;

namespace GSendShared.Overrides
{
    /// <summary>
    /// Logs spindle active time
    /// </summary>
    public sealed class SpindleActiveTime : IGCodeOverride
    {
        public int SortOrder => int.MinValue;

        public void Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            if (overrideContext.GCode.Commands.Any(c => c.Command.Equals('M') && (c.CommandValue.Equals(3) || c.CommandValue.Equals(4))))
            {
                // turn on spindle timer
            }
        }
    }
}
