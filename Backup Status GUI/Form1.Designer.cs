namespace Backup_Status
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colNodeSet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastBackup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNextBackup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBackupNow = new System.Windows.Forms.Button();
            this.btnDiskUsage = new System.Windows.Forms.Button();
            this.btnLogs = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNodeSet,
            this.colLastBackup,
            this.colNextBackup});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(544, 155);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
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
            this.colLastBackup.Width = 200;
            // 
            // colNextBackup
            // 
            this.colNextBackup.HeaderText = "Next Scheduled Backup";
            this.colNextBackup.Name = "colNextBackup";
            this.colNextBackup.Width = 200;
            // 
            // btnBackupNow
            // 
            this.btnBackupNow.Location = new System.Drawing.Point(12, 173);
            this.btnBackupNow.Name = "btnBackupNow";
            this.btnBackupNow.Size = new System.Drawing.Size(77, 23);
            this.btnBackupNow.TabIndex = 2;
            this.btnBackupNow.Text = "Backup Now";
            this.btnBackupNow.UseVisualStyleBackColor = true;
            // 
            // btnDiskUsage
            // 
            this.btnDiskUsage.Location = new System.Drawing.Point(176, 173);
            this.btnDiskUsage.Name = "btnDiskUsage";
            this.btnDiskUsage.Size = new System.Drawing.Size(146, 23);
            this.btnDiskUsage.TabIndex = 3;
            this.btnDiskUsage.Text = "Launch Disk Usage Report";
            this.btnDiskUsage.UseVisualStyleBackColor = true;
            // 
            // btnLogs
            // 
            this.btnLogs.Location = new System.Drawing.Point(95, 173);
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(75, 23);
            this.btnLogs.TabIndex = 4;
            this.btnLogs.Text = "View Logs";
            this.btnLogs.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 205);
            this.Controls.Add(this.btnLogs);
            this.Controls.Add(this.btnDiskUsage);
            this.Controls.Add(this.btnBackupNow);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Backup Status";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNodeSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastBackup;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNextBackup;
        private System.Windows.Forms.Button btnBackupNow;
        private System.Windows.Forms.Button btnDiskUsage;
        private System.Windows.Forms.Button btnLogs;
    }
}

