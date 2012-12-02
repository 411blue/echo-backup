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
        public string sourceGUID;
        public string storageGUID;
        public string storagePath;
        public long size;
        public string dateAndTime;
        public long id;

        public Block(string sourceGUID, string storageGUID, string storagePath, long size, string dateAndTime)
        {
            this.sourceGUID = sourceGUID;
            this.storageGUID = storageGUID;
            this.storagePath = storagePath;
            this.size = size;
            this.dateAndTime = dateAndTime;
            this.id = 0;
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
        public long id;

        public BackupIndex(string sourceGUID, string sourcePath, long firstBlockOffset, long size, string dateAndTime, int backupLevel)
        {
            this.sourceGUID = sourceGUID;
            this.sourcePath = sourcePath;
            this.firstBlockOffset = firstBlockOffset;
            this.size = size;
            this.dateAndTime = dateAndTime;
            this.backupLevel = backupLevel;
            this.id = 0;
        }
    }

    public struct key
    {
        public long id;
        public string guid;

        public key(long id, string guid)
        {
            this.id = id;
            this.guid = guid;
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
            string backupIndexSql = "CREATE TABLE IF NOT EXISTS Backup_Indexes (id INTEGER, source_guid TEXT, source_path TEXT, first_block_offset INTEGER, size INTEGER, date_of_backup DATETIME, backup_level INTEGER, PRIMARY KEY (id, source_guid))";
            string blockStorageSql = "CREATE TABLE IF NOT EXISTS Block_Storage (id INTEGER, source_guid TEXT, storage_guid TEXT, storage_path TEXT, size INTEGER, date_created DATETIME, PRIMARY KEY (id, source_guid))";
            string indexToBlockSql = "CREATE TABLE IF NOT EXISTS Index_to_Block (id INTEGER PRIMARY KEY ASC, index_foreign_id INTEGER, index_foreign_guid TEXT, block_foreign_id INTEGER, block_foreign_guid TEXT, FOREIGN KEY (index_foreign_id, index_foreign_guid) REFERENCES Backup_Indexes(id, source_guid), FOREIGN KEY (block_foreign_id, block_foreign_guid) REFERENCES Block_Storage(id, storage_guid))";
            
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
            long indexID = 0;
            long indexIDCount = 0;

            // Insert index into Backup_Indexes table
            string backupIndexSql = "INSERT INTO Backup_Indexes (id, source_guid, source_path, first_block_offset, size, date_of_backup, backup_level) VALUES (@pID, @pSourceGUID, @pSourcePath, @pFirstBlockOffset, @pSize, @pDateOfBackup, @pBackupLevel)";
            SQLiteCommand backupIndexCmd = new SQLiteCommand(backupIndexSql, conn);
            //Get ID for index
            //Determine if there are any entries for the given guid
            string indexInitialIDQuery = "SELECT COUNT(id) FROM Backup_Indexes WHERE source_guid = @pSourceGUID";
            SQLiteCommand indexInitialIDCmd = new SQLiteCommand(indexInitialIDQuery, conn);
            //Get previous row ID for given guid
            string indexPreviousIDQuery = "SELECT max(id) FROM Backup_Indexes WHERE source_guid = @pSourceGUID";
            SQLiteCommand indexPreviousIDCmd = new SQLiteCommand(indexPreviousIDQuery, conn);

            //SQLite likes dates and times in a certain format.
            //string currentTimeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            indexInitialIDCmd.Parameters.Add(new SQLiteParameter("@pSourceGUID", index.sourceGUID));
            indexPreviousIDCmd.Parameters.Add(new SQLiteParameter("@pSourceGUID", index.sourceGUID));
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
                indexIDCount = Convert.ToInt64(indexInitialIDCmd.ExecuteScalar());
                if (indexIDCount == 0) // If there are no entries for given guid, then this is the first row
                {
                    indexID = 1;
                }
                else //otherwise get the ID for the previous row and increment
                {
                    indexID = Convert.ToInt64(indexPreviousIDCmd.ExecuteScalar());
                    indexID++;
                }
                backupIndexCmd.Parameters.Add(new SQLiteParameter("@pID", indexID));
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

            // Go through the array of Blocks and add each to the Block_Storage table; also, for each block add an entry to Index_to_Block table
            foreach (Block currentBlock in blocks)
            {
                long blockID = 0;
                long blockIDCount = 0;

                string blockStorageSql = "INSERT INTO Block_Storage (id, source_guid, storage_guid, storage_path, size, date_created) VALUES (@pID, @pSourceGUID, @pStorageGUID, @pStoragePath, @pSize, @pDateCreated)";
                SQLiteCommand blockStorageCmd = new SQLiteCommand(blockStorageSql, conn);
                //Get ID for block
                //Determine if there are any entries for the given guid
                string blockInitialIDQuery = "SELECT COUNT(id) FROM Index_to_Block WHERE index_foreign_guid = @pSourceGUID";
                SQLiteCommand blockInitialIDCmd = new SQLiteCommand(blockInitialIDQuery, conn);
                //Get previous row ID for given guid
                string blockPreviousIDQuery = "SELECT max(block_foreign_id) FROM Index_to_Block WHERE index_foreign_guid = @pSourceGUID";
                SQLiteCommand blockPreviousIDCmd = new SQLiteCommand(blockPreviousIDQuery, conn);
                // Insert Index_to_Block entry
                string indexToBlockSql = "INSERT INTO Index_to_Block (index_foreign_id, index_foreign_guid, block_foreign_id, block_foreign_guid) VALUES (@pIndexForeignID, @pIndexForeignGUID, @pBlockForeignID, @pBlockForeignGUID)";
                SQLiteCommand indexToBlockCmd = new SQLiteCommand(indexToBlockSql, conn);

                //SQLite likes dates and times in a certain format (ISO-something or other).
                //string currentTimeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                blockInitialIDCmd.Parameters.Add(new SQLiteParameter("@pSourceGUID", index.sourceGUID));
                blockPreviousIDCmd.Parameters.Add(new SQLiteParameter("@pSourceGUID", index.sourceGUID));

                blockStorageCmd.Parameters.Add(new SQLiteParameter("@pSourceGUID", index.sourceGUID));
                blockStorageCmd.Parameters.Add(new SQLiteParameter("@pStorageGUID", currentBlock.storageGUID));
                blockStorageCmd.Parameters.Add(new SQLiteParameter("@pStoragePath", currentBlock.storagePath));
                blockStorageCmd.Parameters.Add(new SQLiteParameter("@pSize", currentBlock.size));
                blockStorageCmd.Parameters.Add(new SQLiteParameter("@pDateCreated", currentBlock.dateAndTime));

                indexToBlockCmd.Parameters.Add(new SQLiteParameter("@pIndexForeignID", indexID));
                indexToBlockCmd.Parameters.Add(new SQLiteParameter("@pIndexForeignGUID", index.sourceGUID));
                indexToBlockCmd.Parameters.Add(new SQLiteParameter("@pBlockForeignGUID", currentBlock.sourceGUID));

                //open the connection, exectute the query, and close the connection.
                try
                {
                    conn.Open();
                    blockIDCount = Convert.ToInt64(blockInitialIDCmd.ExecuteScalar());
                    if (blockIDCount == 0) // If there are no entries for given guid, then this is the first row
                    {
                        blockID = 1;
                    }
                    else //otherwise get the ID for the previous row and increment
                    {
                        blockID = Convert.ToInt64(blockPreviousIDCmd.ExecuteScalar());
                        blockID++;
                    }
                    blockStorageCmd.Parameters.Add(new SQLiteParameter("@pID", blockID));
                    blockStorageCmd.ExecuteNonQuery();
                    indexToBlockCmd.Parameters.Add(new SQLiteParameter("@pBlockForeignID", blockID));
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
            long id = 0;

            string pathQuery = "SELECT source_path FROM Backup_Indexes WHERE source_guid = @pSourceGUID AND date_of_backup = @pDateOfBackup";
            string offsetQuery = "SELECT first_block_offset FROM Backup_Indexes WHERE source_guid = @pSourceGUID AND date_of_backup = @pDateOfBackup";
            string sizeQuery = "SELECT size FROM Backup_Indexes WHERE source_guid = @pSourceGUID AND date_of_backup = @pDateOfBackup";
            string levelQuery = "SELECT backup_level FROM Backup_Indexes WHERE source_guid = @pSourceGUID AND date_of_backup = @pDateOfBackup";
            string idQuery = "SELECT id FROM Backup_Indexes WHERE source_guid = @pSourceGUID AND date_of_backup = @pDateOfBackup";

            SQLiteCommand pathCmd = new SQLiteCommand(pathQuery, conn);
            SQLiteCommand offsetCmd = new SQLiteCommand(offsetQuery, conn);
            SQLiteCommand sizeCmd = new SQLiteCommand(sizeQuery, conn);
            SQLiteCommand levelCmd = new SQLiteCommand(levelQuery, conn);
            SQLiteCommand idCmd = new SQLiteCommand(idQuery, conn);

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
            idCmd.Parameters.Add(pSourceGUID);
            idCmd.Parameters.Add(pDateOfBackup);

            try
            {
                conn.Open();
                sourcePath = pathCmd.ExecuteScalar().ToString();
                firstBlockOffset = Convert.ToInt64(offsetCmd.ExecuteScalar());
                size = Convert.ToInt64(sizeCmd.ExecuteScalar());
                backupLevel = Convert.ToInt16(levelCmd.ExecuteScalar());
                id = Convert.ToInt64(idCmd.ExecuteScalar());
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
            index.id = id;
            return index;
        }

        /// <summary>
        /// Given a backup index from the Backup_Indexes database table, provides a list of blocks from the 
        /// Block_Storage table that correspond to that index
        /// </summary>
        /// <param name="index">The index from the Backup_Indexes table</param>
        /// <param name="conn">A SQLiteConnection object for connection to index database.</param>
        /// <returns>A list of Block objects; each object corresponds to an entry in the Block_Storage database table</returns>
        public List<Block> GetBlockList(BackupIndex index, SQLiteConnection conn)
        {
            List<Block> blockList = new List<Block>();
            //Get a list of block foreign keys from the Index_to_Block table
            List<key> keyList = new List<key>();

            string query = "SELECT block_foreign_id,block_foreign_guid FROM Index_to_Block WHERE index_foreign_id = @pIndexID AND index_foreign_guid = @pIndexGUID";
            SQLiteCommand cmd = new SQLiteCommand(query, conn);

            //create a parameter for indexPrimaryKey
            SQLiteParameter pIndexID = new SQLiteParameter("@pIndexID", index.id);
            SQLiteParameter pIndexGUID = new SQLiteParameter("@pIndexGUID", index.sourceGUID);

            cmd.Parameters.Add(pIndexID);
            cmd.Parameters.Add(pIndexGUID);

            try
            {
                //open the connection
                conn.Open();

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //'reader' iterates through returned
                        //block foreign key records and each is added to list. 
                        long blockForeignID = reader.GetInt64(reader.GetOrdinal("block_foreign_id"));
                        string blockForeignGUID = reader.GetString(reader.GetOrdinal("block_foreign_guid"));
                        key currentKey = new key(blockForeignID, blockForeignGUID);
                        keyList.Add(currentKey);
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
            foreach (key currentKey in keyList)
            {
                long id = 0;
                string sourceGUID = "";
                string storageGUID = "";
                string storagePath = "";
                long size = 0;
                string dateAndTime = "";

                string blockQuery = "SELECT id,source_guid,storage_guid,storage_path,size,date_created FROM Block_Storage WHERE id = @pBlockForeignID AND source_guid = @pBlockForeignGUID";

                SQLiteCommand blockCmd = new SQLiteCommand(blockQuery, conn);

                SQLiteParameter pBlockForeignID = new SQLiteParameter("@pBlockForeignID", currentKey.id);
                SQLiteParameter pBlockForeignGUID = new SQLiteParameter("@pBlockForeignGUID", currentKey.guid);

                blockCmd.Parameters.Add(pBlockForeignID);
                blockCmd.Parameters.Add(pBlockForeignGUID);

                try
                {
                    conn.Open();

                    using (SQLiteDataReader reader = blockCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //'reader' iterates through returned block fields
                            id = reader.GetInt64(reader.GetOrdinal("id"));
                            sourceGUID = reader.GetString(reader.GetOrdinal("source_guid"));
                            storageGUID = reader.GetString(reader.GetOrdinal("storage_guid"));
                            storagePath = reader.GetString(reader.GetOrdinal("storage_path"));
                            size = reader.GetInt64(reader.GetOrdinal("size"));
                            dateAndTime = reader.GetString(reader.GetOrdinal("date_created"));
                        }
                    }

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

                Block currentBlock = new Block(sourceGUID, storageGUID, storagePath, size, dateAndTime);
                currentBlock.id = id;
                blockList.Add(currentBlock);
            }

            //sort into ascending order
            blockList.Sort((block1, block2) => block1.id.CompareTo(block2.id));
            return blockList;
        }

        private key RemoveBackupIndex(string sourceGUID, string dateTimeOfBackup, SQLiteConnection conn)
        {
            key indexPrimaryKey = new key(0, sourceGUID);

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
                indexPrimaryKey.id = Convert.ToInt64(indexKeyCmd.ExecuteScalar());
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

        private List<key> RemoveIndexToBlocks(key indexForeignKey, SQLiteConnection conn)
        {
            List<key> blockKeys = new List<key>();

            string blockKeyQuery = "SELECT block_foreign_id,block_foreign_guid FROM Index_to_Block WHERE index_foreign_id = @pIndexForeignID AND index_foreign_guid = @pIndexForeignGUID";
            string indexToBlockSql = "DELETE FROM Index_to_Block WHERE index_foreign_id = @pIndexForeignID AND index_foreign_guid = @pIndexForeignGUID";

            SQLiteCommand blockKeyCmd = new SQLiteCommand(blockKeyQuery, conn);
            SQLiteCommand indexToBlockCmd = new SQLiteCommand(indexToBlockSql, conn);

            SQLiteParameter pIndexForeignID = new SQLiteParameter("@pIndexForeignID", indexForeignKey.id);
            SQLiteParameter pIndexForeignGUID = new SQLiteParameter("@pIndexForeignGUID", indexForeignKey.guid);

            blockKeyCmd.Parameters.Add(pIndexForeignID);
            indexToBlockCmd.Parameters.Add(pIndexForeignID);
            blockKeyCmd.Parameters.Add(pIndexForeignGUID);
            indexToBlockCmd.Parameters.Add(pIndexForeignGUID);

            try
            {
                conn.Open();
                using (SQLiteDataReader reader = blockKeyCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //'reader' iterates through returned
                        //block_foreign_key records and each is added to list. 
                        long blockForeignID = reader.GetInt64(reader.GetOrdinal("block_foreign_id"));
                        string blockForeignGUID = reader.GetString(reader.GetOrdinal("block_foreign_guid"));
                        key currentKey = new key(blockForeignID, blockForeignGUID);
                        blockKeys.Add(currentKey);
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

        private void RemoveBlocks(List<key> blockKeys, SQLiteConnection conn)
        {
            foreach (key currentBlockKey in blockKeys)
            {
                string blockStorageSql = "DELETE FROM Block_Storage WHERE id = @pBlockPrimaryID AND source_guid = @pBlockPrimaryGUID";

                SQLiteCommand blockStorageCmd = new SQLiteCommand(blockStorageSql, conn);

                SQLiteParameter pBlockPrimaryID = new SQLiteParameter("@pBlockPrimaryID", currentBlockKey.id);
                SQLiteParameter pBlockPrimaryGUID = new SQLiteParameter("@pBlockPrimaryGUID", currentBlockKey.guid);

                blockStorageCmd.Parameters.Add(pBlockPrimaryID);
                blockStorageCmd.Parameters.Add(pBlockPrimaryGUID);

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
            cleanList.Sort((index1, index2) => index2.dateTime.CompareTo(index1.dateTime));

            DateTime? mostRecentFullBackup = null;
            int currentIncrementalCount = 0; // keep track of incremental backups that other backups will depend on

            //Remove from the list indexes that do not qualify for removal from the database
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

            //All indexes in the list at this point qualify for removal from the database
            //Remove all indexes in the list from the database
            foreach (dateTimeLevel currentIndex in cleanList)
            {
                List<key> blockKeys = new List<key>();
                string dateTimeOfBackup = currentIndex.dateTime.ToString("yyyy-MM-dd HH:mm:ss");

                //Remove entries from Backup_Indexes table
                key indexPrimaryKey = RemoveBackupIndex(guid, dateTimeOfBackup, conn);

                //Remove entries from Index_to_Block table
                blockKeys = RemoveIndexToBlocks(indexPrimaryKey, conn);

                //Remove entries from Block_Storage table
                RemoveBlocks(blockKeys, conn);
            }
        }
    }
}
