using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace Backend 
{
    public partial class EchoBackupService : ServiceBase 
    {
        public EchoBackupService() 
        {
            this.ServiceName = "Echo Backup Service";
            InitializeComponent();
        }

        protected override void OnStart(string[] args) 
        {
            Logger.init();
            Logger.Log("Started Service");

            net = new Backend.Networker();
            messageThread = new Thread(new ThreadStart(net.processMessages));
            rxThread = new Thread(new ThreadStart(net.runReceiver));
            txThread = new Thread(new ThreadStart(net.runTransmitter));
            net.startTransmitter();
            txThread.Start();
            net.startReciever();
            messageThread.Start();
            rxThread.Start();

            MainLoop();
        }

        protected override void OnStop() 
        {
            net.setReceiverAlive(false);
            net.setTransmitterAlive(false);
            messageThread.Abort();
            rxThread.Abort();
            txThread.Abort();
            net.stopReciever();
            net.stopTransmitter();

            Logger.Log("Stopped Service");
            Logger.Close();
        }

        protected void MainLoop() 
        {
            while (true) 
            {

            }
        }

        static void Main() 
        {
            ServiceBase.Run(new EchoBackupService());
        }

        private Version version = new Version("0.1.0.0");
        private Thread messageThread;
        private Thread rxThread;
        private Thread txThread;
        private Backend.Networker net; 
    }
}
