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
        private struct Node
        {
            string uniqueIdentifier;
            string name;
            string ip;
            string mac;
            public int maxBackupCapacity;
            long backupData;
            long nonBackupData;
            long freeSpace;
            long totalCapacity;
            int reliabilityMetric;
            int hops;
            int smart;
            int backupFailed;
            int backupPassed;
            string status;
        }
    }
}
