using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Interfaces;

using GSendShared;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeEndProgram : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(IGCodeAnalyses gCodeAnalyses)
        {
            IGCodeCommand command = gCodeAnalyses.Commands.FirstOrDefault(c => c.Attributes.HasFlag(CommandAttributes.EndProgram));

            gCodeAnalyses.HasEndProgram = command != null;

            if (command != null)
            {
                GCodeCommand cmd = command as GCodeCommand;
                gCodeAnalyses.HasCommandsAfterEndProgram = cmd.NextCommand != null;
            }
        }
    }
}
