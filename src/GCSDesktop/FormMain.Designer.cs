using System.Windows.Forms;

namespace GSendDesktop
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderComPort = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMachineType = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderStatus = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderXPos = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderYPos = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderZPos = new System.Windows.Forms.ColumnHeader();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonGetMachines = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddMachine = new System.Windows.Forms.ToolStripButton();
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderComPort,
            this.columnHeaderMachineType,
            this.columnHeaderStatus,
            this.columnHeaderXPos,
            this.columnHeaderYPos,
            this.columnHeaderZPos});
            this.listView1.FullRowSelect = true;
            this.listView1.LargeImageList = this.imageListLarge;
            this.listView1.Location = new System.Drawing.Point(12, 66);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(676, 293);
            this.listView1.SmallImageList = this.imageListSmall;
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 200;
            // 
            // columnHeaderComPort
            // 
            this.columnHeaderComPort.Text = "Port";
            // 
            // columnHeaderMachineType
            // 
            this.columnHeaderMachineType.Text = "Type";
            this.columnHeaderMachineType.Width = 90;
            // 
            // columnHeaderStatus
            // 
            this.columnHeaderStatus.Text = "Status";
            this.columnHeaderStatus.Width = 80;
            // 
            // columnHeaderXPos
            // 
            this.columnHeaderXPos.Text = "X";
            this.columnHeaderXPos.Width = 50;
            // 
            // columnHeaderYPos
            // 
            this.columnHeaderYPos.Text = "Y";
            this.columnHeaderYPos.Width = 50;
            // 
            // columnHeaderZPos
            // 
            this.columnHeaderZPos.Text = "Z";
            this.columnHeaderZPos.Width = 50;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 362);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(700, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(88, 17);
            this.toolStripStatusLabel1.Text = "Not Connected";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(700, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonGetMachines,
            this.toolStripButtonAddMachine});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(700, 39);
            this.toolStripMain.TabIndex = 5;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripButtonGetMachines
            // 
            this.toolStripButtonGetMachines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGetMachines.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGetMachines.Image")));
            this.toolStripButtonGetMachines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGetMachines.Name = "toolStripButtonGetMachines";
            this.toolStripButtonGetMachines.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonGetMachines.Text = "toolStripButton1";
            this.toolStripButtonGetMachines.Click += new System.EventHandler(this.toolStripButtonGetMachines_Click);
            // 
            // toolStripButtonAddMachine
            // 
            this.toolStripButtonAddMachine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddMachine.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddMachine.Image")));
            this.toolStripButtonAddMachine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddMachine.Name = "toolStripButtonAddMachine";
            this.toolStripButtonAddMachine.Size = new System.Drawing.Size(36, 36);
            this.toolStripButtonAddMachine.Text = "Add";
            this.toolStripButtonAddMachine.Click += new System.EventHandler(this.toolStripButtonAddMachine_Click);
            // 
            // imageListSmall
            // 
            this.imageListSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSmall.Images.SetKeyName(0, "3DPrinter16x16.png");
            this.imageListSmall.Images.SetKeyName(1, "CNC16x16.png");
            // 
            // imageListLarge
            // 
            this.imageListLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLarge.ImageStream")));
            this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListLarge.Images.SetKeyName(0, "3DPrinter32x32.png");
            this.imageListLarge.Images.SetKeyName(1, "CNC32x32.png");
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 384);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.listView1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ListView listView1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStrip toolStripMain;
        private ToolStripButton toolStripButtonGetMachines;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderComPort;
        private ColumnHeader columnHeaderMachineType;
        private ColumnHeader columnHeaderStatus;
        private ColumnHeader columnHeaderXPos;
        private ColumnHeader columnHeaderYPos;
        private ColumnHeader columnHeaderZPos;
        private ToolStripButton toolStripButtonAddMachine;
        private ImageList imageListSmall;
        private ImageList imageListLarge;
    }
}