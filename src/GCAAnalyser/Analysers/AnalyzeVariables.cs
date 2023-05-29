using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSendShared.Abstractions;
using GSendShared;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeVariables : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                List<IGCodeCommand> commandsWithVariables = gCodeAnalyses.Commands.Where(c => c.VariableBlocks.Count > 0).ToList();

                foreach (IGCodeCommand command in commandsWithVariables)
                {
                    foreach (IGCodeVariable varBlock in command.VariableBlocks)
                    {
                        foreach (ushort id in varBlock.VariableIds)
                        {
                            if (!gCodeAnalyses.Variables.ContainsKey(id))
                            {
                                codeAnalyses.AddError(String.Format(GSend.Language.Resources.VariableInvalid6,
                                    $"#{id}", command.LineNumber));
                            }
                            else if (command.LineNumber < gCodeAnalyses.Variables[id].LineNumber)
                            {
                                codeAnalyses.AddError(String.Format(GSend.Language.Resources.VariableInvalid7,
                                    $"#{id}", command.LineNumber, gCodeAnalyses.Variables[id].LineNumber));
                            }
                        }
                    }
                }
            }
            //List<IGCodeCommand> subPrograms = gCodeAnalyses.Commands.Where(c => c.Command.Equals('O'));

            //move error checking of variables from parser to here

            //foreach (ushort varId in gCodeVariable.VariableIds)
            //{
            //    if (!analyses.Variables.ContainsKey(varId))
            //        analyses.AddError(String.Format(GSend.Language.Resources.VariableInvalid6, varId, lineNumber));
            //}
        }
    }
}
