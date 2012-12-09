using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.BZip2;

namespace Backend.Storage
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

    public struct FileInChunk
    {
        public string path;
        //the first byte of the file in the chunk
        //nonzero iff this is not the first part of the file in the chunk
        public long fileStart;
        public FileInChunk(string path, long fileStart)
        {
            this.path = path;
            this.fileStart = fileStart;
        }
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
        private Queue<Chunk> backupResults;

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
            backupResults = new Queue<Chunk>();
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

        public int NumChunks()
        {
            lock (_lock)
            {
                return backupResults.Count;
            }
        }

        public Chunk DequeueChunk()
        {
            lock (_lock)
            {
                return backupResults.Dequeue();
            }
        }

        private void processTask(BackupTask task)
        {
            Logger.Debug("StorageThread:processTask");
            //generate list of files
            LinkedList<FileInChunk> allFiles = new LinkedList<FileInChunk>();
            recursivelyFillFileQueue(ref allFiles, task.path, "");
            int chunkID = 0;
            while (true)
            { //while need more chunks
                Logger.Debug("StorageThread:processTask outer loop");
                if (allFiles.Count() == 0) break;
                string oFilename = task.tempPath + '\\' + guid + '_' + task.backupID + '_' + chunkID + ".tgz";
                TarOutputStream tarOutputStream = newTarOutputStream(oFilename);
                Chunk chunk = new Chunk(task.backupID, chunkID, task.path, oFilename);
                long size = 0;
                string relPath, fullPath;
                while (true)
                { //while need more files in chunk
                    Logger.Debug("StorageThread:processTask inner loop");
                    if (allFiles.Count() == 0)
                    {
                        tarOutputStream.Close();
                        break;
                    }
                    FileInChunk fileInChunk = allFiles.First();
                    relPath = fileInChunk.path;
                    fullPath = task.path + '\\' + relPath;
                    if (Directory.Exists(fullPath))
                    {
                        Logger.Debug("StorageThread:processTask inner loop if 1");
                        TarEntry entry2 = TarEntry.CreateTarEntry(relPath);
                        //entry2.Name = relPath;
                        entry2.TarHeader.TypeFlag = TarHeader.LF_DIR;
                        tarOutputStream.PutNextEntry(entry2);
                        chunk.AddLast(fileInChunk);
                        size += 512;
                        allFiles.RemoveFirst();
                        continue;
                    }
                    Stream inputStream = File.OpenRead(fullPath);
                    long s = inputStream.Length;
                    long toWrite = s;
                    //add file header size to chunk size
                    size += 512;
                    TarEntry entry = TarEntry.CreateTarEntry(relPath);
                    if (size + s < CHUNK_SIZE)
                    {
                        Logger.Debug("StorageThread:processTask inner loop if 2");
                        entry.Size = s;
                        tarOutputStream.PutNextEntry(entry);
                        byte[] buffer = new byte[32 * 1024];
                        int totalRead = 0;
                        while (true)
                        {
                            int numRead = inputStream.Read(buffer, 0, buffer.Length);
                            if (numRead <= 0)
                            {
                                break;
                            }
                            totalRead += numRead;
                            tarOutputStream.Write(buffer, 0, numRead);
                        }
                        tarOutputStream.CloseEntry();
                        chunk.AddLast(fileInChunk);
                        allFiles.RemoveFirst();
                        size += s;
                        //if size is not a multiple of 512, round it up to next multiple of 512
                        if (size % 512 != 0)
                        {
                            size += 512 - (size % 512);
                        }
                    }
                    else
                    { //file is too big to fit in chunk in entirety
                        Logger.Debug("StorageThread:processTask inner loop else 2");
                        long totalFileRead = 0;
                        int partLength = CHUNK_SIZE - (int)size;
                        createSplitFileInChunks(ref allFiles, partLength, s);
                        fileInChunk = allFiles.First();
                        entry.Size = partLength;
                        tarOutputStream.PutNextEntry(entry);
                        byte[] buffer = new byte[32 * 1024];
                        while (true)
                        { //while we have more chunks from this file to write
                            Logger.Debug("StorageThread:processTask inner-inner loop");
                            while (true)
                            {
                                int numRead = inputStream.Read(buffer, 0, Math.Min(buffer.Length, partLength));
                                if (numRead <= 0)
                                {
                                    break;
                                }
                                partLength -= numRead;
                                size += numRead;
                                totalFileRead += numRead;
                                tarOutputStream.Write(buffer, 0, numRead);
                            }
                            tarOutputStream.CloseEntry();
                            partLength = CHUNK_SIZE - 512;
                            chunk.AddLast(fileInChunk);
                            allFiles.RemoveFirst();
                            if (allFiles.Count == 0) break;
                            if (allFiles.First.Value.fileStart != 0)
                            {
                                Logger.Debug("StorageThread:processTask inner-inner loop if");
                                tarOutputStream.Close();
                                lock (_lock)
                                {
                                    backupResults.Enqueue(chunk);
                                }
                                chunkID++;
                                oFilename = task.tempPath + '\\' + guid + '_' + task.backupID + '_' + chunkID + ".tgz";
                                tarOutputStream = newTarOutputStream(oFilename);
                                chunk = new Chunk(task.backupID, chunkID, task.path, oFilename);
                                entry.Size = Math.Min(CHUNK_SIZE - 512, s - totalFileRead);
                                tarOutputStream.PutNextEntry(entry);
                                size = 512;
                            }
                            else
                            {
                                Logger.Debug("StorageThread:processTask inner-inner loop else");
                                break;
                            }
                        }
                    }
                    if (size >= CHUNK_SIZE)
                    {
                        Logger.Debug("StorageThread:processTask inner loop if 3");
                        tarOutputStream.Close();
                        break;
                    }
                }
                lock (_lock)
                {
                    backupResults.Enqueue(chunk);
                }
                chunkID++;
            }
        }

        private TarOutputStream newTarOutputStream(string filename)
        {
            Stream oStream = File.Create(filename);
            //gzipStream = new BZip2OutputStream(oStream);
            GZipOutputStream gzipStream = new GZipOutputStream(oStream);
            gzipStream.SetLevel(3);
            return new TarOutputStream(gzipStream);
        }

        /// <summary>
        /// Removes the first FileInChunk from the list and pushes the smaller component chunks to the front of the list.
        /// </summary>
        /// <param name="allFiles"></param>
        /// <param name="firstPartSize"></param>
        /// <param name="totalSize"></param>
        private void createSplitFileInChunks(ref LinkedList<FileInChunk> allFiles, int firstPartSize, long totalSize)
        {
            LinkedListNode<FileInChunk> first = allFiles.First;
            allFiles.RemoveFirst();
            long lastSize = (totalSize - firstPartSize) % (CHUNK_SIZE - 512);
            if (lastSize == 0) lastSize = CHUNK_SIZE - 512;
            allFiles.AddFirst(new FileInChunk(first.Value.path, totalSize - lastSize));
            lastSize += CHUNK_SIZE - 512;
            while (lastSize < totalSize)
            {
                allFiles.AddFirst(new FileInChunk(first.Value.path, totalSize - lastSize));
                lastSize += CHUNK_SIZE - 512;
            }
            allFiles.AddFirst(new FileInChunk(first.Value.path, 0));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="path"></param>
        /// <param name="recursivePath">This should usually be blank ("")</param>
        private void recursivelyFillFileQueue(ref LinkedList<FileInChunk> queue, string path, string recursivePath)
        {
            Logger.Debug("StorageThread:recursivelyFillFileQueue");
            string[] names = Directory.GetFiles(path);
            foreach (string filename in names)
            {
                queue.AddLast(new FileInChunk(recursivePath + Path.GetFileName(filename), 0));
            }
            names = Directory.GetDirectories(path);
            foreach (string filename in names)
            {
                string dir = Path.GetFileName(filename);
                queue.AddLast(new FileInChunk(dir, 0));
                recursivelyFillFileQueue(ref queue, filename, recursivePath + dir + '\\');
            }
        }

        private void splitFileToTemp(ref LinkedList<FileInChunk> files, string source, string dest, int firstSize)
        {
            Logger.Debug("StorageThread:splitFileToTemp");
            LinkedListNode<FileInChunk> firstFile = files.First;
            byte[] buffer = new byte[COPY_BUFFER_SIZE];
            Stream input = File.OpenRead(source);
            //input.Seek(offset, SeekOrigin.Begin);
            int id = 0;
            long pos = 0;
            long fileSize = new FileInfo(source).Length;
            while (fileSize > 0)
            { //while we need to make another partial file
                string outputFilename = dest + Path.GetFileName(source) + '.' + id + ".ebpart";
                Stream output = File.OpenWrite(outputFilename);
                int bytesRead = 1;
                while (firstSize > 0 && bytesRead > 0 && fileSize > 0)
                {
                    bytesRead = input.Read(buffer, 0, (int)Math.Min(Math.Min(firstSize, COPY_BUFFER_SIZE),fileSize));
                    output.Write(buffer, 0, bytesRead);
                    firstSize -= bytesRead;
                    fileSize -= bytesRead;
                    pos += bytesRead;
                }
                output.Close();
                files.AddBefore(firstFile, new FileInChunk(outputFilename, pos - firstSize));
                firstSize = CHUNK_SIZE;
                id++;
            }
            input.Close();
        }
    }
}
