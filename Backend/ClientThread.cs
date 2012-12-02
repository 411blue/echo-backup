using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Backend
{
    /// <summary>
    /// A class that has a thread to perform communication with a specific remote node. Work is added to the workQueue and received unsolicited communication is added to the eventQueue.
    /// </summary>
    class ClientThread
    {
        //size of message prefix that specifies size of message
        const int MESSAGE_SIZE_SIZE = 2;
        private Thread thread;
        private TcpClient tcpClient;
        //a queue of requests received from the remote node
        private Queue<NetworkRequest> eventQueue;
        //a queue of requests this node should send to another node
        private Queue<NetworkRequest> workQueue;
        private BinaryFormatter binaryFormatter;
        // if true, this instance is created in response to an incoming request. if false, this instance was created to initiate communication with another node.
        private bool isClient;
        // if true, the thread is working on something. if false, the thread is idle (waiting for work). Only applies to isClient=false instances
        private bool working;
        object _lock;
        public ClientThread(TcpClient c, bool isClient)
        {
            tcpClient = c;
            this.isClient = isClient;
            binaryFormatter = new BinaryFormatter();
            eventQueue = new Queue<NetworkRequest>();
            workQueue = new Queue<NetworkRequest>();
            thread = new Thread(new ParameterizedThreadStart(RunClientThread));
            thread.Start(tcpClient);
        }

        /// <summary>
        /// Depending on state of isClient variable, start listening for events on the network or waiting for work.
        /// </summary>
        /// <param name="tcpClient">The TcpClient that will be used in this thread</param>
        private void RunClientThread(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            if (isClient)
            {
                while (true)
                {
                    lock (_lock)
                    {
                        working = true;
                    }
                    NetworkRequest request = receiveMessage(tcpClient);
                    //Die if we failed to receive a message
                    if (request == null)
                    {
                        lock (_lock)
                        {
                            working = false;
                        }
                        break;
                    }
                    lock (_lock)
                    {
                        eventQueue.Enqueue(request);
                    }
                }
            }
            else
            {
                while (true)
                {
                    bool sleep = false;
                    lock (_lock)
                    {
                        working = false;
                        if (workQueue.Count() > 0)
                        {
                            working = true;
                            NetworkRequest request = workQueue.Dequeue();
                        }
                        else
                        {
                            sleep = true;
                        }
                    }
                    if (sleep)
                    {
                        Thread.Sleep(100);
                    }
                    //todo: handle work
                }
            }
        }

        public bool isWorking()
        {
            lock (_lock)
            {
                return working;
            }
        }

        public void enqueueWork(NetworkRequest request)
        {
            lock (_lock)
            {
                if (!isClient)
                {
                    workQueue.Enqueue(request);
                }
            }
        }

        public NetworkRequest dequeueEvent()
        {
            lock (_lock)
            {
                if (eventQueue.Count() > 0)
                {
                    return eventQueue.Dequeue();
                }
                return null;
            }
        }

        /// <summary>
        /// Reads bytes from a TcpClient. Blocks until 'num' bytes are received or the other end disconnects. bytes must be at least num size.
        /// </summary>
        /// <param name="tcpClient">The TcpClient that is receiving some bytes</param>
        /// <param name="bytes">The array in which to store the received bytes</param>
        /// <param name="num">The number of bytes to read.</param>
        /// <returns>The number of bytes read.</returns>
        private int readBytes(TcpClient tcpClient, ref byte[] bytes, int num)
        {
            NetworkStream clientStream = tcpClient.GetStream();
            byte[] b = new byte[num];
            int bytesRead;
            int totalBytesRead = 0;
            while (true)
            {
                bytesRead = 0;
                try
                {
                    //blocks until the client sends a message
                    //read only MESSAGE_SIZE_SIZE bytes -- the length of the message in bytes
                    bytesRead = clientStream.Read(b, 0, num - totalBytesRead);
                    for (int i = 0; i < bytesRead; i++)
                    {
                        bytes[i + totalBytesRead] = b[i];
                    }
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
                if (totalBytesRead == num)
                {
                    return totalBytesRead;
                }
            }
            return totalBytesRead;
        }

        /// <summary>
        /// Writes bytes to a TcpClient. Blocks until all bytes are written.
        /// </summary>
        /// <param name="tcpClient">The TCP connection to send the bytes to</param>
        /// <param name="bytes">the byte array containing the bytes to send starting at index 0</param>
        /// <param name="num">the number of bytes from the array to send. bytes must be at least num size.</param>
        /// <returns>The number of bytes written or a -1 if an error occurred.</returns>
        private int writeBytes(TcpClient tcpClient, byte[] bytes, int num)
        {
            NetworkStream clientStream = tcpClient.GetStream();
            try
            {
                clientStream.Write(bytes, 0, num);
            }
            catch
            {
                return -1;
            }
            return num;
        }

        /// <summary>
        /// Gets the incoming message on a TCP Socket.
        /// </summary>
        /// <param name="tcpClient">The socket that is expected to receive a message</param>
        /// <returns>The TcpNetworkEvent received from the other end. Returns null if there was a problem.</returns>
        private NetworkRequest receiveMessage(TcpClient tcpClient)
        {
            byte[] sizeBytes = new byte[MESSAGE_SIZE_SIZE];
            int num = readBytes(tcpClient, ref sizeBytes, MESSAGE_SIZE_SIZE);
            if (num != 2)
            {
                //we did not get two bytes of size information from the client
                return null;
            }
            int size = BitConverter.ToUInt16(sizeBytes, 0);
            byte[] messageBytes = new byte[size];
            num = readBytes(tcpClient, ref messageBytes, size);
            if (num != size)
            {
                //we did not receive the entire message as defined by the first two bytes that specify the size
                return null;
            }
            MemoryStream mem = new MemoryStream(messageBytes);
            return (NetworkRequest)binaryFormatter.Deserialize(mem);
        }

        /// <summary>
        /// Send a message to the remote node.
        /// </summary>
        /// <param name="tcpClient">The TcpClient for communication with the remote node</param>
        /// <param name="request">The NetworkRequest to send to the remote node</param>
        private void sendMessage(TcpClient tcpClient, NetworkRequest request)
        {
            MemoryStream mem = new MemoryStream();
            binaryFormatter.Serialize(mem, request);
            byte[] bytes = mem.ToArray();
            int num = bytes.Length;
            writeBytes(tcpClient, bytes, num);
        }

        /// <summary>
        /// Reads a file from this object's TcpClient and writes it to the specified path on disk.
        /// </summary>
        /// <param name="numBytes">The size of the file in bytes</param>
        /// <param name="path">The path where the file should be stored</param>
        /// <returns>Zero if successful or non-zero if there was some failure</returns>
        private int readFileToDisk(long numBytes, string path)
        {
            //maximum number of bytes we will buffer at a time
            const int BYTES_PER_READ = 1048576;
            long bytesRemaining = numBytes;
            byte[] bytes = new byte[BYTES_PER_READ];

            //try to open the file. Do not overwrite an existing file.
            FileStream fileStream;
            try
            {
                fileStream = File.Open(path, FileMode.CreateNew);
            }
            catch
            {
                Logger.Log("ClientThread:readFileToDisk failed to open file: " + path);
                return 1;
            }

            //read and write the file
            while (bytesRemaining > 0)
            {
                //Math.Min call will always return BYTES_PER_READ or less so it can safely be cast to int
                int n = (int)Math.Min(bytesRemaining, (long)BYTES_PER_READ);
                int ret = readBytes(tcpClient, ref bytes, n);
                if (ret != n)
                {
                    Logger.Log("ClientThread:readFileToDisk failed to read all bytes from the client");
                    return 2;
                }

                //write the block of the file to disk
                try
                {
                    fileStream.Write(bytes, 0, n);
                }
                catch
                {
                    Logger.Log("ClientThread:readFileToDisk failed to write bytes to file");
                    fileStream.Close();
                    return 3;
                }

                bytesRemaining -= BYTES_PER_READ;
            }

            return 0;
        }
    }
}
