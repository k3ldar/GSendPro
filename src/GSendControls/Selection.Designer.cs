namespace GSendControls
{
    partial class Selection
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
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            this.trackBarValue = new System.Windows.Forms.TrackBar();
            this.lblDescription = new System.Windows.Forms.Label();
            this.groupBoxMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarValue)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMain.Controls.Add(this.trackBarValue);
            this.groupBoxMain.Controls.Add(this.lblDescription);
            this.groupBoxMain.Location = new System.Drawing.Point(3, 3);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Size = new System.Drawing.Size(76, 144);
            this.groupBoxMain.TabIndex = 0;
            this.groupBoxMain.TabStop = false;
            this.groupBoxMain.Text = "groupBoxMain";
            // 
            // trackBarValue
            // 
            this.trackBarValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.trackBarValue.Location = new System.Drawing.Point(6, 22);
            this.trackBarValue.Name = "trackBarValue";
            this.trackBarValue.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarValue.Size = new System.Drawing.Size(45, 97);
            this.trackBarValue.TabIndex = 1;
            this.trackBarValue.ValueChanged += new System.EventHandler(this.trackBarValue_ValueChanged);
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(6, 122);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(38, 15);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "label1";
            // 
            // Selection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxMain);
            this.Name = "Selection";
            this.Size = new System.Drawing.Size(82, 150);
            this.groupBoxMain.ResumeLayout(false);
            this.groupBoxMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMain;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TrackBar trackBarValue;
    }
}
