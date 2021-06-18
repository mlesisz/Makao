using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
   /* public static class  ActionOrganizer
    {
        public static async Task<string> RegisterAsync(Request request)
        {
            string response;
            User user = null;
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
                    response = Response.CreateResponse("Register", "OK");
                }
                else
                    response = Response.CreateResponse("Register", "WRONG", message: "Podany login jest zajęty.");
            }
            catch (Exception e)
            {
                response = Response.CreateResponse("Register", "ERROR", message: e.Message);
            }
            return response;
        }
        public static async (string, User) Login(Request request)
        {
            string response;
            User user = null;
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
        }
    }*/
}
