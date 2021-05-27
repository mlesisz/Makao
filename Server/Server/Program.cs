using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using Server.Data;
using System.IO;

namespace Server
{
    public class ClientThread
    {
        public Socket Socket { get; set; }
        public ClientThread(Socket socket)
        {
            Socket = socket;
        }
        
        public void Run()
        {
            while (true)
            {
                byte[] buffer = new byte[256];
                int len=Socket.Receive(buffer);
                string msg = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(msg);
                buffer = Encoding.UTF8.GetBytes(msg);
                Socket.Send(buffer.Take(len).ToArray());
            }
        }
    }
    class Server
    {
        private static Database database;
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

        public void Run()
        {
            var endPoint = new IPEndPoint(IPAddress.Parse(Address), Port);
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPoint);
            socket.Listen(10);
            try
            {                
                while (true)
                {
                    try
                    {
                        Socket connection = socket.Accept();
                        ClientThread clientThread = new ClientThread(connection);
                        Thread thread = new Thread(new ThreadStart(clientThread.Run));
                        thread.Start();
                        DataLog dataLog = new DataLog()
                        {
                            host = ((IPEndPoint)connection.RemoteEndPoint).Address.ToString(),
                            port = ((IPEndPoint)connection.RemoteEndPoint).Port,
                            message = "Connected to the host.",
                            action = "empty"
                        };
                        Logs.Info(dataLog);
                    }
                    catch (Exception e)
                    {

                    }
                    
                    
                }
            }
            catch
            {

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
