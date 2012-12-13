using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace Backend.Networking
{
    public static class NetworkFunctions
    {
        public static bool NodeOnline(IPAddress ipa)
        {
            Ping pingSender = new Ping();
            int timeout = 100;
            try
            {
                //ndb.SelectNodeIp(Guid.Parse(currentGUID));
                PingReply reply = pingSender.Send(ipa, timeout);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("NetworkFunctions:NodeOnline Error using ping: " + ex.Message);
                return false;
            }
        }
    }
}
