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

            //by opening and closing a connection to a file that does not exist, we create the file.
            conn.Open();
            conn.Close();

            return conn;
        }

        /// <summary>Returns a connection to an existing database.</summary>
        /// <param name="pathAndFileName"></param>
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

        /// <summary>Creates a table containing 'Nodes' records.</summary>
        /// <param name="conn">A SQLiteConnection object.</param>
        public void CreateNodeTable(SQLiteConnection conn)
        {
            //This sql query creates a nodes table.
            string sql = "CREATE TABLE nodes (uniqueId TEXT PRIMARY KEY ASC, name TEXT, ip TEXT, mac TEXT, maxBacupCapacity INTEGER,"
                + " backupData INTEGER, nonBackupData INTEGER, freeSpace INTEGER, totalCapacity INTEGER, reliabilityMetric INTEGER,"
                + " hops INTEGER, smart INTEGER, backupFailed INTEGER, backupPassed INTEGER, status TEXT)";

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
                //if anything is wrong with the sql statement or the database, SQLiteException will show information about it.
                Debug.Print(ex.Message);

                //always make sure the database connection is closed.
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public void InsertDVDRecord(string dvdTitle, string dvdGenre, SQLiteConnection conn)
        {
            string sql = "INSERT INTO dvds (title, genre, date_added) VALUES (@pTitle, @pGenre, @pDateAdded)";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            //We know the title and the genre, so let's get the date for the date_added field.
            //SQLite likes dates and times in a certain format (ISO-something or other).
            //This is given as the parameter to ToString()
            string currentTimeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            cmd.Parameters.Add(new SQLiteParameter("@pTitle", dvdTitle));
            cmd.Parameters.Add(new SQLiteParameter("@pGenre", dvdGenre));
            cmd.Parameters.Add(new SQLiteParameter("@pDateAdded", currentTimeString));

            //open the connection, exectute the query, and close the connection.
            //should probably be a try/catch block here
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void SetNode(string s1, string s2, string s3, string s4, int i1, long l1 
            ,long l2, long l3, long l4, int i2, int i3, int i4, int i5 ,int i6, string s5)
        {

        }
    }

    public struct Node
    {
        public string uniqueId, name, ip, mac;
        public int maxBackupCapacity;
        public long backupData, nonBackupData, freeSpace, totalCapacity;
        public int relialibyMetric, hops, smart, backupsFailed, backupsPassed;
        public string status;

        public Node(string s1, string s2, string s3, string s4, int i1, long l1, long l2, long l3, long l4, int i2, int i3, int i4, 
            int i5, int i6, string s5)
        {
            uniqueId = s1;
            name = s2;
            ip = s3;
            mac = s4;
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
            status = s5;
        }
    }

}
