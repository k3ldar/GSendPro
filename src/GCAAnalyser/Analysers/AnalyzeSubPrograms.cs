using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    public sealed class AnalyzeSubPrograms : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            List<IGCodeCommand> subprograms = gCodeAnalyses.AllSpecificCommands(Constants.CharO).ToList();

            gCodeAnalyses.SubProgramCount = subprograms.Count;

            if (subprograms.Count > 1 && gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                int previousLine = subprograms[0].LineNumber;

                // only have 1 subprogram per line
                for (int i = 1; i < subprograms.Count; i++)
                {
                    if (subprograms[i].LineNumber == previousLine)
                        codeAnalyses.AddError(String.Format(GSend.Language.Resources.SubprogramError1, subprograms[i].LineNumber));

                    previousLine = subprograms[i].LineNumber;
                }
            }
        }
    }
}
