using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeM630RunProgram : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> m630Commands = gCodeAnalyses.AllSpecificCommands(Constants.CharM).Where(c => c.CommandValue.Equals(Constants.MCode630RunProgram)).ToList();

            if (m630Commands.Count == 0)
                return;

            if (m630Commands.Count > 0 && gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                if (!gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.RunProgram))
                    codeAnalyses.AddOptions(AnalysesOptions.RunProgram);

                List<int> lineNumbers = new();

                foreach (IGCodeCommand command in m630Commands)
                {
                    if (lineNumbers.Contains(command.MasterLineNumber))
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalyseError21, command.MasterLineNumber);
                        continue;
                    }

                    lineNumbers.Add(command.MasterLineNumber);

                    string exeFile = command.CommentStripped(true);

                    if (String.IsNullOrWhiteSpace(exeFile))
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalyseError22, command.MasterLineNumber);
                    }
                    else if (!ValidExtension(Path.GetExtension(exeFile).ToUpper()))
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.AnalyseError23, Path.GetExtension(exeFile));
                    }
                    else if (!File.Exists(exeFile))
                    {
                        codeAnalyses.AddWarning(GSend.Language.Resources.AnalyseError20, command.MasterLineNumber, exeFile);
                        continue;
                    }
                }
            }
        }

        private static bool ValidExtension(string extension)
        {
            switch (extension)
            {
                case ".EXE":
                case ".COM":
                case ".BAT":
                    return true;

                default:
                    return false;
            }
        }
    }
}
