using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMakao
{
    public static class Request
    {
        public static string Login(string login, string password)
        {
            string body = "Data:Login:"+login+ "\r\nPassword:" + password;
            return "Action:Login\r\n"+ body + "\r\n\r\n";
        }
        public static string Register (string login, string password)
        {
            string body = "Data:Login:" + login + "\r\nPassword:" + password;
            return "Action:Register\r\n" + body + "\r\n\r\n";
        }
        public static string Logout(string token)
        {
            return "Action:Logout\r\nToken:"+token+"\r\n\r\n";
        }
        public static string CreateTable(string name, string token)
        {
            return "Action:Create\r\nToken:"+token+"\r\nData:"+name+"\r\n\r\n";
        }
        public static string ListTable(string token)
        {
            return "Action:List\r\nToken:"+token+"\r\n\r\n";
        }
        public static string JoinTable(string nameTable, string token)
        {
            string body = "Data:" + nameTable;
            return "Action:Join\r\nToken:"+token+"\r\n" + body+"\r\n\r\n";
        }
        public static string LeaveTable(string token)
        {
            return "Action:Leave\r\nToken:"+token+"\r\n\r\n";
        }
        public static string TakeCart(string token)
        {
            return "Action:Take\r\nToken:"+token+"\r\n\r\n";
        }
        public static string PlayCard(string name, string token)
        {
            string body = "Data:Name:" + name;
            return "Action:Play\r\nToken:"+token+"\r\n" + body +"\r\n\r\n";
        }
        public static string End()
        {
            return "Action:End\r\n\r\n";
        }
    }
}
