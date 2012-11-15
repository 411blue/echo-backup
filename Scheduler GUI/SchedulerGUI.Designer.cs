namespace Scheduler_GUI
{
    partial class SchedulerGUI
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
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.comboFrequency = new System.Windows.Forms.ComboBox();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.comboNodeSets = new System.Windows.Forms.ComboBox();
            this.lblNodeSets = new System.Windows.Forms.Label();
            this.btmSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startDatePicker
            // 
            this.startDatePicker.Location = new System.Drawing.Point(90, 16);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(200, 20);
            this.startDatePicker.TabIndex = 0;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(12, 22);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(72, 13);
            this.lblStartDate.TabIndex = 1;
            this.lblStartDate.Text = "Starting Date:";
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(51, 45);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(33, 13);
            this.lblStartTime.TabIndex = 2;
            this.lblStartTime.Text = "Time:";
            // 
            // startTimePicker
            // 
            this.startTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.startTimePicker.Location = new System.Drawing.Point(90, 39);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.ShowUpDown = true;
            this.startTimePicker.Size = new System.Drawing.Size(85, 20);
            this.startTimePicker.TabIndex = 3;
            // 
            // comboFrequency
            // 
            this.comboFrequency.FormattingEnabled = true;
            this.comboFrequency.Items.AddRange(new object[] {
            "Hourly",
            "Daily",
            "Weekly",
            "Monthly"});
            this.comboFrequency.Location = new System.Drawing.Point(90, 65);
            this.comboFrequency.Name = "comboFrequency";
            this.comboFrequency.Size = new System.Drawing.Size(121, 21);
            this.comboFrequency.TabIndex = 4;
            // 
            // lblFrequency
            // 
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Location = new System.Drawing.Point(24, 68);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(60, 13);
            this.lblFrequency.TabIndex = 5;
            this.lblFrequency.Text = "Frequency:";
            // 
            // comboNodeSets
            // 
            this.comboNodeSets.FormattingEnabled = true;
            this.comboNodeSets.Location = new System.Drawing.Point(90, 92);
            this.comboNodeSets.Name = "comboNodeSets";
            this.comboNodeSets.Size = new System.Drawing.Size(121, 21);
            this.comboNodeSets.TabIndex = 6;
            // 
            // lblNodeSets
            // 
            this.lblNodeSets.AutoSize = true;
            this.lblNodeSets.Location = new System.Drawing.Point(29, 95);
            this.lblNodeSets.Name = "lblNodeSets";
            this.lblNodeSets.Size = new System.Drawing.Size(55, 13);
            this.lblNodeSets.TabIndex = 7;
            this.lblNodeSets.Text = "Node Set:";
            // 
            // btmSave
            // 
            this.btmSave.Location = new System.Drawing.Point(12, 119);
            this.btmSave.Name = "btmSave";
            this.btmSave.Size = new System.Drawing.Size(93, 23);
            this.btmSave.TabIndex = 8;
            this.btmSave.Text = "Save Schedule";
            this.btmSave.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(111, 119);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(128, 23);
            this.btnLoad.TabIndex = 9;
            this.btnLoad.Text = "Load Existing Schedule";
            this.btnLoad.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 158);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btmSave);
            this.Controls.Add(this.lblNodeSets);
            this.Controls.Add(this.comboNodeSets);
            this.Controls.Add(this.lblFrequency);
            this.Controls.Add(this.comboFrequency);
            this.Controls.Add(this.startTimePicker);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.startDatePicker);
            this.Name = "Form1";
            this.Text = "Scheduler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.DateTimePicker startTimePicker;
        private System.Windows.Forms.ComboBox comboFrequency;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.ComboBox comboNodeSets;
        private System.Windows.Forms.Label lblNodeSets;
        private System.Windows.Forms.Button btmSave;
        private System.Windows.Forms.Button btnLoad;
    }
}

