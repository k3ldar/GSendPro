using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Interfaces;
using GSendShared;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeInvalidGCode : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            bool longLine = gCodeAnalyses.Commands.Where(c => c.Attributes.HasFlag(CommandAttributes.InvalidLineTooLong)).Any();

            if (longLine)
            {
                gCodeAnalyses.AddOptions(AnalysesOptions.InvalidGCode);
            }
        }
    }
}
