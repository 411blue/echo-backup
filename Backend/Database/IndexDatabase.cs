using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Diagnostics;
using System.Data;

namespace Backend
{
    public struct Block
    {
        public long sequence;
        public string storageGUID;
        public string storagePath;
        public long size;
        public string dateAndTime;

        public Block(long sequence, string storageGUID, string storagePath, long size, string dateAndTime)
        {
            this.sequence = sequence;
            this.storageGUID = storageGUID;
            this.storagePath = storagePath;
            this.size = size;
            this.dateAndTime = dateAndTime;
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

    public struct dateTimeLevel
    {
        public DateTime dateTime;
        public int backupLevel;

        public dateTimeLevel(DateTime dateTime, int backupLevel)
        {
            this.dateTime = dateTime;
            this.backupLevel = backupLevel;
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
        /// <param name="blocks">A list of Block objects to be added to the Block_Storage table</param>
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

        /// <summary>
        /// Given a GUID, provides a list of dates/times; each date/time corresponds to the index for a backup initiated 
        /// by the source host (identified by the given GUID)
        /// </summary>
        /// <param name="sourceGUID">A unique string that identifies a specific source host.</param>
        /// <param name="conn">A SQLiteConnection object for connection to index database.</param>
        /// <returns>A list of dates/times.</returns>
        public List<string> GetIndexList(string sourceGUID, SQLiteConnection conn)
        {
            List<string> indexList = new List<string>();

            string query = "SELECT date_of_backup FROM Backup_Indexes WHERE source_guid = @pSourceGUID";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);

            //create a parameter for sourceGUID
            SQLiteParameter pSourceGUID = new SQLiteParameter("@pSourceGUID", sourceGUID);

            cmd.Parameters.Add(pSourceGUID);

            try
            {
                //open the connection
                conn.Open();

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //'reader' iterates through returned
                        //date/time records and each is added to list. 
                        string indexDateTime = reader.GetString(0);
                        indexList.Add(indexDateTime);
                    }
                }

                //close the connection
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

            return indexList;
        }

        /// <summary>
        /// Given a GUID and backup date/time, provides the corresponding index from the Backup_Indexes database table
        /// </summary>
        /// <param name="sourceGUID">A unique string that identifies a specific source host.</param>
        /// <param name="dateTimeOfBackup">The date/time of the desired backup; also the value in the date_of_backup field of the
        /// Backup_Indexes database table</param>
        /// <param name="conn">A SQLiteConnection object for connection to index database.</param>
        /// <returns>A BackupIndex from the Backup_Indexes database table</returns>
        public BackupIndex GetBackupIndex(string sourceGUID, string dateTimeOfBackup, SQLiteConnection conn)
        {
            string sourcePath = "";
            long firstBlockOffset = 0;
            long size = 0;
            int backupLevel = 0;
            long primaryKey = 0;

            string pathQuery = "SELECT source_path FROM Backup_Indexes WHERE source_guid = @pSourceGUID AND date_of_backup = @pDateOfBackup";
            string offsetQuery = "SELECT first_block_offset FROM Backup_Indexes WHERE source_guid = @pSourceGUID AND date_of_backup = @pDateOfBackup";
            string sizeQuery = "SELECT size FROM Backup_Indexes WHERE source_guid = @pSourceGUID AND date_of_backup = @pDateOfBackup";
            string levelQuery = "SELECT backup_level FROM Backup_Indexes WHERE source_guid = @pSourceGUID AND date_of_backup = @pDateOfBackup";
            string keyQuery = "SELECT id FROM Backup_Indexes WHERE source_guid = @pSourceGUID AND date_of_backup = @pDateOfBackup";

            SQLiteCommand pathCmd = new SQLiteCommand(pathQuery, conn);
            SQLiteCommand offsetCmd = new SQLiteCommand(offsetQuery, conn);
            SQLiteCommand sizeCmd = new SQLiteCommand(sizeQuery, conn);
            SQLiteCommand levelCmd = new SQLiteCommand(levelQuery, conn);
            SQLiteCommand keyCmd = new SQLiteCommand(keyQuery, conn);

            SQLiteParameter pSourceGUID = new SQLiteParameter("@pSourceGUID", sourceGUID);
            SQLiteParameter pDateOfBackup = new SQLiteParameter("@pDateOfBackup", dateTimeOfBackup);

            pathCmd.Parameters.Add(pSourceGUID);
            pathCmd.Parameters.Add(pDateOfBackup);
            offsetCmd.Parameters.Add(pSourceGUID);
            offsetCmd.Parameters.Add(pDateOfBackup);
            sizeCmd.Parameters.Add(pSourceGUID);
            sizeCmd.Parameters.Add(pDateOfBackup);
            levelCmd.Parameters.Add(pSourceGUID);
            levelCmd.Parameters.Add(pDateOfBackup);
            keyCmd.Parameters.Add(pSourceGUID);
            keyCmd.Parameters.Add(pDateOfBackup);

            try
            {
                conn.Open();
                sourcePath = pathCmd.ExecuteScalar().ToString();
                firstBlockOffset = Convert.ToInt64(offsetCmd.ExecuteScalar());
                size = Convert.ToInt64(sizeCmd.ExecuteScalar());
                backupLevel = Convert.ToInt16(levelCmd.ExecuteScalar());
                primaryKey = Convert.ToInt64(keyCmd.ExecuteScalar());
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

            BackupIndex index = new BackupIndex(sourceGUID, sourcePath, firstBlockOffset, size, dateTimeOfBackup, backupLevel);
            index.primaryKey = primaryKey;
            return index;
        }

        /// <summary>
        /// Given the primary key for an entry in the Backup_Indexes database table, provides a list of blocks from the 
        /// Block_Storage table that correspond to that entry
        /// </summary>
        /// <param name="indexPrimaryKey">The primary key from the Backup_Indexes table</param>
        /// <param name="conn">A SQLiteConnection object for connection to index database.</param>
        /// <returns>A list of Block objects; each object corresponds to an entry in the Block_Storage database table</returns>
        public List<Block> GetBlockList(long indexPrimaryKey, SQLiteConnection conn)
        {
            List<Block> blockList = new List<Block>();
            //Get a list of block foreign keys from the Index_to_Block table
            List<long> keyList = new List<long>();

            string query = "SELECT block_foreign_key FROM Index_to_Block WHERE index_foreign_key = @pIndexKey";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);

            //create a parameter for indexPrimaryKey
            SQLiteParameter pIndexKey = new SQLiteParameter("@pIndexKey", indexPrimaryKey);

            cmd.Parameters.Add(pIndexKey);

            try
            {
                //open the connection
                conn.Open();

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //'reader' iterates through returned
                        //block_foreign_key records and each is added to list. 
                        long blockForeignKey = reader.GetInt64(0);
                        keyList.Add(blockForeignKey);
                    }
                }

                //close the connection
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

            //For each block_foreign_key, extract the block from the database and add it to the list of Blocks
            foreach (long currentKey in keyList)
            {
                long sequence = 0;
                string storageGUID = "";
                string storagePath = "";
                long size = 0;
                string dateAndTime = "";
                
                string sequenceQuery = "SELECT sequence FROM Index_to_Block WHERE block_foreign_key = @pBlockForeignKey";
                string guidQuery = "SELECT storage_guid FROM Block_Storage WHERE id = @pBlockForeignKey";
                string pathQuery = "SELECT storage_path FROM Block_Storage WHERE id = @pBlockForeignKey";
                string sizeQuery = "SELECT size FROM Block_Storage WHERE id = @pBlockForeignKey";
                string dateTimeQuery = "SELECT date_created FROM Block_Storage WHERE id = @pBlockForeignKey";

                SQLiteCommand sequenceCmd = new SQLiteCommand(sequenceQuery, conn);
                SQLiteCommand guidCmd = new SQLiteCommand(guidQuery, conn);
                SQLiteCommand pathCmd = new SQLiteCommand(pathQuery, conn);
                SQLiteCommand sizeCmd = new SQLiteCommand(sizeQuery, conn);
                SQLiteCommand dateTimeCmd = new SQLiteCommand(dateTimeQuery, conn);

                SQLiteParameter pBlockForeignKey = new SQLiteParameter("@pBlockForeignKey", currentKey);

                sequenceCmd.Parameters.Add(pBlockForeignKey);
                guidCmd.Parameters.Add(pBlockForeignKey);
                pathCmd.Parameters.Add(pBlockForeignKey);
                sizeCmd.Parameters.Add(pBlockForeignKey);
                dateTimeCmd.Parameters.Add(pBlockForeignKey);

                try
                {
                    conn.Open();
                    sequence = Convert.ToInt64(sequenceCmd.ExecuteScalar());
                    storageGUID = guidCmd.ExecuteScalar().ToString();
                    storagePath = pathCmd.ExecuteScalar().ToString();
                    size = Convert.ToInt64(sizeCmd.ExecuteScalar());
                    dateAndTime = dateTimeCmd.ExecuteScalar().ToString();
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

                Block currentBlock = new Block(sequence, storageGUID, storagePath, size, dateAndTime);
                blockList.Add(currentBlock);
            }
            
            return blockList;
        }

        private long RemoveBackupIndex(string sourceGUID, string dateTimeOfBackup, SQLiteConnection conn)
        {
            long indexPrimaryKey = 0;

            string indexKeyQuery = "SELECT id FROM Backup_Indexes WHERE source_guid = @pSourceGUID AND date_of_backup = @pDateTimeOfBackup";
            string backupIndexSql = "DELETE FROM Backup_Indexes WHERE source_guid = @pSourceGUID AND date_of_backup = @pDateTimeOfBackup";

            SQLiteCommand indexKeyCmd = new SQLiteCommand(indexKeyQuery, conn);
            SQLiteCommand backupIndexCmd = new SQLiteCommand(backupIndexSql, conn);

            SQLiteParameter pSourceGUID = new SQLiteParameter("@pSourceGUID", sourceGUID);
            SQLiteParameter pDateOfBackup = new SQLiteParameter("@DateTimeOfBackup", dateTimeOfBackup);

            indexKeyCmd.Parameters.Add(pSourceGUID);
            indexKeyCmd.Parameters.Add(pDateOfBackup);
            backupIndexCmd.Parameters.Add(pSourceGUID);
            backupIndexCmd.Parameters.Add(pDateOfBackup);

            try
            {
                conn.Open();
                indexPrimaryKey = Convert.ToInt64(indexKeyCmd.ExecuteScalar());
                backupIndexCmd.ExecuteNonQuery();
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

            return indexPrimaryKey;
        }

        private List<long> RemoveIndexToBlocks(long indexForeignKey, SQLiteConnection conn)
        {
            List<long> blockKeys = new List<long>();

            string blockKeyQuery = "SELECT block_foreign_key FROM Index_to_Block WHERE index_foreign_key = @pIndexForeignKey";
            string indexToBlockSql = "DELETE FROM Index_to_Block WHERE index_foreign_key = @pIndexForeignKey";

            SQLiteCommand blockKeyCmd = new SQLiteCommand(blockKeyQuery, conn);
            SQLiteCommand indexToBlockCmd = new SQLiteCommand(indexToBlockSql, conn);

            SQLiteParameter pIndexForeignKey = new SQLiteParameter("@pIndexForeignKey", indexForeignKey);

            blockKeyCmd.Parameters.Add(pIndexForeignKey);
            indexToBlockCmd.Parameters.Add(pIndexForeignKey);

            try
            {
                conn.Open();
                using (SQLiteDataReader reader = blockKeyCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //'reader' iterates through returned
                        //block_foreign_key records and each is added to list. 
                        long blockForeignKey = reader.GetInt64(0);
                        blockKeys.Add(blockForeignKey);
                    }
                }
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

            return blockKeys;
        }

        private void RemoveBlocks(List<long> blockKeys, SQLiteConnection conn)
        {
            foreach (long currentBlockKey in blockKeys)
            {
                string blockStorageSql = "DELETE FROM Block_Storage WHERE id = @pBlockPrimaryKey";

                SQLiteCommand blockStorageCmd = new SQLiteCommand(blockStorageSql, conn);

                SQLiteParameter pBlockPrimaryKey = new SQLiteParameter("@pBlockPrimaryKey", currentBlockKey);

                blockStorageCmd.Parameters.Add(pBlockPrimaryKey);

                try
                {
                    conn.Open();
                    blockStorageCmd.ExecuteNonQuery();
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

        /// <summary>
        /// Given a guid and either the default or user configured lifetime of an index, remove all indexes that meet the following 
        /// conditions: (1) the index's age is equal to or greater than the given lifetime, (2) the index is not for the most 
        /// recent full backup or a full backup that non-expired incremental backups depend on, and (3) the index is not for 
        /// an incremental backup that other backups depend on (4) the index is for a backup initiated by the source host 
        /// identified by the given guid
        /// </summary>
        /// <param name="guid">A unique string that identifies a specific source host.</param>
        /// <param name="indexLifeTime">The lifetime of an index in months</param>
        /// <param name="conn">A SQLiteConnection object for connection to index database.</param>
        public void CleanIndexes(string guid, int indexLifeTime, SQLiteConnection conn)
        {

            //Get a list of date/times for backups initiated by the node with the given guid
            List<String> indexList = new List<String>(GetIndexList(guid, conn));
            List<dateTimeLevel> cleanList = new List<dateTimeLevel>();

            //Compile a list of backup indexes to be cleaned, identified by date/time and backup level
            foreach (string currentDateTime in indexList)
            {
                int backupLevel = 0;

                string levelQuery = "SELECT backup_level from Backup_Indexes WHERE source_guid = @pGUID AND date_of_backup = @pDateOfBackup";

                SQLiteCommand levelCmd = new SQLiteCommand(levelQuery, conn);

                SQLiteParameter pguid = new SQLiteParameter("@pGUID", guid);
                SQLiteParameter pdateOfBackup = new SQLiteParameter("@DateOfBackup", currentDateTime);

                levelCmd.Parameters.Add(pguid);
                levelCmd.Parameters.Add(pdateOfBackup);

                try
                {
                    conn.Open();
                    backupLevel = Convert.ToInt16(levelCmd.ExecuteScalar());
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

                dateTimeLevel currentIndex = new dateTimeLevel(Convert.ToDateTime(currentDateTime), backupLevel);
                cleanList.Add(currentIndex);
            }

            //Sort into descending order by date/time of backup
            cleanList.Sort((a, b) => b.dateTime.CompareTo(a.dateTime));

            DateTime? mostRecentFullBackup = null;
            int currentIncrementalCount = 0; // keep track of incremental backups that other backups will depend on

            //Remove from the list indexes that do not qualify for removal
            foreach (dateTimeLevel currentIndex in cleanList)
            {
                TimeSpan indexAge = DateTime.Now - currentIndex.dateTime;
                double indexAgeMonths = indexAge.TotalDays / 31.0;

                if (!mostRecentFullBackup.HasValue && currentIndex.backupLevel > 0) //index is for incremental backups dependent on the most recent full backup
                {
                    cleanList.Remove(currentIndex);
                }
                else if (!mostRecentFullBackup.HasValue) //index is for the most recent full backup
                {
                    mostRecentFullBackup = currentIndex.dateTime;
                    cleanList.Remove(currentIndex);
                }
                else
                {
                    if (indexAgeMonths < indexLifeTime) // is the index expired
                    {
                        cleanList.Remove(currentIndex);

                        if (currentIndex.backupLevel > 0)
                        {
                            currentIncrementalCount++;
                        }
                    }
                    else if (currentIncrementalCount > 0) // is the index for a backup that incremental backups depend on
                    {
                        cleanList.Remove(currentIndex);

                        if (currentIndex.backupLevel == 0)
                        {
                            currentIncrementalCount = 0;
                        }
                    }
                }
            }

            //All indexes in the list at this point qualify for removal 
            //Remove all indexes in the list
            foreach (dateTimeLevel currentIndex in cleanList)
            {
                long indexPrimaryKey = 0;
                List<long> blockKeys = new List<long>();
                string dateTimeOfBackup = currentIndex.dateTime.ToString("yyyy-MM-dd HH:mm:ss");

                //Remove entries from Backup_Indexes table
                indexPrimaryKey = RemoveBackupIndex(guid, dateTimeOfBackup, conn);

                //Remove entries from Index_to_Block table
                blockKeys = RemoveIndexToBlocks(indexPrimaryKey, conn);

                //Remove entries from Block_Storage table
                RemoveBlocks(blockKeys, conn);
            }
        }
    }
}
