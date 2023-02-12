namespace GCAAnalyser.Internal
{
    internal sealed class CurrentCommandValues
    {
        public char Command { get; set; }

        public decimal FeedRate { get; set; }

        public decimal SpindleSpeed { get; set; }

        public bool Coolant { get; set; }

        public decimal X { get; set; }

        public decimal Y { get; set; }

        public decimal Z { get; set; }

        public CommandAttributes Attributes { get; set; }

        public CurrentCommandValues Clone()
        {
            return new CurrentCommandValues
            {
                Command = Command,
                FeedRate = FeedRate,
                SpindleSpeed = SpindleSpeed,
                Coolant = Coolant,
                X = X,
                Y = Y,
                Z = Z
            };
        }
    }
}
