namespace GSendDesktop.Forms
{
    partial class FrmRegisterService
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
            this.dateTimeServiceDate = new System.Windows.Forms.DateTimePicker();
            this.lblServiceDate = new System.Windows.Forms.Label();
            this.grpServiceType = new System.Windows.Forms.GroupBox();
            this.rbMajor = new System.Windows.Forms.RadioButton();
            this.rbMinor = new System.Windows.Forms.RadioButton();
            this.rbDaily = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpServiceType.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimeServiceDate
            // 
            this.dateTimeServiceDate.CustomFormat = "dd MMM yyyy HH:mm";
            this.dateTimeServiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeServiceDate.Location = new System.Drawing.Point(12, 27);
            this.dateTimeServiceDate.Name = "dateTimeServiceDate";
            this.dateTimeServiceDate.Size = new System.Drawing.Size(200, 23);
            this.dateTimeServiceDate.TabIndex = 0;
            // 
            // lblServiceDate
            // 
            this.lblServiceDate.AutoSize = true;
            this.lblServiceDate.Location = new System.Drawing.Point(12, 9);
            this.lblServiceDate.Name = "lblServiceDate";
            this.lblServiceDate.Size = new System.Drawing.Size(38, 15);
            this.lblServiceDate.TabIndex = 1;
            this.lblServiceDate.Text = "label1";
            // 
            // grpServiceType
            // 
            this.grpServiceType.Controls.Add(this.rbMajor);
            this.grpServiceType.Controls.Add(this.rbMinor);
            this.grpServiceType.Controls.Add(this.rbDaily);
            this.grpServiceType.Location = new System.Drawing.Point(12, 67);
            this.grpServiceType.Name = "grpServiceType";
            this.grpServiceType.Size = new System.Drawing.Size(200, 100);
            this.grpServiceType.TabIndex = 2;
            this.grpServiceType.TabStop = false;
            this.grpServiceType.Text = "groupBox1";
            // 
            // rbMajor
            // 
            this.rbMajor.AutoSize = true;
            this.rbMajor.Location = new System.Drawing.Point(6, 72);
            this.rbMajor.Name = "rbMajor";
            this.rbMajor.Size = new System.Drawing.Size(94, 19);
            this.rbMajor.TabIndex = 2;
            this.rbMajor.Text = "radioButton3";
            this.rbMajor.UseVisualStyleBackColor = true;
            // 
            // rbMinor
            // 
            this.rbMinor.AutoSize = true;
            this.rbMinor.Location = new System.Drawing.Point(6, 47);
            this.rbMinor.Name = "rbMinor";
            this.rbMinor.Size = new System.Drawing.Size(94, 19);
            this.rbMinor.TabIndex = 1;
            this.rbMinor.Text = "radioButton2";
            this.rbMinor.UseVisualStyleBackColor = true;
            // 
            // rbDaily
            // 
            this.rbDaily.AutoSize = true;
            this.rbDaily.Checked = true;
            this.rbDaily.Location = new System.Drawing.Point(6, 22);
            this.rbDaily.Name = "rbDaily";
            this.rbDaily.Size = new System.Drawing.Size(94, 19);
            this.rbDaily.TabIndex = 0;
            this.rbDaily.TabStop = true;
            this.rbDaily.Text = "radioButton1";
            this.rbDaily.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(137, 182);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "button1";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(56, 182);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "button2";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmRegisterService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(224, 217);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpServiceType);
            this.Controls.Add(this.lblServiceDate);
            this.Controls.Add(this.dateTimeServiceDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRegisterService";
            this.ShowIcon = false;
            this.Text = "RegisterService";
            this.grpServiceType.ResumeLayout(false);
            this.grpServiceType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimeServiceDate;
        private System.Windows.Forms.Label lblServiceDate;
        private System.Windows.Forms.GroupBox grpServiceType;
        private System.Windows.Forms.RadioButton rbMajor;
        private System.Windows.Forms.RadioButton rbMinor;
        private System.Windows.Forms.RadioButton rbDaily;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}