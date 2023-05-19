using GSendShared.Abstractions;

using GSendShared;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeDuplicates : IGCodeAnalyzer
    {
        public int Order => 0;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            if (gCodeAnalyses.Commands.Any(c => c.Attributes.HasFlag(CommandAttributes.Duplicate)))
                gCodeAnalyses.AddOptions(AnalysesOptions.ContainsDuplicates);
        }
    }
}
