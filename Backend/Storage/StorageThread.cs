using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.BZip2;

namespace Backend.Storage
{
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
        private Queue<StorageTask> storageTasks;
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
            storageTasks = new Queue<StorageTask>();
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
                StorageTask task = null;
                lock (_lock)
                {
                    if (stop) return;
                    if (storageTasks.Count() > 0)
                    {
                        sleep = false;
                        working = true;
                        task = storageTasks.Dequeue();
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
                storageTasks.Enqueue(task);
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
                if (backupResults.Count != 0) return backupResults.Dequeue();
                return null;
            }
        }

        private void processTask(StorageTask task)
        {
            if (task is BackupTask)
            {
                processTask((BackupTask)task);
            }
            else if (task is RestoreTask)
            {
                processTask((RestoreTask)task);
            }
            else
            {
                Logger.Error("StorageThread:processTask:StorageTask What kind of StorageTask is this?");
            }
        }

        private void processTask(BackupTask task)
        {
            Logger.Debug("StorageThread:processTask:BackupTask");
            //generate list of files
            LinkedList<FileInChunk> allFiles = new LinkedList<FileInChunk>();
            recursivelyFillFileQueue(ref allFiles, task.Path, "");
            int chunkID = 0;
            while (true)
            { //while need more chunks
                Logger.Debug("StorageThread:processTask outer loop");
                if (allFiles.Count() == 0) break;
                string oFilename = task.TempPath + '\\' + guid + '_' + task.BackupID + '_' + chunkID + ".tgz";
                TarOutputStream tarOutputStream = newTarOutputStream(oFilename);
                Chunk chunk = new Chunk(task.BackupID, chunkID, task.Path, oFilename);
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
                    fullPath = task.Path + '\\' + relPath;
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
                                oFilename = task.TempPath + '\\' + guid + '_' + task.BackupID + '_' + chunkID + ".tgz";
                                tarOutputStream = newTarOutputStream(oFilename);
                                chunk = new Chunk(task.BackupID, chunkID, task.Path, oFilename);
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

        private void processTask(RestoreTask task)
        {
            Logger.Debug("StorageThread:processTask:RestoreTask");
            Stream inStream = File.OpenRead(task.ArchivePath);
            Stream gzipStream = new GZipInputStream(inStream);
            TarInputStream tarStream = new TarInputStream(gzipStream);
            TarEntry entry;
            List<string> list = task.RelativeFilenames();
            while ((entry = tarStream.GetNextEntry()) != null)
            {
                if (entry.IsDirectory) continue;
                if (list.IndexOf(entry.Name) != -1)
                {
                    string name = entry.Name.Replace('/', Path.DirectorySeparatorChar);
                    name = Path.Combine(task.OutputDir, name);
                    Directory.CreateDirectory(Path.GetDirectoryName(name));
                    FileStream outStream = new FileStream(name, FileMode.CreateNew);
                    tarStream.CopyEntryContents(outStream);
                    outStream.Close();
                    DateTime myDt = DateTime.SpecifyKind(entry.ModTime, DateTimeKind.Utc);
                    File.SetLastWriteTime(name, myDt);
                }
            }
            tarStream.Close();
        }
    }
}
