﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GUI_FrontEnd
{
    public partial class SchedulerGUI : Form
    {
        //pass a reference to the MainForm so we
        //can get any data it holds
        private MainForm mainForm;

        public SchedulerGUI(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
    }
}
