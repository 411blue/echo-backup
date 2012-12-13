using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using Backend.Database;
using System.Threading;

namespace Backend
{
    public class IndexDistribution
    {
        public IndexDistribution()
        {
            distribute = false;
        }

        public void SetDistribute(bool d)
        {
            distribute = d;
        }

        // Loop while program is running and ping trusted nodes to determine whether they are online or offline
        // When a node comes online, distribute indexes to that node
        public void BeginIndexDistribution()
        {
            DistributionDatabase ddb = new DistributionDatabase();
            List<string> guidList = new List<string>();
            NodeDatabase ndb = new NodeDatabase();
            string online = "online";
            string offline = "offline";

            ddb.ResetStatus();

            while (distribute)
            {
                guidList = ndb.SelectGUID();

                foreach (string currentGUID in guidList)
                {
                    if (Node.GetGuid() != Guid.Parse(currentGUID))
                    {
                        Thread.Sleep(1000);
                        ddb.InsertNode(currentGUID, offline);
                        if (ndb.SelectNodeTrusted(Guid.Parse(currentGUID)) == "yes")
                        {
                            Ping pingSender = new Ping();
                            int timeout = 100;
                            try
                            {
                                IPAddress ip = ndb.SelectNodeIp(Guid.Parse(currentGUID));
                                PingReply reply = pingSender.Send(ip, timeout);

                                if (reply.Status == IPStatus.Success)
                                {
                                    if (ddb.GetStatus(currentGUID) == offline)
                                    {
                                        sendIndexes(currentGUID);
                                        ddb.UpdateStatus(currentGUID, online);
                                    }
                                }
                                else
                                {
                                    ddb.UpdateStatus(currentGUID, offline);
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.Print(ex.Message);
                            }
                        }
                        else
                        {
                            ddb.UpdateStatus(currentGUID, offline);
                        }
                    }
                }
            }
        }

        // Send indexes to all trusted nodes that are online
        // For use after a backup and associated index insertion have occurred
        public void sendIndexes()
        {
            IndexDatabase idd = new IndexDatabase();
            if (!idd.TablesEmpty()) //if there are indexes to send
            {
                DistributionDatabase ddb = new DistributionDatabase();
                List<string> guidList = new List<string>();
                NodeDatabase ndb = new NodeDatabase();

                string online = "online";
                string indexDBCopy = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\indexes_" + (Properties.Settings.Default.guid).ToString() + ".s3db";

                if (System.IO.File.Exists(idd.GetPathFileName()))
                {
                    try
                    {
                        System.IO.File.Copy(idd.GetPathFileName(), indexDBCopy, true);
                    }
                    catch (System.IO.IOException e)
                    {
                        Debug.Print(e.Message);
                    }
                }

                guidList = ndb.SelectGUID();

                foreach (string currentGUID in guidList)
                {
                    if (Node.GetGuid() != Guid.Parse(currentGUID) && ddb.GetStatus(currentGUID) == online && ndb.SelectNodeTrusted(Guid.Parse(currentGUID)) == "yes")
                    {
                        Guid myGuid = Node.GetGuid();
                        TcpClient tcpClient = new TcpClient((ndb.SelectNodeIp(Guid.Parse(currentGUID))).ToString(), 7890);
                        ClientThread ct = new ClientThread(tcpClient, false, myGuid);
                        PushIndexRequest pir = new PushIndexRequest(indexDBCopy, new FileInfo(indexDBCopy).Length);
                        ct.EnqueueWork(pir);
                        NetworkResponse response = (NetworkResponse)ct.DequeueEvent();
                        while (ct.IsWorking())
                        {
                            Thread.Sleep(1000);
                        }
                        ct.RequestStop();
                        while (ct.IsAlive())
                        {
                            Thread.Sleep(1000);
                        }
                    }
                }
            }
        }

        // Send indexes to a specific node
        // Use after a node comes online
        public void sendIndexes(string guid)
        {
            NodeDatabase ndb = new NodeDatabase();
            IndexDatabase idd = new IndexDatabase();
            if (!idd.TablesEmpty()) //if there are indexes to send
            {
                string indexDBCopy = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\indexes_" + (Properties.Settings.Default.guid).ToString() + ".s3db";

                if (System.IO.File.Exists(idd.GetPathFileName()))
                {
                    try
                    {
                        System.IO.File.Copy(idd.GetPathFileName(), indexDBCopy, true);
                    }
                    catch (System.IO.IOException e)
                    {
                        Debug.Print(e.Message);
                    }
                }

                Guid myGuid = Node.GetGuid();
                TcpClient tcpClient = new TcpClient((ndb.SelectNodeIp(Guid.Parse(guid))).ToString(), 7890);
                ClientThread ct = new ClientThread(tcpClient, false, myGuid);
                PushIndexRequest pir = new PushIndexRequest(indexDBCopy, new FileInfo(indexDBCopy).Length);
                ct.EnqueueWork(pir);
                NetworkResponse response = (NetworkResponse)ct.DequeueEvent();
                while (ct.IsWorking())
                {
                    Thread.Sleep(1000);
                }
                ct.RequestStop();
                while (ct.IsAlive())
                {
                    Thread.Sleep(1000);
                }
            }
        }

        // After the local node receives the index file from a foreign node
        // Merge that foreign index file with the local index file, then delete the foreign index file
        public void processIndexes(string filePath)
        {
            IndexDatabase ind = new IndexDatabase();

            ind.MergeIndexFiles(filePath);
            
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch (System.IO.IOException e)
                {
                    Debug.Print(e.Message);
                }
            }
             
        }

        private bool distribute;
    }
}
