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
            this.listViewMachines = new GSendControls.ListViewEx();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderComPort = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMachineType = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderConnected = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderStatus = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCpu = new System.Windows.Forms.ColumnHeader();
            this.imageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusConnected = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusCpu = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.machineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.addMachineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeMachineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewLargeIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.subProgramsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewSubProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonGetMachines = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddMachine = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRemoeMachine = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPauseAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonResumeAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDisconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClearAlarm = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonResume = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonHome = new System.Windows.Forms.ToolStripButton();
            this.timerUpdateStatus = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewMachines
            // 
            this.listViewMachines.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewMachines.AllowColumnReorder = true;
            this.listViewMachines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMachines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderComPort,
            this.columnHeaderMachineType,
            this.columnHeaderConnected,
            this.columnHeaderStatus,
            this.columnHeaderCpu});
            this.listViewMachines.FullRowSelect = true;
            this.listViewMachines.HideSelection = true;
            this.listViewMachines.LargeImageList = this.imageListLarge;
            this.listViewMachines.Location = new System.Drawing.Point(12, 84);
            this.listViewMachines.MultiSelect = false;
            this.listViewMachines.Name = "listViewMachines";
            this.listViewMachines.OwnerDraw = true;
            this.listViewMachines.SaveName = "";
            this.listViewMachines.ShowToolTip = false;
            this.listViewMachines.Size = new System.Drawing.Size(681, 246);
            this.listViewMachines.SmallImageList = this.imageListSmall;
            this.listViewMachines.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewMachines.TabIndex = 2;
            this.listViewMachines.UseCompatibleStateImageBehavior = false;
            this.listViewMachines.View = System.Windows.Forms.View.Details;
            this.listViewMachines.SelectedIndexChanged += new System.EventHandler(this.listViewMachines_SelectedIndexChanged);
            this.listViewMachines.DoubleClick += new System.EventHandler(this.listViewMachines_DoubleClick);
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
            // columnHeaderConnected
            // 
            this.columnHeaderConnected.Text = "Connected";
            this.columnHeaderConnected.Width = 100;
            // 
            // columnHeaderStatus
            // 
            this.columnHeaderStatus.Text = "Status";
            this.columnHeaderStatus.Width = 120;
            // 
            // columnHeaderCpu
            // 
            this.columnHeaderCpu.Text = "CPU Usage";
            this.columnHeaderCpu.Width = 80;
            // 
            // imageListLarge
            // 
            this.imageListLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLarge.ImageStream")));
            this.imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListLarge.Images.SetKeyName(0, "3DPrinter32x32.png");
            this.imageListLarge.Images.SetKeyName(1, "CNC32x32.png");
            // 
            // imageListSmall
            // 
            this.imageListSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSmall.Images.SetKeyName(0, "3DPrinter16x16.png");
            this.imageListSmall.Images.SetKeyName(1, "CNC16x16.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusConnected,
            this.toolStripStatusCpu});
            this.statusStrip1.Location = new System.Drawing.Point(0, 331);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(705, 24);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusConnected
            // 
            this.toolStripStatusConnected.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusConnected.Name = "toolStripStatusConnected";
            this.toolStripStatusConnected.Size = new System.Drawing.Size(92, 19);
            this.toolStripStatusConnected.Text = "Not Connected";
            // 
            // toolStripStatusCpu
            // 
            this.toolStripStatusCpu.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusCpu.Name = "toolStripStatusCpu";
            this.toolStripStatusCpu.Size = new System.Drawing.Size(122, 19);
            this.toolStripStatusCpu.Text = "toolStripStatusLabel1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.machineToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.subProgramsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(705, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // machineToolStripMenuItem
            // 
            this.machineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.toolStripMenuItem1,
            this.addMachineToolStripMenuItem,
            this.removeMachineToolStripMenuItem,
            this.toolStripMenuItem2,
            this.closeToolStripMenuItem});
            this.machineToolStripMenuItem.Name = "machineToolStripMenuItem";
            this.machineToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.machineToolStripMenuItem.Text = "&Machine";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonGetMachines_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
            // 
            // addMachineToolStripMenuItem
            // 
            this.addMachineToolStripMenuItem.Name = "addMachineToolStripMenuItem";
            this.addMachineToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.addMachineToolStripMenuItem.Text = "Add Machine";
            this.addMachineToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonAddMachine_Click);
            // 
            // removeMachineToolStripMenuItem
            // 
            this.removeMachineToolStripMenuItem.Name = "removeMachineToolStripMenuItem";
            this.removeMachineToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.removeMachineToolStripMenuItem.Text = "Remove Machine";
            this.removeMachineToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonRemoveMachine_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(163, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewLargeIcons,
            this.mnuViewDetails});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // mnuViewLargeIcons
            // 
            this.mnuViewLargeIcons.Name = "mnuViewLargeIcons";
            this.mnuViewLargeIcons.Size = new System.Drawing.Size(134, 22);
            this.mnuViewLargeIcons.Text = "&Large Icons";
            this.mnuViewLargeIcons.Click += new System.EventHandler(this.mnuViewLargeIcons_Click);
            // 
            // mnuViewDetails
            // 
            this.mnuViewDetails.Name = "mnuViewDetails";
            this.mnuViewDetails.Size = new System.Drawing.Size(134, 22);
            this.mnuViewDetails.Text = "&Details";
            this.mnuViewDetails.Click += new System.EventHandler(this.mnuViewSmallIcons_Click);
            // 
            // subProgramsToolStripMenuItem
            // 
            this.subProgramsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewSubProgramToolStripMenuItem});
            this.subProgramsToolStripMenuItem.Name = "subProgramsToolStripMenuItem";
            this.subProgramsToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.subProgramsToolStripMenuItem.Text = "Sub Programs";
            // 
            // viewSubProgramToolStripMenuItem
            // 
            this.viewSubProgramToolStripMenuItem.Name = "viewSubProgramToolStripMenuItem";
            this.viewSubProgramToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.viewSubProgramToolStripMenuItem.Text = "View";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem,
            this.toolStripMenuItem3,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(124, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonGetMachines,
            this.toolStripButtonAddMachine,
            this.toolStripButtonRemoeMachine,
            this.toolStripSeparator1,
            this.toolStripButtonPauseAll,
            this.toolStripButtonResumeAll,
            this.toolStripSeparator2,
            this.toolStripButtonConnect,
            this.toolStripButtonDisconnect,
            this.toolStripButtonClearAlarm,
            this.toolStripButtonResume,
            this.toolStripButtonHome});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(705, 57);
            this.toolStripMain.TabIndex = 5;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripButtonGetMachines
            // 
            this.toolStripButtonGetMachines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGetMachines.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGetMachines.Image")));
            this.toolStripButtonGetMachines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGetMachines.Name = "toolStripButtonGetMachines";
            this.toolStripButtonGetMachines.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonGetMachines.Click += new System.EventHandler(this.toolStripButtonGetMachines_Click);
            // 
            // toolStripButtonAddMachine
            // 
            this.toolStripButtonAddMachine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddMachine.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddMachine.Image")));
            this.toolStripButtonAddMachine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddMachine.Name = "toolStripButtonAddMachine";
            this.toolStripButtonAddMachine.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonAddMachine.Text = "Add";
            this.toolStripButtonAddMachine.Click += new System.EventHandler(this.toolStripButtonAddMachine_Click);
            // 
            // toolStripButtonRemoeMachine
            // 
            this.toolStripButtonRemoeMachine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRemoeMachine.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRemoeMachine.Image")));
            this.toolStripButtonRemoeMachine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRemoeMachine.Name = "toolStripButtonRemoeMachine";
            this.toolStripButtonRemoeMachine.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonRemoeMachine.Text = "toolStripButtonRemoeMachine";
            this.toolStripButtonRemoeMachine.Click += new System.EventHandler(this.toolStripButtonRemoveMachine_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 57);
            this.toolStripSeparator1.Visible = false;
            // 
            // toolStripButtonPauseAll
            // 
            this.toolStripButtonPauseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPauseAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPauseAll.Image")));
            this.toolStripButtonPauseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPauseAll.Name = "toolStripButtonPauseAll";
            this.toolStripButtonPauseAll.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonPauseAll.Text = "toolStripButton1";
            this.toolStripButtonPauseAll.Visible = false;
            this.toolStripButtonPauseAll.Click += new System.EventHandler(this.toolStripButtonPauseAll_Click);
            // 
            // toolStripButtonResumeAll
            // 
            this.toolStripButtonResumeAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonResumeAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonResumeAll.Image")));
            this.toolStripButtonResumeAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonResumeAll.Name = "toolStripButtonResumeAll";
            this.toolStripButtonResumeAll.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonResumeAll.Text = "toolStripButton1";
            this.toolStripButtonResumeAll.Visible = false;
            this.toolStripButtonResumeAll.Click += new System.EventHandler(this.toolStripButtonResumeAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 57);
            this.toolStripSeparator2.Visible = false;
            // 
            // toolStripButtonConnect
            // 
            this.toolStripButtonConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonConnect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonConnect.Image")));
            this.toolStripButtonConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonConnect.Name = "toolStripButtonConnect";
            this.toolStripButtonConnect.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonConnect.Text = "toolStripButton1";
            this.toolStripButtonConnect.Visible = false;
            this.toolStripButtonConnect.Click += new System.EventHandler(this.toolStripButtonConnect_Click);
            // 
            // toolStripButtonDisconnect
            // 
            this.toolStripButtonDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDisconnect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDisconnect.Image")));
            this.toolStripButtonDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDisconnect.Name = "toolStripButtonDisconnect";
            this.toolStripButtonDisconnect.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonDisconnect.Text = "toolStripButton1";
            this.toolStripButtonDisconnect.Visible = false;
            this.toolStripButtonDisconnect.Click += new System.EventHandler(this.toolStripButtonConnect_Click);
            // 
            // toolStripButtonClearAlarm
            // 
            this.toolStripButtonClearAlarm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonClearAlarm.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClearAlarm.Image")));
            this.toolStripButtonClearAlarm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClearAlarm.Name = "toolStripButtonClearAlarm";
            this.toolStripButtonClearAlarm.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonClearAlarm.Text = "toolStripButton1";
            this.toolStripButtonClearAlarm.Visible = false;
            this.toolStripButtonClearAlarm.Click += new System.EventHandler(this.toolStripButtonClearAlarm_Click);
            // 
            // toolStripButtonResume
            // 
            this.toolStripButtonResume.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonResume.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonResume.Image")));
            this.toolStripButtonResume.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonResume.Name = "toolStripButtonResume";
            this.toolStripButtonResume.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonResume.Text = "toolStripButton1";
            this.toolStripButtonResume.Visible = false;
            this.toolStripButtonResume.Click += new System.EventHandler(this.toolStripButtonResume_Click);
            // 
            // toolStripButtonHome
            // 
            this.toolStripButtonHome.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHome.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHome.Image")));
            this.toolStripButtonHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHome.Name = "toolStripButtonHome";
            this.toolStripButtonHome.Size = new System.Drawing.Size(54, 54);
            this.toolStripButtonHome.Text = "toolStripButton1";
            this.toolStripButtonHome.Visible = false;
            this.toolStripButtonHome.Click += new System.EventHandler(this.toolStripButtonHome_Click);
            // 
            // timerUpdateStatus
            // 
            this.timerUpdateStatus.Tick += new System.EventHandler(this.timerUpdateStatus_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 355);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.listViewMachines);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "GSend";
            this.Shown += new System.EventHandler(this.FormMain_Shown);
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
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusConnected;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem machineToolStripMenuItem;
        private ToolStrip toolStripMain;
        private ToolStripButton toolStripButtonGetMachines;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderComPort;
        private ColumnHeader columnHeaderMachineType;
        private ColumnHeader columnHeaderStatus;
        private ColumnHeader columnHeaderConnected;
        private ToolStripButton toolStripButtonAddMachine;
        private ImageList imageListSmall;
        private ImageList imageListLarge;
        private Timer timerUpdateStatus;
        private ColumnHeader columnHeaderCpu;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripStatusLabel toolStripStatusCpu;
        private ToolStripButton toolStripButtonPauseAll;
        private ToolStripButton toolStripButtonResumeAll;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButtonConnect;
        private ToolStripButton toolStripButtonDisconnect;
        private ToolStripButton toolStripButtonClearAlarm;
        private ToolStripButton toolStripButtonResume;
        private ToolStripButton toolStripButtonHome;
        private ToolStripButton toolStripButtonRemoeMachine;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem mnuViewLargeIcons;
        private ToolStripMenuItem mnuViewDetails;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem addMachineToolStripMenuItem;
        private ToolStripMenuItem removeMachineToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem subProgramsToolStripMenuItem;
        private ToolStripMenuItem viewSubProgramToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem viewHelpToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private GSendControls.ListViewEx listViewMachines;
    }
}