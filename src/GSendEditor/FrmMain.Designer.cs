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
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSubPrograms = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.txtGCode = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPagePreview = new System.Windows.Forms.TabPage();
            this.machine2dView1 = new GSendControls.Machine2DView();
            this.tabPageProperties = new System.Windows.Forms.TabPage();
            this.gCodeAnalysesDetails1 = new GSendControls.GCodeAnalysesDetails();
            this.tabPageSubPrograms = new System.Windows.Forms.TabPage();
            this.lvSubprograms = new System.Windows.Forms.ListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDescription = new System.Windows.Forms.ColumnHeader();
            this.lstWarningsErrors = new System.Windows.Forms.ListBox();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStripEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGCode)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.tabPagePreview.SuspendLayout();
            this.tabPageProperties.SuspendLayout();
            this.tabPageSubPrograms.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelWarnings});
            this.statusStrip1.Location = new System.Drawing.Point(0, 409);
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
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
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
            this.mnuEditUndo.Size = new System.Drawing.Size(180, 22);
            this.mnuEditUndo.Text = "Undo";
            this.mnuEditUndo.Click += new System.EventHandler(this.mnuEditUndo_Click);
            // 
            // mnuEditRedo
            // 
            this.mnuEditRedo.Name = "mnuEditRedo";
            this.mnuEditRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.mnuEditRedo.Size = new System.Drawing.Size(180, 22);
            this.mnuEditRedo.Text = "Redo";
            this.mnuEditRedo.Click += new System.EventHandler(this.mnuEditRedo_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnuEditCut.Size = new System.Drawing.Size(180, 22);
            this.mnuEditCut.Text = "Cu&t";
            this.mnuEditCut.Click += new System.EventHandler(this.mnuEditCut_Click);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuEditCopy.Size = new System.Drawing.Size(180, 22);
            this.mnuEditCopy.Text = "&Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.mnuEditCopy_Click);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mnuEditPaste.Size = new System.Drawing.Size(180, 22);
            this.mnuEditPaste.Text = "&Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.mnuEditPaste_Click);
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
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(107, 22);
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
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 40);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainerMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lstWarningsErrors);
            this.splitContainer1.Size = new System.Drawing.Size(1059, 356);
            this.splitContainer1.SplitterDistance = 257;
            this.splitContainer1.TabIndex = 5;
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
            this.splitContainerMain.Size = new System.Drawing.Size(1053, 251);
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
            this.txtGCode.RightBracket = ']';
            this.txtGCode.RightBracket2 = ')';
            this.txtGCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtGCode.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtGCode.ServiceColors")));
            this.txtGCode.Size = new System.Drawing.Size(625, 245);
            this.txtGCode.TabIndex = 0;
            this.txtGCode.Zoom = 100;
            this.txtGCode.ToolTipNeeded += new System.EventHandler<FastColoredTextBoxNS.ToolTipNeededEventArgs>(this.txtGCode_ToolTipNeeded);
            this.txtGCode.SelectionChangedDelayed += new System.EventHandler(this.txtGCode_SelectionChangedDelayed);
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
            this.tabControlMain.Size = new System.Drawing.Size(412, 245);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPagePreview
            // 
            this.tabPagePreview.Controls.Add(this.machine2dView1);
            this.tabPagePreview.Location = new System.Drawing.Point(4, 24);
            this.tabPagePreview.Name = "tabPagePreview";
            this.tabPagePreview.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePreview.Size = new System.Drawing.Size(404, 217);
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
            this.machine2dView1.Size = new System.Drawing.Size(389, 202);
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
            this.tabPageProperties.Size = new System.Drawing.Size(404, 217);
            this.tabPageProperties.TabIndex = 1;
            this.tabPageProperties.Text = "Properties";
            this.tabPageProperties.UseVisualStyleBackColor = true;
            // 
            // gCodeAnalysesDetails1
            // 
            this.gCodeAnalysesDetails1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gCodeAnalysesDetails1.Location = new System.Drawing.Point(9, 9);
            this.gCodeAnalysesDetails1.Name = "gCodeAnalysesDetails1";
            this.gCodeAnalysesDetails1.Size = new System.Drawing.Size(389, 202);
            this.gCodeAnalysesDetails1.TabIndex = 0;
            // 
            // tabPageSubPrograms
            // 
            this.tabPageSubPrograms.Controls.Add(this.lvSubprograms);
            this.tabPageSubPrograms.Location = new System.Drawing.Point(4, 24);
            this.tabPageSubPrograms.Name = "tabPageSubPrograms";
            this.tabPageSubPrograms.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSubPrograms.Size = new System.Drawing.Size(404, 217);
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
            this.lvSubprograms.Size = new System.Drawing.Size(389, 202);
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
            this.lstWarningsErrors.Size = new System.Drawing.Size(1053, 84);
            this.lstWarningsErrors.TabIndex = 0;
            this.lstWarningsErrors.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstWarningsErrors_DrawItem);
            // 
            // ImageList
            // 
            this.ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "Error_red_16x16.png");
            this.ImageList.Images.SetKeyName(1, "Error_red_16x16.png");
            this.ImageList.Images.SetKeyName(2, "Warning_yellow_7231_16x16.png");
            this.ImageList.Images.SetKeyName(3, "Information_blue_6227_16x16.png");
            this.ImageList.Images.SetKeyName(4, "Error_red_16x16.png");
            // 
            // contextMenuStripEditor
            // 
            this.contextMenuStripEditor.Name = "contextMenuStripEditor";
            this.contextMenuStripEditor.Size = new System.Drawing.Size(61, 4);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 431);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtGCode)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.tabPagePreview.ResumeLayout(false);
            this.tabPageProperties.ResumeLayout(false);
            this.tabPageSubPrograms.ResumeLayout(false);
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
        private SplitContainer splitContainer1;
        private SplitContainer splitContainerMain;
        private FastColoredTextBoxNS.FastColoredTextBox txtGCode;
        private TabControl tabControlMain;
        private TabPage tabPagePreview;
        private GSendControls.Machine2DView machine2dView1;
        private TabPage tabPageProperties;
        private GSendControls.GCodeAnalysesDetails gCodeAnalysesDetails1;
        private TabPage tabPageSubPrograms;
        private ListView lvSubprograms;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderDescription;
        private ListBox lstWarningsErrors;
        private ImageList ImageList;
        private ContextMenuStrip contextMenuStripEditor;
    }
}