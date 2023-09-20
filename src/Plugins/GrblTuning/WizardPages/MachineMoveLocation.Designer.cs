namespace GrblTuningWizard
{
    partial class MachineMoveLocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MachineMoveLocation));
            lblHeader = new Label();
            jogControl1 = new GSendControls.JogControl();
            lblAuto = new Label();
            btnMoveAuto = new Button();
            SuspendLayout();
            // 
            // lblHeader
            // 
            lblHeader.Location = new Point(3, 0);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(560, 83);
            lblHeader.TabIndex = 0;
            lblHeader.Text = resources.GetString("lblHeader.Text");
            // 
            // jogControl1
            // 
            jogControl1.FeedMaximum = 50;
            jogControl1.FeedMinimum = 0;
            jogControl1.FeedRate = 0;
            jogControl1.FeedRateDisplay = GSendShared.FeedRateDisplayUnits.MmPerMinute;
            jogControl1.Location = new Point(3, 86);
            jogControl1.Name = "jogControl1";
            jogControl1.Size = new Size(439, 190);
            jogControl1.StepValue = 0;
            jogControl1.TabIndex = 1;
            jogControl1.OnJogStart += JogControl1_OnJogStart;
            jogControl1.OnJogStop += JogControl1_OnJogStop;
            // 
            // lblAuto
            // 
            lblAuto.AutoSize = true;
            lblAuto.Location = new Point(3, 296);
            lblAuto.Name = "lblAuto";
            lblAuto.Size = new Size(385, 15);
            lblAuto.TabIndex = 2;
            lblAuto.Text = "You can manually jog you machine into position or move automatically";
            // 
            // btnMoveAuto
            // 
            btnMoveAuto.Location = new Point(3, 314);
            btnMoveAuto.Name = "btnMoveAuto";
            btnMoveAuto.Size = new Size(183, 23);
            btnMoveAuto.TabIndex = 3;
            btnMoveAuto.Text = "Move Automatically";
            btnMoveAuto.UseVisualStyleBackColor = true;
            btnMoveAuto.Click += btnMoveAuto_Click;
            // 
            // MachineMoveLocation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnMoveAuto);
            Controls.Add(lblAuto);
            Controls.Add(jogControl1);
            Controls.Add(lblHeader);
            Name = "MachineMoveLocation";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHeader;
        private GSendControls.JogControl jogControl1;
        private Label lblAuto;
        private Button btnMoveAuto;
    }
}
