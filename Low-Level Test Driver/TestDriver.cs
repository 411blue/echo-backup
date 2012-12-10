﻿using System;
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
        static void Print(string s)
        {
            Console.WriteLine(s);
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
            TcpClient tcpClient = new TcpClient("localhost", 7890);
            ClientThread ct = new ClientThread(tcpClient, false, myGuid);
            ct.EnqueueWork(new QueryRequest(IPAddress.Parse(Node.GetInternetAddress()), myGuid, 777));
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

        static void Main(string[] args)
        {
            //Logger.init(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\temp\\scratch 2\\log.log");
            //testTarGZip();
            testQueryRequests();
        }
    }
}
