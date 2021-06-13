using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Server.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        //public int IdTable { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        [Ignore]
        public List<string> Cards { get; set; }
        [Ignore]
        public int Points { get; set; }
        [Ignore]
        public Socket Socket { get; set; }
    }
}
