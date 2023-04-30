﻿using System.Diagnostics;

using GSendAnalyser.Internal;

using GSendShared;

using static GSendAnalyser.Internal.Consts;

namespace GSendAnalyser
{
    [DebuggerDisplay("{Command}{CommandValue}; Feed: {CurrentFeedRate}; X:{CurrentX}; Y:{CurrentY}; Z:{CurrentZ}; Spindle:{SpindleOn}; Index: {Index}")]
    public class GCodeCommand : IGCodeCommand
    {
        #region Private Members

        private const int DefaultAccelorationXY = 300;
        private const int DefaultAccelerationZ = 30;
        private const int DefaultRapidsXY = 2000;
        private const int DefaultRapidsZ = 1000;
        private const int SecondsPerMinute = 60;
        private IGCodeCommand _previousCommand;
        private IGCodeCommand _nextCommand;
        private readonly CurrentCommandValues _currentCodeValues;

        #endregion Private Members

        #region Constructors

        public GCodeCommand(int index, char currentCommand, decimal commandValue, string commandValueString, 
            string comment, CurrentCommandValues currentValues, int lineNumber)
        {
            if ((currentCommand < 'A' || currentCommand > 'Z') && currentCommand != '%')
                throw new ArgumentOutOfRangeException(nameof(currentCommand));

            Index = index;
            Command = currentCommand;
            CommandValue = commandValue;
            CommandValueString = commandValueString;
            Comment = comment ?? String.Empty;
            _currentCodeValues = currentValues ?? throw new ArgumentNullException(nameof(currentValues));
            Attributes = currentValues.Attributes;
            LineNumber = lineNumber;
        }

        #endregion Constructors

        #region Properties

        public int LineNumber { get; }

        public char Command { get; }

        public string CommandValueString { get; }

        public decimal CommandValue { get; }

        public string Comment { get; }

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
                CommandValueString.Equals(command.CommandValueString);
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


        #endregion Private Methods
    }
}