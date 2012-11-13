using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;

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
        private Queue<TcpNetworkEvent> eventQueue;
        private Queue<Thread>
        private Thread serverThread;
        private TcpListener tcpListener;

        public CommandServer()
        {
            eventQueue = new Queue<TcpNetworkEvent>();
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
                Thread clientThread = new Thread(new ParameterizedThreadStart(RunClientThread));
                clientThread.Start(tcpClient);
            }
        }

        /// <summary>
        /// Receive event from TcpClient and add it to eventQueue. Use thread-safe behaviour.
        /// </summary>
        /// <param name="tcpClient"></param>
        private void RunClientThread(object client)
        {
            TcpClient tcpClient = (TcpClient) client;
            NetworkStream clientStream = tcpClient.GetStream();
            byte[] bytes = new byte[4096];
            int bytesRead;
            while (true)
            {
                bytesRead = 0;
                uint totalBytesRead = 0;
                uint bytesNeeded = 2;
                try
                {
                    //blocks until the client sends a message
                    //read only 2 bytes -- the ushort that specifies the length of the message in bytes
                    bytesRead = clientStream.Read(bytes, 0, 2);
                    totalBytesRead += bytesRead;
                }
                catch
                {
                    //a socket error has occured
                    break;
                }
                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }
                if (totalBytesRead < bytesNeeded) {
                    continue;
                }

        }
    }
}
