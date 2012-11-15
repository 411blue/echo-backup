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
        //size of message prefix that specifies size of message
        const uint MESSAGE_SIZE_SIZE = 2;
        //flag to continue listening
        private bool keepGoing = true;
        private Queue<TcpNetworkEvent> eventQueue;
        private Queue<Thread> serverThreads; //this had no name and no semi-colon so it wouldn't compile. I just named it serverThreads as it seemed appropriate...Shane
        private Thread serverThread;
        private TcpListener tcpListener;
        private BinaryFormatter binaryFormatter;

        public CommandServer()
        {
            binaryFormatter = new BinaryFormatter();
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
            TcpNetworkEvent tcpNetworkEvent = getMessage(tcpClient);
            
        }

        /// <summary>
        /// Reads bytes from a TcpClient. Blocks until 'num' bytes are received or the other end disconnects. bytes must be at least num size.
        /// </summary>
        /// <param name="tcpClient">The TcpClient that is receiving some bytes</param>
        /// <param name="bytes">The array in which to store the received bytes</param>
        /// <param name="num">The number of bytes to read.</param>
        /// <returns>The number of bytes read.</returns>
        private uint readBytes(TcpClient tcpClient, ref byte[] bytes, uint num)
        {
            NetworkStream clientStream = tcpClient.GetStream();
            byte[] b = new byte[num];
            int bytesRead;
            uint totalBytesRead = 0;
            while (true) {
                bytesRead = 0;
                try {
                    //blocks until the client sends a message
                    //read only MESSAGE_SIZE_SIZE bytes -- the length of the message in bytes
                    bytesRead = clientStream.Read(b, 0, (int)(num-totalBytesRead));
                    for (int i = 0; i < bytesRead; i++)
                    {
                        bytes[i + totalBytesRead] = b[i];
                    }
                    totalBytesRead += (uint)bytesRead;
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
                if (totalBytesRead == num)
                {
                    return totalBytesRead;
                }
            }
            return totalBytesRead;
        }

        /// <summary>
        /// Gets the incoming message on a TCP Socket.
        /// </summary>
        /// <param name="tcpClient">The socket that is expected to receive a message</param>
        /// <returns>The TcpNetworkEvent received from the other end. Returns null if there was a problem.</returns>
        private TcpNetworkEvent getMessage(TcpClient tcpClient)
        {
            byte[] sizeBytes = new byte[MESSAGE_SIZE_SIZE];
            uint num = readBytes(tcpClient, ref sizeBytes, MESSAGE_SIZE_SIZE);
            if (num != 2)
            {
                //we did not get two bytes of size information from the client
                return null;
            }
            uint size = BitConverter.ToUInt16(sizeBytes, 0);
            byte[] messageBytes = new byte[size];
            num = readBytes(tcpClient, ref messageBytes, size);
            if (num != size)
            {
                //we did not receive the entire message as defined by the first two bytes that specify the size
                return null;
            }
            MemoryStream mem = new MemoryStream(messageBytes);
            return (TcpNetworkEvent) binaryFormatter.Deserialize(mem);
        }
    }
}
