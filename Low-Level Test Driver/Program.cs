using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Backend;
using System.Threading;

namespace Low_Level_Test_Driver
{
    /// <summary>
    /// A class that tests things in the other Echo Backup projects.
    /// 
    /// Each test should be written in its own method(s) and Main() should be modified to call only one test's entry method.
    /// </summary>
    class Program
    {
        /// <summary>
        /// tests the basic Tar and BZip2 functionality.
        /// </summary>
        static void testTarBzip2()
        {
            Console.WriteLine("starting testTarBzip2");
            Guid guid = new Guid();
            StorageThread st = new StorageThread(".\\", guid);
            BackupTask task = new BackupTask();
            task.backupID = 123;
            task.level = 0;
            task.path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\scratch";
            task.status = 0;
            task.tempPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\scratch2";
            st.EnqueueBackupTask(task);
            Console.WriteLine("queued task");
            while (st.IsWorking())
            {
                Console.WriteLine("waiting for thread to finish.");
                Thread.Sleep(1000);
            }
            Console.WriteLine("thread finished.");
            st.RequestStop();
            Console.WriteLine("requested thread terminate.");
            while (st.IsAlive())
            {
                Console.WriteLine("waiting for thread to die.");
                Thread.Sleep(1000);
            }
            Console.WriteLine("thread is dead.");
        }

        static void Main(string[] args)
        {
            Logger.init();
            testTarBzip2();
        }
    }
}
