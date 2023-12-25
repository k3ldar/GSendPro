using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using GSendShared;

using static GSend.Language.Resources;

namespace GSendControls
{
    public partial class GCodeAnalysesDetails : UserControl
    {
        private bool _showFileName = true;
        private readonly Dictionary<string, ListViewItem> _propertyItem = [];

        public GCodeAnalysesDetails()
        {
            InitializeComponent();
            LoadResources();
            LoadAnalyser(null);
        }

        private void LoadResources()
        {
            columnHeaderProperty.Text = AnalyserProperty;
            columnHeaderValue.Text = AnalyserValue;
        }

        public void HideFileName()
        {
            listViewAnalyses.Top = 7;
            listViewAnalyses.Height = Height - 14;
            _showFileName = false;
        }

        public void ShowFileName()
        {
            listViewAnalyses.Top = 38;
            listViewAnalyses.Height = Height - 45;
            _showFileName = true;
        }

        public void ClearAnalyser()
        {
            LoadAnalyser(null);
        }

        public void LoadAnalyser(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            LoadAnalyser(gCodeAnalyses);

            if (_showFileName && !String.IsNullOrEmpty(fileName))
            {
                lblFileName.Text = Path.GetFileName(fileName);
            }
        }

        public void LoadAnalyser(IGCodeAnalyses gCodeAnalyses)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => LoadAnalyser(gCodeAnalyses)));
                return;
            }

            listViewAnalyses.BeginUpdate();
            try
            {
                if (_showFileName)
                {
                    if (gCodeAnalyses == null)
                    {
                        lblFileNameDesc.Text = LoadFileForDetails;
                        lblFileName.Visible = false;
                    }
                    else
                    {
                        lblFileNameDesc.Text = FileName;
                        lblFileName.Visible = true;
                    }
                }

                int lineCount = 0;

                _ = gCodeAnalyses?.Lines(out lineCount);

                AddAnalyserProperty(GCodeVariableCount, gCodeAnalyses?.Variables.Count);

                if (gCodeAnalyses?.Variables.Count > 0)
                    AddAnalyserProperty(GCodeVariablesUsed, gCodeAnalyses?.VariablesUsed);

                int totalLineCount = 0;

                if (gCodeAnalyses != null && gCodeAnalyses.AllCommands.Count > 1)
                {
                    totalLineCount = gCodeAnalyses.AllCommands[gCodeAnalyses.AllCommands.Count - 1].MasterLineNumber;
                }

                AddAnalyserProperty(GCodeUnitOfMeasure, gCodeAnalyses?.UnitOfMeasurement);
                AddAnalyserProperty(GCodeSafeZ, gCodeAnalyses?.SafeZ);
                AddAnalyserProperty(GCodeHomeZ, gCodeAnalyses?.HomeZ);
                AddAnalyserProperty(GCodeZBottom, gCodeAnalyses?.ZBottom);
                AddAnalyserProperty(GCodeMaxLayerHeight, gCodeAnalyses?.MaxLayerDepth);
                AddAnalyserProperty(GCodeLayerCount, gCodeAnalyses?.Layers);
                AddAnalyserProperty(GCodeMaxXValue, gCodeAnalyses?.MaxX);
                AddAnalyserProperty(GCodeMaxYValue, gCodeAnalyses?.MaxY);
                AddAnalyserProperty(GCodeMaxXYFeed, gCodeAnalyses?.FeedX);
                AddAnalyserProperty(GCodeMaxZFeed, gCodeAnalyses?.FeedZ);
                AddAnalyserProperty(GCodeTotalDistance, gCodeAnalyses?.TotalDistance);
                AddAnalyserProperty(GCodeTotalTime, gCodeAnalyses?.TotalTime.ToString("hh\\:mm\\:ss"));
                AddAnalyserProperty(GCodeLineCount, lineCount);
                AddAnalyserProperty(GCodeCommandCount, gCodeAnalyses?.Commands.Count);
                AddAnalyserProperty(GCodeTotalLineCount, totalLineCount);
                AddAnalyserProperty(GCodeTotalCommandCount, gCodeAnalyses?.AllCommands.Count);
                AddAnalyserProperty(GCodeMistCoolant, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.UsesMistCoolant));
                AddAnalyserProperty(GCodeFloodCoolant, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.UsesFloodCoolant));
                AddAnalyserProperty(GCodeTurnOffCoolant, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.TurnsOffCoolant));
                AddAnalyserProperty(GCodeAutomaticToolChanges, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.ContainsAutomaticToolChanges));
                AddAnalyserProperty(GCodeToolsUsed, gCodeAnalyses?.Tools);
                AddAnalyserProperty(GCodeComments, gCodeAnalyses?.CommentCount > 0);
                AddAnalyserProperty(GCodeCoordinates, gCodeAnalyses?.CoordinateSystems.Length > 0);

                if (gCodeAnalyses?.CoordinateSystems.Length > 0)
                    AddAnalyserProperty(GCodeCoordinateSystems, gCodeAnalyses?.CoordinateSystems);

                AddAnalyserProperty(GCodePlaysSounds, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.PlaySound));


                AddAnalyserProperty(GCodeHasEndProgram, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.HasEndProgram));
                AddAnalyserProperty(GCodeCommandsAfterEnd, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.HasCommandAfterEnd));
                AddAnalyserProperty(GCodeContainsDuplicates, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.ContainsDuplicates));

                AddAnalyserProperty(GCodeLineEndings, gCodeAnalyses == null ? null : gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.ContainsCRLF) ? "crlf" : "lf");
                AddAnalyserProperty(GCodeFileSize, gCodeAnalyses == null || gCodeAnalyses.FileInformation == null ? null : Shared.Utilities.FileSize(gCodeAnalyses.FileInformation.Length, 2));
                AddAnalyserProperty(GCodeFileCRC, gCodeAnalyses?.FileCRC);
                AddAnalyserProperty(GCodeFileLastWrite, gCodeAnalyses?.FileInformation?.LastWriteTimeUtc.ToString(Thread.CurrentThread.CurrentUICulture.DateTimeFormat.FullDateTimePattern));
                AddAnalyserProperty(GCodeFileCreated, gCodeAnalyses?.FileInformation?.LastWriteTimeUtc.ToString(Thread.CurrentThread.CurrentUICulture.DateTimeFormat.FullDateTimePattern));
            }
            finally
            {
                listViewAnalyses.EndUpdate();
            }
        }

        private void AddAnalyserProperty(string property, object value)
        {
            if (!_propertyItem.TryGetValue(property, out ListViewItem existing))
            {
                existing = new ListViewItem(property);
                _propertyItem.Add(property, existing);
                existing.SubItems.Add(String.Empty);
                listViewAnalyses.Items.Add(existing);
            }

            existing.SubItems[1].Text = value == null ? "-" : value.ToString();
        }
    }
}
