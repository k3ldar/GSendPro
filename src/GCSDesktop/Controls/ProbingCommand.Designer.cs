namespace GSendDesktop.Controls
{
    partial class ProbingCommand
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
            this.lblProbeThickness = new System.Windows.Forms.Label();
            this.numericProbeThickness = new System.Windows.Forms.NumericUpDown();
            this.lblTravelSpeed = new System.Windows.Forms.Label();
            this.trackBarTravelSpeed = new System.Windows.Forms.TrackBar();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtProbeCommand = new System.Windows.Forms.TextBox();
            this.lblProbeCommand = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericProbeThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTravelSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProbeThickness
            // 
            this.lblProbeThickness.AutoSize = true;
            this.lblProbeThickness.Location = new System.Drawing.Point(3, 0);
            this.lblProbeThickness.Name = "lblProbeThickness";
            this.lblProbeThickness.Size = new System.Drawing.Size(38, 15);
            this.lblProbeThickness.TabIndex = 0;
            this.lblProbeThickness.Text = "label1";
            // 
            // numericProbeThickness
            // 
            this.numericProbeThickness.DecimalPlaces = 2;
            this.numericProbeThickness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericProbeThickness.Location = new System.Drawing.Point(8, 18);
            this.numericProbeThickness.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericProbeThickness.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericProbeThickness.Name = "numericProbeThickness";
            this.numericProbeThickness.Size = new System.Drawing.Size(120, 23);
            this.numericProbeThickness.TabIndex = 1;
            this.numericProbeThickness.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblTravelSpeed
            // 
            this.lblTravelSpeed.AutoSize = true;
            this.lblTravelSpeed.Location = new System.Drawing.Point(3, 50);
            this.lblTravelSpeed.Name = "lblTravelSpeed";
            this.lblTravelSpeed.Size = new System.Drawing.Size(38, 15);
            this.lblTravelSpeed.TabIndex = 2;
            this.lblTravelSpeed.Text = "label1";
            // 
            // trackBarTravelSpeed
            // 
            this.trackBarTravelSpeed.Location = new System.Drawing.Point(8, 68);
            this.trackBarTravelSpeed.Maximum = 250;
            this.trackBarTravelSpeed.Minimum = 20;
            this.trackBarTravelSpeed.Name = "trackBarTravelSpeed";
            this.trackBarTravelSpeed.Size = new System.Drawing.Size(153, 45);
            this.trackBarTravelSpeed.TabIndex = 3;
            this.trackBarTravelSpeed.TickFrequency = 10;
            this.trackBarTravelSpeed.Value = 100;
            this.trackBarTravelSpeed.ValueChanged += new System.EventHandler(this.trackBarTravelSpeed_ValueChanged);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(142, 3);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "button1";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(142, 32);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "button2";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtProbeCommand
            // 
            this.txtProbeCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProbeCommand.Location = new System.Drawing.Point(8, 125);
            this.txtProbeCommand.Multiline = true;
            this.txtProbeCommand.Name = "txtProbeCommand";
            this.txtProbeCommand.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtProbeCommand.Size = new System.Drawing.Size(209, 44);
            this.txtProbeCommand.TabIndex = 6;
            this.txtProbeCommand.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lblProbeCommand
            // 
            this.lblProbeCommand.AutoSize = true;
            this.lblProbeCommand.Location = new System.Drawing.Point(0, 107);
            this.lblProbeCommand.Name = "lblProbeCommand";
            this.lblProbeCommand.Size = new System.Drawing.Size(38, 15);
            this.lblProbeCommand.TabIndex = 7;
            this.lblProbeCommand.Text = "label1";
            // 
            // ProbingCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblProbeCommand);
            this.Controls.Add(this.txtProbeCommand);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.trackBarTravelSpeed);
            this.Controls.Add(this.lblTravelSpeed);
            this.Controls.Add(this.numericProbeThickness);
            this.Controls.Add(this.lblProbeThickness);
            this.MinimumSize = new System.Drawing.Size(220, 172);
            this.Name = "ProbingCommand";
            this.Size = new System.Drawing.Size(220, 172);
            ((System.ComponentModel.ISupportInitialize)(this.numericProbeThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTravelSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProbeThickness;
        private System.Windows.Forms.NumericUpDown numericProbeThickness;
        private System.Windows.Forms.Label lblTravelSpeed;
        private System.Windows.Forms.TrackBar trackBarTravelSpeed;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtProbeCommand;
        private System.Windows.Forms.Label lblProbeCommand;
    }
}
