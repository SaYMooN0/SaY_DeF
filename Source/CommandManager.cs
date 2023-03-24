using System.Net;

namespace SaY_DeF.Source
{
    internal static class CommandManager
    {
        public const string Divider = "\n";
        public const string CommandFlag = "\r";
        public const string Request = "Game";
        public static Command GetCommand(string message, IPAddress address)
        {
            string[] args = message.Split(Divider);
            if (args[0] != CommandFlag)
                return new Command(CommandType.NotCommand, null, null);
            switch (args[1])
            {
                case Request: return new Command(CommandType.ConnectionRequest, args[2..], address);
                default: return null;
            }
        }
        public static string GetGameStartRequest(string nickname)
        {
            return CommandFlag + Divider + Request + Divider + nickname;
        }
    }
    public enum CommandType
    {
        NotCommand,
        ConnectionRequest
    }
}
