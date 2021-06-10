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
        public int? ContentLength { get; set; }
        public object Body { get; set; }
        public Request(string request)
        {
            Action = request.Split("Action:")[1].Split("\r\n")[0];
            GetContentLength(request);
            if( ContentLength != null && ContentLength != 0)
                GetBody(request.Split("Data:")[1].Split("\r\n")[0]);
        }
        private void GetContentLength(string request)
        {
            if (request.Contains("Content-Length:"))
            {
                ContentLength = int.Parse(request.Split("Content-Length:")[1].Split("\r\n")[0]);
            }
            else
            {
                ContentLength = null;
            }
        }
        public void GetBody(string data)
        {
            if(Action == "Register" || Action == "Login")
            {
                BodyRegisterOrLogin body = new BodyRegisterOrLogin();
                body.Nick = data.Split("Nick:")[1].Split("\r\n")[0];
                body.Password = data.Split("Password:")[1].Split("\r\n")[0];
                Body = body;
            }
        }
    }
    public class BodyRegisterOrLogin
    {
        public string Nick { get; set; }
        public string Password { get; set; }
    }
}
