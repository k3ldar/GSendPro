using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeMaxXY : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> xCommands = gCodeAnalyses.Commands.Where(c => c.Command.Equals('X')).ToList();
            gCodeAnalyses.MaxX = xCommands.Count > 0 ? xCommands.Max(c => c.CurrentX) : 0;

            List<IGCodeCommand> yCommands = gCodeAnalyses.Commands.Where(c => c.Command.Equals('Y')).ToList();
            gCodeAnalyses.MaxY = yCommands.Count > 0 ? yCommands.Max(c => c.CurrentY) : 0;
        }
    }
}
