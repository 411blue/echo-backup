using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System;

namespace Backend
{
    enum NetworkEventType {Hello, Push, Pull, Query};

    /// <summary>
    /// An abstract representation of a network event. Used to provide a base for the definition of push, pull and query requests.
    /// </summary>
    [Serializable()]
    public abstract class NetworkEvent
    {
        protected IPAddress mySourceIPAddress;
        public IPAddress SourceIPAddress
        {
            get { return mySourceIPAddress; }
            set { mySourceIPAddress = value; }
        }

        protected PhysicalAddress mySourceMacAddress;
        public PhysicalAddress SourceMacAddress
        {
            get { return mySourceMacAddress; }
            set { mySourceMacAddress = value; }
        }

        //The GUID of the sending node to uniquely identify it
        protected Guid mySourceGuid;
        public Guid SourceGuid
        {
            get { return mySourceGuid; }
            set { mySourceGuid = value; }
        }

        //the type of network event. See functional requirements 6.1.4 and 6.2.1.
        //Using inheritance instead of 
        //protected NetworkEventType eventType;

        //A unique identifier for this conversation. Allows one node to ask another node multiple questions without mixing-up responses
        protected int mySequenceNumber;
        public int SequenceNumber
        {
            get { return mySequenceNumber; }
            set { mySequenceNumber = value; }
        }
    }

    /// <summary>
    /// Abstract class with base code for TCP network events. Contains a TcpClient object.
    /// </summary>
    public abstract class TcpNetworkEvent : NetworkEvent
    {
        private TcpClient myTcpClient;
        public TcpClient TcpClient
        {
            get { return myTcpClient; }
            set { myTcpClient = value; }
        }
        
        public TcpNetworkEvent(IPAddress ipAddress, PhysicalAddress macAddress, Guid guid, int sequenceNumber)
        {
            mySourceIPAddress = ipAddress;
            mySourceMacAddress = macAddress;
            mySourceGuid = guid;
            mySequenceNumber = sequenceNumber;
        }

        public TcpNetworkEvent(TcpClient tcpClient)
        {
            myTcpClient = tcpClient;
        }
    }

    /// <summary>
    /// Represents a push request to store a backup file.
    /// </summary>
    public class PushRequest : TcpNetworkEvent
    {
        
        //these constructors is needed to compile!..Shane
        public PushRequest(TcpClient tcpClient)
            : base(tcpClient)
        {
        }

        public PushRequest(IPAddress ipAddress, PhysicalAddress macAddress, Guid guid, int sequenceNumber)
            : base(ipAddress, macAddress, guid, sequenceNumber)
        {
        }

        // The id of the backup this file is a part of
        private long myBackupNumber;
        public long BackupNumber
        {
            get { return myBackupNumber; }
            set { myBackupNumber = value; }
        }

        // The chunk ID of this backup file
        private long myChunkNumber;
        public long ChunkNumber
        {
            get { return myChunkNumber; }
            set { myChunkNumber = value; }
        }

        // The size of the backup file
        private long myFileSize;
        public long FileSize
        {
            get { return myFileSize; }
            set { myFileSize = value; }
        }
    }
}