﻿
using GCAAnalyser.Abstractions;

namespace GCAAnalyser.Analysers
{
    internal class AnalyzeDuplicates : IGCodeAnalyzer
    {
        public int Order => 0;

        public void Analyze(IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            gCodeAnalyses.ContainsDuplicates = gCodeAnalyses
                .Commands.Where(c => c.Attributes.HasFlag(CommandAttributes.Duplicate))
                .Any();
        }
    }
}
