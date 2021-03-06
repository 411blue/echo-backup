﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Backend
{
    /// <summary>
    /// A thread that listens for commands from the test harness. Someone must ask for events from a CommandServer.
    /// </summary>
    public class CommandServer
    {
        //arbitrary unprivileged port
        public static readonly int SERVER_PORT = 7890;
        private Guid guid;
        //flag to continue listening
        private volatile bool keepGoing = true;
        //queue of threads from incoming clients. obviates eventQueue
        private Queue<ClientThread> clientThreads;
        private Thread serverThread;
        private TcpListener tcpListener;

        public CommandServer(Guid guid)
        {
            this.guid = guid;
            clientThreads = new Queue<ClientThread>();
            tcpListener = new TcpListener(IPAddress.Any, SERVER_PORT);
            serverThread = new Thread(new ThreadStart(RunServerThread));
            serverThread.Start();
        }

        /// <summary>
        /// Listen for new connections. As they arrive, spin-up a new client thread to receive the message.
        /// </summary>
        private void RunServerThread()
        {
            tcpListener.Start();
            while (keepGoing)
            {
                try
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    ClientThread clientThread = new ClientThread(tcpClient, true, guid);
                    lock (clientThreads)
                    {
                        clientThreads.Enqueue(clientThread);
                    }
                }
                catch
                {
                    Logger.Error("CommandServer:RunServerThread Error when accepting TcpClient.");
                    break;
                }
            }
        }

        /// <summary>
        /// Returns the next ClientThread in the queue. Returns null if the queue is empty.
        /// </summary>
        /// <returns></returns>
        public ClientThread getClientThread()
        {
            lock (clientThreads)
            {
                if (clientThreads.Count() > 0)
                {
                    return clientThreads.Dequeue();
                }
                return null;
            }
        }

        public int ClientThreadCount()
        {
            lock (clientThreads)
            {
                return clientThreads.Count;
            }
        }

        /// <summary>
        /// Returns the number of ClientThreads in the queue.
        /// </summary>
        /// <returns></returns>
        public int clientThreadCount()
        {
            lock (clientThreads)
            {
                return clientThreads.Count();
            }
        }

        public void Stop()
        {
            keepGoing = false;
            tcpListener.Stop();
        }
    }
}
