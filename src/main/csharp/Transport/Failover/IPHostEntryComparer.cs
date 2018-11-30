using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Apache.NMS.ActiveMQ.Transport.Failover
{
    class IPHostEntryComparer : EqualityComparer<IPHostEntry>
    {
        // It is important to avoid HostName usage for comparison.
        public override bool Equals(IPHostEntry x, IPHostEntry y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return true;

            var xIPV6 = (from ipv6 in x.AddressList
                         where ipv6.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6
                         select ipv6).FirstOrDefault();
            var yIPV6 = (from ipv6 in y.AddressList
                         where ipv6.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6
                         select ipv6).FirstOrDefault();

            var xIPV4 = (from ipv4 in x.AddressList
                         where ipv4.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                         select ipv4).FirstOrDefault();
            var yIPV4 = (from ipv4 in y.AddressList
                         where ipv4.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                         select ipv4).FirstOrDefault();

            if (
                IPAddress.Equals(xIPV6, yIPV6)
                && IPAddress.Equals(xIPV4, yIPV4)
                ) return true;
            else return false;
        }

        public override int GetHashCode(IPHostEntry obj)
        {
            return obj.GetHashCode();
        }
    }
}
