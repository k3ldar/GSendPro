namespace GCAAnalyser.Abstractions
{
    public interface IGCodeAnalyses
    {
        IReadOnlyList<GCodeCommand> Commands { get; }

        bool ContainsCarriageReturn { get; }

        decimal SafeZ { get; set; }

        decimal HomeZ { get; set; }

        decimal TotalDistance { get; set; }

        TimeSpan TotalTime { get; set; }

        UnitOfMeasurement UnitOfMeasurement { get; set; }

        bool ContainsDuplicates { get; set; }

        void Analyse();
    }
}
