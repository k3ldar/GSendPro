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

            if (gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                foreach (var command in invalidGCode)
                {
                    codeAnalyses.AddError(GSend.Language.Resources.AnalyzeError38, command.LineNumber);
                }

                foreach (var command in gCodeAnalyses.AllCommands.Where(c => c.Attributes.HasFlag(CommandAttributes.InvalidGCode)))
                {
                    codeAnalyses.AddError(GSend.Language.Resources.AnalyzeError39, command.LineNumber);
                }
            }
        }
    }
}
