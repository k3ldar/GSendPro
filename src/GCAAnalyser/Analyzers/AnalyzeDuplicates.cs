﻿using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeDuplicates : IGCodeAnalyzer
    {
        public int Order => 0;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            ArgumentNullException.ThrowIfNull(gCodeAnalyses);

            if (gCodeAnalyses.AllCommands.Any(c => c.Attributes.HasFlag(CommandAttributes.Duplicate)))
                gCodeAnalyses.AddOptions(AnalysesOptions.ContainsDuplicates);
        }
    }
}
