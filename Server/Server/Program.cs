using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Server.Data;
using System.IO;
using System.Collections.Generic;

namespace Server
{
    
    class MakaoServerProtocol
    {
        private static Database database;
        public static List<Croupier> croupiers = new List<Croupier>();
        public string Address { get; set; }
        public int Port { get; set; }

        public MakaoServerProtocol(string address, int port)
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
                    database = new Database(Path.Combine(Environment.CurrentDirectory, "Db_Makao.db3"));
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
            Logging.Info("The server started on the address " + Address+" and port "+Port);
            while (true)
            {
                try
                {
                    Socket connection = socket.Accept();
                    ClientThread clientThread = new(connection);
                    Thread thread = new(new ThreadStart(clientThread.Run));

                    thread.Start();

                    Logging.Info("The server accepted the connection and created a new thread for it.");
                }
                catch (Exception e)
                {
                    Logging.ERROR(message: e.Message);
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
            MakaoServerProtocol server = new(Settings.IP,Settings.Port);
            server.StartServer();
        }
    }
}
