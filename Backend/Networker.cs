using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using System.Collections.Generic;

namespace Backend {
    public struct Hello {
        public IPAddress ip;
        public PhysicalAddress mac;
        public Guid guid;
        public Version version;

        public Hello(IPAddress ip, PhysicalAddress mac, Guid guid, Version version) {
            this.ip = ip;
            this.mac = mac;
            this.guid = guid;
            this.version = version;
        }
    }

    class Networker {
        //static members
        const int MULTICAST_PORT = 7777;
        //Must be any address in 239.0.0.0/8
        static readonly byte[] MULTICAST_CHANNEL_BYTES = {239,77,77,77};
        static readonly IPAddress MULTICAST_CHANNEL = new IPAddress(MULTICAST_CHANNEL_BYTES);

        //private members
        UdpClient udpClient;
        IPEndPoint multicastListener;
        Thread multicastThread;
        Thread unicastThread;
        Queue<Hello> hellos;

        public Networker() {
            Init();
        }

        //public methods
        public void Init() {
            //initialize multicast
            udpClient = new UdpClient();
            multicastListener = new IPEndPoint(IPAddress.Any, MULTICAST_PORT);
            udpClient.JoinMulticastGroup(MULTICAST_CHANNEL);
            multicastThread = new Thread(this.MulticastLoop);

            //initialize unicast

        }

        public void SendHello() {
            //create Hello object
            //Hello hello = new Hello("my ip", "my mac", "my guid", "my version");
            
            //serialize Hello object
            
            //send Hello object to multicast channel
            //udpClient.Send();
        }

        private void MulticastLoop() {
            while (true) {
                //wait for hello multicasts
                //add hello multicasts to queue object 'hellos'
            }
        }
    }
}
