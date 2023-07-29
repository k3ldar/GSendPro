namespace GSendControls
{
    partial class ShortcutEditor
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
            this.lvShortcuts = new GSendControls.ListViewEx();
            this.columnHeaderGroupName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderShortcutName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKeyCombo = new System.Windows.Forms.ColumnHeader();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblShortcut = new System.Windows.Forms.Label();
            this.txtKeyCombination = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblInUse = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lvShortcuts
            // 
            this.lvShortcuts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvShortcuts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderGroupName,
            this.columnHeaderShortcutName,
            this.columnHeaderKeyCombo});
            this.lvShortcuts.FullRowSelect = true;
            this.lvShortcuts.Location = new System.Drawing.Point(12, 12);
            this.lvShortcuts.MultiSelect = false;
            this.lvShortcuts.Name = "lvShortcuts";
            this.lvShortcuts.Size = new System.Drawing.Size(648, 297);
            this.lvShortcuts.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvShortcuts.TabIndex = 0;
            this.lvShortcuts.UseCompatibleStateImageBehavior = false;
            this.lvShortcuts.View = System.Windows.Forms.View.Details;
            this.lvShortcuts.SelectedIndexChanged += new System.EventHandler(this.lvShortcuts_SelectedIndexChanged);
            // 
            // columnHeaderGroupName
            // 
            this.columnHeaderGroupName.Width = 150;
            // 
            // columnHeaderShortcutName
            // 
            this.columnHeaderShortcutName.Width = 250;
            // 
            // columnHeaderKeyCombo
            // 
            this.columnHeaderKeyCombo.Width = 200;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(585, 367);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "button1";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblShortcut
            // 
            this.lblShortcut.AutoSize = true;
            this.lblShortcut.Location = new System.Drawing.Point(14, 321);
            this.lblShortcut.Name = "lblShortcut";
            this.lblShortcut.Size = new System.Drawing.Size(38, 15);
            this.lblShortcut.TabIndex = 2;
            this.lblShortcut.Text = "label1";
            // 
            // txtKeyCombination
            // 
            this.txtKeyCombination.Location = new System.Drawing.Point(14, 339);
            this.txtKeyCombination.Name = "txtKeyCombination";
            this.txtKeyCombination.ReadOnly = true;
            this.txtKeyCombination.Size = new System.Drawing.Size(262, 23);
            this.txtKeyCombination.TabIndex = 3;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(282, 338);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "button1";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(363, 338);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "button2";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblInUse
            // 
            this.lblInUse.AutoSize = true;
            this.lblInUse.Location = new System.Drawing.Point(14, 365);
            this.lblInUse.Name = "lblInUse";
            this.lblInUse.Size = new System.Drawing.Size(38, 15);
            this.lblInUse.TabIndex = 6;
            this.lblInUse.Text = "label1";
            // 
            // ShortcutEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 402);
            this.Controls.Add(this.lblInUse);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtKeyCombination);
            this.Controls.Add(this.lblShortcut);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lvShortcuts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShortcutEditor";
            this.ShowIcon = false;
            this.Text = "ShortcutEditor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShortcutEditor_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ShortcutEditor_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GSendControls.ListViewEx lvShortcuts;
        private System.Windows.Forms.ColumnHeader columnHeaderGroupName;
        private System.Windows.Forms.ColumnHeader columnHeaderShortcutName;
        private System.Windows.Forms.ColumnHeader columnHeaderKeyCombo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblShortcut;
        private System.Windows.Forms.TextBox txtKeyCombination;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblInUse;
    }
}