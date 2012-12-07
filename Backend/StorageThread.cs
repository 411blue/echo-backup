using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.BZip2;

namespace Backend
{
    public struct BackupTask
    {
        //the path (file or directory) that should be backed up
        public string path;
        //the path to a directory where I should store temporary files (including trailing backslash)
        public string tempPath;
        //the id of the backup
        public long backupID;
        //the level of the backup. 0=full, 1=1st incr, 2=2nd incr, etc.
        public int level;
        //a number representing the successfulness of the backup
        public int status;
    }

    public struct Chunk
    {
        public int id;
        //basePath of the files in this chunk; includes a trailing backslash
        public string basePath;
        public LinkedList<FileInChunk> files;
        public Chunk(int id, string basePath)
        {
            this.id = id;
            this.basePath = basePath;
            files = new LinkedList<FileInChunk>();
        }
    }

    public struct FileInChunk
    {
        string relativePath;
        //the first byte of the file in the chunk
        //nonzero iff this is not the first part of the file in the chunk
        long fileStart;
    }

    /// <summary>
    /// A class that runs a thread to do compression, chunking and combination without blocking the main thread
    /// </summary>
    public class StorageThread
    {
        public const int COPY_BUFFER_SIZE = 262144;
        public const int CHUNK_SIZE = 104857600;
        private Guid guid;
        private Thread thread;
        private bool working = true, stop = false;
        private object _lock;
        private string tempDir;
        private Queue<BackupTask> backupTasks;
        private Queue<BackupTask> backupResults;

        /// <summary>
        /// Creates a StorageThread with the specified temporary directory and Guid.
        /// </summary>
        /// <param name="tempDir">The temporary directory in which to store generated files. Intermediate and output files will be stored in this directory.</param>
        /// <param name="guid">The Guid of the node to which this thread belongs. Used in filenames.</param>
        public StorageThread(string tempDir, Guid guid)
        {
            _lock = new Object();
            this.guid = guid;
            this.tempDir = tempDir;
            backupTasks = new Queue<BackupTask>();
            backupResults = new Queue<BackupTask>();
            thread = new Thread(new ThreadStart(RunStorageThread));
            thread.Start();
        }

        private void RunStorageThread()
        {
            while (true)
            {
                bool sleep = false;
                //bug prevents the processTask(task) line below from compiling because it thinks task is aunassigned if we leave the RHS of the assignment off.
                BackupTask task = new BackupTask();
                lock (_lock)
                {
                    if (stop) return;
                    if (backupTasks.Count() > 0)
                    {
                        sleep = false;
                        working = true;
                        task = backupTasks.Dequeue();
                    }
                    else
                    {
                        working = false;
                        sleep = true;
                    }
                }
                if (sleep)
                {
                    Thread.Sleep(100);
                    continue;
                }
                processTask(task);
            }
        }

        /// <summary>
        /// Adds a BackupTask to the end of the task queue. Blocks if the queue is locked by another thread.
        /// </summary>
        /// <param name="task">The BackupTask that should be processed</param>
        public void EnqueueBackupTask(BackupTask task)
        {
            lock (_lock)
            {
                backupTasks.Enqueue(task);
                working = true;
            }
        }

        public bool IsWorking()
        {
            lock (_lock)
            {
                return working;
            }
        }

        public bool IsAlive()
        {
            return thread.IsAlive;
        }

        public void RequestStop()
        {
            lock (_lock)
            {
                stop = true;
            }
        }

        private void processTask(BackupTask task)
        {
            Logger.Debug("StorageThread:processTask");
            //setup
            int chunkID = 0;
            string oFilename = task.tempPath + '\\' + guid + '_' + task.backupID + '_' + chunkID + ".tbz2";
            Stream oStream = File.Create(oFilename);
            Stream bz2Stream = new BZip2OutputStream(oStream);
            TarArchive archive = TarArchive.CreateOutputTarArchive(bz2Stream);
            archive.RootPath = task.path;
            //generate list of files
            LinkedList<string> allFiles = new LinkedList<string>();
            recursivelyFillFileQueue(ref allFiles, task.path);
            long resumeOffset = 0;
            while (true)
            { //while need more chunks
                Logger.Debug("StorageThread:processTask outer loop");
                //create new list -- append files until total file size >= chunk size
                long size = 0;
                if (allFiles.Count() == 0) break;
                string filename;
                while (true)
                { //while need more files in chunk
                    Logger.Debug("StorageThread:processTask inner loop");
                    if (allFiles.Count() == 0)
                    {
                        archive.Close();
                        break;
                    }
                    filename = allFiles.First();
                    FileInfo info = new FileInfo(filename);
                    long s = info.Length;
                    //add file header size to chunk size
                    size += 512;
                    if (size + s < CHUNK_SIZE)
                    {
                        Logger.Debug("StorageThread:processTask inner loop if 1");
                        TarEntry entry = TarEntry.CreateEntryFromFile(filename);
                        archive.WriteEntry(entry, false);
                        //if file size is not a multiple of 512, round it up to next multiple of 512
                        if (s % 512 != 0)
                        {
                            s += 512 - (s % 512);
                        }
                        size += s;
                        allFiles.RemoveFirst();
                    }
                    else
                    { //file is to big to fit in chunk in entirety
                        Logger.Debug("StorageThread:processTask inner loop else 1");
                        int partLength = CHUNK_SIZE - (int)size;
                        //split file into all parts in temp storage
                        //create entry for part of file
                        //add entry to archive
                        //add size to size
                        //somehow record where we left off
                        resumeOffset = partLength;
                        splitFileToTemp(ref allFiles, filename, tempDir, partLength);
                        TarEntry entry = TarEntry.CreateEntryFromFile(filename);
                        archive.WriteEntry(entry, false);
                        size += s;
                        archive.Close();
                        break;
                    }
                    if (size >= CHUNK_SIZE)
                    {
                        Logger.Debug("StorageThread:processTask inner loop if 2");
                        resumeOffset = 0;
                        archive.Close();
                        break;
                    }
                }
                chunkID++;
            }
        }

        private void recursivelyFillFileQueue(ref LinkedList<string> queue, string path)
        {
            Logger.Debug("StorageThread:recursivelyFillFileQueue");
            string[] names = Directory.GetFiles(path);
            foreach (string filename in names)
            {
                queue.AddLast(filename);
            }
            names = Directory.GetDirectories(path);
            foreach (string filename in names)
            {
                queue.AddLast(filename);
                recursivelyFillFileQueue(ref queue, filename);
            }
        }

        private void splitFileToTemp(ref LinkedList<String> files, string source, string dest, int firstSize)
        {
            Logger.Debug("StorageThread:splitFileToTemp");
            LinkedListNode<string> firstFile = files.First;
            byte[] buffer = new byte[COPY_BUFFER_SIZE];
            Stream input = File.OpenRead(source);
            //input.Seek(offset, SeekOrigin.Begin);
            int id = 0;
            long fileSize = new FileInfo(source).Length;
            while (fileSize > 0)
            { //while we need to make another partial file
                string outputFilename = dest + '.' + id + ".ebpart";
                Stream output = File.OpenWrite(outputFilename);
                int bytesRead = 1;
                while (firstSize > 0 && bytesRead > 0 && fileSize > 0)
                {
                    bytesRead = input.Read(buffer, 0, (int)Math.Min(Math.Min(firstSize, COPY_BUFFER_SIZE),fileSize));
                    output.Write(buffer, 0, bytesRead);
                    firstSize -= bytesRead;
                    fileSize -= bytesRead;
                }
                output.Close();
                files.AddBefore(firstFile, outputFilename);
                firstSize = CHUNK_SIZE;
            }
            input.Close();
        }
    }
}
