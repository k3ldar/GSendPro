using System.Diagnostics;

using GSendApi;

using GSendCommon;

using GSendControls;

using GSendDesktop.Internal;

using GSendEditor.Internal;

using GSendShared;
using GSendShared.Abstractions;

using Microsoft.Extensions.DependencyInjection;

using Shared.Classes;

namespace GSendEditor
{
    public partial class FrmMain : BaseForm
    {
        private readonly AnalyzerThread _analyzerThread = null;
        private readonly IGSendContext _gSendContext;
        private readonly IGSendApiWrapper _gsendApiWrapper;
        private ISubProgram _subProgram;
        private readonly RecentFiles _recentFiles;
        private readonly Internal.Bookmarks _bookmarks;
        private Internal.Bookmark _activeBookmark;
        private string _fileName;

        public FrmMain(IGSendContext gSendContext)
        {
            _gSendContext = gSendContext ?? throw new ArgumentNullException(nameof(gSendContext));
            _gsendApiWrapper = _gSendContext.ServiceProvider.GetRequiredService<IGSendApiWrapper>();
            InitializeComponent();
            _analyzerThread = new AnalyzerThread(gSendContext.ServiceProvider.GetService<IGCodeParserFactory>(),
                _gSendContext.ServiceProvider.GetRequiredService<ISubprograms>(), txtGCode);
            _analyzerThread.OnAddItem += AnalyzerThread_OnAddItem;
            _analyzerThread.OnRemoveItem += AnalyzerThread_OnRemoveItem;
            txtGCode.SyntaxHighlighter = new GCodeSyntaxHighLighter(txtGCode);

            machine2dView1.UnloadGCode();
            txtGCode.TextChanged += txtGCode_TextChanged;
            UpdateTitleBar();
            gCodeAnalysesDetails1.HideFileName();
            _recentFiles = new();
            _bookmarks = new();
            txtGCode.BookmarkColor = Color.BurlyWood;
        }

        protected override string SectionName => nameof(GSendEditor);

        protected override void SaveSettings()
        {
            base.SaveSettings();
            SaveSettings(splitContainerMain);
            SaveSettings(splitContainerPrimary);
            SaveSettings(lvSubprograms);
            SaveSettings(gCodeAnalysesDetails1.listViewAnalyses);
            SaveSettings(tabControlMain);
        }

        protected override void LoadSettings()
        {
            base.LoadSettings();
            LoadSettings(splitContainerMain);
            LoadSettings(splitContainerPrimary);
            LoadSettings(lvSubprograms);
            LoadSettings(gCodeAnalysesDetails1.listViewAnalyses);
            LoadSettings(tabControlMain);
        }

        protected override void LoadResources()
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

            // bookmark menu


            // view menu
            mnuView.Text = GSend.Language.Resources.AppMenuView;
            mnuViewPreview.Text = GSend.Language.Resources.AppMenuViewPreview;
            mnuViewProperties.Text = GSend.Language.Resources.AppMenuViewProperties;
            mnuViewSubPrograms.Text = GSend.Language.Resources.AppMenuViewSubPrograms;
            mnuViewStatusBar.Text = GSend.Language.Resources.AppMenuViewStatusBar;

            // help menu
            mnuHelp.Text = GSend.Language.Resources.AppMenuHelp;
            mnuHelpAbout.Text = GSend.Language.Resources.AppMenuHelpAbout;


            // toolbar
            toolStripBtnNew.Text = GSend.Language.Resources.New;
            toolStripBtnOpen.Text = GSend.Language.Resources.Open;
            toolStripBtnSave.Text = GSend.Language.Resources.Save;
            toolStripBtnSaveAsSubProgram.Text = GSend.Language.Resources.SaveAsSubprogram;
            toolStripBtnUndo.Text = GSend.Language.Resources.Undo;
            toolStripBtnRedo.Text = GSend.Language.Resources.Redo;
            toolStripBtnToggleBookmark.Text = GSend.Language.Resources.BookmarkToggle;
            toolStripBtnPreviousBookmark.Text = GSend.Language.Resources.BookmarkPrevious;
            toolStripBtnNextBookmark.Text = GSend.Language.Resources.BookmarkNext;
            toolStripBtnClearBookmarks.Text = GSend.Language.Resources.BookmarkClearAll;
            toolStripBtnNew.ToolTipText = GSend.Language.Resources.New;
            toolStripBtnOpen.ToolTipText = GSend.Language.Resources.Open;
            toolStripBtnSave.ToolTipText = GSend.Language.Resources.Save;
            toolStripBtnSaveAsSubProgram.ToolTipText = GSend.Language.Resources.SaveAsSubprogram;
            toolStripBtnUndo.ToolTipText = GSend.Language.Resources.Undo;
            toolStripBtnRedo.ToolTipText = GSend.Language.Resources.Redo;
            toolStripBtnToggleBookmark.ToolTipText = GSend.Language.Resources.BookmarkToggle;
            toolStripBtnPreviousBookmark.ToolTipText = GSend.Language.Resources.BookmarkPrevious;
            toolStripBtnNextBookmark.ToolTipText = GSend.Language.Resources.BookmarkNext;
            toolStripBtnClearBookmarks.ToolTipText = GSend.Language.Resources.BookmarkClearAll;
            toolStripButtonRefreshSubPrograms.ToolTipText = GSend.Language.Resources.ReloadSubprogrammes;


            // tabs
            tabPagePreview.Text = GSend.Language.Resources.Preview;
            tabPageProperties.Text = GSend.Language.Resources.Properties;
            tabPageSubPrograms.Text = GSend.Language.Resources.SubPrograms;

            columnHeaderDescription.Text = GSend.Language.Resources.Description;
            columnHeaderName.Text = GSend.Language.Resources.Subprogram;
        }

        private void CreateAndRunAnalyzerThread()
        {
            if (!ThreadManager.Exists(nameof(AnalyzerThread)))
            {
                ThreadManager.ThreadStart(_analyzerThread, nameof(AnalyzerThread), ThreadPriority.AboveNormal);
                _analyzerThread.WarningContainer = lstWarningsErrors;
                _analyzerThread.Machine2DView = machine2dView1;
                _analyzerThread.AnalysesDetails = gCodeAnalysesDetails1;
            }
        }

        private void UpdateTitleBar()
        {
            Text = $"{GSend.Language.Resources.AppName} - {GSend.Language.Resources.AppNameEditor} {FileName}";
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

        protected override void UpdateEnabledState()
        {
            mnuFileSave.Enabled = HasChanged && !String.IsNullOrEmpty(FileName);
            mnuEditCopy.Enabled = txtGCode.SelectedText.Length > 0;
            mnuEditCut.Enabled = txtGCode.SelectedText.Length > 0;
            mnuEditUndo.Enabled = txtGCode.UndoEnabled;
            mnuEditRedo.Enabled = txtGCode.RedoEnabled;

            mnuBookmarksToggle.Enabled = true;
            mnuBookmarksPrevious.Enabled = txtGCode.Bookmarks.Count > 0;
            mnuBookmarksNext.Enabled = mnuBookmarksPrevious.Enabled;
            mnuBookmarksRemoveAll.Enabled = mnuBookmarksPrevious.Enabled;


            contextMenuStripEditor.Items[0].Enabled = mnuEditUndo.Enabled;
            contextMenuStripEditor.Items[1].Enabled = mnuEditRedo.Enabled;
            contextMenuStripEditor.Items[3].Enabled = mnuEditCut.Enabled;
            contextMenuStripEditor.Items[4].Enabled = mnuEditCopy.Enabled;
            contextMenuStripEditor.Items[5].Enabled = mnuEditPaste.Enabled;

            toolStripBtnNew.Enabled = mnuFileNew.Enabled;
            toolStripBtnOpen.Enabled = mnuFileOpen.Enabled;
            toolStripBtnSave.Enabled = mnuFileSave.Enabled;
            toolStripBtnSaveAsSubProgram.Enabled = mnufileSaveAsSubprogram.Enabled;

            toolStripBtnUndo.Enabled = mnuEditUndo.Enabled;
            toolStripBtnRedo.Enabled = mnuEditRedo.Enabled;

            toolStripBtnToggleBookmark.Enabled = mnuBookmarksToggle.Enabled;
            toolStripBtnPreviousBookmark.Enabled = mnuBookmarksPrevious.Enabled;
            toolStripBtnNextBookmark.Enabled = mnuBookmarksNext.Enabled;
            toolStripBtnClearBookmarks.Enabled = mnuBookmarksRemoveAll.Enabled;
        }

        protected override void CreateContextMenu()
        {
            contextMenuStripEditor.Items.Clear();
            contextMenuStripEditor.Items.Add(GSend.Language.Resources.AppMenuEditUndo, null, mnuEditUndo_Click);
            contextMenuStripEditor.Items.Add(GSend.Language.Resources.AppMenuEditRedo, null, mnuEditRedo_Click);
            contextMenuStripEditor.Items.Add("-");
            contextMenuStripEditor.Items.Add(GSend.Language.Resources.AppMenuEditCut, null, mnuEditCut_Click);
            contextMenuStripEditor.Items.Add(GSend.Language.Resources.AppMenuEditCopy, null, mnuEditCopy_Click);
            contextMenuStripEditor.Items.Add(GSend.Language.Resources.AppMenuEditPaste, null, mnuEditPaste_Click);
        }

        private void warningsAndErrors_OnUpdate(object sender, EventArgs e)
        {
            toolStripStatusLabelWarnings.Invalidate();
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            mnuFileNew_Click(sender, e);

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1 && File.Exists(args[1]))
            {
                LoadGCodeData(args[1]);
            }

            try
            {
                LoadSubprograms();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, String.Format(GSend.Language.Resources.LoadSubProgramsError, ex.Message), Languages.LanguageStrings.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            tmrServerValidation.Enabled = true;
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
                        lstWarningsErrors.Items.Clear();
                    }

                    return false;

                }
                else if (saveResult == DialogResult.Cancel)
                {
                    return true;
                }
            }

            lstWarningsErrors.Items.Clear();
            return false;
        }

        #region Menu's

        private void mnuFile_DropDownOpening(object sender, EventArgs e)
        {
            mnuFileRecent.DropDownItems.Clear();

            List<RecentFile> recentFiles = _recentFiles.GetRecentFiles();

            mnuFileRecent.Enabled = recentFiles.Count > 0;

            // get recently opened files
            foreach (RecentFile recentFile in recentFiles)
            {
                ToolStripMenuItem recent = new(recentFile.FileName)
                {
                    Tag = recentFile
                };

                recent.Click += (sender, e) =>
                {
                    RecentFile recent = (RecentFile)(sender as ToolStripMenuItem).Tag;

                    if (recent == null)
                        return;

                    if (recent.IsSubprogram)
                    {
                        ISubProgram sub = _gsendApiWrapper.SubprogramGet(recent.FileName);

                        if (sub == null)
                            _recentFiles.RemoveRecent(recent);
                        else
                            LoadSubprogram(sub);
                    }
                    else
                    {
                        LoadGCodeData(recent.FileName);
                    }
                };

                mnuFileRecent.DropDownItems.Add(recent);
            }
        }

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
            lstWarningsErrors.Items.Clear();
            gCodeAnalysesDetails1.ClearAnalyser();
            IsSubprogram = false;
            _subProgram = null;

            RetrieveAndLoadBookmarks(FileName);
            UpdateTitleBar();
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            if (SaveIfRequired())
                return;

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                LoadGCodeData(openFileDialog1.FileName);
            }
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            if (!HasChanged)
                return;

            try
            {
                if (IsSubprogram)
                {
                    SaveAsSubProgram(_subProgram.Name, _subProgram.Description);
                }
                else
                {
                    File.WriteAllText(FileName, txtGCode.Text);
                }
            }
            catch (IOException ioException)
            {
                MessageBox.Show(ioException.Message);
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
            _activeBookmark.FileName = FileName;
            _bookmarks.UpdateBookmarks(_activeBookmark);
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
            _activeBookmark.FileName = name;
            _bookmarks.UpdateBookmarks(_activeBookmark);

            UpdateTitleBar();
        }

        private void SaveAsSubProgram(string name, string description)
        {
            if (_subProgram == null)
                _subProgram = new SubprogramModel(name, description, String.Empty);

            _subProgram.Contents = txtGCode.Text;


            IGCodeParserFactory parserFactory = _gSendContext.ServiceProvider.GetService<IGCodeParserFactory>();
            IGCodeParser gCodeParser = parserFactory.CreateParser();
            IGCodeAnalyses gCodeAnalyses = gCodeParser.Parse(_subProgram.Contents);
            gCodeAnalyses.Analyse();

            _subProgram.Variables = new();

            foreach (ushort variable in gCodeAnalyses.Variables.Keys)
            {
                _subProgram.Variables.Add(gCodeAnalyses.Variables[variable]);
            }

            //gCodeAnalyses.Variables.Values.ToList();

            _gsendApiWrapper.SubprogramUpdate(_subProgram);
            LoadSubprograms();
        }

        private void LoadSubprograms()
        {
            lvSubprograms.BeginUpdate();
            try
            {
                lvSubprograms.Items.Clear();
                List<ISubProgram> subprograms = _gsendApiWrapper.SubprogramGet();

                foreach (ISubProgram subProgram in subprograms)
                {
                    ListViewItem listViewItem = new()
                    {
                        Text = subProgram.Name
                    };
                    listViewItem.SubItems.Add(subProgram.Description);
                    listViewItem.Tag = subProgram;
                    listViewItem.ImageIndex = 5;
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

        private void mnuBookmarksToggle_Click(object sender, EventArgs e)
        {
            int line = txtGCode.Selection.FromLine;

            if (txtGCode.Bookmarks.Any(bm => bm.LineIndex == line))
            {
                txtGCode.UnbookmarkLine(line);
                _activeBookmark.Lines.Remove(line);
            }
            else
            {
                txtGCode.BookmarkLine(line);
                _activeBookmark.Lines.Add(line);
            }
            _bookmarks.UpdateBookmarks(_activeBookmark);
            UpdateEnabledState();
        }

        private void mnuBookmarksPrevious_Click(object sender, EventArgs e)
        {
            txtGCode.GotoPrevBookmark(txtGCode.Selection.FromLine);
        }

        private void mnuBookmarksNext_Click(object sender, EventArgs e)
        {
            txtGCode.GotoNextBookmark(txtGCode.Selection.FromLine);
        }

        private void mnuBookmarksRemoveAll_Click(object sender, EventArgs e)
        {
            txtGCode.Bookmarks.Clear();
            _activeBookmark.Lines.Clear();
            _bookmarks.UpdateBookmarks(_activeBookmark);
            UpdateEnabledState();
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

        private void subprogramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabPageSubPrograms;
        }

        private void mnuHelpHelp_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psi = new()
            {
                FileName = Constants.HelpWebsite,
                UseShellExecute = true
            };

            Process.Start(psi);
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            AboutBox.ShowAboutBox(GSend.Language.Resources.AppNameEditor, this.Icon);
        }

        #endregion Menu's

        #region Toolbars

        private void toolStripButtonRefreshSubPrograms_Click(object sender, EventArgs e)
        {
            LoadSubprograms();
        }

        #endregion Toolbars

        private void LoadGCodeData(string fileName)
        {
            if (!File.Exists(fileName))
                return;

            txtGCode.Text = File.ReadAllText(fileName);
            HasChanged = false;
            FileName = fileName;
            txtGCode.ClearUndo();
            lstWarningsErrors.Items.Clear();
            UpdateTitleBar();
            UpdateEnabledState();
            IsSubprogram = false;
            _subProgram = null;
            _recentFiles.AddRecentFile(fileName, false);
            RetrieveAndLoadBookmarks(fileName);
        }

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

            ISubProgram subWithContent = _gsendApiWrapper.SubprogramGet(subProgram.Name);

            if (subWithContent == null)
            {
                MessageBox.Show(this, String.Format(GSend.Language.Resources.SubprogramNotFound, subProgram.Name), GSend.Language.Resources.SubprogramError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            subProgram.Contents = subWithContent.Contents;
            subProgram.Description = subWithContent.Description;

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
            _recentFiles.AddRecentFile(subProgram.Name, true);
            UpdateTitleBar();
            RetrieveAndLoadBookmarks(subProgram.Name);
            IsSubprogram = true;
        }

        private void txtGCode_ToolTipNeeded(object sender, FastColoredTextBoxNS.ToolTipNeededEventArgs e)
        {
            string hoverWord = e.HoveredWord;

            if (String.IsNullOrEmpty(hoverWord))
            {
                FastColoredTextBoxNS.Range r = new(txtGCode, e.Place, e.Place);
                hoverWord = r.GetFragment("[A-Z\\[\\]#0-9]").Text;
            }

            if (_analyzerThread == null || _analyzerThread.Analyses == null || String.IsNullOrEmpty(hoverWord))
                return;

            //bool hasVariables = false;
            //string tip = String.Empty;

            //if (hoverWord.StartsWith('#'))
            //{
            //    //ushort var
            //    //_analyzerThread.Analyses.Variables.TryGetValue()
            //}

            //foreach (IGCodeCommand item in line.Commands)
            //{
            //    foreach (IGCodeVariableBlock varValue in item.VariableBlocks)
            //    {
            //        hasVariables = true;
            //        tip = tip.Replace(varValue.VariableBlock, varValue.Value);
            //    }
            //}

            //if (!hasVariables)
            //    return;

            //e.ToolTipText = tip;
            //e.ToolTipTitle = "Variable Value";
            //e.ToolTipText = tip;
        }

        private void lstWarningsErrors_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            WarningErrorList item = lstWarningsErrors.Items[e.Index] as WarningErrorList;

            if (item == null)
                return;

            using Brush brush = new SolidBrush(e.ForeColor);
            e.Graphics.DrawString(item.Message, lstWarningsErrors.Font, brush, e.Bounds.Left + 22, e.Bounds.Top + 2);

            int imageIndex = 0;

            switch (item.InfoType)
            {
                case InformationType.Warning: // 2
                    imageIndex = 2;
                    break;

                case InformationType.Information://3
                    imageIndex = 3;
                    break;
            }

            e.Graphics.DrawImage(toolbarImageListSmall.Images[imageIndex], e.Bounds.Left + 2, e.Bounds.Top + 1);
        }

        private void AnalyzerThread_OnRemoveItem(object sender, EventArgs e)
        {
            if (lstWarningsErrors.InvokeRequired)
            {
                Invoke(() => AnalyzerThread_OnRemoveItem(sender, e));
                return;
            }

            lstWarningsErrors.Items.Remove((WarningErrorList)sender);
        }

        private void AnalyzerThread_OnAddItem(object sender, EventArgs e)
        {
            if (lstWarningsErrors.InvokeRequired)
            {
                Invoke(() => AnalyzerThread_OnAddItem(sender, e));
                return;
            }

            WarningErrorList item = (WarningErrorList)sender;

            if (item.IsNew)
            {
                lstWarningsErrors.Items.Add((WarningErrorList)sender);
            }

            item.MarkedForRemoval = false;
            item.IsNew = false;
        }

        private void RetrieveAndLoadBookmarks(string fileName)
        {
            txtGCode.Bookmarks.Clear();
            _activeBookmark = _bookmarks.GetBookmarkForFile(fileName);

            foreach (int bookmarkLine in _activeBookmark.Lines)
            {
                txtGCode.BookmarkLine(bookmarkLine);
            }

        }

        private void tmrServerValidation_Tick(object sender, EventArgs e)
        {
            tmrServerValidation.Enabled = false;

            IGSendApiWrapper apiWrapper = _gSendContext.ServiceProvider.GetRequiredService<IGSendApiWrapper>();

            using (MouseControl mc = MouseControl.ShowWaitCursor(this))
            {
                if (!FrmServerValidation.ValidateServer(this, apiWrapper))
                    Application.Exit();
            }

            if (tmrServerValidation.Interval == 5000)
            {
                tmrServerValidation.Interval = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;
            }

            tmrServerValidation.Enabled = true;
        }
    }
}