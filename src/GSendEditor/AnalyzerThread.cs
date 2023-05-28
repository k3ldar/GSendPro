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
                    _gCodeAnalyses.Analyse();

                    AnalyzeWarningAndErrors analyzeWarningAndErrors = new AnalyzeWarningAndErrors();
                    analyzeWarningAndErrors.ViewAndAnalyseWarningsAndErrors(WarningContainer, _gCodeAnalyses);

                    _lastValidateWarningsAndErrors = DateTime.MaxValue;
                }
            }

            return !HasCancelled();
        }
    }
}
