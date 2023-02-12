
using GCAAnalyser.Abstractions;
using GCAAnalyser.Analysers;

namespace GCAAnalyser.Internal
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

            }
            .OrderBy(o => o.Order)
            .ToList();

            return result;
        }
    }
}
