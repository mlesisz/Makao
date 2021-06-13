using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Server.Data;
using System.IO;
using System.Collections.Generic;

namespace Server
{
    
    class Server
    {
        private static Database database;
        public static List<Croupier> croupiers = new List<Croupier>();
        public string Address { get; set; }
        public int Port { get; set; }

        public Server(string address, int port)
        {
            Address = address;
            Port = port;
        }

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Db_Makao.db3"));
                }
                return database;
            }
        }

        public Socket GetSocket()
        {
            var endPoint = new IPEndPoint(IPAddress.Parse(Address), Port);
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPoint);
            socket.Listen(10);
            return socket;
        }

        public void Run()
        {
            var socket = GetSocket();
            while (true)
            {
                try
                {
                    Socket connection = socket.Accept();
                    ClientThread clientThread = new(connection);
                    Thread thread = new(new ThreadStart(clientThread.Run));

                    thread.Start();

                    DataLog dataLog = new()
                    {
                        Host = ((IPEndPoint)connection.RemoteEndPoint).Address.ToString(),
                        Port = ((IPEndPoint)connection.RemoteEndPoint).Port,
                        Message = "Connected to the host.",
                    };
                    Logs.Info(dataLog);
                }
                catch (Exception e)
                {
                    Logs.Error(new DataLog() { Message = e.Message });
                    Console.WriteLine(e);
                }
            }
        }
        public void StartServer()
        {
            Run();
        }
    }
    class Program
    {
        static void Main()
        {
            Server server = new(Settings.IP,Settings.Port);
            server.StartServer();
        }
    }
}
