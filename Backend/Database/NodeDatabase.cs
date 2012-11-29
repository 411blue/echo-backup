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
        /// <summary>Creates a SQLite database. Assumes that the database does not exist already.</summary>
        /// <param name="pathAndFileName">The path and filename of the SQLite database to create.</param>
        /// <returns>A SQLiteConnection object representing a connection to a SQLite database.</returns>
        public SQLiteConnection CreateDataBase(string pathAndFileName)
        {
            //The database should not already exist.
            if (System.IO.File.Exists(pathAndFileName))
            {
                throw new Exception("Database already exists!");
            }

            //This is the connection object. It basically represents the database.
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathAndFileName);

            //By opening and closing a connection to a file that does not exist, we create the file.
            conn.Open();
            conn.Close();

            return conn;
        }

        /// <summary>Returns a connection to an existing database.</summary>
        /// <param name="pathAndFileName">The path and filename of the existing SQLite database.</param>
        /// <returns></returns>
        public SQLiteConnection ConnectToExistingDatabase(string pathAndFileName)
        {
            //The database should already exist.
            if (!System.IO.File.Exists(pathAndFileName))
            {
                throw new Exception("Database does not exist!");
            }

            //This is the connection object. It basically represents the database.
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathAndFileName);
            return conn;
        }

        /// <summary>Creates an node table containing node records.</summary>
        /// <param name="conn">A SQLiteConnection object.</param>
        public void CreateNodeTable(SQLiteConnection conn)
        {
            //This sql query creates a nodes table with uniqueId as the primary key and some other columns.
            string sql = "CREATE TABLE nodes (UniqueId TEXT PRIMARY KEY, Name TEXT, Ip TEXT, Mac TEXT,"
            + " MaxBackupCapacity INTEGER, BackupData INTEGER, NonBackupData INTEGER, FreeSpace INTEGER, TotalCapacity INTEGER,"
            + " RelialibyMetric INTEGER, Hops INTEGER, Smart INTEGER, BackupsFailed INTEGER, BackupsPassed INTEGER, Status TEXT)";

            //SQLiteCommand objects are where we exectute sql statements.
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                //if anything is wrong with the sql statement or the database, a SQLiteException will show information about it.
                Debug.Print(ex.Message);

                //Always make sure the database connectio is closed.
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>Adds a record into the nodes table.</summary>
        /// <param name="conn">A SQLiteConnection object.</param>
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


            //Open the connection, exectute the query, and close the connection.
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }



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
