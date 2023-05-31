using System.Drawing.Drawing2D;

using GSendCommon;

using GSendControls;

using GSendShared;
using GSendShared.Abstractions;

using Microsoft.Extensions.DependencyInjection;

using Shared.Classes;

namespace GSendEditor
{
    public partial class FrmMain : Form
    {
        private readonly AnalyzerThread _analyzerThread = null;
        private readonly IGSendContext _gSendContext;
        private readonly ISubPrograms _subPrograms;
        private ISubProgram _subProgram;

        private string _fileName;

        public FrmMain(IGSendContext gSendContext)
        {
            _gSendContext = gSendContext ?? throw new ArgumentNullException(nameof(gSendContext));
            _subPrograms = _gSendContext.ServiceProvider.GetRequiredService<ISubPrograms>();
            InitializeComponent();
            _analyzerThread = new AnalyzerThread(gSendContext.ServiceProvider.GetService<IGCodeParserFactory>(), txtGCode);
            txtGCode.SyntaxHighlighter = new GCodeSyntaxHighLighter(txtGCode);
            warningsAndErrors.Visible = warningsAndErrors.TotalCount() > 0;
            warningAndErrors_VisibleChanged(this, EventArgs.Empty);
            warningsAndErrors_OnUpdate(this, EventArgs.Empty);
            UpdateEnabledState();
            LoadResources();

            machine2dView1.UnloadGCode();
            txtGCode.TextChanged += txtGCode_TextChanged;
            UpdateTitleBar();
            gCodeAnalysesDetails1.HideFileName();
            LoadSubprograms();
        }

        private void CreateAndRunAnalyzerThread()
        {
            if (!ThreadManager.Exists(nameof(AnalyzerThread)))
            {
                ThreadManager.ThreadStart(_analyzerThread, nameof(AnalyzerThread), ThreadPriority.AboveNormal);
                _analyzerThread.WarningContainer = warningsAndErrors;
                _analyzerThread.Machine2DView = machine2dView1;
                _analyzerThread.AnalysesDetails = gCodeAnalysesDetails1;
            }
        }

        private void UpdateTitleBar()
        {
            Text = $"{GSend.Language.Resources.AppName} - {GSend.Language.Resources.AppNameEditor} {FileName}";
        }

        private void LoadResources()
        {
            saveFileDialog1.Title = GSend.Language.Resources.TitleSaveAs;
            openFileDialog1.Title = GSend.Language.Resources.TitleOpen;

            // file menu
            mnuFile.Text = GSend.Language.Resources.AppMenuFile;
            mnuFileExit.Text = GSend.Language.Resources.AppMenuFileClose;
            mnuFileNew.Text = GSend.Language.Resources.AppMenuFileNew;
            mnuFileOpen.Text = GSend.Language.Resources.AppMenuOpen;
            mnuFileSave.Text = GSend.Language.Resources.AppMenuFileSave;
            mnuFileSaveAs.Text = GSend.Language.Resources.AppMenuFileSaveAs;

            // edit menu
            mnuEdit.Text = GSend.Language.Resources.AppMenuEdit;
            mnuEditUndo.Text = GSend.Language.Resources.AppMenuEditUndo;
            mnuEditRedo.Text = GSend.Language.Resources.AppMenuEditRedo;
            mnuEditCopy.Text = GSend.Language.Resources.AppMenuEditCopy;
            mnuEditCut.Text = GSend.Language.Resources.AppMenuEditCut;
            mnuEditPaste.Text = GSend.Language.Resources.AppMenuEditPaste;

            // view menu
            mnuView.Text = GSend.Language.Resources.AppMenuView;
            mnuViewPreview.Text = GSend.Language.Resources.AppMenuViewPreview;
            mnuViewProperties.Text = GSend.Language.Resources.AppMenuViewProperties;
            mnuViewSubPrograms.Text = GSend.Language.Resources.AppMenuViewSubPrograms;
            mnuViewStatusBar.Text = GSend.Language.Resources.AppMenuViewStatusBar;

            // help menu
            mnuHelp.Text = GSend.Language.Resources.AppMenuHelp;
            mnuHelpAbout.Text = GSend.Language.Resources.AppMenuHelpAbout;

            // tabs
            tabPagePreview.Text = GSend.Language.Resources.Preview;
            tabPageProperties.Text = GSend.Language.Resources.Properties;
            tabPageSubPrograms.Text = GSend.Language.Resources.SubPrograms;

            columnHeaderDescription.Text = GSend.Language.Resources.Description;
            columnHeaderName.Text = GSend.Language.Resources.Subprogram;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SaveIfRequired())
                e.Cancel = true;
        }

        private bool HasChanged { get; set; }

        private string FileName
        {
            get => _fileName;

            set
            {
                if (_fileName == value)
                    return;

                _fileName = value;
                _analyzerThread.FileName = value;
            }
        }

        private bool IsSubprogram { get; set; } = false;

        private void UpdateEnabledState()
        {
            mnuFileSave.Enabled = HasChanged && !String.IsNullOrEmpty(FileName);
            mnuEditCopy.Enabled = txtGCode.SelectedText.Length > 0;
            mnuEditCut.Enabled = txtGCode.SelectedText.Length > 0;
            mnuEditUndo.Enabled = txtGCode.UndoEnabled;
            mnuEditRedo.Enabled = txtGCode.RedoEnabled;
        }

        private void TxtGCode_PaintLine(object sender, FastColoredTextBoxNS.PaintLineEventArgs e)
        {
            if (e.LineIndex == 5)
                using (Brush brush = new LinearGradientBrush(new Rectangle(0, e.LineRect.Top, 15, 15), Color.LightPink, Color.Red, 45))
                {
                    e.Graphics.FillEllipse(brush, 0, e.LineRect.Top, 15, 15);

                    using Brush backGroundBrush = new SolidBrush(Color.Red);
                    e.Graphics.FillRectangle(backGroundBrush, e.LineRect);
                }
        }

        private void warningAndErrors_OnUpdate(object sender, EventArgs e)
        {
            int width = 0;

            if (warningsAndErrors.WarningCount() > 0)
                width += GSendShared.Constants.WarningStatusWidth;

            if (warningsAndErrors.ErrorCount() > 0)
                width += GSendShared.Constants.WarningStatusWidth;

            if (warningsAndErrors.InformationCount() > 0)
                width += GSendShared.Constants.WarningStatusWidth;

            toolStripStatusLabelWarnings.Visible = warningsAndErrors.TotalCount() > 0;
            toolStripStatusLabelWarnings.Width = width;

            warningAndErrors_VisibleChanged(sender, e);
        }

        private void warningAndErrors_VisibleChanged(object sender, EventArgs e)
        {
            if (warningsAndErrors.Visible)
            {
                splitContainerMain.Top = warningsAndErrors.Top + warningsAndErrors.Height + 8;
            }
            else
            {
                splitContainerMain.Top = 37;
            }

            splitContainerMain.Height = statusStrip1.Top - splitContainerMain.Top - 8;
        }

        private void warningsAndErrors_OnUpdate(object sender, EventArgs e)
        {
            toolStripStatusLabelWarnings.Invalidate();
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            warningsAndErrors.ResetLayoutWarningErrorSize();
        }

        private void txtGCode_SelectionChangedDelayed(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        private void txtGCode_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            HasChanged = true;
            UpdateEnabledState();
            RefreshAnalyzerThread();
        }

        private void RefreshAnalyzerThread()
        {
            CreateAndRunAnalyzerThread();
            _analyzerThread?.AnalyzerUpdated();
        }

        private bool SaveIfRequired()
        {
            if (HasChanged)
            {
                DialogResult saveResult = MessageBox.Show("Has changed, save?", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (saveResult == DialogResult.Yes)
                {
                    if (IsSubprogram)
                    {
                        SaveAsSubProgram(_subProgram.Name, _subProgram.Description);
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(FileName))
                        {
                            saveResult = saveFileDialog1.ShowDialog(this);

                            if (saveResult == DialogResult.Cancel)
                                return true;


                            FileName = saveFileDialog1.FileName;
                        }

                        File.WriteAllText(FileName, txtGCode.Text);
                        warningsAndErrors.Clear(true);
                    }

                    return false;

                }
                else if (saveResult == DialogResult.Cancel)
                {
                    return true;
                }
            }

            warningsAndErrors.Clear(true);
            return false;
        }

        #region Menu's

        private void mnuFileNew_Click(object sender, EventArgs e)
        {
            if (SaveIfRequired())
                return;

            machine2dView1.UnloadGCode();
            FileName = String.Empty;
            txtGCode.Text = String.Empty;
            HasChanged = false;
            UpdateEnabledState();
            txtGCode.ClearUndo();
            warningsAndErrors.Clear(true);
            gCodeAnalysesDetails1.ClearAnalyser();
            IsSubprogram = false;
            _subProgram = null;
            UpdateTitleBar();
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            if (SaveIfRequired())
                return;

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                FileName = openFileDialog1.FileName;
                txtGCode.Text = File.ReadAllText(FileName);
                HasChanged = false;
                txtGCode.ClearUndo();
                warningsAndErrors.Clear(true);
            }

            UpdateTitleBar();
            UpdateEnabledState();
            IsSubprogram = false;
            _subProgram = null;
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            if (!HasChanged)
                return;

            if (IsSubprogram)
            {
                SaveAsSubProgram(_subProgram.Name, _subProgram.Description);
            }
            else
            {
                File.WriteAllText(FileName, txtGCode.Text);
            }

            HasChanged = false;
            UpdateEnabledState();
            UpdateTitleBar();
        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult saveResult = saveFileDialog1.ShowDialog(this);

            if (saveResult == DialogResult.Cancel)
                return;

            FileName = saveFileDialog1.FileName;

            File.WriteAllText(FileName, txtGCode.Text);
            HasChanged = false;
            UpdateTitleBar();
        }

        private void mnufileSaveAsSubprogram_Click(object sender, EventArgs e)
        {
            SubProgramForm subProgramForm = _gSendContext.ServiceProvider.GetRequiredService<SubProgramForm>();
            DialogResult saveResult = subProgramForm.ShowDialog(this);

            if (saveResult == DialogResult.Cancel)
                return;

            _subProgram = null;
            string name = subProgramForm.SubprogramName;
            string description = subProgramForm.Description;

            SaveAsSubProgram(name, description);

            FileName = $"{name} - {description}";
            HasChanged = false;

            IsSubprogram = true;

            UpdateTitleBar();
        }

        private void SaveAsSubProgram(string name, string description)
        {
            if (_subProgram == null)
                _subProgram = new SubProgramModel(name, description, String.Empty);

            _subProgram.Contents = txtGCode.Text;
            _subPrograms.Update(_subProgram);
            LoadSubprograms();
        }

        private void LoadSubprograms()
        {
            lvSubprograms.BeginUpdate();
            try
            {
                lvSubprograms.Items.Clear();
                List<ISubProgram> subPrograms = _subPrograms.GetAll();

                foreach (ISubProgram subProgram in subPrograms)
                {
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.Text = subProgram.Name;
                    listViewItem.SubItems.Add(subProgram.Description);
                    listViewItem.Tag = subProgram;
                    lvSubprograms.Items.Add(listViewItem);
                }
            }
            finally
            {
                lvSubprograms.EndUpdate();
            }
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            if (SaveIfRequired())
                return;

            Close();
        }

        private void mnuEditUndo_Click(object sender, EventArgs e)
        {
            if (txtGCode.UndoEnabled)
                txtGCode.Undo();
        }

        private void mnuEditRedo_Click(object sender, EventArgs e)
        {
            if (txtGCode.RedoEnabled)
                txtGCode.Redo();
        }

        private void mnuEditCut_Click(object sender, EventArgs e)
        {
            txtGCode.Cut();
        }

        private void mnuEditCopy_Click(object sender, EventArgs e)
        {
            txtGCode.Copy();
        }

        private void mnuEditPaste_Click(object sender, EventArgs e)
        {
            txtGCode.Paste();
        }

        private void mnuViewStatusBar_Click(object sender, EventArgs e)
        {

        }

        private void mnuViewPreview_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabPagePreview;
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabPageProperties;
        }

        private void subProgramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabPageSubPrograms;
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {

        }

        #endregion Menu's

        private void tabControlMain_Selected(object sender, TabControlEventArgs e)
        {
            txtGCode.Select();
        }

        private void lvSubprograms_DoubleClick(object sender, EventArgs e)
        {
            ISubProgram subProgram = null;

            if (sender is ListView lv)
            {
                if (lv.SelectedItems.Count == 0)
                    return;

                subProgram = lv.SelectedItems[0].Tag as ISubProgram;
            }

            if (subProgram == null)
                return;

            if (SaveIfRequired())
                return;


            LoadSubprogram(subProgram);
        }

        private void LoadSubprogram(ISubProgram subProgram)
        {
            if (subProgram == null)
                return;

            _subProgram = subProgram;

            FileName = $"{subProgram.Name} - {subProgram.Description}";
            txtGCode.Text = subProgram.Contents;

            HasChanged = false;

            UpdateTitleBar();

            IsSubprogram = true;
        }
    }
}