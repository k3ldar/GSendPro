using GSendShared;
using GSendShared.Abstractions;

namespace GSendCommon.OverrideClasses
{
    internal class ToolChangeOverride : IGCodeOverride
    {
        public MachineType MachineType => MachineType.CNC;

        public int SortOrder => Int32.MaxValue;

        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            if (!overrideContext.Machine.Options.HasFlag(MachineOptions.ToolChanger) && 
                (
                    overrideContext.GCode.Commands.Any(c => c.Command.Equals('T')) ||
                    overrideContext.GCode.Commands.Any(c => c.Command.Equals('M') && c.CommandValue.Equals(6))
                )
               )
            {
                overrideContext.SendCommand = false;
                return true;
            }

            return false;
        }

        public void Process(GrblAlarm alarm)
        {

        }

        public void Process(GrblError error)
        {

        }
    }
}
