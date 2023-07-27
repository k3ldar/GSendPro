using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analyzers
{
    public sealed class AnalyzeM600Pause : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> m600Commands = gCodeAnalyses.AllSpecificCommands(Constants.CharM).Where(c => c.CommandValue.Equals(Constants.MCode600Pause)).ToList();

            if (m600Commands.Count == 0)
                return;

            if (m600Commands.Count > 0 && gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                foreach (IGCodeCommand command in m600Commands)
                {
                    if (command.NextCommand == null)
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.M600InvalidNoPCommand, command.MasterLineNumber);
                        continue;
                    }

                    if (command.NextCommand.Command != Constants.CharP)
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.M600InvalidNoPCommand, command.MasterLineNumber);
                    }

                    if (command.NextCommand.CommandValue < 1 || command.NextCommand.CommandValue > 2000)
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.M600InvalidPCommandValue, command.MasterLineNumber);
                    }
                }
            }
        }
    }
}
