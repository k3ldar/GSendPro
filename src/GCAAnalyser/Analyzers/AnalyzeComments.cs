using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analyzers
{
    public sealed class AnalyzeComments : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            gCodeAnalyses.CommentCount = gCodeAnalyses.AllCommands.Count(c => !String.IsNullOrEmpty(c.Comment));
        }
    }
}
