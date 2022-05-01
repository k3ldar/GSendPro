
using static GCAAnalyser.Internal.Consts;

namespace GCAAnalyser
{
    public class GCodeCommand
    {
        #region Private Members

        private GCodeCommand _previousCommand;

        #endregion Private Members

        #region Constructors

        private GCodeCommand()
        {
            Code = Int32.MinValue;
        }

        internal GCodeCommand(int index, Dictionary<byte, decimal> commands, string comment)
        {
            if (commands['G' - AsciiAPosition] == Decimal.MinValue)
                throw new ArgumentOutOfRangeException($"Invalid GCode value: G{commands[CharG - AsciiAPosition]}");

            Index = index;
            Code = Convert.ToInt32(commands[CharG - AsciiAPosition]);

            M = commands[CharM - AsciiAPosition];
            X = commands[CharX - AsciiAPosition];
            Y = commands[CharY - AsciiAPosition];
            Z = commands[CharZ - AsciiAPosition];
            Speed = commands[CharS - AsciiAPosition];
            FeedRate = commands[CharF - AsciiAPosition];
            Comment = comment ?? String.Empty;

            if (Speed > 0)
                Attributes |= CommandAttributes.SpindleSpeed;
        }

        #endregion Constructors

        #region Properties

        public int Code { get; private set; }

        public decimal M { get; private set; }

        public decimal X { get; private set; }

        public decimal Y { get; private set; }

        public decimal Z { get; private set; }

        public decimal Speed { get; private set; }

        public decimal FeedRate { get; private set; }

        public string Comment { get; private set; }

        public int Index { get; private set; }

        public CommandAttributes Attributes { get; internal set; }

        public GCodeCommand PreviousCommand 
        { 
            get => PreviousCommand;

            internal set
            {
                if (value != null)
                {
                    if (value.Equals(this))
                        Attributes |= CommandAttributes.Duplicate;

                    if (Code == value.Code && FeedRate == 0)
                        FeedRate = value.FeedRate;

                    if (Code == value.Code && M > 0 && M == value.M && Speed == 0)
                        Speed = value.Speed;
                }

                _previousCommand = value;
            }
        }

        public GCodeCommand NextCommand { get; set; }

        #endregion Properties

        #region Public Methods

        public override bool Equals(object obj)
        {
            GCodeCommand command = obj as GCodeCommand;

            if (command == null)
                return false;

            return Code.Equals(command.Code) &&
                X.Equals(command.X) && 
                Y.Equals(command.Y) && 
                Z.Equals(command.Z) && 
                M.Equals(command.M) &&
                FeedRate.Equals(command.FeedRate) &&
                Speed.Equals(command.Speed);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"G{Code}; X:{X}; Y:{Y}; Z:{Z}; F:{FeedRate}; Index: {Index}";
        }

        #endregion Public Methods

        #region Private Methods


        #endregion Private Methods
    }
}