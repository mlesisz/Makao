using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Response
    {
        private static int BodyLen(string body)
        {
            return body.Length;
        }
        public static string Register(string status,string? message)
        {
            if (message == null)
                return "Action:Register\r\nContent-Length:0\r\nStatus:" + status + "\r\n\r\n";
            else
            {
                string body = "Message:" + message;
                return "Action:Register\r\nContent-Length:" + BodyLen(body) + "\r\nStatus:" + status + "\r\nMessage:" + message + "\r\n\r\n";
            }
        }
        public static string Login(string status,string token, string? message)
        {
            if (message == null)
                return "Action:Login\r\nContent-Length:0\r\nStatus:" + status +"\r\nToken:"+ token +"\r\n\r\n";
            else
            {
                string body = "Message:" + message;
                return "Action:Login\r\nContent-Length:" + BodyLen(body) + "\r\nStatus:" + status + "\r\nMessage:" + message + "\r\n\r\n";
            }
        }
        public static string Logout(string status)
        {
            return "Action:Logout\r\nContent - Length:0\r\nStatus:"+status+"\r\n\r\n";
        }
    }
}
