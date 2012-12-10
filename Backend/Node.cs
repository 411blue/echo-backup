using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;
using Backend.Properties;

namespace Backend
{
    public class Node
    {
        //Get Internet Protocol Address Version 4 of local node
        public static string GetInternetAddress()
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

        //Get name of local node
        public static string GetHostName()
        {
            return System.Net.Dns.GetHostName();
        }

        //Get Media Access Control of local node and reformats to match ipconfig printout
        public static string GetMAC()
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

        //Get free disk space of drive C on the local node
        public static long GetFreeSpace()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            long freeSpace = 0;
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
        public static long GetTotalSize()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            long totalSize = 0;
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

        //Get the size of directory and all subdirectories
        public static long GetDirectorySize(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] fi = di.GetFiles();
            long directorySize = 0;
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

        //Get disk space of drive C on the local node used for nonbackup data
        public static long GetNonBackupSpace()
        {
            long nonBackupData = Node.GetTotalSize() - GetDirectorySize(GetBackupDirectory()) - Node.GetFreeSpace();
            return nonBackupData;
        }

        //Get disk space of drive C on the local node used for backup data
        public static long GetBackupSpace()
        {
            long backupData = GetDirectorySize(GetBackupDirectory());
            return backupData;
        }

        public static string GetBackupDirectory()
        {
            return Settings.Default.localBackupPath;
        }
    }
}
