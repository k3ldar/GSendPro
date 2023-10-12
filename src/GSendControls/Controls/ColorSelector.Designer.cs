namespace GSendControls
{
    partial class ColorSelector
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
            this.lblDescription = new System.Windows.Forms.Label();
            this.cmbColorSelector = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(4, 4);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Description";
            // 
            // cmbColorSelector
            // 
            this.cmbColorSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbColorSelector.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbColorSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColorSelector.FormattingEnabled = true;
            this.cmbColorSelector.Location = new System.Drawing.Point(7, 21);
            this.cmbColorSelector.Name = "cmbColorSelector";
            this.cmbColorSelector.Size = new System.Drawing.Size(158, 21);
            this.cmbColorSelector.TabIndex = 1;
            this.cmbColorSelector.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbColorSelector_DrawItem);
            // 
            // ColorSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbColorSelector);
            this.Controls.Add(this.lblDescription);
            this.Name = "ColorSelector";
            this.Size = new System.Drawing.Size(176, 50);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.ComboBox cmbColorSelector;
    }
}
