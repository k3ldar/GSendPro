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
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelServerConnect = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDisplayMeasurements = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCpu = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelWarnings = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.machinePositionGeneral = new GSendDesktop.Controls.MachinePosition();
            this.tabPageOverrides = new System.Windows.Forms.TabPage();
            this.cbOverridesDisable = new System.Windows.Forms.CheckBox();
            this.labelSpeedPercent = new System.Windows.Forms.Label();
            this.trackBarPercent = new System.Windows.Forms.TrackBar();
            this.machinePositionOverrides = new GSendDesktop.Controls.MachinePosition();
            this.tabPageJog = new System.Windows.Forms.TabPage();
            this.machinePositionJog = new GSendDesktop.Controls.MachinePosition();
            this.tabPageSpindle = new System.Windows.Forms.TabPage();
            this.tabPageServiceSchedule = new System.Windows.Forms.TabPage();
            this.tabPageUsage = new System.Windows.Forms.TabPage();
            this.tabPageMachineSettings = new System.Windows.Forms.TabPage();
            this.btnApplyGrblUpdates = new System.Windows.Forms.Button();
            this.txtGrblUpdates = new System.Windows.Forms.TextBox();
            this.lblPropertyDesc = new System.Windows.Forms.Label();
            this.lblPropertyHeader = new System.Windows.Forms.Label();
            this.propertyGridGrblSettings = new System.Windows.Forms.PropertyGrid();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.machineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutWarningErrors = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStripMain.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.tabPageOverrides.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPercent)).BeginInit();
            this.tabPageJog.SuspendLayout();
            this.tabPageMachineSettings.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectionOverrideSpindle
            // 
            this.selectionOverrideSpindle.GroupName = "Spindle";
            this.selectionOverrideSpindle.LabelFormat = "{0}";
            this.selectionOverrideSpindle.LabelValue = null;
            this.selectionOverrideSpindle.Location = new System.Drawing.Point(649, 6);
            this.selectionOverrideSpindle.Maximum = 10;
            this.selectionOverrideSpindle.Minimum = 0;
            this.selectionOverrideSpindle.Name = "selectionOverrideSpindle";
            this.selectionOverrideSpindle.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideSpindle.TabIndex = 4;
            this.selectionOverrideSpindle.TickFrequency = 1;
            this.selectionOverrideSpindle.Value = 0;
            // 
            // selectionOverrideZDown
            // 
            this.selectionOverrideZDown.GroupName = "Z Down";
            this.selectionOverrideZDown.LabelFormat = "{0}";
            this.selectionOverrideZDown.LabelValue = null;
            this.selectionOverrideZDown.Location = new System.Drawing.Point(562, 6);
            this.selectionOverrideZDown.Maximum = 10;
            this.selectionOverrideZDown.Minimum = 0;
            this.selectionOverrideZDown.Name = "selectionOverrideZDown";
            this.selectionOverrideZDown.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideZDown.TabIndex = 3;
            this.selectionOverrideZDown.TickFrequency = 1;
            this.selectionOverrideZDown.Value = 0;
            // 
            // selectionOverrideZUp
            // 
            this.selectionOverrideZUp.GroupName = "Z Up";
            this.selectionOverrideZUp.LabelFormat = "{0}";
            this.selectionOverrideZUp.LabelValue = null;
            this.selectionOverrideZUp.Location = new System.Drawing.Point(474, 6);
            this.selectionOverrideZUp.Maximum = 10;
            this.selectionOverrideZUp.Minimum = 0;
            this.selectionOverrideZUp.Name = "selectionOverrideZUp";
            this.selectionOverrideZUp.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideZUp.TabIndex = 2;
            this.selectionOverrideZUp.TickFrequency = 1;
            this.selectionOverrideZUp.Value = 0;
            // 
            // selectionOverrideY
            // 
            this.selectionOverrideY.GroupName = "Y";
            this.selectionOverrideY.LabelFormat = "{0}";
            this.selectionOverrideY.LabelValue = null;
            this.selectionOverrideY.Location = new System.Drawing.Point(386, 6);
            this.selectionOverrideY.Maximum = 10;
            this.selectionOverrideY.Minimum = 0;
            this.selectionOverrideY.Name = "selectionOverrideY";
            this.selectionOverrideY.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideY.TabIndex = 1;
            this.selectionOverrideY.TickFrequency = 1;
            this.selectionOverrideY.Value = 0;
            // 
            // selectionOverrideX
            // 
            this.selectionOverrideX.GroupName = "X";
            this.selectionOverrideX.LabelFormat = "{0}";
            this.selectionOverrideX.LabelValue = null;
            this.selectionOverrideX.Location = new System.Drawing.Point(298, 6);
            this.selectionOverrideX.Maximum = 10;
            this.selectionOverrideX.Minimum = 0;
            this.selectionOverrideX.Name = "selectionOverrideX";
            this.selectionOverrideX.Size = new System.Drawing.Size(82, 228);
            this.selectionOverrideX.TabIndex = 0;
            this.selectionOverrideX.TickFrequency = 1;
            this.selectionOverrideX.Value = 0;
            // 
            // jogControl
            // 
            this.jogControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.jogControl.FeedMaximum = 10;
            this.jogControl.FeedMinimum = 0;
            this.jogControl.FeedRate = 0;
            this.jogControl.Location = new System.Drawing.Point(330, 30);
            this.jogControl.Name = "jogControl";
            this.jogControl.Size = new System.Drawing.Size(439, 190);
            this.jogControl.StepValue = 0;
            this.jogControl.TabIndex = 3;
            this.jogControl.OnJogStart += new GSendShared.JogCommand(this.jogControl1_OnJogStart);
            this.jogControl.OnJogStop += new System.EventHandler(this.jogControl1_OnJogStop);
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.toolStripSeparator4});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(774, 57);
            this.toolStripMain.TabIndex = 6;
            this.toolStripMain.Text = "toolStrip1";
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 57);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelServerConnect,
            this.toolStripStatusLabelDisplayMeasurements,
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabelCpu,
            this.toolStripStatusLabelWarnings});
            this.statusStrip.Location = new System.Drawing.Point(0, 621);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(774, 24);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 7;
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
            this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(4, 19);
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
            this.tabControlMain.Size = new System.Drawing.Size(751, 270);
            this.tabControlMain.TabIndex = 8;
            // 
            // tabPageMain
            // 
            this.tabPageMain.BackColor = System.Drawing.Color.White;
            this.tabPageMain.Controls.Add(this.machinePositionGeneral);
            this.tabPageMain.Location = new System.Drawing.Point(4, 24);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(743, 242);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "General";
            // 
            // machinePositionGeneral
            // 
            this.machinePositionGeneral.DisplayMeasurements = GSendShared.DisplayUnits.MmPerMinute;
            this.machinePositionGeneral.Location = new System.Drawing.Point(6, 6);
            this.machinePositionGeneral.Name = "machinePositionGeneral";
            this.machinePositionGeneral.Size = new System.Drawing.Size(280, 111);
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
            this.tabPageOverrides.Size = new System.Drawing.Size(743, 242);
            this.tabPageOverrides.TabIndex = 1;
            this.tabPageOverrides.Text = "Overrides";
            // 
            // cbOverridesDisable
            // 
            this.cbOverridesDisable.AutoSize = true;
            this.cbOverridesDisable.Location = new System.Drawing.Point(6, 215);
            this.cbOverridesDisable.Name = "cbOverridesDisable";
            this.cbOverridesDisable.Size = new System.Drawing.Size(83, 19);
            this.cbOverridesDisable.TabIndex = 14;
            this.cbOverridesDisable.Text = "checkBox1";
            this.cbOverridesDisable.UseVisualStyleBackColor = true;
            this.cbOverridesDisable.CheckedChanged += new System.EventHandler(this.cbOverridesDisable_CheckedChanged);
            // 
            // labelSpeedPercent
            // 
            this.labelSpeedPercent.AutoSize = true;
            this.labelSpeedPercent.Location = new System.Drawing.Point(6, 171);
            this.labelSpeedPercent.Name = "labelSpeedPercent";
            this.labelSpeedPercent.Size = new System.Drawing.Size(72, 15);
            this.labelSpeedPercent.TabIndex = 13;
            this.labelSpeedPercent.Text = "labelPercent";
            // 
            // trackBarPercent
            // 
            this.trackBarPercent.Location = new System.Drawing.Point(6, 123);
            this.trackBarPercent.Maximum = 100;
            this.trackBarPercent.Name = "trackBarPercent";
            this.trackBarPercent.Size = new System.Drawing.Size(280, 45);
            this.trackBarPercent.TabIndex = 12;
            this.trackBarPercent.TickFrequency = 5;
            this.trackBarPercent.ValueChanged += new System.EventHandler(this.trackBarPercent_ValueChanged);
            // 
            // machinePositionOverrides
            // 
            this.machinePositionOverrides.DisplayMeasurements = GSendShared.DisplayUnits.MmPerMinute;
            this.machinePositionOverrides.Location = new System.Drawing.Point(6, 6);
            this.machinePositionOverrides.Name = "machinePositionOverrides";
            this.machinePositionOverrides.Size = new System.Drawing.Size(280, 111);
            this.machinePositionOverrides.TabIndex = 5;
            // 
            // tabPageJog
            // 
            this.tabPageJog.BackColor = System.Drawing.Color.White;
            this.tabPageJog.Controls.Add(this.machinePositionJog);
            this.tabPageJog.Controls.Add(this.jogControl);
            this.tabPageJog.Location = new System.Drawing.Point(4, 24);
            this.tabPageJog.Name = "tabPageJog";
            this.tabPageJog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJog.Size = new System.Drawing.Size(743, 242);
            this.tabPageJog.TabIndex = 2;
            this.tabPageJog.Text = "Jog";
            // 
            // machinePositionJog
            // 
            this.machinePositionJog.DisplayMeasurements = GSendShared.DisplayUnits.MmPerMinute;
            this.machinePositionJog.Location = new System.Drawing.Point(6, 6);
            this.machinePositionJog.Name = "machinePositionJog";
            this.machinePositionJog.Size = new System.Drawing.Size(280, 111);
            this.machinePositionJog.TabIndex = 4;
            // 
            // tabPageSpindle
            // 
            this.tabPageSpindle.BackColor = System.Drawing.Color.White;
            this.tabPageSpindle.Location = new System.Drawing.Point(4, 24);
            this.tabPageSpindle.Name = "tabPageSpindle";
            this.tabPageSpindle.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSpindle.Size = new System.Drawing.Size(743, 242);
            this.tabPageSpindle.TabIndex = 4;
            this.tabPageSpindle.Text = "Spindle";
            // 
            // tabPageServiceSchedule
            // 
            this.tabPageServiceSchedule.BackColor = System.Drawing.Color.White;
            this.tabPageServiceSchedule.Location = new System.Drawing.Point(4, 24);
            this.tabPageServiceSchedule.Name = "tabPageServiceSchedule";
            this.tabPageServiceSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageServiceSchedule.Size = new System.Drawing.Size(743, 242);
            this.tabPageServiceSchedule.TabIndex = 6;
            this.tabPageServiceSchedule.Text = "Service Scgedule";
            // 
            // tabPageUsage
            // 
            this.tabPageUsage.BackColor = System.Drawing.Color.White;
            this.tabPageUsage.Location = new System.Drawing.Point(4, 24);
            this.tabPageUsage.Name = "tabPageUsage";
            this.tabPageUsage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUsage.Size = new System.Drawing.Size(743, 242);
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
            this.tabPageMachineSettings.Size = new System.Drawing.Size(743, 242);
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
            this.btnApplyGrblUpdates.TabIndex = 5;
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
            this.txtGrblUpdates.TabIndex = 4;
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
            this.propertyGridGrblSettings.TabIndex = 1;
            this.propertyGridGrblSettings.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGridGrblSettings_PropertyValueChanged);
            this.propertyGridGrblSettings.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.propertyGridGrblSettings_SelectedGridItemChanged);
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.Color.White;
            this.tabPageSettings.Location = new System.Drawing.Point(4, 24);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(743, 242);
            this.tabPageSettings.TabIndex = 8;
            this.tabPageSettings.Text = "Settings";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(12, 411);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(750, 193);
            this.textBox2.TabIndex = 9;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.machineToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(774, 24);
            this.menuStrip1.TabIndex = 10;
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
            // flowLayoutWarningErrors
            // 
            this.flowLayoutWarningErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutWarningErrors.AutoScroll = true;
            this.flowLayoutWarningErrors.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutWarningErrors.Location = new System.Drawing.Point(12, 86);
            this.flowLayoutWarningErrors.Name = "flowLayoutWarningErrors";
            this.flowLayoutWarningErrors.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.flowLayoutWarningErrors.Size = new System.Drawing.Size(750, 48);
            this.flowLayoutWarningErrors.TabIndex = 11;
            this.flowLayoutWarningErrors.VisibleChanged += new System.EventHandler(this.flowLayoutWarningErrors_VisibleChanged);
            // 
            // FrmMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 645);
            this.Controls.Add(this.flowLayoutWarningErrors);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(790, 0);
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
            this.tabPageMachineSettings.ResumeLayout(false);
            this.tabPageMachineSettings.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblPropertyDesc;
        private System.Windows.Forms.Label lblPropertyHeader;
        private System.Windows.Forms.TextBox txtGrblUpdates;
        private System.Windows.Forms.Button btnApplyGrblUpdates;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem machineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutWarningErrors;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelWarnings;
    }
}