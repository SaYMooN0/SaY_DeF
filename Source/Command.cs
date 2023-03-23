using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SaY_DeF.Source
{
    internal class Command
    {
        public Command(CommandType commandType, string[] commandArguments, IPAddress address)
        {
            CommandType = commandType;
            CommandArguments = commandArguments;
            Address = address;
        }

        public CommandType CommandType { get; set; }
        public string[] CommandArguments { get; set; }
        public IPAddress Address { get; set; }
    }
}
