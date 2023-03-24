using System.Net;

namespace SaY_DeF.Source
{
    internal class Settings
    {
        public static string myNick = "Bebra";
        IPAddress ip;
        int port = 80;
        int localPort = 80;
        public Settings()
        {
            string Host = Dns.GetHostName();
            ip = Dns.GetHostEntry(Host).AddressList[0];
        }

        public string Name { get { return myNick; } }
        public IPAddress IP { get { return ip; } }
        public int Port { get { return port; } }

        public IPEndPoint EndPoint
        {
            get
            {
                return new IPEndPoint(ip, port);
            }
        }

        public int LocalPort { get { return localPort; } }

    }
}
