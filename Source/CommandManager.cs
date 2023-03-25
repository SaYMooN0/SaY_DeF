﻿using System.Net;

namespace SaY_DeF.Source
{
    internal static class CommandManager
    {
        public const string Divider = "\n";
        public const string CommandFlag = "\r";
        public const string Request = "RQST";
        public const string RequestAgreed = "AGR";
        public static Command GetCommand(string message, IPAddress address)
        {
            string[] args = message.Split(Divider);
            if (args[0] != CommandFlag)
                return new Command(CommandType.NotCommand, null, null);
            switch (args[1])
            {
                case Request: return new Command(CommandType.ConnectionRequest, args[2..], address);
                case RequestAgreed: return new Command(CommandType.RequestApproved, args[2..], address);
                default: return null;
            }
        }
        public static string GetConnectionRequest(string nickname)
        {
            return CommandFlag + Divider + Request + Divider + nickname;
        }
        public static string GetRequestAgreed(string nickname)
        {
            return CommandFlag + Divider + RequestAgreed + Divider + nickname;
        }
    }
    public enum CommandType
    {
        NotCommand,
        RequestApproved,
        ConnectionRequest
    }
}
