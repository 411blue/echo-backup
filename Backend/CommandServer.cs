using System;
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
    class CommandServer
    {
        //arbitrary unprivileged port
        private const int SERVER_PORT = 7890;
        
        //flag to continue listening
        private bool keepGoing = true;
        //queue of threads from incoming clients. obviates eventQueue
        private Queue<ClientThread> clientThreads;
        private Thread serverThread;
        private TcpListener tcpListener;

        public CommandServer()
        {
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
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                ClientThread clientThread = new ClientThread(tcpClient, true);
                clientThreads.Enqueue(clientThread);
            }
        }
        
    }
}
