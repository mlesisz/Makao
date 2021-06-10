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
        private static readonly string pathFile = Path.Combine(Environment.CurrentDirectory,"logs.txt");

        private static void WriteInformationToLogs(DataLog dataLog)
        {
            string log = DateTime.Now.ToString()+" | ";
            log += " Host: "+ dataLog.Host+ 
                " Port: "+ dataLog.Port+
                " Message: "+dataLog.Message+
                " Action: "+dataLog.Action;
            if (File.Exists(pathFile))
            {
                using StreamWriter file = new(pathFile, append: true);
                file.WriteLine(log);
            }
            else
            {
                File.WriteAllText(pathFile,log+"\n");
            }
            Console.WriteLine(log);
        }
        public static void Info( DataLog dataLog)
        {
            WriteInformationToLogs(dataLog);
        }
        public static void Error(DataLog dataLog)
        {
            dataLog.Message = "Error | " + dataLog.Message;
            WriteInformationToLogs(dataLog);
        }
    }

    public class DataLog
    {
        public string Host  { get; set; }
        public int Port { get; set; }
        public string Message { get; set; }
        public string Action { get; set; }
    }
}
