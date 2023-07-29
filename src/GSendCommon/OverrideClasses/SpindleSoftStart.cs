using GSendShared;
using GSendShared.Abstractions;

namespace GSendCommon.OverrideClasses
{
    public sealed class SpindleSoftStart : IGCodeOverride
    {
        private const int MillisecondsPerSecond = 1000;
        private const int DelayMilliseconds = 200;

        public MachineType MachineType => MachineType.CNC;

        public int SortOrder => 0;

        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            if (overrideContext.Machine.MachineType != MachineType.CNC)
                return false;

            if (!overrideContext.Machine.Options.HasFlag(MachineOptions.SoftStart))
                return false;

            if (overrideContext.Machine.SpindleType != SpindleType.Integrated)
                return false;

            if (overrideContext.MachineStateModel.SpindleSpeed > 0)
                return false;

            IGCodeCommand startSpindle = overrideContext.GCode.Commands.FirstOrDefault(c =>
                c.Command.Equals('M') && (c.CommandValue.Equals(3) || c.CommandValue.Equals(4)));

            if (startSpindle == null)
                return false;

            IGCodeCommand spindleSpeed = overrideContext.GCode.Commands.FirstOrDefault(c => c.Command.Equals('S'));

            if (spindleSpeed == null)
                return false;

            overrideContext.SendCommand = false;
            int stepDelay = overrideContext.Machine.SoftStartSeconds * MillisecondsPerSecond / DelayMilliseconds;
            int currentRpm = (int)overrideContext.MachineStateModel.SpindleSpeed;
            int rpmPerStep = Convert.ToInt32(spindleSpeed.CommandValue / stepDelay);

            for (int i = 1; i <= stepDelay; i++)
            {
                if (cancellationToken.IsCancellationRequested)
                    return false;

                currentRpm = i * rpmPerStep;
                overrideContext.Processor.QueueCommand($"S{currentRpm}M{startSpindle.CommandValue}");
                overrideContext.Processor.QueueCommand("M600P0.200");
            }

            if (currentRpm < spindleSpeed.CommandValue)
                overrideContext.Processor.QueueCommand(overrideContext.GCode.GetGCode());

            return true;
        }

        public void Process(GrblAlarm alarm)
        {
            // not used in this context
        }

        public void Process(GrblError error)
        {
            // not used in this context
        }

        public void Process(Exception error)
        {

        }
    }
}
