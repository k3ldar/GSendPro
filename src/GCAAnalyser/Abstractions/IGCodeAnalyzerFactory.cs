namespace GSendAnalyser.Abstractions
{
    public interface IGCodeAnalyzerFactory
    {
        IReadOnlyList<IGCodeAnalyzer> Create();
    }
}
