﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace GUI_FrontEnd
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            numUpDownMaxBackupCapacity.Value = Properties.Settings.Default.maxBackupCapacity;
        }

        #region DiskReportTabStuff
        private void InitializeDiskReportTab()
        {
            if (dataGridViewNodeSets.RowCount > 1)
            {
                for (int i = 0; i < dataGridViewNodeSets.Rows.Count - 1; ++i)
                {
                    DiskList.Items.Add(dataGridViewNodeSets.Rows[i].Cells[1].Value);
                }
            }
        }

        public void lookUpBytes(string h)
        {
            for (int i = 0; i < dataGridViewNodeSets.Rows.Count - 1; ++i)
            {
                if (h == Convert.ToString(dataGridViewNodeSets.Rows[i].Cells[1].Value))
                {
                    draw(long.Parse(Convert.ToString(dataGridViewNodeSets.Rows[i].Cells[6].Value)), long.Parse(Convert.ToString(dataGridViewNodeSets.Rows[i].Cells[7].Value)),
                        long.Parse(Convert.ToString(dataGridViewNodeSets.Rows[i].Cells[8].Value)), long.Parse(Convert.ToString(dataGridViewNodeSets.Rows[i].Cells[9].Value)));
                    break;
                }
            }
        }

        public void draw(long backupData, long nonBackupData, long free, long capacity)
        {
            float total = capacity;
            float deg1 = (free / total) * 360;
            float deg2 = (nonBackupData / total) * 360;
            float deg3 = (backupData / total) * 360;

            Pen p = new Pen(Color.Black, 2);
            
            Rectangle rec = new Rectangle(169, 27, 150, 150);
            Brush b1 = new SolidBrush(Color.Blue);
            Brush b2 = new SolidBrush(Color.Red);
            Brush b3 = new SolidBrush(Color.Orange);

            Graphics g = CreateGraphics();
            //g.Clear(DiskReport.DefaultBackColor);
            g.Clear(SystemColors.Control);
            g.DrawPie(p, rec, 0, deg1);
            g.FillPie(b1, rec, 0, deg1);
            g.DrawPie(p, rec, deg1, deg2);
            g.FillPie(b2, rec, deg1, deg2);
            g.DrawPie(p, rec, deg2 + deg1, deg3);
            g.FillPie(b3, rec, deg2 + deg1, deg3);

            bytes1.Text = Convert.ToString(capacity);
            bytes2.Text = Convert.ToString(free);
            bytes3.Text = Convert.ToString(nonBackupData);
            bytes4.Text = Convert.ToString(backupData);
            gb1.Text = Convert.ToString(capacity / Convert.ToDouble(1073741824));
            gb2.Text = Convert.ToString(free / Convert.ToDouble(1073741824));
            gb3.Text = Convert.ToString((nonBackupData) / Convert.ToDouble(1073741824));
            gb4.Text = Convert.ToString(backupData / Convert.ToDouble(1073741824));
        }

        private void hostList_TextChanged(object sender, EventArgs e)
        {
            lookUpBytes(DiskList.Text);
        }

        #endregion

        private void maxBackupSupport_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.maxBackupCapacity = numUpDownMaxBackupCapacity.Value;
        }    

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 1)
            {
                //initialize the disk report tab
                InitializeDiskReportTab();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void btnSaveSchedule_Click(object sender, EventArgs e)
        {
            saveScheduleDialog.ShowDialog();
        }

        private void btnLoadSchedule_Click(object sender, EventArgs e)
        {
            string text = "";
            DialogResult result = openScheduleDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string schedulePath = openScheduleDialog.FileName;
                try
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(schedulePath))
                    {
                        text = sr.ReadLine();
                        startDatePicker.Value = Convert.ToDateTime(text);
                        Console.WriteLine(text);
                        text = sr.ReadLine();
                        startTimePicker.Value = Convert.ToDateTime(text);
                        Console.WriteLine(text);
                        text = sr.ReadLine();
                        comboFrequency.SelectedItem = text;
                        Console.WriteLine(text);
                        text = sr.ReadLine();
                        comboNodeSets.SelectedItem = text;
                        Console.WriteLine(text);
                    }
                }
                catch (System.IO.IOException)
                {
                }
            }
        }

        private void saveScheduleDialog_FileOk(object sender, CancelEventArgs e)
        {
            string schedulePath = saveScheduleDialog.FileName;

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(schedulePath, true))
            {
                file.WriteLine(startDatePicker.Value.ToString("d"));
                file.WriteLine(startTimePicker.Value.ToString("T"));
                file.WriteLine(comboFrequency.SelectedItem);
                file.WriteLine(comboNodeSets.SelectedItem);
            } 
        } 
    }
}
