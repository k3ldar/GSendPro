using GSendShared;
using GSendShared.Interfaces;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeCoordinateSystemsUsed : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> coordinateSystems = gCodeAnalyses.Commands.Where(c => c.Command.Equals('M') && IsCoordCommand(c.CommandValue)).OrderBy(c => c.CommandValue).ToList();
            gCodeAnalyses.CoordinateSystems = String.Empty;

            if (coordinateSystems.Count > 0)
            {
                gCodeAnalyses.AddOptions(AnalysesOptions.ContainsCoordinateCommands);

                coordinateSystems.ForEach(t =>
                {
                    if (!gCodeAnalyses.CoordinateSystems.Contains(t.ToString()))
                        gCodeAnalyses.CoordinateSystems += $"{t.ToString()},";
                });
            }

            if (gCodeAnalyses.CoordinateSystems.EndsWith(","))
                gCodeAnalyses.CoordinateSystems = gCodeAnalyses.CoordinateSystems[..^1];

        }

        private bool IsCoordCommand(decimal value)
        {
            switch (value)
            {
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                    return true;

                default:
                    return false;
            }
        }
    }
}
