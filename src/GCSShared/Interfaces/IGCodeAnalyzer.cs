namespace GSendShared.Abstractions
{
    public interface IGCodeAnalyzer
    {
        void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses);

        int Order { get; }
    }
}
