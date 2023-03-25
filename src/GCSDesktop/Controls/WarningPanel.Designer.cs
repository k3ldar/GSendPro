namespace GSendDesktop.Controls
{
    partial class WarningPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarningPanel));
            this.panel1 = new System.Windows.Forms.Panel();
            this.imageClose = new System.Windows.Forms.PictureBox();
            this.iageWarning = new System.Windows.Forms.PictureBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iageWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.imageClose);
            this.panel1.Controls.Add(this.iageWarning);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(156, 24);
            this.panel1.TabIndex = 0;
            // 
            // imageClose
            // 
            this.imageClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imageClose.Image = ((System.Drawing.Image)(resources.GetObject("imageClose.Image")));
            this.imageClose.Location = new System.Drawing.Point(136, 4);
            this.imageClose.Name = "imageClose";
            this.imageClose.Size = new System.Drawing.Size(16, 16);
            this.imageClose.TabIndex = 2;
            this.imageClose.TabStop = false;
            this.imageClose.Click += new System.EventHandler(this.imageClose_Click);
            // 
            // iageWarning
            // 
            this.iageWarning.Image = ((System.Drawing.Image)(resources.GetObject("iageWarning.Image")));
            this.iageWarning.Location = new System.Drawing.Point(9, 4);
            this.iageWarning.Name = "iageWarning";
            this.iageWarning.Size = new System.Drawing.Size(16, 16);
            this.iageWarning.TabIndex = 1;
            this.iageWarning.TabStop = false;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(29, 5);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(38, 15);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "label1";
            // 
            // WarningPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.Name = "WarningPanel";
            this.Size = new System.Drawing.Size(156, 24);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iageWarning)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox iageWarning;
        private System.Windows.Forms.PictureBox imageClose;
    }
}
