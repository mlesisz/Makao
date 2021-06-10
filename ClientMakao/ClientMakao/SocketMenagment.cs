using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientMakao
{
    public class SocketMenagment
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public Socket socket;

        public SocketMenagment(string address, int port)
        {
            Address = address;
            Port = port;
        }
        public string StartConnect()
        {
            var endPoint = new IPEndPoint(IPAddress.Parse(Address), Port);
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(endPoint);
                
                //Console.WriteLine($"Połączyłeś się z: {endPoint.Address}:{endPoint.Port}");
                return $"Połączyłeś się z: {endPoint.Address}:{endPoint.Port}";
            }
            catch (Exception e)
            {
                //Console.Error.WriteLine($"Nie można się połączyć z: {endPoint.Address}:{endPoint.Port}");
                return $"Nie można połączyć się z: {endPoint.Address}:{endPoint.Port} "+e.Message;
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
        public string ReceiveResponse()
        {
            try
            {
                byte[] buffer = new byte[1024];
                string msg = "";
                int len = 0;
                while (!msg.Contains("\r\n\r\n"))
                {
                    len = socket.Receive(buffer);
                    msg += Encoding.UTF8.GetString(buffer.Take(len).ToArray());
                }
                Console.WriteLine(msg);
                return msg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}
