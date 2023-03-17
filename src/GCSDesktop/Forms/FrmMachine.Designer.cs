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
            this.toolStripButtonGetMachines = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddMachine = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPauseAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonResumeAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDisconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClearAlarm = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonResume = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHome = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.tabPageOverrides = new System.Windows.Forms.TabPage();
            this.tabPageJog = new System.Windows.Forms.TabPage();
            this.toolStripMain.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageOverrides.SuspendLayout();
            this.tabPageJog.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCoordinates
            // 
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
            this.toolStripButtonGetMachines,
            this.toolStripButtonAddMachine,
            this.toolStripSeparator1,
            this.toolStripButtonPauseAll,
            this.toolStripButtonResumeAll,
            this.toolStripSeparator2,
            this.toolStripButtonConnect,
            this.toolStripButtonDisconnect,
            this.toolStripButtonClearAlarm,
            this.toolStripButtonResume,
            this.toolStripButtonHome});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(804, 57);
            this.toolStripMain.TabIndex = 6;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripButtonGetMachines
            // 
            this.toolStripButtonGetMachines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGetMachines.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGetMachines.Image")));
            this.toolStripButtonGetMachines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGetMachines.Name = "toolStripButtonGetMachines";
            this.toolStripButtonGetMachines.Size = new System.Drawing.Size(54, 54);
            // 
            // toolStripButtonAddMachine
            // 
            this.toolStripButtonAddMachine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddMachine.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddMachine.Image")));
            this.toolStripButtonAddMachine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddMachine.Name = "toolStripButtonAddMachine";
            this.toolStripButtonAddMachine.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonAddMachine.Text = "Add";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 57);
            // 
            // toolStripButtonPauseAll
            // 
            this.toolStripButtonPauseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPauseAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPauseAll.Image")));
            this.toolStripButtonPauseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPauseAll.Name = "toolStripButtonPauseAll";
            this.toolStripButtonPauseAll.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonPauseAll.Text = "toolStripButton1";
            // 
            // toolStripButtonResumeAll
            // 
            this.toolStripButtonResumeAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonResumeAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonResumeAll.Image")));
            this.toolStripButtonResumeAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonResumeAll.Name = "toolStripButtonResumeAll";
            this.toolStripButtonResumeAll.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonResumeAll.Text = "toolStripButton1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 57);
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
            // toolStripButtonClearAlarm
            // 
            this.toolStripButtonClearAlarm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonClearAlarm.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClearAlarm.Image")));
            this.toolStripButtonClearAlarm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClearAlarm.Name = "toolStripButtonClearAlarm";
            this.toolStripButtonClearAlarm.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonClearAlarm.Text = "toolStripButton1";
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
            // toolStripButtonHome
            // 
            this.toolStripButtonHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHome.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHome.Image")));
            this.toolStripButtonHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHome.Name = "toolStripButtonHome";
            this.toolStripButtonHome.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonHome.Text = "toolStripButton1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 342);
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
            // FrmMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 364);
            this.Controls.Add(this.groupBoxCoordinates);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStripMain);
            this.Name = "FrmMachine";
            this.Text = "FrmMachine";
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
        private System.Windows.Forms.ToolStripButton toolStripButtonGetMachines;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddMachine;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonPauseAll;
        private System.Windows.Forms.ToolStripButton toolStripButtonResumeAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
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
    }
}