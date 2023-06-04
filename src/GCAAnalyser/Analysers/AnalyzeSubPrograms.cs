using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    public sealed class AnalyzeSubPrograms : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> subPrograms = gCodeAnalyses.AllCommands.Where(c => c.Command.Equals('O')).ToList();

            gCodeAnalyses.SubProgramCount = subPrograms.Count();

            if (subPrograms.Count > 1 && gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                int previousLine = subPrograms[0].LineNumber;

                // only have 1 subprogram per line
                for (int i = 1; i < subPrograms.Count; i++)
                {
                    if (subPrograms[i].LineNumber == previousLine)
                        codeAnalyses.AddError(String.Format(GSend.Language.Resources.SubprogramError1, subPrograms[i].LineNumber));

                    previousLine = subPrograms[i].LineNumber;
                }
            }
        }
    }
}
