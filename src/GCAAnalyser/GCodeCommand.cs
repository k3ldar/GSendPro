
using System;

using static GCAAnalyser.Internal.Consts;

namespace GCAAnalyser
{
    public class GCodeCommand
    {
        private GCodeCommand()
        {
            Code = Int32.MinValue;
        }

        internal GCodeCommand(Dictionary<byte, decimal> commands, string comment)
        {
            if (commands['G' - AsciiAPosition] == Decimal.MinValue)
                throw new ArgumentOutOfRangeException($"Invalid GCode value: G{commands[CharG - AsciiAPosition]}");

            Code = Convert.ToInt32(commands[CharG - AsciiAPosition]);

            M = commands[CharM - AsciiAPosition];
            X = commands[CharX - AsciiAPosition];
            Y = commands[CharY - AsciiAPosition];
            Z = commands[CharZ - AsciiAPosition];
            Speed = commands[CharS - AsciiAPosition];
            FeedRate = commands[CharF - AsciiAPosition];
            Comment = comment ?? String.Empty;
        }

        public int Code { get; private set; }

        public decimal M { get; private set; }

        public decimal X { get; private set; }

        public decimal Y { get; private set; }

        public decimal Z { get; private set; }

        public decimal Speed { get; private set; }

        public decimal FeedRate { get; private set; }

        public string Comment { get; private set; }

        public GCodeCommand PreviousCommand { get; internal set; }

        public GCodeCommand NextCommand { get; internal set; }
    }
}