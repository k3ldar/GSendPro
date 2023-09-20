namespace GrblTuningWizard
{
    partial class MachineTuneZAxisAcceleration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MachineTuneZAxisAcceleration));
            lblAxisHeader = new Label();
            lblTuneAxis = new Label();
            lblOriginalValueHeader = new Label();
            lblOriginalValue = new Label();
            numericIncrements = new NumericUpDown();
            lblIncrements = new Label();
            lblCurrentTestValue = new Label();
            lblCurrentTestValueHeader = new Label();
            btnRunTest = new Button();
            btnIncrease = new Button();
            btnDecrease = new Button();
            lblMachineRunning = new Label();
            ((System.ComponentModel.ISupportInitialize)numericIncrements).BeginInit();
            SuspendLayout();
            // 
            // lblAxisHeader
            // 
            lblAxisHeader.AutoSize = true;
            lblAxisHeader.Location = new Point(3, 0);
            lblAxisHeader.Name = "lblAxisHeader";
            lblAxisHeader.Size = new Size(38, 15);
            lblAxisHeader.TabIndex = 0;
            lblAxisHeader.Text = "label1";
            // 
            // lblTuneAxis
            // 
            lblTuneAxis.Location = new Point(3, 26);
            lblTuneAxis.Name = "lblTuneAxis";
            lblTuneAxis.Size = new Size(560, 82);
            lblTuneAxis.TabIndex = 1;
            lblTuneAxis.Text = resources.GetString("lblTuneAxis.Text");
            // 
            // lblOriginalValueHeader
            // 
            lblOriginalValueHeader.AutoSize = true;
            lblOriginalValueHeader.Location = new Point(3, 108);
            lblOriginalValueHeader.Name = "lblOriginalValueHeader";
            lblOriginalValueHeader.Size = new Size(38, 15);
            lblOriginalValueHeader.TabIndex = 2;
            lblOriginalValueHeader.Text = "label1";
            // 
            // lblOriginalValue
            // 
            lblOriginalValue.AutoSize = true;
            lblOriginalValue.Location = new Point(3, 123);
            lblOriginalValue.Name = "lblOriginalValue";
            lblOriginalValue.Size = new Size(77, 15);
            lblOriginalValue.TabIndex = 3;
            lblOriginalValue.Text = "lblOriginalVal";
            // 
            // numericIncrements
            // 
            numericIncrements.Location = new Point(3, 166);
            numericIncrements.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            numericIncrements.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            numericIncrements.Name = "numericIncrements";
            numericIncrements.Size = new Size(120, 23);
            numericIncrements.TabIndex = 4;
            numericIncrements.Value = new decimal(new int[] { 150, 0, 0, 0 });
            // 
            // lblIncrements
            // 
            lblIncrements.AutoSize = true;
            lblIncrements.Location = new Point(3, 148);
            lblIncrements.Name = "lblIncrements";
            lblIncrements.Size = new Size(38, 15);
            lblIncrements.TabIndex = 5;
            lblIncrements.Text = "label1";
            // 
            // lblCurrentTestValue
            // 
            lblCurrentTestValue.AutoSize = true;
            lblCurrentTestValue.Location = new Point(114, 123);
            lblCurrentTestValue.Name = "lblCurrentTestValue";
            lblCurrentTestValue.Size = new Size(77, 15);
            lblCurrentTestValue.TabIndex = 7;
            lblCurrentTestValue.Text = "lblOriginalVal";
            // 
            // lblCurrentTestValueHeader
            // 
            lblCurrentTestValueHeader.AutoSize = true;
            lblCurrentTestValueHeader.Location = new Point(114, 108);
            lblCurrentTestValueHeader.Name = "lblCurrentTestValueHeader";
            lblCurrentTestValueHeader.Size = new Size(38, 15);
            lblCurrentTestValueHeader.TabIndex = 6;
            lblCurrentTestValueHeader.Text = "label1";
            // 
            // btnRunTest
            // 
            btnRunTest.Location = new Point(378, 164);
            btnRunTest.Name = "btnRunTest";
            btnRunTest.Size = new Size(111, 23);
            btnRunTest.TabIndex = 8;
            btnRunTest.Text = "button1";
            btnRunTest.UseVisualStyleBackColor = true;
            btnRunTest.Click += btnRunTest_Click;
            // 
            // btnIncrease
            // 
            btnIncrease.Location = new Point(243, 164);
            btnIncrease.Name = "btnIncrease";
            btnIncrease.Size = new Size(75, 23);
            btnIncrease.TabIndex = 9;
            btnIncrease.Text = "button1";
            btnIncrease.UseVisualStyleBackColor = true;
            btnIncrease.Click += btnIncrease_Click;
            // 
            // btnDecrease
            // 
            btnDecrease.Location = new Point(152, 164);
            btnDecrease.Name = "btnDecrease";
            btnDecrease.Size = new Size(75, 23);
            btnDecrease.TabIndex = 10;
            btnDecrease.Text = "button2";
            btnDecrease.UseVisualStyleBackColor = true;
            btnDecrease.Click += btnDecrease_Click;
            // 
            // lblMachineRunning
            // 
            lblMachineRunning.AutoSize = true;
            lblMachineRunning.ForeColor = Color.FromArgb(0, 192, 0);
            lblMachineRunning.Location = new Point(217, 238);
            lblMachineRunning.Name = "lblMachineRunning";
            lblMachineRunning.Size = new Size(101, 15);
            lblMachineRunning.TabIndex = 12;
            lblMachineRunning.Text = "Machine Running";
            // 
            // MachineTuneZAxisAcceleration
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblMachineRunning);
            Controls.Add(btnDecrease);
            Controls.Add(btnIncrease);
            Controls.Add(btnRunTest);
            Controls.Add(lblCurrentTestValue);
            Controls.Add(lblCurrentTestValueHeader);
            Controls.Add(lblIncrements);
            Controls.Add(numericIncrements);
            Controls.Add(lblOriginalValue);
            Controls.Add(lblOriginalValueHeader);
            Controls.Add(lblTuneAxis);
            Controls.Add(lblAxisHeader);
            Name = "MachineTuneZAxisAcceleration";
            ((System.ComponentModel.ISupportInitialize)numericIncrements).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblAxisHeader;
        private Label lblTuneAxis;
        private Label lblOriginalValueHeader;
        private Label lblOriginalValue;
        private NumericUpDown numericIncrements;
        private Label lblIncrements;
        private Label lblCurrentTestValue;
        private Label lblCurrentTestValueHeader;
        private Button btnRunTest;
        private Button btnIncrease;
        private Button btnDecrease;
        private Label lblMachineRunning;
    }
}
