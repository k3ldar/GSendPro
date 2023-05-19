using Microsoft.Extensions.DependencyInjection;

namespace GSendShared.Abstractions
{
    public interface IGCodeAnalyzerFactory
    {
        IReadOnlyList<IGCodeAnalyzer> Create();
    }
}
