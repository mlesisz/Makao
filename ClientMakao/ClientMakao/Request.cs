using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMakao
{
    public static class Request
    {
        private static int BodyLen(string body)
        {
            return body.Length;
        }
        public static string Login(string login, string password)
        {
            string body = "Data:Login:"+login+ "\r\nPassword:" + password;
            return "Action:Login\r\nContent-Length:"+BodyLen(body)+"\r\n"+ body + "\r\n\r\n";
        }
        public static string Register (string login, string password)
        {
            string body = "Data:Login:" + login + "\r\nPassword:" + password;
            return "Action:Register\r\nContent-Length:" + BodyLen(body) + "\r\n" + body + "\r\n\r\n";
        }
        public static string Logout()
        {
            return "Action:Logout\r\nContent-Length:0\r\n\r\n";
        }
        public static string CreateTable()
        {
            return "Action:Create\r\nContent-Length:0\r\n\r\n";
        }
        public static string ListTable()
        {
            return "Action:List\r\nContent-Length:0\r\n\r\n";
        }
        public static string JoinTable(int number)
        {
            string body = "Data:Number:" + number.ToString();
            return "Action:Join\r\nContent-Length:"+BodyLen(body)+ "\r\n" + body+"\r\n\r\n";
        }
        public static string LeaveTable()
        {
            return "Action:Leave\r\nContent-Length:0\r\n\r\n";
        }
        public static string TakeCart()
        {
            return "Action:Take\r\nContent-Length:0\r\n\r\n";
        }
        public static string PlayCard(string name)
        {
            string body = "Data:Name:" + name;
            return "Action:Play\r\nContent-Length:"+BodyLen(body)+ "\r\n" + body +"\r\n\r\n";
        }
        public static string End()
        {
            return "Action:End\r\nContent-Length:0\r\n\r\n";
        }
    }
}
