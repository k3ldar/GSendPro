using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Abstractions;
using GSendShared;

namespace GSendAnalyser.Analysers
{
    public class AnalyzeToolChange : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            if (gCodeAnalyses.Commands.Any(c => c.Command.Equals('M') && c.CommandValue.Equals(6)))
                gCodeAnalyses.AddOptions(AnalysesOptions.ContainsAutomaticToolChanges);

            List<IGCodeCommand> tools = gCodeAnalyses.Commands.Where(c => c.Command.Equals('T')).ToList();

            if (tools.Count > 0)
            {
                gCodeAnalyses.AddOptions(AnalysesOptions.ContainsToolChanges);

                tools.ForEach(t => gCodeAnalyses.Tools += $"{t.CommandValueString},");
            }

            if (gCodeAnalyses.Tools.EndsWith(","))
                gCodeAnalyses.Tools = gCodeAnalyses.Tools[..^1];
        }
    }
}
