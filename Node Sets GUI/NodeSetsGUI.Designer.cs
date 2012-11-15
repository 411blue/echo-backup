namespace GUI_Frontend
{
    partial class frmNodeSets
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
            this.lblListTitle = new System.Windows.Forms.Label();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.numUpDwnRedundancy = new System.Windows.Forms.NumericUpDown();
            this.lblRedundancy = new System.Windows.Forms.Label();
            this.chkUtilizationOverride = new System.Windows.Forms.CheckBox();
            this.btnSaveNodeSet = new System.Windows.Forms.Button();
            this.btnLoadNodeSet = new System.Windows.Forms.Button();
            this.colMacAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReliabilityMetric = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUtilizationPercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAvailableSpace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnRedundancy)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMacAddress,
            this.colDeviceName,
            this.colReliabilityMetric,
            this.colUtilizationPercentage,
            this.colAvailableSpace,
            this.colDescription});
            this.dataGridView1.Location = new System.Drawing.Point(12, 33);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(529, 155);
            this.dataGridView1.TabIndex = 0;
            // 
            // lblListTitle
            // 
            this.lblListTitle.AutoSize = true;
            this.lblListTitle.Location = new System.Drawing.Point(12, 9);
            this.lblListTitle.Name = "lblListTitle";
            this.lblListTitle.Size = new System.Drawing.Size(98, 13);
            this.lblListTitle.TabIndex = 1;
            this.lblListTitle.Text = "Discovered Nodes:";
            // 
            // btnRefreshList
            // 
            this.btnRefreshList.Location = new System.Drawing.Point(466, 4);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshList.TabIndex = 2;
            this.btnRefreshList.Text = "Refresh List";
            this.btnRefreshList.UseVisualStyleBackColor = true;
            // 
            // numUpDwnRedundancy
            // 
            this.numUpDwnRedundancy.Location = new System.Drawing.Point(178, 193);
            this.numUpDwnRedundancy.Name = "numUpDwnRedundancy";
            this.numUpDwnRedundancy.Size = new System.Drawing.Size(61, 20);
            this.numUpDwnRedundancy.TabIndex = 3;
            // 
            // lblRedundancy
            // 
            this.lblRedundancy.AutoSize = true;
            this.lblRedundancy.Location = new System.Drawing.Point(12, 194);
            this.lblRedundancy.Name = "lblRedundancy";
            this.lblRedundancy.Size = new System.Drawing.Size(160, 13);
            this.lblRedundancy.TabIndex = 4;
            this.lblRedundancy.Text = "Number of Redundant Backups:";
            // 
            // chkUtilizationOverride
            // 
            this.chkUtilizationOverride.AutoSize = true;
            this.chkUtilizationOverride.Location = new System.Drawing.Point(341, 193);
            this.chkUtilizationOverride.Name = "chkUtilizationOverride";
            this.chkUtilizationOverride.Size = new System.Drawing.Size(200, 17);
            this.chkUtilizationOverride.TabIndex = 5;
            this.chkUtilizationOverride.Text = "Allow Utilization Percentage Override";
            this.chkUtilizationOverride.UseVisualStyleBackColor = true;
            // 
            // btnSaveNodeSet
            // 
            this.btnSaveNodeSet.Location = new System.Drawing.Point(12, 230);
            this.btnSaveNodeSet.Name = "btnSaveNodeSet";
            this.btnSaveNodeSet.Size = new System.Drawing.Size(92, 23);
            this.btnSaveNodeSet.TabIndex = 6;
            this.btnSaveNodeSet.Text = "Save Node Set";
            this.btnSaveNodeSet.UseVisualStyleBackColor = true;
            // 
            // btnLoadNodeSet
            // 
            this.btnLoadNodeSet.Location = new System.Drawing.Point(110, 230);
            this.btnLoadNodeSet.Name = "btnLoadNodeSet";
            this.btnLoadNodeSet.Size = new System.Drawing.Size(136, 23);
            this.btnLoadNodeSet.TabIndex = 7;
            this.btnLoadNodeSet.Text = "Load Existing Node Set";
            this.btnLoadNodeSet.UseVisualStyleBackColor = true;
            // 
            // colMacAddress
            // 
            this.colMacAddress.HeaderText = "Mac Address";
            this.colMacAddress.Name = "colMacAddress";
            // 
            // colDeviceName
            // 
            this.colDeviceName.HeaderText = "Device Name";
            this.colDeviceName.Name = "colDeviceName";
            // 
            // colReliabilityMetric
            // 
            this.colReliabilityMetric.HeaderText = "Reliability Metric";
            this.colReliabilityMetric.Name = "colReliabilityMetric";
            this.colReliabilityMetric.Width = 60;
            // 
            // colUtilizationPercentage
            // 
            this.colUtilizationPercentage.HeaderText = "Utilization Percentage";
            this.colUtilizationPercentage.Name = "colUtilizationPercentage";
            this.colUtilizationPercentage.Width = 65;
            // 
            // colAvailableSpace
            // 
            this.colAvailableSpace.HeaderText = "Available Space";
            this.colAvailableSpace.Name = "colAvailableSpace";
            this.colAvailableSpace.Width = 60;
            // 
            // colDescription
            // 
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            // 
            // frmNodeSets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 263);
            this.Controls.Add(this.btnLoadNodeSet);
            this.Controls.Add(this.btnSaveNodeSet);
            this.Controls.Add(this.chkUtilizationOverride);
            this.Controls.Add(this.lblRedundancy);
            this.Controls.Add(this.numUpDwnRedundancy);
            this.Controls.Add(this.btnRefreshList);
            this.Controls.Add(this.lblListTitle);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmNodeSets";
            this.Text = "Node Sets";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDwnRedundancy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMacAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeviceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReliabilityMetric;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUtilizationPercentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAvailableSpace;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.Button btnRefreshList;
        private System.Windows.Forms.NumericUpDown numUpDwnRedundancy;
        private System.Windows.Forms.Label lblRedundancy;
        private System.Windows.Forms.CheckBox chkUtilizationOverride;
        private System.Windows.Forms.Button btnSaveNodeSet;
        private System.Windows.Forms.Button btnLoadNodeSet;

    }
}

