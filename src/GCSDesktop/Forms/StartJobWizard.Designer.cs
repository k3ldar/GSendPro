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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartJobWizard));
            btnStart = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            lblJobProfile = new System.Windows.Forms.Label();
            cmbJobProfiles = new System.Windows.Forms.ComboBox();
            cmbTool = new System.Windows.Forms.ComboBox();
            lblTool = new System.Windows.Forms.Label();
            lblWarnings = new System.Windows.Forms.Label();
            lstWarningsAndErrors = new System.Windows.Forms.ListBox();
            imageList1 = new System.Windows.Forms.ImageList(components);
            cbSimulate = new System.Windows.Forms.CheckBox();
            lblErrors = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnStart.Location = new System.Drawing.Point(439, 237);
            btnStart.Name = "btnStart";
            btnStart.Size = new System.Drawing.Size(75, 23);
            btnStart.TabIndex = 0;
            btnStart.Text = "button1";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(358, 237);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "button2";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblJobProfile
            // 
            lblJobProfile.AutoSize = true;
            lblJobProfile.Location = new System.Drawing.Point(12, 9);
            lblJobProfile.Name = "lblJobProfile";
            lblJobProfile.Size = new System.Drawing.Size(38, 15);
            lblJobProfile.TabIndex = 2;
            lblJobProfile.Text = "label1";
            // 
            // cmbJobProfiles
            // 
            cmbJobProfiles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbJobProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbJobProfiles.FormattingEnabled = true;
            cmbJobProfiles.Location = new System.Drawing.Point(12, 27);
            cmbJobProfiles.Name = "cmbJobProfiles";
            cmbJobProfiles.Size = new System.Drawing.Size(229, 24);
            cmbJobProfiles.TabIndex = 3;
            cmbJobProfiles.DrawItem += cmbJobProfiles_DrawItem;
            // 
            // cmbTool
            // 
            cmbTool.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            cmbTool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbTool.FormattingEnabled = true;
            cmbTool.Location = new System.Drawing.Point(12, 86);
            cmbTool.Name = "cmbTool";
            cmbTool.Size = new System.Drawing.Size(229, 24);
            cmbTool.TabIndex = 5;
            cmbTool.DrawItem += cmbTool_DrawItem;
            cmbTool.SelectedIndexChanged += cmbTool_SelectedIndexChanged;
            // 
            // lblTool
            // 
            lblTool.AutoSize = true;
            lblTool.Location = new System.Drawing.Point(12, 68);
            lblTool.Name = "lblTool";
            lblTool.Size = new System.Drawing.Size(38, 15);
            lblTool.TabIndex = 4;
            lblTool.Text = "label1";
            // 
            // lblWarnings
            // 
            lblWarnings.AutoSize = true;
            lblWarnings.Location = new System.Drawing.Point(259, 9);
            lblWarnings.Name = "lblWarnings";
            lblWarnings.Size = new System.Drawing.Size(38, 15);
            lblWarnings.TabIndex = 6;
            lblWarnings.Text = "label1";
            // 
            // lstWarningsAndErrors
            // 
            lstWarningsAndErrors.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            lstWarningsAndErrors.FormattingEnabled = true;
            lstWarningsAndErrors.ItemHeight = 18;
            lstWarningsAndErrors.Location = new System.Drawing.Point(259, 27);
            lstWarningsAndErrors.Name = "lstWarningsAndErrors";
            lstWarningsAndErrors.Size = new System.Drawing.Size(255, 148);
            lstWarningsAndErrors.TabIndex = 7;
            lstWarningsAndErrors.DrawItem += lstWarningAndErrors_DrawItem;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = System.Drawing.Color.Transparent;
            imageList1.Images.SetKeyName(0, "Error_red_16x16.png");
            imageList1.Images.SetKeyName(1, "tick16x16.png");
            imageList1.Images.SetKeyName(2, "Warning_yellow_7231_16x16_cyan.ico");
            // 
            // cbSimulate
            // 
            cbSimulate.AutoSize = true;
            cbSimulate.Location = new System.Drawing.Point(12, 240);
            cbSimulate.Name = "cbSimulate";
            cbSimulate.Size = new System.Drawing.Size(71, 19);
            cbSimulate.TabIndex = 8;
            cbSimulate.Text = "simulate";
            cbSimulate.UseVisualStyleBackColor = true;
            // 
            // lblErrors
            // 
            lblErrors.AutoSize = true;
            lblErrors.Location = new System.Drawing.Point(12, 196);
            lblErrors.Name = "lblErrors";
            lblErrors.Size = new System.Drawing.Size(38, 15);
            lblErrors.TabIndex = 9;
            lblErrors.Text = "label1";
            // 
            // StartJobWizard
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(526, 272);
            Controls.Add(lblErrors);
            Controls.Add(cbSimulate);
            Controls.Add(lstWarningsAndErrors);
            Controls.Add(lblWarnings);
            Controls.Add(cmbTool);
            Controls.Add(lblTool);
            Controls.Add(cmbJobProfiles);
            Controls.Add(lblJobProfile);
            Controls.Add(btnCancel);
            Controls.Add(btnStart);
            DoubleBuffered = true;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StartJobWizard";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            Text = "StartJobWizard";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblJobProfile;
        private System.Windows.Forms.ComboBox cmbJobProfiles;
        private System.Windows.Forms.ComboBox cmbTool;
        private System.Windows.Forms.Label lblTool;
        private System.Windows.Forms.Label lblWarnings;
        private System.Windows.Forms.ListBox lstWarningsAndErrors;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox cbSimulate;
        private System.Windows.Forms.Label lblErrors;
    }
}