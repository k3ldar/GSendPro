namespace GSendEditor
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelWarnings = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnufileSaveAsSubprogram = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBookmarks = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBookmarksToggle = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBookmarksPrevious = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBookmarksNext = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBookmarksRemoveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSubPrograms = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainerPrimary = new System.Windows.Forms.SplitContainer();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.txtGCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.contextMenuStripEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPagePreview = new System.Windows.Forms.TabPage();
            this.machine2dView1 = new GSendControls.Machine2DView();
            this.tabPageProperties = new System.Windows.Forms.TabPage();
            this.gCodeAnalysesDetails1 = new GSendControls.GCodeAnalysesDetails();
            this.tabPageSubPrograms = new System.Windows.Forms.TabPage();
            this.lvSubprograms = new GSendControls.ListViewEx();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDescription = new System.Windows.Forms.ColumnHeader();
            this.toolbarImageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.lstWarningsErrors = new System.Windows.Forms.ListBox();
            this.toolbarMain = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnSaveAsSubProgram = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnUndo = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnToggleBookmark = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnPreviousBookmark = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnNextBookmark = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnClearBookmarks = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrimary)).BeginInit();
            this.splitContainerPrimary.Panel1.SuspendLayout();
            this.splitContainerPrimary.Panel2.SuspendLayout();
            this.splitContainerPrimary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGCode)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.tabPagePreview.SuspendLayout();
            this.tabPageProperties.SuspendLayout();
            this.tabPageSubPrograms.SuspendLayout();
            this.toolbarMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelWarnings});
            this.statusStrip1.Location = new System.Drawing.Point(0, 511);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1083, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelWarnings
            // 
            this.toolStripStatusLabelWarnings.AutoSize = false;
            this.toolStripStatusLabelWarnings.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelWarnings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabelWarnings.Image")));
            this.toolStripStatusLabelWarnings.Name = "toolStripStatusLabelWarnings";
            this.toolStripStatusLabelWarnings.Size = new System.Drawing.Size(29, 17);
            this.toolStripStatusLabelWarnings.Text = "0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuBookmarks,
            this.mnuView,
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1083, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.toolStripMenuItem3,
            this.mnufileSaveAsSubprogram,
            this.toolStripMenuItem1,
            this.mnuFileRecent,
            this.toolStripMenuItem4,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            this.mnuFile.DropDownOpening += new System.EventHandler(this.mnuFile_DropDownOpening);
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(183, 22);
            this.mnuFileNew.Text = "&New";
            this.mnuFileNew.Click += new System.EventHandler(this.mnuFileNew_Click);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(183, 22);
            this.mnuFileOpen.Text = "&Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(183, 22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(183, 22);
            this.mnuFileSaveAs.Text = "Save &As";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileSaveAs_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(180, 6);
            // 
            // mnufileSaveAsSubprogram
            // 
            this.mnufileSaveAsSubprogram.Name = "mnufileSaveAsSubprogram";
            this.mnufileSaveAsSubprogram.Size = new System.Drawing.Size(183, 22);
            this.mnufileSaveAsSubprogram.Text = "Save As Sub&program";
            this.mnufileSaveAsSubprogram.Click += new System.EventHandler(this.mnufileSaveAsSubprogram_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 6);
            // 
            // mnuFileRecent
            // 
            this.mnuFileRecent.Name = "mnuFileRecent";
            this.mnuFileRecent.Size = new System.Drawing.Size(183, 22);
            this.mnuFileRecent.Text = "Recent";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(180, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(183, 22);
            this.mnuFileExit.Text = "&Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditUndo,
            this.mnuEditRedo,
            this.toolStripMenuItem2,
            this.mnuEditCut,
            this.mnuEditCopy,
            this.mnuEditPaste});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditUndo
            // 
            this.mnuEditUndo.Name = "mnuEditUndo";
            this.mnuEditUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.mnuEditUndo.Size = new System.Drawing.Size(144, 22);
            this.mnuEditUndo.Text = "Undo";
            this.mnuEditUndo.Click += new System.EventHandler(this.mnuEditUndo_Click);
            // 
            // mnuEditRedo
            // 
            this.mnuEditRedo.Name = "mnuEditRedo";
            this.mnuEditRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.mnuEditRedo.Size = new System.Drawing.Size(144, 22);
            this.mnuEditRedo.Text = "Redo";
            this.mnuEditRedo.Click += new System.EventHandler(this.mnuEditRedo_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(141, 6);
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnuEditCut.Size = new System.Drawing.Size(144, 22);
            this.mnuEditCut.Text = "Cu&t";
            this.mnuEditCut.Click += new System.EventHandler(this.mnuEditCut_Click);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuEditCopy.Size = new System.Drawing.Size(144, 22);
            this.mnuEditCopy.Text = "&Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.mnuEditCopy_Click);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mnuEditPaste.Size = new System.Drawing.Size(144, 22);
            this.mnuEditPaste.Text = "&Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.mnuEditPaste_Click);
            // 
            // mnuBookmarks
            // 
            this.mnuBookmarks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBookmarksToggle,
            this.mnuBookmarksPrevious,
            this.mnuBookmarksNext,
            this.mnuBookmarksRemoveAll});
            this.mnuBookmarks.Name = "mnuBookmarks";
            this.mnuBookmarks.Size = new System.Drawing.Size(78, 20);
            this.mnuBookmarks.Text = "Bookmarks";
            // 
            // mnuBookmarksToggle
            // 
            this.mnuBookmarksToggle.Name = "mnuBookmarksToggle";
            this.mnuBookmarksToggle.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.B)));
            this.mnuBookmarksToggle.Size = new System.Drawing.Size(213, 22);
            this.mnuBookmarksToggle.Text = "Toggle Bookmarks";
            this.mnuBookmarksToggle.Click += new System.EventHandler(this.mnuBookmarksToggle_Click);
            // 
            // mnuBookmarksPrevious
            // 
            this.mnuBookmarksPrevious.Name = "mnuBookmarksPrevious";
            this.mnuBookmarksPrevious.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.mnuBookmarksPrevious.Size = new System.Drawing.Size(213, 22);
            this.mnuBookmarksPrevious.Text = "Previous Bookmark";
            this.mnuBookmarksPrevious.Click += new System.EventHandler(this.mnuBookmarksPrevious_Click);
            // 
            // mnuBookmarksNext
            // 
            this.mnuBookmarksNext.Name = "mnuBookmarksNext";
            this.mnuBookmarksNext.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.N)));
            this.mnuBookmarksNext.Size = new System.Drawing.Size(213, 22);
            this.mnuBookmarksNext.Text = "Next Bookmark";
            this.mnuBookmarksNext.Click += new System.EventHandler(this.mnuBookmarksNext_Click);
            // 
            // mnuBookmarksRemoveAll
            // 
            this.mnuBookmarksRemoveAll.Name = "mnuBookmarksRemoveAll";
            this.mnuBookmarksRemoveAll.Size = new System.Drawing.Size(213, 22);
            this.mnuBookmarksRemoveAll.Text = "Remove All Bookmarks";
            this.mnuBookmarksRemoveAll.Click += new System.EventHandler(this.mnuBookmarksRemoveAll_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewStatusBar,
            this.mnuViewPreview,
            this.mnuViewProperties,
            this.mnuViewSubPrograms});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 20);
            this.mnuView.Text = "&View";
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(184, 22);
            this.mnuViewStatusBar.Text = "&Status Bar";
            this.mnuViewStatusBar.Visible = false;
            this.mnuViewStatusBar.Click += new System.EventHandler(this.mnuViewStatusBar_Click);
            // 
            // mnuViewPreview
            // 
            this.mnuViewPreview.Name = "mnuViewPreview";
            this.mnuViewPreview.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.mnuViewPreview.Size = new System.Drawing.Size(184, 22);
            this.mnuViewPreview.Text = "&Preview";
            this.mnuViewPreview.Click += new System.EventHandler(this.mnuViewPreview_Click);
            // 
            // mnuViewProperties
            // 
            this.mnuViewProperties.Name = "mnuViewProperties";
            this.mnuViewProperties.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.mnuViewProperties.Size = new System.Drawing.Size(184, 22);
            this.mnuViewProperties.Text = "Properties";
            this.mnuViewProperties.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // mnuViewSubPrograms
            // 
            this.mnuViewSubPrograms.Name = "mnuViewSubPrograms";
            this.mnuViewSubPrograms.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.mnuViewSubPrograms.Size = new System.Drawing.Size(184, 22);
            this.mnuViewSubPrograms.Text = "Sub Programs";
            this.mnuViewSubPrograms.Click += new System.EventHandler(this.subProgramsToolStripMenuItem_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpHelp,
            this.toolStripMenuItem5,
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpHelp
            // 
            this.mnuHelpHelp.Name = "mnuHelpHelp";
            this.mnuHelpHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mnuHelpHelp.Size = new System.Drawing.Size(180, 22);
            this.mnuHelpHelp.Text = "Help";
            this.mnuHelpHelp.Click += new System.EventHandler(this.mnuHelpHelp_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(180, 22);
            this.mnuHelpAbout.Text = "&About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "gcode";
            this.saveFileDialog1.Filter = "G Code Files|*.gcode;*.nc;*.ncc;*.ngc;*.tap;*.txt|All Files|*.*";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "G Code Files|*.gcode;*.nc;*.ncc;*.ngc;*.tap;*.txt|All Files|*.*";
            // 
            // splitContainerPrimary
            // 
            this.splitContainerPrimary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerPrimary.Location = new System.Drawing.Point(12, 56);
            this.splitContainerPrimary.Name = "splitContainerPrimary";
            this.splitContainerPrimary.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerPrimary.Panel1
            // 
            this.splitContainerPrimary.Panel1.Controls.Add(this.splitContainerMain);
            // 
            // splitContainerPrimary.Panel2
            // 
            this.splitContainerPrimary.Panel2.Controls.Add(this.lstWarningsErrors);
            this.splitContainerPrimary.Size = new System.Drawing.Size(1059, 446);
            this.splitContainerPrimary.SplitterDistance = 321;
            this.splitContainerPrimary.TabIndex = 5;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMain.Location = new System.Drawing.Point(3, 3);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.txtGCode);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabControlMain);
            this.splitContainerMain.Size = new System.Drawing.Size(1053, 315);
            this.splitContainerMain.SplitterDistance = 631;
            this.splitContainerMain.TabIndex = 5;
            // 
            // txtGCode
            // 
            this.txtGCode.AllowMacroRecording = false;
            this.txtGCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGCode.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtGCode.AutoIndent = false;
            this.txtGCode.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:]*" +
    "(?<range>:)\\s*(?<range>[^;]+);";
            this.txtGCode.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtGCode.BackBrush = null;
            this.txtGCode.CharHeight = 14;
            this.txtGCode.CharWidth = 8;
            this.txtGCode.ContextMenuStrip = this.contextMenuStripEditor;
            this.txtGCode.DefaultMarkerSize = 8;
            this.txtGCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtGCode.IsReplaceMode = false;
            this.txtGCode.LeftBracket = '[';
            this.txtGCode.LeftBracket2 = '(';
            this.txtGCode.Location = new System.Drawing.Point(3, 3);
            this.txtGCode.Name = "txtGCode";
            this.txtGCode.Paddings = new System.Windows.Forms.Padding(0);
            this.txtGCode.PreferredLineWidth = 256;
            this.txtGCode.RightBracket = ']';
            this.txtGCode.RightBracket2 = ')';
            this.txtGCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtGCode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtGCode.ServiceColors")));
            this.txtGCode.Size = new System.Drawing.Size(625, 313);
            this.txtGCode.TabIndex = 0;
            this.txtGCode.Zoom = 100;
            this.txtGCode.ToolTipNeeded += new System.EventHandler<FastColoredTextBoxNS.ToolTipNeededEventArgs>(this.txtGCode_ToolTipNeeded);
            this.txtGCode.SelectionChangedDelayed += new System.EventHandler(this.txtGCode_SelectionChangedDelayed);
            // 
            // contextMenuStripEditor
            // 
            this.contextMenuStripEditor.Name = "contextMenuStripEditor";
            this.contextMenuStripEditor.Size = new System.Drawing.Size(61, 4);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPagePreview);
            this.tabControlMain.Controls.Add(this.tabPageProperties);
            this.tabControlMain.Controls.Add(this.tabPageSubPrograms);
            this.tabControlMain.Location = new System.Drawing.Point(3, 3);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(412, 309);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPagePreview
            // 
            this.tabPagePreview.Controls.Add(this.machine2dView1);
            this.tabPagePreview.Location = new System.Drawing.Point(4, 24);
            this.tabPagePreview.Name = "tabPagePreview";
            this.tabPagePreview.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePreview.Size = new System.Drawing.Size(404, 281);
            this.tabPagePreview.TabIndex = 0;
            this.tabPagePreview.Text = "tabPagePreview";
            this.tabPagePreview.UseVisualStyleBackColor = true;
            // 
            // machine2dView1
            // 
            this.machine2dView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.machine2dView1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("machine2dView1.BackgroundImage")));
            this.machine2dView1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.machine2dView1.Configuration = GSendShared.AxisConfiguration.None;
            this.machine2dView1.Location = new System.Drawing.Point(9, 9);
            this.machine2dView1.MachineSize = new System.Drawing.Rectangle(0, 0, 5000, 5000);
            this.machine2dView1.Name = "machine2dView1";
            this.machine2dView1.Size = new System.Drawing.Size(389, 266);
            this.machine2dView1.TabIndex = 0;
            this.machine2dView1.XPosition = 0F;
            this.machine2dView1.YPosition = 0F;
            this.machine2dView1.ZoomPanel = null;
            // 
            // tabPageProperties
            // 
            this.tabPageProperties.Controls.Add(this.gCodeAnalysesDetails1);
            this.tabPageProperties.Location = new System.Drawing.Point(4, 24);
            this.tabPageProperties.Name = "tabPageProperties";
            this.tabPageProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProperties.Size = new System.Drawing.Size(404, 281);
            this.tabPageProperties.TabIndex = 1;
            this.tabPageProperties.Text = "Properties";
            this.tabPageProperties.UseVisualStyleBackColor = true;
            // 
            // gCodeAnalysesDetails1
            // 
            this.gCodeAnalysesDetails1.Location = new System.Drawing.Point(6, 6);
            this.gCodeAnalysesDetails1.Name = "gCodeAnalysesDetails1";
            this.gCodeAnalysesDetails1.Size = new System.Drawing.Size(392, 269);
            this.gCodeAnalysesDetails1.TabIndex = 0;
            // 
            // tabPageSubPrograms
            // 
            this.tabPageSubPrograms.Controls.Add(this.lvSubprograms);
            this.tabPageSubPrograms.Location = new System.Drawing.Point(4, 24);
            this.tabPageSubPrograms.Name = "tabPageSubPrograms";
            this.tabPageSubPrograms.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSubPrograms.Size = new System.Drawing.Size(404, 281);
            this.tabPageSubPrograms.TabIndex = 2;
            this.tabPageSubPrograms.Text = "Sub Programs";
            this.tabPageSubPrograms.UseVisualStyleBackColor = true;
            // 
            // lvSubprograms
            // 
            this.lvSubprograms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvSubprograms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderDescription});
            this.lvSubprograms.Location = new System.Drawing.Point(9, 9);
            this.lvSubprograms.Name = "lvSubprograms";
            this.lvSubprograms.Size = new System.Drawing.Size(389, 266);
            this.lvSubprograms.SmallImageList = this.toolbarImageListSmall;
            this.lvSubprograms.TabIndex = 0;
            this.lvSubprograms.UseCompatibleStateImageBehavior = false;
            this.lvSubprograms.View = System.Windows.Forms.View.Details;
            this.lvSubprograms.DoubleClick += new System.EventHandler(this.lvSubprograms_DoubleClick);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Width = 90;
            // 
            // columnHeaderDescription
            // 
            this.columnHeaderDescription.Width = 200;
            // 
            // toolbarImageListSmall
            // 
            this.toolbarImageListSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.toolbarImageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolbarImageListSmall.ImageStream")));
            this.toolbarImageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.toolbarImageListSmall.Images.SetKeyName(0, "Error_red_16x16.png");
            this.toolbarImageListSmall.Images.SetKeyName(1, "Error_red_16x16.png");
            this.toolbarImageListSmall.Images.SetKeyName(2, "Warning_yellow_7231_16x16.png");
            this.toolbarImageListSmall.Images.SetKeyName(3, "Information_blue_6227_16x16.png");
            this.toolbarImageListSmall.Images.SetKeyName(4, "Error_red_16x16.png");
            this.toolbarImageListSmall.Images.SetKeyName(5, "manifest_16xLG.png");
            // 
            // lstWarningsErrors
            // 
            this.lstWarningsErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstWarningsErrors.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstWarningsErrors.FormattingEnabled = true;
            this.lstWarningsErrors.ItemHeight = 20;
            this.lstWarningsErrors.Location = new System.Drawing.Point(3, 0);
            this.lstWarningsErrors.Name = "lstWarningsErrors";
            this.lstWarningsErrors.Size = new System.Drawing.Size(1053, 104);
            this.lstWarningsErrors.TabIndex = 0;
            this.lstWarningsErrors.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstWarningsErrors_DrawItem);
            // 
            // toolbarMain
            // 
            this.toolbarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnNew,
            this.toolStripBtnOpen,
            this.toolStripBtnSave,
            this.toolStripSeparator1,
            this.toolStripBtnSaveAsSubProgram,
            this.toolStripSeparator2,
            this.toolStripBtnUndo,
            this.toolStripBtnRedo,
            this.toolStripSeparator3,
            this.toolStripBtnToggleBookmark,
            this.toolStripBtnPreviousBookmark,
            this.toolStripBtnNextBookmark,
            this.toolStripBtnClearBookmarks});
            this.toolbarMain.Location = new System.Drawing.Point(0, 24);
            this.toolbarMain.Name = "toolbarMain";
            this.toolbarMain.Size = new System.Drawing.Size(1083, 25);
            this.toolbarMain.TabIndex = 6;
            this.toolbarMain.Text = "toolbarMain";
            // 
            // toolStripBtnNew
            // 
            this.toolStripBtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnNew.Image")));
            this.toolStripBtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnNew.Name = "toolStripBtnNew";
            this.toolStripBtnNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnNew.Text = "toolStripBtnNew";
            this.toolStripBtnNew.Click += new System.EventHandler(this.mnuFileNew_Click);
            // 
            // toolStripBtnOpen
            // 
            this.toolStripBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnOpen.Image")));
            this.toolStripBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnOpen.Name = "toolStripBtnOpen";
            this.toolStripBtnOpen.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnOpen.Text = "toolStripBtnOpen";
            this.toolStripBtnOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // toolStripBtnSave
            // 
            this.toolStripBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnSave.Image")));
            this.toolStripBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnSave.Name = "toolStripBtnSave";
            this.toolStripBtnSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnSave.Text = "toolStripBtn";
            this.toolStripBtnSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripBtnSaveAsSubProgram
            // 
            this.toolStripBtnSaveAsSubProgram.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnSaveAsSubProgram.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnSaveAsSubProgram.Image")));
            this.toolStripBtnSaveAsSubProgram.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnSaveAsSubProgram.Name = "toolStripBtnSaveAsSubProgram";
            this.toolStripBtnSaveAsSubProgram.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnSaveAsSubProgram.Text = "toolStripButton5";
            this.toolStripBtnSaveAsSubProgram.Click += new System.EventHandler(this.mnufileSaveAsSubprogram_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripBtnUndo
            // 
            this.toolStripBtnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnUndo.Image")));
            this.toolStripBtnUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnUndo.Name = "toolStripBtnUndo";
            this.toolStripBtnUndo.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnUndo.Text = "toolStripButton2";
            this.toolStripBtnUndo.Click += new System.EventHandler(this.mnuEditUndo_Click);
            // 
            // toolStripBtnRedo
            // 
            this.toolStripBtnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnRedo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnRedo.Image")));
            this.toolStripBtnRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnRedo.Name = "toolStripBtnRedo";
            this.toolStripBtnRedo.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnRedo.Text = "toolStripButton1";
            this.toolStripBtnRedo.Click += new System.EventHandler(this.mnuEditRedo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripBtnToggleBookmark
            // 
            this.toolStripBtnToggleBookmark.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnToggleBookmark.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnToggleBookmark.Image")));
            this.toolStripBtnToggleBookmark.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnToggleBookmark.Name = "toolStripBtnToggleBookmark";
            this.toolStripBtnToggleBookmark.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnToggleBookmark.Text = "toolStripButton6";
            this.toolStripBtnToggleBookmark.Click += new System.EventHandler(this.mnuBookmarksToggle_Click);
            // 
            // toolStripBtnPreviousBookmark
            // 
            this.toolStripBtnPreviousBookmark.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnPreviousBookmark.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnPreviousBookmark.Image")));
            this.toolStripBtnPreviousBookmark.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnPreviousBookmark.Name = "toolStripBtnPreviousBookmark";
            this.toolStripBtnPreviousBookmark.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnPreviousBookmark.Text = "toolStripButton7";
            this.toolStripBtnPreviousBookmark.Click += new System.EventHandler(this.mnuBookmarksPrevious_Click);
            // 
            // toolStripBtnNextBookmark
            // 
            this.toolStripBtnNextBookmark.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnNextBookmark.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnNextBookmark.Image")));
            this.toolStripBtnNextBookmark.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnNextBookmark.Name = "toolStripBtnNextBookmark";
            this.toolStripBtnNextBookmark.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnNextBookmark.Text = "toolStripButton8";
            this.toolStripBtnNextBookmark.Click += new System.EventHandler(this.mnuBookmarksNext_Click);
            // 
            // toolStripBtnClearBookmarks
            // 
            this.toolStripBtnClearBookmarks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnClearBookmarks.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnClearBookmarks.Image")));
            this.toolStripBtnClearBookmarks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnClearBookmarks.Name = "toolStripBtnClearBookmarks";
            this.toolStripBtnClearBookmarks.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnClearBookmarks.Text = "toolStripButton9";
            this.toolStripBtnClearBookmarks.Click += new System.EventHandler(this.mnuBookmarksRemoveAll_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 533);
            this.Controls.Add(this.toolbarMain);
            this.Controls.Add(this.splitContainerPrimary);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainerPrimary.Panel1.ResumeLayout(false);
            this.splitContainerPrimary.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrimary)).EndInit();
            this.splitContainerPrimary.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtGCode)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.tabPagePreview.ResumeLayout(false);
            this.tabPageProperties.ResumeLayout(false);
            this.tabPageSubPrograms.ResumeLayout(false);
            this.toolbarMain.ResumeLayout(false);
            this.toolbarMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private StatusStrip statusStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem mnuFile;
        private SaveFileDialog saveFileDialog1;
        private FontDialog fontDialog1;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripMenuItem mnuFileOpen;
        private ToolStripMenuItem mnuFileSave;
        private ToolStripMenuItem mnuFileSaveAs;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem mnuFileExit;
        private ToolStripMenuItem mnuEdit;
        private ToolStripMenuItem mnuEditCut;
        private ToolStripMenuItem mnuEditCopy;
        private ToolStripMenuItem mnuEditPaste;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuViewPreview;
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripStatusLabel toolStripStatusLabelWarnings;
        private OpenFileDialog openFileDialog1;
        private ToolStripMenuItem mnuEditUndo;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem mnuEditRedo;
        private ToolStripMenuItem mnuViewProperties;
        private ToolStripMenuItem mnuViewSubPrograms;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem mnufileSaveAsSubprogram;
        private SplitContainer splitContainerPrimary;
        private SplitContainer splitContainerMain;
        private FastColoredTextBoxNS.FastColoredTextBox txtGCode;
        private TabControl tabControlMain;
        private TabPage tabPagePreview;
        private GSendControls.Machine2DView machine2dView1;
        private TabPage tabPageProperties;
        private TabPage tabPageSubPrograms;
        private ListView lvSubprograms;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderDescription;
        private ListBox lstWarningsErrors;
        private ImageList toolbarImageListSmall;
        private ContextMenuStrip contextMenuStripEditor;
        private ToolStrip toolbarMain;
        private GSendControls.GCodeAnalysesDetails gCodeAnalysesDetails1;
        private ToolStripMenuItem mnuFileRecent;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripButton toolStripBtnNew;
        private ToolStripButton toolStripBtnOpen;
        private ToolStripButton toolStripBtnSave;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripBtnSaveAsSubProgram;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripBtnToggleBookmark;
        private ToolStripButton toolStripBtnPreviousBookmark;
        private ToolStripButton toolStripBtnNextBookmark;
        private ToolStripButton toolStripBtnClearBookmarks;
        private ToolStripMenuItem mnuBookmarks;
        private ToolStripMenuItem mnuBookmarksToggle;
        private ToolStripMenuItem mnuBookmarksPrevious;
        private ToolStripMenuItem mnuBookmarksNext;
        private ToolStripMenuItem mnuBookmarksRemoveAll;
        private ToolStripButton toolStripBtnUndo;
        private ToolStripButton toolStripBtnRedo;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem mnuHelpHelp;
        private ToolStripSeparator toolStripMenuItem5;
    }
}