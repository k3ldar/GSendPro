namespace GSendControls
{
    partial class WarningContainer
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
            this.flowLayoutWarningErrors = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowLayoutWarningErrors
            // 
            this.flowLayoutWarningErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutWarningErrors.AutoScroll = true;
            this.flowLayoutWarningErrors.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutWarningErrors.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutWarningErrors.Name = "flowLayoutWarningErrors";
            this.flowLayoutWarningErrors.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.flowLayoutWarningErrors.Size = new System.Drawing.Size(309, 48);
            this.flowLayoutWarningErrors.TabIndex = 12;
            // 
            // WarningContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutWarningErrors);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(2048, 48);
            this.MinimumSize = new System.Drawing.Size(204, 27);
            this.Name = "WarningContainer";
            this.Size = new System.Drawing.Size(309, 48);
            this.SizeChanged += new System.EventHandler(this.WarningContainer_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutWarningErrors;
    }
}
