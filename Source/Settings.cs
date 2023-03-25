using System;
using System.Net;
using Newtonsoft.Json;

namespace SaY_DeF.Source
{
    internal class Settings
    {
        [JsonProperty("nickname")]
        public static string myNick;
        [JsonProperty("localPort")]
        int localPort = 80;
        [JsonIgnore]
        public static IPAddress IP
        {
            get
            {
                string hostName = Dns.GetHostName();
                IPAddress[] addresses = Dns.GetHostAddresses(hostName);

                foreach (IPAddress address in addresses)
                {
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        return address;
                }
                throw new Exception("IPv4 address not found");
            }
        }
        [JsonIgnore]
        public int LocalPort { get { return localPort; } }

    }
}