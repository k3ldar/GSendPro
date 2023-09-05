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
            selectionOverrideSpindle = new GSendControls.CheckedSelection();
            selectionOverrideZDown = new GSendControls.CheckedSelection();
            selectionOverrideZUp = new GSendControls.CheckedSelection();
            selectionOverrideRapids = new GSendControls.CheckedSelection();
            jogControl = new GSendControls.JogControl();
            toolStripMain = new System.Windows.Forms.ToolStrip();
            toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonConnect = new System.Windows.Forms.ToolStripButton();
            toolStripButtonDisconnect = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonClearAlarm = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonHome = new System.Windows.Forms.ToolStripButton();
            toolStripButtonProbe = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonResume = new System.Windows.Forms.ToolStripButton();
            toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
            toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            toolStripDropDownButtonCoordinateSystem = new System.Windows.Forms.ToolStripDropDownButton();
            g54ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            g55ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            g56ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            g57ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            g58ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            g59ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            statusStrip = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabelServerConnect = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelCpu = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelWarnings = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelSpindle = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelFeedRate = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelDisplayUnit = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripProgressBarJob = new System.Windows.Forms.ToolStripProgressBar();
            tabControlMain = new System.Windows.Forms.TabControl();
            tabPageMain = new System.Windows.Forms.TabPage();
            lblTotalLines = new System.Windows.Forms.Label();
            lblCommandQueueSize = new System.Windows.Forms.Label();
            lblQueueSize = new System.Windows.Forms.Label();
            lblBufferSize = new System.Windows.Forms.Label();
            lblJobTime = new System.Windows.Forms.Label();
            gCodeAnalysesDetails = new GSendControls.GCodeAnalysesDetails();
            machinePositionGeneral = new GSendControls.MachinePosition();
            tabPageOverrides = new System.Windows.Forms.TabPage();
            cbOverridesDisable = new System.Windows.Forms.CheckBox();
            labelSpeedPercent = new System.Windows.Forms.Label();
            trackBarPercent = new System.Windows.Forms.TrackBar();
            machinePositionOverrides = new GSendControls.MachinePosition();
            selectionOverrideXY = new GSendControls.CheckedSelection();
            tabPageJog = new System.Windows.Forms.TabPage();
            btnZeroAll = new System.Windows.Forms.Button();
            btnZeroZ = new System.Windows.Forms.Button();
            btnZeroY = new System.Windows.Forms.Button();
            btnZeroX = new System.Windows.Forms.Button();
            machinePositionJog = new GSendControls.MachinePosition();
            tabPageSpindle = new System.Windows.Forms.TabPage();
            grpBoxSpindleSpeed = new System.Windows.Forms.GroupBox();
            cbSpindleCounterClockwise = new System.Windows.Forms.CheckBox();
            btnSpindleStop = new System.Windows.Forms.Button();
            btnSpindleStart = new System.Windows.Forms.Button();
            lblSpindleSpeed = new System.Windows.Forms.Label();
            trackBarSpindleSpeed = new System.Windows.Forms.TrackBar();
            lblDelaySpindleStart = new System.Windows.Forms.Label();
            trackBarDelaySpindle = new System.Windows.Forms.TrackBar();
            cbSoftStart = new System.Windows.Forms.CheckBox();
            lblSpindleType = new System.Windows.Forms.Label();
            cmbSpindleType = new System.Windows.Forms.ComboBox();
            tabPageServiceSchedule = new System.Windows.Forms.TabPage();
            lvServices = new GSendControls.ListViewEx();
            columnServiceHeaderDateTime = new System.Windows.Forms.ColumnHeader();
            columnServiceHeaderServiceType = new System.Windows.Forms.ColumnHeader();
            columnServiceHeaderSpindleHours = new System.Windows.Forms.ColumnHeader();
            btnServiceRefresh = new System.Windows.Forms.Button();
            lblSpindleHoursRemaining = new System.Windows.Forms.Label();
            lblServiceDate = new System.Windows.Forms.Label();
            btnServiceReset = new System.Windows.Forms.Button();
            lblNextService = new System.Windows.Forms.Label();
            lblSpindleHours = new System.Windows.Forms.Label();
            trackBarServiceSpindleHours = new System.Windows.Forms.TrackBar();
            cbMaintainServiceSchedule = new System.Windows.Forms.CheckBox();
            trackBarServiceWeeks = new System.Windows.Forms.TrackBar();
            lblServiceSchedule = new System.Windows.Forms.Label();
            tabPageMachineSettings = new System.Windows.Forms.TabPage();
            btnApplyGrblUpdates = new System.Windows.Forms.Button();
            txtGrblUpdates = new System.Windows.Forms.TextBox();
            lblPropertyDesc = new System.Windows.Forms.Label();
            lblPropertyHeader = new System.Windows.Forms.Label();
            propertyGridGrblSettings = new System.Windows.Forms.PropertyGrid();
            tabPageSettings = new System.Windows.Forms.TabPage();
            lblLayerHeightMeasure = new System.Windows.Forms.Label();
            numericLayerHeight = new System.Windows.Forms.NumericUpDown();
            cbLayerHeightWarning = new System.Windows.Forms.CheckBox();
            grpFeedDisplay = new System.Windows.Forms.GroupBox();
            rbFeedDisplayInchMin = new System.Windows.Forms.RadioButton();
            rbFeedDisplayInchSec = new System.Windows.Forms.RadioButton();
            rbFeedDisplayMmSec = new System.Windows.Forms.RadioButton();
            rbFeedDisplayMmMin = new System.Windows.Forms.RadioButton();
            grpDisplayUnits = new System.Windows.Forms.GroupBox();
            cbAutoSelectFeedbackUnit = new System.Windows.Forms.CheckBox();
            rbFeedbackInch = new System.Windows.Forms.RadioButton();
            rbFeedbackMm = new System.Windows.Forms.RadioButton();
            cbCorrectMode = new System.Windows.Forms.CheckBox();
            cbFloodCoolant = new System.Windows.Forms.CheckBox();
            cbMistCoolant = new System.Windows.Forms.CheckBox();
            cbToolChanger = new System.Windows.Forms.CheckBox();
            cbLimitSwitches = new System.Windows.Forms.CheckBox();
            probingCommand1 = new GSendControls.ProbingCommand();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            mnuMachine = new System.Windows.Forms.ToolStripMenuItem();
            mnuMachineLoadGCode = new System.Windows.Forms.ToolStripMenuItem();
            mnuMachineClearGCode = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            mnuMachineRename = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            mnuMachineClose = new System.Windows.Forms.ToolStripMenuItem();
            mnuView = new System.Windows.Forms.ToolStripMenuItem();
            mnuViewGeneral = new System.Windows.Forms.ToolStripMenuItem();
            mnuViewOverrides = new System.Windows.Forms.ToolStripMenuItem();
            mnuViewJog = new System.Windows.Forms.ToolStripMenuItem();
            mnuViewSpindle = new System.Windows.Forms.ToolStripMenuItem();
            mnuViewServiceSchedule = new System.Windows.Forms.ToolStripMenuItem();
            mnuViewMachineSettings = new System.Windows.Forms.ToolStripMenuItem();
            mnuViewSettings = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            mnuViewConsole = new System.Windows.Forms.ToolStripMenuItem();
            mnuAction = new System.Windows.Forms.ToolStripMenuItem();
            mnuActionSaveConfig = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            mnuActionConnect = new System.Windows.Forms.ToolStripMenuItem();
            mnuActionDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            mnuActionClearAlarm = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            mnuActionHome = new System.Windows.Forms.ToolStripMenuItem();
            mnuActionProbe = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            mnuActionRun = new System.Windows.Forms.ToolStripMenuItem();
            mnuActionPause = new System.Windows.Forms.ToolStripMenuItem();
            mnuActionStop = new System.Windows.Forms.ToolStripMenuItem();
            mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            mnuOptionsShortcutKeys = new System.Windows.Forms.ToolStripMenuItem();
            mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            warningsAndErrors = new GSendControls.WarningContainer();
            tabControlSecondary = new System.Windows.Forms.TabControl();
            tabPageConsole = new System.Windows.Forms.TabPage();
            btnGrblCommandSend = new System.Windows.Forms.Button();
            btnGrblCommandClear = new System.Windows.Forms.Button();
            txtUserGrblCommand = new System.Windows.Forms.TextBox();
            textBoxConsoleText = new System.Windows.Forms.TextBox();
            tabPageGCode = new System.Windows.Forms.TabPage();
            listViewGCode = new GSendControls.ListViewEx();
            columnHeaderLine = new System.Windows.Forms.ColumnHeader();
            columnHeaderGCode = new System.Windows.Forms.ColumnHeader();
            columnHeaderComments = new System.Windows.Forms.ColumnHeader();
            columnHeaderFeed = new System.Windows.Forms.ColumnHeader();
            columnHeaderSpindleSpeed = new System.Windows.Forms.ColumnHeader();
            columnHeaderAttributes = new System.Windows.Forms.ColumnHeader();
            columnHeaderStatus = new System.Windows.Forms.ColumnHeader();
            tabPage2DView = new System.Windows.Forms.TabPage();
            panelZoom = new System.Windows.Forms.Panel();
            machine2dView1 = new GSendControls.Machine2DView();
            tabPageHeartbeat = new System.Windows.Forms.TabPage();
            flowLayoutPanelHeartbeat = new System.Windows.Forms.FlowLayoutPanel();
            heartbeatPanelCommandQueue = new GSendControls.HeartbeatPanel();
            heartbeatPanelBufferSize = new GSendControls.HeartbeatPanel();
            heartbeatPanelQueueSize = new GSendControls.HeartbeatPanel();
            heartbeatPanelFeed = new GSendControls.HeartbeatPanel();
            heartbeatPanelSpindle = new GSendControls.HeartbeatPanel();
            heartbeatPanelAvailableBlocks = new GSendControls.HeartbeatPanel();
            heartbeatPanelAvailableRXBytes = new GSendControls.HeartbeatPanel();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            toolStripMain.SuspendLayout();
            statusStrip.SuspendLayout();
            tabControlMain.SuspendLayout();
            tabPageMain.SuspendLayout();
            tabPageOverrides.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarPercent).BeginInit();
            tabPageJog.SuspendLayout();
            tabPageSpindle.SuspendLayout();
            grpBoxSpindleSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarSpindleSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarDelaySpindle).BeginInit();
            tabPageServiceSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarServiceSpindleHours).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarServiceWeeks).BeginInit();
            tabPageMachineSettings.SuspendLayout();
            tabPageSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericLayerHeight).BeginInit();
            grpFeedDisplay.SuspendLayout();
            grpDisplayUnits.SuspendLayout();
            menuStrip1.SuspendLayout();
            tabControlSecondary.SuspendLayout();
            tabPageConsole.SuspendLayout();
            tabPageGCode.SuspendLayout();
            tabPage2DView.SuspendLayout();
            tabPageHeartbeat.SuspendLayout();
            flowLayoutPanelHeartbeat.SuspendLayout();
            SuspendLayout();
            // 
            // selectionOverrideSpindle
            // 
            selectionOverrideSpindle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            selectionOverrideSpindle.Checked = false;
            selectionOverrideSpindle.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            selectionOverrideSpindle.GroupName = "Spindle";
            selectionOverrideSpindle.HandleMouseWheel = false;
            selectionOverrideSpindle.HasDisplayUnits = false;
            selectionOverrideSpindle.LabelFormat = "{0}";
            selectionOverrideSpindle.LabelValue = null;
            selectionOverrideSpindle.LargeTickChange = 5;
            selectionOverrideSpindle.Location = new System.Drawing.Point(685, 6);
            selectionOverrideSpindle.Maximum = 10;
            selectionOverrideSpindle.Minimum = 0;
            selectionOverrideSpindle.Name = "selectionOverrideSpindle";
            selectionOverrideSpindle.Size = new System.Drawing.Size(82, 228);
            selectionOverrideSpindle.SmallTickChange = 1;
            selectionOverrideSpindle.TabIndex = 12;
            selectionOverrideSpindle.TickFrequency = 1;
            selectionOverrideSpindle.Value = 0;
            selectionOverrideSpindle.ValueChanged += selectionOverrideSpindle_ValueChanged;
            // 
            // selectionOverrideZDown
            // 
            selectionOverrideZDown.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            selectionOverrideZDown.Checked = false;
            selectionOverrideZDown.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            selectionOverrideZDown.GroupName = "Z Down";
            selectionOverrideZDown.HandleMouseWheel = false;
            selectionOverrideZDown.HasDisplayUnits = true;
            selectionOverrideZDown.LabelFormat = "{0}";
            selectionOverrideZDown.LabelValue = null;
            selectionOverrideZDown.LargeTickChange = 50;
            selectionOverrideZDown.Location = new System.Drawing.Point(598, 6);
            selectionOverrideZDown.Maximum = 10;
            selectionOverrideZDown.Minimum = 0;
            selectionOverrideZDown.Name = "selectionOverrideZDown";
            selectionOverrideZDown.Size = new System.Drawing.Size(82, 228);
            selectionOverrideZDown.SmallTickChange = 5;
            selectionOverrideZDown.TabIndex = 11;
            selectionOverrideZDown.TickFrequency = 1;
            selectionOverrideZDown.Value = 0;
            // 
            // selectionOverrideZUp
            // 
            selectionOverrideZUp.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            selectionOverrideZUp.Checked = false;
            selectionOverrideZUp.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            selectionOverrideZUp.GroupName = "Z Up";
            selectionOverrideZUp.HandleMouseWheel = false;
            selectionOverrideZUp.HasDisplayUnits = true;
            selectionOverrideZUp.LabelFormat = "{0}";
            selectionOverrideZUp.LabelValue = null;
            selectionOverrideZUp.LargeTickChange = 50;
            selectionOverrideZUp.Location = new System.Drawing.Point(510, 6);
            selectionOverrideZUp.Maximum = 10;
            selectionOverrideZUp.Minimum = 0;
            selectionOverrideZUp.Name = "selectionOverrideZUp";
            selectionOverrideZUp.Size = new System.Drawing.Size(82, 228);
            selectionOverrideZUp.SmallTickChange = 5;
            selectionOverrideZUp.TabIndex = 10;
            selectionOverrideZUp.TickFrequency = 1;
            selectionOverrideZUp.Value = 0;
            // 
            // selectionOverrideRapids
            // 
            selectionOverrideRapids.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            selectionOverrideRapids.Checked = false;
            selectionOverrideRapids.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            selectionOverrideRapids.GroupName = "Rapids";
            selectionOverrideRapids.HandleMouseWheel = true;
            selectionOverrideRapids.HasDisplayUnits = false;
            selectionOverrideRapids.LabelFormat = "High";
            selectionOverrideRapids.LabelValue = null;
            selectionOverrideRapids.LargeTickChange = 1;
            selectionOverrideRapids.Location = new System.Drawing.Point(334, 6);
            selectionOverrideRapids.Maximum = 2;
            selectionOverrideRapids.Minimum = 0;
            selectionOverrideRapids.Name = "selectionOverrideRapids";
            selectionOverrideRapids.Size = new System.Drawing.Size(82, 228);
            selectionOverrideRapids.SmallTickChange = 1;
            selectionOverrideRapids.TabIndex = 8;
            selectionOverrideRapids.TickFrequency = 1;
            selectionOverrideRapids.Value = 2;
            // 
            // jogControl
            // 
            jogControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            jogControl.FeedMaximum = 10;
            jogControl.FeedMinimum = 0;
            jogControl.FeedRate = 0;
            jogControl.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            jogControl.Location = new System.Drawing.Point(320, 6);
            jogControl.Name = "jogControl";
            jogControl.Size = new System.Drawing.Size(439, 190);
            jogControl.StepValue = 0;
            jogControl.TabIndex = 5;
            jogControl.OnJogStart += jogControl_OnJogStart;
            jogControl.OnJogStop += jogControl_OnJogStop;
            jogControl.OnUpdate += jogControl_OnUpdate;
            // 
            // toolStripMain
            // 
            toolStripMain.ImageScalingSize = new System.Drawing.Size(50, 50);
            toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButtonSave, toolStripSeparator4, toolStripButtonConnect, toolStripButtonDisconnect, toolStripSeparator1, toolStripButtonClearAlarm, toolStripSeparator2, toolStripButtonHome, toolStripButtonProbe, toolStripSeparator3, toolStripButtonResume, toolStripButtonPause, toolStripButtonStop, toolStripSeparator5, toolStripDropDownButtonCoordinateSystem });
            toolStripMain.Location = new System.Drawing.Point(0, 24);
            toolStripMain.Name = "toolStripMain";
            toolStripMain.Size = new System.Drawing.Size(810, 57);
            toolStripMain.TabIndex = 1;
            toolStripMain.Text = "toolStrip1";
            // 
            // toolStripButtonSave
            // 
            toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonSave.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonSave.Image");
            toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonSave.Name = "toolStripButtonSave";
            toolStripButtonSave.Size = new System.Drawing.Size(54, 54);
            toolStripButtonSave.Click += toolStripButtonSave_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(6, 57);
            // 
            // toolStripButtonConnect
            // 
            toolStripButtonConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonConnect.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonConnect.Image");
            toolStripButtonConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonConnect.Name = "toolStripButtonConnect";
            toolStripButtonConnect.Size = new System.Drawing.Size(54, 54);
            toolStripButtonConnect.Text = "toolStripButton1";
            toolStripButtonConnect.Click += toolStripButtonConnect_Click;
            // 
            // toolStripButtonDisconnect
            // 
            toolStripButtonDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonDisconnect.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonDisconnect.Image");
            toolStripButtonDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonDisconnect.Name = "toolStripButtonDisconnect";
            toolStripButtonDisconnect.Size = new System.Drawing.Size(54, 54);
            toolStripButtonDisconnect.Text = "toolStripButton1";
            toolStripButtonDisconnect.Click += toolStripButtonDisconnect_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 57);
            // 
            // toolStripButtonClearAlarm
            // 
            toolStripButtonClearAlarm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonClearAlarm.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonClearAlarm.Image");
            toolStripButtonClearAlarm.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonClearAlarm.Name = "toolStripButtonClearAlarm";
            toolStripButtonClearAlarm.Size = new System.Drawing.Size(54, 54);
            toolStripButtonClearAlarm.Text = "toolStripButton1";
            toolStripButtonClearAlarm.Click += toolStripButtonClearAlarm_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 57);
            // 
            // toolStripButtonHome
            // 
            toolStripButtonHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonHome.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonHome.Image");
            toolStripButtonHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonHome.Name = "toolStripButtonHome";
            toolStripButtonHome.Size = new System.Drawing.Size(54, 54);
            toolStripButtonHome.Text = "toolStripButton1";
            toolStripButtonHome.Click += toolStripButtonHome_Click;
            // 
            // toolStripButtonProbe
            // 
            toolStripButtonProbe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonProbe.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonProbe.Image");
            toolStripButtonProbe.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonProbe.Name = "toolStripButtonProbe";
            toolStripButtonProbe.Size = new System.Drawing.Size(54, 54);
            toolStripButtonProbe.Text = "toolStripButton2";
            toolStripButtonProbe.Click += toolStripButtonProbe_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 57);
            // 
            // toolStripButtonResume
            // 
            toolStripButtonResume.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonResume.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonResume.Image");
            toolStripButtonResume.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonResume.Name = "toolStripButtonResume";
            toolStripButtonResume.Size = new System.Drawing.Size(54, 54);
            toolStripButtonResume.Text = "toolStripButton1";
            toolStripButtonResume.Click += toolStripButtonResume_Click;
            // 
            // toolStripButtonPause
            // 
            toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonPause.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonPause.Image");
            toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonPause.Name = "toolStripButtonPause";
            toolStripButtonPause.Size = new System.Drawing.Size(54, 54);
            toolStripButtonPause.Text = "toolStripButton1";
            toolStripButtonPause.Click += toolStripButtonPause_Click;
            // 
            // toolStripButtonStop
            // 
            toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonStop.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonStop.Image");
            toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonStop.Name = "toolStripButtonStop";
            toolStripButtonStop.Size = new System.Drawing.Size(54, 54);
            toolStripButtonStop.Text = "toolStripButton2";
            toolStripButtonStop.Click += toolStripButtonStop_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new System.Drawing.Size(6, 57);
            // 
            // toolStripDropDownButtonCoordinateSystem
            // 
            toolStripDropDownButtonCoordinateSystem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripDropDownButtonCoordinateSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { g54ToolStripMenuItem, g55ToolStripMenuItem, g56ToolStripMenuItem, g57ToolStripMenuItem, g58ToolStripMenuItem, g59ToolStripMenuItem });
            toolStripDropDownButtonCoordinateSystem.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            toolStripDropDownButtonCoordinateSystem.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButtonCoordinateSystem.Image");
            toolStripDropDownButtonCoordinateSystem.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripDropDownButtonCoordinateSystem.Name = "toolStripDropDownButtonCoordinateSystem";
            toolStripDropDownButtonCoordinateSystem.Size = new System.Drawing.Size(63, 54);
            toolStripDropDownButtonCoordinateSystem.Text = "G54";
            // 
            // g54ToolStripMenuItem
            // 
            g54ToolStripMenuItem.Checked = true;
            g54ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            g54ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            g54ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            g54ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            g54ToolStripMenuItem.Name = "g54ToolStripMenuItem";
            g54ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            g54ToolStripMenuItem.Text = "G54";
            g54ToolStripMenuItem.Click += ToolstripButtonCoordinates_Click;
            // 
            // g55ToolStripMenuItem
            // 
            g55ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            g55ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            g55ToolStripMenuItem.Name = "g55ToolStripMenuItem";
            g55ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            g55ToolStripMenuItem.Text = "G55";
            g55ToolStripMenuItem.Click += ToolstripButtonCoordinates_Click;
            // 
            // g56ToolStripMenuItem
            // 
            g56ToolStripMenuItem.CheckOnClick = true;
            g56ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            g56ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            g56ToolStripMenuItem.Name = "g56ToolStripMenuItem";
            g56ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            g56ToolStripMenuItem.Text = "G56";
            g56ToolStripMenuItem.Click += ToolstripButtonCoordinates_Click;
            // 
            // g57ToolStripMenuItem
            // 
            g57ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            g57ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            g57ToolStripMenuItem.Name = "g57ToolStripMenuItem";
            g57ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            g57ToolStripMenuItem.Text = "G57";
            g57ToolStripMenuItem.Click += ToolstripButtonCoordinates_Click;
            // 
            // g58ToolStripMenuItem
            // 
            g58ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            g58ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            g58ToolStripMenuItem.Name = "g58ToolStripMenuItem";
            g58ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            g58ToolStripMenuItem.Text = "G58";
            g58ToolStripMenuItem.Click += ToolstripButtonCoordinates_Click;
            // 
            // g59ToolStripMenuItem
            // 
            g59ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            g59ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            g59ToolStripMenuItem.Name = "g59ToolStripMenuItem";
            g59ToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            g59ToolStripMenuItem.Text = "G59";
            g59ToolStripMenuItem.Click += ToolstripButtonCoordinates_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabelServerConnect, toolStripStatusLabelStatus, toolStripStatusLabelCpu, toolStripStatusLabelWarnings, toolStripStatusLabelSpindle, toolStripStatusLabelFeedRate, toolStripStatusLabelDisplayUnit, toolStripProgressBarJob });
            statusStrip.Location = new System.Drawing.Point(0, 591);
            statusStrip.Name = "statusStrip";
            statusStrip.ShowItemToolTips = true;
            statusStrip.Size = new System.Drawing.Size(810, 24);
            statusStrip.SizingGrip = false;
            statusStrip.TabIndex = 4;
            statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabelServerConnect
            // 
            toolStripStatusLabelServerConnect.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabelServerConnect.Name = "toolStripStatusLabelServerConnect";
            toolStripStatusLabelServerConnect.Size = new System.Drawing.Size(92, 19);
            toolStripStatusLabelServerConnect.Text = "Not Connected";
            // 
            // toolStripStatusLabelStatus
            // 
            toolStripStatusLabelStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            toolStripStatusLabelStatus.Size = new System.Drawing.Size(43, 19);
            toolStripStatusLabelStatus.Text = "Status";
            // 
            // toolStripStatusLabelCpu
            // 
            toolStripStatusLabelCpu.Name = "toolStripStatusLabelCpu";
            toolStripStatusLabelCpu.Size = new System.Drawing.Size(0, 19);
            // 
            // toolStripStatusLabelWarnings
            // 
            toolStripStatusLabelWarnings.AutoSize = false;
            toolStripStatusLabelWarnings.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabelWarnings.Image = (System.Drawing.Image)resources.GetObject("toolStripStatusLabelWarnings.Image");
            toolStripStatusLabelWarnings.Name = "toolStripStatusLabelWarnings";
            toolStripStatusLabelWarnings.Size = new System.Drawing.Size(29, 19);
            toolStripStatusLabelWarnings.Text = "0";
            toolStripStatusLabelWarnings.Paint += toolStripStatusLabelWarnings_Paint;
            // 
            // toolStripStatusLabelSpindle
            // 
            toolStripStatusLabelSpindle.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabelSpindle.Name = "toolStripStatusLabelSpindle";
            toolStripStatusLabelSpindle.Size = new System.Drawing.Size(50, 19);
            toolStripStatusLabelSpindle.Text = "Spindle";
            // 
            // toolStripStatusLabelFeedRate
            // 
            toolStripStatusLabelFeedRate.Name = "toolStripStatusLabelFeedRate";
            toolStripStatusLabelFeedRate.Size = new System.Drawing.Size(32, 19);
            toolStripStatusLabelFeedRate.Text = "Feed";
            // 
            // toolStripStatusLabelDisplayUnit
            // 
            toolStripStatusLabelDisplayUnit.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabelDisplayUnit.Name = "toolStripStatusLabelDisplayUnit";
            toolStripStatusLabelDisplayUnit.Size = new System.Drawing.Size(59, 19);
            toolStripStatusLabelDisplayUnit.Text = "mm/min";
            // 
            // toolStripProgressBarJob
            // 
            toolStripProgressBarJob.Name = "toolStripProgressBarJob";
            toolStripProgressBarJob.Size = new System.Drawing.Size(100, 18);
            // 
            // tabControlMain
            // 
            tabControlMain.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControlMain.Controls.Add(tabPageMain);
            tabControlMain.Controls.Add(tabPageOverrides);
            tabControlMain.Controls.Add(tabPageJog);
            tabControlMain.Controls.Add(tabPageSpindle);
            tabControlMain.Controls.Add(tabPageServiceSchedule);
            tabControlMain.Controls.Add(tabPageMachineSettings);
            tabControlMain.Controls.Add(tabPageSettings);
            tabControlMain.HotTrack = true;
            tabControlMain.Location = new System.Drawing.Point(9, 139);
            tabControlMain.MinimumSize = new System.Drawing.Size(773, 270);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new System.Drawing.Size(787, 270);
            tabControlMain.TabIndex = 2;
            tabControlMain.Resize += tabControlMain_Resize;
            // 
            // tabPageMain
            // 
            tabPageMain.BackColor = System.Drawing.Color.White;
            tabPageMain.Controls.Add(lblTotalLines);
            tabPageMain.Controls.Add(lblCommandQueueSize);
            tabPageMain.Controls.Add(lblQueueSize);
            tabPageMain.Controls.Add(lblBufferSize);
            tabPageMain.Controls.Add(lblJobTime);
            tabPageMain.Controls.Add(gCodeAnalysesDetails);
            tabPageMain.Controls.Add(machinePositionGeneral);
            tabPageMain.Location = new System.Drawing.Point(4, 24);
            tabPageMain.Name = "tabPageMain";
            tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            tabPageMain.Size = new System.Drawing.Size(779, 242);
            tabPageMain.TabIndex = 0;
            tabPageMain.Text = "General";
            // 
            // lblTotalLines
            // 
            lblTotalLines.AutoSize = true;
            lblTotalLines.Location = new System.Drawing.Point(8, 193);
            lblTotalLines.Name = "lblTotalLines";
            lblTotalLines.Size = new System.Drawing.Size(38, 15);
            lblTotalLines.TabIndex = 6;
            lblTotalLines.Text = "label3";
            // 
            // lblCommandQueueSize
            // 
            lblCommandQueueSize.AutoSize = true;
            lblCommandQueueSize.Location = new System.Drawing.Point(8, 175);
            lblCommandQueueSize.Name = "lblCommandQueueSize";
            lblCommandQueueSize.Size = new System.Drawing.Size(38, 15);
            lblCommandQueueSize.TabIndex = 5;
            lblCommandQueueSize.Text = "label3";
            // 
            // lblQueueSize
            // 
            lblQueueSize.AutoSize = true;
            lblQueueSize.Location = new System.Drawing.Point(8, 157);
            lblQueueSize.Name = "lblQueueSize";
            lblQueueSize.Size = new System.Drawing.Size(38, 15);
            lblQueueSize.TabIndex = 4;
            lblQueueSize.Text = "label2";
            // 
            // lblBufferSize
            // 
            lblBufferSize.AutoSize = true;
            lblBufferSize.Location = new System.Drawing.Point(8, 139);
            lblBufferSize.Name = "lblBufferSize";
            lblBufferSize.Size = new System.Drawing.Size(38, 15);
            lblBufferSize.TabIndex = 3;
            lblBufferSize.Text = "label1";
            // 
            // lblJobTime
            // 
            lblJobTime.AutoSize = true;
            lblJobTime.Location = new System.Drawing.Point(8, 121);
            lblJobTime.Name = "lblJobTime";
            lblJobTime.Size = new System.Drawing.Size(67, 15);
            lblJobTime.TabIndex = 2;
            lblJobTime.Text = "Total Time: ";
            // 
            // gCodeAnalysesDetails
            // 
            gCodeAnalysesDetails.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            gCodeAnalysesDetails.Location = new System.Drawing.Point(325, 6);
            gCodeAnalysesDetails.MinimumSize = new System.Drawing.Size(433, 218);
            gCodeAnalysesDetails.Name = "gCodeAnalysesDetails";
            gCodeAnalysesDetails.Size = new System.Drawing.Size(434, 226);
            gCodeAnalysesDetails.TabIndex = 1;
            // 
            // machinePositionGeneral
            // 
            machinePositionGeneral.DisplayFeedbackUnit = GSendShared.FeedbackUnit.Mm;
            machinePositionGeneral.Location = new System.Drawing.Point(6, 6);
            machinePositionGeneral.Name = "machinePositionGeneral";
            machinePositionGeneral.Size = new System.Drawing.Size(314, 112);
            machinePositionGeneral.TabIndex = 0;
            // 
            // tabPageOverrides
            // 
            tabPageOverrides.BackColor = System.Drawing.Color.White;
            tabPageOverrides.Controls.Add(cbOverridesDisable);
            tabPageOverrides.Controls.Add(labelSpeedPercent);
            tabPageOverrides.Controls.Add(trackBarPercent);
            tabPageOverrides.Controls.Add(machinePositionOverrides);
            tabPageOverrides.Controls.Add(selectionOverrideSpindle);
            tabPageOverrides.Controls.Add(selectionOverrideZDown);
            tabPageOverrides.Controls.Add(selectionOverrideRapids);
            tabPageOverrides.Controls.Add(selectionOverrideZUp);
            tabPageOverrides.Controls.Add(selectionOverrideXY);
            tabPageOverrides.Location = new System.Drawing.Point(4, 24);
            tabPageOverrides.Name = "tabPageOverrides";
            tabPageOverrides.Padding = new System.Windows.Forms.Padding(3);
            tabPageOverrides.Size = new System.Drawing.Size(779, 242);
            tabPageOverrides.TabIndex = 1;
            tabPageOverrides.Text = "Overrides";
            // 
            // cbOverridesDisable
            // 
            cbOverridesDisable.AutoSize = true;
            cbOverridesDisable.Location = new System.Drawing.Point(6, 217);
            cbOverridesDisable.Name = "cbOverridesDisable";
            cbOverridesDisable.Size = new System.Drawing.Size(109, 19);
            cbOverridesDisable.TabIndex = 7;
            cbOverridesDisable.Text = "override disable";
            cbOverridesDisable.UseVisualStyleBackColor = true;
            // 
            // labelSpeedPercent
            // 
            labelSpeedPercent.AutoSize = true;
            labelSpeedPercent.Location = new System.Drawing.Point(6, 171);
            labelSpeedPercent.Name = "labelSpeedPercent";
            labelSpeedPercent.Size = new System.Drawing.Size(17, 15);
            labelSpeedPercent.TabIndex = 2;
            labelSpeedPercent.Text = "%";
            // 
            // trackBarPercent
            // 
            trackBarPercent.Location = new System.Drawing.Point(6, 123);
            trackBarPercent.Maximum = 100;
            trackBarPercent.Minimum = 1;
            trackBarPercent.Name = "trackBarPercent";
            trackBarPercent.Size = new System.Drawing.Size(308, 45);
            trackBarPercent.TabIndex = 1;
            trackBarPercent.TickFrequency = 5;
            trackBarPercent.Value = 1;
            trackBarPercent.ValueChanged += trackBarPercent_ValueChanged;
            // 
            // machinePositionOverrides
            // 
            machinePositionOverrides.DisplayFeedbackUnit = GSendShared.FeedbackUnit.Mm;
            machinePositionOverrides.Location = new System.Drawing.Point(6, 6);
            machinePositionOverrides.Name = "machinePositionOverrides";
            machinePositionOverrides.Size = new System.Drawing.Size(314, 112);
            machinePositionOverrides.TabIndex = 0;
            // 
            // selectionOverrideXY
            // 
            selectionOverrideXY.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            selectionOverrideXY.Checked = false;
            selectionOverrideXY.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            selectionOverrideXY.GroupName = "X/Y";
            selectionOverrideXY.HandleMouseWheel = false;
            selectionOverrideXY.HasDisplayUnits = true;
            selectionOverrideXY.LabelFormat = "{0}";
            selectionOverrideXY.LabelValue = null;
            selectionOverrideXY.LargeTickChange = 50;
            selectionOverrideXY.Location = new System.Drawing.Point(422, 6);
            selectionOverrideXY.Maximum = 10;
            selectionOverrideXY.Minimum = 0;
            selectionOverrideXY.Name = "selectionOverrideXY";
            selectionOverrideXY.Size = new System.Drawing.Size(82, 228);
            selectionOverrideXY.SmallTickChange = 5;
            selectionOverrideXY.TabIndex = 9;
            selectionOverrideXY.TickFrequency = 1;
            selectionOverrideXY.Value = 0;
            // 
            // tabPageJog
            // 
            tabPageJog.BackColor = System.Drawing.Color.White;
            tabPageJog.Controls.Add(btnZeroAll);
            tabPageJog.Controls.Add(btnZeroZ);
            tabPageJog.Controls.Add(btnZeroY);
            tabPageJog.Controls.Add(btnZeroX);
            tabPageJog.Controls.Add(machinePositionJog);
            tabPageJog.Controls.Add(jogControl);
            tabPageJog.Location = new System.Drawing.Point(4, 24);
            tabPageJog.Name = "tabPageJog";
            tabPageJog.Padding = new System.Windows.Forms.Padding(3);
            tabPageJog.Size = new System.Drawing.Size(779, 242);
            tabPageJog.TabIndex = 2;
            tabPageJog.Text = "Jog";
            // 
            // btnZeroAll
            // 
            btnZeroAll.Image = (System.Drawing.Image)resources.GetObject("btnZeroAll.Image");
            btnZeroAll.Location = new System.Drawing.Point(249, 123);
            btnZeroAll.Name = "btnZeroAll";
            btnZeroAll.Size = new System.Drawing.Size(64, 64);
            btnZeroAll.TabIndex = 4;
            btnZeroAll.UseVisualStyleBackColor = true;
            btnZeroAll.Click += SetZeroForAxes;
            // 
            // btnZeroZ
            // 
            btnZeroZ.Image = (System.Drawing.Image)resources.GetObject("btnZeroZ.Image");
            btnZeroZ.Location = new System.Drawing.Point(168, 123);
            btnZeroZ.Name = "btnZeroZ";
            btnZeroZ.Size = new System.Drawing.Size(64, 64);
            btnZeroZ.TabIndex = 3;
            btnZeroZ.UseVisualStyleBackColor = true;
            btnZeroZ.Click += SetZeroForAxes;
            // 
            // btnZeroY
            // 
            btnZeroY.Image = (System.Drawing.Image)resources.GetObject("btnZeroY.Image");
            btnZeroY.Location = new System.Drawing.Point(87, 123);
            btnZeroY.Name = "btnZeroY";
            btnZeroY.Size = new System.Drawing.Size(64, 64);
            btnZeroY.TabIndex = 2;
            btnZeroY.UseVisualStyleBackColor = true;
            btnZeroY.Click += SetZeroForAxes;
            // 
            // btnZeroX
            // 
            btnZeroX.Image = (System.Drawing.Image)resources.GetObject("btnZeroX.Image");
            btnZeroX.Location = new System.Drawing.Point(6, 123);
            btnZeroX.Name = "btnZeroX";
            btnZeroX.Size = new System.Drawing.Size(64, 64);
            btnZeroX.TabIndex = 1;
            btnZeroX.UseVisualStyleBackColor = true;
            btnZeroX.Click += SetZeroForAxes;
            // 
            // machinePositionJog
            // 
            machinePositionJog.DisplayFeedbackUnit = GSendShared.FeedbackUnit.Mm;
            machinePositionJog.Location = new System.Drawing.Point(6, 6);
            machinePositionJog.Name = "machinePositionJog";
            machinePositionJog.Size = new System.Drawing.Size(314, 112);
            machinePositionJog.TabIndex = 0;
            // 
            // tabPageSpindle
            // 
            tabPageSpindle.BackColor = System.Drawing.Color.White;
            tabPageSpindle.Controls.Add(grpBoxSpindleSpeed);
            tabPageSpindle.Controls.Add(lblDelaySpindleStart);
            tabPageSpindle.Controls.Add(trackBarDelaySpindle);
            tabPageSpindle.Controls.Add(cbSoftStart);
            tabPageSpindle.Controls.Add(lblSpindleType);
            tabPageSpindle.Controls.Add(cmbSpindleType);
            tabPageSpindle.Location = new System.Drawing.Point(4, 24);
            tabPageSpindle.Name = "tabPageSpindle";
            tabPageSpindle.Padding = new System.Windows.Forms.Padding(3);
            tabPageSpindle.Size = new System.Drawing.Size(779, 242);
            tabPageSpindle.TabIndex = 4;
            tabPageSpindle.Text = "Spindle";
            // 
            // grpBoxSpindleSpeed
            // 
            grpBoxSpindleSpeed.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            grpBoxSpindleSpeed.Controls.Add(cbSpindleCounterClockwise);
            grpBoxSpindleSpeed.Controls.Add(btnSpindleStop);
            grpBoxSpindleSpeed.Controls.Add(btnSpindleStart);
            grpBoxSpindleSpeed.Controls.Add(lblSpindleSpeed);
            grpBoxSpindleSpeed.Controls.Add(trackBarSpindleSpeed);
            grpBoxSpindleSpeed.Location = new System.Drawing.Point(571, 6);
            grpBoxSpindleSpeed.Name = "grpBoxSpindleSpeed";
            grpBoxSpindleSpeed.Size = new System.Drawing.Size(188, 227);
            grpBoxSpindleSpeed.TabIndex = 5;
            grpBoxSpindleSpeed.TabStop = false;
            grpBoxSpindleSpeed.Text = "groupBox1";
            // 
            // cbSpindleCounterClockwise
            // 
            cbSpindleCounterClockwise.Location = new System.Drawing.Point(70, 99);
            cbSpindleCounterClockwise.Name = "cbSpindleCounterClockwise";
            cbSpindleCounterClockwise.Size = new System.Drawing.Size(83, 61);
            cbSpindleCounterClockwise.TabIndex = 4;
            cbSpindleCounterClockwise.Text = "checkBox1";
            cbSpindleCounterClockwise.UseVisualStyleBackColor = true;
            // 
            // btnSpindleStop
            // 
            btnSpindleStop.Location = new System.Drawing.Point(70, 62);
            btnSpindleStop.Name = "btnSpindleStop";
            btnSpindleStop.Size = new System.Drawing.Size(75, 23);
            btnSpindleStop.TabIndex = 3;
            btnSpindleStop.Text = "button1";
            btnSpindleStop.UseVisualStyleBackColor = true;
            btnSpindleStop.Click += btnSpindleStop_Click;
            // 
            // btnSpindleStart
            // 
            btnSpindleStart.Location = new System.Drawing.Point(70, 29);
            btnSpindleStart.Name = "btnSpindleStart";
            btnSpindleStart.Size = new System.Drawing.Size(75, 23);
            btnSpindleStart.TabIndex = 2;
            btnSpindleStart.Text = "button1";
            btnSpindleStart.UseVisualStyleBackColor = true;
            btnSpindleStart.Click += btnSpindleStart_Click;
            // 
            // lblSpindleSpeed
            // 
            lblSpindleSpeed.AutoSize = true;
            lblSpindleSpeed.Location = new System.Drawing.Point(19, 201);
            lblSpindleSpeed.Name = "lblSpindleSpeed";
            lblSpindleSpeed.Size = new System.Drawing.Size(38, 15);
            lblSpindleSpeed.TabIndex = 1;
            lblSpindleSpeed.Text = "label1";
            // 
            // trackBarSpindleSpeed
            // 
            trackBarSpindleSpeed.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            trackBarSpindleSpeed.LargeChange = 200;
            trackBarSpindleSpeed.Location = new System.Drawing.Point(19, 29);
            trackBarSpindleSpeed.Maximum = 20000;
            trackBarSpindleSpeed.Name = "trackBarSpindleSpeed";
            trackBarSpindleSpeed.Orientation = System.Windows.Forms.Orientation.Vertical;
            trackBarSpindleSpeed.Size = new System.Drawing.Size(45, 165);
            trackBarSpindleSpeed.SmallChange = 50;
            trackBarSpindleSpeed.TabIndex = 0;
            trackBarSpindleSpeed.TickFrequency = 300;
            trackBarSpindleSpeed.ValueChanged += trackBarSpindleSpeed_ValueChanged;
            // 
            // lblDelaySpindleStart
            // 
            lblDelaySpindleStart.AutoSize = true;
            lblDelaySpindleStart.Location = new System.Drawing.Point(211, 72);
            lblDelaySpindleStart.Name = "lblDelaySpindleStart";
            lblDelaySpindleStart.Size = new System.Drawing.Size(38, 15);
            lblDelaySpindleStart.TabIndex = 4;
            lblDelaySpindleStart.Text = "label1";
            // 
            // trackBarDelaySpindle
            // 
            trackBarDelaySpindle.Location = new System.Drawing.Point(211, 24);
            trackBarDelaySpindle.Maximum = 120;
            trackBarDelaySpindle.Name = "trackBarDelaySpindle";
            trackBarDelaySpindle.Size = new System.Drawing.Size(240, 45);
            trackBarDelaySpindle.TabIndex = 3;
            trackBarDelaySpindle.TickFrequency = 10;
            trackBarDelaySpindle.Value = 30;
            // 
            // cbSoftStart
            // 
            cbSoftStart.AutoSize = true;
            cbSoftStart.Location = new System.Drawing.Point(6, 77);
            cbSoftStart.Name = "cbSoftStart";
            cbSoftStart.Size = new System.Drawing.Size(83, 19);
            cbSoftStart.TabIndex = 2;
            cbSoftStart.Text = "checkBox1";
            cbSoftStart.UseVisualStyleBackColor = true;
            // 
            // lblSpindleType
            // 
            lblSpindleType.AutoSize = true;
            lblSpindleType.Location = new System.Drawing.Point(6, 6);
            lblSpindleType.Name = "lblSpindleType";
            lblSpindleType.Size = new System.Drawing.Size(38, 15);
            lblSpindleType.TabIndex = 0;
            lblSpindleType.Text = "label1";
            // 
            // cmbSpindleType
            // 
            cmbSpindleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbSpindleType.FormattingEnabled = true;
            cmbSpindleType.Location = new System.Drawing.Point(6, 24);
            cmbSpindleType.Name = "cmbSpindleType";
            cmbSpindleType.Size = new System.Drawing.Size(164, 23);
            cmbSpindleType.TabIndex = 1;
            cmbSpindleType.SelectedIndexChanged += cmbSpindleType_SelectedIndexChanged;
            // 
            // tabPageServiceSchedule
            // 
            tabPageServiceSchedule.BackColor = System.Drawing.Color.White;
            tabPageServiceSchedule.Controls.Add(lvServices);
            tabPageServiceSchedule.Controls.Add(btnServiceRefresh);
            tabPageServiceSchedule.Controls.Add(lblSpindleHoursRemaining);
            tabPageServiceSchedule.Controls.Add(lblServiceDate);
            tabPageServiceSchedule.Controls.Add(btnServiceReset);
            tabPageServiceSchedule.Controls.Add(lblNextService);
            tabPageServiceSchedule.Controls.Add(lblSpindleHours);
            tabPageServiceSchedule.Controls.Add(trackBarServiceSpindleHours);
            tabPageServiceSchedule.Controls.Add(cbMaintainServiceSchedule);
            tabPageServiceSchedule.Controls.Add(trackBarServiceWeeks);
            tabPageServiceSchedule.Controls.Add(lblServiceSchedule);
            tabPageServiceSchedule.Location = new System.Drawing.Point(4, 24);
            tabPageServiceSchedule.Name = "tabPageServiceSchedule";
            tabPageServiceSchedule.Padding = new System.Windows.Forms.Padding(3);
            tabPageServiceSchedule.Size = new System.Drawing.Size(779, 242);
            tabPageServiceSchedule.TabIndex = 6;
            tabPageServiceSchedule.Text = "Service Scgedule";
            // 
            // lvServices
            // 
            lvServices.AllowColumnReorder = true;
            lvServices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnServiceHeaderDateTime, columnServiceHeaderServiceType, columnServiceHeaderSpindleHours });
            lvServices.Location = new System.Drawing.Point(332, 126);
            lvServices.MultiSelect = false;
            lvServices.Name = "lvServices";
            lvServices.OwnerDraw = true;
            lvServices.SaveName = "";
            lvServices.ShowItemToolTips = true;
            lvServices.ShowToolTip = false;
            lvServices.Size = new System.Drawing.Size(427, 110);
            lvServices.TabIndex = 11;
            lvServices.UseCompatibleStateImageBehavior = false;
            lvServices.View = System.Windows.Forms.View.Details;
            // 
            // columnServiceHeaderDateTime
            // 
            columnServiceHeaderDateTime.Width = 130;
            // 
            // columnServiceHeaderServiceType
            // 
            columnServiceHeaderServiceType.Width = 80;
            // 
            // columnServiceHeaderSpindleHours
            // 
            columnServiceHeaderSpindleHours.Width = 180;
            // 
            // btnServiceRefresh
            // 
            btnServiceRefresh.Location = new System.Drawing.Point(657, 89);
            btnServiceRefresh.Name = "btnServiceRefresh";
            btnServiceRefresh.Size = new System.Drawing.Size(102, 23);
            btnServiceRefresh.TabIndex = 10;
            btnServiceRefresh.Text = "button1";
            btnServiceRefresh.UseVisualStyleBackColor = true;
            btnServiceRefresh.Click += btnServiceRefresh_Click;
            // 
            // lblSpindleHoursRemaining
            // 
            lblSpindleHoursRemaining.AutoSize = true;
            lblSpindleHoursRemaining.Location = new System.Drawing.Point(332, 73);
            lblSpindleHoursRemaining.Name = "lblSpindleHoursRemaining";
            lblSpindleHoursRemaining.Size = new System.Drawing.Size(38, 15);
            lblSpindleHoursRemaining.TabIndex = 8;
            lblSpindleHoursRemaining.Text = "label1";
            // 
            // lblServiceDate
            // 
            lblServiceDate.AutoSize = true;
            lblServiceDate.Location = new System.Drawing.Point(332, 43);
            lblServiceDate.Name = "lblServiceDate";
            lblServiceDate.Size = new System.Drawing.Size(38, 15);
            lblServiceDate.TabIndex = 7;
            lblServiceDate.Text = "label1";
            // 
            // btnServiceReset
            // 
            btnServiceReset.Location = new System.Drawing.Point(657, 12);
            btnServiceReset.Name = "btnServiceReset";
            btnServiceReset.Size = new System.Drawing.Size(102, 23);
            btnServiceReset.TabIndex = 6;
            btnServiceReset.Text = "button1";
            btnServiceReset.UseVisualStyleBackColor = true;
            btnServiceReset.Click += btnServiceReset_Click;
            // 
            // lblNextService
            // 
            lblNextService.AutoSize = true;
            lblNextService.Location = new System.Drawing.Point(332, 16);
            lblNextService.Name = "lblNextService";
            lblNextService.Size = new System.Drawing.Size(38, 15);
            lblNextService.TabIndex = 5;
            lblNextService.Text = "label1";
            // 
            // lblSpindleHours
            // 
            lblSpindleHours.AutoSize = true;
            lblSpindleHours.Location = new System.Drawing.Point(18, 168);
            lblSpindleHours.Name = "lblSpindleHours";
            lblSpindleHours.Size = new System.Drawing.Size(38, 15);
            lblSpindleHours.TabIndex = 4;
            lblSpindleHours.Text = "label1";
            // 
            // trackBarServiceSpindleHours
            // 
            trackBarServiceSpindleHours.Location = new System.Drawing.Point(18, 120);
            trackBarServiceSpindleHours.Maximum = 500;
            trackBarServiceSpindleHours.Minimum = 5;
            trackBarServiceSpindleHours.Name = "trackBarServiceSpindleHours";
            trackBarServiceSpindleHours.Size = new System.Drawing.Size(286, 45);
            trackBarServiceSpindleHours.TabIndex = 3;
            trackBarServiceSpindleHours.TickFrequency = 20;
            trackBarServiceSpindleHours.Value = 5;
            // 
            // cbMaintainServiceSchedule
            // 
            cbMaintainServiceSchedule.AutoSize = true;
            cbMaintainServiceSchedule.Location = new System.Drawing.Point(6, 6);
            cbMaintainServiceSchedule.Name = "cbMaintainServiceSchedule";
            cbMaintainServiceSchedule.Size = new System.Drawing.Size(83, 19);
            cbMaintainServiceSchedule.TabIndex = 2;
            cbMaintainServiceSchedule.Text = "checkBox1";
            cbMaintainServiceSchedule.UseVisualStyleBackColor = true;
            // 
            // trackBarServiceWeeks
            // 
            trackBarServiceWeeks.LargeChange = 1;
            trackBarServiceWeeks.Location = new System.Drawing.Point(18, 43);
            trackBarServiceWeeks.Maximum = 52;
            trackBarServiceWeeks.Minimum = 1;
            trackBarServiceWeeks.Name = "trackBarServiceWeeks";
            trackBarServiceWeeks.Size = new System.Drawing.Size(286, 45);
            trackBarServiceWeeks.TabIndex = 1;
            trackBarServiceWeeks.TickFrequency = 3;
            trackBarServiceWeeks.Value = 1;
            // 
            // lblServiceSchedule
            // 
            lblServiceSchedule.AutoSize = true;
            lblServiceSchedule.Location = new System.Drawing.Point(18, 91);
            lblServiceSchedule.Name = "lblServiceSchedule";
            lblServiceSchedule.Size = new System.Drawing.Size(38, 15);
            lblServiceSchedule.TabIndex = 0;
            lblServiceSchedule.Text = "label1";
            // 
            // tabPageMachineSettings
            // 
            tabPageMachineSettings.BackColor = System.Drawing.Color.White;
            tabPageMachineSettings.Controls.Add(btnApplyGrblUpdates);
            tabPageMachineSettings.Controls.Add(txtGrblUpdates);
            tabPageMachineSettings.Controls.Add(lblPropertyDesc);
            tabPageMachineSettings.Controls.Add(lblPropertyHeader);
            tabPageMachineSettings.Controls.Add(propertyGridGrblSettings);
            tabPageMachineSettings.Location = new System.Drawing.Point(4, 24);
            tabPageMachineSettings.Name = "tabPageMachineSettings";
            tabPageMachineSettings.Padding = new System.Windows.Forms.Padding(3);
            tabPageMachineSettings.Size = new System.Drawing.Size(779, 242);
            tabPageMachineSettings.TabIndex = 3;
            tabPageMachineSettings.Text = "Machine Settings";
            // 
            // btnApplyGrblUpdates
            // 
            btnApplyGrblUpdates.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnApplyGrblUpdates.Enabled = false;
            btnApplyGrblUpdates.Location = new System.Drawing.Point(662, 6);
            btnApplyGrblUpdates.Name = "btnApplyGrblUpdates";
            btnApplyGrblUpdates.Size = new System.Drawing.Size(75, 23);
            btnApplyGrblUpdates.TabIndex = 4;
            btnApplyGrblUpdates.Text = "Apply";
            btnApplyGrblUpdates.UseVisualStyleBackColor = true;
            btnApplyGrblUpdates.Click += btnApplyGrblUpdates_Click;
            // 
            // txtGrblUpdates
            // 
            txtGrblUpdates.AcceptsReturn = true;
            txtGrblUpdates.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtGrblUpdates.Location = new System.Drawing.Point(380, 6);
            txtGrblUpdates.Multiline = true;
            txtGrblUpdates.Name = "txtGrblUpdates";
            txtGrblUpdates.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            txtGrblUpdates.Size = new System.Drawing.Size(276, 130);
            txtGrblUpdates.TabIndex = 1;
            txtGrblUpdates.TextChanged += txtGrblUpdates_TextChanged;
            // 
            // lblPropertyDesc
            // 
            lblPropertyDesc.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblPropertyDesc.Location = new System.Drawing.Point(380, 154);
            lblPropertyDesc.Name = "lblPropertyDesc";
            lblPropertyDesc.Size = new System.Drawing.Size(383, 82);
            lblPropertyDesc.TabIndex = 3;
            lblPropertyDesc.Text = "label2";
            // 
            // lblPropertyHeader
            // 
            lblPropertyHeader.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            lblPropertyHeader.AutoSize = true;
            lblPropertyHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblPropertyHeader.Location = new System.Drawing.Point(380, 139);
            lblPropertyHeader.Name = "lblPropertyHeader";
            lblPropertyHeader.Size = new System.Drawing.Size(40, 15);
            lblPropertyHeader.TabIndex = 2;
            lblPropertyHeader.Text = "label1";
            // 
            // propertyGridGrblSettings
            // 
            propertyGridGrblSettings.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            propertyGridGrblSettings.HelpVisible = false;
            propertyGridGrblSettings.Location = new System.Drawing.Point(6, 6);
            propertyGridGrblSettings.Name = "propertyGridGrblSettings";
            propertyGridGrblSettings.Size = new System.Drawing.Size(368, 230);
            propertyGridGrblSettings.TabIndex = 0;
            propertyGridGrblSettings.PropertyValueChanged += propertyGridGrblSettings_PropertyValueChanged;
            propertyGridGrblSettings.SelectedGridItemChanged += propertyGridGrblSettings_SelectedGridItemChanged;
            // 
            // tabPageSettings
            // 
            tabPageSettings.BackColor = System.Drawing.Color.White;
            tabPageSettings.Controls.Add(lblLayerHeightMeasure);
            tabPageSettings.Controls.Add(numericLayerHeight);
            tabPageSettings.Controls.Add(cbLayerHeightWarning);
            tabPageSettings.Controls.Add(grpFeedDisplay);
            tabPageSettings.Controls.Add(grpDisplayUnits);
            tabPageSettings.Controls.Add(cbCorrectMode);
            tabPageSettings.Controls.Add(cbFloodCoolant);
            tabPageSettings.Controls.Add(cbMistCoolant);
            tabPageSettings.Controls.Add(cbToolChanger);
            tabPageSettings.Controls.Add(cbLimitSwitches);
            tabPageSettings.Controls.Add(probingCommand1);
            tabPageSettings.Location = new System.Drawing.Point(4, 24);
            tabPageSettings.Name = "tabPageSettings";
            tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            tabPageSettings.Size = new System.Drawing.Size(779, 242);
            tabPageSettings.TabIndex = 8;
            tabPageSettings.Text = "Settings";
            // 
            // lblLayerHeightMeasure
            // 
            lblLayerHeightMeasure.AutoSize = true;
            lblLayerHeightMeasure.Location = new System.Drawing.Point(299, 132);
            lblLayerHeightMeasure.Name = "lblLayerHeightMeasure";
            lblLayerHeightMeasure.Size = new System.Drawing.Size(38, 15);
            lblLayerHeightMeasure.TabIndex = 10;
            lblLayerHeightMeasure.Text = "label1";
            // 
            // numericLayerHeight
            // 
            numericLayerHeight.DecimalPlaces = 1;
            numericLayerHeight.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericLayerHeight.Location = new System.Drawing.Point(235, 130);
            numericLayerHeight.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numericLayerHeight.Name = "numericLayerHeight";
            numericLayerHeight.Size = new System.Drawing.Size(58, 23);
            numericLayerHeight.TabIndex = 9;
            numericLayerHeight.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            // 
            // cbLayerHeightWarning
            // 
            cbLayerHeightWarning.AutoSize = true;
            cbLayerHeightWarning.Location = new System.Drawing.Point(6, 131);
            cbLayerHeightWarning.Name = "cbLayerHeightWarning";
            cbLayerHeightWarning.Size = new System.Drawing.Size(148, 19);
            cbLayerHeightWarning.TabIndex = 8;
            cbLayerHeightWarning.Text = "cbLayerHeightWarning";
            cbLayerHeightWarning.UseVisualStyleBackColor = true;
            // 
            // grpFeedDisplay
            // 
            grpFeedDisplay.Controls.Add(rbFeedDisplayInchMin);
            grpFeedDisplay.Controls.Add(rbFeedDisplayInchSec);
            grpFeedDisplay.Controls.Add(rbFeedDisplayMmSec);
            grpFeedDisplay.Controls.Add(rbFeedDisplayMmMin);
            grpFeedDisplay.Location = new System.Drawing.Point(274, 154);
            grpFeedDisplay.Name = "grpFeedDisplay";
            grpFeedDisplay.Size = new System.Drawing.Size(221, 82);
            grpFeedDisplay.TabIndex = 6;
            grpFeedDisplay.TabStop = false;
            grpFeedDisplay.Text = "groupBox1";
            // 
            // rbFeedDisplayInchMin
            // 
            rbFeedDisplayInchMin.AutoSize = true;
            rbFeedDisplayInchMin.Location = new System.Drawing.Point(121, 22);
            rbFeedDisplayInchMin.Name = "rbFeedDisplayInchMin";
            rbFeedDisplayInchMin.Size = new System.Drawing.Size(94, 19);
            rbFeedDisplayInchMin.TabIndex = 2;
            rbFeedDisplayInchMin.TabStop = true;
            rbFeedDisplayInchMin.Text = "radioButton4";
            rbFeedDisplayInchMin.UseVisualStyleBackColor = true;
            // 
            // rbFeedDisplayInchSec
            // 
            rbFeedDisplayInchSec.AutoSize = true;
            rbFeedDisplayInchSec.Location = new System.Drawing.Point(121, 47);
            rbFeedDisplayInchSec.Name = "rbFeedDisplayInchSec";
            rbFeedDisplayInchSec.Size = new System.Drawing.Size(94, 19);
            rbFeedDisplayInchSec.TabIndex = 3;
            rbFeedDisplayInchSec.TabStop = true;
            rbFeedDisplayInchSec.Text = "radioButton3";
            rbFeedDisplayInchSec.UseVisualStyleBackColor = true;
            // 
            // rbFeedDisplayMmSec
            // 
            rbFeedDisplayMmSec.AutoSize = true;
            rbFeedDisplayMmSec.Location = new System.Drawing.Point(6, 47);
            rbFeedDisplayMmSec.Name = "rbFeedDisplayMmSec";
            rbFeedDisplayMmSec.Size = new System.Drawing.Size(94, 19);
            rbFeedDisplayMmSec.TabIndex = 1;
            rbFeedDisplayMmSec.TabStop = true;
            rbFeedDisplayMmSec.Text = "radioButton2";
            rbFeedDisplayMmSec.UseVisualStyleBackColor = true;
            // 
            // rbFeedDisplayMmMin
            // 
            rbFeedDisplayMmMin.AutoSize = true;
            rbFeedDisplayMmMin.Location = new System.Drawing.Point(6, 22);
            rbFeedDisplayMmMin.Name = "rbFeedDisplayMmMin";
            rbFeedDisplayMmMin.Size = new System.Drawing.Size(94, 19);
            rbFeedDisplayMmMin.TabIndex = 0;
            rbFeedDisplayMmMin.TabStop = true;
            rbFeedDisplayMmMin.Text = "radioButton1";
            rbFeedDisplayMmMin.UseVisualStyleBackColor = true;
            // 
            // grpDisplayUnits
            // 
            grpDisplayUnits.Controls.Add(cbAutoSelectFeedbackUnit);
            grpDisplayUnits.Controls.Add(rbFeedbackInch);
            grpDisplayUnits.Controls.Add(rbFeedbackMm);
            grpDisplayUnits.Location = new System.Drawing.Point(6, 154);
            grpDisplayUnits.Name = "grpDisplayUnits";
            grpDisplayUnits.Size = new System.Drawing.Size(262, 82);
            grpDisplayUnits.TabIndex = 5;
            grpDisplayUnits.TabStop = false;
            grpDisplayUnits.Text = "groupBox1";
            // 
            // cbAutoSelectFeedbackUnit
            // 
            cbAutoSelectFeedbackUnit.Location = new System.Drawing.Point(121, 22);
            cbAutoSelectFeedbackUnit.Name = "cbAutoSelectFeedbackUnit";
            cbAutoSelectFeedbackUnit.Size = new System.Drawing.Size(135, 43);
            cbAutoSelectFeedbackUnit.TabIndex = 2;
            cbAutoSelectFeedbackUnit.Text = "Automatically select from G-Code file";
            cbAutoSelectFeedbackUnit.UseVisualStyleBackColor = true;
            // 
            // rbFeedbackInch
            // 
            rbFeedbackInch.AutoSize = true;
            rbFeedbackInch.Location = new System.Drawing.Point(6, 47);
            rbFeedbackInch.Name = "rbFeedbackInch";
            rbFeedbackInch.Size = new System.Drawing.Size(94, 19);
            rbFeedbackInch.TabIndex = 1;
            rbFeedbackInch.TabStop = true;
            rbFeedbackInch.Text = "radioButton2";
            rbFeedbackInch.UseVisualStyleBackColor = true;
            // 
            // rbFeedbackMm
            // 
            rbFeedbackMm.AutoSize = true;
            rbFeedbackMm.Location = new System.Drawing.Point(6, 22);
            rbFeedbackMm.Name = "rbFeedbackMm";
            rbFeedbackMm.Size = new System.Drawing.Size(94, 19);
            rbFeedbackMm.TabIndex = 0;
            rbFeedbackMm.TabStop = true;
            rbFeedbackMm.Text = "radioButton1";
            rbFeedbackMm.UseVisualStyleBackColor = true;
            // 
            // cbCorrectMode
            // 
            cbCorrectMode.AutoSize = true;
            cbCorrectMode.Location = new System.Drawing.Point(6, 106);
            cbCorrectMode.Name = "cbCorrectMode";
            cbCorrectMode.Size = new System.Drawing.Size(83, 19);
            cbCorrectMode.TabIndex = 4;
            cbCorrectMode.Text = "checkBox3";
            cbCorrectMode.UseVisualStyleBackColor = true;
            // 
            // cbFloodCoolant
            // 
            cbFloodCoolant.AutoSize = true;
            cbFloodCoolant.Location = new System.Drawing.Point(6, 81);
            cbFloodCoolant.Name = "cbFloodCoolant";
            cbFloodCoolant.Size = new System.Drawing.Size(83, 19);
            cbFloodCoolant.TabIndex = 3;
            cbFloodCoolant.Text = "checkBox2";
            cbFloodCoolant.UseVisualStyleBackColor = true;
            // 
            // cbMistCoolant
            // 
            cbMistCoolant.AutoSize = true;
            cbMistCoolant.Location = new System.Drawing.Point(6, 56);
            cbMistCoolant.Name = "cbMistCoolant";
            cbMistCoolant.Size = new System.Drawing.Size(83, 19);
            cbMistCoolant.TabIndex = 2;
            cbMistCoolant.Text = "checkBox1";
            cbMistCoolant.UseVisualStyleBackColor = true;
            // 
            // cbToolChanger
            // 
            cbToolChanger.AutoSize = true;
            cbToolChanger.Location = new System.Drawing.Point(6, 31);
            cbToolChanger.Name = "cbToolChanger";
            cbToolChanger.Size = new System.Drawing.Size(83, 19);
            cbToolChanger.TabIndex = 1;
            cbToolChanger.Text = "checkBox2";
            cbToolChanger.UseVisualStyleBackColor = true;
            // 
            // cbLimitSwitches
            // 
            cbLimitSwitches.AutoSize = true;
            cbLimitSwitches.Location = new System.Drawing.Point(6, 6);
            cbLimitSwitches.Name = "cbLimitSwitches";
            cbLimitSwitches.Size = new System.Drawing.Size(83, 19);
            cbLimitSwitches.TabIndex = 0;
            cbLimitSwitches.Text = "checkBox1";
            cbLimitSwitches.UseVisualStyleBackColor = true;
            // 
            // probingCommand1
            // 
            probingCommand1.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            probingCommand1.Location = new System.Drawing.Point(501, 6);
            probingCommand1.MinimumSize = new System.Drawing.Size(220, 172);
            probingCommand1.Name = "probingCommand1";
            probingCommand1.Size = new System.Drawing.Size(258, 230);
            probingCommand1.TabIndex = 7;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { mnuMachine, mnuView, mnuAction, mnuOptions, mnuHelp });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(810, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // mnuMachine
            // 
            mnuMachine.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { mnuMachineLoadGCode, mnuMachineClearGCode, toolStripMenuItem2, mnuMachineRename, toolStripMenuItem7, mnuMachineClose });
            mnuMachine.Name = "mnuMachine";
            mnuMachine.Size = new System.Drawing.Size(65, 20);
            mnuMachine.Text = "&Machine";
            // 
            // mnuMachineLoadGCode
            // 
            mnuMachineLoadGCode.Name = "mnuMachineLoadGCode";
            mnuMachineLoadGCode.Size = new System.Drawing.Size(145, 22);
            mnuMachineLoadGCode.Text = "Load G-Code";
            mnuMachineLoadGCode.Click += mnuMachineLoadGCode_Click;
            // 
            // mnuMachineClearGCode
            // 
            mnuMachineClearGCode.Name = "mnuMachineClearGCode";
            mnuMachineClearGCode.Size = new System.Drawing.Size(145, 22);
            mnuMachineClearGCode.Text = "Clear G-Code";
            mnuMachineClearGCode.Click += mnuMachineClearGCode_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(142, 6);
            // 
            // mnuMachineRename
            // 
            mnuMachineRename.Name = "mnuMachineRename";
            mnuMachineRename.Size = new System.Drawing.Size(145, 22);
            mnuMachineRename.Text = "Rename";
            mnuMachineRename.Click += mnuMachineRename_Click;
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new System.Drawing.Size(142, 6);
            // 
            // mnuMachineClose
            // 
            mnuMachineClose.Name = "mnuMachineClose";
            mnuMachineClose.Size = new System.Drawing.Size(145, 22);
            mnuMachineClose.Text = "Close";
            mnuMachineClose.Click += closeToolStripMenuItem_Click;
            // 
            // mnuView
            // 
            mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { mnuViewGeneral, mnuViewOverrides, mnuViewJog, mnuViewSpindle, mnuViewServiceSchedule, mnuViewMachineSettings, mnuViewSettings, toolStripMenuItem1, mnuViewConsole });
            mnuView.Name = "mnuView";
            mnuView.Size = new System.Drawing.Size(44, 20);
            mnuView.Text = "&View";
            // 
            // mnuViewGeneral
            // 
            mnuViewGeneral.Name = "mnuViewGeneral";
            mnuViewGeneral.Size = new System.Drawing.Size(180, 22);
            mnuViewGeneral.Text = "General";
            // 
            // mnuViewOverrides
            // 
            mnuViewOverrides.Name = "mnuViewOverrides";
            mnuViewOverrides.Size = new System.Drawing.Size(180, 22);
            mnuViewOverrides.Text = "Overrides";
            // 
            // mnuViewJog
            // 
            mnuViewJog.Name = "mnuViewJog";
            mnuViewJog.Size = new System.Drawing.Size(180, 22);
            mnuViewJog.Text = "Jog";
            // 
            // mnuViewSpindle
            // 
            mnuViewSpindle.Name = "mnuViewSpindle";
            mnuViewSpindle.Size = new System.Drawing.Size(180, 22);
            mnuViewSpindle.Text = "Spindle";
            // 
            // mnuViewServiceSchedule
            // 
            mnuViewServiceSchedule.Name = "mnuViewServiceSchedule";
            mnuViewServiceSchedule.Size = new System.Drawing.Size(180, 22);
            mnuViewServiceSchedule.Text = "Service Schedule";
            mnuViewServiceSchedule.Visible = false;
            // 
            // mnuViewMachineSettings
            // 
            mnuViewMachineSettings.Name = "mnuViewMachineSettings";
            mnuViewMachineSettings.Size = new System.Drawing.Size(180, 22);
            mnuViewMachineSettings.Text = "Machine Settings";
            // 
            // mnuViewSettings
            // 
            mnuViewSettings.Name = "mnuViewSettings";
            mnuViewSettings.Size = new System.Drawing.Size(180, 22);
            mnuViewSettings.Text = "Settings";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuViewConsole
            // 
            mnuViewConsole.Name = "mnuViewConsole";
            mnuViewConsole.Size = new System.Drawing.Size(180, 22);
            mnuViewConsole.Text = "Console";
            mnuViewConsole.Click += consoleToolStripMenuItem_Click;
            // 
            // mnuAction
            // 
            mnuAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { mnuActionSaveConfig, toolStripMenuItem3, mnuActionConnect, mnuActionDisconnect, toolStripMenuItem6, mnuActionClearAlarm, toolStripMenuItem4, mnuActionHome, mnuActionProbe, toolStripMenuItem5, mnuActionRun, mnuActionPause, mnuActionStop });
            mnuAction.Name = "mnuAction";
            mnuAction.Size = new System.Drawing.Size(54, 20);
            mnuAction.Text = "Action";
            // 
            // mnuActionSaveConfig
            // 
            mnuActionSaveConfig.Name = "mnuActionSaveConfig";
            mnuActionSaveConfig.Size = new System.Drawing.Size(175, 22);
            mnuActionSaveConfig.Text = "Save Configuration";
            mnuActionSaveConfig.Click += toolStripButtonSave_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new System.Drawing.Size(172, 6);
            // 
            // mnuActionConnect
            // 
            mnuActionConnect.Name = "mnuActionConnect";
            mnuActionConnect.Size = new System.Drawing.Size(175, 22);
            mnuActionConnect.Text = "Connect";
            mnuActionConnect.Click += toolStripButtonConnect_Click;
            // 
            // mnuActionDisconnect
            // 
            mnuActionDisconnect.Name = "mnuActionDisconnect";
            mnuActionDisconnect.Size = new System.Drawing.Size(175, 22);
            mnuActionDisconnect.Text = "Disconnect";
            mnuActionDisconnect.Click += toolStripButtonDisconnect_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new System.Drawing.Size(172, 6);
            // 
            // mnuActionClearAlarm
            // 
            mnuActionClearAlarm.Name = "mnuActionClearAlarm";
            mnuActionClearAlarm.Size = new System.Drawing.Size(175, 22);
            mnuActionClearAlarm.Text = "Clear Alarm";
            mnuActionClearAlarm.Click += toolStripButtonClearAlarm_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new System.Drawing.Size(172, 6);
            // 
            // mnuActionHome
            // 
            mnuActionHome.Name = "mnuActionHome";
            mnuActionHome.Size = new System.Drawing.Size(175, 22);
            mnuActionHome.Text = "Home";
            mnuActionHome.Click += toolStripButtonHome_Click;
            // 
            // mnuActionProbe
            // 
            mnuActionProbe.Name = "mnuActionProbe";
            mnuActionProbe.Size = new System.Drawing.Size(175, 22);
            mnuActionProbe.Text = "Probe";
            mnuActionProbe.Click += toolStripButtonProbe_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new System.Drawing.Size(172, 6);
            // 
            // mnuActionRun
            // 
            mnuActionRun.Name = "mnuActionRun";
            mnuActionRun.Size = new System.Drawing.Size(175, 22);
            mnuActionRun.Text = "Run";
            mnuActionRun.Click += toolStripButtonResume_Click;
            // 
            // mnuActionPause
            // 
            mnuActionPause.Name = "mnuActionPause";
            mnuActionPause.Size = new System.Drawing.Size(175, 22);
            mnuActionPause.Text = "Pause";
            mnuActionPause.Click += toolStripButtonPause_Click;
            // 
            // mnuActionStop
            // 
            mnuActionStop.Name = "mnuActionStop";
            mnuActionStop.Size = new System.Drawing.Size(175, 22);
            mnuActionStop.Text = "Stop";
            mnuActionStop.Click += toolStripButtonStop_Click;
            // 
            // mnuOptions
            // 
            mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { mnuOptionsShortcutKeys });
            mnuOptions.Name = "mnuOptions";
            mnuOptions.Size = new System.Drawing.Size(61, 20);
            mnuOptions.Text = "Options";
            // 
            // mnuOptionsShortcutKeys
            // 
            mnuOptionsShortcutKeys.Name = "mnuOptionsShortcutKeys";
            mnuOptionsShortcutKeys.Size = new System.Drawing.Size(146, 22);
            mnuOptionsShortcutKeys.Text = "Shortcut Keys";
            mnuOptionsShortcutKeys.Click += mnuOptionsShortcutKeys_Click;
            // 
            // mnuHelp
            // 
            mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { mnuHelpAbout });
            mnuHelp.Name = "mnuHelp";
            mnuHelp.Size = new System.Drawing.Size(44, 20);
            mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            mnuHelpAbout.Name = "mnuHelpAbout";
            mnuHelpAbout.Size = new System.Drawing.Size(107, 22);
            mnuHelpAbout.Text = "About";
            mnuHelpAbout.Click += mnuHelpAbout_Click;
            // 
            // warningsAndErrors
            // 
            warningsAndErrors.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            warningsAndErrors.Location = new System.Drawing.Point(12, 86);
            warningsAndErrors.Margin = new System.Windows.Forms.Padding(0);
            warningsAndErrors.MaximumSize = new System.Drawing.Size(2048, 48);
            warningsAndErrors.MinimumSize = new System.Drawing.Size(204, 27);
            warningsAndErrors.Name = "warningsAndErrors";
            warningsAndErrors.Size = new System.Drawing.Size(786, 28);
            warningsAndErrors.TabIndex = 2;
            warningsAndErrors.OnUpdate += warningsAndErrors_OnUpdate;
            warningsAndErrors.VisibleChanged += WarningContainer_VisibleChanged;
            warningsAndErrors.Resize += WarningContainer_VisibleChanged;
            // 
            // tabControlSecondary
            // 
            tabControlSecondary.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            tabControlSecondary.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControlSecondary.Controls.Add(tabPageConsole);
            tabControlSecondary.Controls.Add(tabPageGCode);
            tabControlSecondary.Controls.Add(tabPage2DView);
            tabControlSecondary.Controls.Add(tabPageHeartbeat);
            tabControlSecondary.Location = new System.Drawing.Point(12, 411);
            tabControlSecondary.Name = "tabControlSecondary";
            tabControlSecondary.SelectedIndex = 0;
            tabControlSecondary.Size = new System.Drawing.Size(787, 171);
            tabControlSecondary.TabIndex = 3;
            // 
            // tabPageConsole
            // 
            tabPageConsole.Controls.Add(btnGrblCommandSend);
            tabPageConsole.Controls.Add(btnGrblCommandClear);
            tabPageConsole.Controls.Add(txtUserGrblCommand);
            tabPageConsole.Controls.Add(textBoxConsoleText);
            tabPageConsole.Location = new System.Drawing.Point(4, 4);
            tabPageConsole.Name = "tabPageConsole";
            tabPageConsole.Padding = new System.Windows.Forms.Padding(3);
            tabPageConsole.Size = new System.Drawing.Size(779, 143);
            tabPageConsole.TabIndex = 0;
            tabPageConsole.Text = "tabPageConsole";
            tabPageConsole.UseVisualStyleBackColor = true;
            // 
            // btnGrblCommandSend
            // 
            btnGrblCommandSend.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnGrblCommandSend.Location = new System.Drawing.Point(617, 113);
            btnGrblCommandSend.Name = "btnGrblCommandSend";
            btnGrblCommandSend.Size = new System.Drawing.Size(75, 23);
            btnGrblCommandSend.TabIndex = 2;
            btnGrblCommandSend.Text = "button2";
            btnGrblCommandSend.UseVisualStyleBackColor = true;
            btnGrblCommandSend.Click += btnGrblCommandSend_Click;
            // 
            // btnGrblCommandClear
            // 
            btnGrblCommandClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnGrblCommandClear.Location = new System.Drawing.Point(698, 113);
            btnGrblCommandClear.Name = "btnGrblCommandClear";
            btnGrblCommandClear.Size = new System.Drawing.Size(75, 23);
            btnGrblCommandClear.TabIndex = 3;
            btnGrblCommandClear.Text = "button1";
            btnGrblCommandClear.UseVisualStyleBackColor = true;
            btnGrblCommandClear.Click += btnGrblCommandClear_Click;
            // 
            // txtUserGrblCommand
            // 
            txtUserGrblCommand.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtUserGrblCommand.Location = new System.Drawing.Point(6, 114);
            txtUserGrblCommand.Name = "txtUserGrblCommand";
            txtUserGrblCommand.Size = new System.Drawing.Size(605, 23);
            txtUserGrblCommand.TabIndex = 1;
            txtUserGrblCommand.TextChanged += txtUserGrblCommand_TextChanged;
            txtUserGrblCommand.KeyDown += txtUserGrblCommand_KeyDown;
            // 
            // textBoxConsoleText
            // 
            textBoxConsoleText.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxConsoleText.Location = new System.Drawing.Point(6, 6);
            textBoxConsoleText.Multiline = true;
            textBoxConsoleText.Name = "textBoxConsoleText";
            textBoxConsoleText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBoxConsoleText.Size = new System.Drawing.Size(767, 102);
            textBoxConsoleText.TabIndex = 0;
            // 
            // tabPageGCode
            // 
            tabPageGCode.Controls.Add(listViewGCode);
            tabPageGCode.Location = new System.Drawing.Point(4, 4);
            tabPageGCode.Name = "tabPageGCode";
            tabPageGCode.Padding = new System.Windows.Forms.Padding(3);
            tabPageGCode.Size = new System.Drawing.Size(779, 143);
            tabPageGCode.TabIndex = 1;
            tabPageGCode.Text = "tabPageGCode";
            tabPageGCode.UseVisualStyleBackColor = true;
            // 
            // listViewGCode
            // 
            listViewGCode.AllowColumnReorder = true;
            listViewGCode.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            listViewGCode.AutoArrange = false;
            listViewGCode.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeaderLine, columnHeaderGCode, columnHeaderComments, columnHeaderFeed, columnHeaderSpindleSpeed, columnHeaderAttributes, columnHeaderStatus });
            listViewGCode.FullRowSelect = true;
            listViewGCode.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewGCode.Location = new System.Drawing.Point(6, 6);
            listViewGCode.MultiSelect = false;
            listViewGCode.Name = "listViewGCode";
            listViewGCode.OwnerDraw = true;
            listViewGCode.SaveName = "";
            listViewGCode.ShowToolTip = false;
            listViewGCode.Size = new System.Drawing.Size(753, 131);
            listViewGCode.TabIndex = 2;
            listViewGCode.UseCompatibleStateImageBehavior = false;
            listViewGCode.View = System.Windows.Forms.View.Details;
            listViewGCode.VirtualMode = true;
            listViewGCode.RetrieveVirtualItem += ListViewGCode_RetrieveVirtualItem;
            // 
            // columnHeaderLine
            // 
            columnHeaderLine.Text = "Line";
            // 
            // columnHeaderGCode
            // 
            columnHeaderGCode.Text = "gcode";
            columnHeaderGCode.Width = 250;
            // 
            // columnHeaderComments
            // 
            columnHeaderComments.Text = "comments";
            columnHeaderComments.Width = 150;
            // 
            // columnHeaderFeed
            // 
            columnHeaderFeed.Text = "Feed";
            columnHeaderFeed.Width = 70;
            // 
            // columnHeaderSpindleSpeed
            // 
            columnHeaderSpindleSpeed.Text = "spindle";
            columnHeaderSpindleSpeed.Width = 70;
            // 
            // columnHeaderAttributes
            // 
            columnHeaderAttributes.Text = "Attributes";
            columnHeaderAttributes.Width = 200;
            // 
            // columnHeaderStatus
            // 
            columnHeaderStatus.Text = "Status";
            columnHeaderStatus.Width = 100;
            // 
            // tabPage2DView
            // 
            tabPage2DView.Controls.Add(panelZoom);
            tabPage2DView.Controls.Add(machine2dView1);
            tabPage2DView.Location = new System.Drawing.Point(4, 4);
            tabPage2DView.Name = "tabPage2DView";
            tabPage2DView.Padding = new System.Windows.Forms.Padding(3);
            tabPage2DView.Size = new System.Drawing.Size(779, 143);
            tabPage2DView.TabIndex = 2;
            tabPage2DView.Text = "tabPage1";
            tabPage2DView.UseVisualStyleBackColor = true;
            // 
            // panelZoom
            // 
            panelZoom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelZoom.Location = new System.Drawing.Point(512, 6);
            panelZoom.MaximumSize = new System.Drawing.Size(200, 200);
            panelZoom.MinimumSize = new System.Drawing.Size(200, 200);
            panelZoom.Name = "panelZoom";
            panelZoom.Size = new System.Drawing.Size(200, 200);
            panelZoom.TabIndex = 1;
            // 
            // machine2dView1
            // 
            machine2dView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            machine2dView1.BackgroundImage = (System.Drawing.Image)resources.GetObject("machine2dView1.BackgroundImage");
            machine2dView1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            machine2dView1.Configuration = GSendShared.AxisConfiguration.None;
            machine2dView1.Location = new System.Drawing.Point(6, 6);
            machine2dView1.MachineSize = new System.Drawing.Rectangle(0, 0, 0, 0);
            machine2dView1.Name = "machine2dView1";
            machine2dView1.Size = new System.Drawing.Size(485, 131);
            machine2dView1.TabIndex = 0;
            machine2dView1.XPosition = 0F;
            machine2dView1.YPosition = 0F;
            machine2dView1.ZoomPanel = panelZoom;
            // 
            // tabPageHeartbeat
            // 
            tabPageHeartbeat.Controls.Add(flowLayoutPanelHeartbeat);
            tabPageHeartbeat.Location = new System.Drawing.Point(4, 4);
            tabPageHeartbeat.Name = "tabPageHeartbeat";
            tabPageHeartbeat.Padding = new System.Windows.Forms.Padding(3);
            tabPageHeartbeat.Size = new System.Drawing.Size(779, 143);
            tabPageHeartbeat.TabIndex = 3;
            tabPageHeartbeat.Text = "graphs";
            tabPageHeartbeat.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelHeartbeat
            // 
            flowLayoutPanelHeartbeat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            flowLayoutPanelHeartbeat.AutoScroll = true;
            flowLayoutPanelHeartbeat.Controls.Add(heartbeatPanelCommandQueue);
            flowLayoutPanelHeartbeat.Controls.Add(heartbeatPanelBufferSize);
            flowLayoutPanelHeartbeat.Controls.Add(heartbeatPanelQueueSize);
            flowLayoutPanelHeartbeat.Controls.Add(heartbeatPanelFeed);
            flowLayoutPanelHeartbeat.Controls.Add(heartbeatPanelSpindle);
            flowLayoutPanelHeartbeat.Controls.Add(heartbeatPanelAvailableBlocks);
            flowLayoutPanelHeartbeat.Controls.Add(heartbeatPanelAvailableRXBytes);
            flowLayoutPanelHeartbeat.Location = new System.Drawing.Point(6, 6);
            flowLayoutPanelHeartbeat.Name = "flowLayoutPanelHeartbeat";
            flowLayoutPanelHeartbeat.Size = new System.Drawing.Size(753, 131);
            flowLayoutPanelHeartbeat.TabIndex = 0;
            // 
            // heartbeatPanelCommandQueue
            // 
            heartbeatPanelCommandQueue.AutoPoints = true;
            heartbeatPanelCommandQueue.BackGround = System.Drawing.Color.LightCyan;
            heartbeatPanelCommandQueue.GraphName = null;
            heartbeatPanelCommandQueue.Location = new System.Drawing.Point(3, 3);
            heartbeatPanelCommandQueue.MaximumPoints = 60;
            heartbeatPanelCommandQueue.Name = "heartbeatPanelCommandQueue";
            heartbeatPanelCommandQueue.PrimaryColor = System.Drawing.Color.SlateGray;
            heartbeatPanelCommandQueue.SecondaryColor = System.Drawing.Color.BlueViolet;
            heartbeatPanelCommandQueue.Size = new System.Drawing.Size(200, 100);
            heartbeatPanelCommandQueue.TabIndex = 2;
            // 
            // heartbeatPanelBufferSize
            // 
            heartbeatPanelBufferSize.AutoPoints = true;
            heartbeatPanelBufferSize.BackGround = System.Drawing.Color.LightCyan;
            heartbeatPanelBufferSize.GraphName = null;
            heartbeatPanelBufferSize.Location = new System.Drawing.Point(209, 3);
            heartbeatPanelBufferSize.MaximumPoints = 60;
            heartbeatPanelBufferSize.Name = "heartbeatPanelBufferSize";
            heartbeatPanelBufferSize.PrimaryColor = System.Drawing.Color.SlateGray;
            heartbeatPanelBufferSize.SecondaryColor = System.Drawing.Color.BlueViolet;
            heartbeatPanelBufferSize.Size = new System.Drawing.Size(200, 100);
            heartbeatPanelBufferSize.TabIndex = 0;
            // 
            // heartbeatPanelQueueSize
            // 
            heartbeatPanelQueueSize.AutoPoints = true;
            heartbeatPanelQueueSize.BackGround = System.Drawing.Color.LightCyan;
            heartbeatPanelQueueSize.GraphName = null;
            heartbeatPanelQueueSize.Location = new System.Drawing.Point(415, 3);
            heartbeatPanelQueueSize.MaximumPoints = 60;
            heartbeatPanelQueueSize.Name = "heartbeatPanelQueueSize";
            heartbeatPanelQueueSize.PrimaryColor = System.Drawing.Color.SlateGray;
            heartbeatPanelQueueSize.SecondaryColor = System.Drawing.Color.BlueViolet;
            heartbeatPanelQueueSize.Size = new System.Drawing.Size(200, 100);
            heartbeatPanelQueueSize.TabIndex = 1;
            // 
            // heartbeatPanelFeed
            // 
            heartbeatPanelFeed.AutoPoints = true;
            heartbeatPanelFeed.BackGround = System.Drawing.Color.LightCyan;
            heartbeatPanelFeed.GraphName = null;
            heartbeatPanelFeed.Location = new System.Drawing.Point(3, 109);
            heartbeatPanelFeed.MaximumPoints = 60;
            heartbeatPanelFeed.Name = "heartbeatPanelFeed";
            heartbeatPanelFeed.PrimaryColor = System.Drawing.Color.SlateGray;
            heartbeatPanelFeed.SecondaryColor = System.Drawing.Color.BlueViolet;
            heartbeatPanelFeed.Size = new System.Drawing.Size(200, 100);
            heartbeatPanelFeed.TabIndex = 3;
            // 
            // heartbeatPanelSpindle
            // 
            heartbeatPanelSpindle.AutoPoints = true;
            heartbeatPanelSpindle.BackGround = System.Drawing.Color.LightCyan;
            heartbeatPanelSpindle.GraphName = null;
            heartbeatPanelSpindle.Location = new System.Drawing.Point(209, 109);
            heartbeatPanelSpindle.MaximumPoints = 60;
            heartbeatPanelSpindle.Name = "heartbeatPanelSpindle";
            heartbeatPanelSpindle.PrimaryColor = System.Drawing.Color.SlateGray;
            heartbeatPanelSpindle.SecondaryColor = System.Drawing.Color.BlueViolet;
            heartbeatPanelSpindle.Size = new System.Drawing.Size(200, 100);
            heartbeatPanelSpindle.TabIndex = 4;
            // 
            // heartbeatPanelAvailableBlocks
            // 
            heartbeatPanelAvailableBlocks.AutoPoints = true;
            heartbeatPanelAvailableBlocks.BackGround = System.Drawing.Color.LightCyan;
            heartbeatPanelAvailableBlocks.GraphName = null;
            heartbeatPanelAvailableBlocks.Location = new System.Drawing.Point(415, 109);
            heartbeatPanelAvailableBlocks.MaximumPoints = 34;
            heartbeatPanelAvailableBlocks.Name = "heartbeatPanelAvailableBlocks";
            heartbeatPanelAvailableBlocks.PrimaryColor = System.Drawing.Color.SlateGray;
            heartbeatPanelAvailableBlocks.SecondaryColor = System.Drawing.Color.DarkSeaGreen;
            heartbeatPanelAvailableBlocks.Size = new System.Drawing.Size(200, 100);
            heartbeatPanelAvailableBlocks.TabIndex = 5;
            // 
            // heartbeatPanelAvailableRXBytes
            // 
            heartbeatPanelAvailableRXBytes.AutoPoints = true;
            heartbeatPanelAvailableRXBytes.BackGround = System.Drawing.Color.LightCyan;
            heartbeatPanelAvailableRXBytes.GraphName = null;
            heartbeatPanelAvailableRXBytes.Location = new System.Drawing.Point(3, 215);
            heartbeatPanelAvailableRXBytes.MaximumPoints = 255;
            heartbeatPanelAvailableRXBytes.Name = "heartbeatPanelAvailableRXBytes";
            heartbeatPanelAvailableRXBytes.PrimaryColor = System.Drawing.Color.SlateGray;
            heartbeatPanelAvailableRXBytes.SecondaryColor = System.Drawing.Color.DarkSeaGreen;
            heartbeatPanelAvailableRXBytes.Size = new System.Drawing.Size(200, 100);
            heartbeatPanelAvailableRXBytes.TabIndex = 6;
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "G-Code Files|*.gcode;*.nc;*.ncc;*.ngc;*.tap;*.txt|All Files|*.*";
            // 
            // FrmMachine
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CausesValidation = false;
            ClientSize = new System.Drawing.Size(810, 615);
            Controls.Add(tabControlSecondary);
            Controls.Add(warningsAndErrors);
            Controls.Add(tabControlMain);
            Controls.Add(statusStrip);
            Controls.Add(toolStripMain);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new System.Drawing.Size(812, 0);
            Name = "FrmMachine";
            Text = "FrmMachine";
            FormClosing += FrmMachine_FormClosing;
            Shown += FrmMachine_Shown;
            KeyDown += FrmMachine_KeyDown;
            KeyUp += FrmMachine_KeyUp;
            toolStripMain.ResumeLayout(false);
            toolStripMain.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            tabControlMain.ResumeLayout(false);
            tabPageMain.ResumeLayout(false);
            tabPageMain.PerformLayout();
            tabPageOverrides.ResumeLayout(false);
            tabPageOverrides.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarPercent).EndInit();
            tabPageJog.ResumeLayout(false);
            tabPageSpindle.ResumeLayout(false);
            tabPageSpindle.PerformLayout();
            grpBoxSpindleSpeed.ResumeLayout(false);
            grpBoxSpindleSpeed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarSpindleSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarDelaySpindle).EndInit();
            tabPageServiceSchedule.ResumeLayout(false);
            tabPageServiceSchedule.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarServiceSpindleHours).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarServiceWeeks).EndInit();
            tabPageMachineSettings.ResumeLayout(false);
            tabPageMachineSettings.PerformLayout();
            tabPageSettings.ResumeLayout(false);
            tabPageSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericLayerHeight).EndInit();
            grpFeedDisplay.ResumeLayout(false);
            grpFeedDisplay.PerformLayout();
            grpDisplayUnits.ResumeLayout(false);
            grpDisplayUnits.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabControlSecondary.ResumeLayout(false);
            tabPageConsole.ResumeLayout(false);
            tabPageConsole.PerformLayout();
            tabPageGCode.ResumeLayout(false);
            tabPage2DView.ResumeLayout(false);
            tabPageHeartbeat.ResumeLayout(false);
            flowLayoutPanelHeartbeat.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonConnect;
        private System.Windows.Forms.ToolStripButton toolStripButtonDisconnect;
        private System.Windows.Forms.ToolStripButton toolStripButtonClearAlarm;
        private System.Windows.Forms.ToolStripButton toolStripButtonResume;
        private System.Windows.Forms.ToolStripButton toolStripButtonHome;
        private GSendControls.CheckedSelection selectionOverrideSpindle;
        private GSendControls.CheckedSelection selectionOverrideZDown;
        private GSendControls.CheckedSelection selectionOverrideZUp;
        private GSendControls.CheckedSelection selectionOverrideRapids;
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
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFeedRate;
        private GSendControls.CheckedSelection selectionOverrideXY;
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
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
    }
}