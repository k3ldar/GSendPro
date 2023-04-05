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
            this.selectionOverrideSpindle = new GSendDesktop.Controls.Selection();
            this.selectionOverrideZDown = new GSendDesktop.Controls.Selection();
            this.selectionOverrideZUp = new GSendDesktop.Controls.Selection();
            this.selectionOverrideY = new GSendDesktop.Controls.Selection();
            this.selectionOverrideX = new GSendDesktop.Controls.Selection();
            this.jogControl = new GSendDesktop.Controls.JogControl();
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelServerConnect = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDisplayMeasurements = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCpu = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelWarnings = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelBuffer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelSpindle = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.machinePositionGeneral = new GSendDesktop.Controls.MachinePosition();
            this.tabPageOverrides = new System.Windows.Forms.TabPage();
            this.cbOverridesDisable = new System.Windows.Forms.CheckBox();
            this.labelSpeedPercent = new System.Windows.Forms.Label();
            this.trackBarPercent = new System.Windows.Forms.TrackBar();
            this.machinePositionOverrides = new GSendDesktop.Controls.MachinePosition();
            this.tabPageJog = new System.Windows.Forms.TabPage();
            this.btnZeroAll = new System.Windows.Forms.Button();
            this.btnZeroZ = new System.Windows.Forms.Button();
            this.btnZeroY = new System.Windows.Forms.Button();
            this.btnZeroX = new System.Windows.Forms.Button();
            this.machinePositionJog = new GSendDesktop.Controls.MachinePosition();
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
            this.btnServiceRefresh = new System.Windows.Forms.Button();
            this.lstServices = new System.Windows.Forms.ListBox();
            this.lblSpindleHoursRemaining = new System.Windows.Forms.Label();
            this.lblServiceDate = new System.Windows.Forms.Label();
            this.btnServiceReset = new System.Windows.Forms.Button();
            this.lblNextService = new System.Windows.Forms.Label();
            this.lblSpindleHours = new System.Windows.Forms.Label();
            this.trackBarServiceSpindleHours = new System.Windows.Forms.TrackBar();
            this.cbMaintainServiceSchedule = new System.Windows.Forms.CheckBox();
            this.trackBarServiceWeeks = new System.Windows.Forms.TrackBar();
            this.lblServiceSchedule = new System.Windows.Forms.Label();
            this.tabPageUsage = new System.Windows.Forms.TabPage();
            this.tabPageMachineSettings = new System.Windows.Forms.TabPage();
            this.btnApplyGrblUpdates = new System.Windows.Forms.Button();
            this.txtGrblUpdates = new System.Windows.Forms.TextBox();
            this.lblPropertyDesc = new System.Windows.Forms.Label();
            this.lblPropertyHeader = new System.Windows.Forms.Label();
            this.propertyGridGrblSettings = new System.Windows.Forms.PropertyGrid();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.cbToolChanger = new System.Windows.Forms.CheckBox();
            this.cbLimitSwitches = new System.Windows.Forms.CheckBox();
            this.probingCommand1 = new GSendDesktop.Controls.ProbingCommand();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.machineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.warningsAndErrors = new GSendDesktop.Controls.WarningContainer();
            this.tabControlSecondary = new System.Windows.Forms.TabControl();
            this.tabPageConsole = new System.Windows.Forms.TabPage();
            this.btnGrblCommandSend = new System.Windows.Forms.Button();
            this.btnGrblCommandClear = new System.Windows.Forms.Button();
            this.txtUserGrblCommand = new System.Windows.Forms.TextBox();
            this.textBoxConsoleText = new System.Windows.Forms.TextBox();
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
            this.menuStrip1.SuspendLayout();
            this.tabControlSecondary.SuspendLayout();
            this.tabPageConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectionOverrideSpindle
            // 
            this.selectionOverrideSpindle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectionOverrideSpindle.GroupName = "Spindle";
            this.selectionOverrideSpindle.LabelFormat = "{0}";
            this.selectionOverrideSpindle.LabelValue = null;
            this.selectionOverrideSpindle.LargeTickChange = 5;
            this.selectionOverrideSpindle.Location = new System.Drawing.Point(671, 6);
            this.selectionOverrideSpindle.Maximum = 10;
            this.selectionOverrideSpindle.Minimum = 0;
            this.selectionOverrideSpindle.Name = "selectionOverrideSpindle";
            this.selectionOverrideSpindle.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideSpindle.SmallTickChange = 1;
            this.selectionOverrideSpindle.TabIndex = 8;
            this.selectionOverrideSpindle.TickFrequency = 1;
            this.selectionOverrideSpindle.Value = 0;
            this.selectionOverrideSpindle.ValueChanged += new System.EventHandler(this.selectionOverrideSpindle_ValueChanged);
            // 
            // selectionOverrideZDown
            // 
            this.selectionOverrideZDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectionOverrideZDown.GroupName = "Z Down";
            this.selectionOverrideZDown.LabelFormat = "{0}";
            this.selectionOverrideZDown.LabelValue = null;
            this.selectionOverrideZDown.LargeTickChange = 5;
            this.selectionOverrideZDown.Location = new System.Drawing.Point(584, 6);
            this.selectionOverrideZDown.Maximum = 10;
            this.selectionOverrideZDown.Minimum = 0;
            this.selectionOverrideZDown.Name = "selectionOverrideZDown";
            this.selectionOverrideZDown.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideZDown.SmallTickChange = 1;
            this.selectionOverrideZDown.TabIndex = 7;
            this.selectionOverrideZDown.TickFrequency = 1;
            this.selectionOverrideZDown.Value = 0;
            // 
            // selectionOverrideZUp
            // 
            this.selectionOverrideZUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectionOverrideZUp.GroupName = "Z Up";
            this.selectionOverrideZUp.LabelFormat = "{0}";
            this.selectionOverrideZUp.LabelValue = null;
            this.selectionOverrideZUp.LargeTickChange = 5;
            this.selectionOverrideZUp.Location = new System.Drawing.Point(496, 6);
            this.selectionOverrideZUp.Maximum = 10;
            this.selectionOverrideZUp.Minimum = 0;
            this.selectionOverrideZUp.Name = "selectionOverrideZUp";
            this.selectionOverrideZUp.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideZUp.SmallTickChange = 1;
            this.selectionOverrideZUp.TabIndex = 6;
            this.selectionOverrideZUp.TickFrequency = 1;
            this.selectionOverrideZUp.Value = 0;
            // 
            // selectionOverrideY
            // 
            this.selectionOverrideY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectionOverrideY.GroupName = "Y";
            this.selectionOverrideY.LabelFormat = "{0}";
            this.selectionOverrideY.LabelValue = null;
            this.selectionOverrideY.LargeTickChange = 5;
            this.selectionOverrideY.Location = new System.Drawing.Point(408, 6);
            this.selectionOverrideY.Maximum = 10;
            this.selectionOverrideY.Minimum = 0;
            this.selectionOverrideY.Name = "selectionOverrideY";
            this.selectionOverrideY.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideY.SmallTickChange = 1;
            this.selectionOverrideY.TabIndex = 5;
            this.selectionOverrideY.TickFrequency = 1;
            this.selectionOverrideY.Value = 0;
            // 
            // selectionOverrideX
            // 
            this.selectionOverrideX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectionOverrideX.GroupName = "X";
            this.selectionOverrideX.LabelFormat = "{0}";
            this.selectionOverrideX.LabelValue = null;
            this.selectionOverrideX.LargeTickChange = 5;
            this.selectionOverrideX.Location = new System.Drawing.Point(320, 6);
            this.selectionOverrideX.Maximum = 10;
            this.selectionOverrideX.Minimum = 0;
            this.selectionOverrideX.Name = "selectionOverrideX";
            this.selectionOverrideX.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideX.SmallTickChange = 1;
            this.selectionOverrideX.TabIndex = 4;
            this.selectionOverrideX.TickFrequency = 1;
            this.selectionOverrideX.Value = 0;
            // 
            // jogControl
            // 
            this.jogControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.jogControl.FeedMaximum = 10;
            this.jogControl.FeedMinimum = 0;
            this.jogControl.FeedRate = 0;
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
            this.toolStripButtonStop});
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
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelServerConnect,
            this.toolStripStatusLabelDisplayMeasurements,
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabelCpu,
            this.toolStripStatusLabelWarnings,
            this.toolStripStatusLabelBuffer,
            this.toolStripStatusLabelSpindle});
            this.statusStrip.Location = new System.Drawing.Point(0, 621);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.Size = new System.Drawing.Size(796, 24);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabelServerConnect
            // 
            this.toolStripStatusLabelServerConnect.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelServerConnect.Name = "toolStripStatusLabelServerConnect";
            this.toolStripStatusLabelServerConnect.Size = new System.Drawing.Size(92, 19);
            this.toolStripStatusLabelServerConnect.Text = "Not Connected";
            // 
            // toolStripStatusLabelDisplayMeasurements
            // 
            this.toolStripStatusLabelDisplayMeasurements.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelDisplayMeasurements.Name = "toolStripStatusLabelDisplayMeasurements";
            this.toolStripStatusLabelDisplayMeasurements.Size = new System.Drawing.Size(33, 19);
            this.toolStripStatusLabelDisplayMeasurements.Text = "asdf";
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
            this.toolStripStatusLabelWarnings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabelWarnings.Image")));
            this.toolStripStatusLabelWarnings.Name = "toolStripStatusLabelWarnings";
            this.toolStripStatusLabelWarnings.Size = new System.Drawing.Size(29, 19);
            this.toolStripStatusLabelWarnings.Text = "0";
            this.toolStripStatusLabelWarnings.Visible = false;
            // 
            // toolStripStatusLabelBuffer
            // 
            this.toolStripStatusLabelBuffer.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelBuffer.Name = "toolStripStatusLabelBuffer";
            this.toolStripStatusLabelBuffer.Size = new System.Drawing.Size(43, 19);
            this.toolStripStatusLabelBuffer.Text = "Buffer";
            // 
            // toolStripStatusLabelSpindle
            // 
            this.toolStripStatusLabelSpindle.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelSpindle.Name = "toolStripStatusLabelSpindle";
            this.toolStripStatusLabelSpindle.Size = new System.Drawing.Size(50, 19);
            this.toolStripStatusLabelSpindle.Text = "Spindle";
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
            this.tabControlMain.Controls.Add(this.tabPageUsage);
            this.tabControlMain.Controls.Add(this.tabPageMachineSettings);
            this.tabControlMain.Controls.Add(this.tabPageSettings);
            this.tabControlMain.HotTrack = true;
            this.tabControlMain.Location = new System.Drawing.Point(12, 135);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(773, 270);
            this.tabControlMain.TabIndex = 3;
            // 
            // tabPageMain
            // 
            this.tabPageMain.BackColor = System.Drawing.Color.White;
            this.tabPageMain.Controls.Add(this.machinePositionGeneral);
            this.tabPageMain.Location = new System.Drawing.Point(4, 24);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(765, 242);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "General";
            // 
            // machinePositionGeneral
            // 
            this.machinePositionGeneral.DisplayMeasurements = GSendShared.DisplayUnits.MmPerMinute;
            this.machinePositionGeneral.Location = new System.Drawing.Point(6, 6);
            this.machinePositionGeneral.Name = "machinePositionGeneral";
            this.machinePositionGeneral.Size = new System.Drawing.Size(314, 112);
            this.machinePositionGeneral.TabIndex = 0;
            // 
            // tabPageOverrides
            // 
            this.tabPageOverrides.BackColor = System.Drawing.Color.White;
            this.tabPageOverrides.Controls.Add(this.cbOverridesDisable);
            this.tabPageOverrides.Controls.Add(this.labelSpeedPercent);
            this.tabPageOverrides.Controls.Add(this.trackBarPercent);
            this.tabPageOverrides.Controls.Add(this.machinePositionOverrides);
            this.tabPageOverrides.Controls.Add(this.selectionOverrideSpindle);
            this.tabPageOverrides.Controls.Add(this.selectionOverrideZDown);
            this.tabPageOverrides.Controls.Add(this.selectionOverrideX);
            this.tabPageOverrides.Controls.Add(this.selectionOverrideZUp);
            this.tabPageOverrides.Controls.Add(this.selectionOverrideY);
            this.tabPageOverrides.Location = new System.Drawing.Point(4, 24);
            this.tabPageOverrides.Name = "tabPageOverrides";
            this.tabPageOverrides.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOverrides.Size = new System.Drawing.Size(765, 242);
            this.tabPageOverrides.TabIndex = 1;
            this.tabPageOverrides.Text = "Overrides";
            // 
            // cbOverridesDisable
            // 
            this.cbOverridesDisable.AutoSize = true;
            this.cbOverridesDisable.Location = new System.Drawing.Point(6, 215);
            this.cbOverridesDisable.Name = "cbOverridesDisable";
            this.cbOverridesDisable.Size = new System.Drawing.Size(83, 19);
            this.cbOverridesDisable.TabIndex = 3;
            this.cbOverridesDisable.Text = "checkBox1";
            this.cbOverridesDisable.UseVisualStyleBackColor = true;
            this.cbOverridesDisable.CheckedChanged += new System.EventHandler(this.cbOverridesDisable_CheckedChanged);
            // 
            // labelSpeedPercent
            // 
            this.labelSpeedPercent.AutoSize = true;
            this.labelSpeedPercent.Location = new System.Drawing.Point(6, 171);
            this.labelSpeedPercent.Name = "labelSpeedPercent";
            this.labelSpeedPercent.Size = new System.Drawing.Size(0, 15);
            this.labelSpeedPercent.TabIndex = 2;
            // 
            // trackBarPercent
            // 
            this.trackBarPercent.Location = new System.Drawing.Point(6, 123);
            this.trackBarPercent.Maximum = 100;
            this.trackBarPercent.Name = "trackBarPercent";
            this.trackBarPercent.Size = new System.Drawing.Size(280, 45);
            this.trackBarPercent.TabIndex = 1;
            this.trackBarPercent.TickFrequency = 5;
            this.trackBarPercent.ValueChanged += new System.EventHandler(this.trackBarPercent_ValueChanged);
            // 
            // machinePositionOverrides
            // 
            this.machinePositionOverrides.DisplayMeasurements = GSendShared.DisplayUnits.MmPerMinute;
            this.machinePositionOverrides.Location = new System.Drawing.Point(6, 6);
            this.machinePositionOverrides.Name = "machinePositionOverrides";
            this.machinePositionOverrides.Size = new System.Drawing.Size(314, 112);
            this.machinePositionOverrides.TabIndex = 0;
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
            this.machinePositionJog.DisplayMeasurements = GSendShared.DisplayUnits.MmPerMinute;
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
            this.tabPageServiceSchedule.Controls.Add(this.btnServiceRefresh);
            this.tabPageServiceSchedule.Controls.Add(this.lstServices);
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
            // btnServiceRefresh
            // 
            this.btnServiceRefresh.Location = new System.Drawing.Point(684, 89);
            this.btnServiceRefresh.Name = "btnServiceRefresh";
            this.btnServiceRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnServiceRefresh.TabIndex = 10;
            this.btnServiceRefresh.Text = "button1";
            this.btnServiceRefresh.UseVisualStyleBackColor = true;
            this.btnServiceRefresh.Click += new System.EventHandler(this.btnServiceRefresh_Click);
            // 
            // lstServices
            // 
            this.lstServices.FormattingEnabled = true;
            this.lstServices.HorizontalScrollbar = true;
            this.lstServices.ItemHeight = 15;
            this.lstServices.Location = new System.Drawing.Point(388, 118);
            this.lstServices.Name = "lstServices";
            this.lstServices.Size = new System.Drawing.Size(371, 109);
            this.lstServices.TabIndex = 9;
            // 
            // lblSpindleHoursRemaining
            // 
            this.lblSpindleHoursRemaining.AutoSize = true;
            this.lblSpindleHoursRemaining.Location = new System.Drawing.Point(388, 73);
            this.lblSpindleHoursRemaining.Name = "lblSpindleHoursRemaining";
            this.lblSpindleHoursRemaining.Size = new System.Drawing.Size(38, 15);
            this.lblSpindleHoursRemaining.TabIndex = 8;
            this.lblSpindleHoursRemaining.Text = "label1";
            // 
            // lblServiceDate
            // 
            this.lblServiceDate.AutoSize = true;
            this.lblServiceDate.Location = new System.Drawing.Point(388, 43);
            this.lblServiceDate.Name = "lblServiceDate";
            this.lblServiceDate.Size = new System.Drawing.Size(38, 15);
            this.lblServiceDate.TabIndex = 7;
            this.lblServiceDate.Text = "label1";
            // 
            // btnServiceReset
            // 
            this.btnServiceReset.Location = new System.Drawing.Point(684, 12);
            this.btnServiceReset.Name = "btnServiceReset";
            this.btnServiceReset.Size = new System.Drawing.Size(75, 23);
            this.btnServiceReset.TabIndex = 6;
            this.btnServiceReset.Text = "button1";
            this.btnServiceReset.UseVisualStyleBackColor = true;
            this.btnServiceReset.Click += new System.EventHandler(this.btnServiceReset_Click);
            // 
            // lblNextService
            // 
            this.lblNextService.AutoSize = true;
            this.lblNextService.Location = new System.Drawing.Point(388, 16);
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
            this.trackBarServiceSpindleHours.Maximum = 200;
            this.trackBarServiceSpindleHours.Name = "trackBarServiceSpindleHours";
            this.trackBarServiceSpindleHours.Size = new System.Drawing.Size(286, 45);
            this.trackBarServiceSpindleHours.TabIndex = 3;
            this.trackBarServiceSpindleHours.TickFrequency = 10;
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
            this.trackBarServiceWeeks.Maximum = 26;
            this.trackBarServiceWeeks.Name = "trackBarServiceWeeks";
            this.trackBarServiceWeeks.Size = new System.Drawing.Size(286, 45);
            this.trackBarServiceWeeks.TabIndex = 1;
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
            // tabPageUsage
            // 
            this.tabPageUsage.BackColor = System.Drawing.Color.White;
            this.tabPageUsage.Location = new System.Drawing.Point(4, 24);
            this.tabPageUsage.Name = "tabPageUsage";
            this.tabPageUsage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUsage.Size = new System.Drawing.Size(765, 242);
            this.tabPageUsage.TabIndex = 7;
            this.tabPageUsage.Text = "Useage";
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
            this.probingCommand1.Location = new System.Drawing.Point(501, 6);
            this.probingCommand1.MinimumSize = new System.Drawing.Size(220, 172);
            this.probingCommand1.Name = "probingCommand1";
            this.probingCommand1.Size = new System.Drawing.Size(258, 230);
            this.probingCommand1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.machineToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(796, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // machineToolStripMenuItem
            // 
            this.machineToolStripMenuItem.Name = "machineToolStripMenuItem";
            this.machineToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.machineToolStripMenuItem.Text = "&Machine";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
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
            this.warningsAndErrors.Size = new System.Drawing.Size(772, 48);
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
            this.tabControlSecondary.Location = new System.Drawing.Point(12, 411);
            this.tabControlSecondary.Name = "tabControlSecondary";
            this.tabControlSecondary.SelectedIndex = 0;
            this.tabControlSecondary.Size = new System.Drawing.Size(773, 197);
            this.tabControlSecondary.TabIndex = 4;
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
            this.tabPageConsole.Size = new System.Drawing.Size(765, 169);
            this.tabPageConsole.TabIndex = 0;
            this.tabPageConsole.Text = "tabPageConsole";
            this.tabPageConsole.UseVisualStyleBackColor = true;
            // 
            // btnGrblCommandSend
            // 
            this.btnGrblCommandSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGrblCommandSend.Location = new System.Drawing.Point(603, 139);
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
            this.btnGrblCommandClear.Location = new System.Drawing.Point(684, 139);
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
            this.txtUserGrblCommand.Location = new System.Drawing.Point(6, 140);
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
            this.textBoxConsoleText.Size = new System.Drawing.Size(753, 128);
            this.textBoxConsoleText.TabIndex = 0;
            // 
            // FrmMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 645);
            this.Controls.Add(this.tabControlSecondary);
            this.Controls.Add(this.warningsAndErrors);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(812, 0);
            this.Name = "FrmMachine";
            this.Text = "FrmMachine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMachine_FormClosing);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
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
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControlSecondary.ResumeLayout(false);
            this.tabPageConsole.ResumeLayout(false);
            this.tabPageConsole.PerformLayout();
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
        private Controls.Selection selectionOverrideSpindle;
        private Controls.Selection selectionOverrideZDown;
        private Controls.Selection selectionOverrideZUp;
        private Controls.Selection selectionOverrideY;
        private Controls.Selection selectionOverrideX;
        private Controls.JogControl jogControl;
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
        private System.Windows.Forms.TabPage tabPageUsage;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelServerConnect;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDisplayMeasurements;
        private Controls.MachinePosition machinePositionGeneral;
        private Controls.MachinePosition machinePositionOverrides;
        private Controls.MachinePosition machinePositionJog;
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
        private System.Windows.Forms.ToolStripMenuItem machineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelWarnings;
        private Controls.WarningContainer warningsAndErrors;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBuffer;
        private Controls.ProbingCommand probingCommand1;
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
        private System.Windows.Forms.ListBox lstServices;
    }
}