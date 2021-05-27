﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Server.Models
{
    class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int IdTable { get; set; }
        public string Cards { get; set; }
        public int Points { get; set; }
    }
}