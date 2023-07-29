using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    public sealed class AnalyzeM602JobName : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> m602Commands = gCodeAnalyses.AllSpecificCommands(Constants.CharM).Where(c => c.CommandValue.Equals(Constants.MCode602JobName)).ToList();

            if (m602Commands.Count == 0)
                return;

            if (m602Commands.Count == 1)
            {
                if (String.IsNullOrEmpty(m602Commands[0].Comment))
                {
                    gCodeAnalyses.AddOptions(AnalysesOptions.InvalidJobName);
                    return;
                }

                gCodeAnalyses.JobName = m602Commands[0].CommentStripped(false);
                return;
            }

            gCodeAnalyses.AddOptions(AnalysesOptions.MultipleJobNames);
        }
    }
}
