﻿using GSendControls;

using GSendShared;
using GSendShared.Abstractions;

using Shared.Classes;

namespace GSendEditor
{
    internal class AnalyzerThread : ThreadManager
    {
        private DateTime _lastValidateWarningsAndErrors;
        private const int ValidateWarningAndErrorsTimeout = 350;
        private const int ValidateWhenGCodeHasWarningsOrErrorsSeconds = 15;
        private readonly IGCodeParserFactory _gCodeParserFactory;
        private readonly ISubprograms _subprograms;
        private IGCodeAnalyses _gCodeAnalyses;

        public AnalyzerThread(IGCodeParserFactory gCodeParserFactory,
            ISubprograms subprograms, FastColoredTextBoxNS.FastColoredTextBox txtGCode)
            : base(txtGCode, TimeSpan.FromMilliseconds(10))
        {
            base.HangTimeout = 0;
            ContinueIfGlobalException = true;

            _gCodeParserFactory = gCodeParserFactory ?? throw new ArgumentNullException(nameof(gCodeParserFactory));
            _subprograms = subprograms ?? throw new ArgumentNullException(nameof(subprograms));
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

                if (String.IsNullOrEmpty(txtGCode.Text) && WarningContainer != null)
                {
                    List<WarningErrorList> issues = [];

                    for (int i = WarningContainer.Items.Count -1; i > -1; i--)
                    {
                        WarningErrorList item = WarningContainer.Items[i] as WarningErrorList;
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
                    IGCodeParser gCodeParser = _gCodeParserFactory.CreateParser(_subprograms);
                    _gCodeAnalyses = gCodeParser.Parse(txtGCode.Text);
                    _gCodeAnalyses.Analyse(FileName);
                    Lines = _gCodeAnalyses.Lines(out int lineCount);
                    LineCount = lineCount;

                    List<WarningErrorList> issues = [];

                    foreach (WarningErrorList item in WarningContainer.Items)
                    {
                        item.MarkedForRemoval = true;
                        item.IsNew = false;
                        issues.Add(item);
                    }

                    AnalyzeWarningAndErrors analyzeWarningAndErrors = new(_subprograms);
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

                    if (issues.Count > 0)
                        _lastValidateWarningsAndErrors = DateTime.UtcNow.AddSeconds(ValidateWhenGCodeHasWarningsOrErrorsSeconds);
                    else
                        _lastValidateWarningsAndErrors = DateTime.MaxValue;
                }
            }

            return !HasCancelled();
        }
    }
}
