using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSendShared
{
    public interface IGCodeCommand
    {
        public char Command { get; }

        public string CommandValueString { get; }

        public decimal CommandValue { get; }

        public string Comment { get; }

        public int Index { get; }

        public decimal X { get; }

        public decimal Y { get; }

        public decimal Z { get; }

        public decimal FeedRate { get; }

        public decimal Distance { get; }

        public TimeSpan Time { get; }

        public decimal SpindleSpeed { get; }

        public bool SpindleOn { get; }

        public bool CoolantEnabled { get; }

        public CommandAttributes Attributes { get; }
    }
}
