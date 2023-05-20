using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Models;

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

        public bool Overriding => (OfficialXYFeedRate != CurrentXYFeedRate) || (OfficialZFeedRate != CurrentZDownFeedRate) || (OfficialZFeedRate != CurrentZUpFeedRate);

        public bool IsG1Command { get; private set; }

        public bool Process(IGCodeOverrideContext overrideContext, CancellationToken cancellationToken)
        {
            IGCodeCommand feedRate = overrideContext.GCode.Commands.FirstOrDefault(c => c.Command.Equals('F'));
            IsG1Command = overrideContext.GCode.Commands.Any(c => c.Command.Equals('G') && c.CommandValue.Equals(1));

            IGCodeCommand zFeed = overrideContext.GCode.Commands.FirstOrDefault(c => c.Command.Equals('Z'));
            IGCodeCommand xyFeed = overrideContext.GCode.Commands.FirstOrDefault(c => c.Command.Equals('X') || c.Command.Equals('Y'));

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

            // get out early if not overriding
            if (overrideContext.MachineStateModel.Overrides.OverridesDisabled)
            {
                if (Overriding)
                {
                    // send original values for next x, y, z commands
                }

                return false;
            }

            if (xyFeed != null && overrideContext.MachineStateModel.Overrides.OverrideXY)
            {
                overrideContext.CommandQueue.Enqueue(overrideContext.GCode.GetGCode(overrideContext.MachineStateModel.Overrides.AxisXY.NewValue));
                overrideContext.SendCommand = false;
                return true;
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
    }
}
