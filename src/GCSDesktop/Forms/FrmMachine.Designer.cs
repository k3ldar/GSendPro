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
            this.groupBoxCoordinates = new System.Windows.Forms.GroupBox();
            this.selection5 = new GSendDesktop.Controls.Selection();
            this.selection4 = new GSendDesktop.Controls.Selection();
            this.selection3 = new GSendDesktop.Controls.Selection();
            this.selection2 = new GSendDesktop.Controls.Selection();
            this.selection1 = new GSendDesktop.Controls.Selection();
            this.jogControl1 = new GSendDesktop.Controls.JogControl();
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.tabPageOverrides = new System.Windows.Forms.TabPage();
            this.tabPageJog = new System.Windows.Forms.TabPage();
            this.tabPageSpindle = new System.Windows.Forms.TabPage();
            this.tabPageServiceSchedule = new System.Windows.Forms.TabPage();
            this.tabPageUsage = new System.Windows.Forms.TabPage();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.toolStripMain.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageOverrides.SuspendLayout();
            this.tabPageJog.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCoordinates
            // 
            this.groupBoxCoordinates.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxCoordinates.Location = new System.Drawing.Point(25, 100);
            this.groupBoxCoordinates.Name = "groupBoxCoordinates";
            this.groupBoxCoordinates.Size = new System.Drawing.Size(318, 215);
            this.groupBoxCoordinates.TabIndex = 1;
            this.groupBoxCoordinates.TabStop = false;
            this.groupBoxCoordinates.Text = "Position";
            // 
            // selection5
            // 
            this.selection5.GroupName = "Spindle";
            this.selection5.LabelFormat = "{0}";
            this.selection5.LabelValue = null;
            this.selection5.Location = new System.Drawing.Point(684, 6);
            this.selection5.Maximum = 10;
            this.selection5.Name = "selection5";
            this.selection5.Size = new System.Drawing.Size(82, 228);
            this.selection5.TabIndex = 4;
            this.selection5.TickFrequency = 1;
            this.selection5.Value = 0;
            // 
            // selection4
            // 
            this.selection4.GroupName = "Z Down";
            this.selection4.LabelFormat = "{0}";
            this.selection4.LabelValue = null;
            this.selection4.Location = new System.Drawing.Point(597, 6);
            this.selection4.Maximum = 10;
            this.selection4.Name = "selection4";
            this.selection4.Size = new System.Drawing.Size(82, 228);
            this.selection4.TabIndex = 3;
            this.selection4.TickFrequency = 1;
            this.selection4.Value = 0;
            // 
            // selection3
            // 
            this.selection3.GroupName = "Z Up";
            this.selection3.LabelFormat = "{0}";
            this.selection3.LabelValue = null;
            this.selection3.Location = new System.Drawing.Point(509, 6);
            this.selection3.Maximum = 10;
            this.selection3.Name = "selection3";
            this.selection3.Size = new System.Drawing.Size(82, 228);
            this.selection3.TabIndex = 2;
            this.selection3.TickFrequency = 1;
            this.selection3.Value = 0;
            // 
            // selection2
            // 
            this.selection2.GroupName = "Y";
            this.selection2.LabelFormat = "{0}";
            this.selection2.LabelValue = null;
            this.selection2.Location = new System.Drawing.Point(421, 6);
            this.selection2.Maximum = 10;
            this.selection2.Name = "selection2";
            this.selection2.Size = new System.Drawing.Size(82, 228);
            this.selection2.TabIndex = 1;
            this.selection2.TickFrequency = 1;
            this.selection2.Value = 0;
            // 
            // selection1
            // 
            this.selection1.GroupName = "X";
            this.selection1.LabelFormat = "{0}";
            this.selection1.LabelValue = null;
            this.selection1.Location = new System.Drawing.Point(333, 6);
            this.selection1.Maximum = 10;
            this.selection1.Name = "selection1";
            this.selection1.Size = new System.Drawing.Size(82, 228);
            this.selection1.TabIndex = 0;
            this.selection1.TickFrequency = 1;
            this.selection1.Value = 0;
            // 
            // jogControl1
            // 
            this.jogControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.jogControl1.Location = new System.Drawing.Point(327, 30);
            this.jogControl1.Name = "jogControl1";
            this.jogControl1.Size = new System.Drawing.Size(439, 190);
            this.jogControl1.TabIndex = 3;
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
            this.toolStripButtonStop});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(804, 57);
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
            // 
            // toolStripButtonDisconnect
            // 
            this.toolStripButtonDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDisconnect.Image")));
            this.toolStripButtonDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDisconnect.Name = "toolStripButtonDisconnect";
            this.toolStripButtonDisconnect.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonDisconnect.Text = "toolStripButton1";
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
            // 
            // toolStripButtonProbe
            // 
            this.toolStripButtonProbe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonProbe.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProbe.Image")));
            this.toolStripButtonProbe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProbe.Name = "toolStripButtonProbe";
            this.toolStripButtonProbe.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonProbe.Text = "toolStripButton2";
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
            // 
            // toolStripButtonPause
            // 
            this.toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPause.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPause.Image")));
            this.toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPause.Name = "toolStripButtonPause";
            this.toolStripButtonPause.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonPause.Text = "toolStripButton1";
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStop.Image")));
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonStop.Text = "toolStripButton2";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 394);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(804, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageMain);
            this.tabControlMain.Controls.Add(this.tabPageOverrides);
            this.tabControlMain.Controls.Add(this.tabPageJog);
            this.tabControlMain.Controls.Add(this.tabPageSpindle);
            this.tabControlMain.Controls.Add(this.tabPageServiceSchedule);
            this.tabControlMain.Controls.Add(this.tabPageUsage);
            this.tabControlMain.Controls.Add(this.tabPageSettings);
            this.tabControlMain.HotTrack = true;
            this.tabControlMain.Location = new System.Drawing.Point(12, 60);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(780, 270);
            this.tabControlMain.TabIndex = 8;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Location = new System.Drawing.Point(4, 24);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(772, 242);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "General";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // tabPageOverrides
            // 
            this.tabPageOverrides.Controls.Add(this.selection5);
            this.tabPageOverrides.Controls.Add(this.selection4);
            this.tabPageOverrides.Controls.Add(this.selection1);
            this.tabPageOverrides.Controls.Add(this.selection3);
            this.tabPageOverrides.Controls.Add(this.selection2);
            this.tabPageOverrides.Location = new System.Drawing.Point(4, 24);
            this.tabPageOverrides.Name = "tabPageOverrides";
            this.tabPageOverrides.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOverrides.Size = new System.Drawing.Size(772, 242);
            this.tabPageOverrides.TabIndex = 1;
            this.tabPageOverrides.Text = "Overrides";
            this.tabPageOverrides.UseVisualStyleBackColor = true;
            // 
            // tabPageJog
            // 
            this.tabPageJog.Controls.Add(this.jogControl1);
            this.tabPageJog.Location = new System.Drawing.Point(4, 24);
            this.tabPageJog.Name = "tabPageJog";
            this.tabPageJog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJog.Size = new System.Drawing.Size(772, 242);
            this.tabPageJog.TabIndex = 2;
            this.tabPageJog.Text = "Jog";
            this.tabPageJog.UseVisualStyleBackColor = true;
            // 
            // tabPageSpindle
            // 
            this.tabPageSpindle.Location = new System.Drawing.Point(4, 24);
            this.tabPageSpindle.Name = "tabPageSpindle";
            this.tabPageSpindle.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSpindle.Size = new System.Drawing.Size(772, 242);
            this.tabPageSpindle.TabIndex = 4;
            this.tabPageSpindle.Text = "Spindle";
            this.tabPageSpindle.UseVisualStyleBackColor = true;
            // 
            // tabPageServiceSchedule
            // 
            this.tabPageServiceSchedule.Location = new System.Drawing.Point(4, 24);
            this.tabPageServiceSchedule.Name = "tabPageServiceSchedule";
            this.tabPageServiceSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageServiceSchedule.Size = new System.Drawing.Size(772, 242);
            this.tabPageServiceSchedule.TabIndex = 6;
            this.tabPageServiceSchedule.Text = "Service Scgedule";
            this.tabPageServiceSchedule.UseVisualStyleBackColor = true;
            // 
            // tabPageUsage
            // 
            this.tabPageUsage.Location = new System.Drawing.Point(4, 24);
            this.tabPageUsage.Name = "tabPageUsage";
            this.tabPageUsage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUsage.Size = new System.Drawing.Size(772, 242);
            this.tabPageUsage.TabIndex = 7;
            this.tabPageUsage.Text = "Useage";
            this.tabPageUsage.UseVisualStyleBackColor = true;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Location = new System.Drawing.Point(4, 24);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(772, 242);
            this.tabPageSettings.TabIndex = 3;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // FrmMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 416);
            this.Controls.Add(this.groupBoxCoordinates);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStripMain);
            this.DoubleBuffered = true;
            this.Name = "FrmMachine";
            this.Text = "FrmMachine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMachine_FormClosing);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageOverrides.ResumeLayout(false);
            this.tabPageJog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxCoordinates;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonConnect;
        private System.Windows.Forms.ToolStripButton toolStripButtonDisconnect;
        private System.Windows.Forms.ToolStripButton toolStripButtonClearAlarm;
        private System.Windows.Forms.ToolStripButton toolStripButtonResume;
        private System.Windows.Forms.ToolStripButton toolStripButtonHome;
        private Controls.Selection selection5;
        private Controls.Selection selection4;
        private Controls.Selection selection3;
        private Controls.Selection selection2;
        private Controls.Selection selection1;
        private Controls.JogControl jogControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPageOverrides;
        private System.Windows.Forms.TabPage tabPageJog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonProbe;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonPause;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.TabPage tabPageSpindle;
        private System.Windows.Forms.TabPage tabPageServiceSchedule;
        private System.Windows.Forms.TabPage tabPageUsage;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
    }
}