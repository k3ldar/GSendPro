using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Abstractions;
using GSendShared;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeM631_2ReturnCode : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> mCommands = gCodeAnalyses.AllSpecificCommands(Constants.CharM).Where(c => c.CommandValue.Equals(Constants.MCode631RunProgramResult)).ToList();

            if (mCommands.Count == 0)
                return;

            if (mCommands.Count > 0 && gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                foreach (IGCodeCommand command in mCommands)
                {
                    if (command.NextCommand == null)
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.M600InvalidNoPCommand, command.MasterLineNumber);
                        continue;
                    }

                    if (command.NextCommand.Command != Constants.CharP)
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalyzeError20, command.MasterLineNumber);
                    }

                    if (command.NextCommand.CommandValue < Constants.MCodeMinTimeoutValue || command.NextCommand.CommandValue > Constants.MCodeMaxTimeoutValue)
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalyzeError21, command.MasterLineNumber);
                    }
                }
            }
        }
    }
}
