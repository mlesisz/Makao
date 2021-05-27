using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server
{
    public static class Logs
    {
        private static string fileName = @"D:\Studia\Programowanie aplikacji sieciowych\Makao\Server\Server\logs.txt";

        private static void WriteInformationToLogs(DataLog dataLog)
        {
            string log = DateTime.Now.ToString()+" | ";
            log += " Host:"+dataLog.host+ 
                " Port:"+ dataLog.port+
                " Message: "+dataLog.message+
                " Action: "+dataLog.action;
            if (File.Exists(fileName))
            {
                using StreamWriter file = new(fileName, append: true);
                file.WriteLine(log);
            }
            else
            {
                File.WriteAllText(fileName,log+"\n");
            }
            Console.WriteLine(log);
        }
        public static void Info( DataLog dataLog)
        {
            WriteInformationToLogs(dataLog);
        }
        public static void Error(string message)
        {

        }
    }

    public struct DataLog
    {
        public string host;
        public int port;
        public string message;
        public string action;
    }
}
