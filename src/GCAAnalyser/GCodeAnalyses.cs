using GSendShared.Abstractions;
using GSendAnalyser.Internal;

using GSendShared;
using Shared.Classes;
using PluginManager.Abstractions;
using GSendShared.Models;

namespace GSendAnalyser
{
    internal class GCodeAnalyses : IGCodeAnalyses
    {
        private readonly List<IGCodeCommand> _commands = new();
        private readonly IPluginClassesService _pluginClassesService;
        private readonly Dictionary<ushort, IGCodeVariable> _variables = new();
        private readonly List<string> _errors = new();
        private readonly List<string> _warnings = new();

        public GCodeAnalyses(IPluginClassesService pluginClassesService)
        {
            _pluginClassesService = pluginClassesService ?? throw new ArgumentNullException(nameof(pluginClassesService));
        }

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
            IGCodeAnalyzerFactory gCodeAnalyzerFactory = new GCodeAnalyzerFactory(_pluginClassesService);

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

        IReadOnlyList<IGCodeCommand> AllCommands
        { 
            get
            {

            }
        }

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

        public decimal MaxX { get; set; }

        public decimal MaxY { get; set; }

        public int CommentCount { get; set; }

        public int SubProgramCount { get; set; }

        public string CoordinateSystems { get; set; }

        public string JobName { get; set; }

        public FileInfo FileInformation { get; set; }

        public string FileCRC { get; set; }

        public IReadOnlyDictionary<ushort, IGCodeVariable> Variables => _variables;

        public string VariablesUsed { get; private set; } = String.Empty;

        public IReadOnlyList<string> Errors => _errors;

        public IReadOnlyList<string> Warnings => _warnings;

        public List<IGCodeLine> Lines(out int lineCount)
        {
            List<IGCodeLine> Result = new();

            lineCount = 0;
            GCodeLine currentLine = null;

            foreach (IGCodeCommand command in Commands)
            {
                if (command.LineNumber > lineCount)
                {
                    currentLine = new(this);
                    lineCount++;
                    Result.Add(currentLine);
                }

                currentLine.Commands.Add(command);
            }

            return Result;
        }

        internal void AddError(string message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            _errors.Add(message);
        }

        internal void AddWarning(string message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            _warnings.Add(message);
        }

        internal bool AddVariable(GCodeVariableModel variableModel)
        {
            if (_variables.ContainsKey(variableModel.VariableId))
                return false;

            _variables.Add(variableModel.VariableId, variableModel);

            if (VariablesUsed.Length > 0)
                VariablesUsed += $", #{variableModel.VariableId}";
            else
                VariablesUsed = $"#{variableModel.VariableId}";

            return true;
        }
    }
}
