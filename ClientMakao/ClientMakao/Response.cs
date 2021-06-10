using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMakao
{
    class Response
    {
        public string Action { get; set; }
        public int? ContentLength { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public Response(string response)
        {
            Action = response.Split("Action:")[1].Split("\r\n")[0];
            GetContentLength(response);
            Status = response.Split("Status:")[1].Split("\r\n")[0];
            if (ContentLength != null && ContentLength != 0)
            {
                if (response.Contains("Message:"))
                    Message = response.Split("Message:")[1].Split("\r\n")[0];
                else
                    Message = null;

                if (response.Contains("Data:"))
                    GetData(response);
                else
                    Data = null;
            }
        }
        public void GetData(string response)
        {
            if(Action == "Login" && Status == "OK")
            {
                Data = response.Split("Token:")[1].Split("\r\n")[0];
            }
            else
            {
                Data = null;
            }
        }
        private void GetContentLength(string response)
        {
            if (response.Contains("Content-Length:"))
            {
                ContentLength = int.Parse(response.Split("Content-Length:")[1].Split("\r\n")[0]);
            }
            else
            {
                ContentLength = null;
            }
        }
    }
}
