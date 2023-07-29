using GSendShared;
using GSendShared.Abstractions;

namespace GSendCommon.OverrideClasses
{
    internal class FeedAndSpeedOverride : IGCodeOverride
    {

        public FeedAndSpeedOverride()
        {
            OfficialXYFeedRate = -1;
            OfficialZFeedRate = -1;
            IsG1Command = false;
        }

        public MachineType MachineType => MachineType.CNC;

        public int SortOrder => 0;

        public decimal OfficialXYFeedRate { get; private set; }

        public decimal OfficialZFeedRate { get; private set; }

        public decimal CurrentXYFeedRate { get; private set; }

        public decimal CurrentZUpFeedRate { get; private set; }

        public decimal CurrentZDownFeedRate { get; private set; }

        public bool PreviousXYOverride { get; private set; }

        public bool Overriding => (OfficialXYFeedRate != CurrentXYFeedRate) || (OfficialZFeedRate != CurrentZDownFeedRate) || (OfficialZFeedRate != CurrentZUpFeedRate);

        public bool IsG1Command { get; private set; }

        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            IGCodeCommand feedRate = overrideContext.GCode.Commands.FirstOrDefault(c => c.Command.Equals('F'));

            IsG1Command = !overrideContext.GCode.Commands.Any(c => c.Command.Equals('G') && c.CommandValue.Equals(0)) &&
                (overrideContext.GCode.Commands.Any(c => c.Command.Equals('G') && c.CommandValue.Equals(1)) ||
                IsG1Command);

            if (!IsG1Command)
                return false;

            IGCodeCommand zFeed = overrideContext.GCode.Commands.FirstOrDefault(c => c.Command.Equals('Z'));
            IGCodeCommand xyFeed = overrideContext.GCode.Commands.FirstOrDefault(c => c.Command.Equals('X') || c.Command.Equals('Y'));

            if (feedRate != null)
            {
                if (zFeed != null)
                {
                    if (overrideContext.MachineStateModel.Overrides.OverrideZDown && zFeed.Attributes.HasFlag(CommandAttributes.MovementZDown) &&
                        overrideContext.MachineStateModel.Overrides.AxisZDown.NewValue != overrideContext.MachineStateModel.Overrides.AxisZDown.OriginalValue)
                    {
                        overrideContext.CommandQueue.Enqueue(overrideContext.GCode.GetGCode((int)overrideContext.MachineStateModel.Overrides.AxisZDown.NewValue));
                        overrideContext.SendCommand = false;
                        return true;
                    }
                    else if (overrideContext.MachineStateModel.Overrides.OverrideZUp && zFeed.Attributes.HasFlag(CommandAttributes.MovementZUp))
                    {
                        overrideContext.CommandQueue.Enqueue(overrideContext.GCode.GetGCode((int)overrideContext.MachineStateModel.Overrides.AxisZUp.NewValue));
                        overrideContext.SendCommand = false;
                        return true;
                    }
                }
            }

            if (feedRate != null)
            {
                if ((zFeed != null && xyFeed != null) || (zFeed == null && xyFeed == null))
                {
                    // invalid as unsure whether feed rate is aimed at x, y or z
                    return false;
                }
                else if (zFeed != null && OfficialZFeedRate != feedRate.CommandValue)
                {
                    OfficialZFeedRate = feedRate.CommandValue;
                    CurrentZDownFeedRate = feedRate.CommandValue;
                    CurrentZUpFeedRate = feedRate.CommandValue;
                    overrideContext.MachineStateModel.Overrides.AxisZDown.NewValue = (int)feedRate.CommandValue;
                    overrideContext.MachineStateModel.Overrides.AxisZUp.NewValue = (int)feedRate.CommandValue;
                }
                else if (xyFeed != null && OfficialXYFeedRate != feedRate.CommandValue)
                {
                    OfficialXYFeedRate = feedRate.CommandValue;
                    CurrentXYFeedRate = feedRate.CommandValue;
                    overrideContext.MachineStateModel.Overrides.AxisXY.NewValue = (int)feedRate.CommandValue;
                }
            }

            if (xyFeed != null)
            {
                bool isOverrideXy = overrideContext.MachineStateModel.Overrides.OverrideXY && !overrideContext.MachineStateModel.Overrides.OverridesDisabled;

                if (!PreviousXYOverride && isOverrideXy)
                {
                    CurrentXYFeedRate = overrideContext.MachineStateModel.Overrides.AxisXY.NewValue;
                    overrideContext.CommandQueue.Enqueue(overrideContext.GCode.GetGCode((int)CurrentXYFeedRate));
                    overrideContext.SendCommand = false;
                    PreviousXYOverride = true;
                    return true;
                }
                else if (!PreviousXYOverride)
                {
                    return false;
                }
                else if (PreviousXYOverride && (!overrideContext.MachineStateModel.Overrides.OverrideXY || overrideContext.MachineStateModel.Overrides.OverridesDisabled))
                {
                    CurrentXYFeedRate = overrideContext.MachineStateModel.Overrides.AxisXY.OriginalValue;
                    overrideContext.CommandQueue.Enqueue(overrideContext.GCode.GetGCode((int)CurrentXYFeedRate));
                    overrideContext.SendCommand = false;
                    PreviousXYOverride = false;
                    return true;
                }
                else if ((PreviousXYOverride &&
                    (!overrideContext.MachineStateModel.Overrides.OverrideXY || !overrideContext.MachineStateModel.Overrides.OverridesDisabled))
                    && CurrentXYFeedRate != overrideContext.MachineStateModel.Overrides.AxisXY.NewValue)
                {
                    CurrentXYFeedRate = overrideContext.MachineStateModel.Overrides.AxisXY.NewValue;
                    overrideContext.CommandQueue.Enqueue(overrideContext.GCode.GetGCode((int)CurrentXYFeedRate));
                    overrideContext.SendCommand = false;
                    return true;
                }
                else if (isOverrideXy && feedRate != null && feedRate.CommandValue != CurrentXYFeedRate)
                {
                    overrideContext.CommandQueue.Enqueue(overrideContext.GCode.GetGCode((int)CurrentXYFeedRate));
                    overrideContext.SendCommand = false;
                    return true;
                }
            }


            //var xFeed = overrideContext.GCode.Commands.Whe
            return false;
        }

        public void Process(GrblAlarm alarm)
        {

        }

        public void Process(GrblError error)
        {

        }

        public void Process(Exception error)
        {

        }
    }
}
