﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OOP_Cashup;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;

namespace OOP_Cashup {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            #region Log code(changes Console.write line to write to file)
            if (!Directory.Exists(@"./Logs")) {
                Directory.CreateDirectory(@"./Logs");
                Console.WriteLine(DateTime.Now + ": Created Log dir.");
            }

            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try {
                string logFile = Environment.CurrentDirectory  + "\\Logs\\" + DateTime.Now.ToFileTime().ToString() + ".log";
                ostrm = new FileStream(logFile, FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            } catch (Exception e) {
                Console.WriteLine(DateTime.Now +": Cannot open log file for writing");
                Console.WriteLine(e.Message);
                return;
            }

            Console.SetOut(writer);
#endregion
            Console.WriteLine("{0}: Started application", DateTime.Now.ToString());

#if DEBUG
            //Console.WriteLine("in debug mode skipping update");
            //goto skipupdate;

#endif

            FileInfo tFile = new FileInfo(Environment.CurrentDirectory + "\\Cashup.exe");
            string online_hash = GetOnlineHash(tFile);
            string local_hash = GetHash();

            Console.WriteLine(DateTime.Now + ": Online Hash = " + online_hash);
            Console.WriteLine(DateTime.Now + ": Online Hash = " + local_hash);

            var temp = online_hash == local_hash;
#if DEBUG
            temp = true;
            goto skipupdate;
#endif
            if (temp) {

                Console.WriteLine(DateTime.Now + ": no update required");

            } else {

                Console.WriteLine(DateTime.Now + ": Update Required");
                Process proc = new Process();
                proc.StartInfo.FileName = Environment.CurrentDirectory + "\\CashUp Updater.exe";
                proc.EnableRaisingEvents = true;

                proc.Start();

                proc.WaitForExit();

            }
            string hashAfterUpdate = GetHash();
            Console.WriteLine(DateTime.Now + ": New Local Hash = " + hashAfterUpdate);
            goto skipupdate;

            skipupdate:
            Application.Run(new Form1());
            goto Finish;

            Finish:
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");

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

    }
}
