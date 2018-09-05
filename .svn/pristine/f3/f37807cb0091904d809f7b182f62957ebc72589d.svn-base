using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.IO.Compression;
using System.ComponentModel;
namespace Application_Updater
{
    public class Rehab
    {
        internal void GenerateUpdateList() { }
    }

    public class UpdateList
    {

    }

    [Serializable]
    public class UpdateFile : IDisposable
    {
        [NonSerialized]
        BackgroundWorker bgGetHash = new BackgroundWorker();

        [NonSerialized]
        BackgroundWorker bgGetZHash = new BackgroundWorker();

        DateTime LastZHashUpdate = new DateTime(0);
        DateTime LastHashUpdate = new DateTime(0);

        /// <summary>
        /// Name of the File to Upload/Download
        /// </summary>
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// Directory on the Development Computer to The File
        /// </summary>
        internal string Directory;

        private string _rootDirectory;
        public string RootDirectory
        {
            get
            {
                return _rootDirectory;
            }
            set
            {

                value.TrimEnd('/');
                Regex regex = new Regex("^([a-zA-Z]:)");
                if (!regex.IsMatch(value) && !value.StartsWith("/"))
                { value = "/" + value; }

                _rootDirectory = value;
            }
        }

        /// <summary>
        /// Directory Relative to the root Install folder on client computer
        /// </summary>
        private string _relativeDirectory;
        public string RelativeDirectory
        {
            get
            {
                return _relativeDirectory;
            }
            set
            {

                value.TrimEnd('/');
                Regex regex = new Regex("^([a-zA-Z]:)");
                if (regex.IsMatch(value))
                    regex.Replace(value, "");
                if (!value.StartsWith("/") && !value.StartsWith("\\"))
                {
                    value = "/" + value;
                }
                if (!value.EndsWith("/") && !value.EndsWith("\\"))
                { value = value + "/"; }
                _relativeDirectory = value;
            }
        }

        public DateTime TimeUpdated
        {
            get;
            set;
        }

        public string FileSize
        {
            get;
            set;
        }

        public string Online
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public delegate void IgnoreChangedDel(bool IgnoreState, string FileName, UpdateFile sender);
        public event IgnoreChangedDel IgnoreChanged;
        public bool Ignore
        {
            get { return ignore; }
            set
            {
                if (IgnoreChanged != null)
                    IgnoreChanged(value, this.FileName, this);
                ignore = value;
            }
        }
        private bool ignore;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public void CompressFile()
        {
            string basedir = RootDirectory;
            string FileDir = basedir + RelativeDirectory + FileName;
            if (!File.Exists(FileDir))
                return;

            FileInfo fileToCompress = new FileInfo(FileDir);
            basedir = new DirectoryInfo(RootDirectory).Parent.FullName + "/updateFiles";
            string ZippedDir = basedir + RelativeDirectory;

            if (!System.IO.Directory.Exists(ZippedDir))
            { System.IO.Directory.CreateDirectory(ZippedDir); }
            ZippedDir = ZippedDir + FileName + ".dng";

            using (FileStream originalFileStream = fileToCompress.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if ((File.GetAttributes(fileToCompress.FullName) &
                   FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".dng")
                {
                    using (FileStream compressedFileStream = File.Open(ZippedDir, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                           CompressionMode.Compress))
                        {
                            byte[] buffer = new byte[4096];
                            int numRead;
                            while ((numRead = originalFileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                compressionStream.Write(buffer, 0, numRead);
                            }

                        }
                    }
                }

            }
            GetZippedHash();
        }

        /// <summary>
        /// Hash Of the Extracted File
        /// </summary>
        private string _hash;
        public string Hash
        {
            get
            {
                _hash = GetHash();
                return _hash;
            }
        }

        private string GetHash()
        {
            string basedir = RootDirectory;
            string ZippedDir = basedir + RelativeDirectory + FileName;
            if (!File.Exists(ZippedDir))
                return "FileNotFound";
            FileStream f = File.Open(ZippedDir, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            SHA512 sha = SHA512.Create();
            sha.ComputeHash(f);
            f.Close();

            byte[] hash = sha.Hash;

            string hashedPwd = string.Empty;

            for (int i = 0; i < hash.Length; i++)
            {
                hashedPwd += hash[i].ToString("x2");
            }


            return hashedPwd;
        }

        /// <summary>
        /// Zipped Hash
        /// </summary>
        private string _zhash;
        public string ZHash
        {
            get
            {

                _zhash = GetZippedHash();
                return _zhash;
            }
        }

        private string GetZippedHash()
        {
            string basedir = RootDirectory + "/updateFiles";
            string ZippedDir = basedir + RelativeDirectory + FileName + ".dng";
            if (!File.Exists(ZippedDir))
            {
                if (!File.Exists(ZippedDir))
                    return "FileNotFound";
            }
            FileStream f = File.Open(ZippedDir, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            SHA512 sha = SHA512.Create();
            sha.ComputeHash(f);
            f.Close();

            byte[] hash = sha.Hash;

            string hashedPwd = string.Empty;

            for (int i = 0; i < hash.Length; i++)
            {
                hashedPwd += hash[i].ToString("x2");
            }
            return hashedPwd;
        }

        public UpdateFile(string FileName, string Directory, string RootDirectory)
        {
            this.RelativeDirectory = Directory.Replace(RootDirectory, "");
            this.Directory = Directory;
            this.FileName = FileName;
            this.RootDirectory = RootDirectory;
            bgGetHash = new BackgroundWorker();
            bgGetHash.DoWork += bgGetHash_DoWork;
            bgGetZHash = new BackgroundWorker();
            bgGetZHash.DoWork += bgGetZHash_DoWork;
            GetHash();
            GetZippedHash();
        }

        void bgGetHash_DoWork(object sender, DoWorkEventArgs e)
        {
            if (string.IsNullOrEmpty(_hash) ||
                       (DateTime.Now - LastHashUpdate).TotalSeconds > 20)
            {
                _hash = GetHash();
                LastHashUpdate = DateTime.Now;
            }
        }

        public UpdateFile(FileInfo File, string RootDirectory)
        {
            this.RootDirectory = RootDirectory;
            RelativeDirectory = File.Directory.FullName.Replace(RootDirectory, "");
            this.Directory = File.Directory.FullName;
            this.FileName = File.Name;
            this.TimeUpdated = File.LastWriteTime;
            this.FileSize = File.Length.ToString();
            bgGetHash = new BackgroundWorker();
            bgGetHash.DoWork += bgGetHash_DoWork;
            bgGetZHash = new BackgroundWorker();
            bgGetZHash.DoWork += bgGetZHash_DoWork;
        }

        private void bgGetZHash_DoWork(object sender, DoWorkEventArgs e)
        {
            if (string.IsNullOrEmpty(_zhash) ||
                       (DateTime.Now - LastZHashUpdate).TotalSeconds > 20)
            {
                _zhash = GetZippedHash();
                LastZHashUpdate = DateTime.Now;
            }
        }

        public UpdateFile()
        {
            bgGetHash = new BackgroundWorker();
            bgGetHash.DoWork += bgGetHash_DoWork;
            bgGetZHash = new BackgroundWorker();
            bgGetZHash.DoWork += bgGetZHash_DoWork;
        }

        public void GetFile(string Server, string UserName, string Password)
        {
            if (!string.IsNullOrEmpty(ZHash))
            {

            }
        }

        private long FtpFileSize(string url, string Username, string Password)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(url + RelativeDirectory + FileName + ".dng"));
                request.Credentials = new NetworkCredential(Username, Password);
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.UseBinary = true;
                request.UsePassive = false;
                request.KeepAlive = true;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response = (FtpWebResponse)request.GetResponse();
                long contentLength = response.ContentLength;
                response.Close();
                return contentLength;
            }
            catch { return 0; }
        }

        //public bool DownloadFile(string url, string Username, string Password)
        //{
        //    try
        //    {
        //        string basedir = RootDirectory + "/updateFiles";
        //        string ZippedDir = basedir + RelativeDirectory + FileName + ".dng";

        //        if (!System.IO.Directory.Exists(basedir + RelativeDirectory))
        //        {
        //            System.IO.Directory.CreateDirectory(basedir + RelativeDirectory);
        //        }
        //        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(url + "/" + FileName + ".dng"));
        //        request.Credentials = new NetworkCredential(Username, Password);
        //        request.Method = WebRequestMethods.Ftp.DownloadFile;
        //        request.UseBinary = true;
        //        request.UsePassive = false;
        //        request.KeepAlive = true;

        //        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
        //        string welocme = response.WelcomeMessage;

        //        FileStream stream2 = new FileStream(ZippedDir, FileMode.Create, FileAccess.Write);

        //        Stream responseStream = response.GetResponseStream();
        //        long bufferSize = 1024 * 50;
        //        byte[] buffer = new byte[bufferSize];
        //        int count = responseStream.Read(buffer, 0, buffer.Length);
        //        long nowFileSize = this.FtpFileSize(url, Username, Password);
        //        long totalFileSize = nowFileSize;
        //        long downloaded = 0;
        //        int retry = 0;
        //        while (count > 0L && retry < 3)
        //        {
        //            downloaded += count;
        //            nowFileSize -= count;
        //            stream2.Write(buffer, 0, count);
        //            buffer = new byte[bufferSize];
        //            count = responseStream.Read(buffer, 0, buffer.Length);

        //            if (response.StatusCode == FtpStatusCode.ClosingData)
        //            {
        //                if (downloaded < totalFileSize)
        //                {

        //                    stream2.Close();
        //                    responseStream.Close();
        //                    response.Close();
        //                    retry += 1;

        //                    request = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + Username + "@" + url + "/" + FileName));
        //                    request.Credentials = new NetworkCredential(Username, Password);
        //                    request.Method = WebRequestMethods.Ftp.DownloadFile;
        //                    request.UseBinary = true;
        //                    request.UsePassive = false;
        //                    request.KeepAlive = true;
        //                    request.ContentOffset = downloaded;

        //                    response = (FtpWebResponse)request.GetResponse();
        //                    welocme = response.WelcomeMessage;
        //                    Thread.SpinWait(20);

        //                    stream2 = new FileStream(ZippedDir, FileMode.Append, FileAccess.Write);

        //                    responseStream = response.GetResponseStream();
        //                    bufferSize = 1024 * 50;
        //                    buffer = new byte[bufferSize];
        //                    count = responseStream.Read(buffer, 0, buffer.Length);
        //                }
        //            }

        //        }
        //        stream2.Close();
        //        responseStream.Close();
        //        if (retry <= 3)
        //            return true;
        //        return false;
        //    }
        //    catch { return false; }

        //}

        public bool DownloadFile(string url, string Username, string Password)
        {
            try
            {
                url = url.Replace("ftp://protea.dedicated.co.za", "");
                string basedir = RootDirectory + "/updateFiles";
                string ZippedDir = basedir + RelativeDirectory + FileName + ".dng";

                if (!System.IO.Directory.Exists(basedir + RelativeDirectory))
                {
                    System.IO.Directory.CreateDirectory(basedir + RelativeDirectory);
                }
                Limilabs.FTP.Client.Ftp ftpClient = new Limilabs.FTP.Client.Ftp();
                ftpClient.Connect("protea.dedicated.co.za", 21, false);
                ftpClient.Login("dingo_update@proteaboekwinkel.com", "8d8XLgUByXVI");
                ftpClient.Progress += ftpClient_Download_File_Progress;
                this.FileSize = ftpClient.GetFileSize(url + "/" + FileName + ".dng").ToString();
                if (!System.IO.Directory.Exists(RootDirectory + "/temp"))
                {
                    System.IO.Directory.CreateDirectory(RootDirectory + "/temp");
                }
                ftpClient.Download(url + "/" + FileName + ".dng", RootDirectory + "/temp/" + FileName + ".temp");
                ftpClient.Close(false);
                if (File.Exists(ZippedDir))
                    File.Replace(RootDirectory + "/temp/" + FileName + ".temp", ZippedDir, FileName + ".bak");
                else
                    File.Move(RootDirectory + "/temp/" + FileName + ".temp", ZippedDir);
                return true;
            }
            catch (Exception ex)
            {
                Deloitte.WriteErrorLog(new DingoException(ex));
                return false;
            }

        }

        void ftpClient_Download_File_Progress(object sender, Limilabs.FTP.Client.ProgressEventArgs e)
        {

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public bool ExtractFile()
        {
            try
            {
                string basedir = RootDirectory + "/updateFiles";
                string ZippedDir = basedir + RelativeDirectory + FileName + ".dng";
                string ExtractedDir = RootDirectory + RelativeDirectory + FileName;
                if (!System.IO.Directory.Exists(RootDirectory + RelativeDirectory))
                {
                    System.IO.Directory.CreateDirectory(RootDirectory + RelativeDirectory);
                }

                if (File.Exists(ZippedDir))
                {
                    using (var writeStream = File.Create(ExtractedDir))
                    {
                        using (var readStream = File.Open(ZippedDir, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            using (GZipStream zstream = new GZipStream(readStream, CompressionMode.Decompress))
                            {
                                byte[] buffer = new byte[51200];
                                int count;
                                while ((count = zstream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    writeStream.Write(buffer, 0, count);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Deloitte.WriteErrorLog(new DingoException(ex));
                return false;
            }
        }

        protected virtual void Dispose(bool dispose)
        {
            if (dispose)
            {
                bgGetZHash.Dispose();
                bgGetHash.Dispose();
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    [Serializable]
    public class Application
    {
        public string RootDirectory { get; set; }
        public string AppName { get; set; }
        public List<string> MonitoredDirectories { get; set; }

        public Application()
            : this("Dingo")
        { }

        public Application(string AppName) : this(AppName, System.Windows.Forms.Application.StartupPath + '\\' + AppName) { }

        public Application(string AppName, string RootDir)
        {
            this.AppName = AppName;
            this.RootDirectory = RootDir;
            this.MonitoredDirectories = new List<string>();
        }

        public Application(string AppName, string RootDir, string[] MonitoredDirectories)
        {
            this.AppName = AppName;
            this.RootDirectory = RootDir;
            this.MonitoredDirectories = new List<string>();
            this.MonitoredDirectories.AddRange(MonitoredDirectories);
        }

        public Application(string AppName, string RootDir, List<string> MonitoredDirectories)
            : this(AppName, RootDir, MonitoredDirectories.ToArray())
        { }

        public void SetRootDirectory()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            var dres = fbd.ShowDialog();
            if (dres == DialogResult.No || dres == DialogResult.Cancel || dres == DialogResult.Abort)
            {
                return;
            }

            SetRootDirectory(fbd.SelectedPath);
        }

        public void SetRootDirectory(string RootDir)
        {

            RootDirectory = RootDir;
        }

        public void AddDirectory()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            var dres = fbd.ShowDialog();
            if (dres == DialogResult.No || dres == DialogResult.Cancel || dres == DialogResult.Abort)
            {
                return;
            }

            if (!MonitoredDirectories.Contains(fbd.SelectedPath))
            {
                MonitoredDirectories.Add(fbd.SelectedPath);
            }
        }

    }
}
