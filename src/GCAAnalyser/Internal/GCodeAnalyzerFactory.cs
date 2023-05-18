using GSendAnalyser.Analysers;

using GSendShared.Interfaces;

namespace GSendAnalyser.Internal
{
    internal class GCodeAnalyzerFactory : IGCodeAnalyzerFactory
    {
        public IReadOnlyList<IGCodeAnalyzer> Create()
        {
            List<IGCodeAnalyzer> result = new List<IGCodeAnalyzer>()
            {
                new AnalyzeHomeZ(),
                new AnalyzeSafeZ(),
                new AnalyzeDuplicates(),
                new AnalyzeDistance(),
                new AnalyzeUnitOfMeasure(),
                new AnalyzeTime(),
                new AnalyzeEndProgram(),
                new AnalyzeFileDetails(),
                new AnalyzeCoolantUsage(),
                new AnalyzeToolChange(),
                new AnalyzeFeedsAndSpeeds(),
                new AnalyzeComments(),
                new AnalyzeSubPrograms(),
                new AnalyzeCoordinateSystemsUsed(),
                new AnalyzeInvalidGCode(),
                new AnalyzeM650JobName(),
            }
            .OrderBy(o => o.Order)
            .ToList();

            return result;
        }
    }
}
