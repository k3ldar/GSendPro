using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyser.Analysers
{
    internal class AnalyzeLongComments : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses is GCodeAnalyses codeAnalyses)
            {
                List<IGCodeCommand> longComments = gCodeAnalyses.AllCommands.Where(c => c.Attributes.HasFlag(CommandAttributes.InvalidCommentTooLong)).ToList();

                foreach (IGCodeCommand longComment in longComments)
                {
                    codeAnalyses.AddWarning(String.Format(GSend.Language.Resources.WarningLongComment, longComment.LineNumber));
                }
            }
        }
    }
}
