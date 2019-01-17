using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OOP_Cashup;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Xml;
using Hounds;
using System.Globalization;

namespace OOP_Cashup
{
    static class Program
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            log.Info("Started application");
            LoadSettings();
            #region old Log code(changes Console.write line to write to file)
            //if (!Directory.Exists(@"./Logs")) {
            //    Directory.CreateDirectory(@"./Logs");
            //    Console.WriteLine(DateTime.Now + ": Created Log dir.");
            //}

            //FileStream ostrm;
            //StreamWriter writer;
            //TextWriter oldOut = Console.Out;
            //try {
            //    string logFile = Environment.CurrentDirectory  + "\\Logs\\" + DateTime.Now.ToFileTime().ToString() + ".log";
            //    ostrm = new FileStream(logFile, FileMode.OpenOrCreate, FileAccess.Write);
            //    writer = new StreamWriter(ostrm);
            //} catch (Exception e) {
            //    Console.WriteLine(DateTime.Now +": Cannot open log file for writing");
            //    Console.WriteLine(e.Message);
            //    return;
            //}

            //Console.SetOut(writer);
            #endregion

#if DEBUG
            log.Debug("in debug mode skipping update");
            goto skipupdate;

#endif

            FileInfo tFile = new FileInfo(Environment.CurrentDirectory + "\\Cashup.exe");
            string online_hash = GetOnlineHash(tFile);
            string local_hash = GetHash();

            log.Debug("Online Hash = " + online_hash);
            log.Debug("Online Hash = " + local_hash);

            var temp = online_hash == local_hash;
            //#if DEBUG
            //            temp = true;
            //            goto skipupdate;
            //#endif
            if (temp) {

                log.Info("no update required");

            } else if (canPing()) {

                log.Info("Update Required");
                Process proc = new Process();
                proc.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location), "Cashup Updater.exe");
                proc.EnableRaisingEvents = true;

                proc.Start();
                Application.Exit();

            } else {
                log.Warn("cannot contact Protea.dedicated.co.za skipping update");
                goto skipupdate;
            }
            string hashAfterUpdate = GetHash();
            log.Debug("New Local Hash = " + hashAfterUpdate);
            goto skipupdate;

            skipupdate:
            Application.Run(new frmMain());
            goto Finish;

            Finish:
            log.Info("Done");

        }

        static string GetOnlineHash(FileInfo f) {
            var FileName = f.Name;
            var AppName = "CashUp";
            var Method = "getHash";
            string onlineuri = "http://www.proteaboekwinkel.com/Updates/index.php?applicationname={0}&filename={1}&method={2}&RelDir={3}";

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(string.Format(onlineuri, AppName, FileName, Method, "/"));
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            string ResponedValue = "";

            byte[] buffer = new byte[512];
            var stream = resp.GetResponseStream();
            var count = stream.Read(buffer, 0, buffer.Length);
            while (count > 0) {
                ResponedValue += System.Text.Encoding.Default.GetString(buffer, 0, count);

                count = stream.Read(buffer, 0, buffer.Length);
            }
            stream.Close();
            resp.Close();
            return ResponedValue;
        }

        private static string GetHash() {

            var filestr = Environment.CurrentDirectory + "\\Cashup.exe";

            if (!File.Exists(filestr))
                return "FileNotFound";
            FileStream f = File.Open(filestr, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            SHA512 sha = SHA512.Create();
            sha.ComputeHash(f);
            f.Close();

            byte[] hash = sha.Hash;

            string hashedPwd = string.Empty;

            for (int i = 0; i < hash.Length; i++) {
                hashedPwd += hash[i].ToString("x2");
            }


            return hashedPwd;
        }

        private static bool canPing() {

            try {

                Ping myping = new Ping();
                string host = "protea.dedicated.co.za";
                byte[] buffer = new byte[32];
                int timeout = 2000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myping.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            } catch (Exception) {
                return false;
            }
        }

        static void LoadSettings() {
            
            if (File.Exists("./Settings.cfg")) {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("./Settings.cfg");
                XmlNodeList LocalSettings = xDoc.GetElementsByTagName("settings");

                var server = Encryption.Decrypt(LocalSettings[0].ChildNodes[0].InnerText);
                var Username = Encryption.Decrypt(LocalSettings[0].ChildNodes[1].InnerText);
                var Password = Encryption.Decrypt(LocalSettings[0].ChildNodes[2].InnerText);
                var dbName = Encryption.Decrypt(LocalSettings[0].ChildNodes[3].InnerText);
                var _driver = LocalSettings[0].ChildNodes[4].InnerText;

                var DriverProvider = String.Format("Driver={0};provider=ODBC", _driver);
                string ConString = string.Format(CultureInfo.InvariantCulture, "{4};server={0};port=3306;option=67108864;database={3};uid={1};pwd={2};", server, Username, Password, dbName, DriverProvider);
                RuntimeSettings.conString = ConString;

            } else if (!File.Exists("./Settings.cfg")) {

                frmSettings settings = new frmSettings();

                if (DialogResult.OK == settings.ShowDialog()) {

                    LoadSettings();

                }
            }
        }
    }
}
