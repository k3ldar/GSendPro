using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeCoolantUsage : IGCodeAnalyzer
    {
        public int Order => int.MaxValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            if (gCodeAnalyses.AllSpecificCommands(Constants.CharM).Any(c => c.CommandValue.Equals(7)))
                gCodeAnalyses.AddOptions(AnalysesOptions.UsesMistCoolant);

            if (gCodeAnalyses.AllSpecificCommands(Constants.CharM).Any(c => c.CommandValue.Equals(8)))
                gCodeAnalyses.AddOptions(AnalysesOptions.UsesFloodCoolant);

            if (gCodeAnalyses.AllSpecificCommands(Constants.CharM).Any(c => c.CommandValue.Equals(9)))
                gCodeAnalyses.AddOptions(AnalysesOptions.TurnsOffCoolant);
        }
    }
}
