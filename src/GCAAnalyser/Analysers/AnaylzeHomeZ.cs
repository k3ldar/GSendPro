
using GCAAnalyser.Abstractions;

namespace GCAAnalyser.Analysers
{
    internal class AnalyzeHomeZ : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            gCodeAnalyses.HomeZ = gCodeAnalyses.Commands.Max(c => c.Z);

            Parallel.ForEach(gCodeAnalyses.Commands, c =>
            {
                if (c.Z == gCodeAnalyses.HomeZ &&
                    (
                        c.Attributes.HasFlag(CommandAttributes.MovementZDown) ||
                        c.Attributes.HasFlag(CommandAttributes.MovementZUp)
                    ))
                {
                    c.Attributes |= CommandAttributes.HomeZ;
                }
            });
        }
    }
}
