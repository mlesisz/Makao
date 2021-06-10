using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class ClientThread
    {
        public Socket Socket { get; set; }
        protected string Token { get; set; }
        public DataLog DataLog { get; set; }
        public ClientThread(Socket socket)
        {
            Socket = socket;
            DataLog = new DataLog()
            {
                Host = ((IPEndPoint)Socket.RemoteEndPoint).Address.ToString(),
                Port = ((IPEndPoint)Socket.RemoteEndPoint).Port,
                Message = "",
                Action = ""
            };
        }
        public Request GetRequest()
        {
            try
            {
                byte[] buffer = new byte[1024];
                string msg = "";
                int len = 0;
                while (!msg.Contains("\r\n\r\n"))
                {
                    len = Socket.Receive(buffer);
                    msg += Encoding.UTF8.GetString(buffer.Take(len).ToArray());
                }
                Console.WriteLine(msg);
                return new Request(msg);
            }
            catch (Exception e)
            {
                DataLog.Message = e.Message;
                Logs.Error(DataLog);
                return null;
            }
        }
        public void SendResponse(string response)
        {
            try
            {
                var data = Encoding.UTF8.GetBytes(response);
                Socket.Send(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async void CreateResponse(Request request)
        {
            if (request.Action == "Register")
            {
                User user = await Server.Database.GetUserAsync(((BodyRegisterOrLogin)request.Body).Nick);
                if (user == null)
                {
                    user = new User()
                    {
                        Nick = ((BodyRegisterOrLogin)request.Body).Nick,
                        Password = ((BodyRegisterOrLogin)request.Body).Password
                    };
                    Server.Database.SaveUserAsync(user);
                    SendResponse(Response.Register("OK",null));
                }
                else
                    SendResponse(Response.Register("Error","Podany Nick jest już zajęty"));

            }
            else if (request.Action == "Login")
            {
                User user = await Server.Database.GetUserAsync(((BodyRegisterOrLogin)request.Body).Nick);
                if (user != null)
                {
                    if(user.Password == ((BodyRegisterOrLogin)request.Body).Password)
                    {
                        Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                        SendResponse(Response.Login("OK",Token,null));
                    }else
                        SendResponse(Response.Login("ERROR", null, "Błędne dane logowania."));
                }
                else
                    SendResponse(Response.Login("ERROR",null,"Błędne dane logowania."));
            }
            else if (request.Action == "Logout")
            {
                Token = null;
                SendResponse(Response.Logout("OK"));
            }
            else if (request.Action == "Create")
            {

            }
            else if (request.Action == "List")
            {

            }
            else if (request.Action == "Join")
            {

            }
            else if (request.Action == "Leave")
            {

            }
            else if (request.Action == "Take")
            {

            }
            else if (request.Action == "Play")
            {

            }
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    Request request = GetRequest();
                    if (request != null)
                    {
                        if (request.Action == "End") break;
                        CreateResponse(request);
                    }
                }
                catch (Exception e)
                {
                    DataLog.Message = e.Message;
                    Logs.Error(DataLog);
                }
            }
            Socket.Close();
            Monitor.Enter(Server.clients);
            Server.clients.Remove(this);
            Monitor.Exit(Server.clients);
        }
    }
}
