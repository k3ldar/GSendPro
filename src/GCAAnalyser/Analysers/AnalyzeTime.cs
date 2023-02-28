using GSendAnalyser.Abstractions;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeTime : IGCodeAnalyzer
    {
        public int Order => int.MaxValue;

        public void Analyze(IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            Parallel.ForEach(gCodeAnalyses.Commands, c =>
            {
                c.CalculateTime();
            });

            gCodeAnalyses.TotalTime = TimeSpan.FromSeconds(gCodeAnalyses.Commands.Sum(c => c.Time.TotalSeconds));
        }
    }
}
