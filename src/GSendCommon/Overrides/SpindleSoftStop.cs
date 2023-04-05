using GSendShared;
using GSendShared.Interfaces;

namespace GSendCommon.Overrides
{
    public sealed class SpindleSoftStop : IGCodeOverride
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
                c.Command.Equals('M') && c.CommandValue.Equals(5));

            if (startSpindle != null)
            {
                int spindleSpeed = (int)overrideContext.Processor.StateModel.SpindleSpeed;

                int stepDelay = overrideContext.Machine.SoftStartSeconds * MillisecondsPerSecond / DelayMilliseconds;
                int rpmPerStep = Convert.ToInt32(spindleSpeed / stepDelay);
                int currentRpm = 0;
                int spindleStartValue = overrideContext.Processor.StateModel.SpindleClockWise ? 3 : 4;

                for (int i = stepDelay; i > 0; i--)
                {
                    if (cancellationToken.IsCancellationRequested || overrideContext.Processor.StateModel.SpindleSpeed == 0)
                        return;

                    currentRpm = i * rpmPerStep;
                    overrideContext.ComPort.WriteLine($"S{currentRpm}M{spindleStartValue}");
                    overrideContext.StaticMethods.Sleep(200);
                }

                overrideContext.ComPort.WriteLine(overrideContext.GCode.GetGCode());

                return;
            }
        }
    }
}
