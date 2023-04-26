using GSendShared.Interfaces;

using GSendShared;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeSafeZ : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            decimal homeZ = gCodeAnalyses.Commands.Max(c => c.CurrentZ);

            gCodeAnalyses.SafeZ = gCodeAnalyses.Commands.Where(c => c.CurrentZ < homeZ).Max(c => c.CurrentZ);

            Parallel.ForEach(gCodeAnalyses.Commands, c =>
            {
                if (c.CurrentZ == gCodeAnalyses.SafeZ &&
                    (c.Attributes.HasFlag(CommandAttributes.MovementZDown) || c.Attributes.HasFlag(CommandAttributes.MovementZUp)))
                {
                    GCodeCommand gCodeCommand = c as GCodeCommand;
                    gCodeCommand.Attributes |= CommandAttributes.SafeZ;
                }
            });


            List<IGCodeCommand> layerCommands = gCodeAnalyses.Commands.Where(c =>
                c.Command.Equals('Z') &&
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
