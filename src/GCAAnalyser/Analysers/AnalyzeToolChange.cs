﻿using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    public class AnalyzeToolChange : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            if (gCodeAnalyses.AllCommands.Any(c => c.Command.Equals('M') && c.CommandValue.Equals(6)))
                gCodeAnalyses.AddOptions(AnalysesOptions.ContainsAutomaticToolChanges);

            List<IGCodeCommand> tools = gCodeAnalyses.AllCommands.Where(c => c.Command.Equals('T')).ToList();

            if (tools.Count > 0)
            {
                gCodeAnalyses.AddOptions(AnalysesOptions.ContainsToolChanges);

                tools.ForEach(t => gCodeAnalyses.Tools += $"{t.CommandValueString},");
            }
            else if (gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.ContainsAutomaticToolChanges))
            {
                if (gCodeAnalyses is GCodeAnalyses codeAnalyses)
                {
                    codeAnalyses.AddError(GSend.Language.Resources.AnalysesError1);
                }
            }

            if (gCodeAnalyses.Tools.EndsWith(","))
                gCodeAnalyses.Tools = gCodeAnalyses.Tools[..^1];
        }
    }
}
