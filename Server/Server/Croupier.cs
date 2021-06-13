using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Croupier
    {
        public string NameTable { get; set; }
        public bool FreeChairs { get; set; }
        private static List<string> carts = new List<string>() {
            "2 Trefl", "3 Trefl", "4 Trefl", "5 Trefl", "6 Trefl", "7 Trefl", "8 Trefl", "9 Trefl", "10 Trefl", "Walet Trefl", "Dama Trefl", "Król Trefl", "As Trefl",
            "2 Karo", "3 Karo", "4 Karo", "5 Karo", "6 Karo", "7 Karo", "8 Karo", "9 Karo", "10 Karo", "Walet Karo", "Dama Karo", "Król Karo", "As Karo",
            "2 Kier", "3 Kier", "4 Kier", "5 Kier", "6 Kier", "7 Kier", "8 Kier", "9 Kier", "10 Kier", "Walet Kier", "Dama Kier", "Król Kier", "As Kier",
            "2 Pik", "3 Pik", "4 Pik", "5 Pik", "6 Pik", "7 Pik", "8 Pik", "9 Pik", "10 Pik", "Walet Pik", "Dama Pik", "Król Pik", "As Pik"
        };
        private List<string> CartsToBeDealt  { get; set; }
        private List<string> CartsPlayed { get; set; }
        private User[] users = new User[3];
        private Queue<(User,Request)> RequestGame { get; set; }
        public Croupier(string nameTable,User user)
        {
            FreeChairs = true;
            RequestGame = new Queue<(User, Request)>();
            NameTable = nameTable;
            users[0] = user;
            CartsToBeDealt = new List<string>();
            CartsPlayed = new List<string>();
        }
        private void StartGame()
        {
            SendGameStatusToPlayers(Response.CreateResponse("Status",message: "Wszystkie miesjca przy stoliku zajęte. Rozpoczęto tasowanie kart."));
            CardShuffling();
            DealTheCarts();
            Run();
        }
        private void Run()
        {
            
            while (users[0]!= null && users[1] != null && users[2] != null)
            {
                
                if (FreeChairs == true)
                    break;

                Thread.Sleep(2000);
            }
            SendGameStatusToPlayers(Response.CreateResponse("State", message: "Odbieranie kart od graczy."));
        }
        private void DealTheCarts()
        {
         foreach(User user in users)
            {
                user.Cards = new List<string>();
                for (int i = 0; i < 5; i++)
                {
                    user.Cards.Add(CartsToBeDealt.First());
                    Response.SendResponse(user.Socket, Response.CreateResponse("Take", "OK",CartsToBeDealt.First()));
                    CartsToBeDealt.RemoveAt(0);
                }
            }   
        }
        private void CardShuffling()
        {
            var rand = new Random();
            int numberCart;
            List<string> c = new List<string>();
            foreach (string s in carts)
                c.Add(s);

            while(c.Count > 0)
            {
                numberCart = rand.Next(0, c.Count - 1);
                CartsToBeDealt.Add(c[numberCart]);
                c.RemoveAt(numberCart);
            }
        }
        public void SendGameStatusToPlayers(string response)
        {
            foreach(User user in users)
            {
                if (user != null)
                    Response.SendResponse(user.Socket, response);
            }
        }
        public bool Join(User user)
        {
            for(int i=0; i < 3; i++)
            {
                if(users[i]== null)
                {
                    SendGameStatusToPlayers(Response.CreateResponse("State", message: "Gracz " + user.Nick + " dosiadł się do stołu."));
                    users[i] = user;
                    if (users[0] != null && users[1] != null && users[2] != null)
                    {
                        FreeChairs = false;
                        Thread thread = new Thread(new ThreadStart(this.Run));
                        thread.Start();
                    }    
                    return true;
                }
            }
            return false;
        }
        public bool Leave (User user)
        {
            for(int i = 0; i < 3; i++)
            {
                if(users[i] == user)
                {
                    users[i] = null;
                    FreeChairs = true;
                    SendGameStatusToPlayers(Response.CreateResponse("State",message: "Gracz "+ user.Nick + " odszedł od stołu."));
                    RemoveFromListServer();
                    return true;
                }
            }
            return false;
        }
        private void RemoveFromListServer()
        {
            if(users[0]== null && users[1] == null && users[2] == null)
            {
                try
                {
                    Monitor.Enter(Server.croupiers);
                    Server.croupiers.Remove(this);
                    Monitor.Exit(Server.croupiers);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public void AddRequest(User user, Request request)
        {
            Monitor.Enter(RequestGame);
            RequestGame.Enqueue((user,request));
            Monitor.Exit(RequestGame);
        }
    }
}
