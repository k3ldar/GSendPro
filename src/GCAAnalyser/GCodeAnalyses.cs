using GSendAnalyser.Internal;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Models;

using PluginManager.Abstractions;

using Shared.Classes;

namespace GSendAnalyser
{
    internal class GCodeAnalyses : IGCodeAnalyses
    {
        private readonly object _lockObject = new();
        private readonly List<IGCodeCommand> _commands = new();
        private List<IGCodeCommand> _allCommands;
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

        public IReadOnlyList<IGCodeCommand> AllCommands
        {
            get
            {
                using (TimedLock tl = TimedLock.Lock(_lockObject))
                {
                    if (_allCommands == null)
                    {
                        List<IGCodeCommand> Result = new();

                        int lineNumber = 0;
                        RecursivelyRetrieveAllCommands(Result, _commands, ref lineNumber, 0);

                        _allCommands = Result;
                    }

                    return _allCommands;
                }
            }
        }

        private void RecursivelyRetrieveAllCommands(List<IGCodeCommand> Result, IReadOnlyList<IGCodeCommand> commands, ref int lineNumber, int recursionDepth)
        {
            if (recursionDepth > Constants.MaxSubCommandRecursionDepth)
            {
                AddError(GSend.Language.Resources.SubProgramError2);
                return;
            }

            bool first = true;

            foreach (IGCodeCommand command in commands)
            {
                if (command.Command.Equals('O') && command.SubAnalyses != null && command.SubAnalyses.Commands.Count > 0)
                {
                    RecursivelyRetrieveAllCommands(Result, command.SubAnalyses.Commands, ref lineNumber, recursionDepth + 1);
                }
                else
                {
                    if (first || command.LineNumber != Result[Result.Count - 1].LineNumber)
                    {
                        lineNumber++;
                        first = false;
                    }

                    if (command is GCodeCommand gCommand)
                        gCommand.MasterLineNumber = lineNumber;

                    Result.Add(command);
                }
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
                    currentLine = new(this, command);
                    lineCount++;
                    Result.Add(currentLine);
                }

                currentLine.Commands.Add(command);
            }

            return Result;
        }

        public List<IGCodeLine> AllLines(out int lineCount)
        {
            List<IGCodeLine> Result = new();

            lineCount = 0;
            GCodeLine currentLine = null;

            foreach (IGCodeCommand command in AllCommands)
            {
                if (command.MasterLineNumber > lineCount)
                {
                    currentLine = new(this, command);
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

        internal void AddError(string message, params object[] args)
        {
            AddError(String.Format(message, args));
        }

        internal void AddWarning(string message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            _warnings.Add(message);
        }

        internal void AddWarning(string message, params object[] args)
        {
            AddWarning(String.Format(message, args));
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
