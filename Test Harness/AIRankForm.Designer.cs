namespace Test_Harness
{
    partial class AIRankForm
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
            this.lstNodes = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNetwork = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCPU = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtWorstNetwork = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWorstCPU = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWorstName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtUnitTests = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDiskUsage = new System.Windows.Forms.TextBox();
            this.txtWorstDiskUsage = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstNodes
            // 
            this.lstNodes.FormattingEnabled = true;
            this.lstNodes.Location = new System.Drawing.Point(16, 19);
            this.lstNodes.Name = "lstNodes";
            this.lstNodes.Size = new System.Drawing.Size(185, 238);
            this.lstNodes.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstNodes);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 278);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Priority";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDiskUsage);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtNetwork);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtCPU);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.lblName);
            this.groupBox2.Location = new System.Drawing.Point(12, 296);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(219, 132);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Optimal Node";
            // 
            // txtNetwork
            // 
            this.txtNetwork.Location = new System.Drawing.Point(87, 68);
            this.txtNetwork.Name = "txtNetwork";
            this.txtNetwork.ReadOnly = true;
            this.txtNetwork.Size = new System.Drawing.Size(114, 20);
            this.txtNetwork.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Network:";
            // 
            // txtCPU
            // 
            this.txtCPU.Location = new System.Drawing.Point(87, 42);
            this.txtCPU.Name = "txtCPU";
            this.txtCPU.ReadOnly = true;
            this.txtCPU.Size = new System.Drawing.Size(114, 20);
            this.txtCPU.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "CPU:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(87, 16);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(114, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(19, 19);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(41, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name: ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtWorstDiskUsage);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtWorstNetwork);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtWorstCPU);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtWorstName);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(12, 434);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(219, 132);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Worst Node";
            // 
            // txtWorstNetwork
            // 
            this.txtWorstNetwork.Location = new System.Drawing.Point(87, 68);
            this.txtWorstNetwork.Name = "txtWorstNetwork";
            this.txtWorstNetwork.ReadOnly = true;
            this.txtWorstNetwork.Size = new System.Drawing.Size(114, 20);
            this.txtWorstNetwork.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Network:";
            // 
            // txtWorstCPU
            // 
            this.txtWorstCPU.Location = new System.Drawing.Point(87, 42);
            this.txtWorstCPU.Name = "txtWorstCPU";
            this.txtWorstCPU.ReadOnly = true;
            this.txtWorstCPU.Size = new System.Drawing.Size(114, 20);
            this.txtWorstCPU.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "CPU:";
            // 
            // txtWorstName
            // 
            this.txtWorstName.Location = new System.Drawing.Point(87, 16);
            this.txtWorstName.Name = "txtWorstName";
            this.txtWorstName.ReadOnly = true;
            this.txtWorstName.Size = new System.Drawing.Size(114, 20);
            this.txtWorstName.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Name: ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtUnitTests);
            this.groupBox4.Location = new System.Drawing.Point(260, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(449, 549);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Unit Tests";
            // 
            // txtUnitTests
            // 
            this.txtUnitTests.Location = new System.Drawing.Point(16, 19);
            this.txtUnitTests.Multiline = true;
            this.txtUnitTests.Name = "txtUnitTests";
            this.txtUnitTests.Size = new System.Drawing.Size(413, 512);
            this.txtUnitTests.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Disk Usage";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Disk Usage";
            // 
            // txtDiskUsage
            // 
            this.txtDiskUsage.Location = new System.Drawing.Point(87, 97);
            this.txtDiskUsage.Name = "txtDiskUsage";
            this.txtDiskUsage.ReadOnly = true;
            this.txtDiskUsage.Size = new System.Drawing.Size(114, 20);
            this.txtDiskUsage.TabIndex = 7;
            // 
            // txtWorstDiskUsage
            // 
            this.txtWorstDiskUsage.Location = new System.Drawing.Point(87, 98);
            this.txtWorstDiskUsage.Name = "txtWorstDiskUsage";
            this.txtWorstDiskUsage.ReadOnly = true;
            this.txtWorstDiskUsage.Size = new System.Drawing.Size(114, 20);
            this.txtWorstDiskUsage.TabIndex = 8;
            // 
            // AIRankForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 584);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AIRankForm";
            this.Text = "AIRankForm";
            this.Load += new System.EventHandler(this.AIRankForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstNodes;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtNetwork;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCPU;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtWorstNetwork;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWorstCPU;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWorstName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtUnitTests;
        private System.Windows.Forms.TextBox txtDiskUsage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtWorstDiskUsage;
        private System.Windows.Forms.Label label7;
    }
}