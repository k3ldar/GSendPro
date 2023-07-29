namespace GSendShared.Abstractions
{
    public interface IGCodeAnalyzerFactory
    {
        IReadOnlyList<IGCodeAnalyzer> Create();
    }
}
