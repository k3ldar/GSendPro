namespace GSendAnalyser.Abstractions
{
    public interface IGCodeAnalyzer
    {
        void Analyze(IGCodeAnalyses gCodeAnalyses);

        int Order { get; }
    }
}
