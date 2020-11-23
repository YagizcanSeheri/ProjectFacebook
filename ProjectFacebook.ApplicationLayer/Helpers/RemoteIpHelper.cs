using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Helpers
{
    public static class RemoteIpHelper
    {
        public static string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return "Local Ip address not found..!";
        }

        public static string IpAddress { get { return GetIpAddress(); } }
    }
}
