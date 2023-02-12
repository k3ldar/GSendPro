
using GCAAnalyser.Abstractions;

namespace GCAAnalyser.Analysers
{
    internal class AnalyzeSafeZ : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            decimal homeZ = gCodeAnalyses.Commands.Max(c => c.Z);

            gCodeAnalyses.SafeZ = gCodeAnalyses.Commands.Where(c => c.Z < homeZ).Max(c => c.Z);

            Parallel.ForEach(gCodeAnalyses.Commands, c =>
            {
                if (c.Z == gCodeAnalyses.SafeZ &&
                    (c.Attributes.HasFlag(CommandAttributes.MovementZDown) || c.Attributes.HasFlag(CommandAttributes.MovementZUp)))
                {
                    c.Attributes |= CommandAttributes.SafeZ;
                }
            });
        }
    }
}
