using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeVariables : IGCodeAnalyzer
    {
        private readonly ISubPrograms _subPrograms;

        public AnalyzeVariables(ISubPrograms subPrograms)
        {
            _subPrograms = subPrograms ?? throw new ArgumentNullException(nameof(subPrograms));
        }

        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                Dictionary<ushort, IGCodeCommand> subProgramVariableDeclarations = new();
                Dictionary<ushort, int> declaredVariables = new();
                List<IGCodeCommand> subPrograms = gCodeAnalyses.Commands.Where(c => c.Command.Equals('O')).ToList();
                List<ushort> subProgramVariables = new();

                foreach (IGCodeCommand subProgram in subPrograms)
                {
                    string subProgramName = $"O{subProgram.CommandValue}";

                    if (_subPrograms.Exists(subProgramName))
                    {
                        ISubProgram sub = _subPrograms.Get(subProgramName);

                        if (sub != null)
                        {
                            foreach (IGCodeVariable variable in sub.Variables)
                            {
                                if (subProgramVariableDeclarations.ContainsKey(variable.VariableId))
                                {
                                    codeAnalyses.AddError(String.Format(GSend.Language.Resources.AnalysesVariableInvalid8,
                                        variable.VariableId,
                                        $"O{subProgramVariableDeclarations[variable.VariableId].CommandValue}",
                                        subProgramVariableDeclarations[variable.VariableId].LineNumber,
                                        sub.Name,
                                        subProgram.LineNumber));

                                    continue;
                                }

                                subProgramVariableDeclarations.Add(variable.VariableId, subProgram);
                                declaredVariables.Add(variable.VariableId, 0);
                                subProgramVariables.Add(variable.VariableId);
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

                            if (!subProgramVariables.Contains(id))
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
                    if (subProgramVariables.Contains(id))
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
