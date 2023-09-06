using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeInvalidGCode : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            bool longLine = gCodeAnalyses.AllCommands.Any(c => c.Attributes.HasFlag(CommandAttributes.InvalidLineTooLong));

            if (longLine)
            {
                gCodeAnalyses.AddOptions(AnalysesOptions.InvalidGCode);
            }

            var invalidGCode = gCodeAnalyses.AllCommands.Where(c => !c.IsValidGCode).ToList();

            if (invalidGCode.Count > 0 && gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                foreach (var command in invalidGCode)
                {
                    codeAnalyses.AddError(GSend.Language.Resources.AnalyzeError38, command.LineNumber);
                }
            }
        }
    }
}
