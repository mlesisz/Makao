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
        private User User { get; set;}
        private Croupier Croupier { get; set; }
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

        public async Task  RequestHandling(Request request)
        {
            string response ="";
            User user;
            switch (request.Action)
            {
                case "Register":
                    try
                    {
                        user = await Server.Database.GetUserAsync(((BodyRegisterOrLogin)request.Data).Login);
                        if (user == null)
                        {
                            user = new User()
                            {
                                Nick = ((BodyRegisterOrLogin)request.Data).Login,
                                Password = ((BodyRegisterOrLogin)request.Data).Password
                            };
                            Server.Database.SaveUserAsync(user);
                            response=Response.CreateResponse("Register", "OK");
                        }
                        else
                            response = Response.CreateResponse("Register", "WRONG", message: "Podany login jest zajęty.");
                    } catch ( Exception e)
                    {
                        response = Response.CreateResponse("Register", "ERROR", message: e.Message);
                    }
                    break;
                case "Login":
                    try
                    {
                        user = await Server.Database.GetUserAsync(((BodyRegisterOrLogin)request.Data).Login);
                        if (user != null)
                        {
                            if (user.Password == ((BodyRegisterOrLogin)request.Data).Password)
                            {
                                Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                                response = Response.CreateResponse("Login", "OK", Token);
                                user.Socket = Socket;
                                User = user;
                            }
                            else
                                response = Response.CreateResponse("Login", "WRONG", message: "Błędne dane logowania.");
                        }
                        else
                            response = Response.CreateResponse("Login", "WRONG", message: "Błędne dane logowania.");
                    }
                    catch (Exception e)
                    {
                        response = Response.CreateResponse("Login", "ERROR", message: e.Message);
                    }
                    
                    break;
                case "Logout":
                    Token = null;
                    if(Croupier != null)
                    {
                        Croupier.AddRequest(User, new Request() { Action = "Leave" });
                    }
                    User = null;
                    response = Response.CreateResponse("Logout","OK");
                    break;
                case "Create":
                    try
                    {
                        Monitor.Enter(Server.croupiers);
                        bool nameExsist = false;
                        foreach(Croupier cr in Server.croupiers)
                        {
                            if((string)request.Data == cr.NameTable)
                            {
                                nameExsist = true;
                                break;
                            }
                        }
                        if(nameExsist == false)
                        {
                            Croupier croupier = new Croupier((string)request.Data, User);
                            Croupier = croupier;
                            Server.croupiers.Add(croupier);
                            response = Response.CreateResponse(request.Action, "OK", message: "Utworzyłeś stół do gry o nazwie: "+request.Data);
                        }else
                            response = Response.CreateResponse(request.Action, "WRONG",message: "Podana nazwa pokoju już jest zajęta.");
                        Monitor.Exit(Server.croupiers);
                    }
                    catch(Exception e)
                    {
                        response = Response.CreateResponse(request.Action, "ERROR",message: e.Message);
                    }
                    
                    break;
                case "List":
                    try
                    {
                        Monitor.Enter(Server.croupiers);
                        if (Server.croupiers.Count == 0)
                        {
                            response = Response.CreateResponse("List", "WRONG", message: "Brak stolików z wolnymi miejscami.");
                        }
                        else
                        {
                            string list ="";
                            foreach (var croupier in Server.croupiers)
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
                                response = Response.CreateResponse("List", "WRONG", message: "Brak stolików z wolnymi miejscami.");
                            else
                                response = Response.CreateResponse("List", "OK", list);
                        }
                        Monitor.Exit(Server.croupiers);
                    }catch( Exception e)
                    {
                        response = Response.CreateResponse("List", "ERROR", message: e.Message);
                    }
                    break;
                case "Join":
                    try
                    {
                        Monitor.Enter(Server.croupiers);
                        foreach (var croupier in Server.croupiers)
                        {
                            if (croupier.NameTable == (string)request.Data)
                            {
                                if (croupier.Join(User))
                                {
                                    Croupier = croupier;
                                    response = Response.CreateResponse("Join", "OK", message: "Dołączyłeś do stolika: "+croupier.NameTable);
                                }
                                else
                                    response = Response.CreateResponse("Join", "WRONG", message: "Wybrany stolik już jest pełny.");
                                break;
                            }
                        }
                        Monitor.Exit(Server.croupiers);
                    }
                    catch(Exception e)
                    {
                        response = Response.CreateResponse("Join", "ERROR", message: e.Message);
                    }
                    
                    break;
                case "Leave":
                    try
                    {
                        Monitor.Enter(Croupier);
                        if (Croupier.Leave(User))
                        {
                            response = Response.CreateResponse(request.Action,"OK");
                            Monitor.Exit(Croupier);
                            Croupier = null;
                        }
                        else
                        {
                            Monitor.Exit(Croupier);
                            response = Response.CreateResponse(request.Action, "WRONG", message: "Próba opuszczenia pokoju nie powiodła się.");
                        }
                    }catch(Exception e)
                    {
                        response = Response.CreateResponse("Leave", "ERROR", message: e.Message);
                    }
                    break;
                case "Take":
                    Croupier.AddRequest(User, request);
                    break;
                case "Play":
                    Croupier.AddRequest(User, request);
                    break;
            }
            if(response != "")
                Response.SendResponse(Socket,response);
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
                        if (request.Token == Token)
                            RequestHandling(request);
                        else
                            Response.SendResponse(Socket,Response.CreateResponse(request.Action, "ERROR", message: "Podany Token jest nie prawidłowy"));
                    }
                    else
                        break;
                }
                catch (Exception e)
                {
                    DataLog.Message = e.Message;
                    Logs.Error(DataLog);
                }
            }
            Socket.Close();
        }
    }
}
