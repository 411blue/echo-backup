/*
 * ===== Add a line for yourself below =====
 * Author: James Tate II    Date: 2012-10-24
 * 
 */

namespace Test_Harness
{
    partial class TestHarness
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
            this.nodesListBox = new System.Windows.Forms.ListBox();
            this.nodesBox = new System.Windows.Forms.GroupBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.rescanButton = new System.Windows.Forms.Button();
            this.refreshStatusButton = new System.Windows.Forms.Button();
            this.restartNodeButton = new System.Windows.Forms.Button();
            this.startNodeButton = new System.Windows.Forms.Button();
            this.stopNodeButton = new System.Windows.Forms.Button();
            this.fulBackupNowButton = new System.Windows.Forms.Button();
            this.incrBackupNowButton = new System.Windows.Forms.Button();
            this.backupReportButton = new System.Windows.Forms.Button();
            this.viewLogsButton = new System.Windows.Forms.Button();
            this.nodeStatusBox = new System.Windows.Forms.GroupBox();
            this.statusTestDirLabel = new System.Windows.Forms.Label();
            this.statusGUIDLabel = new System.Windows.Forms.Label();
            this.statusIPAddressLabel = new System.Windows.Forms.Label();
            this.statusCurrentTaskLabel = new System.Windows.Forms.Label();
            this.statusIndexVersionLabel = new System.Windows.Forms.Label();
            this.statusRunningLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.nodeConfigBox = new System.Windows.Forms.GroupBox();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.freeSpaceConfigLabel = new System.Windows.Forms.Label();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.fileCountConfigLabel = new System.Windows.Forms.Label();
            this.diskCapacityConfigLabel = new System.Windows.Forms.Label();
            this.backupSizeConfigLabel = new System.Windows.Forms.Label();
            this.testHarnessConfigBox = new System.Windows.Forms.GroupBox();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.rescanIntervalConfigLabel = new System.Windows.Forms.Label();
            this.statusRefreshIntervalConfigLabel = new System.Windows.Forms.Label();
            this.diskUsageButton = new System.Windows.Forms.Button();
            this.pushUpdateButton = new System.Windows.Forms.Button();
            this.statusCodeVersionLabel = new System.Windows.Forms.Label();
            this.nodeControlBox = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.currentDateTimeConfigLabel = new System.Windows.Forms.Label();
            this.backupFrequencyConfigLabel = new System.Windows.Forms.Label();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.FullBackupFrequencyConfigLabel = new System.Windows.Forms.Label();
            this.FullBackupFrequencyConfigLabel2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.StatusRunningTextBox = new System.Windows.Forms.TextBox();
            this.StatusIndexVersionTextBox = new System.Windows.Forms.TextBox();
            this.StatusCurrentTaskTextBox = new System.Windows.Forms.TextBox();
            this.StatusIPAddressTextBox = new System.Windows.Forms.TextBox();
            this.StatusGUIDTextBox = new System.Windows.Forms.TextBox();
            this.StatusTestDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.StatusCodeVersionTextBox = new System.Windows.Forms.TextBox();
            this.nodesBox.SuspendLayout();
            this.nodeStatusBox.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.nodeConfigBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.testHarnessConfigBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            this.nodeControlBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            this.SuspendLayout();
            // 
            // nodesListBox
            // 
            this.nodesListBox.FormattingEnabled = true;
            this.nodesListBox.Items.AddRange(new object[] {
            "Alan-PC",
            "AppleJack",
            "Laptop",
            "rm-rf",
            "OldComputer",
            "Patrick-PC"});
            this.nodesListBox.Location = new System.Drawing.Point(6, 19);
            this.nodesListBox.Name = "nodesListBox";
            this.nodesListBox.Size = new System.Drawing.Size(154, 108);
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
            this.nodesBox.Size = new System.Drawing.Size(166, 166);
            this.nodesBox.TabIndex = 2;
            this.nodesBox.TabStop = false;
            this.nodesBox.Text = "Nodes";
            this.nodesBox.Enter += new System.EventHandler(this.nodesBox_Enter);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(84, 135);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 1;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            // 
            // rescanButton
            // 
            this.rescanButton.Location = new System.Drawing.Point(6, 135);
            this.rescanButton.Name = "rescanButton";
            this.rescanButton.Size = new System.Drawing.Size(75, 23);
            this.rescanButton.TabIndex = 1;
            this.rescanButton.Text = "Rescan";
            this.rescanButton.UseVisualStyleBackColor = true;
            // 
            // refreshStatusButton
            // 
            this.refreshStatusButton.Location = new System.Drawing.Point(6, 19);
            this.refreshStatusButton.Name = "refreshStatusButton";
            this.refreshStatusButton.Size = new System.Drawing.Size(106, 23);
            this.refreshStatusButton.TabIndex = 4;
            this.refreshStatusButton.Text = "Refresh Status";
            this.refreshStatusButton.UseVisualStyleBackColor = true;
            // 
            // restartNodeButton
            // 
            this.restartNodeButton.Location = new System.Drawing.Point(118, 19);
            this.restartNodeButton.Name = "restartNodeButton";
            this.restartNodeButton.Size = new System.Drawing.Size(106, 23);
            this.restartNodeButton.TabIndex = 4;
            this.restartNodeButton.Text = "Restart Node";
            this.restartNodeButton.UseVisualStyleBackColor = true;
            // 
            // startNodeButton
            // 
            this.startNodeButton.Location = new System.Drawing.Point(6, 48);
            this.startNodeButton.Name = "startNodeButton";
            this.startNodeButton.Size = new System.Drawing.Size(106, 23);
            this.startNodeButton.TabIndex = 4;
            this.startNodeButton.Text = "Start Node";
            this.startNodeButton.UseVisualStyleBackColor = true;
            // 
            // stopNodeButton
            // 
            this.stopNodeButton.Location = new System.Drawing.Point(118, 48);
            this.stopNodeButton.Name = "stopNodeButton";
            this.stopNodeButton.Size = new System.Drawing.Size(106, 23);
            this.stopNodeButton.TabIndex = 4;
            this.stopNodeButton.Text = "Stop Node";
            this.stopNodeButton.UseVisualStyleBackColor = true;
            // 
            // fulBackupNowButton
            // 
            this.fulBackupNowButton.Location = new System.Drawing.Point(6, 77);
            this.fulBackupNowButton.Name = "fulBackupNowButton";
            this.fulBackupNowButton.Size = new System.Drawing.Size(106, 23);
            this.fulBackupNowButton.TabIndex = 4;
            this.fulBackupNowButton.Text = "Full Backup Now";
            this.fulBackupNowButton.UseVisualStyleBackColor = true;
            // 
            // incrBackupNowButton
            // 
            this.incrBackupNowButton.Location = new System.Drawing.Point(118, 77);
            this.incrBackupNowButton.Name = "incrBackupNowButton";
            this.incrBackupNowButton.Size = new System.Drawing.Size(106, 23);
            this.incrBackupNowButton.TabIndex = 4;
            this.incrBackupNowButton.Text = "Incr. Backup Now";
            this.incrBackupNowButton.UseVisualStyleBackColor = true;
            // 
            // backupReportButton
            // 
            this.backupReportButton.Location = new System.Drawing.Point(6, 106);
            this.backupReportButton.Name = "backupReportButton";
            this.backupReportButton.Size = new System.Drawing.Size(106, 23);
            this.backupReportButton.TabIndex = 4;
            this.backupReportButton.Text = "Backup Report...";
            this.backupReportButton.UseVisualStyleBackColor = true;
            // 
            // viewLogsButton
            // 
            this.viewLogsButton.Location = new System.Drawing.Point(118, 106);
            this.viewLogsButton.Name = "viewLogsButton";
            this.viewLogsButton.Size = new System.Drawing.Size(106, 23);
            this.viewLogsButton.TabIndex = 4;
            this.viewLogsButton.Text = "View Logs...";
            this.viewLogsButton.UseVisualStyleBackColor = true;
            // 
            // nodeStatusBox
            // 
            this.nodeStatusBox.Controls.Add(this.StatusCodeVersionTextBox);
            this.nodeStatusBox.Controls.Add(this.StatusTestDirectoryTextBox);
            this.nodeStatusBox.Controls.Add(this.StatusIPAddressTextBox);
            this.nodeStatusBox.Controls.Add(this.StatusGUIDTextBox);
            this.nodeStatusBox.Controls.Add(this.StatusCurrentTaskTextBox);
            this.nodeStatusBox.Controls.Add(this.StatusIndexVersionTextBox);
            this.nodeStatusBox.Controls.Add(this.StatusRunningTextBox);
            this.nodeStatusBox.Controls.Add(this.statusCodeVersionLabel);
            this.nodeStatusBox.Controls.Add(this.statusTestDirLabel);
            this.nodeStatusBox.Controls.Add(this.statusGUIDLabel);
            this.nodeStatusBox.Controls.Add(this.statusIPAddressLabel);
            this.nodeStatusBox.Controls.Add(this.statusCurrentTaskLabel);
            this.nodeStatusBox.Controls.Add(this.statusIndexVersionLabel);
            this.nodeStatusBox.Controls.Add(this.statusRunningLabel);
            this.nodeStatusBox.Location = new System.Drawing.Point(191, 12);
            this.nodeStatusBox.Name = "nodeStatusBox";
            this.nodeStatusBox.Size = new System.Drawing.Size(229, 166);
            this.nodeStatusBox.TabIndex = 5;
            this.nodeStatusBox.TabStop = false;
            this.nodeStatusBox.Text = "Node Status";
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 349);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(668, 24);
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
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Margin = new System.Windows.Forms.Padding(4, 3, 1, 3);
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 18);
            // 
            // nodeConfigBox
            // 
            this.nodeConfigBox.Controls.Add(this.comboBox2);
            this.nodeConfigBox.Controls.Add(this.comboBox1);
            this.nodeConfigBox.Controls.Add(this.dateTimePicker1);
            this.nodeConfigBox.Controls.Add(this.numericUpDown6);
            this.nodeConfigBox.Controls.Add(this.numericUpDown2);
            this.nodeConfigBox.Controls.Add(this.freeSpaceConfigLabel);
            this.nodeConfigBox.Controls.Add(this.numericUpDown7);
            this.nodeConfigBox.Controls.Add(this.numericUpDown5);
            this.nodeConfigBox.Controls.Add(this.numericUpDown1);
            this.nodeConfigBox.Controls.Add(this.fileCountConfigLabel);
            this.nodeConfigBox.Controls.Add(this.FullBackupFrequencyConfigLabel2);
            this.nodeConfigBox.Controls.Add(this.FullBackupFrequencyConfigLabel);
            this.nodeConfigBox.Controls.Add(this.backupFrequencyConfigLabel);
            this.nodeConfigBox.Controls.Add(this.currentDateTimeConfigLabel);
            this.nodeConfigBox.Controls.Add(this.diskCapacityConfigLabel);
            this.nodeConfigBox.Controls.Add(this.backupSizeConfigLabel);
            this.nodeConfigBox.Location = new System.Drawing.Point(12, 184);
            this.nodeConfigBox.Name = "nodeConfigBox";
            this.nodeConfigBox.Size = new System.Drawing.Size(408, 150);
            this.nodeConfigBox.TabIndex = 5;
            this.nodeConfigBox.TabStop = false;
            this.nodeConfigBox.Text = "Node Configuration";
            this.nodeConfigBox.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(269, 43);
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown6.TabIndex = 1;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(110, 43);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown2.TabIndex = 1;
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
            // numericUpDown7
            // 
            this.numericUpDown7.Location = new System.Drawing.Point(269, 20);
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown7.TabIndex = 1;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(110, 20);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown1.TabIndex = 1;
            // 
            // fileCountConfigLabel
            // 
            this.fileCountConfigLabel.AutoSize = true;
            this.fileCountConfigLabel.Location = new System.Drawing.Point(209, 22);
            this.fileCountConfigLabel.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this.fileCountConfigLabel.Name = "fileCountConfigLabel";
            this.fileCountConfigLabel.Size = new System.Drawing.Size(57, 13);
            this.fileCountConfigLabel.TabIndex = 0;
            this.fileCountConfigLabel.Text = "File Count:";
            this.fileCountConfigLabel.Click += new System.EventHandler(this.label1_Click_1);
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
            // backupSizeConfigLabel
            // 
            this.backupSizeConfigLabel.AutoSize = true;
            this.backupSizeConfigLabel.Location = new System.Drawing.Point(12, 21);
            this.backupSizeConfigLabel.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this.backupSizeConfigLabel.Name = "backupSizeConfigLabel";
            this.backupSizeConfigLabel.Size = new System.Drawing.Size(95, 13);
            this.backupSizeConfigLabel.TabIndex = 0;
            this.backupSizeConfigLabel.Text = "Backup Size (MB):";
            // 
            // testHarnessConfigBox
            // 
            this.testHarnessConfigBox.Controls.Add(this.numericUpDown3);
            this.testHarnessConfigBox.Controls.Add(this.numericUpDown4);
            this.testHarnessConfigBox.Controls.Add(this.rescanIntervalConfigLabel);
            this.testHarnessConfigBox.Controls.Add(this.statusRefreshIntervalConfigLabel);
            this.testHarnessConfigBox.Location = new System.Drawing.Point(427, 184);
            this.testHarnessConfigBox.Name = "testHarnessConfigBox";
            this.testHarnessConfigBox.Size = new System.Drawing.Size(230, 150);
            this.testHarnessConfigBox.TabIndex = 5;
            this.testHarnessConfigBox.TabStop = false;
            this.testHarnessConfigBox.Text = "Test Harness Configuration";
            this.testHarnessConfigBox.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(154, 42);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown3.TabIndex = 1;
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(154, 19);
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown4.TabIndex = 1;
            // 
            // rescanIntervalConfigLabel
            // 
            this.rescanIntervalConfigLabel.AutoSize = true;
            this.rescanIntervalConfigLabel.Location = new System.Drawing.Point(40, 45);
            this.rescanIntervalConfigLabel.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this.rescanIntervalConfigLabel.Name = "rescanIntervalConfigLabel";
            this.rescanIntervalConfigLabel.Size = new System.Drawing.Size(111, 13);
            this.rescanIntervalConfigLabel.TabIndex = 0;
            this.rescanIntervalConfigLabel.Text = "Rescan Interval (sec):";
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
            this.diskUsageButton.Location = new System.Drawing.Point(6, 135);
            this.diskUsageButton.Name = "diskUsageButton";
            this.diskUsageButton.Size = new System.Drawing.Size(106, 23);
            this.diskUsageButton.TabIndex = 4;
            this.diskUsageButton.Text = "Disk Usage...";
            this.diskUsageButton.UseVisualStyleBackColor = true;
            // 
            // pushUpdateButton
            // 
            this.pushUpdateButton.Location = new System.Drawing.Point(118, 135);
            this.pushUpdateButton.Name = "pushUpdateButton";
            this.pushUpdateButton.Size = new System.Drawing.Size(106, 23);
            this.pushUpdateButton.TabIndex = 4;
            this.pushUpdateButton.Text = "Push Update";
            this.pushUpdateButton.UseVisualStyleBackColor = true;
            // 
            // statusCodeVersionLabel
            // 
            this.statusCodeVersionLabel.AutoSize = true;
            this.statusCodeVersionLabel.Location = new System.Drawing.Point(7, 133);
            this.statusCodeVersionLabel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 4);
            this.statusCodeVersionLabel.Name = "statusCodeVersionLabel";
            this.statusCodeVersionLabel.Size = new System.Drawing.Size(73, 13);
            this.statusCodeVersionLabel.TabIndex = 0;
            this.statusCodeVersionLabel.Text = "Code Version:";
            this.statusCodeVersionLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // nodeControlBox
            // 
            this.nodeControlBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.nodeControlBox.Controls.Add(this.incrBackupNowButton);
            this.nodeControlBox.Controls.Add(this.refreshStatusButton);
            this.nodeControlBox.Controls.Add(this.restartNodeButton);
            this.nodeControlBox.Controls.Add(this.startNodeButton);
            this.nodeControlBox.Controls.Add(this.stopNodeButton);
            this.nodeControlBox.Controls.Add(this.fulBackupNowButton);
            this.nodeControlBox.Controls.Add(this.diskUsageButton);
            this.nodeControlBox.Controls.Add(this.viewLogsButton);
            this.nodeControlBox.Controls.Add(this.backupReportButton);
            this.nodeControlBox.Controls.Add(this.pushUpdateButton);
            this.nodeControlBox.Location = new System.Drawing.Point(427, 12);
            this.nodeControlBox.Name = "nodeControlBox";
            this.nodeControlBox.Size = new System.Drawing.Size(230, 166);
            this.nodeControlBox.TabIndex = 7;
            this.nodeControlBox.TabStop = false;
            this.nodeControlBox.Text = "Node Control";
            this.nodeControlBox.Enter += new System.EventHandler(this.groupBox1_Enter_1);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dddd, MMM dd, yyyy -  hh:mm:sstt";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(127, 69);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(243, 20);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // currentDateTimeConfigLabel
            // 
            this.currentDateTimeConfigLabel.AutoSize = true;
            this.currentDateTimeConfigLabel.Location = new System.Drawing.Point(7, 72);
            this.currentDateTimeConfigLabel.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this.currentDateTimeConfigLabel.Name = "currentDateTimeConfigLabel";
            this.currentDateTimeConfigLabel.Size = new System.Drawing.Size(117, 13);
            this.currentDateTimeConfigLabel.TabIndex = 0;
            this.currentDateTimeConfigLabel.Text = "Current Date and Time:";
            this.currentDateTimeConfigLabel.Click += new System.EventHandler(this.label1_Click_2);
            // 
            // backupFrequencyConfigLabel
            // 
            this.backupFrequencyConfigLabel.AutoSize = true;
            this.backupFrequencyConfigLabel.Location = new System.Drawing.Point(7, 95);
            this.backupFrequencyConfigLabel.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this.backupFrequencyConfigLabel.Name = "backupFrequencyConfigLabel";
            this.backupFrequencyConfigLabel.Size = new System.Drawing.Size(130, 13);
            this.backupFrequencyConfigLabel.TabIndex = 0;
            this.backupFrequencyConfigLabel.Text = "Backup Frequency: Every";
            this.backupFrequencyConfigLabel.Click += new System.EventHandler(this.label1_Click_2);
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(140, 93);
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown5.TabIndex = 1;
            this.numericUpDown5.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown5.ValueChanged += new System.EventHandler(this.numericUpDown5_ValueChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "MicroSeconds",
            "Milliseconds",
            "Seconds",
            "Minutes",
            "Hours",
            "Days",
            "Weeks",
            "Months",
            "Years",
            "Decades",
            "Centuries",
            "Millenia",
            "Megannum"});
            this.comboBox1.Location = new System.Drawing.Point(196, 92);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(131, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Text = "Days";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // FullBackupFrequencyConfigLabel
            // 
            this.FullBackupFrequencyConfigLabel.AutoSize = true;
            this.FullBackupFrequencyConfigLabel.Location = new System.Drawing.Point(7, 121);
            this.FullBackupFrequencyConfigLabel.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this.FullBackupFrequencyConfigLabel.Name = "FullBackupFrequencyConfigLabel";
            this.FullBackupFrequencyConfigLabel.Size = new System.Drawing.Size(149, 13);
            this.FullBackupFrequencyConfigLabel.TabIndex = 0;
            this.FullBackupFrequencyConfigLabel.Text = "Full Backup Frequency: Every";
            this.FullBackupFrequencyConfigLabel.Click += new System.EventHandler(this.label1_Click_2);
            // 
            // FullBackupFrequencyConfigLabel2
            // 
            this.FullBackupFrequencyConfigLabel2.AutoSize = true;
            this.FullBackupFrequencyConfigLabel2.Location = new System.Drawing.Point(217, 121);
            this.FullBackupFrequencyConfigLabel2.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this.FullBackupFrequencyConfigLabel2.Name = "FullBackupFrequencyConfigLabel2";
            this.FullBackupFrequencyConfigLabel2.Size = new System.Drawing.Size(142, 13);
            this.FullBackupFrequencyConfigLabel2.TabIndex = 0;
            this.FullBackupFrequencyConfigLabel2.Text = "backup will be a full backup.";
            this.FullBackupFrequencyConfigLabel2.Click += new System.EventHandler(this.label1_Click_2);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "1st",
            "2nd",
            "3rd",
            "4th",
            "5th",
            "6th",
            "7th",
            "8th",
            "9th",
            "10th",
            "11th",
            "12th",
            "13th",
            "14th",
            "15th",
            "16th",
            "17th",
            "18th",
            "19th",
            "20th",
            "21st",
            "22nd",
            "23rd",
            "24th",
            "25th",
            "26th",
            "27th",
            "28th",
            "29th",
            "30th"});
            this.comboBox2.Location = new System.Drawing.Point(159, 118);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(51, 21);
            this.comboBox2.TabIndex = 4;
            this.comboBox2.Text = "7th";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // StatusRunningTextBox
            // 
            this.StatusRunningTextBox.Location = new System.Drawing.Point(90, 16);
            this.StatusRunningTextBox.Name = "StatusRunningTextBox";
            this.StatusRunningTextBox.Size = new System.Drawing.Size(133, 20);
            this.StatusRunningTextBox.TabIndex = 1;
            this.StatusRunningTextBox.Text = "Yes";
            // 
            // StatusIndexVersionTextBox
            // 
            this.StatusIndexVersionTextBox.Location = new System.Drawing.Point(90, 35);
            this.StatusIndexVersionTextBox.Name = "StatusIndexVersionTextBox";
            this.StatusIndexVersionTextBox.Size = new System.Drawing.Size(133, 20);
            this.StatusIndexVersionTextBox.TabIndex = 1;
            this.StatusIndexVersionTextBox.Text = "12345";
            // 
            // StatusCurrentTaskTextBox
            // 
            this.StatusCurrentTaskTextBox.Location = new System.Drawing.Point(90, 54);
            this.StatusCurrentTaskTextBox.Name = "StatusCurrentTaskTextBox";
            this.StatusCurrentTaskTextBox.Size = new System.Drawing.Size(133, 20);
            this.StatusCurrentTaskTextBox.TabIndex = 1;
            this.StatusCurrentTaskTextBox.Text = "Idle";
            // 
            // StatusIPAddressTextBox
            // 
            this.StatusIPAddressTextBox.Location = new System.Drawing.Point(90, 73);
            this.StatusIPAddressTextBox.Name = "StatusIPAddressTextBox";
            this.StatusIPAddressTextBox.Size = new System.Drawing.Size(133, 20);
            this.StatusIPAddressTextBox.TabIndex = 1;
            this.StatusIPAddressTextBox.Text = "10.0.0.1";
            // 
            // StatusGUIDTextBox
            // 
            this.StatusGUIDTextBox.Location = new System.Drawing.Point(90, 92);
            this.StatusGUIDTextBox.Name = "StatusGUIDTextBox";
            this.StatusGUIDTextBox.Size = new System.Drawing.Size(133, 20);
            this.StatusGUIDTextBox.TabIndex = 1;
            this.StatusGUIDTextBox.Text = "unique1";
            // 
            // StatusTestDirectoryTextBox
            // 
            this.StatusTestDirectoryTextBox.Location = new System.Drawing.Point(90, 111);
            this.StatusTestDirectoryTextBox.Name = "StatusTestDirectoryTextBox";
            this.StatusTestDirectoryTextBox.Size = new System.Drawing.Size(133, 20);
            this.StatusTestDirectoryTextBox.TabIndex = 1;
            this.StatusTestDirectoryTextBox.Text = "C:\\Program  Files\\Echo Backup\\";
            // 
            // StatusCodeVersionTextBox
            // 
            this.StatusCodeVersionTextBox.Location = new System.Drawing.Point(90, 130);
            this.StatusCodeVersionTextBox.Name = "StatusCodeVersionTextBox";
            this.StatusCodeVersionTextBox.Size = new System.Drawing.Size(133, 20);
            this.StatusCodeVersionTextBox.TabIndex = 1;
            this.StatusCodeVersionTextBox.Text = "1.2.3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(668, 373);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.nodeControlBox);
            this.Controls.Add(this.nodeStatusBox);
            this.Controls.Add(this.nodeConfigBox);
            this.Controls.Add(this.testHarnessConfigBox);
            this.Controls.Add(this.nodesBox);
            this.Name = "Form1";
            this.Text = "Echo Backup Test Harness";
            this.nodesBox.ResumeLayout(false);
            this.nodeStatusBox.ResumeLayout(false);
            this.nodeStatusBox.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.nodeConfigBox.ResumeLayout(false);
            this.nodeConfigBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.testHarnessConfigBox.ResumeLayout(false);
            this.testHarnessConfigBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            this.nodeControlBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
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
        private System.Windows.Forms.GroupBox nodeStatusBox;
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
        private System.Windows.Forms.GroupBox testHarnessConfigBox;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.Label statusRefreshIntervalConfigLabel;
        private System.Windows.Forms.Button diskUsageButton;
        private System.Windows.Forms.Button pushUpdateButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label rescanIntervalConfigLabel;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.Label freeSpaceConfigLabel;
        private System.Windows.Forms.NumericUpDown numericUpDown7;
        private System.Windows.Forms.Label fileCountConfigLabel;
        private System.Windows.Forms.Label statusCodeVersionLabel;
        private System.Windows.Forms.GroupBox nodeControlBox;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label currentDateTimeConfigLabel;
        private System.Windows.Forms.Label backupFrequencyConfigLabel;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label FullBackupFrequencyConfigLabel;
        private System.Windows.Forms.Label FullBackupFrequencyConfigLabel2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox StatusCodeVersionTextBox;
        private System.Windows.Forms.TextBox StatusTestDirectoryTextBox;
        private System.Windows.Forms.TextBox StatusIPAddressTextBox;
        private System.Windows.Forms.TextBox StatusGUIDTextBox;
        private System.Windows.Forms.TextBox StatusCurrentTaskTextBox;
        private System.Windows.Forms.TextBox StatusIndexVersionTextBox;
        private System.Windows.Forms.TextBox StatusRunningTextBox;

    }
}

