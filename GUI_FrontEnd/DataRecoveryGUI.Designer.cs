namespace GUI_FrontEnd
{
    partial class DataRecoveryGUI
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileBrowser = new System.Windows.Forms.TextBox();
            this.btnFileBrowser = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrigSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBrowseDestinations = new System.Windows.Forms.Button();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.lblDestination = new System.Windows.Forms.Label();
            this.btnRestore = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a Directory/File:";
            // 
            // txtFileBrowser
            // 
            this.txtFileBrowser.Location = new System.Drawing.Point(126, 12);
            this.txtFileBrowser.Name = "txtFileBrowser";
            this.txtFileBrowser.Size = new System.Drawing.Size(199, 20);
            this.txtFileBrowser.TabIndex = 1;
            // 
            // btnFileBrowser
            // 
            this.btnFileBrowser.Location = new System.Drawing.Point(331, 10);
            this.btnFileBrowser.Name = "btnFileBrowser";
            this.btnFileBrowser.Size = new System.Drawing.Size(104, 23);
            this.btnFileBrowser.TabIndex = 2;
            this.btnFileBrowser.Text = "Browse Directories";
            this.btnFileBrowser.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colVersion,
            this.colDateTime,
            this.colOrigSize});
            this.dataGridView1.Location = new System.Drawing.Point(12, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(444, 155);
            this.dataGridView1.TabIndex = 3;
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
            this.colDateTime.Width = 200;
            // 
            // colOrigSize
            // 
            this.colOrigSize.HeaderText = "Original Size";
            this.colOrigSize.Name = "colOrigSize";
            // 
            // btnBrowseDestinations
            // 
            this.btnBrowseDestinations.Location = new System.Drawing.Point(328, 198);
            this.btnBrowseDestinations.Name = "btnBrowseDestinations";
            this.btnBrowseDestinations.Size = new System.Drawing.Size(104, 23);
            this.btnBrowseDestinations.TabIndex = 6;
            this.btnBrowseDestinations.Text = "Browse Directories";
            this.btnBrowseDestinations.UseVisualStyleBackColor = true;
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(123, 200);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(199, 20);
            this.txtDestination.TabIndex = 5;
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Location = new System.Drawing.Point(9, 203);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(108, 13);
            this.lblDestination.TabIndex = 4;
            this.lblDestination.Text = "Destination Directory:";
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(12, 245);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(79, 23);
            this.btnRestore.TabIndex = 7;
            this.btnRestore.Text = "Restore Now";
            this.btnRestore.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(97, 245);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(334, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 276);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnBrowseDestinations);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnFileBrowser);
            this.Controls.Add(this.txtFileBrowser);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Data Recovery";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileBrowser;
        private System.Windows.Forms.Button btnFileBrowser;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrigSize;
        private System.Windows.Forms.Button btnBrowseDestinations;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

