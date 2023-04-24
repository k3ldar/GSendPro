namespace GSendShared.Interfaces
{
    public interface IGCodeAnalyzer
    {
        void Analyze(string fileName, IGCodeAnalyses gCodeAnalyses);

        int Order { get; }
    }
}
