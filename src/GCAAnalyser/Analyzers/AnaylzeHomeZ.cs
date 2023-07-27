using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeHomeZ : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            List<IGCodeCommand> allCommands = gCodeAnalyses.AllCommands.ToList();
            gCodeAnalyses.HomeZ = allCommands.Count > 0 ? allCommands.Max(c => c.CurrentZ) : 0;

            Parallel.ForEach(gCodeAnalyses.AllCommands, c =>
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
                    gCodeCommand.Attributes &= ~CommandAttributes.SafeZ;
                }
            });
        }
    }
}
