using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Backend;
using Backend.Storage;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace Low_Level_Test_Driver
{
    /// <summary>
    /// A class that tests things in the other Echo Backup projects.
    /// 
    /// Each test should be written in its own method(s) and Main() should be modified to call only one test's entry method.
    /// </summary>
    class TestDriver
    {
        private static EchoBackupService ebs;

        static void Print(string s)
        {
            Console.WriteLine(s);
        }
        static void Wait()
        {
            Console.WriteLine("press a key to continue");
            Console.ReadKey();
        }

        /// <summary>
        /// tests the basic Tar and GZip functionality.
        /// </summary>
        static void testTarGZip()
        {
            Console.WriteLine("starting testTarBzip2");
            byte[] b = {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};
            Guid guid = new Guid(b);
            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch";
            string tempPath2 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch 2";
            StorageThread st = new StorageThread(tempPath, guid);
            BackupTask task = new BackupTask(tempPath, tempPath2, 123, 0);
            st.EnqueueStorageTask(task);
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

        //tests receiving and responding to QueryRequests
        static void testQueryRequestServer()
        {
            Guid myGuid = Guid.NewGuid();
            CommandServer cs = new CommandServer(myGuid);
            int x = 0;
            while (cs.ClientThreadCount() == 0)
            {
                x++;
                Thread.Sleep(1000);
                Print("waiting for message. " + x);
            }
            ClientThread ct = cs.getClientThread();
            Print("got client thread.");
            while (ct.EventCount() == 0)
            {
                x++;
                Thread.Sleep(1000);
                Print("waiting to receive request." + x);
            }
            Print("received request.");
            QueryRequest qr = (QueryRequest)ct.DequeueEvent();
            Print("request: " + qr.QueryType);
            ct.RespondToQuery(qr);
            Print("responded to query.");
            while (ct.IsWorking())
            {
                x++;
                Thread.Sleep(1000);
                Print("waiting for ClientThread to finish working." + x);
            }
            ct.RequestStop();
            Print("requested clientthread stop");
            cs.Stop();
            Print("stopped CommandServer.");
            Console.WriteLine("press a key to continue");
            Console.ReadKey();
        }
        //tests sending a QueryRequest and receiving a response
        static void testQueryRequestClient()
        {
            Guid myGuid = Guid.NewGuid();
            TcpClient tcpClient = new TcpClient("172.18.9.11", 7890);
            ClientThread ct = new ClientThread(tcpClient, false, myGuid);
            QueryRequest qr = new QueryRequest(IPAddress.Parse(Node.GetInternetAddress()), myGuid, 777);
            qr.QueryType = QueryType.Hostname;
            ct.EnqueueWork(qr);
            int x = 0;
            while (ct.EventCount() == 0)
            {
                x++;
                Thread.Sleep(1000);
                Print("waiting for response. " + x);
            }
            NetworkResponse response = (NetworkResponse) ct.DequeueEvent();
            Print("response: " + response.Type + " reason: " + response.Reason);
            ct.RequestStop();
            Print("requested stop");
            while (ct.IsAlive())
            {
                x++;
                Thread.Sleep(1000);
                Print("waiting for thread to die. " + x);
            }
            Print("thread dead.");
            Console.WriteLine("press a key to continue");
            Console.ReadKey();
        }
        //tests QueryRequests and responses. behaves as client or server depending on arg
        static void testQueryRequests()
        {
            Print("Type 'client' to run in client-mode or 'server' to run in server-mode.");
            string s = Console.ReadLine();
            if (s == "server")
            {
                Logger.init(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch 2\\server.log");
                testQueryRequestServer();
            }
            else if (s == "client")
            {
                Logger.init(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch 2\\client.log");
                testQueryRequestClient();
            }
        }

        //test sending/receiving files
        public static void testReceiveBackup()
        {
            Guid myGuid = Guid.NewGuid();
            CommandServer cs = new CommandServer(myGuid);
            int x = 0;
            while (cs.ClientThreadCount() == 0)
            {
                x++;
                Thread.Sleep(1000);
                Print("waiting for message. " + x);
            }
            ClientThread ct = cs.getClientThread();
            Print("got client thread.");
            while (ct.EventCount() == 0)
            {
                x++;
                Thread.Sleep(1000);
                Print("waiting to receive request." + x);
            }
            Print("received request.");
            PushRequest pr = (PushRequest)ct.DequeueEvent();
            Print("request: " + pr.FileSize + ' ' + Path.GetFileName(pr.Path));
            ct.AcceptFileTransfer(pr, "C:\\Users\\411blue\\desktop\\temp\\scratch 2\\" + Path.GetFileName(pr.Path));
            Print("responded to PushRequest.");
            while (ct.IsWorking())
            {
                x++;
                Thread.Sleep(1000);
                Print("waiting for ClientThread to finish working." + x);
            }
            ct.RequestStop();
            Print("requested clientthread stop");
            cs.Stop();
            Print("stopped CommandServer.");
            Console.WriteLine("press a key to continue");
            Console.ReadKey();
        }
        public static void testSendBackup()
        {
            string path = "C:\\Users\\James\\Desktop\\temp\\scratch 2\\How I Met Your Mother Season 06 Episode 24 - Challange Accepted.avi";
            Guid myGuid = Guid.NewGuid();
            TcpClient tcpClient = new TcpClient("172.18.9.11", 7890);
            ClientThread ct = new ClientThread(tcpClient, false, myGuid);
            PushRequest pr = new PushRequest(IPAddress.Parse(Node.GetInternetAddress()), myGuid, 777);
            pr.BackupNumber = 4;
            pr.ChunkNumber = 5;
            pr.Path = path;
            pr.FileSize = new FileInfo(path).Length;
            ct.EnqueueWork(pr);
            int x = 0;
            /*while (ct.EventCount() == 0)
            {
                x++;
                Thread.Sleep(1000);
                Print("waiting for response. " + x);
            }
            NetworkResponse response = (NetworkResponse)ct.DequeueEvent();
            Print("response: " + response.Type + " reason: " + response.Reason);*/
            while (ct.IsWorking())
            {
                x++;
                Thread.Sleep(1000);
                Print("waiting for thread to finish working. " + x);
            }
            Print("thread finished working. " + x);
            ct.RequestStop();
            Print("requested stop");
            while (ct.IsAlive())
            {
                x++;
                Thread.Sleep(1000);
                Print("waiting for thread to die. " + x);
            }
            Print("thread dead.");
            Console.WriteLine("press a key to continue");
            Console.ReadKey();
        }
        public static void testSendReceive(string s)
        {
            if (s == "server")
            {
                Logger.init(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch 2\\server.log");
                testReceiveBackup();
            }
            else
            {
                Logger.init(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch 2\\client.log");
                testSendBackup();
            }
        }

        public static void testUnTarGZip()
        {
            Logger.init(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch 2\\uncompress.log");
            Guid myGuid = Guid.NewGuid();
            string archivePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch 2\\01010101-0101-0101-0101-010101010101_123_0.tgz";
            string outputDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch 2";
            RestoreTask rt = new RestoreTask(archivePath, outputDir);
            rt.Add("cs 410 2012-12-09\\echo backup logo v1.png");
            StorageThread st = new StorageThread("", myGuid);
            st.EnqueueStorageTask(rt);
            Print("added task to storagethread");
            int x=0;
            while (st.IsWorking())
            {
                x++;
                Thread.Sleep(1000);
                Print("waiting for storagethread to finish working " + x);
            }
            Print("storagethread done");
            st.RequestStop();
            Print("requested stop of storagethread");
            while (st.IsAlive())
            {
                x++;
                Thread.Sleep(1000);
                Print("waiting for storagethread to stop " + x);
            }
            Print("storagethread stopped");
            Console.WriteLine("press a key to continue");
            Console.ReadKey();
        }

        public static void testService()
        {
            ebs = new EchoBackupService();
            string[] args = {};
            ebs.testStartThreaded();
            Wait();
            string[] list = new string[1];
            list[0] = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch";
            ebs.StartBackup(list);
            Wait();
            ebs.testStop();

        }

        static void Main(string[] args)
        {
            //Logger.init(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch 2\\log.log");
            //testTarGZip();
            //testQueryRequests();
            //Print(args[0]);
            /*if (args.Length >= 1)
            {
                testSendReceive(args[0]);
            }
            else
            {
                testSendReceive("");
            }*/
            //testUnTarGZip();
            testService();
        }
    }
}
