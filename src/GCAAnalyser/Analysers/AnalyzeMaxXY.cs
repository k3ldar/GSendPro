using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeMaxXY : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            gCodeAnalyses.MaxX = gCodeAnalyses.Commands.Where(c => c.Command.Equals('X')).Max(c => c.CurrentX);
            gCodeAnalyses.MaxY = gCodeAnalyses.Commands.Where(c => c.Command.Equals('Z')).Max(c => c.CurrentY);
        }
    }
}
