using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
        private static List<string> nonFunctionalCards = new List<string>() { "5", "6", "7", "8", "9", "10", "Król Karo", "Król Trefl" };
        private List<string> CardsToBeDealt  { get; set; }
        private List<string> CardsPlayed { get; set; }
        private User[] users = new User[3];
        int playermovment;
        private Queue<(User,Request)> RequestGame { get; set; }
        public Croupier(string nameTable,User user)
        {
            FreeChairs = true;
            RequestGame = new Queue<(User, Request)>();
            NameTable = nameTable;
            users[0] = user;
            CardsToBeDealt = new List<string>();
            CardsPlayed = new List<string>();
        }
        private void StartGame()
        {
            CardsPlayed.Clear();
            CardsToBeDealt.Clear();
            SendGameStatusToPlayers(Response.CreateResponse("State",message: "Wszystkie miesjca przy stoliku zajęte. Rozpoczęto tasowanie kart."));
            CardShuffling();
            DealTheCarts();
            playermovment = 0;
            while (true)
            {
                SendGameStatusToPlayers(Response.CreateResponse("State", message: 
                    "Karta: "+CardsToBeDealt.First()));
                CardsPlayed.Add(CardsToBeDealt.First());
                if (NonFunctionalCards(CardsToBeDealt.First()))
                {
                    CardsToBeDealt.RemoveAt(0);
                    break;
                }else
                    CardsToBeDealt.RemoveAt(0);
            }
            Run();
        }
        private void Run()
        {
            Response.SendResponse(users[playermovment].Socket, Response.CreateResponse("State",message: "Twój ruch."));
            while (users[0]!= null && users[1] != null && users[2] != null)
            {
                
                if (FreeChairs == true)
                    break;
                CheckPlayerMovment();
                if(users[playermovment].Cards.Count == 0)
                {
                    SendGameStatusToPlayers(Response.CreateResponse("State", message: "Te rozdanie wygrał: "+ users[playermovment].Nick));
                    break;
                }
                Thread.Sleep(1000);
            }
            SendGameStatusToPlayers(Response.CreateResponse("State", message: "Odbieranie kart od graczy."));
            foreach(User user in users)
            {
                if (user != null)
                    user.Cards.Clear();
            }
            if(FreeChairs == false)
            {
                StartGame();
            }
        }
        private void CheckPlayerMovment()
        {
            User user;
            Request request;
            if(RequestGame.Count > 0)
            {
                (user, request) = RequestGame.Dequeue();
                for (int index = 0; index < 3; index++)
                {
                    if (user.Nick == users[index].Nick)
                    {
                        if(index != playermovment)
                        {
                            Response.SendResponse(user.Socket, Response.CreateResponse(request.Action, "WRONG", message: "To nie jest twoja kolej."));
                            break;
                        }

                        if(users[index].ForcMovement != null)
                        {
                            BodyPlayCard body = request.Data as BodyPlayCard;
                            if(body != null && body.Card.Split(" ")[0] == "Dama")
                            {
                                if (PlayerPlayCard(index, request))
                                {
                                    users[index].ForcMovement = null;
                                    NextMove();
                                } 
                            }else if(users[index].ForcMovement is ForcMovementColor)
                            {
                                if(request.Action == "Play")
                                {
                                    ForcMovementColor fc = users[index].ForcMovement as ForcMovementColor;
                                    if (body.Card.Split(" ")[1] == fc.Color)
                                    {
                                        if (PlayerPlayCard(index, request))
                                        {
                                            if (!NonFunctionalCards(body.Card)) ServiceFunctionalCard(body);
                                            NextMove();
                                            users[index].ForcMovement = null;
                                        }
                                    }
                                    else
                                    {
                                        Response.SendResponse(users[index].Socket, Response.CreateResponse(request.Action, "WRONG", 
                                            message: "Musisz zagrać karte o kolorze: "+ fc.Color));
                                    }
                                }
                                else
                                {
                                    if (PlayerTakeCard(index, request))
                                    {
                                        users[NextPlayer()].ForcMovement = users[index].ForcMovement;
                                        users[index].ForcMovement = null;
                                    }

                                }
                                
                            }else if (users[index].ForcMovement is ForcMovementNumber)
                            {
                                if (request.Action == "Play")
                                {
                                    ForcMovementNumber fc = users[index].ForcMovement as ForcMovementNumber;
                                    if (body.Card.Split(" ")[1] == fc.Number)
                                    {
                                        if (PlayerPlayCard(index, request))
                                        {
                                            if(!NonFunctionalCards(body.Card)) ServiceFunctionalCard(body);
                                            NextMove();
                                            users[index].ForcMovement = null;
                                        }
                                    }
                                    else
                                    {
                                        Response.SendResponse(users[index].Socket, Response.CreateResponse(request.Action, "WRONG",
                                            message: "Musisz zagrać karte o numerze: " + fc.Number));
                                    }
                                }else
                                {
                                    if(PlayerTakeCard(index, request))
                                        users[index].ForcMovement = null;
                                }
                            }
                            else if(users[index].ForcMovement is ForcMovementTake)
                            {
                                ForcMovementTake ft = users[index].ForcMovement as ForcMovementTake;
                                if (request.Action == "Play")
                                {
                                    Response.SendResponse(users[index].Socket, Response.CreateResponse(request.Action, "WRONG",
                                            message: "Musisz dobrać " + ft.Number + " karty."));
                                }
                                else
                                {
                                    if (CardsToBeDealt.Count == 0)
                                    {
                                        TakeCardsToCardsToBeDealt();
                                    }
                                    string data = CardsToBeDealt.First();
                                    CardsToBeDealt.RemoveAt(0);
                                    for (int z = 0; z < ft.Number - 1; z++)
                                    {
                                        data += ";" + CardsToBeDealt.First();
                                        CardsToBeDealt.RemoveAt(0);
                                    }
                                    Response.SendResponse(users[index].Socket, Response.CreateResponse(request.Action, "OK",
                                            data: data));
                                    SendGameStatusToPlayers(Response.CreateResponse("State",message: "Gracz " + users[index].Nick + "dobrał " + ft.Number + " karty."));
                                    users[index].ForcMovement = null;
                                    NextMove();
                                }
                            }
                        }
                        else
                        {
                            switch (request.Action)
                            {
                                case "Take":
                                    PlayerTakeCard(index,request);
                                    break;
                                case "Play":
                                    BodyPlayCard body = request.Data as BodyPlayCard;
                                    if (CardToCard(body.Card)) // sprawdza czy można daną kartę położyć na kartę 
                                    {
                                        if(PlayerPlayCard(index, request)) // sprawdza czy gracz posiada daną kartę i jeśli tak to obsłguje request
                                        {
                                            if (!NonFunctionalCards(body.Card)) 
                                            {
                                                ServiceFunctionalCard(body); //obsługa kart funkcyjnych
                                            }
                                            NextMove();
                                        }
                                    }
                                    else
                                    {
                                        Response.SendResponse(users[index].Socket, Response.CreateResponse(request.Action, "WRONG", data: body.Card,message: "Nie możesz zagrać tej karty."));
                                    }
                                    break;
                            }
                        }
                        break;
                    }
                }  
            }
        }
        private void ServiceFunctionalCard(BodyPlayCard body)
        {
            switch (body.Card.Split(" ")[0])
            {
                case "2":
                    users[NextPlayer()].ForcMovement = new ForcMovementTake() { Number = 2 };
                    SendMessqgeToPlayer(users[NextPlayer()].Socket, "Musisz dobrać 2 karty.");
                    break;
                case "3":
                    users[NextPlayer()].ForcMovement = new ForcMovementTake() { Number = 3 };
                    SendMessqgeToPlayer(users[NextPlayer()].Socket, "Musisz dobrać 3 karty.");
                    break;
                case "4":
                    users[NextPlayer()].ForcMovement = new ForcMovementTake() { Number = 4 };
                    SendMessqgeToPlayer(users[NextPlayer()].Socket, "Musisz dobrać 4 karty.");
                    break;
                case "Walet":
                    for (int i = 0; i < users.Length; i++)
                    {
                        users[i].ForcMovement = new ForcMovementNumber() { Number = body.Task };
                        SendMessqgeToPlayer(users[i].Socket, "Kiedy będzie twoja kolej musisz zagrać karte o numerze: " + body.Task);
                    }
                    break;
                case "Dama":
                    // Mhm, czy coś tutaj pisać monkaS
                    break;
                case "Król":
                    users[NextPlayer()].ForcMovement = new ForcMovementTake() { Number = 5 };
                    SendMessqgeToPlayer(users[NextPlayer()].Socket, "Musisz dobrać 5 karty.");
                    break;
                case "As":
                    users[NextPlayer()].ForcMovement = new ForcMovementColor() { Color = body.Task };
                    SendMessqgeToPlayer(users[NextPlayer()].Socket, "Musisz zagrać karte o kolorze: " + body.Task);
                    break;
            }
        }
        private void SendMessqgeToPlayer(Socket socket, string message)
        {
            Response.SendResponse(socket,Response.CreateResponse("State", message: message));
        }
        private bool PlayerTakeCard (int index, Request request)
        {
            if (CardsToBeDealt.Count == 0)
            {
                TakeCardsToCardsToBeDealt();
            }
            if (CardsToBeDealt.Count > 0)
            {
                users[index].Cards.Add(CardsToBeDealt.First());
                Response.SendResponse(users[index].Socket, Response.CreateResponse(request.Action, "OK", data: CardsToBeDealt.First()));
                SendGameStatusToPlayers(Response.CreateResponse("State", message: "Gracz " + users[index].Nick + " pobrał kartę."));
                CardsToBeDealt.RemoveAt(0);
                NextMove();
                return true;
            }
            else
            {
                Response.SendResponse(users[index].Socket, Response.CreateResponse(request.Action, "WRONG", message: "Brak wolnych kart do pobrania."));
                return false;
            }
        }
        private bool PlayerPlayCard(int index, Request request)
        {
            BodyPlayCard body = request.Data as BodyPlayCard;
            if (users[index].Cards.Remove(body.Card))
            {
                Response.SendResponse(users[index].Socket, Response.CreateResponse(request.Action, "OK",data: body.Card));
                SendGameStatusToPlayers(Response.CreateResponse("State", message: "Gracz " + users[index].Nick + " zagrał kartę " + body.Card));
                CardsPlayed.Add(body.Card);
                return true;
            }
            else
            {
                Response.SendResponse(users[index].Socket, Response.CreateResponse(request.Action, "ERROR", message: "Błąd w rozgrywce. Serwer nie widzi tej karty w twojej ręce."));
                return false;
            }
        }
        private void TakeCardsToCardsToBeDealt()
        {
            Random random = new Random();
            while (true)
            {
                if (CardsPlayed.Count >= 2)
                {
                    int index = random.Next(0, CardsToBeDealt.Count - 1);
                    CardsToBeDealt.Insert(index, CardsPlayed.First());
                    CardsPlayed.RemoveAt(0);
                }
                else
                    break;
            }
        }
        private bool CardToCard(string card)
        {
            if (CardsPlayed.Last().Split(" ")[0] == "Dama" || card.Split(" ")[0] == "Dama")
                return true;
            else  if (CardsPlayed.Last().Split(" ")[0] == card.Split(" ")[0])
            {
                return true;
            }
            else if (CardsPlayed.Last().Split(" ")[1] == card.Split(" ")[1])
                return true;
            else
                return false;
        }
        private bool NonFunctionalCards(string card)
        {
            bool non = false;
            foreach(string s in nonFunctionalCards)
            {
                if (card == s)
                {
                    non = true;
                    break;
                }else if (s == card.Split(" ")[0])
                {
                    non = true;
                    break;
                }
            }
            return non;
        }
        private int NextPlayer()
        {
            if (playermovment == 2)
            {
                return 0;
            }
            else return playermovment + 1;
        }
        private void NextMove()
        {
            playermovment = NextPlayer();
            Response.SendResponse(users[playermovment].Socket,
                Response.CreateResponse("State", message: "Twój ruch."));
        }
        private void DealTheCarts()
        {
         foreach(User user in users)
            {
                user.Cards = new List<string>();
                for (int i = 0; i < 5; i++)
                {
                    user.Cards.Add(CardsToBeDealt.First());
                    CardsToBeDealt.RemoveAt(0);
                }
                SendCart(user);
            }   
        }
        private void SendCart(User user)
        {
            string cards = user.Cards[0];
            for (int i = 1; i < user.Cards.Count; i++)
            {
                cards += ";" + user.Cards[i];
            }
            Response.SendResponse(user.Socket, Response.CreateResponse("Take", "OK", cards));
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
                CardsToBeDealt.Add(c[numberCart]);
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
                        Thread thread = new Thread(new ThreadStart(this.StartGame));
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
                    Monitor.Enter(MakaoServerProtocol.croupiers);
                    MakaoServerProtocol.croupiers.Remove(this);
                    Monitor.Exit(MakaoServerProtocol.croupiers);
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
