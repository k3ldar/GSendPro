using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeCoordinateSystemsUsed : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> coordinateSystems = gCodeAnalyses.AllSpecificCommands(Constants.CharG).Where(c => IsCoordCommand(c.CommandValue)).OrderBy(c => c.CommandValue).ToList();
            gCodeAnalyses.CoordinateSystems = String.Empty;

            if (coordinateSystems.Count > 0)
            {
                gCodeAnalyses.AddOptions(AnalysesOptions.ContainsCoordinateCommands);

                coordinateSystems.ForEach(t =>
                {
                    if (!gCodeAnalyses.CoordinateSystems.Contains(t.ToString()))
                        gCodeAnalyses.CoordinateSystems += $"{t},";
                });
            }

            if (gCodeAnalyses.CoordinateSystems.EndsWith(","))
                gCodeAnalyses.CoordinateSystems = gCodeAnalyses.CoordinateSystems[..^1];
        }

        private static bool IsCoordCommand(decimal value)
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
