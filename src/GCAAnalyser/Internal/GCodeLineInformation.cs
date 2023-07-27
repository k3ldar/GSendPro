using System.Diagnostics;

using GSendShared;

namespace GSendAnalyzer.Internal
{
    [DebuggerDisplay("{GCode}")]
    internal sealed class GCodeLineInformation : IGCodeLineInfo
    {
        public GCodeLineInformation()
        {
            Comments = String.Empty;
        }

        public string GCode { get; set; }

        public decimal FeedRate { get; set; }

        public decimal SpindleSpeed { get; set; }

        public bool SpindleActive { get; set; }

        public CommandAttributes Attributes { get; set; }

        public string Comments { get; set; }
    }
}
