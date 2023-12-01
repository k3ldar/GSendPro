namespace GSendControls.Forms
{
    partial class FrmConfigureServer
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
            lvServers = new ListViewEx();
            columnHeaderServer = new System.Windows.Forms.ColumnHeader();
            columnHeaderPort = new System.Windows.Forms.ColumnHeader();
            columnHeaderProtocol = new System.Windows.Forms.ColumnHeader();
            columnHeaderActive = new System.Windows.Forms.ColumnHeader();
            lblServer = new System.Windows.Forms.Label();
            txtServer = new System.Windows.Forms.TextBox();
            lblPort = new System.Windows.Forms.Label();
            txtPort = new System.Windows.Forms.TextBox();
            rbHttp = new System.Windows.Forms.RadioButton();
            rbHttps = new System.Windows.Forms.RadioButton();
            btnAdd = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            btnClose = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // lvServers
            // 
            lvServers.AllowColumnReorder = true;
            lvServers.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lvServers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeaderServer, columnHeaderPort, columnHeaderProtocol, columnHeaderActive });
            lvServers.Location = new System.Drawing.Point(12, 12);
            lvServers.Name = "lvServers";
            lvServers.OwnerDraw = true;
            lvServers.SaveName = "";
            lvServers.ShowToolTip = false;
            lvServers.Size = new System.Drawing.Size(579, 160);
            lvServers.TabIndex = 0;
            lvServers.UseCompatibleStateImageBehavior = false;
            lvServers.View = System.Windows.Forms.View.Details;
            lvServers.SelectedIndexChanged += lvServers_SelectedIndexChanged;
            // 
            // columnHeaderServer
            // 
            columnHeaderServer.Width = 200;
            // 
            // columnHeaderPort
            // 
            columnHeaderPort.Width = 100;
            // 
            // columnHeaderProtocol
            // 
            columnHeaderProtocol.Width = 100;
            // 
            // columnHeaderActive
            // 
            columnHeaderActive.Width = 100;
            // 
            // lblServer
            // 
            lblServer.AutoSize = true;
            lblServer.Location = new System.Drawing.Point(12, 187);
            lblServer.Name = "lblServer";
            lblServer.Size = new System.Drawing.Size(38, 15);
            lblServer.TabIndex = 1;
            lblServer.Text = "label1";
            // 
            // txtServer
            // 
            txtServer.Location = new System.Drawing.Point(12, 205);
            txtServer.Name = "txtServer";
            txtServer.Size = new System.Drawing.Size(194, 23);
            txtServer.TabIndex = 2;
            txtServer.TextChanged += ControlValueChanged;
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Location = new System.Drawing.Point(218, 187);
            lblPort.Name = "lblPort";
            lblPort.Size = new System.Drawing.Size(38, 15);
            lblPort.TabIndex = 3;
            lblPort.Text = "label2";
            // 
            // txtPort
            // 
            txtPort.Location = new System.Drawing.Point(218, 205);
            txtPort.Name = "txtPort";
            txtPort.Size = new System.Drawing.Size(73, 23);
            txtPort.TabIndex = 4;
            txtPort.TextChanged += ControlValueChanged;
            // 
            // rbHttp
            // 
            rbHttp.AutoSize = true;
            rbHttp.Checked = true;
            rbHttp.Location = new System.Drawing.Point(306, 206);
            rbHttp.Name = "rbHttp";
            rbHttp.Size = new System.Drawing.Size(49, 19);
            rbHttp.TabIndex = 5;
            rbHttp.TabStop = true;
            rbHttp.Text = "Http";
            rbHttp.UseVisualStyleBackColor = true;
            rbHttp.CheckedChanged += ControlValueChanged;
            // 
            // rbHttps
            // 
            rbHttps.AutoSize = true;
            rbHttps.Location = new System.Drawing.Point(361, 206);
            rbHttps.Name = "rbHttps";
            rbHttps.Size = new System.Drawing.Size(54, 19);
            rbHttps.TabIndex = 6;
            rbHttps.TabStop = true;
            rbHttps.Text = "Https";
            rbHttps.UseVisualStyleBackColor = true;
            rbHttps.CheckedChanged += ControlValueChanged;
            // 
            // btnAdd
            // 
            btnAdd.Location = new System.Drawing.Point(434, 204);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(75, 23);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "button1";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new System.Drawing.Point(515, 204);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(75, 23);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "button2";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnClose.Location = new System.Drawing.Point(515, 248);
            btnClose.Name = "btnClose";
            btnClose.Size = new System.Drawing.Size(75, 23);
            btnClose.TabIndex = 9;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // FrmConfigureServer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(603, 283);
            Controls.Add(btnClose);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(rbHttps);
            Controls.Add(rbHttp);
            Controls.Add(txtPort);
            Controls.Add(lblPort);
            Controls.Add(txtServer);
            Controls.Add(lblServer);
            Controls.Add(lvServers);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmConfigureServer";
            Text = "FrmConfigureServer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListViewEx lvServers;
        private System.Windows.Forms.ColumnHeader columnHeaderServer;
        private System.Windows.Forms.ColumnHeader columnHeaderPort;
        private System.Windows.Forms.ColumnHeader columnHeaderActive;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.RadioButton rbHttp;
        private System.Windows.Forms.RadioButton rbHttps;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ColumnHeader columnHeaderProtocol;
    }
}