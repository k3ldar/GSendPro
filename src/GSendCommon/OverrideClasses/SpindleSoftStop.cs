using GSendShared;
using GSendShared.Abstractions;

namespace GSendCommon.OverrideClasses
{
    public sealed class SpindleSoftStop : IGCodeOverride
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

            if (overrideContext.MachineStateModel.SpindleSpeed == 0)
                return false;

            IGCodeCommand startSpindle = overrideContext.GCode.Commands.Find(c =>
                c.Command.Equals('M') && c.CommandValue.Equals(5));

            if (startSpindle == null)
                return false;

            overrideContext.SendCommand = false;
            int spindleSpeed = (int)overrideContext.Processor.StateModel.SpindleSpeed;

            int stepDelay = overrideContext.Machine.SoftStartSeconds * MillisecondsPerSecond / DelayMilliseconds;
            int rpmPerStep = Convert.ToInt32(spindleSpeed / stepDelay);
            int currentRpm = 0;

            for (int i = stepDelay; i > 0; i--)
            {
                if (cancellationToken.IsCancellationRequested || overrideContext.Processor.StateModel.SpindleSpeed == 0)
                    return false;

                currentRpm = i * rpmPerStep;

                if (overrideContext.Machine.SpindleType == SpindleType.Integrated)
                    overrideContext.Processor.QueueCommand($"S{currentRpm}");

                overrideContext.Processor.QueueCommand("M600P0.200");
            }

            if (overrideContext.Machine.SpindleType == SpindleType.Integrated)
                overrideContext.Processor.QueueCommand($"S{0}M5");

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
