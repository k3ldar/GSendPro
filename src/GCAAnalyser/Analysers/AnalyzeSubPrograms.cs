using GSendShared;
using GSendShared.Interfaces;

namespace GSendAnalyser.Analysers
{
    public sealed class AnalyzeSubPrograms : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            gCodeAnalyses.SubProgramCount = gCodeAnalyses.Commands.Where(c => c.Command.Equals('O')).Count();
        }
    }
}
