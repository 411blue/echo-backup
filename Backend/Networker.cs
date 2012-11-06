using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using System.Collections.Generic;
using System.IO;

namespace Backend 
{   /*
    public struct Hello 
    {
        public IPAddress ip;
        public PhysicalAddress mac;
        public Guid guid;
        public Version version;

        public Hello(IPAddress ip, PhysicalAddress mac, Guid guid, Version version) {
            this.ip = ip;
            this.mac = mac;
            this.guid = guid;
            this.version = version;
        }
    }
    */

    class Networker
    {
        /*
        const int MULTICAST_PORT = 7777;
        static readonly byte[] MULTICAST_CHANNEL_BYTES = { 239, 77, 77, 77 };
        static readonly IPAddress MULTICAST_CHANNEL = new IPAddress(MULTICAST_CHANNEL_BYTES);

        UdpClient udpClient;
        IPEndPoint multicastListener;
        Thread multicastThread;
        Thread unicastThread;
        Queue<Hello> hellos;
        */

        public Networker()
        {
            uniqueId = Guid.Empty;
            hostName = "";
            internetAddress = "";
            mac = "";
            freeSpace = 0;
            totalSize = 0;
            usedSpace = 0;
            backupLimit = 0;
            backupDirectory = "";
            directorySize = 0;
            multicastIp = IPAddress.Parse("224.1.0.1");
        }

        public void server()
        {
            UdpClient sock = new UdpClient(9050);
            Console.WriteLine("Ready to receive…");
            sock.JoinMulticastGroup(multicastIp, 50);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = sock.Receive(ref iep);
            string stringData = Encoding.ASCII.GetString(data, 0, data.Length);
            Console.WriteLine("received: {0}", stringData);
            sock.Close();
        }

        public void client()
        {
            IPAddress multicastIp = IPAddress.Parse("224.1.0.1");
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9051);
            IPEndPoint iep2 = new IPEndPoint(multicastIp, 9050);
            server.Bind(iep);

            string attributes = string.Concat(GetUniqueId() + "/" + GetHostName() + "/" + GetInternetAddress() + "/"
                + GetMAC()) + "/" + GetTotalSize() + "/" + GetUsedSpace() + "/" + GetFreeSpace()
                + "/" + GetMaxBackupSpace() + "/" + GetDirectorySize(GetBackupDirectory());
            byte[] data = Encoding.ASCII.GetBytes(attributes);
            server.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastIp));
            server.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 50);
            server.SendTo(data, iep2);
            server.Close();
        }

        //Generate UniqeID
        public Guid GetUniqueId()
        {
            return Guid.NewGuid();
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
        public double GetFreeSpace()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {
                if (drive.Name == "C:\\")
                {
                    freeSpace = Convert.ToDouble(drive.AvailableFreeSpace / 1073741824);
                }
            }
            return freeSpace;
        }

        //Get total disk space of drive C on the local node
        public double GetTotalSize()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {
                if (drive.Name == "C:\\")
                {
                    totalSize = Convert.ToDouble(drive.TotalSize / 1073741824);
                    break;
                }
            }
            return totalSize;
        }

        //Get used disk space of drive C on the local node
        public double GetUsedSpace()
        {
            return usedSpace = Convert.ToDouble(GetTotalSize() - GetFreeSpace());
        }

        //Get the max backup support of the local node
        public double GetMaxBackupSpace()
        {
            return backupLimit;
        }

        //Get the size of directory and all subdirectories
        public double GetDirectorySize(string path)
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
            return Convert.ToDouble(directorySize);
        }

        //Get the backup directory
        public string GetBackupDirectory()
        {
            return backupDirectory;
        }

        //Set the max backup support of the local node
        public void setMaxBackupSpace(double d1)
        {
            d1 = backupLimit;
        }

        //Set the backup directory
        public void SetBackupDirectory(string s1)
        {
            s1 =  backupDirectory;
        }

        private Guid uniqueId;
        private string hostName;
        private string internetAddress;
        private string mac;
        private double freeSpace;
        private double totalSize;
        private double usedSpace;
        private double backupLimit;
        private double directorySize;
        private string backupDirectory;
        IPAddress multicastIp;
    }
}
