using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;

namespace Backend.Database
{
    class NodeDatabase
    {
        public struct Node
        {
            public string uniqueIdentifier;
            public string name;
            public string ip;
            public string mac;
            public int maxBackupCapacity;
            public long backupData;
            public long nonBackupData;
            public long freeSpace;
            public long totalCapacity;
            public int reliabilityMetric;
            public int hops;
            public int smart;
            public double backupPassPrecentage;
            public string status;
        }
    }
}
