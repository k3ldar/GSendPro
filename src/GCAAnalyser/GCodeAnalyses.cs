using GSendShared.Interfaces;
using GSendAnalyser.Internal;

using GSendShared;
using Shared.Classes;

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
            Analyse(null);
        }

        public void Analyse(string fileName)
        {
            IGCodeAnalyzerFactory gCodeAnalyzerFactory = new GCodeAnalyzerFactory();

            IReadOnlyList<IGCodeAnalyzer> analyzers = gCodeAnalyzerFactory.Create();

            Parallel.ForEach(analyzers, gCodeAnalyzer =>
            {
                gCodeAnalyzer.Analyze(fileName, this);
            });
        }

        public void AddOptions(AnalysesOptions options)
        {
            using (TimedLock tl = TimedLock.Lock(_commands))
            {
                AnalysesOptions |= options;
            }
        }

        public AnalysesOptions AnalysesOptions { get; private set; }

        public IReadOnlyList<IGCodeCommand> Commands => _commands.AsReadOnly();

        public decimal HomeZ { get; set; }

        public decimal SafeZ { get; set; }

        public UnitOfMeasurement UnitOfMeasurement { get; set; }

        public decimal TotalDistance { get; set; }

        public TimeSpan TotalTime { get; set; }

        public string Tools { get; set; } = "";

        public decimal FeedX { get; set; }

        public decimal FeedZ { get; set; }

        public int Layers { get; set; }

        public decimal MaxLayerDepth { get; set; }

        public int CommentCount { get; set; }

        public FileInfo FileInformation { get; set; }

        public string FileCRC { get; set; }

        public List<IGCodeLine> Lines(out int lineCount)
        {
            List<IGCodeLine> Result = new();

            lineCount = 0;
            GCodeLine currentLine = null;

            foreach (IGCodeCommand command in Commands)
            {
                if (command.LineNumber > lineCount)
                {
                    currentLine = new();
                    lineCount++;
                    Result.Add(currentLine);
                }

                currentLine.Commands.Add(command);
            }

            return Result;
        }
    }
}
