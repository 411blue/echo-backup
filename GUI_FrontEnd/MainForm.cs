using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI_FrontEnd
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Welcome to Echo Backup.";
        }

        private void backupStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //make sure it's not already showing
            bool showing = false;
            foreach (Control ctl in this.Controls)
            {
                if (ctl is BackupStatusGUI)
                {
                    showing = true;
                }
            }
            if (!showing)
            {
                GUI_FrontEnd.BackupStatusGUI bsgui = new GUI_FrontEnd.BackupStatusGUI(this);
                bsgui.MdiParent = this;
                
                bsgui.Show();
            }
        }
    }
}
