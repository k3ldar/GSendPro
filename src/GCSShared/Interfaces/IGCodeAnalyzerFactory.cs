using Microsoft.Extensions.DependencyInjection;

namespace GSendShared.Interfaces
{
    public interface IGCodeAnalyzerFactory
    {
        IReadOnlyList<IGCodeAnalyzer> Create();
    }
}
