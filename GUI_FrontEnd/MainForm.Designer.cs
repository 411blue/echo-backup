namespace GUI_FrontEnd
{
    partial class MainForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlTabGUI = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabNodeSets = new System.Windows.Forms.TabPage();
            this.pnlNodeSetsFill = new System.Windows.Forms.Panel();
            this.dataGridViewNodeSets = new System.Windows.Forms.DataGridView();
            this.pnlNodeSetsBottom = new System.Windows.Forms.Panel();
            this.btnLoadNodeSet = new System.Windows.Forms.Button();
            this.btnSaveNodeSet = new System.Windows.Forms.Button();
            this.chkUtilizationOverride = new System.Windows.Forms.CheckBox();
            this.lblRedundancy = new System.Windows.Forms.Label();
            this.numUpDwnRedundancy = new System.Windows.Forms.NumericUpDown();
            this.NodeGrid = new System.Windows.Forms.Panel();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.tabDiskReport = new System.Windows.Forms.TabPage();
            this.gb4 = new System.Windows.Forms.Label();
            this.gb2 = new System.Windows.Forms.Label();
            this.gb3 = new System.Windows.Forms.Label();
            this.gb1 = new System.Windows.Forms.Label();
            this.bytes4 = new System.Windows.Forms.Label();
            this.bytes2 = new System.Windows.Forms.Label();
            this.bytes3 = new System.Windows.Forms.Label();
            this.bytes1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DiskList = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tabBackupStatus = new System.Windows.Forms.TabPage();
            this.pnlBackupStatusFill = new System.Windows.Forms.Panel();
            this.dataGridViewBackupStatus = new System.Windows.Forms.DataGridView();
            this.colNodeSet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastBackup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNextBackup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBackupStatusTop = new System.Windows.Forms.Panel();
            this.pnlBackupStatusBottom = new System.Windows.Forms.Panel();
            this.btnLogs = new System.Windows.Forms.Button();
            this.btnDiskUsage = new System.Windows.Forms.Button();
            this.btnBackupNow = new System.Windows.Forms.Button();
            this.tabDataRecovery = new System.Windows.Forms.TabPage();
            this.pnlDataRecoveryFill = new System.Windows.Forms.Panel();
            this.dataGridViewDataRecovery = new System.Windows.Forms.DataGridView();
            this.colVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrigSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlDataRecoveryBottom = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnBrowseDestinations = new System.Windows.Forms.Button();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.lblDestination = new System.Windows.Forms.Label();
            this.pnlDataRecoveryTop = new System.Windows.Forms.Panel();
            this.btnFileBrowser = new System.Windows.Forms.Button();
            this.txtFileBrowser = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabScheduler = new System.Windows.Forms.TabPage();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btmSave = new System.Windows.Forms.Button();
            this.lblNodeSets = new System.Windows.Forms.Label();
            this.comboNodeSets = new System.Windows.Forms.ComboBox();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.comboFrequency = new System.Windows.Forms.ComboBox();
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitEchoBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.guid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reliablity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxBackup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalCapacity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.freeSpace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nonBackup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.backupData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1.SuspendLayout();
            this.pnlTabGUI.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabNodeSets.SuspendLayout();
            this.pnlNodeSetsFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNodeSets)).BeginInit();
            this.pnlNodeSetsBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnRedundancy)).BeginInit();
            this.NodeGrid.SuspendLayout();
            this.tabDiskReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tabBackupStatus.SuspendLayout();
            this.pnlBackupStatusFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBackupStatus)).BeginInit();
            this.pnlBackupStatusBottom.SuspendLayout();
            this.tabDataRecovery.SuspendLayout();
            this.pnlDataRecoveryFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDataRecovery)).BeginInit();
            this.pnlDataRecoveryBottom.SuspendLayout();
            this.pnlDataRecoveryTop.SuspendLayout();
            this.tabScheduler.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 540);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(142, 17);
            this.lblStatus.Text = "Welcome to Echo Backup";
            // 
            // pnlTabGUI
            // 
            this.pnlTabGUI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTabGUI.Controls.Add(this.tabControl);
            this.pnlTabGUI.Location = new System.Drawing.Point(0, 27);
            this.pnlTabGUI.Name = "pnlTabGUI";
            this.pnlTabGUI.Size = new System.Drawing.Size(784, 513);
            this.pnlTabGUI.TabIndex = 2;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabNodeSets);
            this.tabControl.Controls.Add(this.tabDiskReport);
            this.tabControl.Controls.Add(this.tabBackupStatus);
            this.tabControl.Controls.Add(this.tabDataRecovery);
            this.tabControl.Controls.Add(this.tabScheduler);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(784, 513);
            this.tabControl.TabIndex = 0;
            // 
            // tabNodeSets
            // 
            this.tabNodeSets.BackColor = System.Drawing.SystemColors.Control;
            this.tabNodeSets.Controls.Add(this.pnlNodeSetsFill);
            this.tabNodeSets.Controls.Add(this.pnlNodeSetsBottom);
            this.tabNodeSets.Controls.Add(this.NodeGrid);
            this.tabNodeSets.Location = new System.Drawing.Point(4, 22);
            this.tabNodeSets.Name = "tabNodeSets";
            this.tabNodeSets.Padding = new System.Windows.Forms.Padding(3);
            this.tabNodeSets.Size = new System.Drawing.Size(776, 487);
            this.tabNodeSets.TabIndex = 0;
            this.tabNodeSets.Text = "Node Sets";
            // 
            // pnlNodeSetsFill
            // 
            this.pnlNodeSetsFill.Controls.Add(this.dataGridViewNodeSets);
            this.pnlNodeSetsFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNodeSetsFill.Location = new System.Drawing.Point(3, 65);
            this.pnlNodeSetsFill.Name = "pnlNodeSetsFill";
            this.pnlNodeSetsFill.Size = new System.Drawing.Size(770, 347);
            this.pnlNodeSetsFill.TabIndex = 2;
            // 
            // dataGridViewNodeSets
            // 
            this.dataGridViewNodeSets.AllowUserToOrderColumns = true;
            this.dataGridViewNodeSets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewNodeSets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNodeSets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.guid,
            this.name,
            this.ip,
            this.mac,
            this.reliablity,
            this.maxBackup,
            this.totalCapacity,
            this.freeSpace,
            this.nonBackup,
            this.backupData,
            this.status});
            this.dataGridViewNodeSets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewNodeSets.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewNodeSets.Name = "dataGridViewNodeSets";
            this.dataGridViewNodeSets.Size = new System.Drawing.Size(770, 347);
            this.dataGridViewNodeSets.TabIndex = 1;
            // 
            // pnlNodeSetsBottom
            // 
            this.pnlNodeSetsBottom.BackColor = System.Drawing.SystemColors.Control;
            this.pnlNodeSetsBottom.Controls.Add(this.btnLoadNodeSet);
            this.pnlNodeSetsBottom.Controls.Add(this.btnSaveNodeSet);
            this.pnlNodeSetsBottom.Controls.Add(this.chkUtilizationOverride);
            this.pnlNodeSetsBottom.Controls.Add(this.lblRedundancy);
            this.pnlNodeSetsBottom.Controls.Add(this.numUpDwnRedundancy);
            this.pnlNodeSetsBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNodeSetsBottom.Location = new System.Drawing.Point(3, 412);
            this.pnlNodeSetsBottom.Name = "pnlNodeSetsBottom";
            this.pnlNodeSetsBottom.Size = new System.Drawing.Size(770, 72);
            this.pnlNodeSetsBottom.TabIndex = 1;
            // 
            // btnLoadNodeSet
            // 
            this.btnLoadNodeSet.Location = new System.Drawing.Point(107, 36);
            this.btnLoadNodeSet.Name = "btnLoadNodeSet";
            this.btnLoadNodeSet.Size = new System.Drawing.Size(136, 23);
            this.btnLoadNodeSet.TabIndex = 12;
            this.btnLoadNodeSet.Text = "Load Existing Node Set";
            this.btnLoadNodeSet.UseVisualStyleBackColor = true;
            // 
            // btnSaveNodeSet
            // 
            this.btnSaveNodeSet.Location = new System.Drawing.Point(9, 36);
            this.btnSaveNodeSet.Name = "btnSaveNodeSet";
            this.btnSaveNodeSet.Size = new System.Drawing.Size(92, 23);
            this.btnSaveNodeSet.TabIndex = 11;
            this.btnSaveNodeSet.Text = "Save Node Set";
            this.btnSaveNodeSet.UseVisualStyleBackColor = true;
            // 
            // chkUtilizationOverride
            // 
            this.chkUtilizationOverride.AutoSize = true;
            this.chkUtilizationOverride.Location = new System.Drawing.Point(552, 40);
            this.chkUtilizationOverride.Name = "chkUtilizationOverride";
            this.chkUtilizationOverride.Size = new System.Drawing.Size(200, 17);
            this.chkUtilizationOverride.TabIndex = 10;
            this.chkUtilizationOverride.Text = "Allow Utilization Percentage Override";
            this.chkUtilizationOverride.UseVisualStyleBackColor = true;
            // 
            // lblRedundancy
            // 
            this.lblRedundancy.AutoSize = true;
            this.lblRedundancy.Location = new System.Drawing.Point(6, 12);
            this.lblRedundancy.Name = "lblRedundancy";
            this.lblRedundancy.Size = new System.Drawing.Size(160, 13);
            this.lblRedundancy.TabIndex = 9;
            this.lblRedundancy.Text = "Number of Redundant Backups:";
            // 
            // numUpDwnRedundancy
            // 
            this.numUpDwnRedundancy.Location = new System.Drawing.Point(182, 10);
            this.numUpDwnRedundancy.Name = "numUpDwnRedundancy";
            this.numUpDwnRedundancy.Size = new System.Drawing.Size(61, 20);
            this.numUpDwnRedundancy.TabIndex = 8;
            // 
            // NodeGrid
            // 
            this.NodeGrid.BackColor = System.Drawing.SystemColors.Control;
            this.NodeGrid.Controls.Add(this.btnRefreshList);
            this.NodeGrid.Controls.Add(this.lblListTitle);
            this.NodeGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.NodeGrid.Location = new System.Drawing.Point(3, 3);
            this.NodeGrid.Name = "NodeGrid";
            this.NodeGrid.Size = new System.Drawing.Size(770, 62);
            this.NodeGrid.TabIndex = 0;
            // 
            // btnRefreshList
            // 
            this.btnRefreshList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshList.Location = new System.Drawing.Point(690, 33);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshList.TabIndex = 4;
            this.btnRefreshList.Text = "Refresh List";
            this.btnRefreshList.UseVisualStyleBackColor = true;
            // 
            // lblListTitle
            // 
            this.lblListTitle.AutoSize = true;
            this.lblListTitle.Location = new System.Drawing.Point(5, 46);
            this.lblListTitle.Name = "lblListTitle";
            this.lblListTitle.Size = new System.Drawing.Size(98, 13);
            this.lblListTitle.TabIndex = 3;
            this.lblListTitle.Text = "Discovered Nodes:";
            // 
            // tabDiskReport
            // 
            this.tabDiskReport.BackColor = System.Drawing.SystemColors.Control;
            this.tabDiskReport.Controls.Add(this.gb4);
            this.tabDiskReport.Controls.Add(this.gb2);
            this.tabDiskReport.Controls.Add(this.gb3);
            this.tabDiskReport.Controls.Add(this.gb1);
            this.tabDiskReport.Controls.Add(this.bytes4);
            this.tabDiskReport.Controls.Add(this.bytes2);
            this.tabDiskReport.Controls.Add(this.bytes3);
            this.tabDiskReport.Controls.Add(this.bytes1);
            this.tabDiskReport.Controls.Add(this.label7);
            this.tabDiskReport.Controls.Add(this.label1);
            this.tabDiskReport.Controls.Add(this.DiskList);
            this.tabDiskReport.Controls.Add(this.label6);
            this.tabDiskReport.Controls.Add(this.label5);
            this.tabDiskReport.Controls.Add(this.label4);
            this.tabDiskReport.Controls.Add(this.label3);
            this.tabDiskReport.Controls.Add(this.label2);
            this.tabDiskReport.Controls.Add(this.pictureBox5);
            this.tabDiskReport.Controls.Add(this.pictureBox4);
            this.tabDiskReport.Controls.Add(this.pictureBox3);
            this.tabDiskReport.Location = new System.Drawing.Point(4, 22);
            this.tabDiskReport.Name = "tabDiskReport";
            this.tabDiskReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabDiskReport.Size = new System.Drawing.Size(776, 487);
            this.tabDiskReport.TabIndex = 1;
            this.tabDiskReport.Text = "Disk Report";
            // 
            // gb4
            // 
            this.gb4.AutoSize = true;
            this.gb4.Location = new System.Drawing.Point(272, 266);
            this.gb4.Name = "gb4";
            this.gb4.Size = new System.Drawing.Size(25, 13);
            this.gb4.TabIndex = 92;
            this.gb4.Text = "gb4";
            // 
            // gb2
            // 
            this.gb2.AutoSize = true;
            this.gb2.Location = new System.Drawing.Point(272, 225);
            this.gb2.Name = "gb2";
            this.gb2.Size = new System.Drawing.Size(25, 13);
            this.gb2.TabIndex = 91;
            this.gb2.Text = "gb2";
            // 
            // gb3
            // 
            this.gb3.AutoSize = true;
            this.gb3.Location = new System.Drawing.Point(272, 244);
            this.gb3.Name = "gb3";
            this.gb3.Size = new System.Drawing.Size(25, 13);
            this.gb3.TabIndex = 90;
            this.gb3.Text = "gb3";
            // 
            // gb1
            // 
            this.gb1.AutoSize = true;
            this.gb1.Location = new System.Drawing.Point(272, 203);
            this.gb1.Name = "gb1";
            this.gb1.Size = new System.Drawing.Size(25, 13);
            this.gb1.TabIndex = 89;
            this.gb1.Text = "gb1";
            // 
            // bytes4
            // 
            this.bytes4.AutoSize = true;
            this.bytes4.Location = new System.Drawing.Point(166, 266);
            this.bytes4.Name = "bytes4";
            this.bytes4.Size = new System.Drawing.Size(38, 13);
            this.bytes4.TabIndex = 88;
            this.bytes4.Text = "bytes4";
            // 
            // bytes2
            // 
            this.bytes2.AutoSize = true;
            this.bytes2.Location = new System.Drawing.Point(166, 225);
            this.bytes2.Name = "bytes2";
            this.bytes2.Size = new System.Drawing.Size(38, 13);
            this.bytes2.TabIndex = 87;
            this.bytes2.Text = "bytes2";
            // 
            // bytes3
            // 
            this.bytes3.AutoSize = true;
            this.bytes3.Location = new System.Drawing.Point(166, 244);
            this.bytes3.Name = "bytes3";
            this.bytes3.Size = new System.Drawing.Size(38, 13);
            this.bytes3.TabIndex = 86;
            this.bytes3.Text = "bytes3";
            // 
            // bytes1
            // 
            this.bytes1.AutoSize = true;
            this.bytes1.Location = new System.Drawing.Point(166, 203);
            this.bytes1.Name = "bytes1";
            this.bytes1.Size = new System.Drawing.Size(38, 13);
            this.bytes1.TabIndex = 85;
            this.bytes1.Text = "bytes1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 84;
            this.label7.Text = "Hosts";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 83;
            this.label1.Text = "Bytes";
            // 
            // DiskList
            // 
            this.DiskList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DiskList.FormattingEnabled = true;
            this.DiskList.Location = new System.Drawing.Point(8, 27);
            this.DiskList.Name = "DiskList";
            this.DiskList.Size = new System.Drawing.Size(121, 21);
            this.DiskList.TabIndex = 82;
            this.DiskList.TabStop = false;
            this.DiskList.TextChanged += new System.EventHandler(this.hostList_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 266);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 81;
            this.label6.Text = "Backup Data";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(272, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 80;
            this.label5.Text = "GB";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 225);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 79;
            this.label4.Text = "Free Space";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 78;
            this.label3.Text = "Non-backup Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "Total Capacity";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Orange;
            this.pictureBox5.Location = new System.Drawing.Point(148, 266);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(12, 13);
            this.pictureBox5.TabIndex = 76;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Blue;
            this.pictureBox4.Location = new System.Drawing.Point(148, 225);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(12, 13);
            this.pictureBox4.TabIndex = 75;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Red;
            this.pictureBox3.Location = new System.Drawing.Point(148, 244);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(12, 13);
            this.pictureBox3.TabIndex = 74;
            this.pictureBox3.TabStop = false;
            // 
            // tabBackupStatus
            // 
            this.tabBackupStatus.BackColor = System.Drawing.SystemColors.Control;
            this.tabBackupStatus.Controls.Add(this.pnlBackupStatusFill);
            this.tabBackupStatus.Controls.Add(this.pnlBackupStatusTop);
            this.tabBackupStatus.Controls.Add(this.pnlBackupStatusBottom);
            this.tabBackupStatus.Location = new System.Drawing.Point(4, 22);
            this.tabBackupStatus.Name = "tabBackupStatus";
            this.tabBackupStatus.Size = new System.Drawing.Size(776, 487);
            this.tabBackupStatus.TabIndex = 2;
            this.tabBackupStatus.Text = "Backup Status";
            // 
            // pnlBackupStatusFill
            // 
            this.pnlBackupStatusFill.Controls.Add(this.dataGridViewBackupStatus);
            this.pnlBackupStatusFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackupStatusFill.Location = new System.Drawing.Point(0, 22);
            this.pnlBackupStatusFill.Name = "pnlBackupStatusFill";
            this.pnlBackupStatusFill.Size = new System.Drawing.Size(776, 428);
            this.pnlBackupStatusFill.TabIndex = 2;
            // 
            // dataGridViewBackupStatus
            // 
            this.dataGridViewBackupStatus.AllowUserToOrderColumns = true;
            this.dataGridViewBackupStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBackupStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBackupStatus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNodeSet,
            this.colLastBackup,
            this.colNextBackup});
            this.dataGridViewBackupStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewBackupStatus.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewBackupStatus.Name = "dataGridViewBackupStatus";
            this.dataGridViewBackupStatus.Size = new System.Drawing.Size(776, 428);
            this.dataGridViewBackupStatus.TabIndex = 2;
            // 
            // colNodeSet
            // 
            this.colNodeSet.HeaderText = "Node Set";
            this.colNodeSet.Name = "colNodeSet";
            // 
            // colLastBackup
            // 
            this.colLastBackup.HeaderText = "Last Successful Backup";
            this.colLastBackup.Name = "colLastBackup";
            // 
            // colNextBackup
            // 
            this.colNextBackup.HeaderText = "Next Scheduled Backup";
            this.colNextBackup.Name = "colNextBackup";
            // 
            // pnlBackupStatusTop
            // 
            this.pnlBackupStatusTop.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBackupStatusTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBackupStatusTop.Location = new System.Drawing.Point(0, 0);
            this.pnlBackupStatusTop.Name = "pnlBackupStatusTop";
            this.pnlBackupStatusTop.Size = new System.Drawing.Size(776, 22);
            this.pnlBackupStatusTop.TabIndex = 1;
            // 
            // pnlBackupStatusBottom
            // 
            this.pnlBackupStatusBottom.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBackupStatusBottom.Controls.Add(this.btnLogs);
            this.pnlBackupStatusBottom.Controls.Add(this.btnDiskUsage);
            this.pnlBackupStatusBottom.Controls.Add(this.btnBackupNow);
            this.pnlBackupStatusBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBackupStatusBottom.Location = new System.Drawing.Point(0, 450);
            this.pnlBackupStatusBottom.Name = "pnlBackupStatusBottom";
            this.pnlBackupStatusBottom.Size = new System.Drawing.Size(776, 37);
            this.pnlBackupStatusBottom.TabIndex = 0;
            // 
            // btnLogs
            // 
            this.btnLogs.Location = new System.Drawing.Point(91, 6);
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(75, 23);
            this.btnLogs.TabIndex = 7;
            this.btnLogs.Text = "View Logs";
            this.btnLogs.UseVisualStyleBackColor = true;
            // 
            // btnDiskUsage
            // 
            this.btnDiskUsage.Location = new System.Drawing.Point(172, 6);
            this.btnDiskUsage.Name = "btnDiskUsage";
            this.btnDiskUsage.Size = new System.Drawing.Size(146, 23);
            this.btnDiskUsage.TabIndex = 6;
            this.btnDiskUsage.Text = "Launch Disk Usage Report";
            this.btnDiskUsage.UseVisualStyleBackColor = true;
            // 
            // btnBackupNow
            // 
            this.btnBackupNow.Location = new System.Drawing.Point(8, 6);
            this.btnBackupNow.Name = "btnBackupNow";
            this.btnBackupNow.Size = new System.Drawing.Size(77, 23);
            this.btnBackupNow.TabIndex = 5;
            this.btnBackupNow.Text = "Backup Now";
            this.btnBackupNow.UseVisualStyleBackColor = true;
            // 
            // tabDataRecovery
            // 
            this.tabDataRecovery.BackColor = System.Drawing.SystemColors.Control;
            this.tabDataRecovery.Controls.Add(this.pnlDataRecoveryFill);
            this.tabDataRecovery.Controls.Add(this.pnlDataRecoveryBottom);
            this.tabDataRecovery.Controls.Add(this.pnlDataRecoveryTop);
            this.tabDataRecovery.Location = new System.Drawing.Point(4, 22);
            this.tabDataRecovery.Name = "tabDataRecovery";
            this.tabDataRecovery.Size = new System.Drawing.Size(776, 487);
            this.tabDataRecovery.TabIndex = 3;
            this.tabDataRecovery.Text = "Data Recovery";
            // 
            // pnlDataRecoveryFill
            // 
            this.pnlDataRecoveryFill.Controls.Add(this.dataGridViewDataRecovery);
            this.pnlDataRecoveryFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDataRecoveryFill.Location = new System.Drawing.Point(0, 47);
            this.pnlDataRecoveryFill.Name = "pnlDataRecoveryFill";
            this.pnlDataRecoveryFill.Size = new System.Drawing.Size(776, 357);
            this.pnlDataRecoveryFill.TabIndex = 2;
            // 
            // dataGridViewDataRecovery
            // 
            this.dataGridViewDataRecovery.AllowUserToOrderColumns = true;
            this.dataGridViewDataRecovery.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDataRecovery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDataRecovery.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colVersion,
            this.colDateTime,
            this.colOrigSize});
            this.dataGridViewDataRecovery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDataRecovery.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewDataRecovery.Name = "dataGridViewDataRecovery";
            this.dataGridViewDataRecovery.Size = new System.Drawing.Size(776, 357);
            this.dataGridViewDataRecovery.TabIndex = 4;
            // 
            // colVersion
            // 
            this.colVersion.HeaderText = "Version Number";
            this.colVersion.Name = "colVersion";
            // 
            // colDateTime
            // 
            this.colDateTime.HeaderText = "Date/Time of Backup";
            this.colDateTime.Name = "colDateTime";
            // 
            // colOrigSize
            // 
            this.colOrigSize.HeaderText = "Original Size";
            this.colOrigSize.Name = "colOrigSize";
            // 
            // pnlDataRecoveryBottom
            // 
            this.pnlDataRecoveryBottom.BackColor = System.Drawing.SystemColors.Control;
            this.pnlDataRecoveryBottom.Controls.Add(this.progressBar1);
            this.pnlDataRecoveryBottom.Controls.Add(this.btnRestore);
            this.pnlDataRecoveryBottom.Controls.Add(this.btnBrowseDestinations);
            this.pnlDataRecoveryBottom.Controls.Add(this.txtDestination);
            this.pnlDataRecoveryBottom.Controls.Add(this.lblDestination);
            this.pnlDataRecoveryBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDataRecoveryBottom.Location = new System.Drawing.Point(0, 404);
            this.pnlDataRecoveryBottom.Name = "pnlDataRecoveryBottom";
            this.pnlDataRecoveryBottom.Size = new System.Drawing.Size(776, 83);
            this.pnlDataRecoveryBottom.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(125, 45);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(643, 23);
            this.progressBar1.TabIndex = 13;
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(11, 45);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(79, 23);
            this.btnRestore.TabIndex = 12;
            this.btnRestore.Text = "Restore Now";
            this.btnRestore.UseVisualStyleBackColor = true;
            // 
            // btnBrowseDestinations
            // 
            this.btnBrowseDestinations.Location = new System.Drawing.Point(664, 6);
            this.btnBrowseDestinations.Name = "btnBrowseDestinations";
            this.btnBrowseDestinations.Size = new System.Drawing.Size(104, 23);
            this.btnBrowseDestinations.TabIndex = 11;
            this.btnBrowseDestinations.Text = "Browse Directories";
            this.btnBrowseDestinations.UseVisualStyleBackColor = true;
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(125, 8);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(536, 20);
            this.txtDestination.TabIndex = 10;
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Location = new System.Drawing.Point(8, 11);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(108, 13);
            this.lblDestination.TabIndex = 9;
            this.lblDestination.Text = "Destination Directory:";
            // 
            // pnlDataRecoveryTop
            // 
            this.pnlDataRecoveryTop.BackColor = System.Drawing.SystemColors.Control;
            this.pnlDataRecoveryTop.Controls.Add(this.btnFileBrowser);
            this.pnlDataRecoveryTop.Controls.Add(this.txtFileBrowser);
            this.pnlDataRecoveryTop.Controls.Add(this.label8);
            this.pnlDataRecoveryTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDataRecoveryTop.Location = new System.Drawing.Point(0, 0);
            this.pnlDataRecoveryTop.Name = "pnlDataRecoveryTop";
            this.pnlDataRecoveryTop.Size = new System.Drawing.Size(776, 47);
            this.pnlDataRecoveryTop.TabIndex = 0;
            // 
            // btnFileBrowser
            // 
            this.btnFileBrowser.Location = new System.Drawing.Point(664, 12);
            this.btnFileBrowser.Name = "btnFileBrowser";
            this.btnFileBrowser.Size = new System.Drawing.Size(104, 23);
            this.btnFileBrowser.TabIndex = 5;
            this.btnFileBrowser.Text = "Browse Directories";
            this.btnFileBrowser.UseVisualStyleBackColor = true;
            // 
            // txtFileBrowser
            // 
            this.txtFileBrowser.Location = new System.Drawing.Point(125, 14);
            this.txtFileBrowser.Name = "txtFileBrowser";
            this.txtFileBrowser.Size = new System.Drawing.Size(533, 20);
            this.txtFileBrowser.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Select a Directory/File:";
            // 
            // tabScheduler
            // 
            this.tabScheduler.BackColor = System.Drawing.SystemColors.Control;
            this.tabScheduler.Controls.Add(this.btnLoad);
            this.tabScheduler.Controls.Add(this.btmSave);
            this.tabScheduler.Controls.Add(this.lblNodeSets);
            this.tabScheduler.Controls.Add(this.comboNodeSets);
            this.tabScheduler.Controls.Add(this.lblFrequency);
            this.tabScheduler.Controls.Add(this.comboFrequency);
            this.tabScheduler.Controls.Add(this.startTimePicker);
            this.tabScheduler.Controls.Add(this.lblStartTime);
            this.tabScheduler.Controls.Add(this.lblStartDate);
            this.tabScheduler.Controls.Add(this.startDatePicker);
            this.tabScheduler.Location = new System.Drawing.Point(4, 22);
            this.tabScheduler.Name = "tabScheduler";
            this.tabScheduler.Size = new System.Drawing.Size(776, 487);
            this.tabScheduler.TabIndex = 4;
            this.tabScheduler.Text = "Scheduler";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(112, 115);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(128, 23);
            this.btnLoad.TabIndex = 19;
            this.btnLoad.Text = "Load Existing Schedule";
            this.btnLoad.UseVisualStyleBackColor = true;
            // 
            // btmSave
            // 
            this.btmSave.Location = new System.Drawing.Point(13, 115);
            this.btmSave.Name = "btmSave";
            this.btmSave.Size = new System.Drawing.Size(93, 23);
            this.btmSave.TabIndex = 18;
            this.btmSave.Text = "Save Schedule";
            this.btmSave.UseVisualStyleBackColor = true;
            // 
            // lblNodeSets
            // 
            this.lblNodeSets.AutoSize = true;
            this.lblNodeSets.Location = new System.Drawing.Point(30, 91);
            this.lblNodeSets.Name = "lblNodeSets";
            this.lblNodeSets.Size = new System.Drawing.Size(55, 13);
            this.lblNodeSets.TabIndex = 17;
            this.lblNodeSets.Text = "Node Set:";
            // 
            // comboNodeSets
            // 
            this.comboNodeSets.FormattingEnabled = true;
            this.comboNodeSets.Location = new System.Drawing.Point(91, 88);
            this.comboNodeSets.Name = "comboNodeSets";
            this.comboNodeSets.Size = new System.Drawing.Size(121, 21);
            this.comboNodeSets.TabIndex = 16;
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Location = new System.Drawing.Point(25, 64);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(60, 13);
            this.lblFrequency.TabIndex = 15;
            this.lblFrequency.Text = "Frequency:";
            // 
            // comboFrequency
            // 
            this.comboFrequency.FormattingEnabled = true;
            this.comboFrequency.Items.AddRange(new object[] {
            "Hourly",
            "Daily",
            "Weekly",
            "Monthly"});
            this.comboFrequency.Location = new System.Drawing.Point(91, 61);
            this.comboFrequency.Name = "comboFrequency";
            this.comboFrequency.Size = new System.Drawing.Size(121, 21);
            this.comboFrequency.TabIndex = 14;
            // 
            // startTimePicker
            // 
            this.startTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.startTimePicker.Location = new System.Drawing.Point(91, 35);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.ShowUpDown = true;
            this.startTimePicker.Size = new System.Drawing.Size(85, 20);
            this.startTimePicker.TabIndex = 13;
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(52, 41);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(33, 13);
            this.lblStartTime.TabIndex = 12;
            this.lblStartTime.Text = "Time:";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(13, 18);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 13);
            this.lblStartDate.TabIndex = 11;
            this.lblStartDate.Text = "Starting Date:";
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(91, 12);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(200, 20);
            this.startDatePicker.TabIndex = 10;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitEchoBackupToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitEchoBackupToolStripMenuItem
            // 
            this.exitEchoBackupToolStripMenuItem.Name = "exitEchoBackupToolStripMenuItem";
            this.exitEchoBackupToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.exitEchoBackupToolStripMenuItem.Text = "Exit Echo Backup";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // guid
            // 
            this.guid.HeaderText = "GUID";
            this.guid.Name = "guid";
            this.guid.Visible = false;
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            // 
            // ip
            // 
            this.ip.HeaderText = "Logical Address";
            this.ip.Name = "ip";
            // 
            // mac
            // 
            this.mac.HeaderText = "Pyshical Address";
            this.mac.Name = "mac";
            // 
            // reliablity
            // 
            this.reliablity.HeaderText = "Reliablity Metric";
            this.reliablity.Name = "reliablity";
            this.reliablity.Visible = false;
            // 
            // maxBackup
            // 
            this.maxBackup.HeaderText = "Max Backup Capacity";
            this.maxBackup.Name = "maxBackup";
            this.maxBackup.Visible = false;
            // 
            // totalCapacity
            // 
            this.totalCapacity.HeaderText = "Total Capacity";
            this.totalCapacity.Name = "totalCapacity";
            this.totalCapacity.Visible = false;
            // 
            // freeSpace
            // 
            this.freeSpace.HeaderText = "Avaliable Space";
            this.freeSpace.Name = "freeSpace";
            this.freeSpace.Visible = false;
            // 
            // nonBackup
            // 
            this.nonBackup.HeaderText = "Non-Backup Data";
            this.nonBackup.Name = "nonBackup";
            this.nonBackup.Visible = false;
            // 
            // backupData
            // 
            this.backupData.HeaderText = "Backup Data";
            this.backupData.Name = "backupData";
            this.backupData.Visible = false;
            // 
            // status
            // 
            this.status.HeaderText = "Block Status";
            this.status.Name = "status";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.pnlTabGUI);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Echo Backup";
            this.Load += new System.EventHandler(this.MainForm2_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlTabGUI.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabNodeSets.ResumeLayout(false);
            this.pnlNodeSetsFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNodeSets)).EndInit();
            this.pnlNodeSetsBottom.ResumeLayout(false);
            this.pnlNodeSetsBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnRedundancy)).EndInit();
            this.NodeGrid.ResumeLayout(false);
            this.NodeGrid.PerformLayout();
            this.tabDiskReport.ResumeLayout(false);
            this.tabDiskReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tabBackupStatus.ResumeLayout(false);
            this.pnlBackupStatusFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBackupStatus)).EndInit();
            this.pnlBackupStatusBottom.ResumeLayout(false);
            this.tabDataRecovery.ResumeLayout(false);
            this.pnlDataRecoveryFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDataRecovery)).EndInit();
            this.pnlDataRecoveryBottom.ResumeLayout(false);
            this.pnlDataRecoveryBottom.PerformLayout();
            this.pnlDataRecoveryTop.ResumeLayout(false);
            this.pnlDataRecoveryTop.PerformLayout();
            this.tabScheduler.ResumeLayout(false);
            this.tabScheduler.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Panel pnlTabGUI;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabNodeSets;
        private System.Windows.Forms.TabPage tabDiskReport;
        private System.Windows.Forms.Panel pnlNodeSetsFill;
        private System.Windows.Forms.Panel pnlNodeSetsBottom;
        private System.Windows.Forms.Panel NodeGrid;
        private System.Windows.Forms.Button btnRefreshList;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.DataGridView dataGridViewNodeSets;
        private System.Windows.Forms.Button btnLoadNodeSet;
        private System.Windows.Forms.Button btnSaveNodeSet;
        private System.Windows.Forms.CheckBox chkUtilizationOverride;
        private System.Windows.Forms.Label lblRedundancy;
        private System.Windows.Forms.NumericUpDown numUpDwnRedundancy;
        private System.Windows.Forms.Label gb4;
        private System.Windows.Forms.Label gb2;
        private System.Windows.Forms.Label gb3;
        private System.Windows.Forms.Label gb1;
        private System.Windows.Forms.Label bytes4;
        private System.Windows.Forms.Label bytes2;
        private System.Windows.Forms.Label bytes3;
        private System.Windows.Forms.Label bytes1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DiskList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TabPage tabBackupStatus;
        private System.Windows.Forms.Panel pnlBackupStatusFill;
        private System.Windows.Forms.Panel pnlBackupStatusTop;
        private System.Windows.Forms.Panel pnlBackupStatusBottom;
        private System.Windows.Forms.DataGridView dataGridViewBackupStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNodeSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastBackup;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNextBackup;
        private System.Windows.Forms.Button btnLogs;
        private System.Windows.Forms.Button btnDiskUsage;
        private System.Windows.Forms.Button btnBackupNow;
        private System.Windows.Forms.TabPage tabDataRecovery;
        private System.Windows.Forms.Panel pnlDataRecoveryTop;
        private System.Windows.Forms.Panel pnlDataRecoveryFill;
        private System.Windows.Forms.Panel pnlDataRecoveryBottom;
        private System.Windows.Forms.Button btnFileBrowser;
        private System.Windows.Forms.TextBox txtFileBrowser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnBrowseDestinations;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.DataGridView dataGridViewDataRecovery;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrigSize;
        private System.Windows.Forms.TabPage tabScheduler;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btmSave;
        private System.Windows.Forms.Label lblNodeSets;
        private System.Windows.Forms.ComboBox comboNodeSets;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.ComboBox comboFrequency;
        private System.Windows.Forms.DateTimePicker startTimePicker;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitEchoBackupToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn guid;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn mac;
        private System.Windows.Forms.DataGridViewTextBoxColumn reliablity;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxBackup;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalCapacity;
        private System.Windows.Forms.DataGridViewTextBoxColumn freeSpace;
        private System.Windows.Forms.DataGridViewTextBoxColumn nonBackup;
        private System.Windows.Forms.DataGridViewTextBoxColumn backupData;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
    }
}