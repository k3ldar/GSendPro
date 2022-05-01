
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
                new AnaylzeSafeZ(),
                new AnalyzeDuplicates(),
            }
            .OrderBy(o => o.Order)
            .ToList();

            return result;
        }
    }
}
