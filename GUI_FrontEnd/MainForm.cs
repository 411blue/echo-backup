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

        /// <summary>
        /// Returns true if the main form is already displaying the
        /// form that is sent as the parameter.
        /// </summary>
        /// <param name="form">The form to check for duplicates.</param>
        /// <returns>True if the form is already displaying.</returns>
        private bool FormAlreadyShowing(Form form)
        {
            foreach (Form child in this.MdiChildren)
            {
                if (child.Equals(form))
                {
                    return true;
                }
            }
            return false;
        }

        private void backupStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //make sure it's not already showing
            bool showing = false;
            foreach (Form form in this.MdiChildren)
            {
                if (form is BackupStatusGUI)
                {
                    showing = true;
                }
            }
            if (!showing)
            {
                GUI_FrontEnd.BackupStatusGUI gui = new GUI_FrontEnd.BackupStatusGUI(this);
                gui.MdiParent = this;
                gui.Show();
            }
        }

        private void dataRecoveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //make sure it's not already showing
            bool showing = false;
            foreach (Form form in this.MdiChildren)
            {
                if (form is DataRecoveryGUI)
                {
                    showing = true;
                }
            }
            if (!showing)
            {
                GUI_FrontEnd.DataRecoveryGUI gui = new GUI_FrontEnd.DataRecoveryGUI(this);
                gui.MdiParent = this;
                gui.Show();
            }
        }

        private void nodeSetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //make sure it's not already showing
            bool showing = false;
            foreach (Form form in this.MdiChildren)
            {
                if (form is NodeSetsGUI)
                {
                    showing = true;
                }
            }
            if (!showing)
            {
                GUI_FrontEnd.NodeSetsGUI gui = new GUI_FrontEnd.NodeSetsGUI(this);
                gui.MdiParent = this;
                gui.Show();
            }
        }

        private void schedulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //make sure it's not already showing
            bool showing = false;
            foreach (Form form in this.MdiChildren)
            {
                if (form is SchedulerGUI)
                {
                    showing = true;
                }
            }
            if (!showing)
            {
                GUI_FrontEnd.SchedulerGUI gui = new GUI_FrontEnd.SchedulerGUI(this);
                gui.MdiParent = this;
                gui.Show();
            }
        }
    }
}
