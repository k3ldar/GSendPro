using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Models;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeVariableBlocks : IGCodeAnalyzer
    {
        public int Order => Int32.MaxValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> commandsWithVariables = gCodeAnalyses.Commands.Where(c => c.VariableBlocks.Count > 0).ToList();

            foreach (IGCodeCommand command in commandsWithVariables)
            {
                foreach (IGCodeVariableBlock gCodeVariableBlock in command.VariableBlocks)
                {
                    if (gCodeVariableBlock is GCodeVariableBlockModel variableBlock)
                    {
                        string value = gCodeVariableBlock.VariableBlock;

                        foreach (ushort id in gCodeVariableBlock.VariableIds)
                        {
                            if (gCodeAnalyses.Variables.ContainsKey(id))
                            {
                                string variableValue = gCodeAnalyses.Variables[id].Value.ToString();
                                value = value.Replace($"#{id}", variableValue.Trim());
                            }
                            else
                            {

                            }
                        }

                        variableBlock.Value = value[1..^1];
                    }
                }
            }
        }
    }
}
