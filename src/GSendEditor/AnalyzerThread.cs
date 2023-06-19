using GSendApi;

using GSendControls;

using GSendShared;
using GSendShared.Abstractions;

using Shared.Classes;

namespace GSendEditor
{
    internal class AnalyzerThread : ThreadManager
    {
        private DateTime _lastValidateWarningsAndErrors;
        private const int ValidateWarningAndErrorsTimeout = 250;

        private readonly IGCodeParserFactory _gCodeParserFactory;
        private readonly IGSendApiWrapper _gSendApiWrapper;
        private IGCodeAnalyses _gCodeAnalyses;

        public AnalyzerThread(IGCodeParserFactory gCodeParserFactory,
            IGSendApiWrapper gSendApiWrapper, FastColoredTextBoxNS.FastColoredTextBox txtGCode)
            : base(txtGCode, TimeSpan.FromMilliseconds(10))
        {
#if DEBUG
            base.HangTimeout = 30000;
#else
            base.HangTimeout = 5000;
#endif

            _gCodeParserFactory = gCodeParserFactory ?? throw new ArgumentNullException(nameof(gCodeParserFactory));
            _gSendApiWrapper = gSendApiWrapper ?? throw new ArgumentNullException();
            _lastValidateWarningsAndErrors = DateTime.MaxValue;
        }

        public ListBox WarningContainer { get; set; }

        public Machine2DView Machine2DView { get; set; }

        public GCodeAnalysesDetails AnalysesDetails { get; set; }

        public string FileName { get; set; }

        public int LineCount { get; private set; }

        public List<IGCodeLine> Lines { get; private set; }

        public IGCodeAnalyses Analyses => _gCodeAnalyses;

        public void AnalyzerUpdated()
        {
            _lastValidateWarningsAndErrors = DateTime.UtcNow;
        }

        public event EventHandler OnAddItem;
        public event EventHandler OnRemoveItem;

        protected override bool Run(object parameters)
        {
            if (parameters is FastColoredTextBoxNS.FastColoredTextBox txtGCode)
            {
                TimeSpan overrideUpdateSpan = DateTime.UtcNow - _lastValidateWarningsAndErrors;

                if (String.IsNullOrEmpty(txtGCode.Text))
                {
                    List<WarningErrorList> issues = new();

                    foreach (WarningErrorList item in WarningContainer.Items)
                    {
                        item.MarkedForRemoval = true;
                        item.IsNew = false;
                        issues.Add(item);
                    }

                    foreach (WarningErrorList item in issues)
                    {
                        OnRemoveItem?.Invoke(item, EventArgs.Empty);
                    }
                }
                else if (overrideUpdateSpan.TotalMilliseconds > ValidateWarningAndErrorsTimeout && WarningContainer != null && !String.IsNullOrEmpty(txtGCode.Text))
                {
                    IGCodeParser gCodeParser = _gCodeParserFactory.CreateParser();
                    _gCodeAnalyses = gCodeParser.Parse(txtGCode.Text);
                    _gCodeAnalyses.Analyse(FileName);
                    Lines = _gCodeAnalyses.Lines(out int lineCount);
                    LineCount = lineCount;

                    List<WarningErrorList> issues = new();

                    foreach (WarningErrorList item in WarningContainer.Items)
                    {
                        item.MarkedForRemoval = true;
                        item.IsNew = false;
                        issues.Add(item);
                    }

                    AnalyzeWarningAndErrors analyzeWarningAndErrors = new(_gSendApiWrapper);
                    analyzeWarningAndErrors.ViewAndAnalyseWarningsAndErrors(null, issues, _gCodeAnalyses);

                    foreach (WarningErrorList item in issues)
                    {
                        if (item.MarkedForRemoval)
                            OnRemoveItem?.Invoke(item, EventArgs.Empty);
                        else
                            OnAddItem?.Invoke(item, EventArgs.Empty);
                    }

                    if (String.IsNullOrEmpty(FileName))
                        AnalysesDetails?.LoadAnalyser(_gCodeAnalyses);
                    else
                        AnalysesDetails?.LoadAnalyser(FileName, _gCodeAnalyses);

                    if (Machine2DView != null)
                    {
                        Rectangle machineSize = new(0, 0, (int)(_gCodeAnalyses.MaxX + 500), (int)(_gCodeAnalyses.MaxY + 500));
                        Machine2DView.MachineSize = machineSize;
                        Machine2DView.LoadGCode(_gCodeAnalyses);
                    }

                    _lastValidateWarningsAndErrors = DateTime.MaxValue;
                }
            }

            return !HasCancelled();
        }
    }
}
