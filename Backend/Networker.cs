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
            hops = Properties.Settings.Default.hops;
            smart = Properties.Settings.Default.smart;

            if(uniqueId == Guid.Empty)
            {
                uniqueId = GenerateUniqueId();
            }

            if (hops == 0)
            {
                SetHop(0, 51);
            }

            if (smart == 0)
            {
                SetSmart(0, 89);
            }

            internetAddress = "";
            mac = "";
            freeSpace = 0;
            totalSize = 0;
            nonBackupData = 0;
            backupData = 0;
            backupLimit = 0;
            backupDirectory = "";
            directorySize = 0;
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

        //Get name of local node
        public string GetHostName()
        {
            return System.Net.Dns.GetHostName();
        }

        //Get Internet Protocol Address Version 4 of local node
        public string GetInternetAddress()
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(GetHostName());
            IPAddress[] addr = ipEntry.AddressList;
            for (int i = 0; i < addr.Length; ++i)
            {
                //todo: this needs to be rewritten to not use a deprecated property
                if (addr[i].AddressFamily == AddressFamily.InterNetwork && addr[i].Address != 16777343)
                {
                    byte[] octets = addr[i].GetAddressBytes();
                    internetAddress = string.Concat(Convert.ToString(octets[0]) + '.' + Convert.ToString(octets[1]) + '.' +
                        Convert.ToString(octets[2]) + '.' + Convert.ToString(octets[3]));
                    break;
                }
            }
            return internetAddress;
        }

        //Get Media Access Control of local node and reformats to match ipconfig printout
        public string GetMAC()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
            {
                OperationalStatus ostate = adapter.OperationalStatus;
                NetworkInterfaceType netType = adapter.NetworkInterfaceType;
                if (ostate == OperationalStatus.Up && netType == NetworkInterfaceType.Ethernet || ostate == OperationalStatus.Up && netType == NetworkInterfaceType.Wireless80211)
                {
                    mac = adapter.GetPhysicalAddress().ToString();

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < mac.Length; i++)
                    {
                        if (i != 0 && i % 2 == 0)
                        {
                            sb.Append('-');
                        }
                        sb.Append(mac[i]);
                    }
                    mac = sb.ToString();
                    break;
                }
            }
            return mac;
        }

        //Get free disk space of drive C on the local node
        public long GetFreeSpace()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {
                if (drive.Name == "C:\\")
                {
                    freeSpace = drive.AvailableFreeSpace;
                }
            }
            return freeSpace;
        }

        //Get total disk space of drive C on the local node
        public long GetTotalSize()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {
                if (drive.Name == "C:\\")
                {
                    totalSize = drive.TotalSize;
                    break;
                }
            }
            return totalSize;
        }

        //Get disk space of drive C on the local node used for nonbackup data
        public long GetNonBackupSpace()
        {
            return nonBackupData = GetTotalSize() - GetDirectorySize(GetBackupDirectory()) - GetFreeSpace();
        }

        //Get disk space of drive C on the local node used for backup data
        public long GetBackupSpace()
        {
            return GetDirectorySize(GetBackupDirectory());
        }

        //Get the max backup support of the local node
        public int GetMaxBackupSpace()
        {
            return backupLimit;
        }

        //Get the size of directory and all subdirectories
        public long GetDirectorySize(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] fi = di.GetFiles();

            foreach (FileInfo file in fi)
            {
                directorySize = directorySize + file.Length;
            }

            DirectoryInfo[] subDir = di.GetDirectories();
            foreach (DirectoryInfo dri in subDir)
            {
                GetDirectorySize(Convert.ToString(dri.FullName));
            }
            return directorySize;
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
            return 255 - GetSmart() - GetHops() - (114 * (failed/(failed + passed)));
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
            heartbeat = string.Concat(GetUniqueId() + "/" + GetHostName() + "/" + GetInternetAddress() + "/" + GetMAC()) 
            + "/" + GetMaxBackupSpace() + "/" + GetBackupSpace() + "/" + GetNonBackupSpace() + "/" + GetFreeSpace()
            + "/" + GetTotalSize();
            return heartbeat;
        }

        //Process heartbeats
        public void processMessages()
        {
            while (receiverAlive)
            {
                if (heartbeats.Count > 0)
                {
                    heartbeats.Dequeue();
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
        private string internetAddress;
        private string mac;
        private long freeSpace;
        private long totalSize;
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
    }
}
