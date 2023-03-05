using GSendShared;

namespace GSendAnalyser.Internal
{
    public sealed class CurrentCommandValues
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
                Attributes = this.Attributes,
                Command = this.Command,
                FeedRate = this.FeedRate,
                SpindleSpeed = this.SpindleSpeed,
                Coolant = this.Coolant,
                X = this.X,
                Y = this.Y,
                Z = this.Z
            };
        }
    }
}
