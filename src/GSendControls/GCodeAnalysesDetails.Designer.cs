namespace GSendControls
{
    partial class GCodeAnalysesDetails
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
            this.lblFileNameDesc = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.listViewAnalyses = new GSendControls.ListViewEx();
            this.columnHeaderProperty = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderValue = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // lblFileNameDesc
            // 
            this.lblFileNameDesc.AutoSize = true;
            this.lblFileNameDesc.Location = new System.Drawing.Point(7, 7);
            this.lblFileNameDesc.Name = "lblFileNameDesc";
            this.lblFileNameDesc.Size = new System.Drawing.Size(23, 15);
            this.lblFileNameDesc.TabIndex = 0;
            this.lblFileNameDesc.Text = "file";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(78, 7);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(56, 15);
            this.lblFileName.TabIndex = 1;
            this.lblFileName.Text = "file name";
            // 
            // listViewAnalyses
            // 
            this.listViewAnalyses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAnalyses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderProperty,
            this.columnHeaderValue});
            this.listViewAnalyses.Location = new System.Drawing.Point(7, 36);
            this.listViewAnalyses.Name = "listViewAnalyses";
            this.listViewAnalyses.Size = new System.Drawing.Size(423, 179);
            this.listViewAnalyses.TabIndex = 3;
            this.listViewAnalyses.UseCompatibleStateImageBehavior = false;
            this.listViewAnalyses.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderProperty
            // 
            this.columnHeaderProperty.Width = 160;
            // 
            // columnHeaderValue
            // 
            this.columnHeaderValue.Width = 160;
            // 
            // GCodeAnalysesDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewAnalyses);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.lblFileNameDesc);
            this.Name = "GCodeAnalysesDetails";
            this.Size = new System.Drawing.Size(433, 218);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFileNameDesc;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.ColumnHeader columnHeaderProperty;
        private System.Windows.Forms.ColumnHeader columnHeaderValue;
        public GSendControls.ListViewEx listViewAnalyses;
    }
}
