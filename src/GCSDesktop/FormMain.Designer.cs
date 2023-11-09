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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            listViewMachines = new GSendControls.ListViewEx();
            columnHeaderName = new ColumnHeader();
            columnHeaderComPort = new ColumnHeader();
            columnHeaderMachineType = new ColumnHeader();
            columnHeaderConnected = new ColumnHeader();
            columnHeaderStatus = new ColumnHeader();
            columnHeaderCpu = new ColumnHeader();
            imageListLarge = new ImageList(components);
            imageListSmall = new ImageList(components);
            statusStrip1 = new StatusStrip();
            toolStripStatusConnected = new ToolStripStatusLabel();
            toolStripStatusCpu = new ToolStripStatusLabel();
            menuStripMain = new MenuStrip();
            machineToolStripMenuItem = new ToolStripMenuItem();
            refreshToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            addMachineToolStripMenuItem = new ToolStripMenuItem();
            removeMachineToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            closeToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            mnuViewLargeIcons = new ToolStripMenuItem();
            mnuViewDetails = new ToolStripMenuItem();
            subprogramsToolStripMenuItem = new ToolStripMenuItem();
            viewSubProgramToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            toolStripMain = new ToolStrip();
            toolStripButtonGetMachines = new ToolStripButton();
            toolStripButtonAddMachine = new ToolStripButton();
            toolStripButtonRemoeMachine = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripButtonPauseAll = new ToolStripButton();
            toolStripButtonResumeAll = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripButtonConnect = new ToolStripButton();
            toolStripButtonDisconnect = new ToolStripButton();
            toolStripButtonClearAlarm = new ToolStripButton();
            toolStripButtonResume = new ToolStripButton();
            toolStripButtonHome = new ToolStripButton();
            timerUpdateStatus = new Timer(components);
            statusStrip1.SuspendLayout();
            menuStripMain.SuspendLayout();
            toolStripMain.SuspendLayout();
            SuspendLayout();
            // 
            // listViewMachines
            // 
            listViewMachines.Activation = ItemActivation.OneClick;
            listViewMachines.AllowColumnReorder = true;
            listViewMachines.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewMachines.Columns.AddRange(new ColumnHeader[] { columnHeaderName, columnHeaderComPort, columnHeaderMachineType, columnHeaderConnected, columnHeaderStatus, columnHeaderCpu });
            listViewMachines.FullRowSelect = true;
            listViewMachines.HideSelection = true;
            listViewMachines.LargeImageList = imageListLarge;
            listViewMachines.Location = new System.Drawing.Point(12, 84);
            listViewMachines.MultiSelect = false;
            listViewMachines.Name = "listViewMachines";
            listViewMachines.OwnerDraw = true;
            listViewMachines.SaveName = "";
            listViewMachines.ShowToolTip = false;
            listViewMachines.Size = new System.Drawing.Size(681, 246);
            listViewMachines.SmallImageList = imageListSmall;
            listViewMachines.Sorting = SortOrder.Ascending;
            listViewMachines.TabIndex = 2;
            listViewMachines.UseCompatibleStateImageBehavior = false;
            listViewMachines.View = View.Details;
            listViewMachines.SelectedIndexChanged += listViewMachines_SelectedIndexChanged;
            listViewMachines.DoubleClick += listViewMachines_DoubleClick;
            // 
            // columnHeaderName
            // 
            columnHeaderName.Text = "Name";
            columnHeaderName.Width = 200;
            // 
            // columnHeaderComPort
            // 
            columnHeaderComPort.Text = "Port";
            // 
            // columnHeaderMachineType
            // 
            columnHeaderMachineType.Text = "Type";
            columnHeaderMachineType.Width = 90;
            // 
            // columnHeaderConnected
            // 
            columnHeaderConnected.Text = "Connected";
            columnHeaderConnected.Width = 100;
            // 
            // columnHeaderStatus
            // 
            columnHeaderStatus.Text = "Status";
            columnHeaderStatus.Width = 120;
            // 
            // columnHeaderCpu
            // 
            columnHeaderCpu.Text = "CPU Usage";
            columnHeaderCpu.Width = 80;
            // 
            // imageListLarge
            // 
            imageListLarge.ColorDepth = ColorDepth.Depth8Bit;
            imageListLarge.ImageStream = (ImageListStreamer)resources.GetObject("imageListLarge.ImageStream");
            imageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            imageListLarge.Images.SetKeyName(0, "3DPrinter32x32.png");
            imageListLarge.Images.SetKeyName(1, "CNC32x32.png");
            // 
            // imageListSmall
            // 
            imageListSmall.ColorDepth = ColorDepth.Depth8Bit;
            imageListSmall.ImageStream = (ImageListStreamer)resources.GetObject("imageListSmall.ImageStream");
            imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            imageListSmall.Images.SetKeyName(0, "3DPrinter16x16.png");
            imageListSmall.Images.SetKeyName(1, "CNC16x16.png");
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusConnected, toolStripStatusCpu });
            statusStrip1.Location = new System.Drawing.Point(0, 331);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(705, 24);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusConnected
            // 
            toolStripStatusConnected.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripStatusConnected.Name = "toolStripStatusConnected";
            toolStripStatusConnected.Size = new System.Drawing.Size(92, 19);
            toolStripStatusConnected.Text = "Not Connected";
            // 
            // toolStripStatusCpu
            // 
            toolStripStatusCpu.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripStatusCpu.Name = "toolStripStatusCpu";
            toolStripStatusCpu.Size = new System.Drawing.Size(122, 19);
            toolStripStatusCpu.Text = "toolStripStatusLabel1";
            // 
            // menuStripMain
            // 
            menuStripMain.Items.AddRange(new ToolStripItem[] { machineToolStripMenuItem, viewToolStripMenuItem, subprogramsToolStripMenuItem, helpToolStripMenuItem });
            menuStripMain.Location = new System.Drawing.Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new System.Drawing.Size(705, 24);
            menuStripMain.TabIndex = 4;
            menuStripMain.Text = "menuStrip1";
            // 
            // machineToolStripMenuItem
            // 
            machineToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { refreshToolStripMenuItem, toolStripMenuItem1, addMachineToolStripMenuItem, removeMachineToolStripMenuItem, toolStripMenuItem2, closeToolStripMenuItem });
            machineToolStripMenuItem.Name = "machineToolStripMenuItem";
            machineToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            machineToolStripMenuItem.Text = "&Machine";
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += toolStripButtonGetMachines_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(163, 6);
            // 
            // addMachineToolStripMenuItem
            // 
            addMachineToolStripMenuItem.Name = "addMachineToolStripMenuItem";
            addMachineToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            addMachineToolStripMenuItem.Text = "Add Machine";
            addMachineToolStripMenuItem.Click += toolStripButtonAddMachine_Click;
            // 
            // removeMachineToolStripMenuItem
            // 
            removeMachineToolStripMenuItem.Name = "removeMachineToolStripMenuItem";
            removeMachineToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            removeMachineToolStripMenuItem.Text = "Remove Machine";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(163, 6);
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            closeToolStripMenuItem.Text = "Close";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuViewLargeIcons, mnuViewDetails });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            viewToolStripMenuItem.Text = "&View";
            // 
            // mnuViewLargeIcons
            // 
            mnuViewLargeIcons.Name = "mnuViewLargeIcons";
            mnuViewLargeIcons.Size = new System.Drawing.Size(134, 22);
            mnuViewLargeIcons.Text = "&Large Icons";
            mnuViewLargeIcons.Click += mnuViewLargeIcons_Click;
            // 
            // mnuViewDetails
            // 
            mnuViewDetails.Name = "mnuViewDetails";
            mnuViewDetails.Size = new System.Drawing.Size(134, 22);
            mnuViewDetails.Text = "&Details";
            mnuViewDetails.Click += mnuViewSmallIcons_Click;
            // 
            // subprogramsToolStripMenuItem
            // 
            subprogramsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { viewSubProgramToolStripMenuItem });
            subprogramsToolStripMenuItem.Name = "subprogramsToolStripMenuItem";
            subprogramsToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            subprogramsToolStripMenuItem.Text = "Subprograms";
            // 
            // viewSubProgramToolStripMenuItem
            // 
            viewSubProgramToolStripMenuItem.Name = "viewSubProgramToolStripMenuItem";
            viewSubProgramToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            viewSubProgramToolStripMenuItem.Text = "View";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // toolStripMain
            // 
            toolStripMain.ImageScalingSize = new System.Drawing.Size(50, 50);
            toolStripMain.Items.AddRange(new ToolStripItem[] { toolStripButtonGetMachines, toolStripButtonAddMachine, toolStripButtonRemoeMachine, toolStripSeparator1, toolStripButtonPauseAll, toolStripButtonResumeAll, toolStripSeparator2, toolStripButtonConnect, toolStripButtonDisconnect, toolStripButtonClearAlarm, toolStripButtonResume, toolStripButtonHome });
            toolStripMain.Location = new System.Drawing.Point(0, 24);
            toolStripMain.Name = "toolStripMain";
            toolStripMain.Size = new System.Drawing.Size(705, 57);
            toolStripMain.TabIndex = 5;
            toolStripMain.Text = "toolStrip1";
            // 
            // toolStripButtonGetMachines
            // 
            toolStripButtonGetMachines.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonGetMachines.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonGetMachines.Image");
            toolStripButtonGetMachines.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonGetMachines.Name = "toolStripButtonGetMachines";
            toolStripButtonGetMachines.Size = new System.Drawing.Size(54, 54);
            toolStripButtonGetMachines.Click += toolStripButtonGetMachines_Click;
            // 
            // toolStripButtonAddMachine
            // 
            toolStripButtonAddMachine.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonAddMachine.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonAddMachine.Image");
            toolStripButtonAddMachine.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonAddMachine.Name = "toolStripButtonAddMachine";
            toolStripButtonAddMachine.Size = new System.Drawing.Size(54, 54);
            toolStripButtonAddMachine.Text = "Add";
            toolStripButtonAddMachine.Click += toolStripButtonAddMachine_Click;
            // 
            // toolStripButtonRemoeMachine
            // 
            toolStripButtonRemoeMachine.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonRemoeMachine.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonRemoeMachine.Image");
            toolStripButtonRemoeMachine.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonRemoeMachine.Name = "toolStripButtonRemoeMachine";
            toolStripButtonRemoeMachine.Size = new System.Drawing.Size(54, 54);
            toolStripButtonRemoeMachine.Text = "toolStripButtonRemoeMachine";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 57);
            toolStripSeparator1.Visible = false;
            // 
            // toolStripButtonPauseAll
            // 
            toolStripButtonPauseAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonPauseAll.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonPauseAll.Image");
            toolStripButtonPauseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonPauseAll.Name = "toolStripButtonPauseAll";
            toolStripButtonPauseAll.Size = new System.Drawing.Size(54, 54);
            toolStripButtonPauseAll.Text = "toolStripButton1";
            toolStripButtonPauseAll.Visible = false;
            toolStripButtonPauseAll.Click += toolStripButtonPauseAll_Click;
            // 
            // toolStripButtonResumeAll
            // 
            toolStripButtonResumeAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonResumeAll.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonResumeAll.Image");
            toolStripButtonResumeAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonResumeAll.Name = "toolStripButtonResumeAll";
            toolStripButtonResumeAll.Size = new System.Drawing.Size(54, 54);
            toolStripButtonResumeAll.Text = "toolStripButton1";
            toolStripButtonResumeAll.Visible = false;
            toolStripButtonResumeAll.Click += toolStripButtonResumeAll_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 57);
            toolStripSeparator2.Visible = false;
            // 
            // toolStripButtonConnect
            // 
            toolStripButtonConnect.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonConnect.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonConnect.Image");
            toolStripButtonConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonConnect.Name = "toolStripButtonConnect";
            toolStripButtonConnect.Size = new System.Drawing.Size(54, 54);
            toolStripButtonConnect.Text = "toolStripButton1";
            toolStripButtonConnect.Visible = false;
            toolStripButtonConnect.Click += toolStripButtonConnect_Click;
            // 
            // toolStripButtonDisconnect
            // 
            toolStripButtonDisconnect.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonDisconnect.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonDisconnect.Image");
            toolStripButtonDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonDisconnect.Name = "toolStripButtonDisconnect";
            toolStripButtonDisconnect.Size = new System.Drawing.Size(54, 54);
            toolStripButtonDisconnect.Text = "toolStripButton1";
            toolStripButtonDisconnect.Visible = false;
            toolStripButtonDisconnect.Click += toolStripButtonConnect_Click;
            // 
            // toolStripButtonClearAlarm
            // 
            toolStripButtonClearAlarm.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonClearAlarm.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonClearAlarm.Image");
            toolStripButtonClearAlarm.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonClearAlarm.Name = "toolStripButtonClearAlarm";
            toolStripButtonClearAlarm.Size = new System.Drawing.Size(54, 54);
            toolStripButtonClearAlarm.Text = "toolStripButton1";
            toolStripButtonClearAlarm.Visible = false;
            toolStripButtonClearAlarm.Click += toolStripButtonClearAlarm_Click;
            // 
            // toolStripButtonResume
            // 
            toolStripButtonResume.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonResume.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonResume.Image");
            toolStripButtonResume.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonResume.Name = "toolStripButtonResume";
            toolStripButtonResume.Size = new System.Drawing.Size(54, 54);
            toolStripButtonResume.Text = "toolStripButton1";
            toolStripButtonResume.Visible = false;
            toolStripButtonResume.Click += toolStripButtonResume_Click;
            // 
            // toolStripButtonHome
            // 
            toolStripButtonHome.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonHome.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonHome.Image");
            toolStripButtonHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonHome.Name = "toolStripButtonHome";
            toolStripButtonHome.Size = new System.Drawing.Size(54, 54);
            toolStripButtonHome.Text = "toolStripButton1";
            toolStripButtonHome.Visible = false;
            toolStripButtonHome.Click += toolStripButtonHome_Click;
            // 
            // timerUpdateStatus
            // 
            timerUpdateStatus.Tick += timerUpdateStatus_Tick;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(705, 355);
            Controls.Add(toolStripMain);
            Controls.Add(statusStrip1);
            Controls.Add(menuStripMain);
            Controls.Add(listViewMachines);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStripMain;
            Name = "FormMain";
            Text = "GSend";
            Shown += FormMain_Shown;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            toolStripMain.ResumeLayout(false);
            toolStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusConnected;
        private MenuStrip menuStripMain;
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
        private ToolStripMenuItem subprogramsToolStripMenuItem;
        private ToolStripMenuItem viewSubProgramToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private GSendControls.ListViewEx listViewMachines;
    }
}