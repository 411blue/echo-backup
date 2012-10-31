using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Backend {
    public partial class EchoBackupService : ServiceBase {
        private Version version = new Version("0.1.0.0");
        private Logger logger;

        public EchoBackupService() {
            this.ServiceName = "Echo Backup Service";
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            logger = new Logger();
            logger.Log("Started Service");
            MainLoop();
        }

        protected override void OnStop() {
            logger.Log("Stopped Service");
            logger.Close();
        }

        protected void MainLoop() {
            while (true) {

            }
        }

        static void Main() {
            ServiceBase.Run(new EchoBackupService());
        }
    }
}
