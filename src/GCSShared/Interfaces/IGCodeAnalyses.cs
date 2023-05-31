using GSendShared.Abstractions;

namespace GSendShared
{
    public interface IGCodeAnalyses
    {
        IReadOnlyList<IGCodeCommand> Commands { get; }

        decimal SafeZ { get; set; }

        decimal HomeZ { get; set; }

        decimal TotalDistance { get; set; }

        TimeSpan TotalTime { get; set; }

        UnitOfMeasurement UnitOfMeasurement { get; set; }

        string Tools { get; set; }

        decimal FeedX { get; set; }

        decimal FeedZ { get; set; }

        int Layers { get; set; }

        decimal MaxLayerDepth { get; set; }

        int CommentCount { get; set; }

        int SubProgramCount { get; set; }

        string CoordinateSystems { get; set; }

        string JobName { get; set; }

        FileInfo FileInformation { get; set; }

        string FileCRC { get; set; }

        AnalysesOptions AnalysesOptions { get; }

        IReadOnlyDictionary<ushort, IGCodeVariable> Variables { get; }

        string VariablesUsed { get; }

        IReadOnlyList<string> Errors { get; }

        IReadOnlyList<string> Warnings { get; }

        decimal MaxX { get; set; }

        decimal MaxY { get; set; }

        void Analyse();

        void Analyse(string fileName);

        void AddOptions(AnalysesOptions options);

        List<IGCodeLine> Lines(out int lineCount);
    }
}
