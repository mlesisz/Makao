using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server
{
    public class Request
    {
        public string Action { get; set; }
        public string Token { get; set; }
        public object Data { get; set; }
        public Request()
        {

        }
        public Request(string request)
        {
            Action = request.Split("Action:")[1].Split("\r\n")[0];
            if(request.Contains("Token:"))
                Token=request.Split("Token:")[1].Split("\r\n")[0];
            if (request.Contains("Data:"))
                GetData(request.Split("Data:")[1]);
        }
        public void GetData(string data)
        {
            if(Action == "Register" || Action == "Login")
            {
                BodyRegisterOrLogin body = new BodyRegisterOrLogin();
                body.Login = data.Split("Login:")[1].Split("\r\n")[0];
                body.Password = data.Split("Password:")[1].Split("\r\n")[0];
                Data = body;
            }
            else
            {
                Data = data.Split("\r\n")[0];
            }
        }
    }
    public class BodyRegisterOrLogin
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
