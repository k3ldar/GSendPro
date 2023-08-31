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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelWarnings = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            mnuFile = new ToolStripMenuItem();
            mnuFileNew = new ToolStripMenuItem();
            mnuFileOpen = new ToolStripMenuItem();
            mnuFileSave = new ToolStripMenuItem();
            mnuFileSaveAs = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripSeparator();
            mnufileSaveAsSubprogram = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            mnuFileRecent = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripSeparator();
            mnuFileExit = new ToolStripMenuItem();
            mnuEdit = new ToolStripMenuItem();
            mnuEditUndo = new ToolStripMenuItem();
            mnuEditRedo = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            mnuEditCut = new ToolStripMenuItem();
            mnuEditCopy = new ToolStripMenuItem();
            mnuEditPaste = new ToolStripMenuItem();
            mnuBookmarks = new ToolStripMenuItem();
            mnuBookmarksToggle = new ToolStripMenuItem();
            mnuBookmarksPrevious = new ToolStripMenuItem();
            mnuBookmarksNext = new ToolStripMenuItem();
            mnuBookmarksRemoveAll = new ToolStripMenuItem();
            mnuView = new ToolStripMenuItem();
            mnuViewStatusBar = new ToolStripMenuItem();
            mnuViewPreview = new ToolStripMenuItem();
            mnuViewProperties = new ToolStripMenuItem();
            mnuViewSubPrograms = new ToolStripMenuItem();
            mnuHelp = new ToolStripMenuItem();
            mnuHelpHelp = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripSeparator();
            mnuHelpAbout = new ToolStripMenuItem();
            saveFileDialog1 = new SaveFileDialog();
            fontDialog1 = new FontDialog();
            openFileDialog1 = new OpenFileDialog();
            splitContainerPrimary = new SplitContainer();
            splitContainerMain = new SplitContainer();
            txtGCode = new FastColoredTextBoxNS.FastColoredTextBox();
            contextMenuStripEditor = new ContextMenuStrip(components);
            tabControlMain = new TabControl();
            tabPagePreview = new TabPage();
            machine2dView1 = new GSendControls.Machine2DView();
            tabPageProperties = new TabPage();
            gCodeAnalysesDetails1 = new GSendControls.GCodeAnalysesDetails();
            tabPageSubPrograms = new TabPage();
            lvSubprograms = new GSendControls.ListViewEx();
            columnHeaderName = new ColumnHeader();
            columnHeaderDescription = new ColumnHeader();
            toolbarImageListSmall = new ImageList(components);
            lstWarningsErrors = new ListBox();
            toolbarMain = new ToolStrip();
            toolStripBtnNew = new ToolStripButton();
            toolStripBtnOpen = new ToolStripButton();
            toolStripBtnSave = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripBtnSaveAsSubProgram = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripBtnUndo = new ToolStripButton();
            toolStripBtnRedo = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripBtnToggleBookmark = new ToolStripButton();
            toolStripBtnPreviousBookmark = new ToolStripButton();
            toolStripBtnNextBookmark = new ToolStripButton();
            toolStripBtnClearBookmarks = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            toolStripButtonRefreshSubPrograms = new ToolStripButton();
            tmrServerValidation = new System.Windows.Forms.Timer(components);
            tmrUpdateSubprograms = new System.Windows.Forms.Timer(components);
            toolStripMenuItem6 = new ToolStripSeparator();
            bugsAndIdeasToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerPrimary).BeginInit();
            splitContainerPrimary.Panel1.SuspendLayout();
            splitContainerPrimary.Panel2.SuspendLayout();
            splitContainerPrimary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtGCode).BeginInit();
            tabControlMain.SuspendLayout();
            tabPagePreview.SuspendLayout();
            tabPageProperties.SuspendLayout();
            tabPageSubPrograms.SuspendLayout();
            toolbarMain.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelWarnings });
            statusStrip1.Location = new Point(0, 511);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1083, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelWarnings
            // 
            toolStripStatusLabelWarnings.AutoSize = false;
            toolStripStatusLabelWarnings.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabelWarnings.Image = (Image)resources.GetObject("toolStripStatusLabelWarnings.Image");
            toolStripStatusLabelWarnings.Name = "toolStripStatusLabelWarnings";
            toolStripStatusLabelWarnings.Size = new Size(29, 17);
            toolStripStatusLabelWarnings.Text = "0";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { mnuFile, mnuEdit, mnuBookmarks, mnuView, mnuHelp });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1083, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuFileNew, mnuFileOpen, mnuFileSave, mnuFileSaveAs, toolStripMenuItem3, mnufileSaveAsSubprogram, toolStripMenuItem1, mnuFileRecent, toolStripMenuItem4, mnuFileExit });
            mnuFile.Name = "mnuFile";
            mnuFile.Size = new Size(37, 20);
            mnuFile.Text = "&File";
            mnuFile.DropDownOpening += mnuFile_DropDownOpening;
            // 
            // mnuFileNew
            // 
            mnuFileNew.Name = "mnuFileNew";
            mnuFileNew.Size = new Size(183, 22);
            mnuFileNew.Text = "&New";
            mnuFileNew.Click += mnuFileNew_Click;
            // 
            // mnuFileOpen
            // 
            mnuFileOpen.Name = "mnuFileOpen";
            mnuFileOpen.Size = new Size(183, 22);
            mnuFileOpen.Text = "&Open";
            mnuFileOpen.Click += mnuFileOpen_Click;
            // 
            // mnuFileSave
            // 
            mnuFileSave.Name = "mnuFileSave";
            mnuFileSave.Size = new Size(183, 22);
            mnuFileSave.Text = "&Save";
            mnuFileSave.Click += mnuFileSave_Click;
            // 
            // mnuFileSaveAs
            // 
            mnuFileSaveAs.Name = "mnuFileSaveAs";
            mnuFileSaveAs.Size = new Size(183, 22);
            mnuFileSaveAs.Text = "Save &As";
            mnuFileSaveAs.Click += mnuFileSaveAs_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(180, 6);
            // 
            // mnufileSaveAsSubprogram
            // 
            mnufileSaveAsSubprogram.Name = "mnufileSaveAsSubprogram";
            mnufileSaveAsSubprogram.Size = new Size(183, 22);
            mnufileSaveAsSubprogram.Text = "Save As Sub&program";
            mnufileSaveAsSubprogram.Click += mnufileSaveAsSubprogram_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(180, 6);
            // 
            // mnuFileRecent
            // 
            mnuFileRecent.Name = "mnuFileRecent";
            mnuFileRecent.Size = new Size(183, 22);
            mnuFileRecent.Text = "Recent";
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(180, 6);
            // 
            // mnuFileExit
            // 
            mnuFileExit.Name = "mnuFileExit";
            mnuFileExit.Size = new Size(183, 22);
            mnuFileExit.Text = "&Exit";
            mnuFileExit.Click += mnuFileExit_Click;
            // 
            // mnuEdit
            // 
            mnuEdit.DropDownItems.AddRange(new ToolStripItem[] { mnuEditUndo, mnuEditRedo, toolStripMenuItem2, mnuEditCut, mnuEditCopy, mnuEditPaste });
            mnuEdit.Name = "mnuEdit";
            mnuEdit.Size = new Size(39, 20);
            mnuEdit.Text = "&Edit";
            // 
            // mnuEditUndo
            // 
            mnuEditUndo.Name = "mnuEditUndo";
            mnuEditUndo.ShortcutKeys = Keys.Control | Keys.Z;
            mnuEditUndo.Size = new Size(144, 22);
            mnuEditUndo.Text = "Undo";
            mnuEditUndo.Click += mnuEditUndo_Click;
            // 
            // mnuEditRedo
            // 
            mnuEditRedo.Name = "mnuEditRedo";
            mnuEditRedo.ShortcutKeys = Keys.Control | Keys.Y;
            mnuEditRedo.Size = new Size(144, 22);
            mnuEditRedo.Text = "Redo";
            mnuEditRedo.Click += mnuEditRedo_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(141, 6);
            // 
            // mnuEditCut
            // 
            mnuEditCut.Name = "mnuEditCut";
            mnuEditCut.ShortcutKeys = Keys.Control | Keys.X;
            mnuEditCut.Size = new Size(144, 22);
            mnuEditCut.Text = "Cu&t";
            mnuEditCut.Click += mnuEditCut_Click;
            // 
            // mnuEditCopy
            // 
            mnuEditCopy.Name = "mnuEditCopy";
            mnuEditCopy.ShortcutKeys = Keys.Control | Keys.C;
            mnuEditCopy.Size = new Size(144, 22);
            mnuEditCopy.Text = "&Copy";
            mnuEditCopy.Click += mnuEditCopy_Click;
            // 
            // mnuEditPaste
            // 
            mnuEditPaste.Name = "mnuEditPaste";
            mnuEditPaste.ShortcutKeys = Keys.Control | Keys.V;
            mnuEditPaste.Size = new Size(144, 22);
            mnuEditPaste.Text = "&Paste";
            mnuEditPaste.Click += mnuEditPaste_Click;
            // 
            // mnuBookmarks
            // 
            mnuBookmarks.DropDownItems.AddRange(new ToolStripItem[] { mnuBookmarksToggle, mnuBookmarksPrevious, mnuBookmarksNext, mnuBookmarksRemoveAll });
            mnuBookmarks.Name = "mnuBookmarks";
            mnuBookmarks.Size = new Size(78, 20);
            mnuBookmarks.Text = "Bookmarks";
            // 
            // mnuBookmarksToggle
            // 
            mnuBookmarksToggle.Name = "mnuBookmarksToggle";
            mnuBookmarksToggle.ShortcutKeys = Keys.Alt | Keys.B;
            mnuBookmarksToggle.Size = new Size(213, 22);
            mnuBookmarksToggle.Text = "Toggle Bookmarks";
            mnuBookmarksToggle.Click += mnuBookmarksToggle_Click;
            // 
            // mnuBookmarksPrevious
            // 
            mnuBookmarksPrevious.Name = "mnuBookmarksPrevious";
            mnuBookmarksPrevious.ShortcutKeys = Keys.Alt | Keys.P;
            mnuBookmarksPrevious.Size = new Size(213, 22);
            mnuBookmarksPrevious.Text = "Previous Bookmark";
            mnuBookmarksPrevious.Click += mnuBookmarksPrevious_Click;
            // 
            // mnuBookmarksNext
            // 
            mnuBookmarksNext.Name = "mnuBookmarksNext";
            mnuBookmarksNext.ShortcutKeys = Keys.Alt | Keys.N;
            mnuBookmarksNext.Size = new Size(213, 22);
            mnuBookmarksNext.Text = "Next Bookmark";
            mnuBookmarksNext.Click += mnuBookmarksNext_Click;
            // 
            // mnuBookmarksRemoveAll
            // 
            mnuBookmarksRemoveAll.Name = "mnuBookmarksRemoveAll";
            mnuBookmarksRemoveAll.Size = new Size(213, 22);
            mnuBookmarksRemoveAll.Text = "Remove All Bookmarks";
            mnuBookmarksRemoveAll.Click += mnuBookmarksRemoveAll_Click;
            // 
            // mnuView
            // 
            mnuView.DropDownItems.AddRange(new ToolStripItem[] { mnuViewStatusBar, mnuViewPreview, mnuViewProperties, mnuViewSubPrograms });
            mnuView.Name = "mnuView";
            mnuView.Size = new Size(44, 20);
            mnuView.Text = "&View";
            // 
            // mnuViewStatusBar
            // 
            mnuViewStatusBar.Name = "mnuViewStatusBar";
            mnuViewStatusBar.Size = new Size(181, 22);
            mnuViewStatusBar.Text = "&Status Bar";
            mnuViewStatusBar.Visible = false;
            mnuViewStatusBar.Click += mnuViewStatusBar_Click;
            // 
            // mnuViewPreview
            // 
            mnuViewPreview.Name = "mnuViewPreview";
            mnuViewPreview.ShortcutKeys = Keys.Alt | Keys.P;
            mnuViewPreview.Size = new Size(181, 22);
            mnuViewPreview.Text = "&Preview";
            mnuViewPreview.Click += mnuViewPreview_Click;
            // 
            // mnuViewProperties
            // 
            mnuViewProperties.Name = "mnuViewProperties";
            mnuViewProperties.ShortcutKeys = Keys.Alt | Keys.R;
            mnuViewProperties.Size = new Size(181, 22);
            mnuViewProperties.Text = "Properties";
            mnuViewProperties.Click += propertiesToolStripMenuItem_Click;
            // 
            // mnuViewSubPrograms
            // 
            mnuViewSubPrograms.Name = "mnuViewSubPrograms";
            mnuViewSubPrograms.ShortcutKeys = Keys.Alt | Keys.S;
            mnuViewSubPrograms.Size = new Size(181, 22);
            mnuViewSubPrograms.Text = "Subprograms";
            mnuViewSubPrograms.Click += subprogramsToolStripMenuItem_Click;
            // 
            // mnuHelp
            // 
            mnuHelp.DropDownItems.AddRange(new ToolStripItem[] { mnuHelpHelp, toolStripMenuItem5, bugsAndIdeasToolStripMenuItem, toolStripMenuItem6, mnuHelpAbout });
            mnuHelp.Name = "mnuHelp";
            mnuHelp.Size = new Size(44, 20);
            mnuHelp.Text = "&Help";
            // 
            // mnuHelpHelp
            // 
            mnuHelpHelp.Name = "mnuHelpHelp";
            mnuHelpHelp.ShortcutKeys = Keys.F1;
            mnuHelpHelp.Size = new Size(180, 22);
            mnuHelpHelp.Text = "Help";
            mnuHelpHelp.Click += mnuHelpHelp_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(177, 6);
            // 
            // mnuHelpAbout
            // 
            mnuHelpAbout.Name = "mnuHelpAbout";
            mnuHelpAbout.Size = new Size(180, 22);
            mnuHelpAbout.Text = "&About";
            mnuHelpAbout.Click += mnuHelpAbout_Click;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.DefaultExt = "gcode";
            saveFileDialog1.Filter = "G Code Files|*.gcode;*.nc;*.ncc;*.ngc;*.tap;*.txt|All Files|*.*";
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "G Code Files|*.gcode;*.nc;*.ncc;*.ngc;*.tap;*.txt|All Files|*.*";
            // 
            // splitContainerPrimary
            // 
            splitContainerPrimary.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainerPrimary.Location = new Point(12, 56);
            splitContainerPrimary.Name = "splitContainerPrimary";
            splitContainerPrimary.Orientation = Orientation.Horizontal;
            // 
            // splitContainerPrimary.Panel1
            // 
            splitContainerPrimary.Panel1.Controls.Add(splitContainerMain);
            // 
            // splitContainerPrimary.Panel2
            // 
            splitContainerPrimary.Panel2.Controls.Add(lstWarningsErrors);
            splitContainerPrimary.Size = new Size(1059, 446);
            splitContainerPrimary.SplitterDistance = 321;
            splitContainerPrimary.TabIndex = 5;
            // 
            // splitContainerMain
            // 
            splitContainerMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainerMain.Location = new Point(3, 3);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(txtGCode);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(tabControlMain);
            splitContainerMain.Size = new Size(1053, 315);
            splitContainerMain.SplitterDistance = 631;
            splitContainerMain.TabIndex = 5;
            // 
            // txtGCode
            // 
            txtGCode.AllowMacroRecording = false;
            txtGCode.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtGCode.AutoCompleteBracketsList = new char[] { '(', ')', '{', '}', '[', ']', '"', '"', '\'', '\'' };
            txtGCode.AutoIndent = false;
            txtGCode.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);";
            txtGCode.AutoScrollMinSize = new Size(27, 14);
            txtGCode.BackBrush = null;
            txtGCode.CharHeight = 14;
            txtGCode.CharWidth = 8;
            txtGCode.ContextMenuStrip = contextMenuStripEditor;
            txtGCode.DefaultMarkerSize = 8;
            txtGCode.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            txtGCode.IsReplaceMode = false;
            txtGCode.LeftBracket = '[';
            txtGCode.LeftBracket2 = '(';
            txtGCode.Location = new Point(3, 3);
            txtGCode.Name = "txtGCode";
            txtGCode.Paddings = new Padding(0);
            txtGCode.PreferredLineWidth = 256;
            txtGCode.RightBracket = ']';
            txtGCode.RightBracket2 = ')';
            txtGCode.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            txtGCode.ServiceColors = (FastColoredTextBoxNS.ServiceColors)resources.GetObject("txtGCode.ServiceColors");
            txtGCode.Size = new Size(625, 313);
            txtGCode.TabIndex = 0;
            txtGCode.Zoom = 100;
            txtGCode.ToolTipNeeded += txtGCode_ToolTipNeeded;
            txtGCode.SelectionChangedDelayed += txtGCode_SelectionChangedDelayed;
            // 
            // contextMenuStripEditor
            // 
            contextMenuStripEditor.Name = "contextMenuStripEditor";
            contextMenuStripEditor.Size = new Size(61, 4);
            // 
            // tabControlMain
            // 
            tabControlMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlMain.Controls.Add(tabPagePreview);
            tabControlMain.Controls.Add(tabPageProperties);
            tabControlMain.Controls.Add(tabPageSubPrograms);
            tabControlMain.Location = new Point(3, 3);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(412, 309);
            tabControlMain.TabIndex = 0;
            // 
            // tabPagePreview
            // 
            tabPagePreview.Controls.Add(machine2dView1);
            tabPagePreview.Location = new Point(4, 24);
            tabPagePreview.Name = "tabPagePreview";
            tabPagePreview.Padding = new Padding(3);
            tabPagePreview.Size = new Size(404, 281);
            tabPagePreview.TabIndex = 0;
            tabPagePreview.Text = "tabPagePreview";
            tabPagePreview.UseVisualStyleBackColor = true;
            // 
            // machine2dView1
            // 
            machine2dView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            machine2dView1.BackgroundImage = (Image)resources.GetObject("machine2dView1.BackgroundImage");
            machine2dView1.BackgroundImageLayout = ImageLayout.None;
            machine2dView1.Configuration = GSendShared.AxisConfiguration.None;
            machine2dView1.Location = new Point(9, 9);
            machine2dView1.MachineSize = new Rectangle(0, 0, 5000, 5000);
            machine2dView1.Name = "machine2dView1";
            machine2dView1.Size = new Size(389, 266);
            machine2dView1.TabIndex = 0;
            machine2dView1.XPosition = 0F;
            machine2dView1.YPosition = 0F;
            machine2dView1.ZoomPanel = null;
            // 
            // tabPageProperties
            // 
            tabPageProperties.Controls.Add(gCodeAnalysesDetails1);
            tabPageProperties.Location = new Point(4, 24);
            tabPageProperties.Name = "tabPageProperties";
            tabPageProperties.Padding = new Padding(3);
            tabPageProperties.Size = new Size(404, 281);
            tabPageProperties.TabIndex = 1;
            tabPageProperties.Text = "Properties";
            tabPageProperties.UseVisualStyleBackColor = true;
            // 
            // gCodeAnalysesDetails1
            // 
            gCodeAnalysesDetails1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gCodeAnalysesDetails1.Location = new Point(6, 6);
            gCodeAnalysesDetails1.Name = "gCodeAnalysesDetails1";
            gCodeAnalysesDetails1.Size = new Size(392, 269);
            gCodeAnalysesDetails1.TabIndex = 0;
            // 
            // tabPageSubPrograms
            // 
            tabPageSubPrograms.Controls.Add(lvSubprograms);
            tabPageSubPrograms.Location = new Point(4, 24);
            tabPageSubPrograms.Name = "tabPageSubPrograms";
            tabPageSubPrograms.Padding = new Padding(3);
            tabPageSubPrograms.Size = new Size(404, 281);
            tabPageSubPrograms.TabIndex = 2;
            tabPageSubPrograms.Text = "Subprograms";
            tabPageSubPrograms.UseVisualStyleBackColor = true;
            // 
            // lvSubprograms
            // 
            lvSubprograms.AllowColumnReorder = true;
            lvSubprograms.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lvSubprograms.Columns.AddRange(new ColumnHeader[] { columnHeaderName, columnHeaderDescription });
            lvSubprograms.Location = new Point(9, 9);
            lvSubprograms.Name = "lvSubprograms";
            lvSubprograms.OwnerDraw = true;
            lvSubprograms.SaveName = "";
            lvSubprograms.ShowToolTip = false;
            lvSubprograms.Size = new Size(389, 266);
            lvSubprograms.SmallImageList = toolbarImageListSmall;
            lvSubprograms.TabIndex = 0;
            lvSubprograms.UseCompatibleStateImageBehavior = false;
            lvSubprograms.View = View.Details;
            lvSubprograms.DoubleClick += lvSubprograms_DoubleClick;
            // 
            // columnHeaderName
            // 
            columnHeaderName.Width = 90;
            // 
            // columnHeaderDescription
            // 
            columnHeaderDescription.Width = 200;
            // 
            // toolbarImageListSmall
            // 
            toolbarImageListSmall.ColorDepth = ColorDepth.Depth8Bit;
            toolbarImageListSmall.ImageStream = (ImageListStreamer)resources.GetObject("toolbarImageListSmall.ImageStream");
            toolbarImageListSmall.TransparentColor = Color.Transparent;
            toolbarImageListSmall.Images.SetKeyName(0, "Error_red_16x16.png");
            toolbarImageListSmall.Images.SetKeyName(1, "Error_red_16x16.png");
            toolbarImageListSmall.Images.SetKeyName(2, "Warning_yellow_7231_16x16.png");
            toolbarImageListSmall.Images.SetKeyName(3, "Information_blue_6227_16x16.png");
            toolbarImageListSmall.Images.SetKeyName(4, "Error_red_16x16.png");
            toolbarImageListSmall.Images.SetKeyName(5, "manifest_16xLG.png");
            // 
            // lstWarningsErrors
            // 
            lstWarningsErrors.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstWarningsErrors.DrawMode = DrawMode.OwnerDrawFixed;
            lstWarningsErrors.FormattingEnabled = true;
            lstWarningsErrors.ItemHeight = 20;
            lstWarningsErrors.Location = new Point(3, 0);
            lstWarningsErrors.Name = "lstWarningsErrors";
            lstWarningsErrors.Size = new Size(1053, 104);
            lstWarningsErrors.TabIndex = 0;
            lstWarningsErrors.DrawItem += lstWarningsErrors_DrawItem;
            // 
            // toolbarMain
            // 
            toolbarMain.Items.AddRange(new ToolStripItem[] { toolStripBtnNew, toolStripBtnOpen, toolStripBtnSave, toolStripSeparator1, toolStripBtnSaveAsSubProgram, toolStripSeparator2, toolStripBtnUndo, toolStripBtnRedo, toolStripSeparator3, toolStripBtnToggleBookmark, toolStripBtnPreviousBookmark, toolStripBtnNextBookmark, toolStripBtnClearBookmarks, toolStripSeparator4, toolStripButtonRefreshSubPrograms });
            toolbarMain.Location = new Point(0, 24);
            toolbarMain.Name = "toolbarMain";
            toolbarMain.Size = new Size(1083, 25);
            toolbarMain.TabIndex = 6;
            toolbarMain.Text = "toolbarMain";
            // 
            // toolStripBtnNew
            // 
            toolStripBtnNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnNew.Image = (Image)resources.GetObject("toolStripBtnNew.Image");
            toolStripBtnNew.ImageTransparentColor = Color.Magenta;
            toolStripBtnNew.Name = "toolStripBtnNew";
            toolStripBtnNew.Size = new Size(23, 22);
            toolStripBtnNew.Text = "toolStripBtnNew";
            toolStripBtnNew.Click += mnuFileNew_Click;
            // 
            // toolStripBtnOpen
            // 
            toolStripBtnOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnOpen.Image = (Image)resources.GetObject("toolStripBtnOpen.Image");
            toolStripBtnOpen.ImageTransparentColor = Color.Magenta;
            toolStripBtnOpen.Name = "toolStripBtnOpen";
            toolStripBtnOpen.Size = new Size(23, 22);
            toolStripBtnOpen.Text = "toolStripBtnOpen";
            toolStripBtnOpen.Click += mnuFileOpen_Click;
            // 
            // toolStripBtnSave
            // 
            toolStripBtnSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnSave.Image = (Image)resources.GetObject("toolStripBtnSave.Image");
            toolStripBtnSave.ImageTransparentColor = Color.Magenta;
            toolStripBtnSave.Name = "toolStripBtnSave";
            toolStripBtnSave.Size = new Size(23, 22);
            toolStripBtnSave.Text = "toolStripBtn";
            toolStripBtnSave.Click += mnuFileSave_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripBtnSaveAsSubProgram
            // 
            toolStripBtnSaveAsSubProgram.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnSaveAsSubProgram.Image = (Image)resources.GetObject("toolStripBtnSaveAsSubProgram.Image");
            toolStripBtnSaveAsSubProgram.ImageTransparentColor = Color.Magenta;
            toolStripBtnSaveAsSubProgram.Name = "toolStripBtnSaveAsSubProgram";
            toolStripBtnSaveAsSubProgram.Size = new Size(23, 22);
            toolStripBtnSaveAsSubProgram.Text = "toolStripButton5";
            toolStripBtnSaveAsSubProgram.Click += mnufileSaveAsSubprogram_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolStripBtnUndo
            // 
            toolStripBtnUndo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnUndo.Image = (Image)resources.GetObject("toolStripBtnUndo.Image");
            toolStripBtnUndo.ImageTransparentColor = Color.Magenta;
            toolStripBtnUndo.Name = "toolStripBtnUndo";
            toolStripBtnUndo.Size = new Size(23, 22);
            toolStripBtnUndo.Text = "toolStripButton2";
            toolStripBtnUndo.Click += mnuEditUndo_Click;
            // 
            // toolStripBtnRedo
            // 
            toolStripBtnRedo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnRedo.Image = (Image)resources.GetObject("toolStripBtnRedo.Image");
            toolStripBtnRedo.ImageTransparentColor = Color.Magenta;
            toolStripBtnRedo.Name = "toolStripBtnRedo";
            toolStripBtnRedo.Size = new Size(23, 22);
            toolStripBtnRedo.Text = "toolStripButton1";
            toolStripBtnRedo.Click += mnuEditRedo_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // toolStripBtnToggleBookmark
            // 
            toolStripBtnToggleBookmark.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnToggleBookmark.Image = (Image)resources.GetObject("toolStripBtnToggleBookmark.Image");
            toolStripBtnToggleBookmark.ImageTransparentColor = Color.Magenta;
            toolStripBtnToggleBookmark.Name = "toolStripBtnToggleBookmark";
            toolStripBtnToggleBookmark.Size = new Size(23, 22);
            toolStripBtnToggleBookmark.Text = "toolStripButton6";
            toolStripBtnToggleBookmark.Click += mnuBookmarksToggle_Click;
            // 
            // toolStripBtnPreviousBookmark
            // 
            toolStripBtnPreviousBookmark.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnPreviousBookmark.Image = (Image)resources.GetObject("toolStripBtnPreviousBookmark.Image");
            toolStripBtnPreviousBookmark.ImageTransparentColor = Color.Magenta;
            toolStripBtnPreviousBookmark.Name = "toolStripBtnPreviousBookmark";
            toolStripBtnPreviousBookmark.Size = new Size(23, 22);
            toolStripBtnPreviousBookmark.Text = "toolStripButton7";
            toolStripBtnPreviousBookmark.Click += mnuBookmarksPrevious_Click;
            // 
            // toolStripBtnNextBookmark
            // 
            toolStripBtnNextBookmark.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnNextBookmark.Image = (Image)resources.GetObject("toolStripBtnNextBookmark.Image");
            toolStripBtnNextBookmark.ImageTransparentColor = Color.Magenta;
            toolStripBtnNextBookmark.Name = "toolStripBtnNextBookmark";
            toolStripBtnNextBookmark.Size = new Size(23, 22);
            toolStripBtnNextBookmark.Text = "toolStripButton8";
            toolStripBtnNextBookmark.Click += mnuBookmarksNext_Click;
            // 
            // toolStripBtnClearBookmarks
            // 
            toolStripBtnClearBookmarks.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnClearBookmarks.Image = (Image)resources.GetObject("toolStripBtnClearBookmarks.Image");
            toolStripBtnClearBookmarks.ImageTransparentColor = Color.Magenta;
            toolStripBtnClearBookmarks.Name = "toolStripBtnClearBookmarks";
            toolStripBtnClearBookmarks.Size = new Size(23, 22);
            toolStripBtnClearBookmarks.Text = "toolStripButton9";
            toolStripBtnClearBookmarks.Click += mnuBookmarksRemoveAll_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 25);
            // 
            // toolStripButtonRefreshSubPrograms
            // 
            toolStripButtonRefreshSubPrograms.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonRefreshSubPrograms.Image = (Image)resources.GetObject("toolStripButtonRefreshSubPrograms.Image");
            toolStripButtonRefreshSubPrograms.ImageTransparentColor = Color.Magenta;
            toolStripButtonRefreshSubPrograms.Name = "toolStripButtonRefreshSubPrograms";
            toolStripButtonRefreshSubPrograms.Size = new Size(23, 22);
            toolStripButtonRefreshSubPrograms.Click += toolStripButtonRefreshSubPrograms_Click;
            // 
            // tmrServerValidation
            // 
            tmrServerValidation.Interval = 5000;
            tmrServerValidation.Tick += tmrServerValidation_Tick;
            // 
            // tmrUpdateSubprograms
            // 
            tmrUpdateSubprograms.Interval = 1000;
            tmrUpdateSubprograms.Tick += tmrUpdateSubprograms_Tick;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(177, 6);
            // 
            // bugsAndIdeasToolStripMenuItem
            // 
            bugsAndIdeasToolStripMenuItem.Name = "bugsAndIdeasToolStripMenuItem";
            bugsAndIdeasToolStripMenuItem.Size = new Size(180, 22);
            bugsAndIdeasToolStripMenuItem.Text = "Bugs and Ideas";
            bugsAndIdeasToolStripMenuItem.Click += bugsAndIdeasToolStripMenuItem_Click;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1083, 533);
            Controls.Add(toolbarMain);
            Controls.Add(splitContainerPrimary);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "FrmMain";
            Text = "Form1";
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            Shown += FrmMain_Shown;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainerPrimary.Panel1.ResumeLayout(false);
            splitContainerPrimary.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerPrimary).EndInit();
            splitContainerPrimary.ResumeLayout(false);
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtGCode).EndInit();
            tabControlMain.ResumeLayout(false);
            tabPagePreview.ResumeLayout(false);
            tabPageProperties.ResumeLayout(false);
            tabPageSubPrograms.ResumeLayout(false);
            toolbarMain.ResumeLayout(false);
            toolbarMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private GSendControls.ListViewEx lvSubprograms;
        private System.Windows.Forms.Timer tmrServerValidation;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton toolStripButtonRefreshSubPrograms;
        private System.Windows.Forms.Timer tmrUpdateSubprograms;
        private ToolStripMenuItem bugsAndIdeasToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem6;
    }
}