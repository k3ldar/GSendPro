using GCAAnalyser.Abstractions;

namespace GCAAnalyser.Analysers
{
    internal class AnalyzeTime : IGCodeAnalyzer
    {
        public int Order => int.MaxValue;

        public void Analyze(IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            int mmCount = gCodeAnalyses.Commands.Count(c => c.Command.Equals('G') && c.CommandValue.Equals(21));

            Parallel.ForEach(gCodeAnalyses.Commands, c =>
            {
                c.CalculateTime(mmCount > 0);
            });

            gCodeAnalyses.TotalTime = TimeSpan.FromSeconds(gCodeAnalyses.Commands.Sum(c => c.Time.TotalSeconds));
        }
    }
}
