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
            this.menuStripMainForm = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitEchoBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartEchoBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataRecoveryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeSetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schedulerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMainForm = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStripMainForm.SuspendLayout();
            this.statusStripMainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMainForm
            // 
            this.menuStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStripMainForm.Location = new System.Drawing.Point(0, 0);
            this.menuStripMainForm.Name = "menuStripMainForm";
            this.menuStripMainForm.Size = new System.Drawing.Size(784, 24);
            this.menuStripMainForm.TabIndex = 1;
            this.menuStripMainForm.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitEchoBackupToolStripMenuItem,
            this.restartEchoBackupToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitEchoBackupToolStripMenuItem
            // 
            this.exitEchoBackupToolStripMenuItem.Name = "exitEchoBackupToolStripMenuItem";
            this.exitEchoBackupToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.exitEchoBackupToolStripMenuItem.Text = "Exit Echo Backup";
            // 
            // restartEchoBackupToolStripMenuItem
            // 
            this.restartEchoBackupToolStripMenuItem.Name = "restartEchoBackupToolStripMenuItem";
            this.restartEchoBackupToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.restartEchoBackupToolStripMenuItem.Text = "Restart Echo Backup";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupStatusToolStripMenuItem,
            this.dataRecoveryToolStripMenuItem,
            this.nodeSetsToolStripMenuItem,
            this.schedulerToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // backupStatusToolStripMenuItem
            // 
            this.backupStatusToolStripMenuItem.Name = "backupStatusToolStripMenuItem";
            this.backupStatusToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.backupStatusToolStripMenuItem.Text = "Backup Status";
            this.backupStatusToolStripMenuItem.Click += new System.EventHandler(this.backupStatusToolStripMenuItem_Click);
            // 
            // dataRecoveryToolStripMenuItem
            // 
            this.dataRecoveryToolStripMenuItem.Name = "dataRecoveryToolStripMenuItem";
            this.dataRecoveryToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dataRecoveryToolStripMenuItem.Text = "Data Recovery";
            // 
            // nodeSetsToolStripMenuItem
            // 
            this.nodeSetsToolStripMenuItem.Name = "nodeSetsToolStripMenuItem";
            this.nodeSetsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.nodeSetsToolStripMenuItem.Text = "Node Sets";
            // 
            // schedulerToolStripMenuItem
            // 
            this.schedulerToolStripMenuItem.Name = "schedulerToolStripMenuItem";
            this.schedulerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.schedulerToolStripMenuItem.Text = "Scheduler";
            // 
            // statusStripMainForm
            // 
            this.statusStripMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStripMainForm.Location = new System.Drawing.Point(0, 540);
            this.statusStripMainForm.Name = "statusStripMainForm";
            this.statusStripMainForm.Size = new System.Drawing.Size(784, 22);
            this.statusStripMainForm.TabIndex = 2;
            this.statusStripMainForm.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(118, 17);
            this.lblStatus.Text = "toolStripStatusLabel1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.statusStripMainForm);
            this.Controls.Add(this.menuStripMainForm);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStripMainForm;
            this.Name = "MainForm";
            this.Text = "Echo Backup";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStripMainForm.ResumeLayout(false);
            this.menuStripMainForm.PerformLayout();
            this.statusStripMainForm.ResumeLayout(false);
            this.statusStripMainForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMainForm;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitEchoBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartEchoBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataRecoveryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nodeSetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schedulerToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStripMainForm;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}

