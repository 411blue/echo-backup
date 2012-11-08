using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GUI_Frontend
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmNodeSets());
            //test comment. james 2012-10-03
            //another comment.
        }
    }
}
