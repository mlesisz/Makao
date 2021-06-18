using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Response
    {
        private static string CreateResponse(string action, string status ="", string data ="", string message = "")
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
        public static bool SendResponse(Socket socket, string action, string status ="" , string data = "", string message = "")
        {
            
            try
            {
                string response = CreateResponse(action, status, data, message);
                string host = "" + ((IPEndPoint)socket.RemoteEndPoint).Address.ToString() + ":" + ((IPEndPoint)socket.RemoteEndPoint).Port;
                if (action == "State")
                    Logging.OK(action, message,host);
                else
                    switch (status)
                    {
                        case "OK":
                            Logging.OK(action, message, host);
                            break;
                        case "WRONG":
                            Logging.WRONG(action, message, host);
                            break;
                        case "ERROR":
                            Logging.ERROR(action, message, host);
                            break;
                    }

                var msg = Encoding.UTF8.GetBytes(response);
                socket.Send(msg);
                return true;
            }
            catch (Exception e)
            {
                string host = "" + ((IPEndPoint)socket.RemoteEndPoint).Address.ToString() + ":" + ((IPEndPoint)socket.RemoteEndPoint).Port;
                Logging.ERROR(message: e.Message, host: host);
                return false;
            }
        }
    }
}
