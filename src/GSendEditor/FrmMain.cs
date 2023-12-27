using System.Text;

using FastColoredTextBoxNS;

using GSendApi;

using GSendCommon;
using GSendCommon.Abstractions;

using GSendControls;
using GSendControls.Abstractions;
using GSendControls.Plugins;
using GSendControls.Threads;

using GSendDesktop.Internal;

using GSendEditor.Internal;

using GSendShared;
using GSendShared.Abstractions;
using GSendShared.Interfaces;
using GSendShared.Models;
using GSendShared.Plugins;

using Microsoft.Extensions.DependencyInjection;

using Shared.Classes;

namespace GSendEditor
{
    public partial class FrmMain : BaseForm, IShortcutImplementation, IEditorPluginHost, IOnlineStatusUpdate
    {
        private readonly ServerValidationThread _validationThread;
        private readonly object _lockObject = new();
        private readonly object _updateSubprogramsLock = new();
        private readonly IGSendContext _gSendContext;
        private readonly IGSendApiWrapper _gsendApiWrapper;
        private readonly ServerBasedSubPrograms _serverBasedSubPrograms;
        private AnalyzerThread _analyzerThread = null;
        private ISubprogram _subProgram;
        private readonly RecentFiles _recentFiles;
        private readonly Internal.Bookmarks _bookmarks;
        private Internal.Bookmark _activeBookmark;
        private string _fileName;
        private List<IShortcut> _shortcuts;
        private readonly ShortcutHandler _shortcutHandler;
        private readonly IPluginHelper _pluginHelper;
        private bool _isSubprogram = false;
        private bool _isOnline;
        private readonly TextEditorBridge _textEditorBridge;

        public FrmMain(IGSendContext gSendContext)
        {
            _gSendContext = gSendContext ?? throw new ArgumentNullException(nameof(gSendContext));
            _gsendApiWrapper = _gSendContext.ServiceProvider.GetRequiredService<IGSendApiWrapper>();
            _gsendApiWrapper.ServerUriChanged += GsendApiWrapper_ServerUriChanged;
            _pluginHelper = _gSendContext.ServiceProvider.GetRequiredService<IPluginHelper>();
            InitializeComponent();
            _textEditorBridge = new(txtGCode);
            _serverBasedSubPrograms = new ServerBasedSubPrograms(_gsendApiWrapper);
            CreateAnalyzerThread(gSendContext.ServiceProvider.GetService<IGCodeParserFactory>(),
                _serverBasedSubPrograms);
            txtGCode.SyntaxHighlighter = new GCodeSyntaxHighLighter(txtGCode);

            machine2dView1.UnloadGCode();
            txtGCode.TextChanged += txtGCode_TextChanged;
            UpdateTitleBar();
            gCodeAnalysesDetails1.HideFileName();
            _recentFiles = new();
            _bookmarks = new();
            txtGCode.BookmarkColor = Color.BurlyWood;


            // shortcuts have to be setup prior to plugins
            _shortcutHandler = new()
            {
                RegisterKeyCombo = false
            };
            _shortcutHandler.OnKeyComboDown += ShortcutHandler_OnKeyComboDown;
            _shortcutHandler.OnKeyComboUp += ShortcutHandler_OnKeyComboUp;

            _shortcuts = RetrieveAvailableShortcuts();


            mnuFile.Tag = new InternalPluginMenu(mnuFile);
            mnuEdit.Tag = new InternalPluginMenu(mnuEdit);
            mnuView.Tag = new InternalPluginMenu(mnuView);
            mnuTools.Tag = new InternalPluginMenu(mnuTools);
            mnuHelp.Tag = new InternalPluginMenu(mnuHelp);

            _pluginHelper.InitializeAllPlugins(this);

            UpdateShortcutKeyValues(_shortcuts);
            UpdateOnlineStatus(false, GSend.Language.Resources.ServerNoConnection);
            ServerValidationThread validationThread = new(this);
            ThreadManager.ThreadStart(validationThread, "Server Validation Thread", ThreadPriority.BelowNormal, true);
            _validationThread = validationThread;
        }

        private void CreateAnalyzerThread(IGCodeParserFactory gCodeParserFactory, ISubprograms subprograms)
        {
            if (_analyzerThread != null)
            {
                _analyzerThread.OnAddItem -= AnalyzerThread_OnAddItem;
                _analyzerThread.OnRemoveItem -= AnalyzerThread_OnRemoveItem;
                _analyzerThread.CancelThread();

                while (ThreadManager.Exists(_analyzerThread.Name))
                    Thread.Sleep(20);
            }

            _analyzerThread = new AnalyzerThread(gCodeParserFactory, subprograms, txtGCode);
            _analyzerThread.OnAddItem += AnalyzerThread_OnAddItem;
            _analyzerThread.OnRemoveItem += AnalyzerThread_OnRemoveItem;

            CreateAndRunAnalyzerThread();
            _analyzerThread.AnalyzerUpdated();
        }

        public IGSendApiWrapper ApiWrapper => _gsendApiWrapper;

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
            machine2dView1.LockUpdate = true;
            try
            {
                base.LoadSettings();
                LoadSettings(splitContainerMain);
                LoadSettings(splitContainerPrimary);
                LoadSettings(lvSubprograms);
                LoadSettings(gCodeAnalysesDetails1.listViewAnalyses);
                LoadSettings(tabControlMain);
            }
            finally
            {
                machine2dView1.LockUpdate = false;
            }
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
            mnuViewSubprograms.Text = GSend.Language.Resources.AppMenuViewSubPrograms;
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
            Application.DoEvents();
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1 && File.Exists(args[1]))
            {
                mnuFileNew_Click(sender, e);
                LoadGCodeData(args[1]);
            }
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
                string savePrompt;
                string fileName = FileName;

                if (String.IsNullOrEmpty(fileName))
                    savePrompt = GSend.Language.Resources.FileChangedNew;
                else
                    savePrompt = String.Format(GSend.Language.Resources.FileChanged, fileName);

                DialogResult saveResult = MessageBox.Show(
                    savePrompt,
                    GSend.Language.Resources.SaveChanges,
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (saveResult == DialogResult.Yes)
                {
                    if (_isSubprogram)
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

                        SaveTextToFile();
                        ClearWarnings();
                    }

                    return false;

                }
                else if (saveResult == DialogResult.Cancel)
                {
                    return true;
                }
            }

            ClearWarnings();
            return false;
        }

        private void ClearWarnings()
        {
            if (InvokeRequired)
            {
                Invoke(ClearWarnings);
                return;
            }

            lstWarningsErrors.Items.Clear();
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
                        ISubprogram sub = _gsendApiWrapper.SubprogramGet(recent.FileName);

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
            ClearWarnings();
            gCodeAnalysesDetails1.ClearAnalyser();
            _isSubprogram = false;
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
                if (_isSubprogram)
                {
                    SaveAsSubProgram(_subProgram.Name, _subProgram.Description);
                }
                else
                {
                    SaveTextToFile();
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

            SaveTextToFile();
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

            _isSubprogram = true;
            _activeBookmark.FileName = name;
            _bookmarks.UpdateBookmarks(_activeBookmark);

            UpdateTitleBar();
        }

        private void SaveAsSubProgram(string name, string description)
        {
            _subProgram ??= new SubprogramModel(name, description, String.Empty);

            _subProgram.Contents = txtGCode.Text;

            IGCodeParserFactory parserFactory = _gSendContext.ServiceProvider.GetService<IGCodeParserFactory>();
            IGCodeParser gCodeParser = parserFactory.CreateParser();
            IGCodeAnalyses gCodeAnalyses = gCodeParser.Parse(_subProgram.Contents);
            gCodeAnalyses.Analyse();

            _subProgram.Variables = [];

            foreach (ushort variable in gCodeAnalyses.Variables.Keys)
            {
                _subProgram.Variables.Add(gCodeAnalyses.Variables[variable]);
            }

            _gsendApiWrapper.SubprogramUpdate(_subProgram);
            LoadSubprograms();
        }

        private void LoadSubprograms()
        {
            if (lvSubprograms.InvokeRequired)
            {
                Invoke(LoadSubprograms);
                return;
            }

            using (TimedLock tl = TimedLock.Lock(_updateSubprogramsLock))
            {
                lvSubprograms.BeginUpdate();
                try
                {
                    foreach (ListViewItem item in lvSubprograms.Items)
                        item.Tag = null;

                    List<ISubprogram> subprograms = _gsendApiWrapper.SubprogramGet();

                    foreach (ISubprogram subProgram in subprograms)
                    {
                        ListViewItem listViewItem = GetSubprogramListItem(subProgram.Name);

                        listViewItem.SubItems[1].Text = subProgram.Description;
                        listViewItem.Tag = subProgram;
                        listViewItem.ImageIndex = 5;
                    }

                    for (int i = lvSubprograms.Items.Count - 1; i >= 0; i--)
                    {
                        if (lvSubprograms.Items[i].Tag == null)
                            lvSubprograms.Items.RemoveAt(i);
                    }
                }
                finally
                {
                    lvSubprograms.EndUpdate();
                }
            }
        }

        private ListViewItem GetSubprogramListItem(string name)
        {
            for (int i = lvSubprograms.Items.Count - 1; i >= 0; i--)
            {
                if (lvSubprograms.Items[i].Text == name)
                {
                    return lvSubprograms.Items[i];
                }
            }

            ListViewItem listViewItem = new()
            {
                Text = name,
            };

            listViewItem.SubItems.Add(String.Empty);
            lvSubprograms.Items.Add(listViewItem);

            return listViewItem;
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

        private void mnuViewPreview_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabPagePreview;
        }

        private void mnuViewProperties_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabPageProperties;
        }

        private void mnuViewSubprograms_Click(object sender, EventArgs e)
        {
            tabControlMain.SelectedTab = tabPageSubPrograms;
        }


        private void mnuToolsShortcutKeys_Click(object sender, EventArgs e)
        {
            if (ShortcutEditor.ShowDialog(this, ref _shortcuts))
            {
                UpdateShortcutKeyValues(_shortcuts);

                foreach (IShortcut shortcut in _shortcuts)
                {
                    DesktopSettings.WriteValue("Editor Shortcut Keys", shortcut.Name, String.Join(';', shortcut.DefaultKeys));
                }
            }
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

            if (txtGCode.InvokeRequired)
            {
                Invoke(LoadGCodeData, fileName);
                return;
            }

            using (MouseControl mc = MouseControl.ShowWaitCursor(this))
            {
                toolStripStatusLabelLoadingFile.Visible = true;
                Application.DoEvents();
                txtGCode.Text = ReadTextFromFile(fileName);
                HasChanged = false;
                FileName = fileName;
                txtGCode.ClearUndo();
                ClearWarnings();
                UpdateTitleBar();
                UpdateEnabledState();
                _isSubprogram = false;
                _subProgram = null;
                _recentFiles.AddRecentFile(fileName, false);
                RetrieveAndLoadBookmarks(fileName);
                toolStripStatusLabelLoadingFile.Visible = false;
                GC.Collect();
            }
        }

        private void tabControlMain_Selected(object sender, TabControlEventArgs e)
        {
            txtGCode.Select();
        }

        private void lvSubprograms_DoubleClick(object sender, EventArgs e)
        {
            ISubprogram subProgram = null;

            if (sender is ListView lv)
            {
                if (lv.SelectedItems.Count == 0)
                    return;

                subProgram = lv.SelectedItems[0].Tag as ISubprogram;
            }

            if (subProgram == null)
                return;

            if (SaveIfRequired())
                return;

            ISubprogram subWithContent = _gsendApiWrapper.SubprogramGet(subProgram.Name);

            if (subWithContent == null)
            {
                MessageBox.Show(this, String.Format(GSend.Language.Resources.SubprogramNotFound, subProgram.Name), GSend.Language.Resources.SubprogramError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            subProgram.Contents = subWithContent.Contents;
            subProgram.Description = subWithContent.Description;

            LoadSubprogram(subProgram);
        }

        private void LoadSubprogram(ISubprogram subProgram)
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
            _isSubprogram = true;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "Left for now as potential future work")]
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


            if (lstWarningsErrors.Items[e.Index] is not WarningErrorList item)
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
            if (IsDisposed)
                return;

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

        public void UpdateOnlineStatus(bool isOnline, string server)
        {
            if (this.InvokeRequired)
            {
                Invoke(() => UpdateOnlineStatus(isOnline, server));
                return;
            }

            _isOnline = isOnline;
            string statusText = String.Format(GSend.Language.Resources.ServerStatus, server);

            if (_isOnline)
            {
                if (!tabControlMain.TabPages.Contains(tabPageSubPrograms))
                    tabControlMain.TabPages.Add(tabPageSubPrograms);

                if (!tmrUpdateSubprograms.Enabled)
                    tmrUpdateSubprograms.Enabled = true;
            }
            else
            {
                if (tabControlMain.TabPages.Contains(tabPageSubPrograms))
                    tabControlMain.TabPages.Remove(tabPageSubPrograms);

                if (tmrUpdateSubprograms.Enabled)
                    tmrUpdateSubprograms.Enabled = false;
            }

            if (toolStripStatusLabelConnectedToServer.Text != statusText)
                toolStripStatusLabelConnectedToServer.Text = statusText;
        }

        private void SaveTextToFile()
        {
            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                byte[] fileBytes = Encoding.UTF8.GetBytes(txtGCode.Text);
                using FileStream fs = File.OpenWrite(FileName);
                fs.Position = 0;
                fs.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        private string ReadTextFromFile(string fileName)
        {
            using (TimedLock tl = TimedLock.Lock(_lockObject))
            {
                StringBuilder result = new();
                const int BufferSize = 2048;

                using FileStream fileStream = new(fileName, FileMode.Open, FileAccess.Read);

                int remaining = (int)fileStream.Length;
                int totalRead = 0;

                while (remaining > 0)
                {
                    int toRead = remaining > BufferSize ? BufferSize : remaining;
                    byte[] bytes = new byte[toRead];
                    Span<byte> byteSpan = new(bytes);
                    int chunkRead = fileStream.Read(byteSpan);

                    if (chunkRead == 0)
                        break;

                    remaining -= chunkRead;
                    totalRead += chunkRead;

                    result.Append(Encoding.UTF8.GetString(bytes));
                }

                return result.ToString();
            }
        }

        private void tmrUpdateSubprograms_Tick(object sender, EventArgs e)
        {
            tmrUpdateSubprograms.Enabled = false;

            if (tmrUpdateSubprograms.Interval == 1000)
                tmrUpdateSubprograms.Interval = 30000;

            LoadSubprograms();

            tmrUpdateSubprograms.Enabled = true;
        }

        private void GsendApiWrapper_ServerUriChanged()
        {
            using MouseControl mc = MouseControl.ShowWaitCursor(this);
            _validationThread.ValidateConnection();
            _analyzerThread.AnalyzerUpdated();
            CreateAnalyzerThread(_gSendContext.ServiceProvider.GetService<IGCodeParserFactory>(),
                _serverBasedSubPrograms);
            GC.KeepAlive(mc);
        }

        #region Shortcuts

        private void ShortcutHandler_OnKeyComboDown(object sender, ShortcutArgs e)
        {
            IShortcut shortcut = _shortcuts.Find(s => s.Name.Equals(e.Name));

            shortcut?.Trigger(true);
        }

        private void ShortcutHandler_OnKeyComboUp(object sender, ShortcutArgs e)
        {
            IShortcut shortcut = _shortcuts.Find(s => s.Name.Equals(e.Name));

            shortcut?.Trigger(false);
        }

        private List<IShortcut> RetrieveAvailableShortcuts()
        {
            List<IShortcut> Result = [];
            RecursivelyRetrieveAllShortcutClasses(this, Result, 0);

            return Result;
        }

        private void UpdateShortcutKeyValues(List<IShortcut> Result)
        {
            foreach (IShortcut shortcut in Result)
            {
                _shortcutHandler.AddKeyCombo(shortcut.Name, shortcut.DefaultKeys);

                string keyArray = String.Join(';', shortcut.DefaultKeys);

                // is it overridden?
                string shortcutValue = DesktopSettings.ReadValue<string>("Editor Shortcut Keys", shortcut.Name, keyArray);

                if (!String.IsNullOrEmpty(shortcutValue) && keyArray != shortcutValue)
                {
                    shortcut.DefaultKeys.Clear();

                    string[] keyItems = shortcutValue.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                    foreach (string item in keyItems)
                    {
                        if (Int32.TryParse(item, out int keyValue))
                            shortcut.DefaultKeys.Add(keyValue);
                    }
                }

                shortcut.KeysUpdated?.Invoke(shortcut.DefaultKeys);
            }
        }

        private static void RecursivelyRetrieveAllShortcutClasses(Control control, List<IShortcut> shortcuts, int depth)
        {
            if (depth > 25)
            {
                return;
            }

            if (control is IShortcutImplementation shortcutImpl)
            {
                shortcuts.AddRange(shortcutImpl.GetShortcuts());
            }

            foreach (Control childControl in control.Controls)
            {
                RecursivelyRetrieveAllShortcutClasses(childControl, shortcuts, depth + 1);
            }
        }

        private static void UpdateMenuShortCut(ToolStripMenuItem menu, List<int> keys)
        {
            if (menu == null || keys == null || keys.Count == 0)
                return;

            Keys key = Keys.None;

            foreach (int intKeyValue in keys)
            {
                key |= (Keys)intKeyValue;
            }

            menu.ShortcutKeys = key;
        }

        public List<IShortcut> GetShortcuts()
        {
            string groupNameFileMenu = GSend.Language.Resources.ShortcutFileMenu;
            string groupNameEditMenu = GSend.Language.Resources.ShortcutEditMenu;
            string groupNameBookmarkMenu = GSend.Language.Resources.ShortcutBookmarkMenu;
            string groupNameViewMenu = GSend.Language.Resources.ShortcutMenuView;

            return
            [
                // File menu
                new ShortcutModel(groupNameFileMenu, GSend.Language.Resources.New,
                    [(int)Keys.Control, (int)Keys.N],
                    (bool isKeyDown) => { if (isKeyDown && mnuFileNew.Enabled) mnuFileNew_Click(mnuFileNew, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuFileNew, keys)),
                new ShortcutModel(groupNameFileMenu, GSend.Language.Resources.Open,
                    [(int)Keys.Control, (int)Keys.O],
                    (bool isKeyDown) => { if (isKeyDown && mnuFileOpen.Enabled) mnuFileOpen_Click(mnuFileOpen, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuFileOpen, keys)),
                new ShortcutModel(groupNameFileMenu, GSend.Language.Resources.Save,
                    [(int)Keys.Control, (int)Keys.S],
                    (bool isKeyDown) => { if (isKeyDown && mnuFileSave.Enabled) mnuFileSave_Click(mnuFileSave, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuFileSave, keys)),
                new ShortcutModel(groupNameFileMenu, GSend.Language.Resources.SaveAsSubprogram,
                    [(int)Keys.Control, (int)Keys.B],
                    (bool isKeyDown) => { if (isKeyDown && mnufileSaveAsSubprogram.Enabled) mnufileSaveAsSubprogram_Click(mnufileSaveAsSubprogram, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnufileSaveAsSubprogram, keys)),
                new ShortcutModel(groupNameFileMenu, GSend.Language.Resources.Exit,
                    [(int)Keys.Alt, (int)Keys.F4],
                    (bool isKeyDown) => { if (isKeyDown && mnuFileExit.Enabled) mnuFileExit_Click(mnuFileExit, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuFileExit, keys)),

                // Edit menu
                new ShortcutModel(groupNameEditMenu, GSend.Language.Resources.Undo,
                    [(int)Keys.Control, (int)Keys.Z],
                    (bool isKeyDown) => { if (isKeyDown && mnuEditUndo.Enabled) mnuEditUndo_Click(mnuEditUndo, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuEditUndo, keys)),
                new ShortcutModel(groupNameEditMenu, GSend.Language.Resources.Redo,
                    [(int)Keys.Control, (int)Keys.Y],
                    (bool isKeyDown) => { if (isKeyDown && mnuEditRedo.Enabled) mnuEditRedo_Click(mnuEditRedo, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuEditRedo, keys)),
                new ShortcutModel(groupNameEditMenu, GSend.Language.Resources.Cut,
                    [(int)Keys.Control, (int)Keys.X],
                    (bool isKeyDown) => { if (isKeyDown && mnuEditCut.Enabled) mnuEditCut_Click(mnuEditCut, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuEditCut, keys)),
                new ShortcutModel(groupNameEditMenu, GSend.Language.Resources.Copy,
                    [(int)Keys.Control, (int)Keys.C],
                    (bool isKeyDown) => { if (isKeyDown && mnuEditCopy.Enabled) mnuEditCopy_Click(mnuEditCopy, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuEditCopy, keys)),
                new ShortcutModel(groupNameEditMenu, GSend.Language.Resources.Paste,
                    [(int)Keys.Control, (int)Keys.V],
                    (bool isKeyDown) => { if (isKeyDown && mnuEditPaste.Enabled) mnuEditPaste_Click(mnuEditPaste, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuEditPaste, keys)),

                // bookmark menu
                new ShortcutModel(groupNameBookmarkMenu, GSend.Language.Resources.BookmarkToggle,
                    [(int)Keys.Alt, (int)Keys.B],
                    (bool isKeyDown) => { if (isKeyDown && mnuBookmarksToggle.Enabled) mnuBookmarksToggle_Click(mnuBookmarksToggle, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuBookmarksToggle, keys)),
                new ShortcutModel(groupNameBookmarkMenu, GSend.Language.Resources.BookmarkPrevious,
                    [(int)Keys.Alt, (int)Keys.P],
                    (bool isKeyDown) => { if (isKeyDown && mnuBookmarksPrevious.Enabled) mnuBookmarksPrevious_Click(mnuBookmarksPrevious, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuBookmarksPrevious, keys)),
                new ShortcutModel(groupNameBookmarkMenu, GSend.Language.Resources.BookmarkNext,
                    [(int)Keys.Alt, (int)Keys.N],
                    (bool isKeyDown) => { if (isKeyDown && mnuBookmarksNext.Enabled) mnuBookmarksNext_Click(mnuBookmarksNext, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuBookmarksNext, keys)),

                // view menu
                new ShortcutModel(groupNameViewMenu, GSend.Language.Resources.ShortcutTabGeneral,
                    [(int)Keys.Control, (int)Keys.W],
                    (bool isKeyDown) => { if (isKeyDown && mnuViewPreview.Enabled) mnuViewPreview_Click(mnuViewPreview, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuViewPreview, keys)),
                new ShortcutModel(groupNameViewMenu, GSend.Language.Resources.ShortcutTabOverrides,
                    [(int)Keys.Control, (int)Keys.R],
                    (bool isKeyDown) => { if (isKeyDown && mnuViewProperties.Enabled) mnuViewProperties_Click(mnuViewProperties, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuViewProperties, keys)),
                new ShortcutModel(groupNameViewMenu, GSend.Language.Resources.ShortcutTabJog,
                    [(int)Keys.Control, (int)Keys.S],
                    (bool isKeyDown) => { if (isKeyDown && mnuViewSubprograms.Enabled) mnuViewSubprograms_Click(mnuViewSubprograms, EventArgs.Empty); },
                    (List<int> keys) => UpdateMenuShortCut(mnuViewSubprograms, keys)),
            ];
        }

        #endregion Shortcuts

        #region ISenderPluginHost

        public PluginHosts Host => PluginHosts.Editor;

        public int MaximumMenuIndex => menuStripMain.Items.IndexOf(mnuHelp);

        public IPluginMenu GetMenu(MenuParent menuParent)
        {
            switch (menuParent)
            {
                case MenuParent.File:
                    return mnuFile.Tag as IPluginMenu;

                case MenuParent.Edit:
                    return mnuEdit.Tag as IPluginMenu;

                case MenuParent.View:
                    return mnuView.Tag as IPluginMenu;

                case MenuParent.Tools:
                    return mnuTools.Tag as IPluginMenu;

                case MenuParent.Help:
                    return mnuHelp.Tag as IPluginMenu;
            }

            return null;
        }

        public void AddPlugin(IGSendPluginModule pluginModule)
        {
            // nothing special to do for this host
        }

        public void AddMenu(IPluginMenu pluginMenu)
        {
            pluginMenu.UpdateHost(this as IEditorPluginHost);
            _pluginHelper.AddMenu(this, menuStripMain, pluginMenu, null);
        }

        public void AddToolbar(IPluginToolbarButton toolbarButton)
        {
            toolbarButton.UpdateHost(this as IEditorPluginHost);
            _pluginHelper.AddToolbarButton(this, toolbarMain, toolbarButton);
        }

        public void AddMessage(InformationType informationType, string message)
        {
            throw new InvalidOperationException();
        }

        #endregion ISenderPluginHost

        #region IEditorPluginHost

        public bool IsDirty => HasChanged;

        public bool IsSubprogram => _isSubprogram;

        public string FileName
        {
            get => _fileName;

            private set
            {
                if (_fileName == value)
                    return;

                _fileName = value;
                _analyzerThread.FileName = value;
            }
        }

        public ITextEditor Editor => _textEditorBridge;

        public IGSendContext GSendContext => _gSendContext;

        #endregion IEditorPluginHost
    }
}