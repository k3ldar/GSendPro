
using GCAAnalyser.Abstractions;

namespace GCAAnalyser.Analysers
{
    internal class AnaylzeSafeZ : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            gCodeAnalyses.SafeZ = gCodeAnalyses.Commands.Max(c => c.Z);

            Parallel.ForEach(gCodeAnalyses.Commands, c =>
            {
                if (c.Z == gCodeAnalyses.SafeZ)
                    c.Attributes |= CommandAttributes.SafeZ;
            });
        }
    }
}
