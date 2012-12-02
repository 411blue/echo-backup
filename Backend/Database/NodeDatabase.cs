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
        //Creates a database. Database must not exist.
        public SQLiteConnection CreateDataBase(string pathAndFileName)
        {
            //This is the connection object. The database should not already exist.
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathAndFileName);

            //By opening and closing a connection to a file that does not exist, we create the file.
            conn.Open();
            conn.Close();

            return conn;
        }

        /// Returns a connection to an existing database. Database must exist.
        public SQLiteConnection ConnectToExistingDatabase(string pathAndFileName)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathAndFileName);
            return conn;
        }

        //Create a node table. 
        public void CreateNodeTable(SQLiteConnection conn)
        {
            //This sql query creates a nodes table with uniqueId as the primary key.
            string sql = "CREATE TABLE nodes (UniqueId TEXT PRIMARY KEY, Name TEXT, Ip TEXT, Mac TEXT,"
            + " MaxBackupCapacity INTEGER, BackupData INTEGER, NonBackupData INTEGER, FreeSpace INTEGER, TotalCapacity INTEGER,"
            + " RelialibyMetric INTEGER, Hops INTEGER, Smart INTEGER, BackupsFailed INTEGER, BackupsPassed INTEGER, Status TEXT)";

            //SQLiteCommand objects are where we exectute sql state
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Insert a new record. Primary key must not be in table already.
        public void InsertNodeRecord(Node n1, SQLiteConnection conn)
        {
            string sql = "INSERT INTO nodes (UniqueId, Name, Ip, Mac, MaxBackupCapacity,"
                + " BackupData, NonBackupData, FreeSpace, TotalCapacity,"
                + " RelialibyMetric, Hops, Smart, BackupsFailed, BackupsPassed, Status)"
                + " VALUES (@pUniqueId, @pName, @pIp, @pMac, @pMaxBackupCapacity, @pBackupData, @pNonBackupData, @pFreeSpace"
                + " @pTotalCapaciy, @pReliablityMetric, @pHops, @pSmart, @pBackupsFailed, @pBackupsPassed, @pStatus)";

            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", n1.uniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pName", n1.name));
            cmd.Parameters.Add(new SQLiteParameter("@pIp", n1.ip));
            cmd.Parameters.Add(new SQLiteParameter("@pMac", n1.mac));
            cmd.Parameters.Add(new SQLiteParameter("@pMaxBackupCapacity", n1.maxBackupCapacity));
            cmd.Parameters.Add(new SQLiteParameter("@pBackupData", n1.backupData));
            cmd.Parameters.Add(new SQLiteParameter("@pNonBackupData", n1.nonBackupData));
            cmd.Parameters.Add(new SQLiteParameter("@pFreeSpace", n1.freeSpace));
            cmd.Parameters.Add(new SQLiteParameter("@pTotalCapacity", n1.totalCapacity));
            cmd.Parameters.Add(new SQLiteParameter("@pReliabityMetric", n1.relialibyMetric));
            cmd.Parameters.Add(new SQLiteParameter("@pHops", n1.hops));
            cmd.Parameters.Add(new SQLiteParameter("@pSmart", n1.smart));
            cmd.Parameters.Add(new SQLiteParameter("@pBackkupsFailed", n1.backupsFailed));
            cmd.Parameters.Add(new SQLiteParameter("@pBackupsPassed", n1.backupsPassed));
            cmd.Parameters.Add(new SQLiteParameter("@pStatus", n1.status));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Replace a record.
        public void ReplaceNodeRecord(Node n1, SQLiteConnection conn)
        {
            string sql = "REPLACE INTO nodes (UniqueId, Name, Ip, Mac, MaxBackupCapacity,"
                + " BackupData, NonBackupData, FreeSpace, TotalCapacity,"
                + " RelialibyMetric, Hops, Smart, BackupsFailed, BackupsPassed, Status)"
                + " VALUES (@pUniqueId, @pName, @pIp, @pMac, @pMaxBackupCapacity, @pBackupData, @pNonBackupData, @pFreeSpace"
                + " @pTotalCapaciy, @pReliablityMetric, @pHops, @pSmart, @pBackupsFailed, @pBackupsPassed, @pStatus)";

            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", n1.uniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pName", n1.name));
            cmd.Parameters.Add(new SQLiteParameter("@pIp", n1.ip));
            cmd.Parameters.Add(new SQLiteParameter("@pMac", n1.mac));
            cmd.Parameters.Add(new SQLiteParameter("@pMaxBackupCapacity", n1.maxBackupCapacity));
            cmd.Parameters.Add(new SQLiteParameter("@pBackupData", n1.backupData));
            cmd.Parameters.Add(new SQLiteParameter("@pNonBackupData", n1.nonBackupData));
            cmd.Parameters.Add(new SQLiteParameter("@pFreeSpace", n1.freeSpace));
            cmd.Parameters.Add(new SQLiteParameter("@pTotalCapacity", n1.totalCapacity));
            cmd.Parameters.Add(new SQLiteParameter("@pReliabityMetric", n1.relialibyMetric));
            cmd.Parameters.Add(new SQLiteParameter("@pHops", n1.hops));
            cmd.Parameters.Add(new SQLiteParameter("@pSmart", n1.smart));
            cmd.Parameters.Add(new SQLiteParameter("@pBackkupsFailed", n1.backupsFailed));
            cmd.Parameters.Add(new SQLiteParameter("@pBackupsPassed", n1.backupsPassed));
            cmd.Parameters.Add(new SQLiteParameter("@pStatus", n1.status));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Delete a record. Primary key must be in table
        public void DeleteNodeRecord(Guid id, SQLiteConnection conn)
        {
            string sql = "DELETE FROM nodes where Id = @pId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.Add(new SQLiteParameter("@pId", id));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Get the Hostname in a record. Primary key must be in table already.
        public string SelectNodeName(Guid id, SQLiteConnection conn)
        {
            string sql = "SELECT Name FROM nodes WHERE Id = @pId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.Add(new SQLiteParameter("@pId", id));

            conn.Open();
            string name = cmd.ExecuteScalar().ToString();
            conn.Close();

            return name;
        }

        //Update Hostname in a record. Primary key must be in table already.
        public void UpdateNodeName(Guid id, string name, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set Name = @pName WHERE Id = @pId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pId", id));
            cmd.Parameters.Add(new SQLiteParameter("@pName", name));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }

        //Defines a node and provides constructor
        public struct Node
        {
            public Guid uniqueId; 
            public string name, ip, mac;
            public int maxBackupCapacity;
            public long backupData, nonBackupData, freeSpace, totalCapacity;
            public int relialibyMetric, hops, smart, backupsFailed, backupsPassed;
            public string status;

            public Node(Guid g1, string s1, string s2, string s3, int i1, long l1, long l2, long l3, long l4, int i2, int i3, int i4, 
            int i5, int i6, string s4)
            {
                uniqueId = g1;
                name = s1;
                ip = s2;
                mac = s3;
                maxBackupCapacity = i1;
                backupData = l1;
                nonBackupData = l2;
                freeSpace = l3;
                totalCapacity = l4;
                relialibyMetric = i2;
                hops = i3;
                smart = i4;
                backupsFailed = i5;
                backupsPassed = i6;
                status = s4;
            }
        }
}
