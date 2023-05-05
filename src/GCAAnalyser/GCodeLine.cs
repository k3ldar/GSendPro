using System.Diagnostics;
using System.Text;

using GSendAnalyser.Internal;

using GSendShared;

namespace GSendAnalyser
{
    [DebuggerDisplay("{GetGCodeInfo()}")]
    public sealed class GCodeLine : IGCodeLine
    {
        public LineStatus Status { get; set; }

        public List<IGCodeCommand> Commands { get; } = new();

        public string GetGCode()
        {
            StringBuilder Result = new();

            foreach (IGCodeCommand command in Commands)
            {
                if (command.Command.Equals('%'))
                    Result.Append(command.Command);
                else
                    Result.Append($"{command.Command}{command.CommandValue}");
            }

            return Result.ToString();
        }

        public IGCodeLineInfo GetGCodeInfo()
        {
            GCodeLineInformation Result = new();
            StringBuilder sb = new();

            foreach (IGCodeCommand command in Commands)
            {
                if (command.Command.Equals('%'))
                    sb.Append(command.Command);
                else
                    sb.Append($"{command.Command}{command.CommandValue}");

                Result.Comments += command.Comment;

                if (Result.FeedRate == 0 && command.FeedRate > 0)
                    Result.FeedRate = command.FeedRate;

                Result.Attributes |= command.Attributes;
                Result.SpindleActive = command.SpindleOn;
                
                if (Result.SpindleSpeed == 0 && command.SpindleSpeed > 0)
                    Result.SpindleSpeed = command.SpindleSpeed;
            }

            if (Result.Attributes.HasFlag(CommandAttributes.HomeZ) && Result.Attributes.HasFlag(CommandAttributes.SafeZ))
                Result.Attributes &= ~CommandAttributes.HomeZ;

            Result.GCode = sb.ToString();

            return Result;
        }
    }
}
