using Hounds;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Collections.Generic;
using Microsoft.Win32;

namespace OOP_Cashup
{
    public partial class frmSettings : Form
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public frmSettings() {
            InitializeComponent();
            foreach (object systemDriver in frmSettings.GetSystemDriverList())
                this.cmbxDrivers.Items.Add(systemDriver);
        }

        private void btnSubmit_Click(object sender, EventArgs e) {

            if (!File.Exists("./Settings.cfg")) {

                XDocument doc = new XDocument(new XElement("settings",
                                                new XElement("Host", Encryption.Encrypt(txtbIP.Text)),
                                                new XElement("Username", Encryption.Encrypt(txtbUsername.Text)),
                                                new XElement("Password", Encryption.Encrypt(txtbPassword.Text)),
                                                new XElement("DB", Encryption.Encrypt(txtbDBName.Text)),
                                                new XElement("Driver", cmbxDrivers.Text)));
                doc.Save("./Settings.cfg");
                log.Info("Settings.cfg created");
            }
            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private static List<string> GetSystemDriverList() {
            List<string> stringList = new List<string>();
            RegistryKey registryKey1 = Registry.LocalMachine.OpenSubKey("Software");
            if (registryKey1 != null) {
                RegistryKey registryKey2 = registryKey1.OpenSubKey("ODBC");
                if (registryKey2 != null) {
                    RegistryKey registryKey3 = registryKey2.OpenSubKey("ODBCINST.INI");
                    if (registryKey3 != null) {
                        RegistryKey registryKey4 = registryKey3.OpenSubKey("ODBC Drivers");
                        if (registryKey4 != null) {
                            foreach (string valueName in registryKey4.GetValueNames())
                                stringList.Add(valueName);
                        }
                        try {
                            registryKey4.Close();
                        } catch {
                        }
                    }
                }
            }
            return stringList;
        }

    }
}
