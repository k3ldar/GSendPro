using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeTime : IGCodeAnalyzer
    {
        public int Order => int.MaxValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            Parallel.ForEach(gCodeAnalyses.AllCommands, c =>
            {
                GCodeCommand gCodeCommand = c as GCodeCommand;
                gCodeCommand.CalculateTime();
            });

            gCodeAnalyses.TotalTime = TimeSpan.FromSeconds(gCodeAnalyses.AllCommands.Sum(c => c.Time.TotalSeconds));
        }
    }
}
