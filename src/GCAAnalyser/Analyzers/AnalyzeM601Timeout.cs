using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    public sealed class AnalyzeM601Pause : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> m601Commands = gCodeAnalyses.AllSpecificCommands(Constants.CharM).Where(c => c.CommandValue.Equals(Constants.MCode601Timeout)).ToList();

            if (m601Commands.Count == 0)
                return;

            if (m601Commands.Count > 0 && gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                foreach (IGCodeCommand command in m601Commands)
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
