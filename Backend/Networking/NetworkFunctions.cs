using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using Backend.Database;

namespace Backend.Networking
{
    public struct GuidAndIP
    {
        public IPAddress ipAddress;
        public Guid guid;
        public GuidAndIP(IPAddress i, Guid g)
        {
            ipAddress = i;
            guid = g;
        }
    }

    public static class NetworkFunctions
    {
        


        /// <summary>
        /// Returns true if a node is online based on a ICMP echo request with 100ms timeout.
        /// </summary>
        /// <param name="ipa"></param>
        /// <returns></returns>
        public static bool NodeOnline(IPAddress ipa)
        {
            Ping pingSender = new Ping();
            int timeout = 100;
            try
            {
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

        /// <summary>
        /// Returns a list of GuidAndIP structs of the online nodes.
        /// </summary>
        /// <returns></returns>
        public static List<GuidAndIP> GetOnlineNodesIPAddresses()
        {
            Logger.Debug("NetworkFunctions:GetOnlineNodeIPAddresses");
            List<GuidAndIP> list = new List<GuidAndIP>();
            NodeDatabase ndb = new NodeDatabase();
            List<string> guidList = ndb.SelectGUID();
            foreach (string guid in guidList)
            {
                Guid parsed = Guid.Parse(guid);
                if (Node.GetGuid() != parsed && ndb.SelectNodeTrusted(parsed) == "yes")
                {
                    IPAddress ipa = ndb.SelectNodeIp(Guid.Parse(guid));
                    if (NodeOnline(ipa)) list.Add(new GuidAndIP(ipa, Guid.Parse(guid)));
                }
            }
            return list;
        }
    }

}
