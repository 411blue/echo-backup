using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SQLite;
using Backend.Database;
using Backend.Properties;

namespace GUI_FrontEnd
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            numUpDownMaxBackupCapacity.Value = Settings.Default.maxBackupCapacity;
            db = new NodeDatabase();
            
            dataGridViewNodeSets.Rows.Add("936DA01F-9ABD-4d9d-80C7-02AF85C822A8", "PC1", "192.168.1.1", "00-21-70-FE-23-EF", "1", "51", "89", "0", "100","yes");
            dataGridViewNodeSets.Rows.Add("936DA01F-9ABD-4d9d-80C7-02AF85C822A8", "PC2", "192.168.1.2", "00-21-69-FE-23-AB", "32", "50", "88", "25", "75", "yes");
            dataGridViewNodeSets.Rows.Add("936DA01F-9ABD-4d9d-80C7-02AF85C822AC", "PC3", "192.168.1.3", "00-21-69-FE-23-AC", "62", "49", "87", "50", "50", "no");
             
        }

        #region DiskReportTabStuff
        private void InitializeDiskReportTab()
        {
            DiskList.Items.AddRange(db.GetNodeNames(db.ConnectToDatabase(@"C\nodes.db")));
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
            draw(1, 1, 1, 1);
        }

        #endregion

        private void maxBackupSupport_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.maxBackupCapacity = numUpDownMaxBackupCapacity.Value;
        }    

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 1)
            {
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

        private void btnBackupFiles_Click(object sender, EventArgs e)
        {
            DialogResult result = openBackupFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string backupFilePath = openBackupFileDialog.FileName;
                try
                {
                    txtBackupFiles.Text = backupFilePath;
                }
                catch (System.IO.IOException)
                {
                }
            }
        }

        private void btnAddBackupFile_Click(object sender, EventArgs e)
        {
            string[] rowArray = new string[] {txtBackupFiles.Text};
            dataGridViewBackupFiles.Rows.Add(rowArray[0]);
        }

        private void btnBackupDirectory_Click(object sender, EventArgs e)
        {
            DialogResult result = LocalBackupDirectoryBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string backupFilePath = LocalBackupDirectoryBrowserDialog.SelectedPath;
                try
                {
                    txtBackupDirectory.Text = backupFilePath;
                }
                catch (System.IO.IOException)
                {
                }
            }
        }

        private void btnSetBackupDirectory_Click(object sender, EventArgs e)
        {
            Settings.Default.localBackupPath = txtBackupDirectory.Text;
            MessageBox.Show("Local Backup Directory Saved.");
        }

        private void btnRefreshList_Click(object sender, EventArgs e)
        {
            string sql = "select * from Nodes";
            string sqliteDBFile = @"C:\Users\Tom\Desktop\nodes.db";
            cnn = db.ConnectToDatabase(sqliteDBFile);
            try
            {
                
                cnn.Open();
                
                SQLiteCommand myCommand = new SQLiteCommand(sql, cnn);
                SQLiteDataReader reader = myCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                string text = "";
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        text += row[i] + ", ";
                    }
                    text += "\n";

                    string[] rowArray = new string[] {row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[9].ToString(), row[10].ToString(), 
                                                        row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString()};
                    dataGridViewNodeSets.Rows.Add(rowArray[0]);

                    numUpDownMaxBackupCapacity.Value = decimal.Parse(row[4].ToString());
                }
                Console.WriteLine(text);
            }
            catch (Exception error)
            {
                Console.WriteLine("Caught exception: " + error.Message);
            }
            finally
            {
                if (cnn != null)
                {
                    cnn.Close();
                }
            }
        }

        private void btnRecoveryFileBrowser_Click(object sender, EventArgs e)
        {
            DialogResult result = openRecoveryFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string recoveryFilePath = openRecoveryFileDialog.FileName;
                try
                {
                    txtRecoveryFileBrowser.Text = recoveryFilePath;

                    string sqliteDBFile = @"C:\Users\Tom\Desktop\index.db";
                    cnn = db.ConnectToDatabase(sqliteDBFile);
                    
                    string query = "SELECT date_of_backup, size FROM Backup_Indexes WHERE source_path = @pSourcePath";
                    SQLiteCommand cmd = new SQLiteCommand(query, cnn);

                    //create a parameter for sourceGUID
                    SQLiteParameter pSourceGUID = new SQLiteParameter("@pSourcePath", recoveryFilePath);

                    cmd.Parameters.Add(pSourceGUID);
                    try
                    {
                        cnn.Open();

                        SQLiteCommand myCommand = new SQLiteCommand(query, cnn);
                        SQLiteDataReader reader = myCommand.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        string text = "";
                        foreach (DataRow row in dt.Rows)
                        {
                            for (int i = 0; i < 15; i++)
                            {
                                text += row[i] + ", ";
                            }
                            text += "\n";

                            string[] rowArray = new string[] {row[0].ToString(), row[1].ToString()};
                            dataGridViewDataRecovery.Rows.Add(rowArray[0]);
                        }
                        Console.WriteLine(text);
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine("Caught exception: " + error.Message);
                    }
                    finally
                    {
                        if (cnn != null)
                        {
                            cnn.Close();
                        }
                    }
                }
                catch (System.IO.IOException)
                {
                }
            }
        }

        private void btnRecoveryBrowseDestinations_Click(object sender, EventArgs e)
        {
            DialogResult result = RecoveryDestinationBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string recoveryDestination = RecoveryDestinationBrowserDialog.SelectedPath;
                try
                {
                    txtRecoveryDestination.Text = recoveryDestination;
                }
                catch (System.IO.IOException)
                {
                }
            }
        }

        //Placeholder for Jame's code to backup files
        private void btnBackupNow_Click(object sender, EventArgs e)
        {
            //Collects all filepaths into an array
            string[] rowArray = new string[dataGridViewBackupFiles.Rows.Count];
            for (int i = 0; i < (dataGridViewBackupFiles.Rows.Count - 1); i++)
            {
                rowArray[i] = dataGridViewBackupFiles.Rows[i].Cells[0].Value.ToString();
            }
        }

        //Placeholder for Jame's code to restore files
        private void btnRestore_Click(object sender, EventArgs e)
        {
            //Retrieves data about selected recovery file, stores in object {date/time, original size}
            string[] recoveryFile = new string[]{dataGridViewDataRecovery.SelectedRows[0].Cells[0].Value.ToString(), dataGridViewDataRecovery.SelectedRows[0].Cells[1].Value.ToString()};

            string recoveryDestination = txtRecoveryDestination.Text;
        }

        private NodeDatabase db;
        private SQLiteConnection cnn;
    }
}
