using GSendAnalyser.Abstractions;
using GSendAnalyser.Internal;

using GSendShared;

namespace GSendAnalyser
{
    internal class GCodeAnalyses : IGCodeAnalyses
    {
        private readonly List<IGCodeCommand> _commands = new();

        internal void Add(GCodeCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            _commands.Add(command);
        }

        public void Analyse()
        {
            IGCodeAnalyzerFactory gCodeAnalyzerFactory = new GCodeAnalyzerFactory();

            IReadOnlyList<IGCodeAnalyzer> analyzers = gCodeAnalyzerFactory.Create();

            Parallel.ForEach(analyzers, gCodeAnalyzer =>
            {
                gCodeAnalyzer.Analyze(this);
            });
        }

        public IReadOnlyList<IGCodeCommand> Commands => _commands.AsReadOnly();

        public bool ContainsCarriageReturn { get; internal set; }

        public decimal HomeZ { get; set; }

        public decimal SafeZ { get; set; }

        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        public decimal TotalDistance { get; set; }

        public TimeSpan TotalTime { get; set; }

        public bool ContainsDuplicates { get; set; }

        public bool HasEndProgram { get; set; }

        public bool HasCommandsAfterEndProgram { get; set; }
    }
}
