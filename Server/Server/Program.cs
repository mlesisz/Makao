using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main()
        {
            var endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 500);
            using (var socket = new Socket(SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(endPoint);
                socket.Listen(10);

                while (true)
                {
                    using( var connection = socket.Accept())
                    {
                        while (true)
                        {
                            byte[] buffer = new byte[1024];
                            int len = 0;
                            try
                            {
                                len = connection.Receive(buffer);
                            }
                            catch (Exception e)
                            {
                                break;
                            }
                            if (len == 0) break;
                            connection.Send(buffer.Take(len).ToArray());
                        }
                    }
                }
            }
        }
    }
}
