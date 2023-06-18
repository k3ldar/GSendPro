namespace GSendDesktop.Forms
{
    partial class FrmMachine
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMachine));
            this.selectionOverrideSpindle = new GSendControls.Selection();
            this.selectionOverrideZDown = new GSendControls.Selection();
            this.selectionOverrideZUp = new GSendControls.Selection();
            this.selectionOverrideRapids = new GSendControls.Selection();
            this.jogControl = new GSendControls.JogControl();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDisconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonClearAlarm = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonHome = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonProbe = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonResume = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButtonCoordinateSystem = new System.Windows.Forms.ToolStripDropDownButton();
            this.g54ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.g55ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.g56ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.g57ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.g58ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.g59ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelServerConnect = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCpu = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelWarnings = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSpindle = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelFeedRate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDisplayUnit = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBarJob = new System.Windows.Forms.ToolStripProgressBar();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.lblTotalLines = new System.Windows.Forms.Label();
            this.lblCommandQueueSize = new System.Windows.Forms.Label();
            this.lblQueueSize = new System.Windows.Forms.Label();
            this.lblBufferSize = new System.Windows.Forms.Label();
            this.lblJobTime = new System.Windows.Forms.Label();
            this.gCodeAnalysesDetails = new GSendControls.GCodeAnalysesDetails();
            this.machinePositionGeneral = new GSendControls.MachinePosition();
            this.tabPageOverrides = new System.Windows.Forms.TabPage();
            this.cbOverrideLinkSpindle = new System.Windows.Forms.CheckBox();
            this.cbOverrideLinkZDown = new System.Windows.Forms.CheckBox();
            this.cbOverrideLinkZUp = new System.Windows.Forms.CheckBox();
            this.cbOverrideLinkXY = new System.Windows.Forms.CheckBox();
            this.cbOverrideLinkRapids = new System.Windows.Forms.CheckBox();
            this.cbOverridesDisable = new System.Windows.Forms.CheckBox();
            this.labelSpeedPercent = new System.Windows.Forms.Label();
            this.trackBarPercent = new System.Windows.Forms.TrackBar();
            this.machinePositionOverrides = new GSendControls.MachinePosition();
            this.selectionOverrideXY = new GSendControls.Selection();
            this.tabPageJog = new System.Windows.Forms.TabPage();
            this.btnZeroAll = new System.Windows.Forms.Button();
            this.btnZeroZ = new System.Windows.Forms.Button();
            this.btnZeroY = new System.Windows.Forms.Button();
            this.btnZeroX = new System.Windows.Forms.Button();
            this.machinePositionJog = new GSendControls.MachinePosition();
            this.tabPageSpindle = new System.Windows.Forms.TabPage();
            this.grpBoxSpindleSpeed = new System.Windows.Forms.GroupBox();
            this.cbSpindleCounterClockwise = new System.Windows.Forms.CheckBox();
            this.btnSpindleStop = new System.Windows.Forms.Button();
            this.btnSpindleStart = new System.Windows.Forms.Button();
            this.lblSpindleSpeed = new System.Windows.Forms.Label();
            this.trackBarSpindleSpeed = new System.Windows.Forms.TrackBar();
            this.lblDelaySpindleStart = new System.Windows.Forms.Label();
            this.trackBarDelaySpindle = new System.Windows.Forms.TrackBar();
            this.cbSoftStart = new System.Windows.Forms.CheckBox();
            this.lblSpindleType = new System.Windows.Forms.Label();
            this.cmbSpindleType = new System.Windows.Forms.ComboBox();
            this.tabPageServiceSchedule = new System.Windows.Forms.TabPage();
            this.lvServices = new GSendControls.ListViewEx();
            this.columnServiceHeaderDateTime = new System.Windows.Forms.ColumnHeader();
            this.columnServiceHeaderServiceType = new System.Windows.Forms.ColumnHeader();
            this.columnServiceHeaderSpindleHours = new System.Windows.Forms.ColumnHeader();
            this.btnServiceRefresh = new System.Windows.Forms.Button();
            this.lblSpindleHoursRemaining = new System.Windows.Forms.Label();
            this.lblServiceDate = new System.Windows.Forms.Label();
            this.btnServiceReset = new System.Windows.Forms.Button();
            this.lblNextService = new System.Windows.Forms.Label();
            this.lblSpindleHours = new System.Windows.Forms.Label();
            this.trackBarServiceSpindleHours = new System.Windows.Forms.TrackBar();
            this.cbMaintainServiceSchedule = new System.Windows.Forms.CheckBox();
            this.trackBarServiceWeeks = new System.Windows.Forms.TrackBar();
            this.lblServiceSchedule = new System.Windows.Forms.Label();
            this.tabPageMachineSettings = new System.Windows.Forms.TabPage();
            this.btnApplyGrblUpdates = new System.Windows.Forms.Button();
            this.txtGrblUpdates = new System.Windows.Forms.TextBox();
            this.lblPropertyDesc = new System.Windows.Forms.Label();
            this.lblPropertyHeader = new System.Windows.Forms.Label();
            this.propertyGridGrblSettings = new System.Windows.Forms.PropertyGrid();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.lblLayerHeightMeasure = new System.Windows.Forms.Label();
            this.numericLayerHeight = new System.Windows.Forms.NumericUpDown();
            this.cbLayerHeightWarning = new System.Windows.Forms.CheckBox();
            this.grpFeedDisplay = new System.Windows.Forms.GroupBox();
            this.rbFeedDisplayInchMin = new System.Windows.Forms.RadioButton();
            this.rbFeedDisplayInchSec = new System.Windows.Forms.RadioButton();
            this.rbFeedDisplayMmSec = new System.Windows.Forms.RadioButton();
            this.rbFeedDisplayMmMin = new System.Windows.Forms.RadioButton();
            this.grpDisplayUnits = new System.Windows.Forms.GroupBox();
            this.cbAutoSelectFeedbackUnit = new System.Windows.Forms.CheckBox();
            this.rbFeedbackInch = new System.Windows.Forms.RadioButton();
            this.rbFeedbackMm = new System.Windows.Forms.RadioButton();
            this.cbCorrectMode = new System.Windows.Forms.CheckBox();
            this.cbFloodCoolant = new System.Windows.Forms.CheckBox();
            this.cbMistCoolant = new System.Windows.Forms.CheckBox();
            this.cbToolChanger = new System.Windows.Forms.CheckBox();
            this.cbLimitSwitches = new System.Windows.Forms.CheckBox();
            this.probingCommand1 = new GSendControls.ProbingCommand();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuMachine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMachineLoadGCode = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMachineClearGCode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMachineRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMachineClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewGeneral = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewOverrides = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewJog = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSpindle = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewServiceSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewMachineSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewConsole = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAction = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActionSaveConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuActionConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActionDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuActionClearAlarm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuActionHome = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActionProbe = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuActionRun = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActionPause = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActionStop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptionsShortcutKeys = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.warningsAndErrors = new GSendControls.WarningContainer();
            this.tabControlSecondary = new System.Windows.Forms.TabControl();
            this.tabPageConsole = new System.Windows.Forms.TabPage();
            this.btnGrblCommandSend = new System.Windows.Forms.Button();
            this.btnGrblCommandClear = new System.Windows.Forms.Button();
            this.txtUserGrblCommand = new System.Windows.Forms.TextBox();
            this.textBoxConsoleText = new System.Windows.Forms.TextBox();
            this.tabPageGCode = new System.Windows.Forms.TabPage();
            this.listViewGCode = new GSendControls.ListViewEx();
            this.columnHeaderLine = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderGCode = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderComments = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderFeed = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderSpindleSpeed = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAttributes = new System.Windows.Forms.ColumnHeader();
            this.tabPage2DView = new System.Windows.Forms.TabPage();
            this.panelZoom = new System.Windows.Forms.Panel();
            this.machine2dView1 = new GSendControls.Machine2DView();
            this.tabPageHeartbeat = new System.Windows.Forms.TabPage();
            this.flowLayoutPanelHeartbeat = new System.Windows.Forms.FlowLayoutPanel();
            this.heartbeatPanelCommandQueue = new GSendControls.HeartbeatPanel();
            this.heartbeatPanelBufferSize = new GSendControls.HeartbeatPanel();
            this.heartbeatPanelQueueSize = new GSendControls.HeartbeatPanel();
            this.heartbeatPanelFeed = new GSendControls.HeartbeatPanel();
            this.heartbeatPanelSpindle = new GSendControls.HeartbeatPanel();
            this.heartbeatPanelAvailableBlocks = new GSendControls.HeartbeatPanel();
            this.heartbeatPanelAvailableRXBytes = new GSendControls.HeartbeatPanel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStripMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.tabPageOverrides.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPercent)).BeginInit();
            this.tabPageJog.SuspendLayout();
            this.tabPageSpindle.SuspendLayout();
            this.grpBoxSpindleSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpindleSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDelaySpindle)).BeginInit();
            this.tabPageServiceSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarServiceSpindleHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarServiceWeeks)).BeginInit();
            this.tabPageMachineSettings.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLayerHeight)).BeginInit();
            this.grpFeedDisplay.SuspendLayout();
            this.grpDisplayUnits.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControlSecondary.SuspendLayout();
            this.tabPageConsole.SuspendLayout();
            this.tabPageGCode.SuspendLayout();
            this.tabPage2DView.SuspendLayout();
            this.tabPageHeartbeat.SuspendLayout();
            this.flowLayoutPanelHeartbeat.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectionOverrideSpindle
            // 
            this.selectionOverrideSpindle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectionOverrideSpindle.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            this.selectionOverrideSpindle.GroupName = "Spindle";
            this.selectionOverrideSpindle.HandleMouseWheel = false;
            this.selectionOverrideSpindle.HasDisplayUnits = false;
            this.selectionOverrideSpindle.LabelFormat = "{0}";
            this.selectionOverrideSpindle.LabelValue = null;
            this.selectionOverrideSpindle.LargeTickChange = 5;
            this.selectionOverrideSpindle.Location = new System.Drawing.Point(671, 6);
            this.selectionOverrideSpindle.Maximum = 10;
            this.selectionOverrideSpindle.Minimum = 0;
            this.selectionOverrideSpindle.Name = "selectionOverrideSpindle";
            this.selectionOverrideSpindle.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideSpindle.SmallTickChange = 1;
            this.selectionOverrideSpindle.TabIndex = 12;
            this.selectionOverrideSpindle.TickFrequency = 1;
            this.selectionOverrideSpindle.Value = 0;
            this.selectionOverrideSpindle.ValueChanged += new System.EventHandler(this.selectionOverrideSpindle_ValueChanged);
            // 
            // selectionOverrideZDown
            // 
            this.selectionOverrideZDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectionOverrideZDown.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            this.selectionOverrideZDown.GroupName = "Z Down";
            this.selectionOverrideZDown.HandleMouseWheel = false;
            this.selectionOverrideZDown.HasDisplayUnits = true;
            this.selectionOverrideZDown.LabelFormat = "{0}";
            this.selectionOverrideZDown.LabelValue = null;
            this.selectionOverrideZDown.LargeTickChange = 50;
            this.selectionOverrideZDown.Location = new System.Drawing.Point(584, 6);
            this.selectionOverrideZDown.Maximum = 10;
            this.selectionOverrideZDown.Minimum = 0;
            this.selectionOverrideZDown.Name = "selectionOverrideZDown";
            this.selectionOverrideZDown.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideZDown.SmallTickChange = 5;
            this.selectionOverrideZDown.TabIndex = 11;
            this.selectionOverrideZDown.TickFrequency = 1;
            this.selectionOverrideZDown.Value = 0;
            // 
            // selectionOverrideZUp
            // 
            this.selectionOverrideZUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectionOverrideZUp.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            this.selectionOverrideZUp.GroupName = "Z Up";
            this.selectionOverrideZUp.HandleMouseWheel = false;
            this.selectionOverrideZUp.HasDisplayUnits = true;
            this.selectionOverrideZUp.LabelFormat = "{0}";
            this.selectionOverrideZUp.LabelValue = null;
            this.selectionOverrideZUp.LargeTickChange = 50;
            this.selectionOverrideZUp.Location = new System.Drawing.Point(496, 6);
            this.selectionOverrideZUp.Maximum = 10;
            this.selectionOverrideZUp.Minimum = 0;
            this.selectionOverrideZUp.Name = "selectionOverrideZUp";
            this.selectionOverrideZUp.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideZUp.SmallTickChange = 5;
            this.selectionOverrideZUp.TabIndex = 10;
            this.selectionOverrideZUp.TickFrequency = 1;
            this.selectionOverrideZUp.Value = 0;
            // 
            // selectionOverrideRapids
            // 
            this.selectionOverrideRapids.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectionOverrideRapids.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            this.selectionOverrideRapids.GroupName = "Rapids";
            this.selectionOverrideRapids.HandleMouseWheel = true;
            this.selectionOverrideRapids.HasDisplayUnits = false;
            this.selectionOverrideRapids.LabelFormat = "High";
            this.selectionOverrideRapids.LabelValue = null;
            this.selectionOverrideRapids.LargeTickChange = 1;
            this.selectionOverrideRapids.Location = new System.Drawing.Point(320, 6);
            this.selectionOverrideRapids.Maximum = 2;
            this.selectionOverrideRapids.Minimum = 0;
            this.selectionOverrideRapids.Name = "selectionOverrideRapids";
            this.selectionOverrideRapids.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideRapids.SmallTickChange = 1;
            this.selectionOverrideRapids.TabIndex = 8;
            this.selectionOverrideRapids.TickFrequency = 1;
            this.selectionOverrideRapids.Value = 2;
            // 
            // jogControl
            // 
            this.jogControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.jogControl.FeedMaximum = 10;
            this.jogControl.FeedMinimum = 0;
            this.jogControl.FeedRate = 0;
            this.jogControl.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            this.jogControl.Location = new System.Drawing.Point(320, 6);
            this.jogControl.Name = "jogControl";
            this.jogControl.Size = new System.Drawing.Size(439, 190);
            this.jogControl.StepValue = 0;
            this.jogControl.TabIndex = 5;
            this.jogControl.OnJogStart += new GSendShared.JogCommand(this.jogControl_OnJogStart);
            this.jogControl.OnJogStop += new System.EventHandler(this.jogControl_OnJogStop);
            this.jogControl.OnUpdate += new System.EventHandler(this.jogControl_OnUpdate);
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSave,
            this.toolStripSeparator4,
            this.toolStripButtonConnect,
            this.toolStripButtonDisconnect,
            this.toolStripSeparator1,
            this.toolStripButtonClearAlarm,
            this.toolStripSeparator2,
            this.toolStripButtonHome,
            this.toolStripButtonProbe,
            this.toolStripSeparator3,
            this.toolStripButtonResume,
            this.toolStripButtonPause,
            this.toolStripButtonStop,
            this.toolStripSeparator5,
            this.toolStripDropDownButtonCoordinateSystem});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(796, 57);
            this.toolStripMain.TabIndex = 1;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 57);
            // 
            // toolStripButtonConnect
            // 
            this.toolStripButtonConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonConnect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonConnect.Image")));
            this.toolStripButtonConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonConnect.Name = "toolStripButtonConnect";
            this.toolStripButtonConnect.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonConnect.Text = "toolStripButton1";
            this.toolStripButtonConnect.Click += new System.EventHandler(this.toolStripButtonConnect_Click);
            // 
            // toolStripButtonDisconnect
            // 
            this.toolStripButtonDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDisconnect.Image")));
            this.toolStripButtonDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDisconnect.Name = "toolStripButtonDisconnect";
            this.toolStripButtonDisconnect.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonDisconnect.Text = "toolStripButton1";
            this.toolStripButtonDisconnect.Click += new System.EventHandler(this.toolStripButtonDisconnect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 57);
            // 
            // toolStripButtonClearAlarm
            // 
            this.toolStripButtonClearAlarm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonClearAlarm.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClearAlarm.Image")));
            this.toolStripButtonClearAlarm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClearAlarm.Name = "toolStripButtonClearAlarm";
            this.toolStripButtonClearAlarm.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonClearAlarm.Text = "toolStripButton1";
            this.toolStripButtonClearAlarm.Click += new System.EventHandler(this.toolStripButtonClearAlarm_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 57);
            // 
            // toolStripButtonHome
            // 
            this.toolStripButtonHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHome.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHome.Image")));
            this.toolStripButtonHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHome.Name = "toolStripButtonHome";
            this.toolStripButtonHome.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonHome.Text = "toolStripButton1";
            this.toolStripButtonHome.Click += new System.EventHandler(this.toolStripButtonHome_Click);
            // 
            // toolStripButtonProbe
            // 
            this.toolStripButtonProbe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonProbe.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProbe.Image")));
            this.toolStripButtonProbe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProbe.Name = "toolStripButtonProbe";
            this.toolStripButtonProbe.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonProbe.Text = "toolStripButton2";
            this.toolStripButtonProbe.Click += new System.EventHandler(this.toolStripButtonProbe_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 57);
            // 
            // toolStripButtonResume
            // 
            this.toolStripButtonResume.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonResume.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonResume.Image")));
            this.toolStripButtonResume.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonResume.Name = "toolStripButtonResume";
            this.toolStripButtonResume.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonResume.Text = "toolStripButton1";
            this.toolStripButtonResume.Click += new System.EventHandler(this.toolStripButtonResume_Click);
            // 
            // toolStripButtonPause
            // 
            this.toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPause.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPause.Image")));
            this.toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPause.Name = "toolStripButtonPause";
            this.toolStripButtonPause.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonPause.Text = "toolStripButton1";
            this.toolStripButtonPause.Click += new System.EventHandler(this.toolStripButtonPause_Click);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStop.Image")));
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonStop.Text = "toolStripButton2";
            this.toolStripButtonStop.Click += new System.EventHandler(this.toolStripButtonStop_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 57);
            // 
            // toolStripDropDownButtonCoordinateSystem
            // 
            this.toolStripDropDownButtonCoordinateSystem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonCoordinateSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.g54ToolStripMenuItem,
            this.g55ToolStripMenuItem,
            this.g56ToolStripMenuItem,
            this.g57ToolStripMenuItem,
            this.g58ToolStripMenuItem,
            this.g59ToolStripMenuItem});
            this.toolStripDropDownButtonCoordinateSystem.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolStripDropDownButtonCoordinateSystem.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonCoordinateSystem.Image")));
            this.toolStripDropDownButtonCoordinateSystem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonCoordinateSystem.Name = "toolStripDropDownButtonCoordinateSystem";
            this.toolStripDropDownButtonCoordinateSystem.Size = new System.Drawing.Size(63, 54);
            this.toolStripDropDownButtonCoordinateSystem.Text = "G54";
            // 
            // g54ToolStripMenuItem
            // 
            this.g54ToolStripMenuItem.Checked = true;
            this.g54ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.g54ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.g54ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.g54ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.g54ToolStripMenuItem.Name = "g54ToolStripMenuItem";
            this.g54ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.g54ToolStripMenuItem.Text = "G54";
            this.g54ToolStripMenuItem.Click += new System.EventHandler(this.ToolstripButtonCoordinates_Click);
            // 
            // g55ToolStripMenuItem
            // 
            this.g55ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.g55ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.g55ToolStripMenuItem.Name = "g55ToolStripMenuItem";
            this.g55ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.g55ToolStripMenuItem.Text = "G55";
            this.g55ToolStripMenuItem.Click += new System.EventHandler(this.ToolstripButtonCoordinates_Click);
            // 
            // g56ToolStripMenuItem
            // 
            this.g56ToolStripMenuItem.CheckOnClick = true;
            this.g56ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.g56ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.g56ToolStripMenuItem.Name = "g56ToolStripMenuItem";
            this.g56ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.g56ToolStripMenuItem.Text = "G56";
            this.g56ToolStripMenuItem.Click += new System.EventHandler(this.ToolstripButtonCoordinates_Click);
            // 
            // g57ToolStripMenuItem
            // 
            this.g57ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.g57ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.g57ToolStripMenuItem.Name = "g57ToolStripMenuItem";
            this.g57ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.g57ToolStripMenuItem.Text = "G57";
            this.g57ToolStripMenuItem.Click += new System.EventHandler(this.ToolstripButtonCoordinates_Click);
            // 
            // g58ToolStripMenuItem
            // 
            this.g58ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.g58ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.g58ToolStripMenuItem.Name = "g58ToolStripMenuItem";
            this.g58ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.g58ToolStripMenuItem.Text = "G58";
            this.g58ToolStripMenuItem.Click += new System.EventHandler(this.ToolstripButtonCoordinates_Click);
            // 
            // g59ToolStripMenuItem
            // 
            this.g59ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.g59ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.g59ToolStripMenuItem.Name = "g59ToolStripMenuItem";
            this.g59ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.g59ToolStripMenuItem.Text = "G59";
            this.g59ToolStripMenuItem.Click += new System.EventHandler(this.ToolstripButtonCoordinates_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelServerConnect,
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabelCpu,
            this.toolStripStatusLabelWarnings,
            this.toolStripStatusLabelSpindle,
            this.toolStripStatusLabelFeedRate,
            this.toolStripStatusLabelDisplayUnit,
            this.toolStripProgressBarJob});
            this.statusStrip.Location = new System.Drawing.Point(0, 591);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.Size = new System.Drawing.Size(796, 24);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabelServerConnect
            // 
            this.toolStripStatusLabelServerConnect.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelServerConnect.Name = "toolStripStatusLabelServerConnect";
            this.toolStripStatusLabelServerConnect.Size = new System.Drawing.Size(92, 19);
            this.toolStripStatusLabelServerConnect.Text = "Not Connected";
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(43, 19);
            this.toolStripStatusLabelStatus.Text = "Status";
            // 
            // toolStripStatusLabelCpu
            // 
            this.toolStripStatusLabelCpu.Name = "toolStripStatusLabelCpu";
            this.toolStripStatusLabelCpu.Size = new System.Drawing.Size(0, 19);
            // 
            // toolStripStatusLabelWarnings
            // 
            this.toolStripStatusLabelWarnings.AutoSize = false;
            this.toolStripStatusLabelWarnings.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelWarnings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabelWarnings.Image")));
            this.toolStripStatusLabelWarnings.Name = "toolStripStatusLabelWarnings";
            this.toolStripStatusLabelWarnings.Size = new System.Drawing.Size(29, 19);
            this.toolStripStatusLabelWarnings.Text = "0";
            this.toolStripStatusLabelWarnings.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripStatusLabelWarnings_Paint);
            // 
            // toolStripStatusLabelSpindle
            // 
            this.toolStripStatusLabelSpindle.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelSpindle.Name = "toolStripStatusLabelSpindle";
            this.toolStripStatusLabelSpindle.Size = new System.Drawing.Size(50, 19);
            this.toolStripStatusLabelSpindle.Text = "Spindle";
            // 
            // toolStripStatusLabelFeedRate
            // 
            this.toolStripStatusLabelFeedRate.Name = "toolStripStatusLabelFeedRate";
            this.toolStripStatusLabelFeedRate.Size = new System.Drawing.Size(32, 19);
            this.toolStripStatusLabelFeedRate.Text = "Feed";
            // 
            // toolStripStatusLabelDisplayUnit
            // 
            this.toolStripStatusLabelDisplayUnit.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelDisplayUnit.Name = "toolStripStatusLabelDisplayUnit";
            this.toolStripStatusLabelDisplayUnit.Size = new System.Drawing.Size(59, 19);
            this.toolStripStatusLabelDisplayUnit.Text = "mm/min";
            // 
            // toolStripProgressBarJob
            // 
            this.toolStripProgressBarJob.Name = "toolStripProgressBarJob";
            this.toolStripProgressBarJob.Size = new System.Drawing.Size(100, 18);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageMain);
            this.tabControlMain.Controls.Add(this.tabPageOverrides);
            this.tabControlMain.Controls.Add(this.tabPageJog);
            this.tabControlMain.Controls.Add(this.tabPageSpindle);
            this.tabControlMain.Controls.Add(this.tabPageServiceSchedule);
            this.tabControlMain.Controls.Add(this.tabPageMachineSettings);
            this.tabControlMain.Controls.Add(this.tabPageSettings);
            this.tabControlMain.HotTrack = true;
            this.tabControlMain.Location = new System.Drawing.Point(9, 139);
            this.tabControlMain.MinimumSize = new System.Drawing.Size(773, 270);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(773, 270);
            this.tabControlMain.TabIndex = 2;
            this.tabControlMain.Resize += new System.EventHandler(this.tabControlMain_Resize);
            // 
            // tabPageMain
            // 
            this.tabPageMain.BackColor = System.Drawing.Color.White;
            this.tabPageMain.Controls.Add(this.lblTotalLines);
            this.tabPageMain.Controls.Add(this.lblCommandQueueSize);
            this.tabPageMain.Controls.Add(this.lblQueueSize);
            this.tabPageMain.Controls.Add(this.lblBufferSize);
            this.tabPageMain.Controls.Add(this.lblJobTime);
            this.tabPageMain.Controls.Add(this.gCodeAnalysesDetails);
            this.tabPageMain.Controls.Add(this.machinePositionGeneral);
            this.tabPageMain.Location = new System.Drawing.Point(4, 24);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(765, 242);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "General";
            // 
            // lblTotalLines
            // 
            this.lblTotalLines.AutoSize = true;
            this.lblTotalLines.Location = new System.Drawing.Point(8, 193);
            this.lblTotalLines.Name = "lblTotalLines";
            this.lblTotalLines.Size = new System.Drawing.Size(38, 15);
            this.lblTotalLines.TabIndex = 6;
            this.lblTotalLines.Text = "label3";
            // 
            // lblCommandQueueSize
            // 
            this.lblCommandQueueSize.AutoSize = true;
            this.lblCommandQueueSize.Location = new System.Drawing.Point(8, 175);
            this.lblCommandQueueSize.Name = "lblCommandQueueSize";
            this.lblCommandQueueSize.Size = new System.Drawing.Size(38, 15);
            this.lblCommandQueueSize.TabIndex = 5;
            this.lblCommandQueueSize.Text = "label3";
            // 
            // lblQueueSize
            // 
            this.lblQueueSize.AutoSize = true;
            this.lblQueueSize.Location = new System.Drawing.Point(8, 157);
            this.lblQueueSize.Name = "lblQueueSize";
            this.lblQueueSize.Size = new System.Drawing.Size(38, 15);
            this.lblQueueSize.TabIndex = 4;
            this.lblQueueSize.Text = "label2";
            // 
            // lblBufferSize
            // 
            this.lblBufferSize.AutoSize = true;
            this.lblBufferSize.Location = new System.Drawing.Point(8, 139);
            this.lblBufferSize.Name = "lblBufferSize";
            this.lblBufferSize.Size = new System.Drawing.Size(38, 15);
            this.lblBufferSize.TabIndex = 3;
            this.lblBufferSize.Text = "label1";
            // 
            // lblJobTime
            // 
            this.lblJobTime.AutoSize = true;
            this.lblJobTime.Location = new System.Drawing.Point(8, 121);
            this.lblJobTime.Name = "lblJobTime";
            this.lblJobTime.Size = new System.Drawing.Size(67, 15);
            this.lblJobTime.TabIndex = 2;
            this.lblJobTime.Text = "Total Time: ";
            // 
            // gCodeAnalysesDetails
            // 
            this.gCodeAnalysesDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gCodeAnalysesDetails.Location = new System.Drawing.Point(325, 6);
            this.gCodeAnalysesDetails.MinimumSize = new System.Drawing.Size(433, 218);
            this.gCodeAnalysesDetails.Name = "gCodeAnalysesDetails";
            this.gCodeAnalysesDetails.Size = new System.Drawing.Size(434, 226);
            this.gCodeAnalysesDetails.TabIndex = 1;
            // 
            // machinePositionGeneral
            // 
            this.machinePositionGeneral.DisplayFeedbackUnit = GSendShared.FeedbackUnit.Mm;
            this.machinePositionGeneral.Location = new System.Drawing.Point(6, 6);
            this.machinePositionGeneral.Name = "machinePositionGeneral";
            this.machinePositionGeneral.Size = new System.Drawing.Size(314, 112);
            this.machinePositionGeneral.TabIndex = 0;
            // 
            // tabPageOverrides
            // 
            this.tabPageOverrides.BackColor = System.Drawing.Color.White;
            this.tabPageOverrides.Controls.Add(this.cbOverrideLinkSpindle);
            this.tabPageOverrides.Controls.Add(this.cbOverrideLinkZDown);
            this.tabPageOverrides.Controls.Add(this.cbOverrideLinkZUp);
            this.tabPageOverrides.Controls.Add(this.cbOverrideLinkXY);
            this.tabPageOverrides.Controls.Add(this.cbOverrideLinkRapids);
            this.tabPageOverrides.Controls.Add(this.cbOverridesDisable);
            this.tabPageOverrides.Controls.Add(this.labelSpeedPercent);
            this.tabPageOverrides.Controls.Add(this.trackBarPercent);
            this.tabPageOverrides.Controls.Add(this.machinePositionOverrides);
            this.tabPageOverrides.Controls.Add(this.selectionOverrideSpindle);
            this.tabPageOverrides.Controls.Add(this.selectionOverrideZDown);
            this.tabPageOverrides.Controls.Add(this.selectionOverrideRapids);
            this.tabPageOverrides.Controls.Add(this.selectionOverrideZUp);
            this.tabPageOverrides.Controls.Add(this.selectionOverrideXY);
            this.tabPageOverrides.Location = new System.Drawing.Point(4, 24);
            this.tabPageOverrides.Name = "tabPageOverrides";
            this.tabPageOverrides.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOverrides.Size = new System.Drawing.Size(765, 242);
            this.tabPageOverrides.TabIndex = 1;
            this.tabPageOverrides.Text = "Overrides";
            // 
            // cbOverrideLinkSpindle
            // 
            this.cbOverrideLinkSpindle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOverrideLinkSpindle.AutoSize = true;
            this.cbOverrideLinkSpindle.Location = new System.Drawing.Point(678, 9);
            this.cbOverrideLinkSpindle.Name = "cbOverrideLinkSpindle";
            this.cbOverrideLinkSpindle.Size = new System.Drawing.Size(83, 19);
            this.cbOverrideLinkSpindle.TabIndex = 13;
            this.cbOverrideLinkSpindle.Text = "checkBox1";
            this.cbOverrideLinkSpindle.UseVisualStyleBackColor = true;
            // 
            // cbOverrideLinkZDown
            // 
            this.cbOverrideLinkZDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOverrideLinkZDown.AutoSize = true;
            this.cbOverrideLinkZDown.Location = new System.Drawing.Point(592, 9);
            this.cbOverrideLinkZDown.Name = "cbOverrideLinkZDown";
            this.cbOverrideLinkZDown.Size = new System.Drawing.Size(67, 19);
            this.cbOverrideLinkZDown.TabIndex = 6;
            this.cbOverrideLinkZDown.Text = "Z Down";
            this.cbOverrideLinkZDown.UseVisualStyleBackColor = true;
            this.cbOverrideLinkZDown.CheckedChanged += new System.EventHandler(this.OverrideAxis_Checked);
            // 
            // cbOverrideLinkZUp
            // 
            this.cbOverrideLinkZUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOverrideLinkZUp.AutoSize = true;
            this.cbOverrideLinkZUp.Location = new System.Drawing.Point(504, 9);
            this.cbOverrideLinkZUp.Name = "cbOverrideLinkZUp";
            this.cbOverrideLinkZUp.Size = new System.Drawing.Size(51, 19);
            this.cbOverrideLinkZUp.TabIndex = 5;
            this.cbOverrideLinkZUp.Text = "Z Up";
            this.cbOverrideLinkZUp.UseVisualStyleBackColor = true;
            this.cbOverrideLinkZUp.CheckedChanged += new System.EventHandler(this.OverrideAxis_Checked);
            // 
            // cbOverrideLinkXY
            // 
            this.cbOverrideLinkXY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOverrideLinkXY.AutoSize = true;
            this.cbOverrideLinkXY.Location = new System.Drawing.Point(416, 9);
            this.cbOverrideLinkXY.Name = "cbOverrideLinkXY";
            this.cbOverrideLinkXY.Size = new System.Drawing.Size(45, 19);
            this.cbOverrideLinkXY.TabIndex = 4;
            this.cbOverrideLinkXY.Text = "X/Y";
            this.cbOverrideLinkXY.UseVisualStyleBackColor = true;
            this.cbOverrideLinkXY.CheckedChanged += new System.EventHandler(this.OverrideAxis_Checked);
            // 
            // cbOverrideLinkRapids
            // 
            this.cbOverrideLinkRapids.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOverrideLinkRapids.AutoSize = true;
            this.cbOverrideLinkRapids.Location = new System.Drawing.Point(328, 9);
            this.cbOverrideLinkRapids.Name = "cbOverrideLinkRapids";
            this.cbOverrideLinkRapids.Size = new System.Drawing.Size(61, 19);
            this.cbOverrideLinkRapids.TabIndex = 3;
            this.cbOverrideLinkRapids.Text = "Rapids";
            this.cbOverrideLinkRapids.UseVisualStyleBackColor = true;
            this.cbOverrideLinkRapids.CheckedChanged += new System.EventHandler(this.OverrideAxis_Checked);
            // 
            // cbOverridesDisable
            // 
            this.cbOverridesDisable.AutoSize = true;
            this.cbOverridesDisable.Location = new System.Drawing.Point(6, 217);
            this.cbOverridesDisable.Name = "cbOverridesDisable";
            this.cbOverridesDisable.Size = new System.Drawing.Size(109, 19);
            this.cbOverridesDisable.TabIndex = 7;
            this.cbOverridesDisable.Text = "override disable";
            this.cbOverridesDisable.UseVisualStyleBackColor = true;
            this.cbOverridesDisable.CheckedChanged += new System.EventHandler(this.SelectionOverride_ValueChanged);
            // 
            // labelSpeedPercent
            // 
            this.labelSpeedPercent.AutoSize = true;
            this.labelSpeedPercent.Location = new System.Drawing.Point(6, 171);
            this.labelSpeedPercent.Name = "labelSpeedPercent";
            this.labelSpeedPercent.Size = new System.Drawing.Size(17, 15);
            this.labelSpeedPercent.TabIndex = 2;
            this.labelSpeedPercent.Text = "%";
            // 
            // trackBarPercent
            // 
            this.trackBarPercent.Location = new System.Drawing.Point(6, 123);
            this.trackBarPercent.Maximum = 100;
            this.trackBarPercent.Minimum = 1;
            this.trackBarPercent.Name = "trackBarPercent";
            this.trackBarPercent.Size = new System.Drawing.Size(308, 45);
            this.trackBarPercent.TabIndex = 1;
            this.trackBarPercent.TickFrequency = 5;
            this.trackBarPercent.Value = 1;
            this.trackBarPercent.ValueChanged += new System.EventHandler(this.trackBarPercent_ValueChanged);
            // 
            // machinePositionOverrides
            // 
            this.machinePositionOverrides.DisplayFeedbackUnit = GSendShared.FeedbackUnit.Mm;
            this.machinePositionOverrides.Location = new System.Drawing.Point(6, 6);
            this.machinePositionOverrides.Name = "machinePositionOverrides";
            this.machinePositionOverrides.Size = new System.Drawing.Size(314, 112);
            this.machinePositionOverrides.TabIndex = 0;
            // 
            // selectionOverrideXY
            // 
            this.selectionOverrideXY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectionOverrideXY.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            this.selectionOverrideXY.GroupName = "Y";
            this.selectionOverrideXY.HandleMouseWheel = false;
            this.selectionOverrideXY.HasDisplayUnits = true;
            this.selectionOverrideXY.LabelFormat = "{0}";
            this.selectionOverrideXY.LabelValue = null;
            this.selectionOverrideXY.LargeTickChange = 50;
            this.selectionOverrideXY.Location = new System.Drawing.Point(408, 6);
            this.selectionOverrideXY.Maximum = 10;
            this.selectionOverrideXY.Minimum = 0;
            this.selectionOverrideXY.Name = "selectionOverrideXY";
            this.selectionOverrideXY.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideXY.SmallTickChange = 5;
            this.selectionOverrideXY.TabIndex = 9;
            this.selectionOverrideXY.TickFrequency = 1;
            this.selectionOverrideXY.Value = 0;
            // 
            // tabPageJog
            // 
            this.tabPageJog.BackColor = System.Drawing.Color.White;
            this.tabPageJog.Controls.Add(this.btnZeroAll);
            this.tabPageJog.Controls.Add(this.btnZeroZ);
            this.tabPageJog.Controls.Add(this.btnZeroY);
            this.tabPageJog.Controls.Add(this.btnZeroX);
            this.tabPageJog.Controls.Add(this.machinePositionJog);
            this.tabPageJog.Controls.Add(this.jogControl);
            this.tabPageJog.Location = new System.Drawing.Point(4, 24);
            this.tabPageJog.Name = "tabPageJog";
            this.tabPageJog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJog.Size = new System.Drawing.Size(765, 242);
            this.tabPageJog.TabIndex = 2;
            this.tabPageJog.Text = "Jog";
            // 
            // btnZeroAll
            // 
            this.btnZeroAll.Image = ((System.Drawing.Image)(resources.GetObject("btnZeroAll.Image")));
            this.btnZeroAll.Location = new System.Drawing.Point(249, 123);
            this.btnZeroAll.Name = "btnZeroAll";
            this.btnZeroAll.Size = new System.Drawing.Size(64, 64);
            this.btnZeroAll.TabIndex = 4;
            this.btnZeroAll.UseVisualStyleBackColor = true;
            this.btnZeroAll.Click += new System.EventHandler(this.SetZeroForAxes);
            // 
            // btnZeroZ
            // 
            this.btnZeroZ.Image = ((System.Drawing.Image)(resources.GetObject("btnZeroZ.Image")));
            this.btnZeroZ.Location = new System.Drawing.Point(168, 123);
            this.btnZeroZ.Name = "btnZeroZ";
            this.btnZeroZ.Size = new System.Drawing.Size(64, 64);
            this.btnZeroZ.TabIndex = 3;
            this.btnZeroZ.UseVisualStyleBackColor = true;
            this.btnZeroZ.Click += new System.EventHandler(this.SetZeroForAxes);
            // 
            // btnZeroY
            // 
            this.btnZeroY.Image = ((System.Drawing.Image)(resources.GetObject("btnZeroY.Image")));
            this.btnZeroY.Location = new System.Drawing.Point(87, 123);
            this.btnZeroY.Name = "btnZeroY";
            this.btnZeroY.Size = new System.Drawing.Size(64, 64);
            this.btnZeroY.TabIndex = 2;
            this.btnZeroY.UseVisualStyleBackColor = true;
            this.btnZeroY.Click += new System.EventHandler(this.SetZeroForAxes);
            // 
            // btnZeroX
            // 
            this.btnZeroX.Image = ((System.Drawing.Image)(resources.GetObject("btnZeroX.Image")));
            this.btnZeroX.Location = new System.Drawing.Point(6, 123);
            this.btnZeroX.Name = "btnZeroX";
            this.btnZeroX.Size = new System.Drawing.Size(64, 64);
            this.btnZeroX.TabIndex = 1;
            this.btnZeroX.UseVisualStyleBackColor = true;
            this.btnZeroX.Click += new System.EventHandler(this.SetZeroForAxes);
            // 
            // machinePositionJog
            // 
            this.machinePositionJog.DisplayFeedbackUnit = GSendShared.FeedbackUnit.Mm;
            this.machinePositionJog.Location = new System.Drawing.Point(6, 6);
            this.machinePositionJog.Name = "machinePositionJog";
            this.machinePositionJog.Size = new System.Drawing.Size(314, 112);
            this.machinePositionJog.TabIndex = 0;
            // 
            // tabPageSpindle
            // 
            this.tabPageSpindle.BackColor = System.Drawing.Color.White;
            this.tabPageSpindle.Controls.Add(this.grpBoxSpindleSpeed);
            this.tabPageSpindle.Controls.Add(this.lblDelaySpindleStart);
            this.tabPageSpindle.Controls.Add(this.trackBarDelaySpindle);
            this.tabPageSpindle.Controls.Add(this.cbSoftStart);
            this.tabPageSpindle.Controls.Add(this.lblSpindleType);
            this.tabPageSpindle.Controls.Add(this.cmbSpindleType);
            this.tabPageSpindle.Location = new System.Drawing.Point(4, 24);
            this.tabPageSpindle.Name = "tabPageSpindle";
            this.tabPageSpindle.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSpindle.Size = new System.Drawing.Size(765, 242);
            this.tabPageSpindle.TabIndex = 4;
            this.tabPageSpindle.Text = "Spindle";
            // 
            // grpBoxSpindleSpeed
            // 
            this.grpBoxSpindleSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxSpindleSpeed.Controls.Add(this.cbSpindleCounterClockwise);
            this.grpBoxSpindleSpeed.Controls.Add(this.btnSpindleStop);
            this.grpBoxSpindleSpeed.Controls.Add(this.btnSpindleStart);
            this.grpBoxSpindleSpeed.Controls.Add(this.lblSpindleSpeed);
            this.grpBoxSpindleSpeed.Controls.Add(this.trackBarSpindleSpeed);
            this.grpBoxSpindleSpeed.Location = new System.Drawing.Point(571, 6);
            this.grpBoxSpindleSpeed.Name = "grpBoxSpindleSpeed";
            this.grpBoxSpindleSpeed.Size = new System.Drawing.Size(188, 227);
            this.grpBoxSpindleSpeed.TabIndex = 5;
            this.grpBoxSpindleSpeed.TabStop = false;
            this.grpBoxSpindleSpeed.Text = "groupBox1";
            // 
            // cbSpindleCounterClockwise
            // 
            this.cbSpindleCounterClockwise.Location = new System.Drawing.Point(70, 99);
            this.cbSpindleCounterClockwise.Name = "cbSpindleCounterClockwise";
            this.cbSpindleCounterClockwise.Size = new System.Drawing.Size(83, 61);
            this.cbSpindleCounterClockwise.TabIndex = 4;
            this.cbSpindleCounterClockwise.Text = "checkBox1";
            this.cbSpindleCounterClockwise.UseVisualStyleBackColor = true;
            // 
            // btnSpindleStop
            // 
            this.btnSpindleStop.Location = new System.Drawing.Point(70, 62);
            this.btnSpindleStop.Name = "btnSpindleStop";
            this.btnSpindleStop.Size = new System.Drawing.Size(75, 23);
            this.btnSpindleStop.TabIndex = 3;
            this.btnSpindleStop.Text = "button1";
            this.btnSpindleStop.UseVisualStyleBackColor = true;
            this.btnSpindleStop.Click += new System.EventHandler(this.btnSpindleStop_Click);
            // 
            // btnSpindleStart
            // 
            this.btnSpindleStart.Location = new System.Drawing.Point(70, 29);
            this.btnSpindleStart.Name = "btnSpindleStart";
            this.btnSpindleStart.Size = new System.Drawing.Size(75, 23);
            this.btnSpindleStart.TabIndex = 2;
            this.btnSpindleStart.Text = "button1";
            this.btnSpindleStart.UseVisualStyleBackColor = true;
            this.btnSpindleStart.Click += new System.EventHandler(this.btnSpindleStart_Click);
            // 
            // lblSpindleSpeed
            // 
            this.lblSpindleSpeed.AutoSize = true;
            this.lblSpindleSpeed.Location = new System.Drawing.Point(19, 201);
            this.lblSpindleSpeed.Name = "lblSpindleSpeed";
            this.lblSpindleSpeed.Size = new System.Drawing.Size(38, 15);
            this.lblSpindleSpeed.TabIndex = 1;
            this.lblSpindleSpeed.Text = "label1";
            // 
            // trackBarSpindleSpeed
            // 
            this.trackBarSpindleSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarSpindleSpeed.LargeChange = 200;
            this.trackBarSpindleSpeed.Location = new System.Drawing.Point(19, 29);
            this.trackBarSpindleSpeed.Maximum = 20000;
            this.trackBarSpindleSpeed.Name = "trackBarSpindleSpeed";
            this.trackBarSpindleSpeed.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarSpindleSpeed.Size = new System.Drawing.Size(45, 165);
            this.trackBarSpindleSpeed.SmallChange = 50;
            this.trackBarSpindleSpeed.TabIndex = 0;
            this.trackBarSpindleSpeed.TickFrequency = 300;
            this.trackBarSpindleSpeed.ValueChanged += new System.EventHandler(this.trackBarSpindleSpeed_ValueChanged);
            // 
            // lblDelaySpindleStart
            // 
            this.lblDelaySpindleStart.AutoSize = true;
            this.lblDelaySpindleStart.Location = new System.Drawing.Point(211, 72);
            this.lblDelaySpindleStart.Name = "lblDelaySpindleStart";
            this.lblDelaySpindleStart.Size = new System.Drawing.Size(38, 15);
            this.lblDelaySpindleStart.TabIndex = 4;
            this.lblDelaySpindleStart.Text = "label1";
            // 
            // trackBarDelaySpindle
            // 
            this.trackBarDelaySpindle.Location = new System.Drawing.Point(211, 24);
            this.trackBarDelaySpindle.Maximum = 120;
            this.trackBarDelaySpindle.Name = "trackBarDelaySpindle";
            this.trackBarDelaySpindle.Size = new System.Drawing.Size(240, 45);
            this.trackBarDelaySpindle.TabIndex = 3;
            this.trackBarDelaySpindle.TickFrequency = 10;
            this.trackBarDelaySpindle.Value = 30;
            // 
            // cbSoftStart
            // 
            this.cbSoftStart.AutoSize = true;
            this.cbSoftStart.Location = new System.Drawing.Point(6, 77);
            this.cbSoftStart.Name = "cbSoftStart";
            this.cbSoftStart.Size = new System.Drawing.Size(83, 19);
            this.cbSoftStart.TabIndex = 2;
            this.cbSoftStart.Text = "checkBox1";
            this.cbSoftStart.UseVisualStyleBackColor = true;
            // 
            // lblSpindleType
            // 
            this.lblSpindleType.AutoSize = true;
            this.lblSpindleType.Location = new System.Drawing.Point(6, 6);
            this.lblSpindleType.Name = "lblSpindleType";
            this.lblSpindleType.Size = new System.Drawing.Size(38, 15);
            this.lblSpindleType.TabIndex = 0;
            this.lblSpindleType.Text = "label1";
            // 
            // cmbSpindleType
            // 
            this.cmbSpindleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSpindleType.FormattingEnabled = true;
            this.cmbSpindleType.Location = new System.Drawing.Point(6, 24);
            this.cmbSpindleType.Name = "cmbSpindleType";
            this.cmbSpindleType.Size = new System.Drawing.Size(164, 23);
            this.cmbSpindleType.TabIndex = 1;
            this.cmbSpindleType.SelectedIndexChanged += new System.EventHandler(this.cmbSpindleType_SelectedIndexChanged);
            // 
            // tabPageServiceSchedule
            // 
            this.tabPageServiceSchedule.BackColor = System.Drawing.Color.White;
            this.tabPageServiceSchedule.Controls.Add(this.lvServices);
            this.tabPageServiceSchedule.Controls.Add(this.btnServiceRefresh);
            this.tabPageServiceSchedule.Controls.Add(this.lblSpindleHoursRemaining);
            this.tabPageServiceSchedule.Controls.Add(this.lblServiceDate);
            this.tabPageServiceSchedule.Controls.Add(this.btnServiceReset);
            this.tabPageServiceSchedule.Controls.Add(this.lblNextService);
            this.tabPageServiceSchedule.Controls.Add(this.lblSpindleHours);
            this.tabPageServiceSchedule.Controls.Add(this.trackBarServiceSpindleHours);
            this.tabPageServiceSchedule.Controls.Add(this.cbMaintainServiceSchedule);
            this.tabPageServiceSchedule.Controls.Add(this.trackBarServiceWeeks);
            this.tabPageServiceSchedule.Controls.Add(this.lblServiceSchedule);
            this.tabPageServiceSchedule.Location = new System.Drawing.Point(4, 24);
            this.tabPageServiceSchedule.Name = "tabPageServiceSchedule";
            this.tabPageServiceSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageServiceSchedule.Size = new System.Drawing.Size(765, 242);
            this.tabPageServiceSchedule.TabIndex = 6;
            this.tabPageServiceSchedule.Text = "Service Scgedule";
            // 
            // lvServices
            // 
            this.lvServices.AllowColumnReorder = true;
            this.lvServices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnServiceHeaderDateTime,
            this.columnServiceHeaderServiceType,
            this.columnServiceHeaderSpindleHours});
            this.lvServices.Location = new System.Drawing.Point(332, 126);
            this.lvServices.MultiSelect = false;
            this.lvServices.Name = "lvServices";
            this.lvServices.OwnerDraw = true;
            this.lvServices.SaveName = "";
            this.lvServices.ShowItemToolTips = true;
            this.lvServices.ShowToolTip = false;
            this.lvServices.Size = new System.Drawing.Size(427, 110);
            this.lvServices.TabIndex = 11;
            this.lvServices.UseCompatibleStateImageBehavior = false;
            this.lvServices.View = System.Windows.Forms.View.Details;
            // 
            // columnServiceHeaderDateTime
            // 
            this.columnServiceHeaderDateTime.Width = 130;
            // 
            // columnServiceHeaderServiceType
            // 
            this.columnServiceHeaderServiceType.Width = 80;
            // 
            // columnServiceHeaderSpindleHours
            // 
            this.columnServiceHeaderSpindleHours.Width = 180;
            // 
            // btnServiceRefresh
            // 
            this.btnServiceRefresh.Location = new System.Drawing.Point(657, 89);
            this.btnServiceRefresh.Name = "btnServiceRefresh";
            this.btnServiceRefresh.Size = new System.Drawing.Size(102, 23);
            this.btnServiceRefresh.TabIndex = 10;
            this.btnServiceRefresh.Text = "button1";
            this.btnServiceRefresh.UseVisualStyleBackColor = true;
            this.btnServiceRefresh.Click += new System.EventHandler(this.btnServiceRefresh_Click);
            // 
            // lblSpindleHoursRemaining
            // 
            this.lblSpindleHoursRemaining.AutoSize = true;
            this.lblSpindleHoursRemaining.Location = new System.Drawing.Point(332, 73);
            this.lblSpindleHoursRemaining.Name = "lblSpindleHoursRemaining";
            this.lblSpindleHoursRemaining.Size = new System.Drawing.Size(38, 15);
            this.lblSpindleHoursRemaining.TabIndex = 8;
            this.lblSpindleHoursRemaining.Text = "label1";
            // 
            // lblServiceDate
            // 
            this.lblServiceDate.AutoSize = true;
            this.lblServiceDate.Location = new System.Drawing.Point(332, 43);
            this.lblServiceDate.Name = "lblServiceDate";
            this.lblServiceDate.Size = new System.Drawing.Size(38, 15);
            this.lblServiceDate.TabIndex = 7;
            this.lblServiceDate.Text = "label1";
            // 
            // btnServiceReset
            // 
            this.btnServiceReset.Location = new System.Drawing.Point(657, 12);
            this.btnServiceReset.Name = "btnServiceReset";
            this.btnServiceReset.Size = new System.Drawing.Size(102, 23);
            this.btnServiceReset.TabIndex = 6;
            this.btnServiceReset.Text = "button1";
            this.btnServiceReset.UseVisualStyleBackColor = true;
            this.btnServiceReset.Click += new System.EventHandler(this.btnServiceReset_Click);
            // 
            // lblNextService
            // 
            this.lblNextService.AutoSize = true;
            this.lblNextService.Location = new System.Drawing.Point(332, 16);
            this.lblNextService.Name = "lblNextService";
            this.lblNextService.Size = new System.Drawing.Size(38, 15);
            this.lblNextService.TabIndex = 5;
            this.lblNextService.Text = "label1";
            // 
            // lblSpindleHours
            // 
            this.lblSpindleHours.AutoSize = true;
            this.lblSpindleHours.Location = new System.Drawing.Point(18, 168);
            this.lblSpindleHours.Name = "lblSpindleHours";
            this.lblSpindleHours.Size = new System.Drawing.Size(38, 15);
            this.lblSpindleHours.TabIndex = 4;
            this.lblSpindleHours.Text = "label1";
            // 
            // trackBarServiceSpindleHours
            // 
            this.trackBarServiceSpindleHours.Location = new System.Drawing.Point(18, 120);
            this.trackBarServiceSpindleHours.Maximum = 500;
            this.trackBarServiceSpindleHours.Minimum = 5;
            this.trackBarServiceSpindleHours.Name = "trackBarServiceSpindleHours";
            this.trackBarServiceSpindleHours.Size = new System.Drawing.Size(286, 45);
            this.trackBarServiceSpindleHours.TabIndex = 3;
            this.trackBarServiceSpindleHours.TickFrequency = 20;
            this.trackBarServiceSpindleHours.Value = 5;
            // 
            // cbMaintainServiceSchedule
            // 
            this.cbMaintainServiceSchedule.AutoSize = true;
            this.cbMaintainServiceSchedule.Location = new System.Drawing.Point(6, 6);
            this.cbMaintainServiceSchedule.Name = "cbMaintainServiceSchedule";
            this.cbMaintainServiceSchedule.Size = new System.Drawing.Size(83, 19);
            this.cbMaintainServiceSchedule.TabIndex = 2;
            this.cbMaintainServiceSchedule.Text = "checkBox1";
            this.cbMaintainServiceSchedule.UseVisualStyleBackColor = true;
            // 
            // trackBarServiceWeeks
            // 
            this.trackBarServiceWeeks.LargeChange = 1;
            this.trackBarServiceWeeks.Location = new System.Drawing.Point(18, 43);
            this.trackBarServiceWeeks.Maximum = 52;
            this.trackBarServiceWeeks.Minimum = 1;
            this.trackBarServiceWeeks.Name = "trackBarServiceWeeks";
            this.trackBarServiceWeeks.Size = new System.Drawing.Size(286, 45);
            this.trackBarServiceWeeks.TabIndex = 1;
            this.trackBarServiceWeeks.TickFrequency = 3;
            this.trackBarServiceWeeks.Value = 1;
            // 
            // lblServiceSchedule
            // 
            this.lblServiceSchedule.AutoSize = true;
            this.lblServiceSchedule.Location = new System.Drawing.Point(18, 91);
            this.lblServiceSchedule.Name = "lblServiceSchedule";
            this.lblServiceSchedule.Size = new System.Drawing.Size(38, 15);
            this.lblServiceSchedule.TabIndex = 0;
            this.lblServiceSchedule.Text = "label1";
            // 
            // tabPageMachineSettings
            // 
            this.tabPageMachineSettings.BackColor = System.Drawing.Color.White;
            this.tabPageMachineSettings.Controls.Add(this.btnApplyGrblUpdates);
            this.tabPageMachineSettings.Controls.Add(this.txtGrblUpdates);
            this.tabPageMachineSettings.Controls.Add(this.lblPropertyDesc);
            this.tabPageMachineSettings.Controls.Add(this.lblPropertyHeader);
            this.tabPageMachineSettings.Controls.Add(this.propertyGridGrblSettings);
            this.tabPageMachineSettings.Location = new System.Drawing.Point(4, 24);
            this.tabPageMachineSettings.Name = "tabPageMachineSettings";
            this.tabPageMachineSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMachineSettings.Size = new System.Drawing.Size(765, 242);
            this.tabPageMachineSettings.TabIndex = 3;
            this.tabPageMachineSettings.Text = "Machine Settings";
            // 
            // btnApplyGrblUpdates
            // 
            this.btnApplyGrblUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyGrblUpdates.Enabled = false;
            this.btnApplyGrblUpdates.Location = new System.Drawing.Point(662, 6);
            this.btnApplyGrblUpdates.Name = "btnApplyGrblUpdates";
            this.btnApplyGrblUpdates.Size = new System.Drawing.Size(75, 23);
            this.btnApplyGrblUpdates.TabIndex = 4;
            this.btnApplyGrblUpdates.Text = "Apply";
            this.btnApplyGrblUpdates.UseVisualStyleBackColor = true;
            this.btnApplyGrblUpdates.Click += new System.EventHandler(this.btnApplyGrblUpdates_Click);
            // 
            // txtGrblUpdates
            // 
            this.txtGrblUpdates.AcceptsReturn = true;
            this.txtGrblUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGrblUpdates.Location = new System.Drawing.Point(380, 6);
            this.txtGrblUpdates.Multiline = true;
            this.txtGrblUpdates.Name = "txtGrblUpdates";
            this.txtGrblUpdates.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGrblUpdates.Size = new System.Drawing.Size(276, 130);
            this.txtGrblUpdates.TabIndex = 1;
            this.txtGrblUpdates.TextChanged += new System.EventHandler(this.txtGrblUpdates_TextChanged);
            // 
            // lblPropertyDesc
            // 
            this.lblPropertyDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPropertyDesc.Location = new System.Drawing.Point(380, 154);
            this.lblPropertyDesc.Name = "lblPropertyDesc";
            this.lblPropertyDesc.Size = new System.Drawing.Size(383, 82);
            this.lblPropertyDesc.TabIndex = 3;
            this.lblPropertyDesc.Text = "label2";
            // 
            // lblPropertyHeader
            // 
            this.lblPropertyHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPropertyHeader.AutoSize = true;
            this.lblPropertyHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPropertyHeader.Location = new System.Drawing.Point(380, 139);
            this.lblPropertyHeader.Name = "lblPropertyHeader";
            this.lblPropertyHeader.Size = new System.Drawing.Size(40, 15);
            this.lblPropertyHeader.TabIndex = 2;
            this.lblPropertyHeader.Text = "label1";
            // 
            // propertyGridGrblSettings
            // 
            this.propertyGridGrblSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGridGrblSettings.HelpVisible = false;
            this.propertyGridGrblSettings.Location = new System.Drawing.Point(6, 6);
            this.propertyGridGrblSettings.Name = "propertyGridGrblSettings";
            this.propertyGridGrblSettings.Size = new System.Drawing.Size(368, 230);
            this.propertyGridGrblSettings.TabIndex = 0;
            this.propertyGridGrblSettings.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridGrblSettings_PropertyValueChanged);
            this.propertyGridGrblSettings.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.propertyGridGrblSettings_SelectedGridItemChanged);
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.Color.White;
            this.tabPageSettings.Controls.Add(this.lblLayerHeightMeasure);
            this.tabPageSettings.Controls.Add(this.numericLayerHeight);
            this.tabPageSettings.Controls.Add(this.cbLayerHeightWarning);
            this.tabPageSettings.Controls.Add(this.grpFeedDisplay);
            this.tabPageSettings.Controls.Add(this.grpDisplayUnits);
            this.tabPageSettings.Controls.Add(this.cbCorrectMode);
            this.tabPageSettings.Controls.Add(this.cbFloodCoolant);
            this.tabPageSettings.Controls.Add(this.cbMistCoolant);
            this.tabPageSettings.Controls.Add(this.cbToolChanger);
            this.tabPageSettings.Controls.Add(this.cbLimitSwitches);
            this.tabPageSettings.Controls.Add(this.probingCommand1);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 24);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(765, 242);
            this.tabPageSettings.TabIndex = 8;
            this.tabPageSettings.Text = "Settings";
            // 
            // lblLayerHeightMeasure
            // 
            this.lblLayerHeightMeasure.AutoSize = true;
            this.lblLayerHeightMeasure.Location = new System.Drawing.Point(299, 132);
            this.lblLayerHeightMeasure.Name = "lblLayerHeightMeasure";
            this.lblLayerHeightMeasure.Size = new System.Drawing.Size(38, 15);
            this.lblLayerHeightMeasure.TabIndex = 10;
            this.lblLayerHeightMeasure.Text = "label1";
            // 
            // numericLayerHeight
            // 
            this.numericLayerHeight.DecimalPlaces = 1;
            this.numericLayerHeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericLayerHeight.Location = new System.Drawing.Point(235, 130);
            this.numericLayerHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericLayerHeight.Name = "numericLayerHeight";
            this.numericLayerHeight.Size = new System.Drawing.Size(58, 23);
            this.numericLayerHeight.TabIndex = 9;
            this.numericLayerHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // cbLayerHeightWarning
            // 
            this.cbLayerHeightWarning.AutoSize = true;
            this.cbLayerHeightWarning.Location = new System.Drawing.Point(6, 131);
            this.cbLayerHeightWarning.Name = "cbLayerHeightWarning";
            this.cbLayerHeightWarning.Size = new System.Drawing.Size(148, 19);
            this.cbLayerHeightWarning.TabIndex = 8;
            this.cbLayerHeightWarning.Text = "cbLayerHeightWarning";
            this.cbLayerHeightWarning.UseVisualStyleBackColor = true;
            // 
            // grpFeedDisplay
            // 
            this.grpFeedDisplay.Controls.Add(this.rbFeedDisplayInchMin);
            this.grpFeedDisplay.Controls.Add(this.rbFeedDisplayInchSec);
            this.grpFeedDisplay.Controls.Add(this.rbFeedDisplayMmSec);
            this.grpFeedDisplay.Controls.Add(this.rbFeedDisplayMmMin);
            this.grpFeedDisplay.Location = new System.Drawing.Point(274, 154);
            this.grpFeedDisplay.Name = "grpFeedDisplay";
            this.grpFeedDisplay.Size = new System.Drawing.Size(221, 82);
            this.grpFeedDisplay.TabIndex = 6;
            this.grpFeedDisplay.TabStop = false;
            this.grpFeedDisplay.Text = "groupBox1";
            // 
            // rbFeedDisplayInchMin
            // 
            this.rbFeedDisplayInchMin.AutoSize = true;
            this.rbFeedDisplayInchMin.Location = new System.Drawing.Point(121, 22);
            this.rbFeedDisplayInchMin.Name = "rbFeedDisplayInchMin";
            this.rbFeedDisplayInchMin.Size = new System.Drawing.Size(94, 19);
            this.rbFeedDisplayInchMin.TabIndex = 2;
            this.rbFeedDisplayInchMin.TabStop = true;
            this.rbFeedDisplayInchMin.Text = "radioButton4";
            this.rbFeedDisplayInchMin.UseVisualStyleBackColor = true;
            // 
            // rbFeedDisplayInchSec
            // 
            this.rbFeedDisplayInchSec.AutoSize = true;
            this.rbFeedDisplayInchSec.Location = new System.Drawing.Point(121, 47);
            this.rbFeedDisplayInchSec.Name = "rbFeedDisplayInchSec";
            this.rbFeedDisplayInchSec.Size = new System.Drawing.Size(94, 19);
            this.rbFeedDisplayInchSec.TabIndex = 3;
            this.rbFeedDisplayInchSec.TabStop = true;
            this.rbFeedDisplayInchSec.Text = "radioButton3";
            this.rbFeedDisplayInchSec.UseVisualStyleBackColor = true;
            // 
            // rbFeedDisplayMmSec
            // 
            this.rbFeedDisplayMmSec.AutoSize = true;
            this.rbFeedDisplayMmSec.Location = new System.Drawing.Point(6, 47);
            this.rbFeedDisplayMmSec.Name = "rbFeedDisplayMmSec";
            this.rbFeedDisplayMmSec.Size = new System.Drawing.Size(94, 19);
            this.rbFeedDisplayMmSec.TabIndex = 1;
            this.rbFeedDisplayMmSec.TabStop = true;
            this.rbFeedDisplayMmSec.Text = "radioButton2";
            this.rbFeedDisplayMmSec.UseVisualStyleBackColor = true;
            // 
            // rbFeedDisplayMmMin
            // 
            this.rbFeedDisplayMmMin.AutoSize = true;
            this.rbFeedDisplayMmMin.Location = new System.Drawing.Point(6, 22);
            this.rbFeedDisplayMmMin.Name = "rbFeedDisplayMmMin";
            this.rbFeedDisplayMmMin.Size = new System.Drawing.Size(94, 19);
            this.rbFeedDisplayMmMin.TabIndex = 0;
            this.rbFeedDisplayMmMin.TabStop = true;
            this.rbFeedDisplayMmMin.Text = "radioButton1";
            this.rbFeedDisplayMmMin.UseVisualStyleBackColor = true;
            // 
            // grpDisplayUnits
            // 
            this.grpDisplayUnits.Controls.Add(this.cbAutoSelectFeedbackUnit);
            this.grpDisplayUnits.Controls.Add(this.rbFeedbackInch);
            this.grpDisplayUnits.Controls.Add(this.rbFeedbackMm);
            this.grpDisplayUnits.Location = new System.Drawing.Point(6, 154);
            this.grpDisplayUnits.Name = "grpDisplayUnits";
            this.grpDisplayUnits.Size = new System.Drawing.Size(262, 82);
            this.grpDisplayUnits.TabIndex = 5;
            this.grpDisplayUnits.TabStop = false;
            this.grpDisplayUnits.Text = "groupBox1";
            // 
            // cbAutoSelectFeedbackUnit
            // 
            this.cbAutoSelectFeedbackUnit.Location = new System.Drawing.Point(121, 22);
            this.cbAutoSelectFeedbackUnit.Name = "cbAutoSelectFeedbackUnit";
            this.cbAutoSelectFeedbackUnit.Size = new System.Drawing.Size(135, 43);
            this.cbAutoSelectFeedbackUnit.TabIndex = 2;
            this.cbAutoSelectFeedbackUnit.Text = "Automatically select from G Code file";
            this.cbAutoSelectFeedbackUnit.UseVisualStyleBackColor = true;
            // 
            // rbFeedbackInch
            // 
            this.rbFeedbackInch.AutoSize = true;
            this.rbFeedbackInch.Location = new System.Drawing.Point(6, 47);
            this.rbFeedbackInch.Name = "rbFeedbackInch";
            this.rbFeedbackInch.Size = new System.Drawing.Size(94, 19);
            this.rbFeedbackInch.TabIndex = 1;
            this.rbFeedbackInch.TabStop = true;
            this.rbFeedbackInch.Text = "radioButton2";
            this.rbFeedbackInch.UseVisualStyleBackColor = true;
            // 
            // rbFeedbackMm
            // 
            this.rbFeedbackMm.AutoSize = true;
            this.rbFeedbackMm.Location = new System.Drawing.Point(6, 22);
            this.rbFeedbackMm.Name = "rbFeedbackMm";
            this.rbFeedbackMm.Size = new System.Drawing.Size(94, 19);
            this.rbFeedbackMm.TabIndex = 0;
            this.rbFeedbackMm.TabStop = true;
            this.rbFeedbackMm.Text = "radioButton1";
            this.rbFeedbackMm.UseVisualStyleBackColor = true;
            // 
            // cbCorrectMode
            // 
            this.cbCorrectMode.AutoSize = true;
            this.cbCorrectMode.Location = new System.Drawing.Point(6, 106);
            this.cbCorrectMode.Name = "cbCorrectMode";
            this.cbCorrectMode.Size = new System.Drawing.Size(83, 19);
            this.cbCorrectMode.TabIndex = 4;
            this.cbCorrectMode.Text = "checkBox3";
            this.cbCorrectMode.UseVisualStyleBackColor = true;
            // 
            // cbFloodCoolant
            // 
            this.cbFloodCoolant.AutoSize = true;
            this.cbFloodCoolant.Location = new System.Drawing.Point(6, 81);
            this.cbFloodCoolant.Name = "cbFloodCoolant";
            this.cbFloodCoolant.Size = new System.Drawing.Size(83, 19);
            this.cbFloodCoolant.TabIndex = 3;
            this.cbFloodCoolant.Text = "checkBox2";
            this.cbFloodCoolant.UseVisualStyleBackColor = true;
            // 
            // cbMistCoolant
            // 
            this.cbMistCoolant.AutoSize = true;
            this.cbMistCoolant.Location = new System.Drawing.Point(6, 56);
            this.cbMistCoolant.Name = "cbMistCoolant";
            this.cbMistCoolant.Size = new System.Drawing.Size(83, 19);
            this.cbMistCoolant.TabIndex = 2;
            this.cbMistCoolant.Text = "checkBox1";
            this.cbMistCoolant.UseVisualStyleBackColor = true;
            // 
            // cbToolChanger
            // 
            this.cbToolChanger.AutoSize = true;
            this.cbToolChanger.Location = new System.Drawing.Point(6, 31);
            this.cbToolChanger.Name = "cbToolChanger";
            this.cbToolChanger.Size = new System.Drawing.Size(83, 19);
            this.cbToolChanger.TabIndex = 1;
            this.cbToolChanger.Text = "checkBox2";
            this.cbToolChanger.UseVisualStyleBackColor = true;
            // 
            // cbLimitSwitches
            // 
            this.cbLimitSwitches.AutoSize = true;
            this.cbLimitSwitches.Location = new System.Drawing.Point(6, 6);
            this.cbLimitSwitches.Name = "cbLimitSwitches";
            this.cbLimitSwitches.Size = new System.Drawing.Size(83, 19);
            this.cbLimitSwitches.TabIndex = 0;
            this.cbLimitSwitches.Text = "checkBox1";
            this.cbLimitSwitches.UseVisualStyleBackColor = true;
            // 
            // probingCommand1
            // 
            this.probingCommand1.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            this.probingCommand1.Location = new System.Drawing.Point(501, 6);
            this.probingCommand1.MinimumSize = new System.Drawing.Size(220, 172);
            this.probingCommand1.Name = "probingCommand1";
            this.probingCommand1.Size = new System.Drawing.Size(258, 230);
            this.probingCommand1.TabIndex = 7;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMachine,
            this.mnuView,
            this.mnuAction,
            this.mnuOptions,
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(796, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuMachine
            // 
            this.mnuMachine.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMachineLoadGCode,
            this.mnuMachineClearGCode,
            this.toolStripMenuItem2,
            this.mnuMachineRename,
            this.toolStripMenuItem7,
            this.mnuMachineClose});
            this.mnuMachine.Name = "mnuMachine";
            this.mnuMachine.Size = new System.Drawing.Size(65, 20);
            this.mnuMachine.Text = "&Machine";
            // 
            // mnuMachineLoadGCode
            // 
            this.mnuMachineLoadGCode.Name = "mnuMachineLoadGCode";
            this.mnuMachineLoadGCode.Size = new System.Drawing.Size(143, 22);
            this.mnuMachineLoadGCode.Text = "Load G Code";
            this.mnuMachineLoadGCode.Click += new System.EventHandler(this.mnuMachineLoadGCode_Click);
            // 
            // mnuMachineClearGCode
            // 
            this.mnuMachineClearGCode.Name = "mnuMachineClearGCode";
            this.mnuMachineClearGCode.Size = new System.Drawing.Size(143, 22);
            this.mnuMachineClearGCode.Text = "Clear G Code";
            this.mnuMachineClearGCode.Click += new System.EventHandler(this.mnuMachineClearGCode_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuMachineRename
            // 
            this.mnuMachineRename.Name = "mnuMachineRename";
            this.mnuMachineRename.Size = new System.Drawing.Size(143, 22);
            this.mnuMachineRename.Text = "Rename";
            this.mnuMachineRename.Click += new System.EventHandler(this.mnuMachineRename_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuMachineClose
            // 
            this.mnuMachineClose.Name = "mnuMachineClose";
            this.mnuMachineClose.Size = new System.Drawing.Size(143, 22);
            this.mnuMachineClose.Text = "Close";
            this.mnuMachineClose.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewGeneral,
            this.mnuViewOverrides,
            this.mnuViewJog,
            this.mnuViewSpindle,
            this.mnuViewServiceSchedule,
            this.mnuViewMachineSettings,
            this.mnuViewSettings,
            this.toolStripMenuItem1,
            this.mnuViewConsole});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 20);
            this.mnuView.Text = "&View";
            // 
            // mnuViewGeneral
            // 
            this.mnuViewGeneral.Name = "mnuViewGeneral";
            this.mnuViewGeneral.Size = new System.Drawing.Size(165, 22);
            this.mnuViewGeneral.Text = "General";
            // 
            // mnuViewOverrides
            // 
            this.mnuViewOverrides.Name = "mnuViewOverrides";
            this.mnuViewOverrides.Size = new System.Drawing.Size(165, 22);
            this.mnuViewOverrides.Text = "Overrides";
            // 
            // mnuViewJog
            // 
            this.mnuViewJog.Name = "mnuViewJog";
            this.mnuViewJog.Size = new System.Drawing.Size(165, 22);
            this.mnuViewJog.Text = "Jog";
            // 
            // mnuViewSpindle
            // 
            this.mnuViewSpindle.Name = "mnuViewSpindle";
            this.mnuViewSpindle.Size = new System.Drawing.Size(165, 22);
            this.mnuViewSpindle.Text = "Spindle";
            // 
            // mnuViewServiceSchedule
            // 
            this.mnuViewServiceSchedule.Name = "mnuViewServiceSchedule";
            this.mnuViewServiceSchedule.Size = new System.Drawing.Size(165, 22);
            this.mnuViewServiceSchedule.Text = "Service Schedule";
            // 
            // mnuViewMachineSettings
            // 
            this.mnuViewMachineSettings.Name = "mnuViewMachineSettings";
            this.mnuViewMachineSettings.Size = new System.Drawing.Size(165, 22);
            this.mnuViewMachineSettings.Text = "Machine Settings";
            // 
            // mnuViewSettings
            // 
            this.mnuViewSettings.Name = "mnuViewSettings";
            this.mnuViewSettings.Size = new System.Drawing.Size(165, 22);
            this.mnuViewSettings.Text = "Settings";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(162, 6);
            // 
            // mnuViewConsole
            // 
            this.mnuViewConsole.Name = "mnuViewConsole";
            this.mnuViewConsole.Size = new System.Drawing.Size(165, 22);
            this.mnuViewConsole.Text = "Console";
            this.mnuViewConsole.Click += new System.EventHandler(this.consoleToolStripMenuItem_Click);
            // 
            // mnuAction
            // 
            this.mnuAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuActionSaveConfig,
            this.toolStripMenuItem3,
            this.mnuActionConnect,
            this.mnuActionDisconnect,
            this.toolStripMenuItem6,
            this.mnuActionClearAlarm,
            this.toolStripMenuItem4,
            this.mnuActionHome,
            this.mnuActionProbe,
            this.toolStripMenuItem5,
            this.mnuActionRun,
            this.mnuActionPause,
            this.mnuActionStop});
            this.mnuAction.Name = "mnuAction";
            this.mnuAction.Size = new System.Drawing.Size(54, 20);
            this.mnuAction.Text = "Action";
            // 
            // mnuActionSaveConfig
            // 
            this.mnuActionSaveConfig.Name = "mnuActionSaveConfig";
            this.mnuActionSaveConfig.Size = new System.Drawing.Size(175, 22);
            this.mnuActionSaveConfig.Text = "Save Configuration";
            this.mnuActionSaveConfig.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(172, 6);
            // 
            // mnuActionConnect
            // 
            this.mnuActionConnect.Name = "mnuActionConnect";
            this.mnuActionConnect.Size = new System.Drawing.Size(175, 22);
            this.mnuActionConnect.Text = "Connect";
            this.mnuActionConnect.Click += new System.EventHandler(this.toolStripButtonConnect_Click);
            // 
            // mnuActionDisconnect
            // 
            this.mnuActionDisconnect.Name = "mnuActionDisconnect";
            this.mnuActionDisconnect.Size = new System.Drawing.Size(175, 22);
            this.mnuActionDisconnect.Text = "Disconnect";
            this.mnuActionDisconnect.Click += new System.EventHandler(this.toolStripButtonDisconnect_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(172, 6);
            // 
            // mnuActionClearAlarm
            // 
            this.mnuActionClearAlarm.Name = "mnuActionClearAlarm";
            this.mnuActionClearAlarm.Size = new System.Drawing.Size(175, 22);
            this.mnuActionClearAlarm.Text = "Clear Alarm";
            this.mnuActionClearAlarm.Click += new System.EventHandler(this.toolStripButtonClearAlarm_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(172, 6);
            // 
            // mnuActionHome
            // 
            this.mnuActionHome.Name = "mnuActionHome";
            this.mnuActionHome.Size = new System.Drawing.Size(175, 22);
            this.mnuActionHome.Text = "Home";
            this.mnuActionHome.Click += new System.EventHandler(this.toolStripButtonHome_Click);
            // 
            // mnuActionProbe
            // 
            this.mnuActionProbe.Name = "mnuActionProbe";
            this.mnuActionProbe.Size = new System.Drawing.Size(175, 22);
            this.mnuActionProbe.Text = "Probe";
            this.mnuActionProbe.Click += new System.EventHandler(this.toolStripButtonProbe_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(172, 6);
            // 
            // mnuActionRun
            // 
            this.mnuActionRun.Name = "mnuActionRun";
            this.mnuActionRun.Size = new System.Drawing.Size(175, 22);
            this.mnuActionRun.Text = "Run";
            this.mnuActionRun.Click += new System.EventHandler(this.toolStripButtonResume_Click);
            // 
            // mnuActionPause
            // 
            this.mnuActionPause.Name = "mnuActionPause";
            this.mnuActionPause.Size = new System.Drawing.Size(175, 22);
            this.mnuActionPause.Text = "Pause";
            this.mnuActionPause.Click += new System.EventHandler(this.toolStripButtonPause_Click);
            // 
            // mnuActionStop
            // 
            this.mnuActionStop.Name = "mnuActionStop";
            this.mnuActionStop.Size = new System.Drawing.Size(175, 22);
            this.mnuActionStop.Text = "Stop";
            this.mnuActionStop.Click += new System.EventHandler(this.toolStripButtonStop_Click);
            // 
            // mnuOptions
            // 
            this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptionsShortcutKeys});
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(61, 20);
            this.mnuOptions.Text = "Options";
            // 
            // mnuOptionsShortcutKeys
            // 
            this.mnuOptionsShortcutKeys.Name = "mnuOptionsShortcutKeys";
            this.mnuOptionsShortcutKeys.Size = new System.Drawing.Size(146, 22);
            this.mnuOptionsShortcutKeys.Text = "Shortcut Keys";
            this.mnuOptionsShortcutKeys.Click += new System.EventHandler(this.mnuOptionsShortcutKeys_Click);
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
            this.mnuHelpAbout.Size = new System.Drawing.Size(180, 22);
            this.mnuHelpAbout.Text = "About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // warningsAndErrors
            // 
            this.warningsAndErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.warningsAndErrors.Location = new System.Drawing.Point(12, 86);
            this.warningsAndErrors.Margin = new System.Windows.Forms.Padding(0);
            this.warningsAndErrors.MaximumSize = new System.Drawing.Size(2048, 48);
            this.warningsAndErrors.MinimumSize = new System.Drawing.Size(204, 27);
            this.warningsAndErrors.Name = "warningsAndErrors";
            this.warningsAndErrors.Size = new System.Drawing.Size(772, 28);
            this.warningsAndErrors.TabIndex = 2;
            this.warningsAndErrors.OnUpdate += new System.EventHandler(this.warningsAndErrors_OnUpdate);
            this.warningsAndErrors.VisibleChanged += new System.EventHandler(this.WarningContainer_VisibleChanged);
            this.warningsAndErrors.Resize += new System.EventHandler(this.WarningContainer_VisibleChanged);
            // 
            // tabControlSecondary
            // 
            this.tabControlSecondary.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControlSecondary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSecondary.Controls.Add(this.tabPageConsole);
            this.tabControlSecondary.Controls.Add(this.tabPageGCode);
            this.tabControlSecondary.Controls.Add(this.tabPage2DView);
            this.tabControlSecondary.Controls.Add(this.tabPageHeartbeat);
            this.tabControlSecondary.Location = new System.Drawing.Point(12, 411);
            this.tabControlSecondary.Name = "tabControlSecondary";
            this.tabControlSecondary.SelectedIndex = 0;
            this.tabControlSecondary.Size = new System.Drawing.Size(773, 171);
            this.tabControlSecondary.TabIndex = 3;
            // 
            // tabPageConsole
            // 
            this.tabPageConsole.Controls.Add(this.btnGrblCommandSend);
            this.tabPageConsole.Controls.Add(this.btnGrblCommandClear);
            this.tabPageConsole.Controls.Add(this.txtUserGrblCommand);
            this.tabPageConsole.Controls.Add(this.textBoxConsoleText);
            this.tabPageConsole.Location = new System.Drawing.Point(4, 4);
            this.tabPageConsole.Name = "tabPageConsole";
            this.tabPageConsole.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConsole.Size = new System.Drawing.Size(765, 143);
            this.tabPageConsole.TabIndex = 0;
            this.tabPageConsole.Text = "tabPageConsole";
            this.tabPageConsole.UseVisualStyleBackColor = true;
            // 
            // btnGrblCommandSend
            // 
            this.btnGrblCommandSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrblCommandSend.Location = new System.Drawing.Point(603, 113);
            this.btnGrblCommandSend.Name = "btnGrblCommandSend";
            this.btnGrblCommandSend.Size = new System.Drawing.Size(75, 23);
            this.btnGrblCommandSend.TabIndex = 2;
            this.btnGrblCommandSend.Text = "button2";
            this.btnGrblCommandSend.UseVisualStyleBackColor = true;
            this.btnGrblCommandSend.Click += new System.EventHandler(this.btnGrblCommandSend_Click);
            // 
            // btnGrblCommandClear
            // 
            this.btnGrblCommandClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrblCommandClear.Location = new System.Drawing.Point(684, 113);
            this.btnGrblCommandClear.Name = "btnGrblCommandClear";
            this.btnGrblCommandClear.Size = new System.Drawing.Size(75, 23);
            this.btnGrblCommandClear.TabIndex = 3;
            this.btnGrblCommandClear.Text = "button1";
            this.btnGrblCommandClear.UseVisualStyleBackColor = true;
            this.btnGrblCommandClear.Click += new System.EventHandler(this.btnGrblCommandClear_Click);
            // 
            // txtUserGrblCommand
            // 
            this.txtUserGrblCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserGrblCommand.Location = new System.Drawing.Point(6, 114);
            this.txtUserGrblCommand.Name = "txtUserGrblCommand";
            this.txtUserGrblCommand.Size = new System.Drawing.Size(591, 23);
            this.txtUserGrblCommand.TabIndex = 1;
            this.txtUserGrblCommand.TextChanged += new System.EventHandler(this.txtUserGrblCommand_TextChanged);
            this.txtUserGrblCommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserGrblCommand_KeyDown);
            // 
            // textBoxConsoleText
            // 
            this.textBoxConsoleText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConsoleText.Location = new System.Drawing.Point(6, 6);
            this.textBoxConsoleText.Multiline = true;
            this.textBoxConsoleText.Name = "textBoxConsoleText";
            this.textBoxConsoleText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxConsoleText.Size = new System.Drawing.Size(753, 102);
            this.textBoxConsoleText.TabIndex = 0;
            // 
            // tabPageGCode
            // 
            this.tabPageGCode.Controls.Add(this.listViewGCode);
            this.tabPageGCode.Location = new System.Drawing.Point(4, 4);
            this.tabPageGCode.Name = "tabPageGCode";
            this.tabPageGCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGCode.Size = new System.Drawing.Size(765, 143);
            this.tabPageGCode.TabIndex = 1;
            this.tabPageGCode.Text = "tabPageGCode";
            this.tabPageGCode.UseVisualStyleBackColor = true;
            // 
            // listViewGCode
            // 
            this.listViewGCode.AllowColumnReorder = true;
            this.listViewGCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewGCode.AutoArrange = false;
            this.listViewGCode.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLine,
            this.columnHeaderGCode,
            this.columnHeaderComments,
            this.columnHeaderFeed,
            this.columnHeaderSpindleSpeed,
            this.columnHeaderAttributes});
            this.listViewGCode.FullRowSelect = true;
            this.listViewGCode.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewGCode.Location = new System.Drawing.Point(6, 6);
            this.listViewGCode.MultiSelect = false;
            this.listViewGCode.Name = "listViewGCode";
            this.listViewGCode.OwnerDraw = true;
            this.listViewGCode.SaveName = "";
            this.listViewGCode.ShowToolTip = false;
            this.listViewGCode.Size = new System.Drawing.Size(753, 131);
            this.listViewGCode.TabIndex = 2;
            this.listViewGCode.UseCompatibleStateImageBehavior = false;
            this.listViewGCode.View = System.Windows.Forms.View.Details;
            this.listViewGCode.VirtualMode = true;
            this.listViewGCode.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ListViewGCode_RetrieveVirtualItem);
            // 
            // columnHeaderLine
            // 
            this.columnHeaderLine.Text = "Line";
            // 
            // columnHeaderGCode
            // 
            this.columnHeaderGCode.Text = "gcode";
            this.columnHeaderGCode.Width = 250;
            // 
            // columnHeaderComments
            // 
            this.columnHeaderComments.Text = "comments";
            this.columnHeaderComments.Width = 150;
            // 
            // columnHeaderFeed
            // 
            this.columnHeaderFeed.Text = "Feed";
            this.columnHeaderFeed.Width = 70;
            // 
            // columnHeaderSpindleSpeed
            // 
            this.columnHeaderSpindleSpeed.Text = "spindle";
            this.columnHeaderSpindleSpeed.Width = 70;
            // 
            // columnHeaderAttributes
            // 
            this.columnHeaderAttributes.Text = "Attributes";
            this.columnHeaderAttributes.Width = 200;
            // 
            // tabPage2DView
            // 
            this.tabPage2DView.Controls.Add(this.panelZoom);
            this.tabPage2DView.Controls.Add(this.machine2dView1);
            this.tabPage2DView.Location = new System.Drawing.Point(4, 4);
            this.tabPage2DView.Name = "tabPage2DView";
            this.tabPage2DView.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2DView.Size = new System.Drawing.Size(765, 143);
            this.tabPage2DView.TabIndex = 2;
            this.tabPage2DView.Text = "tabPage1";
            this.tabPage2DView.UseVisualStyleBackColor = true;
            // 
            // panelZoom
            // 
            this.panelZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelZoom.Location = new System.Drawing.Point(512, 6);
            this.panelZoom.MaximumSize = new System.Drawing.Size(200, 200);
            this.panelZoom.MinimumSize = new System.Drawing.Size(200, 200);
            this.panelZoom.Name = "panelZoom";
            this.panelZoom.Size = new System.Drawing.Size(200, 200);
            this.panelZoom.TabIndex = 1;
            // 
            // machine2dView1
            // 
            this.machine2dView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.machine2dView1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("machine2dView1.BackgroundImage")));
            this.machine2dView1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.machine2dView1.Configuration = GSendShared.AxisConfiguration.None;
            this.machine2dView1.Location = new System.Drawing.Point(6, 6);
            this.machine2dView1.MachineSize = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.machine2dView1.Name = "machine2dView1";
            this.machine2dView1.Size = new System.Drawing.Size(485, 131);
            this.machine2dView1.TabIndex = 0;
            this.machine2dView1.XPosition = 0F;
            this.machine2dView1.YPosition = 0F;
            this.machine2dView1.ZoomPanel = this.panelZoom;
            // 
            // tabPageHeartbeat
            // 
            this.tabPageHeartbeat.Controls.Add(this.flowLayoutPanelHeartbeat);
            this.tabPageHeartbeat.Location = new System.Drawing.Point(4, 4);
            this.tabPageHeartbeat.Name = "tabPageHeartbeat";
            this.tabPageHeartbeat.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHeartbeat.Size = new System.Drawing.Size(765, 143);
            this.tabPageHeartbeat.TabIndex = 3;
            this.tabPageHeartbeat.Text = "graphs";
            this.tabPageHeartbeat.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelHeartbeat
            // 
            this.flowLayoutPanelHeartbeat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelHeartbeat.AutoScroll = true;
            this.flowLayoutPanelHeartbeat.Controls.Add(this.heartbeatPanelCommandQueue);
            this.flowLayoutPanelHeartbeat.Controls.Add(this.heartbeatPanelBufferSize);
            this.flowLayoutPanelHeartbeat.Controls.Add(this.heartbeatPanelQueueSize);
            this.flowLayoutPanelHeartbeat.Controls.Add(this.heartbeatPanelFeed);
            this.flowLayoutPanelHeartbeat.Controls.Add(this.heartbeatPanelSpindle);
            this.flowLayoutPanelHeartbeat.Controls.Add(this.heartbeatPanelAvailableBlocks);
            this.flowLayoutPanelHeartbeat.Controls.Add(this.heartbeatPanelAvailableRXBytes);
            this.flowLayoutPanelHeartbeat.Location = new System.Drawing.Point(6, 6);
            this.flowLayoutPanelHeartbeat.Name = "flowLayoutPanelHeartbeat";
            this.flowLayoutPanelHeartbeat.Size = new System.Drawing.Size(753, 131);
            this.flowLayoutPanelHeartbeat.TabIndex = 0;
            // 
            // heartbeatPanelCommandQueue
            // 
            this.heartbeatPanelCommandQueue.AutoPoints = true;
            this.heartbeatPanelCommandQueue.BackGround = System.Drawing.Color.LightCyan;
            this.heartbeatPanelCommandQueue.GraphName = null;
            this.heartbeatPanelCommandQueue.Location = new System.Drawing.Point(3, 3);
            this.heartbeatPanelCommandQueue.MaximumPoints = 60;
            this.heartbeatPanelCommandQueue.Name = "heartbeatPanelCommandQueue";
            this.heartbeatPanelCommandQueue.PrimaryColor = System.Drawing.Color.SlateGray;
            this.heartbeatPanelCommandQueue.SecondaryColor = System.Drawing.Color.BlueViolet;
            this.heartbeatPanelCommandQueue.Size = new System.Drawing.Size(200, 100);
            this.heartbeatPanelCommandQueue.TabIndex = 2;
            // 
            // heartbeatPanelBufferSize
            // 
            this.heartbeatPanelBufferSize.AutoPoints = true;
            this.heartbeatPanelBufferSize.BackGround = System.Drawing.Color.LightCyan;
            this.heartbeatPanelBufferSize.GraphName = null;
            this.heartbeatPanelBufferSize.Location = new System.Drawing.Point(209, 3);
            this.heartbeatPanelBufferSize.MaximumPoints = 60;
            this.heartbeatPanelBufferSize.Name = "heartbeatPanelBufferSize";
            this.heartbeatPanelBufferSize.PrimaryColor = System.Drawing.Color.SlateGray;
            this.heartbeatPanelBufferSize.SecondaryColor = System.Drawing.Color.BlueViolet;
            this.heartbeatPanelBufferSize.Size = new System.Drawing.Size(200, 100);
            this.heartbeatPanelBufferSize.TabIndex = 0;
            // 
            // heartbeatPanelQueueSize
            // 
            this.heartbeatPanelQueueSize.AutoPoints = true;
            this.heartbeatPanelQueueSize.BackGround = System.Drawing.Color.LightCyan;
            this.heartbeatPanelQueueSize.GraphName = null;
            this.heartbeatPanelQueueSize.Location = new System.Drawing.Point(415, 3);
            this.heartbeatPanelQueueSize.MaximumPoints = 60;
            this.heartbeatPanelQueueSize.Name = "heartbeatPanelQueueSize";
            this.heartbeatPanelQueueSize.PrimaryColor = System.Drawing.Color.SlateGray;
            this.heartbeatPanelQueueSize.SecondaryColor = System.Drawing.Color.BlueViolet;
            this.heartbeatPanelQueueSize.Size = new System.Drawing.Size(200, 100);
            this.heartbeatPanelQueueSize.TabIndex = 1;
            // 
            // heartbeatPanelFeed
            // 
            this.heartbeatPanelFeed.AutoPoints = true;
            this.heartbeatPanelFeed.BackGround = System.Drawing.Color.LightCyan;
            this.heartbeatPanelFeed.GraphName = null;
            this.heartbeatPanelFeed.Location = new System.Drawing.Point(3, 109);
            this.heartbeatPanelFeed.MaximumPoints = 60;
            this.heartbeatPanelFeed.Name = "heartbeatPanelFeed";
            this.heartbeatPanelFeed.PrimaryColor = System.Drawing.Color.SlateGray;
            this.heartbeatPanelFeed.SecondaryColor = System.Drawing.Color.BlueViolet;
            this.heartbeatPanelFeed.Size = new System.Drawing.Size(200, 100);
            this.heartbeatPanelFeed.TabIndex = 3;
            // 
            // heartbeatPanelSpindle
            // 
            this.heartbeatPanelSpindle.AutoPoints = true;
            this.heartbeatPanelSpindle.BackGround = System.Drawing.Color.LightCyan;
            this.heartbeatPanelSpindle.GraphName = null;
            this.heartbeatPanelSpindle.Location = new System.Drawing.Point(209, 109);
            this.heartbeatPanelSpindle.MaximumPoints = 60;
            this.heartbeatPanelSpindle.Name = "heartbeatPanelSpindle";
            this.heartbeatPanelSpindle.PrimaryColor = System.Drawing.Color.SlateGray;
            this.heartbeatPanelSpindle.SecondaryColor = System.Drawing.Color.BlueViolet;
            this.heartbeatPanelSpindle.Size = new System.Drawing.Size(200, 100);
            this.heartbeatPanelSpindle.TabIndex = 4;
            // 
            // heartbeatPanelAvailableBlocks
            // 
            this.heartbeatPanelAvailableBlocks.AutoPoints = true;
            this.heartbeatPanelAvailableBlocks.BackGround = System.Drawing.Color.LightCyan;
            this.heartbeatPanelAvailableBlocks.GraphName = null;
            this.heartbeatPanelAvailableBlocks.Location = new System.Drawing.Point(415, 109);
            this.heartbeatPanelAvailableBlocks.MaximumPoints = 34;
            this.heartbeatPanelAvailableBlocks.Name = "heartbeatPanelAvailableBlocks";
            this.heartbeatPanelAvailableBlocks.PrimaryColor = System.Drawing.Color.SlateGray;
            this.heartbeatPanelAvailableBlocks.SecondaryColor = System.Drawing.Color.DarkSeaGreen;
            this.heartbeatPanelAvailableBlocks.Size = new System.Drawing.Size(200, 100);
            this.heartbeatPanelAvailableBlocks.TabIndex = 5;
            // 
            // heartbeatPanelAvailableRXBytes
            // 
            this.heartbeatPanelAvailableRXBytes.AutoPoints = true;
            this.heartbeatPanelAvailableRXBytes.BackGround = System.Drawing.Color.LightCyan;
            this.heartbeatPanelAvailableRXBytes.GraphName = null;
            this.heartbeatPanelAvailableRXBytes.Location = new System.Drawing.Point(3, 215);
            this.heartbeatPanelAvailableRXBytes.MaximumPoints = 255;
            this.heartbeatPanelAvailableRXBytes.Name = "heartbeatPanelAvailableRXBytes";
            this.heartbeatPanelAvailableRXBytes.PrimaryColor = System.Drawing.Color.SlateGray;
            this.heartbeatPanelAvailableRXBytes.SecondaryColor = System.Drawing.Color.DarkSeaGreen;
            this.heartbeatPanelAvailableRXBytes.Size = new System.Drawing.Size(200, 100);
            this.heartbeatPanelAvailableRXBytes.TabIndex = 6;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "G Code Files|*.gcode;*.nc;*.ncc;*.ngc;*.tap;*.txt|All Files|*.*";
            // 
            // FrmMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(796, 615);
            this.Controls.Add(this.tabControlSecondary);
            this.Controls.Add(this.warningsAndErrors);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(812, 0);
            this.Name = "FrmMachine";
            this.Text = "FrmMachine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMachine_FormClosing);
            this.Shown += new System.EventHandler(this.FrmMachine_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMachine_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmMachine_KeyUp);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.tabPageMain.PerformLayout();
            this.tabPageOverrides.ResumeLayout(false);
            this.tabPageOverrides.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPercent)).EndInit();
            this.tabPageJog.ResumeLayout(false);
            this.tabPageSpindle.ResumeLayout(false);
            this.tabPageSpindle.PerformLayout();
            this.grpBoxSpindleSpeed.ResumeLayout(false);
            this.grpBoxSpindleSpeed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpindleSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDelaySpindle)).EndInit();
            this.tabPageServiceSchedule.ResumeLayout(false);
            this.tabPageServiceSchedule.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarServiceSpindleHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarServiceWeeks)).EndInit();
            this.tabPageMachineSettings.ResumeLayout(false);
            this.tabPageMachineSettings.PerformLayout();
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLayerHeight)).EndInit();
            this.grpFeedDisplay.ResumeLayout(false);
            this.grpFeedDisplay.PerformLayout();
            this.grpDisplayUnits.ResumeLayout(false);
            this.grpDisplayUnits.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControlSecondary.ResumeLayout(false);
            this.tabPageConsole.ResumeLayout(false);
            this.tabPageConsole.PerformLayout();
            this.tabPageGCode.ResumeLayout(false);
            this.tabPage2DView.ResumeLayout(false);
            this.tabPageHeartbeat.ResumeLayout(false);
            this.flowLayoutPanelHeartbeat.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonConnect;
        private System.Windows.Forms.ToolStripButton toolStripButtonDisconnect;
        private System.Windows.Forms.ToolStripButton toolStripButtonClearAlarm;
        private System.Windows.Forms.ToolStripButton toolStripButtonResume;
        private System.Windows.Forms.ToolStripButton toolStripButtonHome;
        private GSendControls.Selection selectionOverrideSpindle;
        private GSendControls.Selection selectionOverrideZDown;
        private GSendControls.Selection selectionOverrideZUp;
        private GSendControls.Selection selectionOverrideRapids;
        private GSendControls.JogControl jogControl;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPageOverrides;
        private System.Windows.Forms.TabPage tabPageJog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonProbe;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonPause;
        private System.Windows.Forms.TabPage tabPageMachineSettings;
        private System.Windows.Forms.TabPage tabPageSpindle;
        private System.Windows.Forms.TabPage tabPageServiceSchedule;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelServerConnect;
        private GSendControls.MachinePosition machinePositionGeneral;
        private GSendControls.MachinePosition machinePositionOverrides;
        private GSendControls.MachinePosition machinePositionJog;
        private System.Windows.Forms.Label labelSpeedPercent;
        private System.Windows.Forms.TrackBar trackBarPercent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.CheckBox cbOverridesDisable;
        private System.Windows.Forms.PropertyGrid propertyGridGrblSettings;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCpu;
        private System.Windows.Forms.Label lblPropertyDesc;
        private System.Windows.Forms.Label lblPropertyHeader;
        private System.Windows.Forms.TextBox txtGrblUpdates;
        private System.Windows.Forms.Button btnApplyGrblUpdates;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuMachine;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelWarnings;
        private GSendControls.WarningContainer warningsAndErrors;
        private GSendControls.ProbingCommand probingCommand1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.Label lblSpindleType;
        private System.Windows.Forms.ComboBox cmbSpindleType;
        private System.Windows.Forms.CheckBox cbSoftStart;
        private System.Windows.Forms.Label lblDelaySpindleStart;
        private System.Windows.Forms.TrackBar trackBarDelaySpindle;
        private System.Windows.Forms.Button btnZeroY;
        private System.Windows.Forms.Button btnZeroX;
        private System.Windows.Forms.Button btnZeroAll;
        private System.Windows.Forms.Button btnZeroZ;
        private System.Windows.Forms.TabControl tabControlSecondary;
        private System.Windows.Forms.TabPage tabPageConsole;
        private System.Windows.Forms.TextBox textBoxConsoleText;
        private System.Windows.Forms.Button btnGrblCommandSend;
        private System.Windows.Forms.Button btnGrblCommandClear;
        private System.Windows.Forms.TextBox txtUserGrblCommand;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSpindle;
        private System.Windows.Forms.GroupBox grpBoxSpindleSpeed;
        private System.Windows.Forms.Button btnSpindleStop;
        private System.Windows.Forms.Button btnSpindleStart;
        private System.Windows.Forms.Label lblSpindleSpeed;
        private System.Windows.Forms.TrackBar trackBarSpindleSpeed;
        private System.Windows.Forms.CheckBox cbSpindleCounterClockwise;
        private System.Windows.Forms.CheckBox cbToolChanger;
        private System.Windows.Forms.CheckBox cbLimitSwitches;
        private System.Windows.Forms.CheckBox cbMaintainServiceSchedule;
        private System.Windows.Forms.TrackBar trackBarServiceWeeks;
        private System.Windows.Forms.Label lblServiceSchedule;
        private System.Windows.Forms.Label lblSpindleHours;
        private System.Windows.Forms.TrackBar trackBarServiceSpindleHours;
        private System.Windows.Forms.Button btnServiceReset;
        private System.Windows.Forms.Label lblNextService;
        private System.Windows.Forms.Label lblSpindleHoursRemaining;
        private System.Windows.Forms.Label lblServiceDate;
        private System.Windows.Forms.Button btnServiceRefresh;
        private System.Windows.Forms.ToolStripMenuItem mnuViewGeneral;
        private System.Windows.Forms.ToolStripMenuItem mnuViewOverrides;
        private System.Windows.Forms.ToolStripMenuItem mnuViewJog;
        private System.Windows.Forms.ToolStripMenuItem mnuViewSpindle;
        private System.Windows.Forms.ToolStripMenuItem mnuViewServiceSchedule;
        private System.Windows.Forms.ToolStripMenuItem mnuViewMachineSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuViewSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuViewConsole;
        private System.Windows.Forms.ToolStripMenuItem mnuMachineLoadGCode;
        private System.Windows.Forms.ToolStripMenuItem mnuMachineClearGCode;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuMachineClose;
        private System.Windows.Forms.ToolStripMenuItem mnuAction;
        private System.Windows.Forms.ToolStripMenuItem mnuActionSaveConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuActionConnect;
        private System.Windows.Forms.ToolStripMenuItem mnuActionDisconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem mnuActionClearAlarm;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mnuActionHome;
        private System.Windows.Forms.ToolStripMenuItem mnuActionProbe;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuActionRun;
        private System.Windows.Forms.ToolStripMenuItem mnuActionPause;
        private System.Windows.Forms.ToolStripMenuItem mnuActionStop;
        private GSendControls.ListViewEx lvServices;
        private System.Windows.Forms.ColumnHeader columnServiceHeaderDateTime;
        private System.Windows.Forms.ColumnHeader columnServiceHeaderServiceType;
        private System.Windows.Forms.ColumnHeader columnServiceHeaderSpindleHours;
        private System.Windows.Forms.CheckBox cbCorrectMode;
        private System.Windows.Forms.CheckBox cbFloodCoolant;
        private System.Windows.Forms.CheckBox cbMistCoolant;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonCoordinateSystem;
        private System.Windows.Forms.ToolStripMenuItem g54ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem g55ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem g56ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem g57ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem g58ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem g59ToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpDisplayUnits;
        private System.Windows.Forms.RadioButton rbFeedbackInch;
        private System.Windows.Forms.RadioButton rbFeedbackMm;
        private System.Windows.Forms.CheckBox cbAutoSelectFeedbackUnit;
        private System.Windows.Forms.GroupBox grpFeedDisplay;
        private System.Windows.Forms.RadioButton rbFeedDisplayInchMin;
        private System.Windows.Forms.RadioButton rbFeedDisplayInchSec;
        private System.Windows.Forms.RadioButton rbFeedDisplayMmSec;
        private System.Windows.Forms.RadioButton rbFeedDisplayMmMin;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDisplayUnit;
        private System.Windows.Forms.CheckBox cbOverrideLinkZDown;
        private System.Windows.Forms.CheckBox cbOverrideLinkZUp;
        private System.Windows.Forms.CheckBox cbOverrideLinkRapids;
        private System.Windows.Forms.CheckBox cbOverrideLinkSpindle;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFeedRate;
        private System.Windows.Forms.CheckBox cbOverrideLinkXY;
        private GSendControls.Selection selectionOverrideXY;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private GSendControls.GCodeAnalysesDetails gCodeAnalysesDetails;
        private System.Windows.Forms.CheckBox cbLayerHeightWarning;
        private System.Windows.Forms.NumericUpDown numericLayerHeight;
        private System.Windows.Forms.Label lblLayerHeightMeasure;
        private System.Windows.Forms.TabPage tabPageGCode;
        private System.Windows.Forms.Label lblJobTime;
        private System.Windows.Forms.TabPage tabPage2DView;
        private GSendControls.Machine2DView machine2dView1;
        private System.Windows.Forms.Panel panelZoom;
        private System.Windows.Forms.Label lblCommandQueueSize;
        private System.Windows.Forms.Label lblQueueSize;
        private System.Windows.Forms.Label lblBufferSize;
        private System.Windows.Forms.Label lblTotalLines;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarJob;
        private GSendControls.ListViewEx listViewGCode;
        private System.Windows.Forms.ColumnHeader columnHeaderGCode;
        private System.Windows.Forms.ColumnHeader columnHeaderComments;
        private System.Windows.Forms.ColumnHeader columnHeaderFeed;
        private System.Windows.Forms.ColumnHeader columnHeaderSpindleSpeed;
        private System.Windows.Forms.ColumnHeader columnHeaderAttributes;
        private System.Windows.Forms.ColumnHeader columnHeaderLine;
        private System.Windows.Forms.TabPage tabPageHeartbeat;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelHeartbeat;
        private GSendControls.HeartbeatPanel heartbeatPanelBufferSize;
        private GSendControls.HeartbeatPanel heartbeatPanelQueueSize;
        private GSendControls.HeartbeatPanel heartbeatPanelCommandQueue;
        private GSendControls.HeartbeatPanel heartbeatPanelFeed;
        private GSendControls.HeartbeatPanel heartbeatPanelSpindle;
        private GSendControls.HeartbeatPanel heartbeatPanelAvailableBlocks;
        private GSendControls.HeartbeatPanel heartbeatPanelAvailableRXBytes;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuOptionsShortcutKeys;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuMachineRename;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
    }
}