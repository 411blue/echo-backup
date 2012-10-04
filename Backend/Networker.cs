using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Backend {
    class Networker {
        //static members
        const int MULTICAST_PORT = 7777;
        //Must be any address in 239.0.0.0/8
        static readonly byte[] MULTICAST_CHANNEL_BYTES = {239,77,77,77};
        static readonly IPAddress MULTICAST_CHANNEL = new IPAddress(MULTICAST_CHANNEL_BYTES);

        //private members
        UdpClient udpClient;
        IPEndPoint multicastListener;

        //public methods
        public void init() {
            udpClient = new UdpClient();
            multicastListener = new IPEndPoint(IPAddress.Any, MULTICAST_PORT);
            udpClient.JoinMulticastGroup(MULTICAST_CHANNEL);

        }
    }
}
