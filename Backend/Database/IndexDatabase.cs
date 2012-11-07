using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;

namespace Backend.Database
{
    public struct Block
    {
        public long sequence;
        public string storageGUID;
        public string storagePath;
        public long size;
        public string dateAndTime;
        public long primaryKey;

        public Block(long sequence, string storageGUID, string storagePath, long size, string dateAndTime)
        {
            this.sequence = sequence;
            this.storageGUID = storageGUID;
            this.storagePath = storagePath;
            this.size = size;
            this.dateAndTime = dateAndTime;
            this.primaryKey = 0;
        }
    }

    public struct BackupIndex
    {
        public string sourceGUID;
        public string sourcePath;
        public long firstBlockOffset;
        public long size;
        public string dateAndTime;
        public int backupLevel;
        public long primaryKey;

        public BackupIndex(string sourceGUID, string sourcePath, long firstBlockOffset, long size, string dateAndTime, int backupLevel)
        {
            this.sourceGUID = sourceGUID;
            this.sourcePath = sourcePath;
            this.firstBlockOffset = firstBlockOffset;
            this.size = size;
            this.dateAndTime = dateAndTime;
            this.backupLevel = backupLevel;
            this.primaryKey = 0;
        }
    }

    public class IndexDatabase
    {
        /// <summary>
        /// Connects to the index database. Creates the database file and initial tables if they do not exist already.
        /// </summary>
        /// <returns>A SQLiteConnection object representing
        /// a connection to the index database.</returns>
        public SQLiteConnection ConnectToIndexDatabase()
        {
            //Connect to index database.
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + @"indexes.s3db");

            if (!System.IO.File.Exists(@"indexes.s3db"))
            {
                //Create the file if it does not exist.
                conn.Open();
                conn.Close();
                //Initialize tables
                CreateIndexTables(conn);
            }            

            return conn;
        }

        /// <summary>
        /// Creates Index tables: Backup Indexes, Index-to-Block, and Block Storage.
        /// </summary>
        /// <param name="conn">A SQLiteConnection object for connection to index database.</param>
        private void CreateIndexTables(SQLiteConnection conn)
        {
            //sqlite statements for creating each table
            string backupIndexSql = "CREATE TABLE IF NOT EXISTS Backup_Indexes (id INTEGER PRIMARY KEY ASC, source_guid TEXT, source_path TEXT, first_block_offset INTEGER, size INTEGER, date_of_backup DATETIME, backup_level INTEGER)";
            string blockStorageSql = "CREATE TABLE IF NOT EXISTS Block_Storage (id INTEGER PRIMARY KEY ASC, storage_guid TEXT, storage_path TEXT, size INTEGER, date_created DATETIME)";
            string indexToBlockSql = "CREATE TABLE IF NOT EXISTS Index_to_Block (id INTEGER PRIMARY KEY ASC, index_foreign_key INTEGER, block_foreign_key INTEGER, sequence INTEGER, FOREIGN KEY (index_foreign_key) REFERENCES Backup_Indexes(id), FOREIGN KEY (block_foreign_key) REFERENCES Block_Storage(id))";

            //create tables
            SQLiteCommand backupIndexCmd = new SQLiteCommand(backupIndexSql, conn);
            SQLiteCommand blockStorageCmd = new SQLiteCommand(blockStorageSql, conn);
            SQLiteCommand indexToBlockCmd = new SQLiteCommand(indexToBlockSql, conn);
            try
            {
                conn.Open();
                backupIndexCmd.ExecuteNonQuery();
                blockStorageCmd.ExecuteNonQuery();
                indexToBlockCmd.ExecuteNonQuery();
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
        /// Inserts an index into the database.
        /// </summary>
        /// <param name="index">A BackupIndex object to be added to the Backup_Indexes table.</param>
        /// <param name="blocks">An array of Block objects to be added to the Block_Storage table</param>
        /// <param name="conn">A SQLiteConnection object for connection to index database.</param>
        public void InsertIndex(BackupIndex index, List<Block> blocks, SQLiteConnection conn)
        {
            long indexForeignKey = 0;

            // Insert index into Backup_Indexes table
            string backupIndexSql = "INSERT INTO Backup_Indexes (source_guid, source_path, first_block_offset, size, date_of_backup, backup_level) VALUES (@pSourceGUID, @pSourcePath, @pFirstBlockOffset, @pSize, @pDateOfBackup, @pBackupLevel)";
            SQLiteCommand backupIndexCmd = new SQLiteCommand(backupIndexSql, conn);
            //Get primary key for index entry to insert into Index_to_Block entry
            string indexPrimaryQuery = "SELECT last_insert_rowid()";
            SQLiteCommand indexPrimaryCmd = new SQLiteCommand(indexPrimaryQuery, conn);

            //SQLite likes dates and times in a certain format.
            //string currentTimeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            backupIndexCmd.Parameters.Add(new SQLiteParameter("@pSourceGUID", index.sourceGUID));
            backupIndexCmd.Parameters.Add(new SQLiteParameter("@pSourcePath", index.sourcePath));
            backupIndexCmd.Parameters.Add(new SQLiteParameter("@pFirstBlockOffset", index.firstBlockOffset));
            backupIndexCmd.Parameters.Add(new SQLiteParameter("@pSize", index.size));
            backupIndexCmd.Parameters.Add(new SQLiteParameter("@pDateOfBackup", index.dateAndTime));
            backupIndexCmd.Parameters.Add(new SQLiteParameter("@pBackupLevel", index.backupLevel));

            //open the connection, exectute the query, and close the connection.
            try
            {
                conn.Open();
                backupIndexCmd.ExecuteNonQuery();
                indexForeignKey = Convert.ToInt64(indexPrimaryCmd.ExecuteScalar());
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

            // Go through the array of Blocks and add each to the Block_Storage table; also, for each block add an entry to Index_to_Block table
            foreach (Block currentBlock in blocks)
            {
                long blockForeignKey = 0;

                string blockStorageSql = "INSERT INTO Block_Storage (storage_guid, storage_path, size, date_created) VALUES (@pStorageGUID, @pStoragePath, @pSize, @pDateCreated)";
                SQLiteCommand blockStorageCmd = new SQLiteCommand(blockStorageSql, conn);
                //Get primary key for the most recent Block_Storage entry to insert into corresponding Index_to_Block entry
                string blockPrimaryQuery = "SELECT last_insert_rowid()";
                SQLiteCommand blockPrimaryCmd = new SQLiteCommand(blockPrimaryQuery, conn);

                //SQLite likes dates and times in a certain format (ISO-something or other).
                //string currentTimeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                blockStorageCmd.Parameters.Add(new SQLiteParameter("@pStorageGUID", currentBlock.storageGUID));
                blockStorageCmd.Parameters.Add(new SQLiteParameter("@pStoragePath", currentBlock.storagePath));
                blockStorageCmd.Parameters.Add(new SQLiteParameter("@pSize", currentBlock.size));
                blockStorageCmd.Parameters.Add(new SQLiteParameter("@pDateCreated", currentBlock.dateAndTime));

                //open the connection, exectute the query, and close the connection.
                try
                {
                    conn.Open();
                    blockStorageCmd.ExecuteNonQuery();
                    blockForeignKey = Convert.ToInt64(blockPrimaryCmd.ExecuteScalar());
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

                // Insert Index_to_Block entry
                string indexToBlockSql = "INSERT INTO Index_to_Block (index_foreign_key, block_foreign_key, sequence) VALUES (@pIndexForeignKey, @pBlockForeignKey, @pSequence)";
                SQLiteCommand indexToBlockCmd = new SQLiteCommand(indexToBlockSql, conn);

                indexToBlockCmd.Parameters.Add(new SQLiteParameter("@pIndexForeignKey", indexForeignKey));
                indexToBlockCmd.Parameters.Add(new SQLiteParameter("@pBlockForeignKey", blockForeignKey));
                indexToBlockCmd.Parameters.Add(new SQLiteParameter("@pSequence", currentBlock.sequence));

                //open the connection, exectute the query, and close the connection.
                try
                {
                    conn.Open();
                    indexToBlockCmd.ExecuteNonQuery();
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
        }
    }
}
