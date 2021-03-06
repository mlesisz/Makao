using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Server
{
    public class ClientThread
    {
        public Socket Socket { get; set; }
        protected string Token { get; set; }
        private User User { get; set; }
        private Croupier Croupier { get; set; }
        private string host;
        public ClientThread(Socket socket)
        {
            host = "" + ((IPEndPoint)socket.RemoteEndPoint).Address.ToString() + ":" + ((IPEndPoint)socket.RemoteEndPoint).Port;
            Socket = socket;
        }
        public (Request,string) GetRequest(string rest)
        {
            try
            {
                byte[] buffer = new byte[256];
                string msg = rest;
                string r = "";
                int len = 0;
                while (!msg.Contains("\r\n\r\n"))
                {
                    len = Socket.Receive(buffer);
                    msg += Encoding.UTF8.GetString(buffer.Take(len).ToArray());
                }
                (msg,r)= ParseRecevedData(msg);
                Logging.Info(host+" send request: "+msg);
                return (new Request(msg),r);
            }
            catch (Exception e)
            {
                Logging.ERROR(message: e.Message, host: host);
                return (null,rest);
            }
        }
        private (string, string) ParseRecevedData(string data)
        {
            string msg = data.Split("\r\n\r\n")[0] + "\r\n\r\n";
            string rest = data.Remove(0, msg.Length);
            return (msg, rest);
        }

        public async Task RequestHandling(Request request)
        {
            switch (request.Action)
            {
                case "Register":
                    try
                    {
                       if(await new Authentication().Register(((BodyRegisterOrLogin)request.Data).Login, ((BodyRegisterOrLogin)request.Data).Password))
                        {
                            Response.SendResponse(Socket, "Register", "OK");
                        }
                        else
                            Response.SendResponse(Socket, "Register", "WRONG", message: "Podany login jest zajęty.");
                    }
                    catch (Exception e)
                    {
                        Response.SendResponse(Socket, "Register", "ERROR", message: e.Message);
                    }
                    break;
                case "Login":
                    try
                    {
                        (User, Token) = await new Authentication().Login(((BodyRegisterOrLogin)request.Data).Login, ((BodyRegisterOrLogin)request.Data).Password);
                        if(Token != null)
                        {
                            User.Socket = Socket;
                            Response.SendResponse(Socket, "Login", "OK", Token);
                        }
                        else
                            Response.SendResponse(Socket, "Login", "WRONG", message: "Błędne dane logowania.");
                    }
                    catch (Exception e)
                    {
                        Response.SendResponse(Socket, "Login", "ERROR", message: e.Message);
                    }

                    break;
                case "Logout":
                    Token = null;
                    if (Croupier != null)
                    {
                        Monitor.Enter(Croupier);
                        Croupier.Leave(User);
                        Monitor.Exit(Croupier);
                    }
                    User = null;
                    Response.SendResponse(Socket, "Logout", "OK");
                    break;
                case "Create":
                    try
                    {
                        Monitor.Enter(MakaoServerProtocol.croupiers);
                        bool nameExsist = false;
                        foreach (Croupier cr in MakaoServerProtocol.croupiers)
                        {
                            if ((string)request.Data == cr.NameTable)
                            {
                                nameExsist = true;
                                break;
                            }
                        }
                        if (nameExsist == false)
                        {
                            Croupier croupier = new Croupier((string)request.Data, User);
                            Croupier = croupier;
                            MakaoServerProtocol.croupiers.Add(croupier);
                            Response.SendResponse(Socket, request.Action, "OK");
                        }
                        else
                            Response.SendResponse(Socket, request.Action, "WRONG", message: "Podana nazwa pokoju już jest zajęta.");
                        Monitor.Exit(MakaoServerProtocol.croupiers);
                    }
                    catch (Exception e)
                    {
                        Response.SendResponse(Socket, request.Action, "ERROR", message: e.Message);
                    }

                    break;
                case "List":
                    try
                    {
                        Monitor.Enter(MakaoServerProtocol.croupiers);
                        if (MakaoServerProtocol.croupiers.Count == 0)
                        {
                            Response.SendResponse(Socket, "List", "WRONG", message: "Brak stolików z wolnymi miejscami.");
                        }
                        else
                        {
                            string list = "";
                            foreach (var croupier in MakaoServerProtocol.croupiers)
                            {
                                if (croupier.FreeChairs)
                                {
                                    if (list == "")
                                        list = croupier.NameTable;
                                    else
                                        list += ";" + croupier.NameTable;
                                }
                            }
                            if (list == "")
                                Response.SendResponse(Socket, "List", "WRONG", message: "Brak stolików z wolnymi miejscami.");
                            else
                                Response.SendResponse(Socket, "List", "OK", list);
                        }
                        Monitor.Exit(MakaoServerProtocol.croupiers);
                    }
                    catch (Exception e)
                    {
                        Response.SendResponse(Socket, "List", "ERROR", message: e.Message);
                    }
                    break;
                case "Join":
                    try
                    {
                        Monitor.Enter(MakaoServerProtocol.croupiers);
                        bool join = false;
                        foreach (var croupier in MakaoServerProtocol.croupiers)
                        {
                            if (croupier.NameTable == (string)request.Data)
                            {
                                if (croupier.Join(User))
                                {
                                    Croupier = croupier;
                                    Response.SendResponse(Socket, "Join", "OK", message: "Dołączyłeś do stolika: " + croupier.NameTable);
                                }
                                else
                                    Response.SendResponse(Socket, "Join", "WRONG", message: "Wybrany stolik już jest pełny.");
                                join = true;
                                break;
                            }
                            if (!join)
                                Response.SendResponse(Socket, "Join", "WRONG", message: "Wybrany stolik już nie istnieje.");
                        }
                        Monitor.Exit(MakaoServerProtocol.croupiers);
                    }
                    catch (Exception e)
                    {
                        Response.SendResponse(Socket, "Join", "ERROR", message: e.Message);
                    }

                    break;
                case "Leave":
                    try
                    {
                        Monitor.Enter(Croupier);
                        if (Croupier.Leave(User))
                        {
                            Response.SendResponse(Socket, request.Action, "OK");
                            Monitor.Exit(Croupier);
                            Croupier = null;
                        }
                        else
                        {
                            Monitor.Exit(Croupier);
                            Response.SendResponse(Socket, request.Action, "WRONG", message: "Próba opuszczenia pokoju nie powiodła się.");
                        }
                    }
                    catch (Exception e)
                    {
                        Response.SendResponse(Socket, "Leave", "ERROR", message: e.Message);
                    }
                    break;
                case "Take":
                    if (Croupier.FreeChairs)
                        Response.SendResponse(Socket, request.Action, "WRONG", message: "Gra jeszcze nie wystartowała.");
                    else
                        Croupier.AddRequest(User, request);
                    break;
                case "Play":
                    if (Croupier.FreeChairs)
                        Response.SendResponse(Socket, request.Action, "WRONG", message: "Gra jeszcze nie wystartowała.");
                    else
                        Croupier.AddRequest(User, request);
                    break;
            }
        }

        public void Run()
        {
            string rest = "";
            while (true)
            {
                try
                {
                    Request request;
                    (request,rest)= GetRequest(rest);
                    if (request != null)
                    {
                        if (request.Action == "End")
                        {
                            if (Croupier != null)
                            {
                                Croupier.Leave(User);
                                Croupier = null;
                            }
                            Logging.Info("Host " + host + " terminated the connection.");
                            break;
                        }
                        if (request.Token == Token)
                            RequestHandling(request);
                        else
                            Response.SendResponse(Socket, request.Action, "ERROR", message: "Podany Token jest nie prawidłowy");
                    }
                    else
                        break;
                }
                catch (Exception e)
                {
                    Logging.ERROR(message: e.Message, host: host);
                }
            }
            Socket.Close();
        }
    }
}
