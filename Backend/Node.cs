﻿using System;
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
        //get globally-unique ID of this node
        public static Guid GetUniqueID()
        {
            return GetGuid();
        }
        public static Guid GetGuid()
        {
            return Properties.Settings.Default.guid;
        }

        /// <summary>
        /// Returns the path to the directory this executable is running from.
        /// </summary>
        /// <returns></returns>
        public static string ExecutableDir()
        {
            //return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            //return Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp";
            return @"C:\Users\411blue\Desktop\temp";
        }

        // get/set the directory used to store temporary and intermediate files
        public static string GetTemporaryDirectory()
        {
            return Properties.Settings.Default.temporaryDirectory;
        }

        public static void SetTemporaryDirectory(string path)
        {
            CreateDirectoryIfNotExists(path);
            Properties.Settings.Default.temporaryDirectory = path;
            Properties.Settings.Default.Save();
        }

        //Get the max backup support of the local node
        public static long GetMaxBackupSpace()
        {
            return Properties.Settings.Default.maxBackupCapacity;
        }
        //Set the max backup support of the local node
        public static void SetMaxBackupCapacity(long l)
        {
            Properties.Settings.Default.maxBackupCapacity = l;
            Properties.Settings.Default.Save();
        }

        //Get SMART Data
        public static int GetSmart()
        {
            Random rnd = new Random();
            int smart = rnd.Next(0, 89);
            return smart;
        }

        //Get Hop Data
        public static int GetHops()
        {
            Random rnd = new Random();
            int hop = rnd.Next(0, 51);
            return hop;

        }

        //Get the backup directory
        public static string GetBackupDirectory()
        {
            return Properties.Settings.Default.localBackupPath;
        }
        //Set the backup directory
        public static void SetBackupDirectory(string path)
        {
            CreateDirectoryIfNotExists(path);
            Properties.Settings.Default.localBackupPath = path;
            Properties.Settings.Default.Save();
        }

        //Get Internet Protocol Address Version 4 of local node
        public static string GetInternetAddress()
        {
            IPAddress ipa = GetIPAddress();
            byte[] octets = ipa.GetAddressBytes();
            return string.Concat(Convert.ToString(octets[0]) + '.' + Convert.ToString(octets[1]) + '.' +
                Convert.ToString(octets[2]) + '.' + Convert.ToString(octets[3]));
        }
        public static IPAddress GetIPAddress()
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(GetHostName());
            IPAddress[] addr = ipEntry.AddressList;
            for (int i = 0; i < addr.Length; ++i)
            {
                //todo: this needs to be rewritten to not use a deprecated property
                if (addr[i].AddressFamily == AddressFamily.InterNetwork && addr[i].Address != 16777343) //lol 1.0.0.127
                {
                    return addr[i];
                }
            }
            Logger.Error("Node:GetIPAddress Could not determine my IP address");
            return null;
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

        public static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Logger.Notice("Node:CreateDirectoryIfNotExists Path '" + path + "' does not exist. Creating directory(s).");
                Directory.CreateDirectory(path);
            }
        }
        //Get the size of directory and all subdirectories
        public static long GetDirectorySize(string path)
        {
            CreateDirectoryIfNotExists(path);
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

        //Defines a node and provides constructor
        public struct PC
        {
            public Guid uniqueId;
            public string name;
            public IPAddress ip;
            public string mac;
            public int maxBackupCapacity;
            public long backupData, nonBackupData, freeSpace, totalCapacity;
            public int reliablityMetric, hops, smart, backupsFailed, backupsPassed;
            public string trusted;

            public PC(Guid uniqueId, string name, IPAddress ip, string mac, int maxBackupCapacity, long backupData,
                long nonBackupData, long freeSpace, long totalCapacity, int reliabilityMetric, int hops, int smart,
                int backupsFailed, int backupsPassed, string trusted)
            {
                this.uniqueId = uniqueId;
                this.name = name;
                this.ip = ip;
                this.mac = mac;
                this.maxBackupCapacity = maxBackupCapacity;
                this.backupData = backupData;
                this.nonBackupData = nonBackupData;
                this.freeSpace = freeSpace;
                this.totalCapacity = totalCapacity;
                this.reliablityMetric = reliabilityMetric;
                this.hops = hops;
                this.smart = smart;
                this.backupsFailed = backupsFailed;
                this.backupsPassed = backupsPassed;
                this.trusted = trusted;
            }
        }
    }
}
