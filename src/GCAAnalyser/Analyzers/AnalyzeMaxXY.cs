using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeMaxXY : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> xCommands = gCodeAnalyses.AllSpecificCommands(Constants.CharX).ToList();
            gCodeAnalyses.MaxX = xCommands.Count > 0 ? xCommands.Max(c => c.CurrentX) : 0;

            List<IGCodeCommand> yCommands = gCodeAnalyses.AllSpecificCommands(Constants.CharY).ToList();
            gCodeAnalyses.MaxY = yCommands.Count > 0 ? yCommands.Max(c => c.CurrentY) : 0;
        }
    }
}
