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
using Backend.Database;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Backend 
{
    [ServiceContract(Namespace = "http://Backend")]
    public interface EBInterface
    {
        [OperationContract]
        void StartBackup(List<string> directories);
        [OperationContract]
        void StartRecovery(long fileID, Guid sourceNode, string outputPath);
        [OperationContract]
        string CheckRecovery(/*long fileID, Guid sourceNode*/);
    }

    public partial class EchoBackupService : ServiceBase, EBInterface
    {
        private CommandServer commandServer;
        //this value should not change as long as the service running.
        //in fact, it should never change after it has been initialized with Guid.NewGuid()
        private Guid guid;
        private StorageThread storageThread;
        private ServiceHost servicehost;
        private Version version = new Version("0.1.0.0");
        private Thread messageThread;
        private Thread rxThread;
        private Thread txThread;
        private Thread distributionThread;
        private Backend.Networker net;
        private Backend.IndexDistribution indexdi;
        private Queue<RecoverResult> recoverResults;
        private List<ClientThread> clientThreads;

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
            recoverResults = new Queue<RecoverResult>();
            clientThreads = new List<ClientThread>();
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

        protected void setupWCF()
        {
            Uri baseAddress = new Uri("http://localhost:8765/Backend/Service");
            servicehost = new ServiceHost(typeof(EchoBackupService), baseAddress);
            try
            {
                servicehost.AddServiceEndpoint(typeof(EBInterface), new WSHttpBinding(), "EchobackupService");
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                servicehost.Description.Behaviors.Add(smb);
                servicehost.Open();
                Logger.Info("EchobackupService:setupWCF Successfully initialized WCF.");
            }
            catch
            {
                Logger.Error("EchobackupService:setupWCF An exception occured when initializing the WCF service stuff.");
            }
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

            stopNetworking();
            storageThread.RequestStop();

            Logger.Log("Stopped Service");
            Logger.Close();
        }
        protected void stopNetworking()
        {
            commandServer.Stop();
            lock (clientThreads)
            {
                foreach (ClientThread ct in clientThreads)
                {
                    ct.RequestStop();
                }
            }
        }

        protected void MainLoop()
        {
            Logger.Debug("EchoBackupService:MainLoop");
            while (true)
            {
                Thread.Sleep(250);
                checkCommandServer();
                //todo: as chunks finish compressing, add to networkthread
                checkStorageThread();
                //todo: as backup files retrieved from nodes, recover file from chunk/block
                checkClientThreads();
            }
        }

        protected void checkCommandServer()
        {
            ClientThread ct = commandServer.getClientThread();
            if (ct != null)
            {
                clientThreads.Add(ct);
            }
        }
        protected void checkStorageThread()
        {
            if (storageThread.NumChunks() > 0)
            {
                Chunk chunk = storageThread.DequeueChunk();
                Logger.Debug("EchoBackupService:checkStorageThread Finished archiving FileShare for BackupIndex.");
                //identify host(s) to send to.
                //send chunk to hosts.
                //do something with the index so we know about this backup
            }
            if (storageThread.NumRecoverStructs() > 0)
            {
                RecoverResult rs = storageThread.DequeueRecoverResult();
                lock (recoverResults)
                {
                    recoverResults.Enqueue(rs);
                }
            }
        }
        protected void checkClientThreads()
        {
            lock (clientThreads)
            {
                //foreach (ClientThread thread in clientThreads)
                for (int i=0; i<clientThreads.Count; i++)
                {
                    if (clientThreads[i].EventCount() > 0)
                    {
                        NetworkEvent ne = clientThreads[i].DequeueEvent();
                        processNetworkEvent(clientThreads[i], ne);
                    }
                    else
                    {
                        if (clientThreads[i].IsAlive() == false)
                        { //remove dead threads with empty event queues from the list
                            clientThreads.RemoveAt(i);
                            i--;
                            continue;
                        }
                    }
                }
            }
        }
        protected void processNetworkEvent(ClientThread ct, NetworkEvent ne)
        {
            Logger.Debug("EchoBackupService:processNetworkEvent");
            if (ne is NetworkResponse)
            {
                processNetworkResponse(ct, (NetworkResponse)ne);
            }
            else if (ne is QueryRequest)
            {
                processQueryRequest(ct, (QueryRequest)ne);
            }
            else if (ne is PushRequest)
            {
                processPushRequest(ct, (PushRequest)ne);
            }
            else if (ne is PullRequest)
            {
                processPullRequest(ct, (PullRequest)ne);
            }
            else if (ne is PushIndexRequest)
            {
                processPushIndexRequest(ct, (PushIndexRequest)ne);
            }
        }
        protected void processNetworkResponse(ClientThread ct, NetworkResponse response)
        {
            Logger.Debug("EchoBackupService:processNetworkResponse doing nothing");
            //todo
        }
        protected void processQueryRequest(ClientThread ct, QueryRequest request)
        {
            Logger.Debug("EchoBackupService:processQueryRequest doing nothing");
            //todo
        }
        protected void processPushRequest(ClientThread ct, PushRequest request)
        {
            Logger.Debug("EchoBackupService:processPushRequest");
            //there probably ought to be some processing here but oh well
            ct.EnqueueWork((NetworkRequest)request);

        }
        protected void processPullRequest(ClientThread ct, PullRequest request)
        {
            Logger.Debug("EchoBackupService:processPullRequest");
            //there probably ought to be some processing here but oh well
            ct.EnqueueWork((NetworkRequest)request);
        }
        protected void processPushIndexRequest(ClientThread ct, PushIndexRequest request)
        {
            Logger.Debug("EchoBackupService:processPushIndexRequest");
            //there probably ought to be some processing here but oh well
            ct.EnqueueWork((NetworkRequest)request);
        }

        static void Main()
        {
            ServiceBase.Run(new EchoBackupService());
        }

        public void StartBackup(List<string> directories)
        {
            foreach (string dir in directories)
            {
                if (Directory.Exists(dir))
                {
                    BackupTask bt = new BackupTask(dir, Node.GetTemporaryDirectory(), 123, 0);
                    storageThread.EnqueueStorageTask(bt);
                }
                else
                {
                    Logger.Warn("EchoBackupService:StartBackup Directory '" + dir + "' does not exist.");
                }
            }
        }
        public void StartRecovery(long fileID, Guid sourceNode, string outputPath)
        {
            //get host with chunk/block from index
            //add network task to pull chunk/block from host

            //in mainLooP:
            //recover file form chunk/block

        }
        /// <summary>
        /// Returns "" if there is no completed recovery or a recovery failed.
        /// Returns the path to the output directory if recovery succeeded.
        /// </summary>
        /// <returns></returns>
        public string CheckRecovery(/*long fileID, Guid sourceNode*/)
        {
            lock (recoverResults)
            {
                if (recoverResults.Count > 0)
                {
                    RecoverResult rr = recoverResults.Dequeue();
                    if (rr.Successful)
                    {
                        return Path.GetDirectoryName(rr.Path);
                    }
                    else
                    {
                        Logger.Error("EchobackupService:CheckRecovery Recovery failed");
                    }
                }
            }
            return "";
        }
    }
}
