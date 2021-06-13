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
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public Response(string response)
        {
            Action = response.Split("Action:")[1].Split("\r\n")[0];
            if(response.Contains("Status:"))
                Status = response.Split("Status:")[1].Split("\r\n")[0];
            if (response.Contains("Message:"))
                Message = response.Split("Message:")[1].Split("\r\n")[0];
            else
                Message = null;

            if (response.Contains("Data:"))
                Data = response.Split("Data:")[1].Split("\r\n")[0];
            else
                Data = null;
        }
    }
}
