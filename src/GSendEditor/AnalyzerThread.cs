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

        public AnalyzerThread(IGCodeParserFactory gCodeParserFactory, FastColoredTextBoxNS.FastColoredTextBox txtGCode)
            : base(txtGCode, TimeSpan.FromMilliseconds(10))
        {
            _gCodeParserFactory = gCodeParserFactory ?? throw new ArgumentNullException(nameof(gCodeParserFactory));
            _lastValidateWarningsAndErrors = DateTime.MaxValue;
        }

        public WarningContainer WarningContainer { get; set; }

        public Machine2DView Machine2DView { get; set; }

        public GCodeAnalysesDetails AnalysesDetails { get; set; }
        public string FileName { get; set; }

        public void AnalyzerUpdated()
        {
            _lastValidateWarningsAndErrors = DateTime.UtcNow;
        }

        protected override bool Run(object parameters)
        {
            if (parameters is FastColoredTextBoxNS.FastColoredTextBox txtGCode)
            {
                TimeSpan overrideUpdateSpan = DateTime.UtcNow - _lastValidateWarningsAndErrors;

                if (overrideUpdateSpan.TotalMilliseconds > ValidateWarningAndErrorsTimeout && WarningContainer != null && !String.IsNullOrEmpty(txtGCode.Text))
                {
                    IGCodeParser gCodeParser = _gCodeParserFactory.CreateParser();
                    IGCodeAnalyses _gCodeAnalyses = gCodeParser.Parse(txtGCode.Text);
                    _gCodeAnalyses.Analyse(FileName);

                    AnalyzeWarningAndErrors analyzeWarningAndErrors = new AnalyzeWarningAndErrors();
                    analyzeWarningAndErrors.ViewAndAnalyseWarningsAndErrors(WarningContainer, _gCodeAnalyses);

                    if (String.IsNullOrEmpty(FileName))
                        AnalysesDetails?.LoadAnalyser(_gCodeAnalyses);
                    else
                        AnalysesDetails?.LoadAnalyser(FileName, _gCodeAnalyses);

                    if (Machine2DView != null)
                    {
                        Rectangle machineSize = new Rectangle(0, 0, (int)(_gCodeAnalyses.MaxX + 50), (int)(_gCodeAnalyses.MaxY + 50));
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
