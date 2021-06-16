
namespace ClientMakao
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelLoginAndRegister = new System.Windows.Forms.Panel();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.Hasło = new System.Windows.Forms.Label();
            this.textNick = new System.Windows.Forms.TextBox();
            this.Nick = new System.Windows.Forms.Label();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxNameTable = new System.Windows.Forms.TextBox();
            this.buttonJoinTable = new System.Windows.Forms.Button();
            this.listBoxTable = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonLogOut = new System.Windows.Forms.Button();
            this.buttonGetListTable = new System.Windows.Forms.Button();
            this.buttonCreateTable = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelGame = new System.Windows.Forms.Panel();
            this.richTextMessages = new System.Windows.Forms.RichTextBox();
            this.buttonLeaveTable = new System.Windows.Forms.Button();
            this.buttonPlayCart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxCart = new System.Windows.Forms.ListBox();
            this.buttonTakeCart = new System.Windows.Forms.Button();
            this.ServerInfo = new System.Windows.Forms.TextBox();
            this.backgroundWorkerThreadReceiveResponse = new System.ComponentModel.BackgroundWorker();
            this.labelColor = new System.Windows.Forms.Label();
            this.listBoxColor = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listBoxCardOnWalet = new System.Windows.Forms.ListBox();
            this.panelLoginAndRegister.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.panelGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLoginAndRegister
            // 
            this.panelLoginAndRegister.Controls.Add(this.buttonLogin);
            this.panelLoginAndRegister.Controls.Add(this.buttonRegister);
            this.panelLoginAndRegister.Controls.Add(this.textPassword);
            this.panelLoginAndRegister.Controls.Add(this.Hasło);
            this.panelLoginAndRegister.Controls.Add(this.textNick);
            this.panelLoginAndRegister.Controls.Add(this.Nick);
            this.panelLoginAndRegister.Location = new System.Drawing.Point(12, 112);
            this.panelLoginAndRegister.Name = "panelLoginAndRegister";
            this.panelLoginAndRegister.Size = new System.Drawing.Size(136, 284);
            this.panelLoginAndRegister.TabIndex = 0;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(10, 107);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(100, 23);
            this.buttonLogin.TabIndex = 5;
            this.buttonLogin.Text = "Zaloguj się";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonRegister
            // 
            this.buttonRegister.Location = new System.Drawing.Point(10, 136);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(100, 23);
            this.buttonRegister.TabIndex = 4;
            this.buttonRegister.Text = "Zarejestruj się";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(10, 77);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(100, 23);
            this.textPassword.TabIndex = 3;
            // 
            // Hasło
            // 
            this.Hasło.AutoSize = true;
            this.Hasło.Location = new System.Drawing.Point(10, 58);
            this.Hasło.Name = "Hasło";
            this.Hasło.Size = new System.Drawing.Size(37, 15);
            this.Hasło.TabIndex = 2;
            this.Hasło.Text = "Hasło";
            // 
            // textNick
            // 
            this.textNick.Location = new System.Drawing.Point(10, 28);
            this.textNick.Name = "textNick";
            this.textNick.Size = new System.Drawing.Size(100, 23);
            this.textNick.TabIndex = 1;
            // 
            // Nick
            // 
            this.Nick.AutoSize = true;
            this.Nick.Location = new System.Drawing.Point(10, 10);
            this.Nick.Name = "Nick";
            this.Nick.Size = new System.Drawing.Size(31, 15);
            this.Nick.TabIndex = 0;
            this.Nick.Text = "Nick";
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.label4);
            this.panelMenu.Controls.Add(this.textBoxNameTable);
            this.panelMenu.Controls.Add(this.buttonJoinTable);
            this.panelMenu.Controls.Add(this.listBoxTable);
            this.panelMenu.Controls.Add(this.label2);
            this.panelMenu.Controls.Add(this.buttonLogOut);
            this.panelMenu.Controls.Add(this.buttonGetListTable);
            this.panelMenu.Controls.Add(this.buttonCreateTable);
            this.panelMenu.Controls.Add(this.label1);
            this.panelMenu.Location = new System.Drawing.Point(13, 112);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(135, 317);
            this.panelMenu.TabIndex = 6;
            this.panelMenu.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Nazwa stołu";
            // 
            // textBoxNameTable
            // 
            this.textBoxNameTable.Location = new System.Drawing.Point(9, 72);
            this.textBoxNameTable.MaxLength = 15;
            this.textBoxNameTable.Name = "textBoxNameTable";
            this.textBoxNameTable.Size = new System.Drawing.Size(100, 23);
            this.textBoxNameTable.TabIndex = 6;
            // 
            // buttonJoinTable
            // 
            this.buttonJoinTable.Location = new System.Drawing.Point(10, 253);
            this.buttonJoinTable.Name = "buttonJoinTable";
            this.buttonJoinTable.Size = new System.Drawing.Size(100, 23);
            this.buttonJoinTable.TabIndex = 5;
            this.buttonJoinTable.Text = "Dołącz do stołu";
            this.buttonJoinTable.UseVisualStyleBackColor = true;
            this.buttonJoinTable.Click += new System.EventHandler(this.buttonJoinTable_Click);
            // 
            // listBoxTable
            // 
            this.listBoxTable.FormattingEnabled = true;
            this.listBoxTable.ItemHeight = 15;
            this.listBoxTable.Location = new System.Drawing.Point(10, 167);
            this.listBoxTable.Name = "listBoxTable";
            this.listBoxTable.Size = new System.Drawing.Size(100, 79);
            this.listBoxTable.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Wolne stoły:";
            // 
            // buttonLogOut
            // 
            this.buttonLogOut.Location = new System.Drawing.Point(10, 282);
            this.buttonLogOut.Name = "buttonLogOut";
            this.buttonLogOut.Size = new System.Drawing.Size(100, 23);
            this.buttonLogOut.TabIndex = 1;
            this.buttonLogOut.Text = "Wyloguj się ";
            this.buttonLogOut.UseVisualStyleBackColor = true;
            this.buttonLogOut.Click += new System.EventHandler(this.buttonLogOut_Click);
            // 
            // buttonGetListTable
            // 
            this.buttonGetListTable.Location = new System.Drawing.Point(9, 101);
            this.buttonGetListTable.Name = "buttonGetListTable";
            this.buttonGetListTable.Size = new System.Drawing.Size(100, 41);
            this.buttonGetListTable.TabIndex = 2;
            this.buttonGetListTable.Text = "Pobierz listę stołów";
            this.buttonGetListTable.UseVisualStyleBackColor = true;
            this.buttonGetListTable.Click += new System.EventHandler(this.buttonGetListTable_Click);
            // 
            // buttonCreateTable
            // 
            this.buttonCreateTable.Location = new System.Drawing.Point(10, 29);
            this.buttonCreateTable.Name = "buttonCreateTable";
            this.buttonCreateTable.Size = new System.Drawing.Size(100, 23);
            this.buttonCreateTable.TabIndex = 1;
            this.buttonCreateTable.Text = "Utwórz stół";
            this.buttonCreateTable.UseVisualStyleBackColor = true;
            this.buttonCreateTable.Click += new System.EventHandler(this.buttonCreateTable_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Menu";
            // 
            // panelGame
            // 
            this.panelGame.Controls.Add(this.listBoxCardOnWalet);
            this.panelGame.Controls.Add(this.label5);
            this.panelGame.Controls.Add(this.listBoxColor);
            this.panelGame.Controls.Add(this.labelColor);
            this.panelGame.Controls.Add(this.richTextMessages);
            this.panelGame.Controls.Add(this.buttonLeaveTable);
            this.panelGame.Controls.Add(this.buttonPlayCart);
            this.panelGame.Controls.Add(this.label3);
            this.panelGame.Controls.Add(this.listBoxCart);
            this.panelGame.Controls.Add(this.buttonTakeCart);
            this.panelGame.Location = new System.Drawing.Point(154, 12);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(608, 396);
            this.panelGame.TabIndex = 2;
            this.panelGame.Visible = false;
            // 
            // richTextMessages
            // 
            this.richTextMessages.BackColor = System.Drawing.Color.White;
            this.richTextMessages.Cursor = System.Windows.Forms.Cursors.Default;
            this.richTextMessages.Location = new System.Drawing.Point(10, 9);
            this.richTextMessages.Name = "richTextMessages";
            this.richTextMessages.ReadOnly = true;
            this.richTextMessages.Size = new System.Drawing.Size(449, 337);
            this.richTextMessages.TabIndex = 6;
            this.richTextMessages.TabStop = false;
            this.richTextMessages.Text = "";
            // 
            // buttonLeaveTable
            // 
            this.buttonLeaveTable.Location = new System.Drawing.Point(465, 188);
            this.buttonLeaveTable.Name = "buttonLeaveTable";
            this.buttonLeaveTable.Size = new System.Drawing.Size(130, 24);
            this.buttonLeaveTable.TabIndex = 5;
            this.buttonLeaveTable.Text = "Odejdź od stolika";
            this.buttonLeaveTable.UseVisualStyleBackColor = true;
            this.buttonLeaveTable.Click += new System.EventHandler(this.buttonLeaveTable_Click);
            // 
            // buttonPlayCart
            // 
            this.buttonPlayCart.Location = new System.Drawing.Point(465, 128);
            this.buttonPlayCart.Name = "buttonPlayCart";
            this.buttonPlayCart.Size = new System.Drawing.Size(130, 23);
            this.buttonPlayCart.TabIndex = 4;
            this.buttonPlayCart.Text = "Zagraj kartę";
            this.buttonPlayCart.UseVisualStyleBackColor = true;
            this.buttonPlayCart.Click += new System.EventHandler(this.buttonPlayCart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(465, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Twoje Karty";
            // 
            // listBoxCart
            // 
            this.listBoxCart.FormattingEnabled = true;
            this.listBoxCart.ItemHeight = 15;
            this.listBoxCart.Location = new System.Drawing.Point(465, 31);
            this.listBoxCart.Name = "listBoxCart";
            this.listBoxCart.Size = new System.Drawing.Size(130, 94);
            this.listBoxCart.TabIndex = 2;
            // 
            // buttonTakeCart
            // 
            this.buttonTakeCart.Location = new System.Drawing.Point(465, 159);
            this.buttonTakeCart.Name = "buttonTakeCart";
            this.buttonTakeCart.Size = new System.Drawing.Size(130, 23);
            this.buttonTakeCart.TabIndex = 1;
            this.buttonTakeCart.Text = "Dobierz kartę";
            this.buttonTakeCart.UseVisualStyleBackColor = true;
            this.buttonTakeCart.Click += new System.EventHandler(this.buttonTakeCart_Click);
            // 
            // ServerInfo
            // 
            this.ServerInfo.Location = new System.Drawing.Point(13, 12);
            this.ServerInfo.Multiline = true;
            this.ServerInfo.Name = "ServerInfo";
            this.ServerInfo.ReadOnly = true;
            this.ServerInfo.Size = new System.Drawing.Size(135, 94);
            this.ServerInfo.TabIndex = 7;
            // 
            // backgroundWorkerThreadReceiveResponse
            // 
            this.backgroundWorkerThreadReceiveResponse.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerReceiveResponse_DoWork);
            // 
            // labelColor
            // 
            this.labelColor.AutoSize = true;
            this.labelColor.Location = new System.Drawing.Point(466, 219);
            this.labelColor.Name = "labelColor";
            this.labelColor.Size = new System.Drawing.Size(77, 15);
            this.labelColor.TabIndex = 7;
            this.labelColor.Text = "Kolor po Asie";
            // 
            // listBoxColor
            // 
            this.listBoxColor.FormattingEnabled = true;
            this.listBoxColor.ItemHeight = 15;
            this.listBoxColor.Items.AddRange(new object[] {
            "Trefl",
            "Karo",
            "Kier",
            "Pik"});
            this.listBoxColor.Location = new System.Drawing.Point(466, 238);
            this.listBoxColor.Name = "listBoxColor";
            this.listBoxColor.Size = new System.Drawing.Size(120, 49);
            this.listBoxColor.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(466, 294);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Jaka karta po Walecie";
            // 
            // listBoxCardOnWalet
            // 
            this.listBoxCardOnWalet.FormattingEnabled = true;
            this.listBoxCardOnWalet.ItemHeight = 15;
            this.listBoxCardOnWalet.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.listBoxCardOnWalet.Location = new System.Drawing.Point(465, 312);
            this.listBoxCardOnWalet.Name = "listBoxCardOnWalet";
            this.listBoxCardOnWalet.Size = new System.Drawing.Size(120, 79);
            this.listBoxCardOnWalet.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 441);
            this.Controls.Add(this.ServerInfo);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelGame);
            this.Controls.Add(this.panelLoginAndRegister);
            this.MaximumSize = new System.Drawing.Size(800, 480);
            this.MinimumSize = new System.Drawing.Size(800, 480);
            this.Name = "Form1";
            this.Text = "Makao";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing_1);
            this.panelLoginAndRegister.ResumeLayout(false);
            this.panelLoginAndRegister.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.panelGame.ResumeLayout(false);
            this.panelGame.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelLoginAndRegister;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label Hasło;
        private System.Windows.Forms.TextBox textNick;
        private System.Windows.Forms.Label Nick;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button buttonJoinTable;
        private System.Windows.Forms.ListBox listBoxTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonGetListTable;
        private System.Windows.Forms.Button buttonCreateTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLogOut;
        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.Button buttonLeaveTable;
        private System.Windows.Forms.Button buttonPlayCart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxCart;
        private System.Windows.Forms.Button buttonTakeCart;
        private System.Windows.Forms.RichTextBox richTextMessages;
        private System.Windows.Forms.TextBox ServerInfo;
        private System.ComponentModel.BackgroundWorker backgroundWorkerMainThread;
        private System.ComponentModel.BackgroundWorker backgroundWorkerThreadReceiveResponse;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxNameTable;
        private System.Windows.Forms.ListBox listBoxCardOnWalet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBoxColor;
        private System.Windows.Forms.Label labelColor;
    }
}

