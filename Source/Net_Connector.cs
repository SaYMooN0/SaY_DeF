using System.Data;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using System.Threading;
using System.Windows.Input;
using System.Windows;

namespace SaY_DeF.Source
{
    class Net_Connector
    {
        private Thread thread;
        Settings settings;
        public event EventHandler<Command> requsetGot;
        public Net_Connector()
        {
            settings = new Settings();
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
                UdpClient uClient = new UdpClient(settings.LocalPort);
                IPEndPoint ipEnd = null;
                while (true)
                {
                    byte[] responce = uClient.Receive(ref ipEnd);
                    string strResult = Encoding.Unicode.GetString(responce);


                    Command command = CommandManager.GetCommand(strResult, ipEnd.Address);
                    if (command.CommandType == CommandType.NotCommand)
                    {
                        MessageBox.Show(strResult);
                    }
                    else 
                    {
                        CommandProcessing(command);
                    }
                }
                uClient.Close();
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
        public void Send(string message, IPAddress address)
        {
            UdpClient uClient = new UdpClient();

            IPEndPoint ipEnd = new IPEndPoint(address, settings.LocalPort);
            try
            {
                byte[] bytes = Encoding.Unicode.GetBytes(message);
                uClient.Send(bytes, bytes.Length, ipEnd);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
            }
            finally
            {
                uClient.Close();
            }
        }
        private void CommandProcessing(Command com)
        {
            switch (com.CommandType)
            {
                case CommandType.ConnectionRequest:
                    {
                         if (requsetGot != null)
                            requsetGot.Invoke(this, com);
                        break;
                    }
            }

        }
    }
}

