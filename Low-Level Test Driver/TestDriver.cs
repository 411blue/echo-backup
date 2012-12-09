using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Backend;
using Backend.Storage;
using System.Threading;

namespace Low_Level_Test_Driver
{
    /// <summary>
    /// A class that tests things in the other Echo Backup projects.
    /// 
    /// Each test should be written in its own method(s) and Main() should be modified to call only one test's entry method.
    /// </summary>
    class TestDriver
    {
        /// <summary>
        /// tests the basic Tar and BZip2 functionality.
        /// </summary>
        static void testTarBzip2()
        {
            Console.WriteLine("starting testTarBzip2");
            byte[] b = {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};
            Guid guid = new Guid(b);
            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch";
            StorageThread st = new StorageThread(tempPath, guid);
            BackupTask task = new BackupTask();
            task.backupID = 123;
            task.level = 0;
            task.path = tempPath;
            task.status = 0;
            task.tempPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch 2";
            st.EnqueueBackupTask(task);
            Console.WriteLine("queued task");
            int x = 0;
            while (st.IsWorking())
            {
                x++;
                Console.WriteLine("waiting for thread to finish." + x);
                Thread.Sleep(1000);
            }
            Console.WriteLine("thread finished.");
            st.RequestStop();
            Console.WriteLine("requested thread terminate.");
            while (st.IsAlive())
            {
                x++;
                Console.WriteLine("waiting for thread to die. " + x);
                Thread.Sleep(1000);
            }
            Console.WriteLine("thread is dead.");
            Console.WriteLine("number of chunks: " + st.NumChunks());
            while (st.NumChunks() > 0)
            {
                Chunk c = st.DequeueChunk();
                Console.WriteLine(c);
            }

            Console.WriteLine("press a key to continue");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Logger.init(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch 2\\log.log");
            testTarBzip2();
        }
    }
}
