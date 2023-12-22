using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    public class AnalyzeToolChange : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            ArgumentNullException.ThrowIfNull(gCodeAnalyses);

            if (gCodeAnalyses.AllSpecificCommands(Constants.CharM).Any(c => c.CommandValue.Equals(6)))
                gCodeAnalyses.AddOptions(AnalysesOptions.ContainsAutomaticToolChanges);

            List<IGCodeCommand> tools = gCodeAnalyses.AllSpecificCommands(Constants.CharT).ToList();

            if (tools.Count > 0)
            {
                gCodeAnalyses.AddOptions(AnalysesOptions.ContainsToolChanges);

                tools.ForEach(t => gCodeAnalyses.Tools += $"{t.CommandValueString},");
            }
            else if (gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.ContainsAutomaticToolChanges) && (gCodeAnalyses is GCodeAnalyses codeAnalyses))
            {
                codeAnalyses.AddError(GSend.Language.Resources.AnalyzeError1);
            }

            if (gCodeAnalyses.Tools.EndsWith(','))
                gCodeAnalyses.Tools = gCodeAnalyses.Tools[..^1];
        }
    }
}
