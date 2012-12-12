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
using Backend.Storage;

namespace Backend 
{
    public partial class EchoBackupService : ServiceBase 
    {
        private CommandServer commandServer;
        //this value should not change as long as the service running.
        //in fact, it should never change after it has been initialized with Guid.NewGuid()
        private Guid guid;
        private StorageThread storageThread;

        public EchoBackupService() 
        {
            //setup guid
            Logger.init();
            guid = Node.GetGuid();
            if (guid == Guid.Empty)
            {
                guid = Guid.NewGuid();
                Properties.Settings.Default.guid = guid;
                Properties.Settings.Default.Save();
            }
            if (Node.GetTemporaryDirectory() == "")
            {
                Node.SetTemporaryDirectory(Path.Combine(Node.ExecutableDir(), "temp"));
            }
            if (Node.GetBackupDirectory() == "")
            {
                Node.SetBackupDirectory(Path.Combine(Node.ExecutableDir(), "backups"));
            }
            Logger.Debug2("About to set service name");
            this.ServiceName = "Echo Backup Service";
            Logger.Debug2("set service name");
            InitializeComponent();
            Logger.Debug2("initialized component");
        }

        public void testStart()
        {
            string[] args = { };
            OnStart(args);
        }

        protected override void OnStart(string[] args) 
        {
            #region logging
            Logger.Log("Started Service");
            #endregion
            #region heartbeats
            net = new Backend.Networker();
            Logger.Debug2("created NetWorker");
            indexdi = new Backend.IndexDistribution();
            Logger.Debug2("created indexdi");
            indexdi.SetDistribute(true);
            Logger.Debug2("set distribute on indexdi");
            messageThread = new Thread(new ThreadStart(net.processMessages));
            Logger.Debug2("created messagethread");
            rxThread = new Thread(new ThreadStart(net.runReceiver));
            Logger.Debug2("created rxThread");
            txThread = new Thread(new ThreadStart(net.runTransmitter));
            Logger.Debug2("created txthread");
            distributionThread = new Thread(new ThreadStart(indexdi.BeginIndexDistribution));
            Logger.Debug2("created dithread");
            net.startTransmitter();
            Logger.Debug2("Started net transmitter");
            txThread.Start();
            Logger.Debug2("Started txthread");
            net.startReciever();
            Logger.Debug2("Started net receiver");
            messageThread.Start();
            Logger.Debug2("started messagethread");
            rxThread.Start();
            Logger.Debug2("Started rxThread");
            distributionThread.Start();
            Logger.Debug2("started dithread");
            #endregion
            #region networking
            commandServer = new CommandServer(guid);
            Logger.Debug2("started CommandServer");
            #endregion
            #region storage
            storageThread = new StorageThread(Node.GetTemporaryDirectory(), this.guid);
            Logger.Debug2("started StorageThread");
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
            Logger.Debug("EchoBackupService:MainLoop");
            while (true) 
            {
                Thread.Sleep(1000);
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
