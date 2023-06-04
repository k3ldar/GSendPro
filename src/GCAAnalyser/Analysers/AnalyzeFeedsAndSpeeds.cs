using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    public sealed class AnalyzeFeedsAndSpeeds : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> xCommands = gCodeAnalyses.AllCommands.Where(c => c.Command.Equals('X')).ToList();
            gCodeAnalyses.FeedX = xCommands.Count > 0 ? xCommands.Max(c => c.FeedRate) : 0;

            List<IGCodeCommand> zCommands = gCodeAnalyses.AllCommands.Where(c => c.Command.Equals('Z')).ToList();
            gCodeAnalyses.FeedZ = zCommands.Count > 0 ? zCommands.Max(c => c.FeedRate) : 0;
        }
    }
}
