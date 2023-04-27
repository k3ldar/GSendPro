using GSendShared;
using GSendShared.Interfaces;

namespace GSendAnalyser.Analysers
{
    public sealed class AnalyzeComments : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            gCodeAnalyses.CommentCount = gCodeAnalyses.Commands.Count(c => !String.IsNullOrEmpty(c.Comment));
        }
    }
}
