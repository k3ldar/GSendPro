using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeDistance : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            ArgumentNullException.ThrowIfNull(gCodeAnalyses);

            gCodeAnalyses.TotalDistance = gCodeAnalyses.AllCommands.Sum(c => c.Distance);
        }
    }
}
