using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace server1
{
    class Program
    {
        static TcpListener server = null;
        static TcpClient clientSocket = null;
        static int counter = 0;

        public static Dictionary<TcpClient, string> clientList = new Dictionary<TcpClient, string>();
        static void Main(string[] args)
        {
            Thread t = new Thread(InitSocket);
            t.IsBackground = true;
            t.Start();
            t.Join();
        }
        private static void InitSocket()
        {
            server = new TcpListener(IPAddress.Any, 13000);
            clientSocket = default(TcpClient);
            server.Start();
            Console.WriteLine(">> Server Started");

            while (true)
            {
                try
                {
                    counter++;
                    clientSocket = server.AcceptTcpClient();
                    Console.WriteLine(">> Accept conncetion from client");

                    NetworkStream stream = clientSocket.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string userName = Encoding.Unicode.GetString(buffer, 0, buffer.Length);
                    userName = userName.Substring(0, userName.IndexOf("$"));

                    clientList.Add(clientSocket, userName);

                    SendMessageAll(clientSocket, userName + "님이 입장하셨습니다.", "", false);
                    SaveLog(userName, userName + "입장하셨습니다.");

                    HandleClient h_client = new HandleClient();
                    h_client.OnReceived += new HandleClient.MessageDisplayHandler(OnReceived);
                    h_client.OnDisconnected += new HandleClient.DisconnectedHandler(h_client_OnDisconnected);
                    h_client.StartClient(clientSocket, clientList);
                }
                catch (SocketException se)
                {
                    Trace.WriteLine(string.Format("InitSocket - SocketException : {0}", se.Message));
                    break;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("InitSocket - Exception : {0}", ex.Message));
                    break;
                }
            }
            clientSocket.Close();
            server.Stop();
        }
        private static void SaveLog(string userName, string message)
        {
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                writer.WriteLine("DateTime : {0} User : {1} Message : {2}", DateTime.Now.ToString(), userName, message);
            }
        }
        private static void h_client_OnDisconnected(TcpClient clientSocket)
        {
            if (clientList.ContainsKey(clientSocket))
                clientList.Remove(clientSocket);
        }

        private static void OnReceived(TcpClient clientSocket, string message, string userName)
        {
            string displayMessage = userName + " : " + message;
            Console.WriteLine("{0}", displayMessage);
            SendMessageAll(clientSocket, message, userName, true);
            SaveLog(userName, message);
        }


        private static void SendMessageAll(TcpClient clientSocket, string message, string userName, bool flag)
        {
            foreach (var item in clientList)
            {
                if (item.Key != clientSocket)
                {
                    Trace.WriteLine(string.Format("tcpclient : {0} userName : {1}", item.Key, item.Value));

                    TcpClient client = item.Key as TcpClient;
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = null;

                    if (flag)
                        buffer = Encoding.Unicode.GetBytes(userName + " : " + message);
                    else
                        buffer = Encoding.Unicode.GetBytes(message);

                    stream.Write(buffer, 0, buffer.Length);
                    stream.Flush();
                }
            }
        }
    }
}
