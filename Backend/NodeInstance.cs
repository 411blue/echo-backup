using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;

namespace Backend
{
    public class NodeInstance : Node
    {
        public string BackupDirectory { get; set; }
        public double CPU_Utilization { get; set; }
        public double UsedBandwidth { get; set; } //bits/second
        public double MaxBandwidth { get; set; }
        public long MaxBackupSpace { get; set; }
        public long BackupSpaceUsage { get; set; }
        public string Name { get; set; }
        public Guid GUID { get; set; }
        public int SMART_Score { get; set; }
        
        //public Networker Networker { get; set; }
        //Random rand;

        public NodeInstance()
        {
        }

        public NodeInstance(string name, int smartScore, 
            string backupDir, long maxBackupSpace, long backupSpaceUsage)
        {
            Name = name;
            SMART_Score = smartScore;
            BackupDirectory = backupDir;
            MaxBackupSpace = maxBackupSpace;
            BackupSpaceUsage = backupSpaceUsage;
            GUID = new Guid();
            CPU_Utilization = 0;
            MaxBandwidth = 3000000; //simulating
        }

        public long FreeSpace
        {
            get { return MaxBackupSpace - BackupSpaceUsage; }
        }

        public string IPAddress
        {
            get
            {
                string internetAddress = "";
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

            set{} //for simulating
        }


        //Get name of local node
        public string HostName
        {
            get
            {
                return System.Net.Dns.GetHostName();
            }
            set { } //for simulating
        }

        //Get Media Access Control of local node and reformats to match ipconfig printout
        public string MAC
        {
            get
            {
                string mac = "";
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
            set { } //for simulating
        }

    }
}
