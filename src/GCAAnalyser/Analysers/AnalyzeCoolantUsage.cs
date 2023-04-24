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

            gCodeAnalyses.UsesMistCoolant = gCodeAnalyses.Commands.Any(c => c.ToString().Equals("M7"));
            gCodeAnalyses.UsesFloodCoolant = gCodeAnalyses.Commands.Any(c => c.ToString().Equals("M8"));
            gCodeAnalyses.TurnsOffCoolant = gCodeAnalyses.Commands.Any(c => c.ToString().Equals("M9"));
        }
    }
}
