using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static GSend.Language.Resources;

using GSendShared;
using System.Threading;
using System.IO;

namespace GSendDesktop.Controls
{
    public partial class GCodeAnalysesDetails : UserControl
    {
        public GCodeAnalysesDetails()
        {
            InitializeComponent();
            LoadResources();
            LoadAnalyser(null);
        }

        private void LoadResources()
        {
            columnHeaderProperty.Text = GSend.Language.Resources.AnalyserProperty;
            columnHeaderValue.Text = GSend.Language.Resources.AnalyserValue;
        }

        public void LoadAnalyser(string fileName, IGCodeAnalyses gCodeAnalyses)
        {
            LoadAnalyser(gCodeAnalyses);

            if (!String.IsNullOrEmpty(fileName))
            {
                lblFileName.Text = Path.GetFileName(fileName);
            }
        }

        public void LoadAnalyser(IGCodeAnalyses gCodeAnalyses)
        {
            listViewAnalyses.Items.Clear();
            int lineCount = 0;

            if (gCodeAnalyses == null)
            {
                lblFileNameDesc.Text = GSend.Language.Resources.LoadFileForDetails;
                lblFileName.Visible = false;
            }
            else
            {
                lblFileNameDesc.Text = GSend.Language.Resources.FileName;
                lblFileName.Visible = true;
                _ = gCodeAnalyses.Lines(out lineCount);
            }
            

            AddAnalyserProperty(GCodeUnitOfMeasure, gCodeAnalyses?.UnitOfMeasurement);
            AddAnalyserProperty(GCodeSafeZ, gCodeAnalyses?.SafeZ);
            AddAnalyserProperty(GCodeHomeZ, gCodeAnalyses?.HomeZ);
            AddAnalyserProperty(GCodeMaxLayerHeight, gCodeAnalyses?.MaxLayerDepth);
            AddAnalyserProperty(GCodeLayerCount, gCodeAnalyses?.Layers);
            AddAnalyserProperty(GCodeMaxXYFeed, gCodeAnalyses?.FeedX);
            AddAnalyserProperty(GCodeMaxZFeed, gCodeAnalyses?.FeedZ);
            AddAnalyserProperty(GCodeTotalDistance, gCodeAnalyses?.TotalDistance);
            AddAnalyserProperty(GCodeTotalTime, gCodeAnalyses?.TotalTime.ToString("hh\\:mm\\:ss"));
            AddAnalyserProperty(GCodeLineCount, lineCount);
            AddAnalyserProperty(GCodeCommandCount, gCodeAnalyses?.Commands.Count);
            AddAnalyserProperty(GCodeMistCoolant, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.UsesMistCoolant));
            AddAnalyserProperty(GCodeFloodCoolant, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.UsesFloodCoolant));
            AddAnalyserProperty(GCodeTurnOffCoolant, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.TurnsOffCoolant));
            AddAnalyserProperty(GCodeAutomaticToolChanges, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.ContainsAutomaticToolChanges));
            AddAnalyserProperty(GCodeToolsUsed, gCodeAnalyses?.Tools);


            AddAnalyserProperty(GCodeHasEndProgram, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.HasEndProgram));
            AddAnalyserProperty(GCodeCommandsAfterEnd, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.HasCommandAfterEnd));
            AddAnalyserProperty(GCodeContainsDuplicates, gCodeAnalyses?.AnalysesOptions.HasFlag(AnalysesOptions.ContainsDuplicates));

            AddAnalyserProperty(GCodeLineEndings, gCodeAnalyses == null ? null : gCodeAnalyses.AnalysesOptions.HasFlag(AnalysesOptions.ContainsCRLF) ? "crlf" : "lf");
            AddAnalyserProperty(GCodeFileSize, gCodeAnalyses == null || gCodeAnalyses.FileInformation == null ? null : Shared.Utilities.FileSize(gCodeAnalyses.FileInformation.Length, 2));
            AddAnalyserProperty(GCodeFileCRC, gCodeAnalyses?.FileCRC);
            AddAnalyserProperty(GCodeFileLastWrite, gCodeAnalyses?.FileInformation?.LastWriteTimeUtc.ToString(Thread.CurrentThread.CurrentUICulture.DateTimeFormat.FullDateTimePattern));
            AddAnalyserProperty(GCodeFileCreated, gCodeAnalyses?.FileInformation?.LastWriteTimeUtc.ToString(Thread.CurrentThread.CurrentUICulture.DateTimeFormat.FullDateTimePattern));
        }

        private void AddAnalyserProperty(string property, object value)
        {
            ListViewItem item = new ListViewItem(property);
            item.SubItems.Add(value == null ? "-" : value.ToString());
            listViewAnalyses.Items.Add(item);
        }
    }
}
