namespace Test_Harness
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.nodesListBox = new System.Windows.Forms.ListBox();
            this.nodesBox = new System.Windows.Forms.GroupBox();
            this.rescanButton = new System.Windows.Forms.Button();
            this.refreshStatusButton = new System.Windows.Forms.Button();
            this.restartNodeButton = new System.Windows.Forms.Button();
            this.startNodeButton = new System.Windows.Forms.Button();
            this.stopNodeButton = new System.Windows.Forms.Button();
            this.fulBackupNowButton = new System.Windows.Forms.Button();
            this.incrBackupNowButton = new System.Windows.Forms.Button();
            this.backupReportButton = new System.Windows.Forms.Button();
            this.viewLogsButton = new System.Windows.Forms.Button();
            this.nodesStatusBox = new System.Windows.Forms.GroupBox();
            this.statusTestDirLabel = new System.Windows.Forms.Label();
            this.statusGUIDLabel = new System.Windows.Forms.Label();
            this.statusIPAddressLabel = new System.Windows.Forms.Label();
            this.statusCurrentTaskLabel = new System.Windows.Forms.Label();
            this.statusIndexVersionLabel = new System.Windows.Forms.Label();
            this.statusRunningLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.nodeConfigBox = new System.Windows.Forms.GroupBox();
            this.backupSizeConfigLabel = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.diskCapacityConfigLabel = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.testHarnessConfigBox = new System.Windows.Forms.GroupBox();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.statusRefreshIntervalConfigLabel = new System.Windows.Forms.Label();
            this.diskUsageButton = new System.Windows.Forms.Button();
            this.pushUpdateButton = new System.Windows.Forms.Button();
            this.rescanIntervalConfigLabel = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.clearButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.freeSpaceConfigLabel = new System.Windows.Forms.Label();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.fileCountConfigLabel = new System.Windows.Forms.Label();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.nodesBox.SuspendLayout();
            this.nodesStatusBox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.nodeConfigBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.testHarnessConfigBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            this.SuspendLayout();
            // 
            // nodesListBox
            // 
            this.nodesListBox.FormattingEnabled = true;
            this.nodesListBox.Location = new System.Drawing.Point(6, 19);
            this.nodesListBox.Name = "nodesListBox";
            this.nodesListBox.Size = new System.Drawing.Size(154, 95);
            this.nodesListBox.TabIndex = 0;
            this.nodesListBox.SelectedIndexChanged += new System.EventHandler(this.nodesListBox_SelectedIndexChanged);
            // 
            // nodesBox
            // 
            this.nodesBox.Controls.Add(this.clearButton);
            this.nodesBox.Controls.Add(this.rescanButton);
            this.nodesBox.Controls.Add(this.nodesListBox);
            this.nodesBox.Location = new System.Drawing.Point(12, 12);
            this.nodesBox.Name = "nodesBox";
            this.nodesBox.Size = new System.Drawing.Size(166, 150);
            this.nodesBox.TabIndex = 2;
            this.nodesBox.TabStop = false;
            this.nodesBox.Text = "Nodes";
            this.nodesBox.Enter += new System.EventHandler(this.nodesBox_Enter);
            // 
            // rescanButton
            // 
            this.rescanButton.Location = new System.Drawing.Point(7, 121);
            this.rescanButton.Name = "rescanButton";
            this.rescanButton.Size = new System.Drawing.Size(75, 23);
            this.rescanButton.TabIndex = 1;
            this.rescanButton.Text = "Rescan";
            this.rescanButton.UseVisualStyleBackColor = true;
            // 
            // refreshStatusButton
            // 
            this.refreshStatusButton.Location = new System.Drawing.Point(427, 21);
            this.refreshStatusButton.Name = "refreshStatusButton";
            this.refreshStatusButton.Size = new System.Drawing.Size(106, 23);
            this.refreshStatusButton.TabIndex = 4;
            this.refreshStatusButton.Text = "Refresh Status";
            this.refreshStatusButton.UseVisualStyleBackColor = true;
            // 
            // restartNodeButton
            // 
            this.restartNodeButton.Location = new System.Drawing.Point(539, 21);
            this.restartNodeButton.Name = "restartNodeButton";
            this.restartNodeButton.Size = new System.Drawing.Size(106, 23);
            this.restartNodeButton.TabIndex = 4;
            this.restartNodeButton.Text = "Restart Node";
            this.restartNodeButton.UseVisualStyleBackColor = true;
            // 
            // startNodeButton
            // 
            this.startNodeButton.Location = new System.Drawing.Point(427, 50);
            this.startNodeButton.Name = "startNodeButton";
            this.startNodeButton.Size = new System.Drawing.Size(106, 23);
            this.startNodeButton.TabIndex = 4;
            this.startNodeButton.Text = "Start Node";
            this.startNodeButton.UseVisualStyleBackColor = true;
            // 
            // stopNodeButton
            // 
            this.stopNodeButton.Location = new System.Drawing.Point(539, 50);
            this.stopNodeButton.Name = "stopNodeButton";
            this.stopNodeButton.Size = new System.Drawing.Size(106, 23);
            this.stopNodeButton.TabIndex = 4;
            this.stopNodeButton.Text = "Stop Node";
            this.stopNodeButton.UseVisualStyleBackColor = true;
            // 
            // fulBackupNowButton
            // 
            this.fulBackupNowButton.Location = new System.Drawing.Point(427, 79);
            this.fulBackupNowButton.Name = "fulBackupNowButton";
            this.fulBackupNowButton.Size = new System.Drawing.Size(106, 23);
            this.fulBackupNowButton.TabIndex = 4;
            this.fulBackupNowButton.Text = "Full Backup Now";
            this.fulBackupNowButton.UseVisualStyleBackColor = true;
            // 
            // incrBackupNowButton
            // 
            this.incrBackupNowButton.Location = new System.Drawing.Point(539, 79);
            this.incrBackupNowButton.Name = "incrBackupNowButton";
            this.incrBackupNowButton.Size = new System.Drawing.Size(106, 23);
            this.incrBackupNowButton.TabIndex = 4;
            this.incrBackupNowButton.Text = "Incr. Backup Now";
            this.incrBackupNowButton.UseVisualStyleBackColor = true;
            // 
            // backupReportButton
            // 
            this.backupReportButton.Location = new System.Drawing.Point(427, 108);
            this.backupReportButton.Name = "backupReportButton";
            this.backupReportButton.Size = new System.Drawing.Size(106, 23);
            this.backupReportButton.TabIndex = 4;
            this.backupReportButton.Text = "Backup Report...";
            this.backupReportButton.UseVisualStyleBackColor = true;
            // 
            // viewLogsButton
            // 
            this.viewLogsButton.Location = new System.Drawing.Point(539, 108);
            this.viewLogsButton.Name = "viewLogsButton";
            this.viewLogsButton.Size = new System.Drawing.Size(106, 23);
            this.viewLogsButton.TabIndex = 4;
            this.viewLogsButton.Text = "View Logs...";
            this.viewLogsButton.UseVisualStyleBackColor = true;
            // 
            // nodesStatusBox
            // 
            this.nodesStatusBox.Controls.Add(this.statusTestDirLabel);
            this.nodesStatusBox.Controls.Add(this.statusGUIDLabel);
            this.nodesStatusBox.Controls.Add(this.statusIPAddressLabel);
            this.nodesStatusBox.Controls.Add(this.statusCurrentTaskLabel);
            this.nodesStatusBox.Controls.Add(this.statusIndexVersionLabel);
            this.nodesStatusBox.Controls.Add(this.statusRunningLabel);
            this.nodesStatusBox.Location = new System.Drawing.Point(191, 12);
            this.nodesStatusBox.Name = "nodesStatusBox";
            this.nodesStatusBox.Size = new System.Drawing.Size(229, 150);
            this.nodesStatusBox.TabIndex = 5;
            this.nodesStatusBox.TabStop = false;
            this.nodesStatusBox.Text = "Node Status";
            // 
            // statusTestDirLabel
            // 
            this.statusTestDirLabel.AutoSize = true;
            this.statusTestDirLabel.Location = new System.Drawing.Point(6, 114);
            this.statusTestDirLabel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this.statusTestDirLabel.Name = "statusTestDirLabel";
            this.statusTestDirLabel.Size = new System.Drawing.Size(76, 13);
            this.statusTestDirLabel.TabIndex = 0;
            this.statusTestDirLabel.Text = "Test Directory:";
            this.statusTestDirLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // statusGUIDLabel
            // 
            this.statusGUIDLabel.AutoSize = true;
            this.statusGUIDLabel.Location = new System.Drawing.Point(6, 95);
            this.statusGUIDLabel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this.statusGUIDLabel.Name = "statusGUIDLabel";
            this.statusGUIDLabel.Size = new System.Drawing.Size(37, 13);
            this.statusGUIDLabel.TabIndex = 0;
            this.statusGUIDLabel.Text = "GUID:";
            this.statusGUIDLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // statusIPAddressLabel
            // 
            this.statusIPAddressLabel.AutoSize = true;
            this.statusIPAddressLabel.Location = new System.Drawing.Point(6, 76);
            this.statusIPAddressLabel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this.statusIPAddressLabel.Name = "statusIPAddressLabel";
            this.statusIPAddressLabel.Size = new System.Drawing.Size(61, 13);
            this.statusIPAddressLabel.TabIndex = 0;
            this.statusIPAddressLabel.Text = "IP Address:";
            this.statusIPAddressLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // statusCurrentTaskLabel
            // 
            this.statusCurrentTaskLabel.AutoSize = true;
            this.statusCurrentTaskLabel.Location = new System.Drawing.Point(6, 57);
            this.statusCurrentTaskLabel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this.statusCurrentTaskLabel.Name = "statusCurrentTaskLabel";
            this.statusCurrentTaskLabel.Size = new System.Drawing.Size(71, 13);
            this.statusCurrentTaskLabel.TabIndex = 0;
            this.statusCurrentTaskLabel.Text = "Current Task:";
            this.statusCurrentTaskLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // statusIndexVersionLabel
            // 
            this.statusIndexVersionLabel.AutoSize = true;
            this.statusIndexVersionLabel.Location = new System.Drawing.Point(6, 38);
            this.statusIndexVersionLabel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this.statusIndexVersionLabel.Name = "statusIndexVersionLabel";
            this.statusIndexVersionLabel.Size = new System.Drawing.Size(74, 13);
            this.statusIndexVersionLabel.TabIndex = 0;
            this.statusIndexVersionLabel.Text = "Index Version:";
            // 
            // statusRunningLabel
            // 
            this.statusRunningLabel.AutoSize = true;
            this.statusRunningLabel.Location = new System.Drawing.Point(6, 19);
            this.statusRunningLabel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this.statusRunningLabel.Name = "statusRunningLabel";
            this.statusRunningLabel.Size = new System.Drawing.Size(50, 13);
            this.statusRunningLabel.TabIndex = 0;
            this.statusRunningLabel.Text = "Running:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 328);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(785, 24);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(122, 19);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // nodeConfigBox
            // 
            this.nodeConfigBox.Controls.Add(this.numericUpDown6);
            this.nodeConfigBox.Controls.Add(this.numericUpDown2);
            this.nodeConfigBox.Controls.Add(this.freeSpaceConfigLabel);
            this.nodeConfigBox.Controls.Add(this.numericUpDown7);
            this.nodeConfigBox.Controls.Add(this.numericUpDown1);
            this.nodeConfigBox.Controls.Add(this.fileCountConfigLabel);
            this.nodeConfigBox.Controls.Add(this.diskCapacityConfigLabel);
            this.nodeConfigBox.Controls.Add(this.backupSizeConfigLabel);
            this.nodeConfigBox.Location = new System.Drawing.Point(12, 168);
            this.nodeConfigBox.Name = "nodeConfigBox";
            this.nodeConfigBox.Size = new System.Drawing.Size(408, 150);
            this.nodeConfigBox.TabIndex = 5;
            this.nodeConfigBox.TabStop = false;
            this.nodeConfigBox.Text = "Node Configuration";
            this.nodeConfigBox.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // backupSizeConfigLabel
            // 
            this.backupSizeConfigLabel.AutoSize = true;
            this.backupSizeConfigLabel.Location = new System.Drawing.Point(7, 22);
            this.backupSizeConfigLabel.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this.backupSizeConfigLabel.Name = "backupSizeConfigLabel";
            this.backupSizeConfigLabel.Size = new System.Drawing.Size(95, 13);
            this.backupSizeConfigLabel.TabIndex = 0;
            this.backupSizeConfigLabel.Text = "Backup Size (MB):";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(105, 20);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown1.TabIndex = 1;
            // 
            // diskCapacityConfigLabel
            // 
            this.diskCapacityConfigLabel.AutoSize = true;
            this.diskCapacityConfigLabel.Location = new System.Drawing.Point(7, 45);
            this.diskCapacityConfigLabel.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this.diskCapacityConfigLabel.Name = "diskCapacityConfigLabel";
            this.diskCapacityConfigLabel.Size = new System.Drawing.Size(100, 13);
            this.diskCapacityConfigLabel.TabIndex = 0;
            this.diskCapacityConfigLabel.Text = "Disk Capacity (MB):";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(110, 43);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown2.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Margin = new System.Windows.Forms.Padding(4, 3, 1, 3);
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 18);
            // 
            // testHarnessConfigBox
            // 
            this.testHarnessConfigBox.Controls.Add(this.numericUpDown5);
            this.testHarnessConfigBox.Controls.Add(this.numericUpDown3);
            this.testHarnessConfigBox.Controls.Add(this.numericUpDown4);
            this.testHarnessConfigBox.Controls.Add(this.label2);
            this.testHarnessConfigBox.Controls.Add(this.rescanIntervalConfigLabel);
            this.testHarnessConfigBox.Controls.Add(this.statusRefreshIntervalConfigLabel);
            this.testHarnessConfigBox.Location = new System.Drawing.Point(427, 168);
            this.testHarnessConfigBox.Name = "testHarnessConfigBox";
            this.testHarnessConfigBox.Size = new System.Drawing.Size(218, 150);
            this.testHarnessConfigBox.TabIndex = 5;
            this.testHarnessConfigBox.TabStop = false;
            this.testHarnessConfigBox.Text = "Test Harness Configuration";
            this.testHarnessConfigBox.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(154, 19);
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown4.TabIndex = 1;
            // 
            // statusRefreshIntervalConfigLabel
            // 
            this.statusRefreshIntervalConfigLabel.AutoSize = true;
            this.statusRefreshIntervalConfigLabel.Location = new System.Drawing.Point(7, 22);
            this.statusRefreshIntervalConfigLabel.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this.statusRefreshIntervalConfigLabel.Name = "statusRefreshIntervalConfigLabel";
            this.statusRefreshIntervalConfigLabel.Size = new System.Drawing.Size(144, 13);
            this.statusRefreshIntervalConfigLabel.TabIndex = 0;
            this.statusRefreshIntervalConfigLabel.Text = "Status Refresh Interval (sec):";
            // 
            // diskUsageButton
            // 
            this.diskUsageButton.Location = new System.Drawing.Point(427, 137);
            this.diskUsageButton.Name = "diskUsageButton";
            this.diskUsageButton.Size = new System.Drawing.Size(106, 23);
            this.diskUsageButton.TabIndex = 4;
            this.diskUsageButton.Text = "Disk Usage...";
            this.diskUsageButton.UseVisualStyleBackColor = true;
            // 
            // pushUpdateButton
            // 
            this.pushUpdateButton.Location = new System.Drawing.Point(539, 137);
            this.pushUpdateButton.Name = "pushUpdateButton";
            this.pushUpdateButton.Size = new System.Drawing.Size(106, 23);
            this.pushUpdateButton.TabIndex = 4;
            this.pushUpdateButton.Text = "Push Update";
            this.pushUpdateButton.UseVisualStyleBackColor = true;
            // 
            // rescanIntervalConfigLabel
            // 
            this.rescanIntervalConfigLabel.AutoSize = true;
            this.rescanIntervalConfigLabel.Location = new System.Drawing.Point(7, 45);
            this.rescanIntervalConfigLabel.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this.rescanIntervalConfigLabel.Name = "rescanIntervalConfigLabel";
            this.rescanIntervalConfigLabel.Size = new System.Drawing.Size(111, 13);
            this.rescanIntervalConfigLabel.TabIndex = 0;
            this.rescanIntervalConfigLabel.Text = "Rescan Interval (sec):";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(154, 42);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown3.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Status Refresh Interval (sec):";
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(154, 64);
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown5.TabIndex = 1;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(85, 121);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 1;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(651, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "--unused--";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(651, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "~~not used~~";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(651, 79);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "__not in use__";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(651, 137);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(129, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "##todo: use##";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(651, 108);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(129, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Configure Schedule...";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // freeSpaceConfigLabel
            // 
            this.freeSpaceConfigLabel.AutoSize = true;
            this.freeSpaceConfigLabel.Location = new System.Drawing.Point(176, 45);
            this.freeSpaceConfigLabel.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this.freeSpaceConfigLabel.Name = "freeSpaceConfigLabel";
            this.freeSpaceConfigLabel.Size = new System.Drawing.Size(90, 13);
            this.freeSpaceConfigLabel.TabIndex = 0;
            this.freeSpaceConfigLabel.Text = "Free Space (MB):";
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(269, 43);
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown6.TabIndex = 1;
            // 
            // fileCountConfigLabel
            // 
            this.fileCountConfigLabel.AutoSize = true;
            this.fileCountConfigLabel.Location = new System.Drawing.Point(176, 22);
            this.fileCountConfigLabel.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this.fileCountConfigLabel.Name = "fileCountConfigLabel";
            this.fileCountConfigLabel.Size = new System.Drawing.Size(57, 13);
            this.fileCountConfigLabel.TabIndex = 0;
            this.fileCountConfigLabel.Text = "File Count:";
            this.fileCountConfigLabel.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // numericUpDown7
            // 
            this.numericUpDown7.Location = new System.Drawing.Point(269, 20);
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown7.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(785, 352);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.nodeConfigBox);
            this.Controls.Add(this.testHarnessConfigBox);
            this.Controls.Add(this.nodesStatusBox);
            this.Controls.Add(this.viewLogsButton);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.pushUpdateButton);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.backupReportButton);
            this.Controls.Add(this.diskUsageButton);
            this.Controls.Add(this.incrBackupNowButton);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.fulBackupNowButton);
            this.Controls.Add(this.stopNodeButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.startNodeButton);
            this.Controls.Add(this.restartNodeButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.refreshStatusButton);
            this.Controls.Add(this.nodesBox);
            this.Name = "Form1";
            this.Text = "Test Harness";
            this.nodesBox.ResumeLayout(false);
            this.nodesStatusBox.ResumeLayout(false);
            this.nodesStatusBox.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.nodeConfigBox.ResumeLayout(false);
            this.nodeConfigBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.testHarnessConfigBox.ResumeLayout(false);
            this.testHarnessConfigBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox nodesListBox;
        private System.Windows.Forms.GroupBox nodesBox;
        private System.Windows.Forms.Button rescanButton;
        private System.Windows.Forms.Button refreshStatusButton;
        private System.Windows.Forms.Button restartNodeButton;
        private System.Windows.Forms.Button startNodeButton;
        private System.Windows.Forms.Button stopNodeButton;
        private System.Windows.Forms.Button fulBackupNowButton;
        private System.Windows.Forms.Button incrBackupNowButton;
        private System.Windows.Forms.Button backupReportButton;
        private System.Windows.Forms.Button viewLogsButton;
        private System.Windows.Forms.GroupBox nodesStatusBox;
        private System.Windows.Forms.Label statusIPAddressLabel;
        private System.Windows.Forms.Label statusCurrentTaskLabel;
        private System.Windows.Forms.Label statusIndexVersionLabel;
        private System.Windows.Forms.Label statusRunningLabel;
        private System.Windows.Forms.Label statusGUIDLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.GroupBox nodeConfigBox;
        private System.Windows.Forms.Label backupSizeConfigLabel;
        private System.Windows.Forms.Label statusTestDirLabel;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label diskCapacityConfigLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.GroupBox testHarnessConfigBox;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label statusRefreshIntervalConfigLabel;
        private System.Windows.Forms.Button diskUsageButton;
        private System.Windows.Forms.Button pushUpdateButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label rescanIntervalConfigLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.Label freeSpaceConfigLabel;
        private System.Windows.Forms.NumericUpDown numericUpDown7;
        private System.Windows.Forms.Label fileCountConfigLabel;

    }
}

