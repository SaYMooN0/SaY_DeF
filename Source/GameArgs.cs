using System.Net;

namespace SaY_DeF.Source
{
    internal class GameArgs
    {
        public IPAddress MyIP { get; set; }
        public IPAddress EnIP { get; set; }
        public IPAddress ServerIP { get; set; }
        public string MyNick { get; set; }
        public string EnNick { get; set; }
        public GameArgs(IPAddress myIP, string myNick,IPAddress enIP, string enNick, IPAddress serverIP)
        {
            MyIP = myIP;
            EnIP = enIP;
            MyNick = myNick;
            EnNick = enNick;
            ServerIP = serverIP;
        }
        public override string ToString()
        {
            return $"MyNicK:  {MyNick}\t{MyIP}\nEnemyIP:{EnNick}\t{EnIP}";
        }
    }
}
