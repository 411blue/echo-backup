﻿using System;
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
        public int CalculateReliablity(int hops, int smart, int passed, int failed)
        {
            int ratio = 0;
            if (failed + passed == 0)
            {
                ratio = 1;
            }
            else
            {
                ratio = Convert.ToInt32((double) failed / (double) (failed + passed));
            }

            reliabilityMetric = 255 - smart- hops - (114 * ratio);
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
            + "/" + Node.GetTotalSize());
            return heartbeat;
        }

        //Process heartbeats
        public void processMessages()
        {
            while (receiverAlive)
            {
                Thread.Sleep(1000);
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
                        Node.PC existingPC = new Node.PC(Guid.Parse(attributes[0]), attributes[1], 
                            IPAddress.Parse(attributes[2]), attributes[3], Convert.ToInt32(attributes[4]), long.Parse(attributes[5]),
                            long.Parse(attributes[6]), long.Parse(attributes[7]),long.Parse(attributes[8]),
                            -1,-1,-1,-1,-1,"");
                        nd.UpdateNodeRecord(existingPC);
                    }
                    else
                    {
                        int s = Node.GetSmart();
                        int h = Node.GetHops();
                        Node.PC newPC = new Node.PC(Guid.Parse(attributes[0]), attributes[1], 
                            IPAddress.Parse(attributes[2]), attributes[3], Convert.ToInt32(attributes[4]), long.Parse(attributes[5]),
                            long.Parse(attributes[6]), long.Parse(attributes[7]),long.Parse(attributes[8]), 
                            CalculateReliablity(h, s, 0,0), h, s, 0, 0, "yes");
                        nd.InsertNodeRecord(newPC);
                    }
                }
            }
        }

        //Starts transmitter
        public void startTransmitter()
        {
            transmitterAlive = true;
            try
            {
                transmitter.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip));
                transmitter.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 25);
                IPEndPoint txipep = new IPEndPoint(ip, 4567);
                transmitter.Connect(txipep);
            }
            catch (SocketException ex)
            {
                Logger.Error("Networker:startTransmitter ErrorCode: " + ex.ErrorCode + ' ' + ex.Message);
            }
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
        private int reliabilityMetric;
    }
}
