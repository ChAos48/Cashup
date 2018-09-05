using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Application_Updater
{
    internal static class Deloitte
    {
        
        internal static void WriteErrorLog(DingoException ex)
        {
            if (!Directory.Exists("./Logs"))
            { Directory.CreateDirectory("./Logs"); }
            FileStream fs = new FileStream("./Logs/Error " + DateTime.Now.ToString("dd MMM yyyy HHmm") + ".log", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            XmlSerializer bf = new XmlSerializer(ex.GetType());
            bf.Serialize(fs, ex);
            fs.Close();
        }

        internal static void WriteErrorLog(string Error)
        {
            if (!Directory.Exists("./Logs"))
            { Directory.CreateDirectory("./Logs"); }
            FileStream fs = new FileStream("./Logs/Error " + DateTime.Now.ToString("dd MMM yyyy HHmm") + ".log", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            byte[] buffer = Encoding.Default.GetBytes(Error);
            fs.Write(buffer, 0, buffer.Length);
            fs.Close();
        }
    }
    /// <summary>
    /// Exceptions handled by dingle
    /// </summary>
    [Serializable]
    public class DingoException
    {
        /// <summary>
        /// Error message the exception returns
        /// </summary>
        public string ErrorMessage;
        /// <summary>
        /// The stack trace of events occurred as a string value
        /// </summary>
        public string StackTrace;
        /// <summary>
        /// Inner exceptions returned
        /// </summary>
        public string[] InnerExceptions;

        /// <summary>
        /// Initialize dingo with a standard .net exception
        /// </summary>
        public DingoException() : this(new Exception()) { }

        /// <summary>
        /// Initialize with an instance of an exception
        /// </summary>
        /// <param name="ex"></param>
        public DingoException(Exception ex)
        {
            ErrorMessage = ex.Message == null ? "" : ex.Message;
            StackTrace = ex.StackTrace == null ? "" : ex.StackTrace;
            InnerExceptions = new string[3] { "", "", "" };
            var inner1 = ex.InnerException;
            if (inner1 != null)
            {
                InnerExceptions[0] = inner1.Message;

                var inner2 = inner1.InnerException;
                if (inner2 != null)
                {
                    InnerExceptions[1] = inner2.Message;

                    var inner3 = inner2.InnerException;
                    if (inner3 != null)
                    {
                        InnerExceptions[2] = inner3.Message;
                    }
                }
            }
        }
    }
}
