using GSendAnalyser.Abstractions;

using GSendShared;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeDistance : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            gCodeAnalyses.TotalDistance = gCodeAnalyses.Commands.Sum(c => c.Distance);
        }
    }
}
