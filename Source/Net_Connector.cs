using System.Data;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using System.Threading;
using System.Windows.Input;
using System.Windows;
using System.Threading.Tasks;

namespace SaY_DeF.Source
{
    class Net_Connector
    {
        private Thread thread;
        Settings settings;
        public event EventHandler<Command> requsetGot;
        public event EventHandler<Command> positiveResponse;
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
                TcpListener serverSocket = new TcpListener(settings.LocalPort);
                serverSocket.Start();

                while (true)
                {
                    TcpClient clientSocket = serverSocket.AcceptTcpClient();

                    byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);
                    string strResult = Encoding.Unicode.GetString(bytesFrom);

                    IPEndPoint ipEnd = (IPEndPoint)clientSocket.Client.RemoteEndPoint;

                    Command command = CommandManager.GetCommand(strResult, ipEnd.Address);
                    if (command.CommandType == CommandType.NotCommand)
                    {
                        MessageBox.Show(strResult);
                    }
                    else
                    {
                        CommandProcessing(command);
                    }

                    clientSocket.Close();
                }

                serverSocket.Stop();
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
        public bool Send(string message, IPAddress address)
        {
            TcpClient clientSocket = new TcpClient();

            try
            {
                var connectTask = clientSocket.ConnectAsync(address, settings.LocalPort);
                if (!connectTask.Wait(TimeSpan.FromSeconds(1.5)))
                {
                    throw new TimeoutException("Wrong ip");
                }

                NetworkStream networkStream = clientSocket.GetStream();

                byte[] bytesToSend = Encoding.Unicode.GetBytes(message);
                networkStream.Write(bytesToSend, 0, bytesToSend.Length);
                networkStream.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.Message);
                clientSocket.Close();
                return false;
            }
            clientSocket.Close();
            return true;
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
                case CommandType.RequestApproved:
                    {
                        if (positiveResponse != null)
                            positiveResponse.Invoke(this, com);
                        break;
                    }
            }

        }
    }
}

