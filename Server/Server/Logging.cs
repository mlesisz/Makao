using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Server
{
    public static class Logging
    {
        private static readonly string pathFile = Path.Combine(Environment.CurrentDirectory,"logs.txt");

        private static void Write(string message)
        {
            string log = DateTime.Now.ToString()+" | ";
            log += message;
            Monitor.Enter(pathFile);
            if (File.Exists(pathFile))
            {
                using StreamWriter file = new(pathFile, append: true);
                file.WriteLine(log);
                file.Close();
            }
            else
            {
                File.WriteAllText(pathFile,log+"\n");
            }
            Monitor.Exit(pathFile);
            Console.WriteLine(log);
        }
        public static void Info( string message = "")
        {
            Write(message);
        }
        public static void OK( string action, string message = "", string host = "")
        {
            string ms = ""+host+" "+action+": OK - "+ message;
            Write(ms);
        }
        public static void WRONG( string action, string message = "", string host = "")
        {
            string ms = "" + host + " " + action + ": WRONG - " + message;
            Write(ms);
        }
        public static void ERROR(string action ="", string message = "", string host ="")
        {
            string ms = "" + host + " " + action + ": ERROR - " + message;
            Write(ms);
        }
    }

}
