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
    public partial class DiskReport : Form
    {
        public DiskReport(DataGridView dgv1)
        {
            InitializeComponent();
            hosts = new DataGridView();

            dgv1 = hosts;

            if (hosts.RowCount > 1)
            {
                for (int i = 0; i < hosts.Rows.Count - 1; ++i)
                {
                    hostList.Items.Add(hosts.Rows[i].Cells[0].Value);
                }
            }
        }

        public void lookUpBytes(string h)
        {
            for (int i = 0; i < hosts.Rows.Count - 1; ++i)
            {
                if (h == Convert.ToString(hosts.Rows[i].Cells[0].Value))
                {
                    draw(long.Parse(Convert.ToString(hosts.Rows[i].Cells[1].Value)), long.Parse(Convert.ToString(hosts.Rows[i].Cells[2].Value)),
                        long.Parse(Convert.ToString(hosts.Rows[i].Cells[3].Value)), long.Parse(Convert.ToString(hosts.Rows[i].Cells[4].Value)));
                    break;
                }
            }
        }

        public void draw(long capacity, long free, long nonBackupData, long backupData)
        {
            float total = capacity;
            float deg1 = (free / total) * 360;
            float deg2 = (nonBackupData / total) * 360;
            float deg3 = (backupData / total) * 360;

            Pen p = new Pen(Color.Black, 2);
            Rectangle rec = new Rectangle(163, 12, 150, 150);
            Brush b1 = new SolidBrush(Color.Blue);
            Brush b2 = new SolidBrush(Color.Red);
            Brush b3 = new SolidBrush(Color.Orange);

            Graphics g = CreateGraphics();
            g.Clear(DiskReport.DefaultBackColor);
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

        public void hostList_TextChanged(object sender, EventArgs e)
        {
            lookUpBytes(hostList.Text);
        }

        private DataGridView hosts;
    }
}
