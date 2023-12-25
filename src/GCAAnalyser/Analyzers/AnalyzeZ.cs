using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeZ : IGCodeAnalyzer
    {
        private const int MinimumLayerCount = 2;

        public int Order => Int32.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            ArgumentNullException.ThrowIfNull(gCodeAnalyses);


            decimal homeZ = gCodeAnalyses.AllCommands.Count > 0 ? gCodeAnalyses.AllCommands.Max(c => c.CurrentZ) : 0;

            List<IGCodeCommand> homeZCommands = gCodeAnalyses.AllCommands.Where(c => c.CurrentZ < homeZ).ToList();
            decimal safeZ = homeZCommands.Count > 0 ? homeZCommands.Max(c => c.CurrentZ) : 0;

            List<IGCodeCommand> layerCommands = gCodeAnalyses.AllSpecificCommands(Constants.CharZ).Where(c =>
                c.CommandValue < safeZ &&
                (
                    !c.Attributes.HasFlag(CommandAttributes.SafeZ) &&
                    !c.Attributes.HasFlag(CommandAttributes.HomeZ) &&
                    !c.Attributes.HasFlag(CommandAttributes.StartProgram)
                )
            )
            .DistinctBy(c => c.CurrentZ)
            .OrderByDescending(c => c.CurrentZ)
            .ToList();

            if (layerCommands.Count > MinimumLayerCount)
            {
                if (layerCommands[0].CommandValue > 0)
                {
                    gCodeAnalyses.ZBottom = layerCommands[0].CommandValue > layerCommands[^1].CommandValue;

                    if (gCodeAnalyses.ZBottom.HasValue && gCodeAnalyses.ZBottom.Value && layerCommands.Exists(c => c.CommandValue < 0) && gCodeAnalyses is GCodeAnalyses analysis)
                        analysis.AddWarning(GSend.Language.Resources.WarnBitBelowSpoilboard);
                }
                else
                    gCodeAnalyses.ZBottom = layerCommands[0].CommandValue < layerCommands[^1].CommandValue;
            }
        }
    }
}
