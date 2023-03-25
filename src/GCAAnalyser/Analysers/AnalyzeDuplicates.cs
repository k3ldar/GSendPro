using GSendShared.Interfaces;

using GSendShared;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeDuplicates : IGCodeAnalyzer
    {
        public int Order => 0;

        public void Analyze(IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            gCodeAnalyses.ContainsDuplicates = gCodeAnalyses.Commands.Any(c => c.Attributes.HasFlag(CommandAttributes.Duplicate));
        }
    }
}
