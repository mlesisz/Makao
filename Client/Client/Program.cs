using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Text.Encoding;

namespace Client
{
    class Program
    {
        static void Main()
        {
            var endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 500);
            using (var socket = new Socket(SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    socket.Connect(endPoint);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"Couldn't connect to {endPoint.Address}:{endPoint.Port}");
                    Environment.Exit(1);
                }
                Console.WriteLine("Connected. Enter blank line to quit");
                while (true)
                {
                    Console.Write("Enter your message: ");
                    var message = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(message)) { break; }
                    var data = ASCII.GetBytes(message);
                    try
                    {
                        socket.Send(data);
                    }
                    catch (Exception e)
                    {
                        break;
                    }
                    var buffer = new byte[1024];
                    int dataSize = socket.Receive(buffer);
                    var responseMsg = ASCII.GetString(buffer
                        .Take(dataSize)
                        .ToArray());
                    if (string.IsNullOrWhiteSpace(responseMsg)) { break; }
                    Console.WriteLine($"Response: {responseMsg}");
                }
                Console.WriteLine("Disconnected from server");
            }
        }
    }
}
