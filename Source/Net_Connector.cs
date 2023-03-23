using System.Data;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using System.Threading;
using System.Windows.Input;

namespace SaY_DeF.Source
{
    class Net_Connector
    {
        private Thread thread;
        Settings settings;
        public Net_Connector()
        {
            if (thread == null)
            {
                thread = new Thread(new ThreadStart(ThreadFuncReceive));
                thread.IsBackground = true;
                thread.Start();
            }
        }
        public void ThreadFuncReceive()
        {
            try
            {
                //UdpClient uClient = new UdpClient(settings.LocalPort);
                //IPEndPoint ipEnd = null;
                //while (true)
                //{
                //    byte[] responce = uClient.Receive(ref ipEnd);
                //    string strResult = Encoding.Unicode.GetString(responce);

                //    Command command = CommandManager.GetCommand(strResult, ipEnd.Address);

                //    if (command.CommandType != CommandType.NotCommand)
                //    {
                //        if (reciveCommand != null)
                //            reciveCommand.Invoke(this, new CommandArgs() { Command = command });
                //    }
                //    else if (reciveMessage != null)
                //    {
                //        reciveMessage.Invoke(this, new MessageArgs() { Message = strResult, Address = ipEnd.Address });
                //    }
                //}
                //uClient.Close();
            }
            catch (SocketException sockEx)
            {
                Console.WriteLine("Socket exception: " + sockEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
            }
        }
    }
}
