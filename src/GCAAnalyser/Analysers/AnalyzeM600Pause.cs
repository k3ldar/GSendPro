using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    public sealed class AnalyzeM600Pause : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> m605Commands = gCodeAnalyses.AllCommands.Where(c => c.Command.Equals(Constants.CharM) && c.CommandValue.Equals(Constants.MCode600)).ToList();

            if (m605Commands.Count == 0)
                return;

            if (m605Commands.Count > 0 && gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                foreach (IGCodeCommand command in m605Commands)
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
