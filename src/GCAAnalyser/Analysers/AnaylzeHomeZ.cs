using GSendShared.Interfaces;

using GSendShared;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeHomeZ : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            gCodeAnalyses.HomeZ = gCodeAnalyses.Commands.Max(c => c.CurrentZ);

            Parallel.ForEach(gCodeAnalyses.Commands, c =>
            {
                if (c.CurrentZ == gCodeAnalyses.HomeZ &&
                    (
                        c.Attributes.HasFlag(CommandAttributes.MovementZDown) ||
                        c.Attributes.HasFlag(CommandAttributes.MovementZUp) ||
                        c.Attributes.HasFlag(CommandAttributes.None)
                    ))
                {
                    GCodeCommand gCodeCommand = c as GCodeCommand;
                    gCodeCommand.Attributes |= CommandAttributes.HomeZ;
                }
            });
        }
    }
}
