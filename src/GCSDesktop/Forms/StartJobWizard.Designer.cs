namespace GSendDesktop.Forms
{
    partial class StartJobWizard
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartJobWizard));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblJobProfile = new System.Windows.Forms.Label();
            this.cmbJobProfiles = new System.Windows.Forms.ComboBox();
            this.cmbTool = new System.Windows.Forms.ComboBox();
            this.lblTool = new System.Windows.Forms.Label();
            this.lblWarnings = new System.Windows.Forms.Label();
            this.lstCoordinates = new System.Windows.Forms.ListBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cbSimulate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnStart.Location = new System.Drawing.Point(439, 237);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "button1";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(358, 237);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "button2";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblJobProfile
            // 
            this.lblJobProfile.AutoSize = true;
            this.lblJobProfile.Location = new System.Drawing.Point(12, 9);
            this.lblJobProfile.Name = "lblJobProfile";
            this.lblJobProfile.Size = new System.Drawing.Size(38, 15);
            this.lblJobProfile.TabIndex = 2;
            this.lblJobProfile.Text = "label1";
            // 
            // cmbJobProfiles
            // 
            this.cmbJobProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJobProfiles.FormattingEnabled = true;
            this.cmbJobProfiles.Location = new System.Drawing.Point(12, 27);
            this.cmbJobProfiles.Name = "cmbJobProfiles";
            this.cmbJobProfiles.Size = new System.Drawing.Size(269, 23);
            this.cmbJobProfiles.TabIndex = 3;
            // 
            // cmbTool
            // 
            this.cmbTool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTool.FormattingEnabled = true;
            this.cmbTool.Location = new System.Drawing.Point(12, 86);
            this.cmbTool.Name = "cmbTool";
            this.cmbTool.Size = new System.Drawing.Size(269, 23);
            this.cmbTool.TabIndex = 5;
            // 
            // lblTool
            // 
            this.lblTool.AutoSize = true;
            this.lblTool.Location = new System.Drawing.Point(12, 68);
            this.lblTool.Name = "lblTool";
            this.lblTool.Size = new System.Drawing.Size(38, 15);
            this.lblTool.TabIndex = 4;
            this.lblTool.Text = "label1";
            // 
            // lblWarnings
            // 
            this.lblWarnings.AutoSize = true;
            this.lblWarnings.Location = new System.Drawing.Point(296, 9);
            this.lblWarnings.Name = "lblWarnings";
            this.lblWarnings.Size = new System.Drawing.Size(38, 15);
            this.lblWarnings.TabIndex = 6;
            this.lblWarnings.Text = "label1";
            // 
            // lstCoordinates
            // 
            this.lstCoordinates.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstCoordinates.FormattingEnabled = true;
            this.lstCoordinates.ItemHeight = 18;
            this.lstCoordinates.Location = new System.Drawing.Point(296, 27);
            this.lstCoordinates.Name = "lstCoordinates";
            this.lstCoordinates.Size = new System.Drawing.Size(218, 148);
            this.lstCoordinates.TabIndex = 7;
            this.lstCoordinates.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstCoordinates_DrawItem);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Error_red_16x16.png");
            this.imageList1.Images.SetKeyName(1, "tick16x16.png");
            // 
            // cbSimulate
            // 
            this.cbSimulate.AutoSize = true;
            this.cbSimulate.Location = new System.Drawing.Point(12, 240);
            this.cbSimulate.Name = "cbSimulate";
            this.cbSimulate.Size = new System.Drawing.Size(71, 19);
            this.cbSimulate.TabIndex = 8;
            this.cbSimulate.Text = "simulate";
            this.cbSimulate.UseVisualStyleBackColor = true;
            // 
            // StartJobWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 272);
            this.Controls.Add(this.cbSimulate);
            this.Controls.Add(this.lstCoordinates);
            this.Controls.Add(this.lblWarnings);
            this.Controls.Add(this.cmbTool);
            this.Controls.Add(this.lblTool);
            this.Controls.Add(this.cmbJobProfiles);
            this.Controls.Add(this.lblJobProfile);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartJobWizard";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "StartJobWizard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblJobProfile;
        private System.Windows.Forms.ComboBox cmbJobProfiles;
        private System.Windows.Forms.ComboBox cmbTool;
        private System.Windows.Forms.Label lblTool;
        private System.Windows.Forms.Label lblWarnings;
        private System.Windows.Forms.ListBox lstCoordinates;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox cbSimulate;
    }
}