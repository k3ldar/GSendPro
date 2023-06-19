using GSendApi;

using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeVariables : IGCodeAnalyzer
    {
        private readonly IGSendApiWrapper _apiWrapper;

        public AnalyzeVariables(IGSendApiWrapper apiWrapper)
        {
            _apiWrapper = apiWrapper ?? throw new ArgumentNullException(nameof(apiWrapper));
        }

        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                Dictionary<ushort, IGCodeCommand> subprogramVariableDeclarations = new();
                Dictionary<ushort, int> declaredVariables = new();
                List<IGCodeCommand> subprograms = gCodeAnalyses.Commands.Where(c => c.Command.Equals('O')).ToList();
                List<ushort> subprogramVariables = new();

                foreach (IGCodeCommand subProgram in subprograms)
                {
                    string subProgramName = $"O{subProgram.CommandValue}";

                    if (_apiWrapper.SubprogramExists(subProgramName))
                    {
                        ISubProgram sub = _apiWrapper.SubprogramGet(subProgramName);

                        if (sub != null)
                        {
                            foreach (IGCodeVariable variable in sub.Variables)
                            {
                                if (subprogramVariableDeclarations.ContainsKey(variable.VariableId))
                                {
                                    codeAnalyses.AddError(String.Format(GSend.Language.Resources.AnalysesVariableInvalid8,
                                        variable.VariableId,
                                        $"O{subprogramVariableDeclarations[variable.VariableId].CommandValue}",
                                        subprogramVariableDeclarations[variable.VariableId].LineNumber,
                                        sub.Name,
                                        subProgram.LineNumber));

                                    continue;
                                }

                                subprogramVariableDeclarations.Add(variable.VariableId, subProgram);
                                declaredVariables.Add(variable.VariableId, 0);
                                subprogramVariables.Add(variable.VariableId);
                            }
                        }
                    }
                }

                List<IGCodeCommand> commandsWithVariables = gCodeAnalyses.Commands.Where(c => c.VariableBlocks.Count > 0).ToList();

                foreach (IGCodeCommand command in commandsWithVariables)
                {
                    foreach (IGCodeVariableBlock varBlock in command.VariableBlocks)
                    {
                        foreach (ushort id in varBlock.VariableIds)
                        {
                            if (!declaredVariables.ContainsKey(id))
                            {
                                declaredVariables.Add(id, 1);
                            }
                            else
                            {
                                declaredVariables[id]++;
                            }

                            if (!subprogramVariables.Contains(id))
                            {
                                if (!gCodeAnalyses.Variables.ContainsKey(id))
                                {
                                    codeAnalyses.AddError(String.Format(GSend.Language.Resources.AnalysesVariableInvalid6,
                                        id, command.LineNumber));
                                }
                                else if (command.LineNumber < gCodeAnalyses.Variables[id].LineNumber)
                                {
                                    codeAnalyses.AddError(String.Format(GSend.Language.Resources.AnalysesVariableInvalid7,
                                        id, command.LineNumber, gCodeAnalyses.Variables[id].LineNumber));
                                }
                            }
                        }
                    }
                }

                List<ushort> unusedVariables = gCodeAnalyses.Variables.Keys
                    .Where(k => !declaredVariables.ContainsKey(k) || (declaredVariables.ContainsKey(k) && declaredVariables[k].Equals(0))).ToList();

                foreach (ushort id in unusedVariables)
                {
                    if (subprogramVariables.Contains(id))
                    {
                        codeAnalyses.AddWarning(String.Format(GSend.Language.Resources.AnalysesVariableWarning2,
                            id, gCodeAnalyses.Variables[id].LineNumber));
                    }
                    else
                    {
                        codeAnalyses.AddWarning(String.Format(GSend.Language.Resources.AnalysesVariableWarning1,
                            id, gCodeAnalyses.Variables[id].LineNumber));
                    }
                }
            }
        }
    }
}
