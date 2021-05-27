using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Server.Models
{
    class Table
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public int UserID1 { get; set; }
        public int UserID2 { get; set; }
        public int UserID3 { get; set; }
        public string CardsToDownload { get; set; }
        public string CardsPlayed { get; set; }

    }
}
