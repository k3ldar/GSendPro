using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    public sealed class AnalyzeM605PlaySounds : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> m605Commands = gCodeAnalyses.AllSpecificCommands(Constants.CharM).Where(c => c.CommandValue.Equals(Constants.MCode605)).ToList();

            if (m605Commands.Count == 0)
                return;

            if (m605Commands.Count > 0 && gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                if (!gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.PlaySound))
                    codeAnalyses.AddOptions(AnalysesOptions.PlaySound);

                List<int> lineNumbers = [];

                foreach (IGCodeCommand command in m605Commands)
                {
                    if (lineNumbers.Contains(command.MasterLineNumber))
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.M605InvalidMultipleLines, command.MasterLineNumber);
                        continue;
                    }

                    lineNumbers.Add(command.MasterLineNumber);

                    string soundFile = command.CommentStripped(true);

                    if (String.IsNullOrWhiteSpace(soundFile))
                    {
                        codeAnalyses.AddError(GSend.Language.Resources.M605InvalidSoundFileEmpty, command.MasterLineNumber);
                    }
                    else if (!File.Exists(soundFile))
                    {
                        codeAnalyses.AddWarning(GSend.Language.Resources.M605InvalidSoundFile, soundFile, command.MasterLineNumber);
                        continue;
                    }
                }
            }
        }
    }
}
