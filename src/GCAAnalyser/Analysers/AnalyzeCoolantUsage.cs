using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Interfaces;
using GSendShared;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeCoolantUsage : IGCodeAnalyzer
    {
        public int Order => int.MaxValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            if (gCodeAnalyses.Commands.Any(c => c.ToString().Equals("M7")))
                gCodeAnalyses.AddOptions(AnalysesOptions.UsesMistCoolant);

            if (gCodeAnalyses.Commands.Any(c => c.ToString().Equals("M8")))
                gCodeAnalyses.AddOptions(AnalysesOptions.UsesFloodCoolant);

            if (gCodeAnalyses.Commands.Any(c => c.ToString().Equals("M9")))
                gCodeAnalyses.AddOptions(AnalysesOptions.TurnsOffCoolant);
        }
    }
}
