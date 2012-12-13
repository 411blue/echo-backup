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
using Backend.Properties;
using Backend.Storage;

namespace Backend
{
    /// <summary>
    /// A class that has a thread to perform communication with a specific remote node. Work is added to the workQueue and received unsolicited communication is added to the eventQueue.
    /// </summary>
    public class ClientThread
    {
        //size of message prefix that specifies size of message
        const int MESSAGE_SIZE_SIZE = 2;
        //maximum number of bytes we will buffer at a time
        const int BYTES_PER_READ = 1048576;
        private Guid guid;
        private Thread thread;
        private TcpClient tcpClient;
        //a queue of requests received from the remote node
        private Queue<NetworkEvent> eventQueue;
        //a queue of requests this node should send to another node
        private Queue<NetworkRequest> workQueue;
        private BinaryFormatter binaryFormatter;
        // if true, this instance is created in response to an incoming request. if false, this instance was created to initiate communication with another node.
        private bool isClient;
        // if true, the thread is working on something. if false, the thread is idle (waiting for work). Only applies to isClient=false instances
        private bool working;
        private bool doWork = true;
        private bool stop = false;
        //the path that file should be written to/from when accepting a Push/PullRequest
        private string path;
        object _lock;
        public ClientThread(TcpClient c, bool isClient, Guid guid)
        {
            _lock = new Object();
            this.guid = guid;
            tcpClient = c;
            this.isClient = isClient;
            binaryFormatter = new BinaryFormatter();
            eventQueue = new Queue<NetworkEvent>();
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
            mainLoop();
            tcpClient.Close();
            working = false;
        }

        private void mainLoop()
        {
            //James should draw up a network state machine diagram to model this mainLoop() function
            while (true)
            {
                if (isClient)
                {
                    lock (_lock)
                    {
                        working = false;
                        if (stop)
                        {
                            return;
                        }
                    }
                    //here we assume that if we are in this state we can only possibly be receiving a network request
                    NetworkRequest request = (NetworkRequest)receiveMessage(tcpClient);
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
                        doWork = true;
                    }
                }
                while (doWork | !isClient)
                {
                    bool sleep = false;
                    NetworkRequest request = null;
                    lock (_lock)
                    {
                        if (stop)
                        {
                            return;
                        }
                        if (workQueue.Count() > 0)
                        {
                            working = true;
                            request = workQueue.Dequeue();
                            doWork = false;
                        }
                        else
                        {
                            working = false;
                            sleep = true;
                        }
                    }
                    if (sleep)
                    {
                        Thread.Sleep(100);
                        continue;
                    }
                    handleWork(request);
                    doWork = false;
                }
            }
        }

        public bool IsWorking()
        {
            lock (_lock)
            {
                return working;
            }
        }

        public bool IsAlive()
        {
            return thread.IsAlive;
        }

        public void RequestStop()
        {
            lock (_lock)
            {
                stop = true;
            }
        }

        public void EnqueueWork(NetworkRequest request)
        {
            lock (_lock)
            {
                if (!isClient)
                {
                    working = true;
                    workQueue.Enqueue(request);
                }
            }
        }

        public NetworkEvent DequeueEvent()
        {
            lock (_lock)
            {
                if (eventQueue.Count() > 0)
                {
                    Logger.Debug2("ClientThread:DequeueEvent Returning NetworkEvent.");
                    return eventQueue.Dequeue();
                }
                return null;
            }
        }

        public int EventCount()
        {
            lock (_lock)
            {
                return eventQueue.Count();
            }
        }

        private void handleWork(NetworkRequest request)
        {
            Logger.Debug("ClientThread:handleWork");
            if (this.guid.Equals(request.SourceGuid))
            { //if sourceguid=this.guid: make a network request and go from there
                if (request is PushRequest)
                {
                    processMyPushRequest((PushRequest)request);
                }
                else if (request is PullRequest)
                {
                    processMyPullRequest((PullRequest)request);
                }
                else if (request is QueryRequest)
                {
                    processMyQueryRequest((QueryRequest)request);
                }
                else if (request is PushIndexRequest)
                {
                    processMyPushIndexRequest((PushIndexRequest)request);
                }
                else
                { //what kind of request is this?
                    Logger.Warn("ClientThread:handleWork my request is not a known request instance");
                }
                return;
            }
            else
            {//if sourceguid!=this.guid: receive or send the file requested
                if (request is PushRequest)
                {
                    PushRequest pr = (PushRequest)request;
                    sendMessage(tcpClient, new NetworkResponse(ResponseType.Yes, "", guid, request.SequenceNumber));
                    //readFileToDisk(pr.FileSize, Chunk.PathToChunk(pr));
                    readFileToDisk(pr.FileSize, this.path);
                }
                else if (request is PullRequest)
                {
                    PullRequest pr = (PullRequest) request;
                    sendMessage(tcpClient, new NetworkResponse(ResponseType.Yes, "", guid, request.SequenceNumber));
                    writeFileToNetwork(Chunk.PathToChunk(pr));
                }
                else if (request is QueryRequest)
                {
                    QueryRequest qr = (QueryRequest)request;
                    processRemoteQueryRequest(qr);
                }
                else if (request is PushIndexRequest)
                {
                    processRemotePushIndexRequest((PushIndexRequest)request);
                }
                else
                { //what kind of request is this?
                    Logger.Warn("ClientThread:handleWork remote request is not a known request instance");
                }
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
            Logger.Debug("ClientThread:readBytes");
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
            Logger.Debug("ClientThread:writeBytes");
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
        private NetworkEvent receiveMessage(TcpClient tcpClient)
        {
            Logger.Debug("ClientThread:receiveMessage");
            byte[] sizeBytes = new byte[MESSAGE_SIZE_SIZE];
            int num = readBytes(tcpClient, ref sizeBytes, MESSAGE_SIZE_SIZE);
            if (num != 2)
            {
                Logger.Debug2("ClientThread:receiveMessage we did not get two bytes of size information.");
                return null;
            }
            int size = BitConverter.ToUInt16(sizeBytes, 0);
            byte[] messageBytes = new byte[size];
            num = readBytes(tcpClient, ref messageBytes, size);
            if (num != size)
            {
                Logger.Debug2("ClientThread:receiveMessage we did not receive the entire message as defined by the first two bytes.");
                return null;
            }
            MemoryStream mem = new MemoryStream(messageBytes);
            return (NetworkEvent)binaryFormatter.Deserialize(mem);
        }

        /// <summary>
        /// Send a message to the remote node.
        /// </summary>
        /// <param name="tcpClient">The TcpClient for communication with the remote node</param>
        /// <param name="message">The NetworkRequest to send to the remote node</param>
        /// <returns>The number of bytes written to the remote node or -1 if an error occurred.</returns>
        private int sendMessage(TcpClient tcpClient, NetworkEvent message)
        {
            Logger.Debug("ClientThread:sendMessage");
            MemoryStream mem = new MemoryStream();
            binaryFormatter.Serialize(mem, message);
            byte[] bytes = mem.ToArray();
            int num = bytes.Length;
            byte[] numBytes = BitConverter.GetBytes((ushort)num);
            int ret = writeBytes(tcpClient, numBytes, MESSAGE_SIZE_SIZE);
            if (ret == -1)
            {
                Logger.Error("ClientThread:sendMessage failed to send message size");
            }
            ret = writeBytes(tcpClient, bytes, num);
            if (ret == -1)
            {
                Logger.Error("ClientThread:sendMessage failed to send a message");
            }
            return ret;
        }

        /// <summary>
        /// Reads a file from this object's TcpClient and writes it to the specified path on disk.
        /// </summary>
        /// <param name="numBytes">The size of the file in bytes</param>
        /// <param name="path">The path where the file should be stored</param>
        /// <returns>Zero if successful or non-zero if there was some failure</returns>
        private int readFileToDisk(long numBytes, string path)
        {
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
                    fileStream.Close();
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
            fileStream.Close();
            return 0;
        }

        /// <summary>
        /// Reads a file from disk and writes it to this instance's TcpClient.
        /// </summary>
        /// <param name="path">The path to the file that should be transmitted</param>
        /// <returns>If successful, 0 is returned. Any kind of failure will return a nonzero integer.</returns>
        private int writeFileToNetwork(string path)
        {
            FileStream fileStream;
            byte[] bytes = new byte[BYTES_PER_READ];
            try
            {
                fileStream = File.OpenRead(path);
            }
            catch
            {
                Logger.Log("ClientThread:writeFiletoNetwork failed to open '" + path + "' for reading");
                return 1;
            }
            while (true)
            {
                //read a block of data
                int count;
                try
                {
                    count = fileStream.Read(bytes, 0, BYTES_PER_READ);
                    if (count == 0) break;
                }
                catch
                {
                    Logger.Log("ClientThread:writeFiletoNetwork failed to read from '" + path + "'");
                    return 2;
                }
                //send a block of data
                count = writeBytes(tcpClient, bytes, count);
                if (count == -1)
                {
                    Logger.Log("ClientThread:writeFiletoNetwork failed to write bytes to remote node");
                    break;
                }
            }
            fileStream.Close();
            return 0;
        }

        /*/// <summary>
        /// Responds to a QueryRequest from a remote node by sending a NetworkResponse object. This method blocks until the response is sent.
        /// </summary>
        /// <param name="request">The NetworkRequest that we are responding to</param>
        /// <param name="response">The response code to be sent to the remote node</param>
        /// <param name="details">The details of or reason for the response</param>
        public void RespondToQuery(QueryRequest request, ResponseType response, string details)
        {
            NetworkResponse networkResponse = new NetworkResponse(response, details, guid, request.SequenceNumber);
            int ret = sendMessage(tcpClient, networkResponse);
            if (ret == -1)
            {
                Logger.Error("ClientThread:respondToQuery failed to respond to a node. Response: " + response + " sequenceNumber: " + request.SequenceNumber + " Details: " + details);
            }
            else
            {
                Logger.Info("ClientThread:respondToQuery successfully responded to a node. Response: " + response + " sequenceNumber: " + request.SequenceNumber + " Details: " + details);
            }
        }*/
        public void RespondToQuery(QueryRequest request)
        {
            lock (_lock)
            {
                working = true;
                workQueue.Enqueue(request);
            }
        }

        public void AcceptFileTransfer(PushRequest request, string path)
        {
            Logger.Debug("ClientThread:AcceptFileTransfer:PushRequest");
            acceptFileTransfer(request, path);
        }

        public void AcceptFileTransfer(PullRequest request, string path)
        {
            acceptFileTransfer(request, path);
        }

        private void acceptFileTransfer(NetworkRequest request, string path)
        {
            //doWork = true
            //send affirmative response
            //enqueue request in work queue
            //this function should be called when this thread has received a message and that message has been dequeued.
            //if this function is called while this thread is in the wrong state, it will not do anything until a message is received or the socket closes.
            doWork = true;
            working = true;
            this.path = path;
            lock (_lock)
            {
                workQueue.Enqueue(request);
            }
        }

        private void processMyPushRequest(PushRequest request)
        {
            int ret = sendMessage(tcpClient, request);
            NetworkResponse response = (NetworkResponse)receiveMessage(tcpClient);
            if (response.Type == Backend.ResponseType.Yes)
            {
                ret = writeFileToNetwork(((PushRequest)request).Path);
                //todo: add result to event queue
            }
            else
            {
                //other node said willnot, cannot, or notimplemented
                //add result to event queue
                lock (_lock)
                {
                    eventQueue.Enqueue(response);
                }
            }
        }

        private void processMyPullRequest(PullRequest request)
        {
            int ret = sendMessage(tcpClient, request);
            NetworkResponse response = (NetworkResponse)receiveMessage(tcpClient);
            if (response.Type == Backend.ResponseType.Yes)
            {
                ret = readFileToDisk(((PullRequest)request).FileSize, ((PullRequest)request).Path);
                //todo: add result to event queue
            }
            else
            {
                //other node said willnot, cannot, or notimplemented
                //add result to event queue
                lock (_lock)
                {
                    eventQueue.Enqueue(response);
                }
            }
        }

        private void processMyQueryRequest(QueryRequest request)
        {
            int ret = sendMessage(tcpClient, request);
            NetworkResponse response = (NetworkResponse)receiveMessage(tcpClient);
            //add response to eventQueue
            lock (_lock)
            {
                eventQueue.Enqueue(response);
            }
        }

        private void processRemoteQueryRequest(QueryRequest request)
        {
            //todo: process queries by responding with values from Node staitc methods
            //each query's Field string should have the name of the Backend.QueryType enum
            QueryType qt = request.QueryType;
            string reason = "";
            ResponseType rt = ResponseType.Yes;
            switch (qt)
            {
                case QueryType.BackupSpace:
                    reason = Node.GetBackupSpace().ToString();
                    break;
                case QueryType.FreeSpace:
                    reason = Node.GetFreeSpace().ToString();
                    break;
                case QueryType.Hostname:
                    reason = Node.GetHostName();
                    break;
                case QueryType.IPAddress:
                    reason = Node.GetInternetAddress();
                    break;
                case QueryType.MACAddress:
                    reason = Node.GetMAC();
                    break;
                case QueryType.NonBackupSpace:
                    reason = Node.GetNonBackupSpace().ToString();
                    break;
                case QueryType.TotalSpace:
                    reason = Node.GetTotalSize().ToString();
                    break;
                default:
                    rt = ResponseType.NotImplemented;
                    break;
            }
            sendMessage(tcpClient, new NetworkResponse(rt, reason, this.guid, request.SequenceNumber));
        }

        /// <summary>
        /// Handles a PushIndexRequest sent through this ClientThread's TcpCLient
        /// </summary>
        /// <param name="pir">The PushIndexRequest we need to process</param>
        private void processMyPushIndexRequest(PushIndexRequest pir)
        {
            Logger.Debug("ClientThread:processMyPushIndexRequest");
            int ret = sendMessage(tcpClient, pir);
            NetworkResponse response = (NetworkResponse)receiveMessage(tcpClient);
            if (response.Type == Backend.ResponseType.Yes)
            {
                ret = writeFileToNetwork(pir.Path);
                lock (_lock)
                {
                    eventQueue.Enqueue(response);
                }
            }
            else
            {
                Logger.Warn("ClientThread:processMyPushIndexRequest Remote node refused to store index file: " + response.Type + ' ' + response.Reason);
                //other node said 'no' (willnot, cannot, or notimplemented)
                //add result to event queue
                lock (_lock)
                {
                    eventQueue.Enqueue(response);
                }
            }
        }
        /// <summary>
        /// Handles a PushIndexRequest received from this ClientThread's TcpClient
        /// </summary>
        /// <param name="pir">The PushIndexRequest we need to process</param>
        private void processRemotePushIndexRequest(PushIndexRequest pir)
        {
            Logger.Debug("ClientThread:processRemotePushIndexRequest");
            sendMessage(tcpClient, new NetworkResponse(ResponseType.Yes, "", guid, pir.SequenceNumber));
            //pir.Path should be modified to be the local path where we want to store the received database
            readFileToDisk(pir.DBSize, pir.Path);
            IndexDistribution idd = new IndexDistribution();
            idd.processIndexes(pir.Path);
        }
    }
}
