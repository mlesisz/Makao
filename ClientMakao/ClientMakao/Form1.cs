using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        }
        private void backgroundWorkerThreadReceiveResponse_DoWork(object sender, DoWorkEventArgs e)
        {
            Response response;
            while (!backgroundWorkerMainThread.CancellationPending)
            {
                response = new Response(SocketMenagment.ReceiveResponse());
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
                        break;
                    case "List":
                        break;
                    case "Join":
                        break;
                    case "Leave":
                        break;
                    case "Take":
                        break;
                    case "Play":
                        break;
                    case "Status":
                    default:
                        SetTextServerInfo("Serwer wysłał odpowiedź której nie znam.");
                        break;
                }

            }
        }
        private void LogoutStatus(Response response)
        {
            switch (response.Status)
            {
                case "OK":
                    buttonLogOut.Visible = false;
                    panelMenu.Visible = false;
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
                    panelMenu.Visible = true;
                    buttonLogOut.Visible = true;
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
        delegate void SetTextServerInfoCallBack(string text);
        private void SetTextServerInfo(string text)
        {
            if (ServerInfo.InvokeRequired)
            {
                SetTextServerInfoCallBack st = new SetTextServerInfoCallBack(SetTextServerInfo);
                this.Invoke(st, new object[] { text });
            }
            else
                this.ServerInfo.Text = text;
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
            SocketMenagment.SendRequest(Request.Logout());
        }

        private void buttonCreateTable_Click(object sender, EventArgs e)
        {
            SocketMenagment.SendRequest(Request.CreateTable());
        }

        private void buttonGetListTable_Click(object sender, EventArgs e)
        {
            SocketMenagment.SendRequest(Request.ListTable());
        }

        private void buttonTakeCart_Click(object sender, EventArgs e)
        {
            SocketMenagment.SendRequest(Request.TakeCart());
        }

        private void buttonLeaveTable_Click(object sender, EventArgs e)
        {
            SocketMenagment.SendRequest(Request.LeaveTable());
        }

        private void buttonJoinTable_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(listBoxTable.ValueMember))
            {
                SocketMenagment.SendRequest(Request.JoinTable(int.Parse(listBoxTable.ValueMember)));
            }
            
        }

        private void buttonPlayCart_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(listBoxCart.ValueMember))
            {
                SocketMenagment.SendRequest(Request.PlayCard(listBoxCart.ValueMember));
            }
        }

        
    }
}
