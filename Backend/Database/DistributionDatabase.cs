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
        /// <summary>
        /// Connects to the distribution database. Creates the database file and initial table if they do not exist already.
        /// </summary>
        /// <returns>A SQLiteConnection object representing
        /// a connection to the distribution database.</returns>
        public SQLiteConnection ConnectToDistributionDatabase()
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + @"distribution.s3db");

            if (!System.IO.File.Exists(@"distribution.s3db"))
            {
                //Create the file if it does not exist.
                conn.Open();
                conn.Close();
                //Initialize table
                CreateDistributionTable(conn);
            }

            return conn;
        }

        /// <summary>
        /// Creates Distribution table: Node_Status.
        /// </summary>
        /// <param name="conn">A SQLiteConnection object for connection to distribution database.</param>
        private void CreateDistributionTable(SQLiteConnection conn)
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
        /// <param name="conn">A SQLiteConnection object for connection to distribution database.</param>
        public void InsertNode(string guid, string status, SQLiteConnection conn)
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
        /// <param name="conn">A SQLiteConnection object for connection to distribution database.</param>
        public void UpdateStatus(string guid, string status, SQLiteConnection conn)
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
        /// <param name="conn">A SQLiteConnection object for connection to distribution database.</param>
        public void ResetStatus(SQLiteConnection conn)
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
        /// <param name="conn">A SQLiteConnection object for connection to distribution database.</param>
        /// <returns>A string with the node status for the given guid</returns>
        public string GetStatus(string guid, SQLiteConnection conn)
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
    }
}