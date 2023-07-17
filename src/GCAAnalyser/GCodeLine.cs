using System.Diagnostics;
using System.Text;

using GSendAnalyser.Internal;

using GSendShared;

namespace GSendAnalyser
{
    [DebuggerDisplay("{GetGCodeInfo()}")]
    public sealed class GCodeLine : IGCodeLine
    {
        private readonly IGCodeAnalyses _codeAnalyses;

        public GCodeLine(IGCodeAnalyses gCodeAnalyses)
            : this(gCodeAnalyses, null)
        {
        }

        public GCodeLine(IGCodeAnalyses gCodeAnalyses, IGCodeCommand command)
        {
            _codeAnalyses = gCodeAnalyses ?? throw new ArgumentNullException(nameof(gCodeAnalyses));

            if (command != null)
            {
                LineNumber = command.LineNumber;
                MasterLineNumber = command.MasterLineNumber;
            }
        }

        public LineStatus Status { get; set; }

        public List<IGCodeCommand> Commands { get; } = new();

        public int LineNumber { get; }

        public int MasterLineNumber { get; }

        public string GetGCode()
        {
            StringBuilder Result = new();

            foreach (IGCodeCommand command in Commands)
            {
                if (command.Command.Equals('%'))
                    Result.Append(command.Command);
                else if (String.IsNullOrEmpty(command.CommandValueString))
                    Result.Append($"{command.Command}{command.CommandValue}");
                else
                    Result.Append($"{command.Command}{command.CommandValueString}");

                foreach (IGCodeVariableBlock varBlock in command.VariableBlocks)
                    Result.Replace(varBlock.VariableBlock, varBlock.Value);
            }

            return Result.ToString();
        }

        public IGCodeLine GetGCode(int feedRate)
        {
            GCodeLine Result = new(_codeAnalyses);

            bool feedRateFound = false;
            int index = 0;

            foreach (IGCodeCommand command in Commands)
            {
                if (command.Command.Equals('%'))
                {
                    Result.Commands.Add(command);
                }
                else
                {
                    if (command.Command.Equals('F'))
                    {
                        feedRateFound = true;
                        Result.Commands.Add(new GCodeCommand(index, 'F', feedRate, feedRate.ToString(), String.Empty, command.VariableBlocks, new CurrentCommandValues(), -2, _codeAnalyses));
                    }
                    else
                    {
                        Result.Commands.Add(command);
                    }
                }

                index++;
            }

            if (!feedRateFound)
                Result.Commands.Add(new GCodeCommand(index, 'F', feedRate, feedRate.ToString(), String.Empty, null, new CurrentCommandValues(), -2, _codeAnalyses));

            return Result;
        }

        public bool IsCommentOnly => (Commands.Count == 1 && Commands[0].Command.Equals('\0') && !String.IsNullOrEmpty(Commands[0].Comment));

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
