using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientMakao
{
    public partial class Form1 : Form
    {
        public SocketMenagment SocketMenagment { get; set; }
        public string Token { get; set; }
        public string Nickname { get; set; }
        public Form1()
        {
            InitializeComponent();
            SocketMenagment = new SocketMenagment("::1", 4000);
            ServerInfo.Text = SocketMenagment.StartConnect();
            backgroundWorkerThreadReceiveResponse.RunWorkerAsync();
        }
        private void backgroundWorkerReceiveResponse_DoWork(object sender, DoWorkEventArgs e)
        {
            Response response;
            string string_response;
            while (!backgroundWorkerThreadReceiveResponse.CancellationPending)
            {
                string_response = SocketMenagment.ReceiveResponse();
                if (string_response == null)
                    break;
                response = new Response(string_response);
                switch (response.Action)
                {
                    case "Register":
                        RegisterStatus(response);
                        break;
                    case "Login":
                        LoginStatus(response);
                        break;
                    case "Logout":
                        LogoutStatus(response);
                        break;
                    case "Create":
                        CrateStatus(response);
                        break;
                    case "List":
                        ListStatus(response);
                        break;
                    case "Join":
                        JoinStatus(response);
                        break;
                    case "Leave":
                        LeaveStatus(response);
                        break;
                    case "Take":
                        break;
                    case "Play":
                        break;
                    case "State":
                        StateStatus(response);
                        break;
                    default:
                        SetTextServerInfo("Serwer wysłał odpowiedź której nie znam.");
                        break;
                }

            }
        }
        private void StateStatus(Response response)
        {
            SetTextRich(response.Message);
            if(response.Message == "Odbieranie kart od graczy.")
            {
                SetListBoxCarts("",true);
            }else if(response.Message == "Twój ruch.")
            {
                ChangeEnabledButtonTakeCart();
                ChangeEnabledButtonPlayCard();
            }
        }
        private void JoinStatus(Response response)
        {
            switch (response.Status)
            {
                case "OK":
                    SetTextServerInfo(response.Message);
                    ChangeVisiblePanelGame();
                    ChangeVisiblePanelMenu();
                    break;
                case "ERROR":
                    SetTextServerInfo(response.Message);
                    break;
                case "WRONG":
                    SetTextServerInfo(response.Message);
                    break;
            }
        }
        private void LeaveStatus(Response response)
        {
            switch (response.Status)
            {
                case "OK":
                    SetTextServerInfo("Odszedłeś od stolika.");
                    ChangeVisiblePanelGame();
                    ChangeVisiblePanelMenu();
                    break;
                case "ERROR":
                    SetTextServerInfo(response.Message);
                    break;
                case "WRONG":
                    SetTextServerInfo(response.Message);
                    break;
            }
        }
        private void ListStatus(Response response)
        {
            switch (response.Status)
            {
                case "OK":
                    SetListBoxTable((string)response.Data);
                    break;
                case "ERROR":
                    SetTextServerInfo(response.Message);
                    break;
                case "WRONG":
                    SetTextServerInfo(response.Message);
                    SetListBoxTable("");
                    break;
            }
        }
        private void CrateStatus(Response response)
        {
            switch (response.Status)
            {
                case "OK":
                    ChangeVisiblePanelMenu();
                    ChangeVisiblePanelGame();
                    SetTextServerInfo((string)response.Message);
                    break;
                case "ERROR":
                    SetTextServerInfo(response.Message);
                    break;
                case "WRONG":
                    SetTextServerInfo(response.Message);
                    break;
            }
        }
        private void LogoutStatus(Response response)
        {
            switch (response.Status)
            {
                case "OK":
                    ChangeVisiblepanelLoginAndRegister();
                    ChangeVisiblePanelMenu();
                    SetTextServerInfo("Wylogowanie udało się.");
                    break;
                case "ERROR":
                    SetTextServerInfo(response.Message);
                    break;
                case "WRONG":
                    SetTextServerInfo("Wylogowanie nie udane.");
                    break;
            }

        }
        private void LoginStatus (Response response)
        {
            switch (response.Status)
            {
                case "OK":
                    Token = (string)response.Data;
                    SetTextServerInfo("Logowanie udane.");
                    ChangeVisiblepanelLoginAndRegister();
                    ChangeVisiblePanelMenu();
                    break;
                case "ERROR":
                    SetTextServerInfo(response.Message);
                    break;
                case "WRONG":
                    SetTextServerInfo(response.Message);
                    break;
            }
        }
        private void RegisterStatus(Response response)
        {
            switch (response.Status)
            {
                case "OK":
                    SetTextServerInfo("Rejestracja zakończona sukcesem. Zaloguj się aby zagrać.");
                    break;
                case "ERROR":
                    SetTextServerInfo(response.Message);
                    break;
                case "WRONG":
                    SetTextServerInfo(response.Message);
                    break;
                default:
                    SetTextServerInfo("Status odpowiedzi nieznany");
                    break;
            }
        }

        private void ChangeEnabledButtonTakeCart()
        {
            Invoke(new Action(() => { buttonTakeCart.Enabled = !buttonTakeCart.Enabled; }));
        }

        private void ChangeEnabledButtonPlayCard()
        {
            Invoke(new Action(() => { buttonPlayCart.Enabled = !buttonPlayCart.Enabled; }));
        }

        delegate void SetTextRichCallBack(string text);
        private void SetTextRich(string text)
        {
            if (ServerInfo.InvokeRequired)
            {
                SetTextRichCallBack st = new SetTextRichCallBack(SetTextRich);
                Invoke(st, new object[] { text });
            }
            else
                richTextMessages.Text += text + "\n"; 
        }

        delegate void SetListBoxCartsCallBack(string cart, bool remove);
        private void SetListBoxCarts(string cart, bool remove)
        {
            if (listBoxCart.InvokeRequired)
            {
                SetListBoxCartsCallBack sl = new SetListBoxCartsCallBack(SetListBoxCarts);
                Invoke(sl, new object[] { cart,remove });
            }
            else
            {
                if (cart == "")
                    listBoxCart.Items.Clear();
                else if (remove)
                    listBoxCart.Items.Remove(cart);
                else
                    listBoxCart.Items.Add(cart);
            }
        }

        delegate void SetListBoxTableCallBack(string listTable);
        private void SetListBoxTable(string listTable)
        {
            if (listBoxTable.InvokeRequired)
            {
                SetListBoxTableCallBack sl = new SetListBoxTableCallBack(SetListBoxTable);
                Invoke(sl, new object []{listTable });
            }
            else
            {
                string[] list = listTable.Split(";");
                listBoxTable.Items.Clear();
                foreach(string item in list)
                {
                    listBoxTable.Items.Add(item);
                }
            }
        }

        private void ChangeVisiblePanelGame()
        {
            Invoke(new Action(() => { panelGame.Visible = !panelGame.Visible; }));
        }

        private void ChangeVisiblepanelLoginAndRegister()
        {
            Invoke(new Action(() => { panelLoginAndRegister.Visible = !panelLoginAndRegister.Visible; }));
        }

        private void ChangeVisiblePanelMenu()
        {
            Invoke(new Action(() => { panelMenu.Visible = !panelMenu.Visible; }));
        }

        delegate void SetTextServerInfoCallBack(string text);
        private void SetTextServerInfo(string text)
        {
            if (ServerInfo.InvokeRequired)
            {
                SetTextServerInfoCallBack st = new SetTextServerInfoCallBack(SetTextServerInfo);
                Invoke(st, new object[] { text });
            }
            else
                ServerInfo.Text = text;
        }
       
        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            SocketMenagment.SendRequest(Request.End());
            SocketMenagment.socket.Close();

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string nick = textNick.Text;
            string password = textPassword.Text;
            SocketMenagment.SendRequest(Request.Login(nick,password));
            textNick.Text = "";
            textPassword.Text = "";
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string nick = textNick.Text;
            string password = textPassword.Text;
            SocketMenagment.SendRequest(Request.Register(nick, password));
            textNick.Text = "";
            textPassword.Text = "";
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            SocketMenagment.SendRequest(Request.Logout(Token));
        }

        private void buttonCreateTable_Click(object sender, EventArgs e)
        {
            SocketMenagment.SendRequest(Request.CreateTable(textBoxNameTable.Text,Token));
        }

        private void buttonGetListTable_Click(object sender, EventArgs e)
        {
            SocketMenagment.SendRequest(Request.ListTable(Token));
        }

        private void buttonTakeCart_Click(object sender, EventArgs e)
        {
            SocketMenagment.SendRequest(Request.TakeCart(Token));
        }

        private void buttonLeaveTable_Click(object sender, EventArgs e)
        {
            SocketMenagment.SendRequest(Request.LeaveTable(Token));
        }

        private void buttonJoinTable_Click(object sender, EventArgs e)
        {
            if(listBoxTable.SelectedItem != null)
            {
                SocketMenagment.SendRequest(Request.JoinTable(listBoxTable.SelectedItem.ToString(),Token));
            }
            
        }

        private void buttonPlayCart_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(listBoxCart.ValueMember))
            {
                SocketMenagment.SendRequest(Request.PlayCard(listBoxCart.ValueMember, Token));
            }
        }

        
    }
}
