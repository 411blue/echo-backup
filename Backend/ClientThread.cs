﻿using System;
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
    class ClientThread
    {
        //size of message prefix that specifies size of message
        const uint MESSAGE_SIZE_SIZE = 2;
        private Thread thread;
        private TcpClient tcpClient;
        private Queue<TcpNetworkEvent> eventQueue;
        private BinaryFormatter binaryFormatter;
        public ClientThread(TcpClient c)
        {
            tcpClient = c;
            binaryFormatter = new BinaryFormatter();
            eventQueue = new Queue<TcpNetworkEvent>();
            thread = new Thread(new ParameterizedThreadStart(RunClientThread));
            thread.Start(tcpClient);
        }

        /// <summary>
        /// Receive event from TcpClient and add it to eventQueue. Use thread-safe behaviour.
        /// </summary>
        /// <param name="tcpClient"></param>
        private void RunClientThread(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            TcpNetworkEvent tcpNetworkEvent = receiveMessage(tcpClient);
            eventQueue.Enqueue(tcpNetworkEvent);
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
            while (true)
            {
                bytesRead = 0;
                try
                {
                    //blocks until the client sends a message
                    //read only MESSAGE_SIZE_SIZE bytes -- the length of the message in bytes
                    bytesRead = clientStream.Read(b, 0, (int)(num - totalBytesRead));
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
        private TcpNetworkEvent receiveMessage(TcpClient tcpClient)
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
            return (TcpNetworkEvent)binaryFormatter.Deserialize(mem);
        }

        private void sendMessage(TcpClient tcpClient, TcpNetworkEvent tcpNetworkEvent)
        {
            MemoryStream mem = new MemoryStream();
            binaryFormatter.Serialize(mem, tcpNetworkEvent);
            byte[] bytes = mem.ToArray();
            int num = bytes.Length;
            writeBytes(tcpClient, bytes, num);
        }
    }
}
