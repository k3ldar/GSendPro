namespace GSendControls
{
    partial class AboutBox
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
            this.btnOK = new System.Windows.Forms.Button();
            this.imageIcon = new System.Windows.Forms.PictureBox();
            this.lblProgName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblVerNo = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lnkHomePage = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.imageIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(364, 212);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "button1";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // imageIcon
            // 
            this.imageIcon.Location = new System.Drawing.Point(12, 12);
            this.imageIcon.Name = "imageIcon";
            this.imageIcon.Size = new System.Drawing.Size(32, 32);
            this.imageIcon.TabIndex = 1;
            this.imageIcon.TabStop = false;
            // 
            // lblProgName
            // 
            this.lblProgName.AutoSize = true;
            this.lblProgName.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblProgName.Location = new System.Drawing.Point(61, 7);
            this.lblProgName.Name = "lblProgName";
            this.lblProgName.Size = new System.Drawing.Size(86, 37);
            this.lblProgName.TabIndex = 2;
            this.lblProgName.Text = "label1";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(67, 75);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(38, 15);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "label1";
            // 
            // lblVerNo
            // 
            this.lblVerNo.AutoSize = true;
            this.lblVerNo.Location = new System.Drawing.Point(177, 75);
            this.lblVerNo.Name = "lblVerNo";
            this.lblVerNo.Size = new System.Drawing.Size(38, 15);
            this.lblVerNo.TabIndex = 4;
            this.lblVerNo.Text = "label1";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(67, 153);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(38, 15);
            this.lblCopyright.TabIndex = 5;
            this.lblCopyright.Text = "label2";
            // 
            // lnkHomePage
            // 
            this.lnkHomePage.AutoSize = true;
            this.lnkHomePage.Location = new System.Drawing.Point(67, 114);
            this.lnkHomePage.Name = "lnkHomePage";
            this.lnkHomePage.Size = new System.Drawing.Size(100, 15);
            this.lnkHomePage.TabIndex = 6;
            this.lnkHomePage.TabStop = true;
            this.lnkHomePage.Text = "https://gsend.pro";
            this.lnkHomePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHomePage_LinkClicked);
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 247);
            this.Controls.Add(this.lnkHomePage);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblVerNo);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblProgName);
            this.Controls.Add(this.imageIcon);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Text = "AboutBox";
            ((System.ComponentModel.ISupportInitialize)(this.imageIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox imageIcon;
        private System.Windows.Forms.Label lblProgName;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblVerNo;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.LinkLabel lnkHomePage;
    }
}