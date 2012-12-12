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
            //this section moved to EchoBackupService.cs:EchoBackupService()
            //uniqueId = Properties.Settings.Default.guid;
            /*if(uniqueId == Guid.Empty)
            {
                uniqueId = GenerateUniqueId();
            }*/

            smart = Node.GetSmart();
            hops = Node.GetHops();
            if (smart == 0)
            {
                Node.CalculateAndSetSmart(0, 89);
            }

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

        

        //Calculate reliabity metric
        public int CalculateReliablity(int passed, int failed)
        {
            reliabilityMetric = 255 - Node.GetSmart() - Node.GetHops() - (114 * (failed / (failed + passed)));
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
            heartbeat = string.Concat(Node.GetUniqueID() + "/" + Node.GetHostName() + "/" + Node.GetInternetAddress() + "/" + Node.GetMAC() 
            + "/" + Node.GetMaxBackupSpace() + "/" + Node.GetBackupSpace() + "/" + Node.GetNonBackupSpace() + "/" + Node.GetFreeSpace()
            + "/" + Node.GetTotalSize() + "/" + Node.GetSmart());
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

                    attributes = hb.Split('/');

                    Backend.Database.NodeDatabase nd = new Backend.Database.NodeDatabase();
                    
                    //If node is new, then add record with heartbreat attributes and set other attritbutes to default
                    //If node is existing, then just add heartbeat attributes
                    if(nd.NodePrimaryKeyCheck(Guid.Parse(attributes[0])))
                    {
                        Backend.Database.Node existingNode = new Backend.Database.Node(Guid.Parse(attributes[0]), attributes[1], 
                            IPAddress.Parse(attributes[2]), attributes[3], Convert.ToInt32(attributes[4]), long.Parse(attributes[5]),
                            long.Parse(attributes[6]), long.Parse(attributes[7]),Convert.ToInt32(attributes[8]), 
                            -1, -1, Convert.ToInt32(attributes[9]), -1, -1, "");
                        nd.ReplaceNodeRecord(existingNode);
                    }
                    else
                    {
                        Backend.Database.Node newNode = new Backend.Database.Node(Guid.Parse(attributes[0]), attributes[1], 
                            IPAddress.Parse(attributes[2]), attributes[3], Convert.ToInt32(attributes[4]), long.Parse(attributes[5]),
                            long.Parse(attributes[6]), long.Parse(attributes[7]),Convert.ToInt32(attributes[8]), 
                            CalculateReliablity(0,0), Node.GetHops(), Convert.ToInt32(attributes[9]), 0, 0, "yes");
                        nd.InsertNodeRecord(newNode);
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
                //txBuffer = Encoding.ASCII.GetBytes("Hello");
                txBuffer = Encoding.ASCII.GetBytes(GetHeartbeat());
                transmitter.Send(txBuffer, txBuffer.Length, SocketFlags.None);
                Thread.Sleep(60000);
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
