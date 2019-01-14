using System;
//using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Data.Odbc;

namespace Hounds
{
    internal static class Deloitte
    {
        /// <summary>
        /// Adds Auditing to the database
        /// </summary>
        /// <param name="Action">Action Performed by the User</param>
        /// <param name="UserID">Unique Identifier of the User who Performed the Action</param>
        /// <param name="UserName">Name of the User who Performed the action</param>
        /// <param name="Method">Where in the Code did the action take place</param>
        internal static void WriteAudit(string Action, string UserID, string UserName, string Method)
        {
            var Con = new OdbcConnection("Driver=MySQL ODBC 5.1 Driver;provider=ODBC;server=25.46.14.119;port=3306;option=67108864;database=protea_pit_passwords;uid=proteasql;pwd=ymkPf5Ns;");
            try { Con.Open(); }
            catch (OdbcException ex)
            {
                Deloitte.WriteErrorLog(new DingoException(ex));
                return;
            }
            try
            {
                string query = "INSERT INTO hound_audit ('Time', 'User', 'UserName', 'Action', 'Where') VALUES (NOW(), ?id, ?name, ?action, ?method)";

                var cmd = new OdbcCommand(query, Con);
                cmd.Parameters.AddWithValue("?id", UserID);
                cmd.Parameters.AddWithValue("?name", UserName);
                cmd.Parameters.AddWithValue("?action", Action);
                cmd.Parameters.AddWithValue("?method", Method);

                cmd.ExecuteNonQuery();
                
            } 
                catch(OdbcException ex)
            {
                Deloitte.WriteErrorLog(new DingoException(ex));
            }
            finally
            {
                if(Con.State != System.Data.ConnectionState.Closed)
                { Con.Close(); }
            }
        }

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
