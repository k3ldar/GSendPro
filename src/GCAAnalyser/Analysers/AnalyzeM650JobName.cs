﻿using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    public sealed class AnalyzeM650JobName : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> m650Commands = gCodeAnalyses.AllSpecificCommands(Constants.CharM).Where(c => c.CommandValue.Equals(Constants.MCode650JobName)).ToList();

            if (m650Commands.Count == 0)
                return;

            if (m650Commands.Count == 1)
            {
                if (String.IsNullOrEmpty(m650Commands[0].Comment))
                {
                    gCodeAnalyses.AddOptions(AnalysesOptions.InvalidJobName);
                    return;
                }

                gCodeAnalyses.JobName = m650Commands[0].CommentStripped(false);
                return;
            }

            gCodeAnalyses.AddOptions(AnalysesOptions.MultipleJobNames);
        }
    }
}
