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
            this.components = new System.ComponentModel.Container();
            this.lblServerUri = new System.Windows.Forms.Label();
            this.txtServerAddress = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblLicenseValidation = new System.Windows.Forms.Label();
            this.pnlLicenseCheck = new System.Windows.Forms.Panel();
            this.btnCheckNow = new System.Windows.Forms.Button();
            this.lblNextLicenseCheck = new System.Windows.Forms.Label();
            this.tmrLicenseCheck = new System.Windows.Forms.Timer(this.components);
            this.btnViewLicense = new System.Windows.Forms.Button();
            this.pnlLicenseCheck.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblServerUri
            // 
            this.lblServerUri.AutoSize = true;
            this.lblServerUri.Location = new System.Drawing.Point(12, 18);
            this.lblServerUri.Name = "lblServerUri";
            this.lblServerUri.Size = new System.Drawing.Size(38, 15);
            this.lblServerUri.TabIndex = 0;
            this.lblServerUri.Text = "label1";
            // 
            // txtServerAddress
            // 
            this.txtServerAddress.Location = new System.Drawing.Point(12, 36);
            this.txtServerAddress.Name = "txtServerAddress";
            this.txtServerAddress.Size = new System.Drawing.Size(461, 23);
            this.txtServerAddress.TabIndex = 1;
            this.txtServerAddress.TextChanged += new System.EventHandler(this.txtServerAddress_TextChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(398, 174);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "button1";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(317, 174);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "button2";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblLicenseValidation
            // 
            this.lblLicenseValidation.AutoSize = true;
            this.lblLicenseValidation.Location = new System.Drawing.Point(12, 80);
            this.lblLicenseValidation.Name = "lblLicenseValidation";
            this.lblLicenseValidation.Size = new System.Drawing.Size(38, 15);
            this.lblLicenseValidation.TabIndex = 4;
            this.lblLicenseValidation.Text = "label1";
            // 
            // pnlLicenseCheck
            // 
            this.pnlLicenseCheck.Controls.Add(this.btnCheckNow);
            this.pnlLicenseCheck.Controls.Add(this.lblNextLicenseCheck);
            this.pnlLicenseCheck.Location = new System.Drawing.Point(1, 98);
            this.pnlLicenseCheck.Name = "pnlLicenseCheck";
            this.pnlLicenseCheck.Size = new System.Drawing.Size(472, 41);
            this.pnlLicenseCheck.TabIndex = 5;
            // 
            // btnCheckNow
            // 
            this.btnCheckNow.Location = new System.Drawing.Point(361, 11);
            this.btnCheckNow.Name = "btnCheckNow";
            this.btnCheckNow.Size = new System.Drawing.Size(108, 23);
            this.btnCheckNow.TabIndex = 1;
            this.btnCheckNow.Text = "Check Now";
            this.btnCheckNow.UseVisualStyleBackColor = true;
            this.btnCheckNow.Click += new System.EventHandler(this.btnCheckNow_Click);
            // 
            // lblNextLicenseCheck
            // 
            this.lblNextLicenseCheck.AutoSize = true;
            this.lblNextLicenseCheck.Location = new System.Drawing.Point(11, 15);
            this.lblNextLicenseCheck.Name = "lblNextLicenseCheck";
            this.lblNextLicenseCheck.Size = new System.Drawing.Size(38, 15);
            this.lblNextLicenseCheck.TabIndex = 0;
            this.lblNextLicenseCheck.Text = "label1";
            // 
            // tmrLicenseCheck
            // 
            this.tmrLicenseCheck.Interval = 300;
            this.tmrLicenseCheck.Tick += new System.EventHandler(this.tmrLicenseCheck_Tick);
            // 
            // btnViewLicense
            // 
            this.btnViewLicense.Location = new System.Drawing.Point(12, 174);
            this.btnViewLicense.Name = "btnViewLicense";
            this.btnViewLicense.Size = new System.Drawing.Size(91, 23);
            this.btnViewLicense.TabIndex = 6;
            this.btnViewLicense.Text = "button1";
            this.btnViewLicense.UseVisualStyleBackColor = true;
            this.btnViewLicense.Click += new System.EventHandler(this.btnViewLicense_Click);
            // 
            // FrmServerValidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 209);
            this.Controls.Add(this.btnViewLicense);
            this.Controls.Add(this.pnlLicenseCheck);
            this.Controls.Add(this.lblLicenseValidation);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtServerAddress);
            this.Controls.Add(this.lblServerUri);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmServerValidation";
            this.Text = "FrmLicenseValidation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmServerValidation_FormClosing);
            this.Shown += new System.EventHandler(this.FrmLicenseValidation_Shown);
            this.pnlLicenseCheck.ResumeLayout(false);
            this.pnlLicenseCheck.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblServerUri;
        private System.Windows.Forms.TextBox txtServerAddress;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblLicenseValidation;
        private System.Windows.Forms.Panel pnlLicenseCheck;
        private System.Windows.Forms.Button btnCheckNow;
        private System.Windows.Forms.Label lblNextLicenseCheck;
        private System.Windows.Forms.Timer tmrLicenseCheck;
        private System.Windows.Forms.Button btnViewLicense;
    }
}