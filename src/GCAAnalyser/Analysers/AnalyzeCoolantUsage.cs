using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeCoolantUsage : IGCodeAnalyzer
    {
        public int Order => int.MaxValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            if (gCodeAnalyses.AllCommands.Any(c => c.ToString().Equals("M7")))
                gCodeAnalyses.AddOptions(AnalysesOptions.UsesMistCoolant);

            if (gCodeAnalyses.AllCommands.Any(c => c.ToString().Equals("M8")))
                gCodeAnalyses.AddOptions(AnalysesOptions.UsesFloodCoolant);

            if (gCodeAnalyses.AllCommands.Any(c => c.ToString().Equals("M9")))
                gCodeAnalyses.AddOptions(AnalysesOptions.TurnsOffCoolant);
        }
    }
}
