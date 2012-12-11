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
        private CommandServer commandServer;
        //this value should not change as long as the service running.
        //in fact, it should never change after it has been initialized with Guid.NewGuid()
        private Guid guid;
        public EchoBackupService() 
        {
            //setup guid
            guid = Node.GetGuid();
            if (guid == Guid.Empty)
            {
                guid = Guid.NewGuid();
                Properties.Settings.Default.guid = guid;
                Properties.Settings.Default.Save();
            }
            this.ServiceName = "Echo Backup Service";
            InitializeComponent();
        }

        protected override void OnStart(string[] args) 
        {
            #region logging
            Logger.init();
            Logger.Log("Started Service");
            #endregion
            #region heartbeats
            net = new Backend.Networker();
            indexdi = new Backend.IndexDistribution();
            indexdi.SetDistribute(true);
            messageThread = new Thread(new ThreadStart(net.processMessages));
            rxThread = new Thread(new ThreadStart(net.runReceiver));
            txThread = new Thread(new ThreadStart(net.runTransmitter));
            distributionThread = new Thread(new ThreadStart(indexdi.BeginIndexDistribution));
            net.startTransmitter();
            txThread.Start();
            net.startReciever();
            messageThread.Start();
            rxThread.Start();
            distributionThread.Start();
            #endregion
            #region networking
            commandServer = new CommandServer(guid);
            #endregion
            #region storage
            #endregion

            MainLoop();
        }

        protected override void OnStop() 
        {
            net.setReceiverAlive(false);
            net.setTransmitterAlive(false);
            indexdi.SetDistribute(false);
            messageThread.Abort();
            rxThread.Abort();
            txThread.Abort();
            distributionThread.Abort();
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
        private Thread distributionThread;
        private Backend.Networker net;
        private Backend.IndexDistribution indexdi;
    }
}
