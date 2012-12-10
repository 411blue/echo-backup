using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;
using System.Windows.Forms;
using System.Net;

namespace Backend.Database
{
    public class NodeDatabase
    {
        public NodeDatabase()
        {
        }

        /// Returns a connection to an existing. If it does not exist, it will be created on an open attempted.
        public SQLiteConnection ConnectToDatabase(string pathAndFileName)
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + pathAndFileName);
            return conn;
        }

        //Create a node table with UniqueId as the primary key
        public void CreateNodeTable(SQLiteConnection conn)
        {
            string sql = "CREATE TABLE nodes (UniqueId TEXT PRIMARY KEY, Name TEXT, Ip TEXT, Mac TEXT,"
            + " MaxBackupCapacity INTEGER, BackupData INTEGER, NonBackupData INTEGER, FreeSpace INTEGER, TotalCapacity INTEGER,"
            + " ReliabilityMetric INTEGER, Hops INTEGER, Smart INTEGER, BackupsFailed INTEGER, BackupsPassed INTEGER, Trusted TEXT)";
            
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Insert a new record. Primary key must not be in table already.
        public void InsertNodeRecord(Node n1, SQLiteConnection conn)
        {
            try
            {
                string sql = "INSERT INTO nodes (UniqueId, Name, Ip, Mac, MaxBackupCapacity,"
                    + " BackupData, NonBackupData, FreeSpace, TotalCapacity,"
                    + " ReliabilityMetric, Hops, Smart, BackupsFailed, BackupsPassed, Trusted)"
                    + " VALUES (@pUniqueId, @pName, @pIp, @pMac, @pMaxBackupCapacity, @pBackupData, @pNonBackupData, @pFreeSpace"
                    + " @pTotalCapaciy, @pReliabilityMetric, @pHops, @pSmart, @pBackupsFailed, @pBackupsPassed, @pTrusted)";

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
                cmd.Parameters.Add(new SQLiteParameter("@pReliabilityMetric", n1.reliablityMetric));
                cmd.Parameters.Add(new SQLiteParameter("@pHops", n1.hops));
                cmd.Parameters.Add(new SQLiteParameter("@pSmart", n1.smart));
                cmd.Parameters.Add(new SQLiteParameter("@pBackkupsFailed", n1.backupsFailed));
                cmd.Parameters.Add(new SQLiteParameter("@pBackupsPassed", n1.backupsPassed));
                cmd.Parameters.Add(new SQLiteParameter("@pTrusted", n1.trusted));

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SQLiteException)
            {
                MessageBox.Show("Possbile errors, record already existed or connection error.", "Database Insert Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Replace a record. Table unchanged if UniqueId is not present, table unchanged. 
        public void ReplaceNodeRecord(Node n1, SQLiteConnection conn)
        {
            string sql = "REPLACE INTO nodes (UniqueId, Name, Ip, Mac, MaxBackupCapacity,"
                + " BackupData, NonBackupData, FreeSpace, TotalCapacity,"
                + " RelialibyMetric, Hops, Smart, BackupsFailed, BackupsPassed, Trusted)"
                + " VALUES (@pUniqueId, @pName, @pIp, @pMac, @pMaxBackupCapacity, @pBackupData, @pNonBackupData, @pFreeSpace"
                + " @pTotalCapaciy, @pSmart)";

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
            cmd.Parameters.Add(new SQLiteParameter("@pSmart", n1.smart));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Delete a record. If UniqueId is not present, table is unchanged.
        public void DeleteNodeRecord(Guid UniqueId, SQLiteConnection conn)
        {
            string sql = "DELETE FROM nodes where UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Select the Name in a record. If UniqueId is not present, null is returned.
        public string SelectNodeName(Guid UniqueId, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT Name FROM nodes WHERE UniqueId = @pUniqueId";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

                conn.Open();
                string name = cmd.ExecuteScalar().ToString();
                conn.Close();

                return name;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        //Update the Name in a record.If UniqueId is not present, table is unchanged.
        public void UpdateNodeName(Guid UniqueId, string name, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set Name = @pName WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pName", name));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Select the Ip in a record. If UniqueId is not present, 0.0.0.0 is returned.
        public IPAddress SelectNodeIp(Guid UniqueId, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT Ip FROM nodes WHERE UniqueId = @pUniqueId";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

                conn.Open();
                string ip = cmd.ExecuteScalar().ToString();
                conn.Close();

                return IPAddress.Parse(ip);
            }
            catch (NullReferenceException)
            {
                return IPAddress.Parse("0.0.0.0");
            }

        }

        //Update the Ip in a record.If UniqueId is not present, table is unchanged.
        public void UpdateNodeIp(Guid UniqueId, IPAddress ip, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set Ip = @pIp WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pIp", ip));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Select the Name in a record. If UniqueId is not present, null is returned.
        public string SelectNodeMac(Guid UniqueId, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT Mac FROM nodes WHERE UniqueId = @pUniqueId";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

                conn.Open();
                string mac = cmd.ExecuteScalar().ToString();
                conn.Close();

                return mac;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        //Update the Mac in a record.If UniqueId is not present, table is unchanged.
        public void UpdateNodeMac(Guid UniqueId, string mac, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set Mac = @pMac WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pMac", mac));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Select the MaxBackupCapacity in a record. If UniqueId is not present, 0 is returned.
        public int SelectNodeMaxBackupCapacity(Guid UniqueId, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT MaxBackupCapacity FROM nodes WHERE UniqueId = @pUniqueId";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

                conn.Open();
                int maxBackupCapacity = (Int32) cmd.ExecuteScalar();
                conn.Close();

                return maxBackupCapacity;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        //Update the MaxBackupCapacity in a record.If UniqueId is not present, table is unchanged.
        public void UpdateNodeMaxBackupCapacity(Guid UniqueId, int MaxBackupCapacity, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set MaxBackupCapacity = @pMaxBackupCapacity WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pMaxBackupCapacity", MaxBackupCapacity));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Select the BackupData in a record. If UniqueId is not present, 0 is returned.
        public long SelectNodeBackupData(Guid UniqueId, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT BackupData FROM nodes WHERE UniqueId = @pUniqueId";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

                conn.Open();
                long BackupData = (long)cmd.ExecuteScalar();
                conn.Close();

                return BackupData;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }


        //Select the BackupData in a record by Name. If Name is not present, 0 is returned.
        public long SelectNodeBackupData(String Name, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT BackupData FROM nodes WHERE Name = @pName";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pName", Name));

                conn.Open();
                long BackupData = (long)cmd.ExecuteScalar();
                conn.Close();

                return BackupData;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        //Update the BackupData in a record.If UniqueId is not present, table is unchanged.
        public void UpdateNodeBackupData(Guid UniqueId, long BackupData, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set BackupData = @pBacupData WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pBackupData", BackupData));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Select the NonBackupData in a record. If UniqueId is not present, 0 is returned.
        public long SelectNodeNonBackupData(Guid UniqueId, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT NonBackupData FROM nodes WHERE UniqueId = @pUniqueId";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

                conn.Open();
                long NonBackupData = (long)cmd.ExecuteScalar();
                conn.Close();

                return NonBackupData;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        //Select the NonBackupData in a record by Name. If Name is not present, 0 is returned.
        public long SelectNodeNonBackupData(String Name, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT NonBackupData FROM nodes WHERE Name = @pName";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pName", Name));

                conn.Open();
                long NonBackupData = (long)cmd.ExecuteScalar();
                conn.Close();

                return NonBackupData;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        //Update the NonBackupData in a record.If UniqueId is not present, table is unchanged.
        public void UpdateNodeNonBackupData(Guid UniqueId, long NonBackupData, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set NonBackupData = @pNonBacupData WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pNonBackupData", NonBackupData));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Select the FreeSpace in a record. If UniqueId is not present, 0 is returned.
        public long SelectFreeSpace(Guid UniqueId, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT FreeSpace FROM nodes WHERE UniqueId = @pUniqueId";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

                conn.Open();
                long NonBackupData = (long)cmd.ExecuteScalar();
                conn.Close();

                return NonBackupData;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        //Select the FreeSpace in a record by Name. If Name is not present, 0 is returned.
        public long SelectFreeSpace(string Name, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT FreeSpace FROM nodes WHERE Name = @pName";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pName", Name));

                conn.Open();
                long NonBackupData = (long)cmd.ExecuteScalar();
                conn.Close();

                return NonBackupData;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        //Update the FreeSpace in a record.If UniqueId is not present, table is unchanged.
        public void UpdateNodeFreeSpace(Guid UniqueId, long FreeSpace, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set FreeSpace = @pFreeSpace WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pFreeSpace", FreeSpace));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Select the TotalCapacity in a record. If UniqueId is not present, 0 is returned.
        public long SelectTotalCapacity(Guid UniqueId, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT TotalCapacity FROM nodes WHERE UniqueId = @pUniqueId";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

                conn.Open();
                long NonBackupData = (long)cmd.ExecuteScalar();
                conn.Close();

                return NonBackupData;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }


        //Select the TotalCapacity in a record by Name. If Name is not present, 0 is returned.
        public long SelectTotalCapacity(string Name, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT TotalCapacity FROM nodes WHERE Name = @pName";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pName", Name));

                conn.Open();
                long NonBackupData = (long)cmd.ExecuteScalar();
                conn.Close();

                return NonBackupData;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        //Update the TotalCapacity in a record.If UniqueId is not present, table is unchanged.
        public void UpdateNodeTotalCapacity(Guid UniqueId, long TotalCapacity, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set TotalCapacity = @pTotalCapacity WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pTotalCapacity", TotalCapacity));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Select the ReliablityMetric in a record. If UniqueId is not present, 0 is returned.
        public int SelectNodeReliablityMetric(Guid UniqueId, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT ReliablityMetric FROM nodes WHERE UniqueId = @pUniqueId";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

                conn.Open();
                int ReliablityMetric = (Int32)cmd.ExecuteScalar();
                conn.Close();

                return ReliablityMetric;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        //Update the ReliablityMetric in a record.If UniqueId is not present, table is unchanged.
        public void UpdateNodeReliablityMetric(Guid UniqueId, int ReliablityMetric, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set ReliablityMetric = @pRelialibyMetric WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pReliablityMetric", ReliablityMetric));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Select the Hops in a record. If UniqueId is not present, 0 is returned.
        public int SelectNodeHops(Guid UniqueId, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT Hops FROM nodes WHERE UniqueId = @pUniqueId";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

                conn.Open();
                int Hops = (Int32)cmd.ExecuteScalar();
                conn.Close();

                return Hops;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        //Update the Hops in a record.If UniqueId is not present, table is unchanged.
        public void UpdateNodeHops(Guid UniqueId, int Hops, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set Hops = @pHops WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pHops", Hops));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Select the Smart in a record. If UniqueId is not present, 0 is returned.
        public int SelectNodeSmart(Guid UniqueId, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT Smart FROM nodes WHERE UniqueId = @pUniqueId";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

                conn.Open();
                int Smart = (Int32)cmd.ExecuteScalar();
                conn.Close();

                return Smart;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        //Update the Smart in a record.If UniqueId is not present, table is unchanged.
        public void UpdateNodeSmart(Guid UniqueId, int Smart, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set Smart = @pSmart WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pSmart", Smart));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Select the BackupsFailed in a record. If UniqueId is not present, 0 is returned.
        public int SelectNodeBackupsFailed(Guid UniqueId, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT BackupsFailed FROM nodes WHERE UniqueId = @pUniqueId";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

                conn.Open();
                int BackupsFailed = (Int32)cmd.ExecuteScalar();
                conn.Close();

                return BackupsFailed;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        //Update the BackupsFailed in a record.If UniqueId is not present, table is unchanged.
        public void UpdateNodeBackupsFailed(Guid UniqueId, int BackupsFailed, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set BackupsFailed = @pBackupsFailed WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pBackupsFailed", BackupsFailed));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Select the BackupsPassed in a record. If UniqueId is not present, 0 is returned.
        public int SelectNodeBackupsPassed(Guid UniqueId, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT BackupsPassed FROM nodes WHERE UniqueId = @pUniqueId";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

                conn.Open();
                int BackupsPassed = (Int32)cmd.ExecuteScalar();
                conn.Close();

                return BackupsPassed;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }

        //Update the BackupsPassed in a record.If UniqueId is not present, table is unchanged.
        public void UpdateNodeBackupsPassed(Guid UniqueId, int BackupsPassed, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set BackupsPassed = @pBackupsPassed WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pBackupsPassed", BackupsPassed));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Select the Name in a record. If UniqueId is not present, null is returned.
        public string SelectNodeTrusted(Guid UniqueId, SQLiteConnection conn)
        {
            try
            {
                string sql = "SELECT Trusted FROM nodes WHERE UniqueId = @pUniqueId";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

                conn.Open();
                string Trusted = cmd.ExecuteScalar().ToString();
                conn.Close();

                return Trusted;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        //Update the Status in a record.If UniqueId is not present, table is unchanged.
        public void UpdateNodeTrusted(Guid UniqueId, string Status, SQLiteConnection conn)
        {
            string sql = "UPDATE nodes Set Trusted = @pTrusted WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));
            cmd.Parameters.Add(new SQLiteParameter("@pTrusted", Status));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //Return true if primary key was found and false if not
        public bool PrimaryKeyCheck(Guid UniqueId, SQLiteConnection conn)
        {
            object value = new object();
            string sql = "SELECT UniqueId FROM nodes WHERE UniqueId = @pUniqueId";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.Add(new SQLiteParameter("@pUniqueId", UniqueId));

            conn.Open();
            value = cmd.ExecuteScalar();
            conn.Close();

            if (value != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Get the Names from the node database
        public string[] GetNodeNames(SQLiteConnection conn)
        {
            string sql = "SELECT Name  FROM nodes";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            conn.Open();
            SQLiteDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            string[] Names = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];                
                Names[i] = Convert.ToString(row["Name"]);                           
            }
            return Names;
        }

        //Get database
        public DataTable GetNodes(SQLiteConnection conn)
        {
            string sql = "Select * from nodes";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            conn.Open();
            SQLiteDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            return dt;
        }
    }

        //Defines a node and provides constructor
        public struct Node
        {
            public Guid uniqueId; 
            public string name;
            public IPAddress ip;
            public string mac;
            public int maxBackupCapacity;
            public long backupData, nonBackupData, freeSpace, totalCapacity;
            public int reliablityMetric, hops, smart, backupsFailed, backupsPassed;
            public string trusted;

            public Node(Guid uniqueId, string name, IPAddress ip, string mac, int maxBackupCapacity, long backupData, 
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
