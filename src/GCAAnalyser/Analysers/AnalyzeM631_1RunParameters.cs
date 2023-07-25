using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeM631_1RunParameters : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> mCommands = gCodeAnalyses.AllSpecificCommands(Constants.CharM).Where(c => c.CommandValue.Equals(Constants.MCode631RunProgramParams)).ToList();

            if (mCommands.Count == 0)
                return;

            if (mCommands.Count > 0 && gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                if (!gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.RunProgram))
                    codeAnalyses.AddOptions(AnalysesOptions.RunProgram);

                List<int> lineNumbers = new();

                foreach (IGCodeCommand command in mCommands)
                {
                    if (lineNumbers.Contains(command.MasterLineNumber))
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalyseError26, command.MasterLineNumber);
                        continue;
                    }

                    lineNumbers.Add(command.MasterLineNumber);

                    string parameters = command.CommentStripped(true);

                    if (String.IsNullOrWhiteSpace(parameters))
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalyseError27, command.MasterLineNumber);
                    }
                }
            }
        }
    }
}
