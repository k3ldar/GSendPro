using GSendShared;
using GSendShared.Interfaces;

namespace GSendCommon.Overrides
{
    public sealed class SpindleSoftStart : IGCodeOverride
    {
        private const int MillisecondsPerSecond = 1000;
        private const int DelayMilliseconds = 200;

        public int SortOrder => 0;

        public void Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            if (overrideContext.Machine.MachineType != MachineType.CNC)
                return;

            if (!overrideContext.Machine.Options.HasFlag(MachineOptions.SoftStart))
                return;

            if (overrideContext.Machine.SpindleType != SpindleType.Integrated)
                return;

            IGCodeCommand startSpindle = overrideContext.GCode.Commands.FirstOrDefault(c =>
                c.Command.Equals('M') && (c.CommandValue.Equals(3) || c.CommandValue.Equals(4)));

            if (startSpindle != null)
            {
                IGCodeCommand spindleSpeed = overrideContext.GCode.Commands.FirstOrDefault(c => c.Command.Equals('S'));
                overrideContext.SendCommand = false;

                if (spindleSpeed == null)
                    return;

                int stepDelay = overrideContext.Machine.SoftStartSeconds * MillisecondsPerSecond / DelayMilliseconds;
                int rpmPerStep = Convert.ToInt32(spindleSpeed.CommandValue / stepDelay);
                int currentRpm = 0;

                for (int i = 1; i <= stepDelay; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;

                    currentRpm = i * rpmPerStep;
                    overrideContext.ComPort.WriteLine($"S{currentRpm}M{startSpindle.CommandValue}");
                    overrideContext.StaticMethods.Sleep(200);
                }

                if (currentRpm < spindleSpeed.CommandValue)
                    overrideContext.ComPort.WriteLine(overrideContext.GCode.GetGCode());

                return;
            }
        }

        public void Process(GrblAlarm alarm)
        {
            
        }

        public void Process(GrblError error)
        {
            
        }
    }
}
