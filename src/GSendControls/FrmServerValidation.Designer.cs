namespace GSendControls
{
    partial class FrmServerValidation
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
            lblServerUri = new System.Windows.Forms.Label();
            txtServerAddress = new System.Windows.Forms.TextBox();
            btnOK = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            lblServerValidation = new System.Windows.Forms.Label();
            pnlLicenseCheck = new System.Windows.Forms.Panel();
            btnCheckNow = new System.Windows.Forms.Button();
            lblNextServerCheck = new System.Windows.Forms.Label();
            tmrLicenseCheck = new System.Windows.Forms.Timer(components);
            btnViewLicense = new System.Windows.Forms.Button();
            pnlLicenseCheck.SuspendLayout();
            SuspendLayout();
            // 
            // lblServerUri
            // 
            lblServerUri.AutoSize = true;
            lblServerUri.Location = new System.Drawing.Point(12, 18);
            lblServerUri.Name = "lblServerUri";
            lblServerUri.Size = new System.Drawing.Size(38, 15);
            lblServerUri.TabIndex = 0;
            lblServerUri.Text = "label1";
            // 
            // txtServerAddress
            // 
            txtServerAddress.Location = new System.Drawing.Point(12, 36);
            txtServerAddress.Name = "txtServerAddress";
            txtServerAddress.Size = new System.Drawing.Size(461, 23);
            txtServerAddress.TabIndex = 1;
            txtServerAddress.TextChanged += txtServerAddress_TextChanged;
            // 
            // btnOK
            // 
            btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnOK.Location = new System.Drawing.Point(398, 174);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(75, 23);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(317, 174);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(75, 23);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblServerValidation
            // 
            lblServerValidation.AutoSize = true;
            lblServerValidation.Location = new System.Drawing.Point(12, 80);
            lblServerValidation.Name = "lblServerValidation";
            lblServerValidation.Size = new System.Drawing.Size(38, 15);
            lblServerValidation.TabIndex = 4;
            lblServerValidation.Text = "label1";
            // 
            // pnlLicenseCheck
            // 
            pnlLicenseCheck.Controls.Add(btnCheckNow);
            pnlLicenseCheck.Controls.Add(lblNextServerCheck);
            pnlLicenseCheck.Location = new System.Drawing.Point(1, 98);
            pnlLicenseCheck.Name = "pnlLicenseCheck";
            pnlLicenseCheck.Size = new System.Drawing.Size(472, 41);
            pnlLicenseCheck.TabIndex = 5;
            // 
            // btnCheckNow
            // 
            btnCheckNow.Location = new System.Drawing.Point(361, 11);
            btnCheckNow.Name = "btnCheckNow";
            btnCheckNow.Size = new System.Drawing.Size(108, 23);
            btnCheckNow.TabIndex = 1;
            btnCheckNow.Text = "Check Now";
            btnCheckNow.UseVisualStyleBackColor = true;
            btnCheckNow.Click += btnCheckNow_Click;
            // 
            // lblNextServerCheck
            // 
            lblNextServerCheck.AutoSize = true;
            lblNextServerCheck.Location = new System.Drawing.Point(11, 15);
            lblNextServerCheck.Name = "lblNextServerCheck";
            lblNextServerCheck.Size = new System.Drawing.Size(38, 15);
            lblNextServerCheck.TabIndex = 0;
            lblNextServerCheck.Text = "label1";
            // 
            // tmrLicenseCheck
            // 
            tmrLicenseCheck.Interval = 300;
            tmrLicenseCheck.Tick += tmrLicenseCheck_Tick;
            // 
            // btnViewLicense
            // 
            btnViewLicense.Location = new System.Drawing.Point(12, 174);
            btnViewLicense.Name = "btnViewLicense";
            btnViewLicense.Size = new System.Drawing.Size(91, 23);
            btnViewLicense.TabIndex = 6;
            btnViewLicense.Text = "button1";
            btnViewLicense.UseVisualStyleBackColor = true;
            btnViewLicense.Visible = false;
            btnViewLicense.Click += btnViewLicense_Click;
            // 
            // FrmServerValidation
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(485, 209);
            Controls.Add(btnViewLicense);
            Controls.Add(pnlLicenseCheck);
            Controls.Add(lblServerValidation);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(txtServerAddress);
            Controls.Add(lblServerUri);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmServerValidation";
            Text = "FrmLicenseValidation";
            FormClosing += FrmServerValidation_FormClosing;
            Shown += FrmLicenseValidation_Shown;
            pnlLicenseCheck.ResumeLayout(false);
            pnlLicenseCheck.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblServerUri;
        private System.Windows.Forms.TextBox txtServerAddress;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblServerValidation;
        private System.Windows.Forms.Panel pnlLicenseCheck;
        private System.Windows.Forms.Button btnCheckNow;
        private System.Windows.Forms.Label lblNextServerCheck;
        private System.Windows.Forms.Timer tmrLicenseCheck;
        private System.Windows.Forms.Button btnViewLicense;
    }
}