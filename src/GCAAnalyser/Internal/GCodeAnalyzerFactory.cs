using GSendShared.Interfaces;
using GSendAnalyser.Analysers;
using Microsoft.Extensions.DependencyInjection;

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
            }
            .OrderBy(o => o.Order)
            .ToList();

            return result;
        }
    }
}
