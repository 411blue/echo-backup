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

        //Get used disk space of drive C on the local node
        public long GetUsedSpace()
        {
            return usedSpace = GetTotalSize() - GetFreeSpace();
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

        //Set the max backup support of the local node
        public void setMaxBackupSpace(int i1)
        {
            backupLimit = i1;
        }

        //Set the backup directory
        public void SetBackupDirectory(string s1)
        {
            backupDirectory = s1;
        }

        //Get Hello message
        public string GetHello()
        {
            string hello = string.Concat("Hello" + "/" + Convert.ToString(GetUniqueId()));
            return hello;
        }

        //Get update message
        public string GetUpdate()
        {
            string update = string.Concat("Update" + "/" + Convert.ToString(GetUniqueId()) + "/" + GetHostName() + "/" + GetInternetAddress() + "/"
            + GetMAC()) + "/" + GetTotalSize() + "/" + GetUsedSpace() + "/" + GetFreeSpace()
            + "/" + GetMaxBackupSpace();
            return update;
        }

        //Get Bye message
        public string GetBye()
        {
            string bye = string.Concat("Hello" + "/" + GetUniqueId());
            return bye;
        }

        private Guid uniqueId;
        private string hostName;
        private string internetAddress;
        private string mac;
        private long freeSpace;
        private long totalSize;
        private long usedSpace;
        private int backupLimit;
        private long directorySize;
        private string backupDirectory;
    }
}
