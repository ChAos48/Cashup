using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Application_Updater
{
    public partial class Form1 : Form
    {
        string uri = "http://www.proteaboekwinkel.com/Updates/index.php?applicationname={0}&filename={1}&method={2}&RelDir={3}";
        string ftpUri = "ftp://protea.dedicated.co.za/";
        string ftpUserName = "dingo_update@proteaboekwinkel.com";
        string ftpPass = "8d8XLgUByXVI";
        List<UpdateFile> Files;
        int currentListRow = -1;

        Application CurrentApp = new Application("CashUp", "C:\\Protea\\Cashup");

        public Form1() {
            InitializeComponent();
            this.Text = CurrentApp.AppName + " Updater";

            btnUpdate_Click(this, new EventArgs());
        }



        private void btnUpdate_Click(object sender, EventArgs e) {
            this.btnUpdate.Enabled = false;
            if (!bgCheckFiles.IsBusy) {
                lstUpdate.Items.Clear();
                bgCheckFiles.RunWorkerAsync();
            }
        }

        public void GetUpdateList() {
            var AppName = CurrentApp.AppName;
            var Method = "getFiles";


            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(string.Format(uri, AppName, "", Method, ""));
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            byte[] buffer = new byte[512];
            var stream = resp.GetResponseStream();

            var serializer = new DataContractJsonSerializer(typeof(onlineupdateFile[]));
            var obj = (Array)serializer.ReadObject(stream);
            stream.Close();
            resp.Close();

            foreach (onlineupdateFile f in obj) {
                Files.Add(new UpdateFile(f.FileName, CurrentApp.RootDirectory + "/" + f.relativeDir, CurrentApp.RootDirectory));
            }
        }

        private void CheckFilesState() {
            foreach (var f in Files) {
                AddListItem("Checking File: " + f.FileName);
                CheckFileState(f);
            }
            AddListItem("Done.");
        }

        private delegate void SetItemCallback(string Text);
        private void SetListItem(string Text) {
            if (lstUpdate.InvokeRequired) {
                lstUpdate.BeginInvoke(new SetItemCallback(SetListItem), Text);
            } else {
                lstUpdate.Items[currentListRow] = Text;
            }
        }

        private delegate void AddItemCallback(string Text);
        private void AddListItem(string Text) {
            if (lstUpdate.InvokeRequired) {
                lstUpdate.BeginInvoke(new AddItemCallback(AddListItem), Text);
            } else {
                currentListRow = lstUpdate.Items.Add(Text);
            }
        }

        private delegate void ClearListCallback();
        private void ClearList() {
            if (lstUpdate.InvokeRequired) {
                lstUpdate.BeginInvoke(new ClearListCallback(ClearList));
            } else {
                lstUpdate.Items.Clear();
            }
        }

        private void CheckFileState(UpdateFile f) {
            SetListItem("Comparing Hashes");
            try {
                if (File.Exists(f.Directory + '\\' + f.FileName)) {
                    var onlineHash = GetOnlineHash(f);
                    if (f.Hash != onlineHash) {
                        CheckZHash(f);
                    } else
                        SetListItem(f.FileName + " is up to Date");
                } else {
                    CheckZHash(f);
                }
            } catch (Exception ex) {
                Deloitte.WriteErrorLog(new DingoException(ex));
            }
        }

        private string GetOnlineRootDir(UpdateFile f) {
            var FileName = f.FileName;
            var AppName = CurrentApp.AppName;
            var Method = "getUploadDir";


            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(string.Format(uri, AppName, FileName, Method, f.RelativeDirectory));
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

            return ResponedValue.TrimEnd('/').Replace("Updates/", "");
        }

        private void DownloadFile(UpdateFile f) {
            SetListItem("Downloading File");
            var ftpDir = GetOnlineRootDir(f);
            if (f.DownloadFile(ftpUri + ftpDir, ftpUserName, ftpPass)) {
                CheckZHash(f);
            } else {
                SetListItem(f.FileName + " Download Failed");
            }
        }

        public void CheckZHash(UpdateFile f) {
            var curHash = f.ZHash;
            string zhash = GetOnlineZHash(f);
            if (zhash == f.ZHash) {
                if (f.ExtractFile()) {
                    CheckFileState(f);
                }
            } else {
                DownloadFile(f);
            }
        }

        private string GetOnlineZHash(UpdateFile f) {
            var FileName = f.FileName;
            var AppName = CurrentApp.AppName;
            var Method = "getZHash";


            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(string.Format(uri, AppName, FileName, Method, f.RelativeDirectory));
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

        private string GetOnlineHash(UpdateFile f) {
            var FileName = f.FileName;
            var AppName = CurrentApp.AppName;
            var Method = "getHash";


            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(string.Format(uri, AppName, FileName, Method, f.RelativeDirectory));
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

        private void bgCheckFiles_DoWork(object sender, DoWorkEventArgs e) {
            Files = new List<UpdateFile>();
            AddListItem("Getting File Details");
            GetUpdateList();
            ClearList();
            CheckFilesState();
        }

        private void btnExit_Click(object sender, EventArgs e) {
            if (!bgCheckFiles.IsBusy) {
                this.Close();
            } else {
                MessageBox.Show("Please wait for update to finish");
            }
        }

        private void bgCheckFiles_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (!bgCheckFiles.IsBusy) {
                this.Close();
            }
        }
    }

    public class onlineupdateFile
    {
        public string FileName { get; set; }
        public string relativeDir { get; set; }

        public onlineupdateFile() {

        }
    }
}
