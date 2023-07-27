using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeM631_1RunParameters : BaseAnalyzer, IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> mCommands = gCodeAnalyses.AllSpecificCommands(Constants.CharM).Where(c => c.CommandValue.Equals(Constants.MCode631RunProgramParams)).ToList();

            if (mCommands.Count == 0)
                return;

            if (mCommands.Count > 0 && gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                List<int> lineNumbers = new();

                foreach (IGCodeCommand command in mCommands)
                {
                    if (lineNumbers.Contains(command.MasterLineNumber) ||
                        (command.PreviousCommand != null && command.PreviousCommand.MasterLineNumber == command.MasterLineNumber) ||
                        (command.NextCommand != null && command.NextCommand.MasterLineNumber == command.MasterLineNumber))
                    {
                        string error = String.Format(GSend.Language.Resources.AnalyseError26, command.MasterLineNumber);
                        if (!codeAnalyses.Errors.Contains(error))
                            codeAnalyses.AddError(error);

                        continue;
                    }

                    lineNumbers.Add(command.MasterLineNumber);

                    string parameters = command.CommentStripped(true);

                    if (String.IsNullOrWhiteSpace(parameters))
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalyseError27, command.MasterLineNumber);
                    }

                    if (!ValidateNextCommand(command, Constants.MCode631RunProgram, new decimal[] { Constants.MCode631RunProgramParams, Constants.MCode631RunProgramResult}, new char[] { Constants.CharP }, 0))
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalyseError29, command.MasterLineNumber);
                    }
                }
            }
        }
    }
}
