using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    public sealed class AnalyzeM605PlaySounds : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> m605Commands = gCodeAnalyses.AllCommands.Where(c => c.Command.Equals(Constants.CharM) && c.CommandValue.Equals(Constants.MCode605)).ToList();

            if (m605Commands.Count == 0)
                return;

            if (m605Commands.Count == 1)
            {
                if (String.IsNullOrEmpty(m605Commands[0].Comment))
                {
                    gCodeAnalyses.AddOptions(AnalysesOptions.InvalidJobName);
                    return;
                }

                gCodeAnalyses.JobName = m605Commands[0].Comment.Replace(";", String.Empty).Replace("(", String.Empty).Replace(")", String.Empty);
                return;
            }

            gCodeAnalyses.AddOptions(AnalysesOptions.MultipleJobNames);
        }
    }
}
