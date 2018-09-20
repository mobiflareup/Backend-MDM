using MobiOcean.MDM.Web.Log.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MobiOcean.MDM.Web.Log.Repository
{
    public class Log : ILog
    {
        private Log()
        {
        }
        private static readonly Lazy<Log> instance = new Lazy<Log>(() => new Log());

        public static Log GetInstance
        {
            get
            {
                return instance.Value;
            }
        }

        public void LogData(string message)
        {
            string fileName = "Log\\LogFiles\\" + string.Format("{0}_{1}.log", "Exception", DateTime.Now.ToString("dd_MM_yyyy"));
            WriteException(message, fileName);
        }
        public void LogAPIData(string message)
        {
            string fileName = "Log\\LogFiles\\" + string.Format("{0}_{1}_API.log", "Exception", DateTime.Now.ToString("dd_MM_yyyy"));
            WriteException(message, fileName);
        }

        private static void WriteException(string message, string fileName)
        {
            string logFilePath = string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, fileName);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----------------------------------------");
            sb.AppendLine(DateTime.Now.ToString());
            sb.AppendLine(message);
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.Write(sb.ToString());
                writer.Flush();
            }
        }
    }
}