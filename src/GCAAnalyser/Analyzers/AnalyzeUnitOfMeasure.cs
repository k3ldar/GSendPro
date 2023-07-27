using GSendShared;
using GSendShared.Abstractions;

namespace GSendAnalyzer.Analyzers
{
    internal class AnalyzeUnitOfMeasure : BaseAnalyzer, IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            IReadOnlyList<IGCodeCommand> allCommands = gCodeAnalyses.AllSpecificCommands(Constants.CharG).Where(c => (c.CommandValue.Equals(20) || c.CommandValue.Equals(21))).ToList();

            int inchesCount = allCommands.Count(c => c.CommandValue.Equals(20));
            int mmCount = allCommands.Count(c => c.CommandValue.Equals(21));

            if ((inchesCount > 0 && mmCount > 0) || (inchesCount == 0 && mmCount == 0))
                gCodeAnalyses.UnitOfMeasurement = UnitOfMeasurement.Error;
            else if (inchesCount > 0)
                gCodeAnalyses.UnitOfMeasurement = UnitOfMeasurement.Inch;
            else if (mmCount > 0)
                gCodeAnalyses.UnitOfMeasurement = UnitOfMeasurement.Mm;
        }
    }
}
