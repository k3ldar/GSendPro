using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analyzers
{
    public sealed class AnalyzeFeedsAndSpeeds : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> xCommands = gCodeAnalyses.AllSpecificCommands(Constants.CharX).ToList();
            gCodeAnalyses.FeedX = xCommands.Count > 0 ? xCommands.Max(c => c.FeedRate) : 0;

            List<IGCodeCommand> zCommands = gCodeAnalyses.AllSpecificCommands(Constants.CharZ).ToList();
            gCodeAnalyses.FeedZ = zCommands.Count > 0 ? zCommands.Max(c => c.FeedRate) : 0;
        }
    }
}
