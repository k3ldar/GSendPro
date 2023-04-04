using GSendShared.Interfaces;

namespace GSendShared.Overrides
{
    /// <summary>
    /// Logs spindle active time
    /// </summary>
    public sealed class SpindleActiveTime : IGCodeOverride
    {
        public int SortOrder => int.MinValue;

        public async Task Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (overrideContext.Machine.MachineType != MachineType.CNC)
                    return;

                if (overrideContext.GCode.Commands.Any(c => c.Command.Equals('M') && (c.CommandValue.Equals(3) || c.CommandValue.Equals(4))))
                {
                    // turn on spindle timer
                }
            });
        }
    }
}
