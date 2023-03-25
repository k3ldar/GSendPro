using GSendShared;

namespace GSendShared.Interfaces
{
    public interface IGCodeAnalyzer
    {
        void Analyze(IGCodeAnalyses gCodeAnalyses);

        int Order { get; }
    }
}
