using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeM631_2ReturnCode : BaseAnalyzer, IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> mCommands = gCodeAnalyses.AllSpecificCommands(Constants.CharM).Where(c => c.CommandValue.Equals(Constants.MCode631RunProgramResult)).ToList();

            if (mCommands.Count == 0)
                return;

            if (gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                foreach (IGCodeCommand command in mCommands)
                {
                    if (!ValidatePreviousNextCommand(command, Constants.CharP))
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalyzeError30, command.MasterLineNumber);
                        continue;
                    }

                    if (HasCommandsOnSameLine(command, Constants.CharP))
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalyzeError31, command.MasterLineNumber);
                    }

                    if (!ValidateNextCommand(command, Constants.MCode631RunProgram, new decimal[] { Constants.MCode631RunProgramParams, Constants.MCode631RunProgramResult }, new char[] { Constants.CharP }, 0))
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalyzeError32, command.MasterLineNumber);
                    }
                }
            }
        }
    }
}
