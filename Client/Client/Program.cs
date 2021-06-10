using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client
{
    public class Client {
        public string Address { get; set; }
        public int Port { get; set; }
        protected Socket socket;

        public Client(string address, int port, string nick)
        {
            Address = address;
            Port = port;
        }
        public Socket StartConnect()
        {
            var endPoint = new IPEndPoint(IPAddress.Parse(Address), Port);
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(endPoint);
                Console.WriteLine($"Połączyłeś się z: {endPoint.Address}:{endPoint.Port}");
                return socket;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Nie można się połączyć z: {endPoint.Address}:{endPoint.Port}");
                Environment.Exit(1);
                return null;
            }
            
        }
        public bool SendRequest(string request)
        {
            try
            {
                var data = Encoding.UTF8.GetBytes(request);
                socket.Send(data);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public string RecveiveMessage()
        {
            try
            {
                byte[] buffer = new byte[256];
                string msg = "";
                while (!msg.Contains("\r\n\r\n"))
                {
                    socket.Receive(buffer);
                    msg += Encoding.UTF8.GetString(buffer);
                }
                return msg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }
        public static void HandleInput()
        {

        }
        public void Run()
        {
            socket = StartConnect();
            if(socket != null)
            {
                Thread thread = new(start: new ThreadStart(HandleInput));
                thread.Start();
                while (true)
                {
                    
                    string msg = RecveiveMessage();
                    if(msg == null)
                    {
                        break;
                    }
                }
                Console.WriteLine("Disconnected from server");
            }
        }
    
    }
    class Program
    {
        static void Main()
        {
            Console.Write("Podaj nick: ");
            string nick = Console.ReadLine();
            Client client = new Client(Settings.IP, Settings.Port,);
            client.Run();
        }
    }
}
