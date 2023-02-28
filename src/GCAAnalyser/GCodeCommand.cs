using System.Diagnostics;

using GSendAnalyser.Internal;

using GSendShared;

using static GSendAnalyser.Internal.Consts;

namespace GSendAnalyser
{
    [DebuggerDisplay("{Command}{CommandValue}; X:{X}; Y:{Y}; Z:{Z}; Spindle:{SpindleOn}; Index: {Index}")]
    public class GCodeCommand : IGCodeCommand
    {
        #region Private Members

        private const int DefaultAccelorationXY = 300;
        private const int DefaultAccelerationZ = 30;
        private const int DefaultRapidsXY = 2000;
        private const int DefaultRapidsZ = 1000;
        private const int SecondsPerMinute = 60;
        private IGCodeCommand _previousCommand;
        private readonly CurrentCommandValues _currentCodeValues;

        #endregion Private Members

        #region Constructors

        public GCodeCommand(int index, char currentCommand, decimal commandValue, string commandValueString, string comment, CurrentCommandValues currentValues)
        {
            if (currentCommand < 'A' || currentCommand > 'Z')
                throw new ArgumentOutOfRangeException(nameof(currentCommand));

            Index = index;
            Command = currentCommand;
            CommandValue = commandValue;
            CommandValueString = commandValueString;
            Comment = comment ?? String.Empty;
            _currentCodeValues = currentValues ?? throw new ArgumentNullException(nameof(currentValues));
        }

        #endregion Constructors

        #region Properties

        public char Command { get; }

        public string CommandValueString { get; }

        public decimal CommandValue { get; }

        public string Comment { get; }

        public int Index { get; }

        public decimal X => _currentCodeValues.X;

        public decimal Y => _currentCodeValues.Y;

        public decimal Z => _currentCodeValues.Z;

        public decimal FeedRate => _currentCodeValues.FeedRate;

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
                            if (Z < _previousCommand.Z)
                                Attributes |= CommandAttributes.MovementZDown;
                            else if (Z > _previousCommand.Z)
                                Attributes |= CommandAttributes.MovementZUp;

                            Distance = Math.Abs(Z - _previousCommand.Z);

                            break;

                        case CharX:
                            if (X < _previousCommand.X)
                                Attributes |= CommandAttributes.MovementXMinus;
                            else if (X > _previousCommand.X)
                                Attributes |= CommandAttributes.MovementXPlus;

                            Distance = Math.Abs(X - _previousCommand.X);

                            break;

                        case CharY:
                            if (Y < _previousCommand.Y)
                                Attributes |= CommandAttributes.MovementYMinus;
                            else if (Y > _previousCommand.Y)
                                Attributes |= CommandAttributes.MovementYPlus;

                            Distance = Math.Abs(Y - _previousCommand.Y);

                            break;
                    }
                }

            }
        }

        public IGCodeCommand NextCommand { get; internal set; }

        #endregion Properties

        #region Public Methods

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
            if (FeedRate < 1 || Distance <= 0)
                return;

            decimal velocity = 0.5m / (Command.Equals(CharZ) ? DefaultAccelerationZ : DefaultAccelorationXY);
            int rapids = Command.Equals(CharZ) ? DefaultRapidsZ : DefaultRapidsXY;

            int distPerSecond = rapids / SecondsPerMinute;

            if (Distance > distPerSecond)
                Time = TimeSpan.FromSeconds((double)((Distance / (FeedRate / SecondsPerMinute)) + (velocity * 2)));
            else
                Time = TimeSpan.FromSeconds((double)(Distance / (FeedRate / SecondsPerMinute)));
        }

        #endregion Public Methods

        #region Private Methods


        #endregion Private Methods
    }
}