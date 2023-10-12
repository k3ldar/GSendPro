namespace GSendControls
{
    partial class WizardForm
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
            btnNext = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            flowPanelWizard = new System.Windows.Forms.FlowLayoutPanel();
            btnFinish = new System.Windows.Forms.Button();
            btnPrevious = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // btnNext
            // 
            btnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnNext.Location = new System.Drawing.Point(398, 377);
            btnNext.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnNext.Name = "btnNext";
            btnNext.Size = new System.Drawing.Size(88, 27);
            btnNext.TabIndex = 1;
            btnNext.Text = "&Next";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(209, 377);
            btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(88, 27);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // flowPanelWizard
            // 
            flowPanelWizard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            flowPanelWizard.Location = new System.Drawing.Point(14, 14);
            flowPanelWizard.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            flowPanelWizard.Name = "flowPanelWizard";
            flowPanelWizard.Size = new System.Drawing.Size(566, 356);
            flowPanelWizard.TabIndex = 0;
            // 
            // btnFinish
            // 
            btnFinish.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnFinish.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnFinish.Location = new System.Drawing.Point(493, 377);
            btnFinish.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new System.Drawing.Size(88, 27);
            btnFinish.TabIndex = 2;
            btnFinish.Text = "&Finish";
            btnFinish.UseVisualStyleBackColor = true;
            btnFinish.Click += btnFinish_Click;
            // 
            // btnPrevious
            // 
            btnPrevious.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnPrevious.Location = new System.Drawing.Point(304, 377);
            btnPrevious.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new System.Drawing.Size(88, 27);
            btnPrevious.TabIndex = 3;
            btnPrevious.Text = "&Previous";
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += btnPrevious_Click;
            // 
            // WizardForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(594, 417);
            Controls.Add(btnPrevious);
            Controls.Add(btnFinish);
            Controls.Add(flowPanelWizard);
            Controls.Add(btnCancel);
            Controls.Add(btnNext);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "WizardForm";
            Text = "Wizard";
            FormClosing += WizardForm_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FlowLayoutPanel flowPanelWizard;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnPrevious;
    }
}