using GCAAnalyser.Abstractions;

namespace GCAAnalyser.Analysers
{
    internal class AnalyzeUnitOfMeasure : IGCodeAnalyzer
    {
        public int Order => int.MinValue;

        public void Analyze(IGCodeAnalyses gCodeAnalyses)
        {
            if (gCodeAnalyses == null)
                throw new ArgumentNullException(nameof(gCodeAnalyses));

            int inchesCount = gCodeAnalyses.Commands.Count(c => c.Command.Equals('G') && c.CommandValue.Equals(20));
            int mmCount = gCodeAnalyses.Commands.Count(c => c.Command.Equals('G') && c.CommandValue.Equals(21));

            if ((inchesCount > 0 && mmCount > 0) || (inchesCount == 0 && mmCount == 0))
                gCodeAnalyses.UnitOfMeasurement = UnitOfMeasurement.Error;
            else if (inchesCount > 0)
                gCodeAnalyses.UnitOfMeasurement = UnitOfMeasurement.Inch;
            else if (mmCount > 0)
                gCodeAnalyses.UnitOfMeasurement = UnitOfMeasurement.MM;
        }
    }
}
