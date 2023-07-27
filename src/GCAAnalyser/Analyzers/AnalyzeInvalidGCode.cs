using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeInvalidGCode : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            bool longLine = gCodeAnalyses.AllCommands.Where(c => c.Attributes.HasFlag(CommandAttributes.InvalidLineTooLong)).Any();

            if (longLine)
            {
                gCodeAnalyses.AddOptions(AnalysesOptions.InvalidGCode);
            }
        }
    }
}
