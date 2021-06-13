using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Response
    {
        public static string CreateResponse(string action, string status ="", string data ="", string message = "")
        {
            string res = "Action:" + action + "\r\n";
            if(status != "")
            {
                res += "Status:" + status + "\r\n";
            }
            if(data != "")
            {
                res += "Data:" + data + "\r\n";
            }
            if(message != "")
            {
                res += "Message:" + message + "\r\n";
            }
            res += "\r\n";
            return res;
        }
        public static bool SendResponse(Socket socket, string response)
        {
            try
            {
                var data = Encoding.UTF8.GetBytes(response);
                socket.Send(data);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
