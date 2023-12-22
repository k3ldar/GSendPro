using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeSafeZ : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            ArgumentNullException.ThrowIfNull(gCodeAnalyses);

            decimal homeZ = gCodeAnalyses.AllCommands.Count > 0 ? gCodeAnalyses.AllCommands.Max(c => c.CurrentZ) : 0;

            List<IGCodeCommand> homeZCommands = gCodeAnalyses.AllCommands.Where(c => c.CurrentZ < homeZ).ToList();
            gCodeAnalyses.SafeZ = homeZCommands.Count > 0 ? homeZCommands.Max(c => c.CurrentZ) : 0;

            Parallel.ForEach(gCodeAnalyses.AllCommands, c =>
            {
                if (c.CurrentZ == gCodeAnalyses.SafeZ &&
                    (c.Attributes.HasFlag(CommandAttributes.MovementZDown) || c.Attributes.HasFlag(CommandAttributes.MovementZUp)))
                {
                    GCodeCommand gCodeCommand = c as GCodeCommand;
                    gCodeCommand.Attributes |= CommandAttributes.SafeZ;
                    gCodeCommand.Attributes &= ~CommandAttributes.HomeZ;
                }
            });


            List<IGCodeCommand> layerCommands = gCodeAnalyses.AllSpecificCommands(Constants.CharZ).Where(c =>
                c.CommandValue < gCodeAnalyses.SafeZ &&
                (
                    !c.Attributes.HasFlag(CommandAttributes.SafeZ) &&
                    !c.Attributes.HasFlag(CommandAttributes.HomeZ) &&
                    !c.Attributes.HasFlag(CommandAttributes.StartProgram)
                )
            )
            .DistinctBy(c => c.CurrentZ)
            .OrderByDescending(c => c.CurrentZ)
            .ToList();

            gCodeAnalyses.Layers = layerCommands.Count;

            for (int i = 1; i < layerCommands.Count; i++)
            {
                decimal height = layerCommands[i - 1].CurrentZ - layerCommands[i].CurrentZ;

                if (height > gCodeAnalyses.MaxLayerDepth)
                    gCodeAnalyses.MaxLayerDepth = height;
            }
        }
    }
}
