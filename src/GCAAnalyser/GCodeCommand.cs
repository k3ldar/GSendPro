using System.Diagnostics;

using GSendAnalyzer.Internal;

using GSendShared;

using static GSendShared.Constants;

namespace GSendAnalyzer
{
    [DebuggerDisplay("{Command}{CommandValue}; Line: {LineNumber}; Feed: {CurrentFeedRate}; X:{CurrentX}; Y:{CurrentY}; Z:{CurrentZ}; Spindle:{SpindleOn}; Index: {Index}")]
    public class GCodeCommand : IGCodeCommand
    {
        #region Private Members

        private const string ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ%\0";
        private const int DefaultAccelorationXY = 300;
        private const int DefaultAccelerationZ = 30;
        private const int DefaultRapidsXY = 2000;
        private const int DefaultRapidsZ = 1000;
        private const int SecondsPerMinute = 60;
        private IGCodeCommand _previousCommand;
        private IGCodeCommand _nextCommand;
        private readonly CurrentCommandValues _currentCodeValues;
        private IGCodeAnalyses _subAnalyses;

        #endregion Private Members

        #region Constructors

        public GCodeCommand(int index, char currentCommand, decimal commandValue, string commandValueString,
            string comment, List<IGCodeVariableBlock> variables, CurrentCommandValues currentValues,
            int lineNumber, IGCodeAnalyses subAnalyses)
        {

            if ((currentCommand.Equals('\0') && String.IsNullOrEmpty(comment)) || !ValidChars.Contains(currentCommand))
                throw new ArgumentOutOfRangeException(nameof(currentCommand));

            Index = index;
            Command = currentCommand;
            CommandValue = commandValue;
            CommandValueString = commandValueString;
            Comment = comment ?? String.Empty;
            VariableBlocks = variables ?? new();
            _currentCodeValues = currentValues ?? throw new ArgumentNullException(nameof(currentValues));
            Attributes = currentValues.Attributes;
            LineNumber = lineNumber;
            SubAnalyses = subAnalyses;
            UpdateAttributes();
        }

        #endregion Constructors

        #region Properties

        public int LineNumber { get; }

        public int MasterLineNumber { get; internal set; }

        public char Command { get; }

        public string CommandValueString { get; }

        public decimal CommandValue { get; }

        public string Comment { get; }

        public List<IGCodeVariableBlock> VariableBlocks { get; }

        public int Index { get; }

        public decimal CurrentX => _currentCodeValues.X;

        public decimal CurrentY => _currentCodeValues.Y;

        public decimal CurrentZ => _currentCodeValues.Z;

        public decimal CurrentFeedRate => _currentCodeValues.FeedRate;

        public decimal FeedRate { get; internal set; }

        public decimal Distance { get; private set; }

        public TimeSpan Time { get; private set; }

        public decimal SpindleSpeed => _currentCodeValues.SpindleSpeed;

        public bool SpindleOn => _currentCodeValues.SpindleSpeed != 0;

        public bool CoolantEnabled => _currentCodeValues.Coolant;

        public CommandAttributes Attributes { get; internal set; }

        public IGCodeAnalyses SubAnalyses
        {
            get => _subAnalyses;

            private set
            {
                _subAnalyses = value;

                if (_subAnalyses == null)
                    return;

                // bring in parent attributes
                foreach (IGCodeCommand command in _subAnalyses.Commands)
                {
                    foreach (CommandAttributes attr in Enum.GetValues(typeof(CommandAttributes)))
                    {
                        if (attr != CommandAttributes.None && command.Attributes.HasFlag(attr))
                            Attributes |= attr;
                    }
                }
            }
        }

        public IGCodeCommand PreviousCommand
        {
            get => _previousCommand;

            internal set
            {
                _previousCommand = value;

                if (_previousCommand != null)
                {
                    if (_previousCommand.Equals(this))
                    {
                        Attributes |= CommandAttributes.Duplicate;
                    }

                    switch (Command)
                    {
                        case CharZ:
                            if (CurrentZ < _previousCommand.CurrentZ)
                                Attributes |= CommandAttributes.MovementZDown;
                            else if (CurrentZ > _previousCommand.CurrentZ)
                                Attributes |= CommandAttributes.MovementZUp;

                            Distance = Math.Abs(CurrentZ - _previousCommand.CurrentZ);

                            break;

                        case CharX:
                            if (CurrentX < _previousCommand.CurrentX)
                                Attributes |= CommandAttributes.MovementXMinus;
                            else if (CurrentX > _previousCommand.CurrentX)
                                Attributes |= CommandAttributes.MovementXPlus;

                            Distance = Math.Abs(CurrentX - _previousCommand.CurrentX);

                            break;

                        case CharY:
                            if (CurrentY < _previousCommand.CurrentY)
                                Attributes |= CommandAttributes.MovementYMinus;
                            else if (CurrentY > _previousCommand.CurrentY)
                                Attributes |= CommandAttributes.MovementYPlus;

                            Distance = Math.Abs(CurrentY - _previousCommand.CurrentY);

                            break;

                        case CharF:
                            if (_previousCommand is GCodeCommand prevCommand)
                            {
                                prevCommand.FeedRate = CommandValue;
                            }

                            break;
                    }
                }

            }
        }

        public IGCodeCommand NextCommand
        {
            get => _nextCommand;

            internal set
            {
                _nextCommand = value;

                if (Command == 'G' && CommandValue == 1 && _nextCommand != null && _nextCommand is GCodeCommand nextCommand)
                {
                    nextCommand.SendG1 = true;
                }
            }
        }

        public bool SendG1 { get; set; } = false;

        #endregion Properties

        #region Public Methods

        public string CommentStripped(bool replaceVariables)
        {
            string Result = Comment;

            if (String.IsNullOrWhiteSpace(Result))
                return Result;

            if (Result.StartsWith(CharSemiColon))
                Result = Result.Substring(1);
            else if (Result.StartsWith(CharOpeningBracket) && Result.EndsWith(CharClosingBracket))
                Result = Result[1..^1];

            if (replaceVariables && VariableBlocks.Count > 0)
            {
                foreach (IGCodeVariableBlock varBlock in VariableBlocks)
                    Result = Result.Replace(varBlock.VariableBlock, varBlock.Value);
            }

            return Result;
        }

        public string GetCommand()
        {
            string Result = "";

            if (SendG1)
                Result += "G1";

            Result += $"{Command}{CommandValueString}";

            if (FeedRate > 0)
                Result += $"F{CurrentFeedRate}";

            return Result;
        }

        public override bool Equals(object obj)
        {
            if (obj is not GCodeCommand command)
                return false;

            return Command.Equals(command.Command) &&
                CommandValueString.Equals(command.CommandValueString) &&
                Comment.Equals(command.Comment);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Command}{CommandValueString}";
        }
        public void CalculateTime()
        {
            //mm/min based
            if (CurrentFeedRate < 1 || Distance <= 0)
                return;

            decimal velocity = 0.5m / (Command.Equals(CharZ) ? DefaultAccelerationZ : DefaultAccelorationXY);
            int rapids = Command.Equals(CharZ) ? DefaultRapidsZ : DefaultRapidsXY;

            int distPerSecond = rapids / SecondsPerMinute;

            if (Distance > distPerSecond)
                Time = TimeSpan.FromSeconds((double)((Distance / (CurrentFeedRate / SecondsPerMinute)) + (velocity * 2)));
            else
                Time = TimeSpan.FromSeconds((double)(Distance / (CurrentFeedRate / SecondsPerMinute)));
        }

        #endregion Public Methods

        #region Private Methods

        private void UpdateAttributes()
        {
            switch (Command)
            {
                case CharT:
                    Attributes |= CommandAttributes.ToolChange;

                    break;

                case CharM:
                    switch (CommandValue)
                    {
                        case 0:
                            Attributes |= CommandAttributes.UnconditionalStop;
                            break;
                    }

                    break;

                case CharG:
                    switch (CommandValue)
                    {
                        case 2:
                        case 3:
                            Attributes |= CommandAttributes.Arc;
                            break;

                        case 4:
                            Attributes |= CommandAttributes.Dwell;
                            break;
                    }

                    break;

                case CharO:
                    Attributes |= CommandAttributes.SubProgram;
                    break;
            }
        }

        #endregion Private Methods
    }
}