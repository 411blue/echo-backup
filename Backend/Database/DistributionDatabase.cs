using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;

namespace Backend.Database
{
    public class DistributionDatabase
    {
        public DistributionDatabase()
        {
            pathAndFileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\distribution.s3db";
            conn = new SQLiteConnection("Data Source=" + pathAndFileName);

            if (!System.IO.File.Exists(pathAndFileName))
            {
                //Create the file if it does not exist.
                conn.Open();
                conn.Close();
                //Initialize tables
                CreateDistributionTable();
            }
        }

        /// <summary>
        /// Connects to the distribution database.
        /// </summary>
        /// <returns>A SQLiteConnection object representing
        /// a connection to the index database.</returns>
        public SQLiteConnection ConnectToIndexDatabase()
        {
            return conn;
        }

        public string GetPathFileName()
        {
            return pathAndFileName;
        }

        /// <summary>
        /// Creates Distribution table: Node_Status.
        /// </summary>
        private void CreateDistributionTable()
        {
            string nodeStatusSql = "CREATE TABLE IF NOT EXISTS Node_Status (guid TEXT, status TEXT, PRIMARY KEY (guid))";

            //create tables
            SQLiteCommand nodeStatusCmd = new SQLiteCommand(nodeStatusSql, conn);

            try
            {
                conn.Open();
                nodeStatusCmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                //if anything is wrong with the sql statement or the database,
                //a SQLiteException will show information about it.
                Debug.Print(ex.Message);

                //always make sure the database connection is closed.
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Inserts an entry into the database.
        /// </summary>
        /// <param name="guid">A unique string that identifies a specific node.</param>
        /// <param name="status">A string that = 'online' if the node is online and 'offline' if it is offline</param>
        public void InsertNode(string guid, string status)
        {
            string nodeStatusSql = "INSERT OR IGNORE INTO Node_Status (guid, status) VALUES (@pGUID, @pStatus)";

            SQLiteCommand nodeStatusCmd = new SQLiteCommand(nodeStatusSql, conn);

            nodeStatusCmd.Parameters.Add(new SQLiteParameter("@pGUID", guid));
            nodeStatusCmd.Parameters.Add(new SQLiteParameter("@pStatus", status));

            //open the connection, exectute the statement, and close the connection.
            try
            {
                conn.Open();
                nodeStatusCmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                //if anything is wrong with the sql statement or the database,
                //a SQLiteException will show information about it.
                Debug.Print(ex.Message);

                //always make sure the database connection is closed.
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Updates the status field for an entry.
        /// </summary>
        /// <param name="guid">A unique string that identifies a specific node.</param>
        /// <param name="status">A string to update the status field with</param>
        public void UpdateStatus(string guid, string status)
        {
            string nodeStatusSql = "UPDATE Node_Status SET status = @pStatus WHERE guid = @pGUID";

            SQLiteCommand nodeStatusCmd = new SQLiteCommand(nodeStatusSql, conn);

            nodeStatusCmd.Parameters.Add(new SQLiteParameter("@pGUID", guid));
            nodeStatusCmd.Parameters.Add(new SQLiteParameter("@pStatus", status));

            //open the connection, exectute the statement, and close the connection.
            try
            {
                conn.Open();
                nodeStatusCmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                //if anything is wrong with the sql statement or the database,
                //a SQLiteException will show information about it.
                Debug.Print(ex.Message);

                //always make sure the database connection is closed.
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Resets the status field for every entry.
        /// </summary>
        public void ResetStatus()
        {
            string nodeStatusSql = "UPDATE Node_Status SET status = @pStatus";

            SQLiteCommand nodeStatusCmd = new SQLiteCommand(nodeStatusSql, conn);

            nodeStatusCmd.Parameters.Add(new SQLiteParameter("@pStatus", @"offline"));

            //open the connection, exectute the statement, and close the connection.
            try
            {
                conn.Open();
                nodeStatusCmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                //if anything is wrong with the sql statement or the database,
                //a SQLiteException will show information about it.
                Debug.Print(ex.Message);

                //always make sure the database connection is closed.
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Given a GUID, returns the current status for that node.
        /// </summary>
        /// <param name="guid">A unique string that identifies a specific node.</param>
        /// <returns>A string with the node status for the given guid</returns>
        public string GetStatus(string guid)
        {
            string status = "";

            string statusQuery = "SELECT status FROM Node_Status WHERE guid = @pGUID";

            SQLiteCommand statusCmd = new SQLiteCommand(statusQuery, conn);

            SQLiteParameter pGUID = new SQLiteParameter("@pGUID", guid);

            statusCmd.Parameters.Add(pGUID);

            try
            {
                conn.Open();
                status = statusCmd.ExecuteScalar().ToString();
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                //if anything is wrong with the sql statement or the database,
                //a SQLiteException will show information about it.
                Debug.Print(ex.Message);

                //always make sure the database connection is closed.
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return status;
        }

        private string pathAndFileName;
        private SQLiteConnection conn;
    }
}