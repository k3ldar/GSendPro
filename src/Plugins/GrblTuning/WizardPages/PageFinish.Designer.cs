namespace GrblTuningWizard
{
    partial class PageFinish
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
            lblFinishHeader = new Label();
            lblMachineNameHeader = new Label();
            lblMachineName = new Label();
            numericAdjustment = new NumericUpDown();
            lblReductionAmount = new Label();
            lvDetails = new GSendControls.ListViewEx();
            columnHeaderName = new ColumnHeader();
            columnHeaderOriginal = new ColumnHeader();
            columnHeaderMax = new ColumnHeader();
            columnHeaderFinal = new ColumnHeader();
            columnHeaderPercent = new ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)numericAdjustment).BeginInit();
            SuspendLayout();
            // 
            // lblFinishHeader
            // 
            lblFinishHeader.Location = new Point(3, 3);
            lblFinishHeader.Name = "lblFinishHeader";
            lblFinishHeader.Size = new Size(554, 43);
            lblFinishHeader.TabIndex = 0;
            lblFinishHeader.Text = "You have now finished tuning your machine, it is recommended that the final acceleration and feed rates are reduced by about 10 to 20% below the maximum rates.";
            // 
            // lblMachineNameHeader
            // 
            lblMachineNameHeader.AutoSize = true;
            lblMachineNameHeader.Location = new Point(3, 56);
            lblMachineNameHeader.Name = "lblMachineNameHeader";
            lblMachineNameHeader.Size = new Size(38, 15);
            lblMachineNameHeader.TabIndex = 1;
            lblMachineNameHeader.Text = "label2";
            // 
            // lblMachineName
            // 
            lblMachineName.AutoSize = true;
            lblMachineName.Location = new Point(156, 56);
            lblMachineName.Name = "lblMachineName";
            lblMachineName.Size = new Size(38, 15);
            lblMachineName.TabIndex = 2;
            lblMachineName.Text = "label2";
            // 
            // numericAdjustment
            // 
            numericAdjustment.Location = new Point(3, 294);
            numericAdjustment.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numericAdjustment.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericAdjustment.Name = "numericAdjustment";
            numericAdjustment.Size = new Size(75, 23);
            numericAdjustment.TabIndex = 30;
            numericAdjustment.Value = new decimal(new int[] { 20, 0, 0, 0 });
            numericAdjustment.ValueChanged += numericAdjustment_ValueChanged;
            // 
            // lblReductionAmount
            // 
            lblReductionAmount.AutoSize = true;
            lblReductionAmount.Location = new Point(3, 276);
            lblReductionAmount.Name = "lblReductionAmount";
            lblReductionAmount.Size = new Size(129, 15);
            lblReductionAmount.TabIndex = 31;
            lblReductionAmount.Text = "Final reduction percent";
            // 
            // lvDetails
            // 
            lvDetails.AllowColumnReorder = true;
            lvDetails.Columns.AddRange(new ColumnHeader[] { columnHeaderName, columnHeaderOriginal, columnHeaderMax, columnHeaderFinal, columnHeaderPercent });
            lvDetails.Location = new Point(3, 91);
            lvDetails.Name = "lvDetails";
            lvDetails.OwnerDraw = true;
            lvDetails.SaveName = "";
            lvDetails.ShowToolTip = false;
            lvDetails.Size = new Size(560, 168);
            lvDetails.TabIndex = 32;
            lvDetails.UseCompatibleStateImageBehavior = false;
            lvDetails.View = View.Details;
            // 
            // columnHeaderName
            // 
            columnHeaderName.Width = 160;
            // 
            // columnHeaderOriginal
            // 
            columnHeaderOriginal.Width = 90;
            // 
            // columnHeaderMax
            // 
            columnHeaderMax.Width = 90;
            // 
            // columnHeaderFinal
            // 
            columnHeaderFinal.Width = 90;
            // 
            // columnHeaderPercent
            // 
            columnHeaderPercent.Width = 90;
            // 
            // PageFinish
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(lvDetails);
            Controls.Add(lblReductionAmount);
            Controls.Add(numericAdjustment);
            Controls.Add(lblMachineName);
            Controls.Add(lblMachineNameHeader);
            Controls.Add(lblFinishHeader);
            Name = "PageFinish";
            ((System.ComponentModel.ISupportInitialize)numericAdjustment).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFinishHeader;
        private Label lblMachineNameHeader;
        private Label lblMachineName;
        private NumericUpDown numericAdjustment;
        private Label lblReductionAmount;
        private GSendControls.ListViewEx lvDetails;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderOriginal;
        private ColumnHeader columnHeaderMax;
        private ColumnHeader columnHeaderFinal;
        private ColumnHeader columnHeaderPercent;
    }
}
