using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;
using System.Net;
using System.Net.NetworkInformation; 
using Backend.Database;

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
                guidList = ndb.SelectTrustedGUID();

                foreach (string currentGUID in guidList)
                {
                    ddb.InsertNode(currentGUID, offline);

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

                guidList = ndb.SelectTrustedGUID();

                foreach (string currentGUID in guidList)
                {
                    if (ddb.GetStatus(currentGUID) == online)
                    {
                        // transmit file to 'guid'
                    }
                }
            }
        }

        // Send indexes to a specific node
        // Use after a node comes online
        public void sendIndexes(string guid)
        {
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

                // transmit file to 'guid'
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
