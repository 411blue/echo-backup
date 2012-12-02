using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.BZip2;

namespace Backend
{
    public struct BackupTask
    {
        //the path (file or directory) that should be backed up
        string path;
        //the path to a directory where I should store temporary files
        string tempPath;
        //the id of the backup
        long backupID;
        //the level of the backup. 0=full, 1=1st incr, 2=2nd incr, etc.
        int level;
        //a number representing the successfulness of the backup
        int status;
    }

    public class StorageThread
    {
        private Thread thread;
        private bool working = true;
        private object _lock;
        private Queue<BackupTask> backupTasks;
        private Queue<BackupTask> backupResults;

        public StorageThread(string path, bool full)
        {
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
                BackupTask task;
                lock (_lock)
                {
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
                //process backup task here
            }

        }

        public bool IsWorking()
        {
            lock (_lock)
            {
                return working;
            }
        }
    }
}
