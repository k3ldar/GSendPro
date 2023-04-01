namespace GSendShared
{
    public interface IGCodeAnalyses
    {
        IReadOnlyList<IGCodeCommand> Commands { get; }

        bool ContainsCarriageReturn { get; }

        decimal SafeZ { get; set; }

        decimal HomeZ { get; set; }

        decimal TotalDistance { get; set; }

        TimeSpan TotalTime { get; set; }

        UnitOfMeasurement UnitOfMeasurement { get; set; }

        bool ContainsDuplicates { get; set; }

        bool HasEndProgram { get; set; }

        bool HasCommandsAfterEndProgram { get; set; }

        void Analyse();

        List<IGCodeLine> Lines(out int lineCount);
    }
}
