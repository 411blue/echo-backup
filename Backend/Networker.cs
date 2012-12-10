using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO;

namespace Backend 
{   
    public class Networker
    {
        public Networker()
        {
            uniqueId = Properties.Settings.Default.guid;
            smart = Properties.Settings.Default.smart;
            hops = 0;

            if(uniqueId == Guid.Empty)
            {
                uniqueId = GenerateUniqueId();
            }

            if (smart == 0)
            {
                SetSmart(0, 89);
            }

            nonBackupData = 0;
            backupData = 0;
            backupLimit = 0;
            backupDirectory = "";
            directorySize = 0;
            reliabilityMetric = 0;
            receiverAlive = false;
            transmitterAlive = false;
            ip = IPAddress.Parse("224.1.0.1");
            receiver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            transmitter = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            rxBuffer = new byte[256];
            txBuffer = new byte[256];
            heartbeat = "";
            heartbeats = new Queue<string>();
        }

        //Generate UniqeId
        public Guid GenerateUniqueId()
        {
            return Guid.NewGuid();
        }

        //Get UniqueId
        public Guid GetUniqueId()
        {
            return uniqueId;
        }

        

        //Get the max backup support of the local node
        public int GetMaxBackupSpace()
        {
            return backupLimit;
        }

        

        //Get the backup directory
        public string GetBackupDirectory()
        {
            return backupDirectory;
        }

        //Get Hop score
        public int GetHops()
        {
            return hops;
        }

        //Get SMART score
        public int GetSmart()
        {
            return smart;
        }

        //Set the max backup support of the local node
        public void SetMaxBackupCapacity(int i1)
        {
            backupLimit = i1;
        }

        //Set the backup directory
        public void SetBackupDirectory(string s1)
        {
            backupDirectory = s1;
        }

        //Set SMART Data
        public int SetSmart(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }

        //Set Hop Data
        public int SetHop(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }

        //Calculate reliabity metric
        public int CalculateReliablity(int passed, int failed)
        {
            reliabilityMetric = 255 - GetSmart() - GetHops() - (114 * (failed / (failed + passed)));
            return reliabilityMetric;
        }

        ~Networker()
        {
            Properties.Settings.Default.Save();
        }

        //Starts reciever
        public void startReciever()
        {
            receiverAlive = true;
            IPEndPoint rxipep = new IPEndPoint(IPAddress.Any, 4567);
            receiver.Bind(rxipep);
            receiver.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip, IPAddress.Any));
        }

        //Runs the receiver
        public void runReceiver()
        {
            while (receiverAlive)
            {
                receiver.Receive(rxBuffer);
                string str = System.Text.Encoding.ASCII.GetString(rxBuffer, 0, rxBuffer.Length);
                heartbeats.Enqueue(str);
            }
        }

        //Get update message
        public string GetHeartbeat()
        {
            heartbeat = string.Concat(GetUniqueId() + "/" + Node.GetHostName() + "/" + Node.GetInternetAddress() + "/" + Node.GetMAC() 
            + "/" + GetMaxBackupSpace() + "/" + Node.GetBackupSpace() + "/" + Node.GetNonBackupSpace() + "/" + Node.GetFreeSpace()
            + "/" + Node.GetTotalSize() + "/" + GetSmart());
            return heartbeat;
        }

        //Process heartbeats
        public void processMessages()
        {
            while (receiverAlive)
            {
                if (heartbeats.Count > 0)
                {
                    string[] attributes = new string[10];
                    string hb = heartbeats.Dequeue();
                    attributes = hb.Split();
                    Backend.Database.NodeDatabase nd = new Backend.Database.NodeDatabase();
                    
                    //If node is new, then add record with heartbreat attributes and set other attritbutes to default
                    //If node is existing, then just add heartbeat attributes
                    if(nd.PrimaryKeyCheck(Guid.Parse(attributes[0]), nd.ConnectToDatabase(@"C:\nodes.db")))
                    {
                        Backend.Database.Node existingNode = new Backend.Database.Node(Guid.Parse(attributes[0]), attributes[1], 
                            IPAddress.Parse(attributes[2]), attributes[3], Convert.ToInt32(attributes[4]), long.Parse(attributes[5]),
                            long.Parse(attributes[6]), long.Parse(attributes[7]),Convert.ToInt32(attributes[8]), 
                            -1, -1, Convert.ToInt32(attributes[9]), -1, -1, "");
                        nd.ReplaceNodeRecord(existingNode, nd.ConnectToDatabase(@"C:\nodes.db"));
                    }
                    else
                    {
                        Backend.Database.Node newNode = new Backend.Database.Node(Guid.Parse(attributes[0]), attributes[1], 
                            IPAddress.Parse(attributes[2]), attributes[3], Convert.ToInt32(attributes[4]), long.Parse(attributes[5]),
                            long.Parse(attributes[6]), long.Parse(attributes[7]),Convert.ToInt32(attributes[8]), 
                            CalculateReliablity(0,0), GetHops(), Convert.ToInt32(attributes[9]), 0, 0, "yes");
                        nd.InsertNodeRecord(newNode, nd.ConnectToDatabase(@"C:\nodes.db"));
                    }
                }
            }
        }

        //Starts transmitter
        public void startTransmitter()
        {
            transmitterAlive = true;
            transmitter.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip));
            transmitter.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 25);
            IPEndPoint txipep = new IPEndPoint(ip, 4567);
            transmitter.Connect(txipep);
        }

        //Run transmittter
        public void runTransmitter()
        {
            while (transmitterAlive)
            {
                txBuffer = Encoding.ASCII.GetBytes("Hello");
                transmitter.Send(txBuffer, txBuffer.Length, SocketFlags.None);
                Thread.Sleep(10000);
            }
        }

        //Close transmitter socket
        public void stopTransmitter()
        {
            transmitter.Close();
        }

        //Close receiver socket
        public void stopReciever()
        {
            receiver.Close();
        }

        //Receiver alive is used for thread control
        public void setReceiverAlive(bool b1)
        {
            receiverAlive = b1;
        }

        //Transimitter alive is used for thread control
        public void setTransmitterAlive(bool b1)
        {
            transmitterAlive = b1;
        }    

        private Guid uniqueId;
        private long backupData;
        private long nonBackupData;
        private int backupLimit;
        private long directorySize;
        private string backupDirectory;
        private IPAddress ip;
        private Socket receiver;
        private Socket transmitter;
        private byte[] rxBuffer;
        private byte[] txBuffer;
        private string heartbeat;
        private Queue<string> heartbeats;
        private bool receiverAlive;
        private bool transmitterAlive;
        private int smart;
        private int hops;
        private int reliabilityMetric;
    }
}
