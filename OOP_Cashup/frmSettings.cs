using Hounds;
using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Collections.Generic;

namespace OOP_Cashup
{
    public partial class frmSettings : Form
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public frmSettings() {
            InitializeComponent();
            cmbxDrivers.DataSource = GetSystemDriverList();
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

        private static List<String> GetSystemDriverList() {
            List<string> names = new List<string>();
            // get system dsn's
            Microsoft.Win32.RegistryKey reg = (Microsoft.Win32.Registry.LocalMachine).OpenSubKey("Software");
            if (reg != null) {
                reg = reg.OpenSubKey("ODBC");
                if (reg != null) {
                    reg = reg.OpenSubKey("ODBCINST.INI");
                    if (reg != null) {

                        reg = reg.OpenSubKey("ODBC Drivers");
                        if (reg != null) {
                            // Get all DSN entries defined in DSN_LOC_IN_REGISTRY.
                            foreach (string sName in reg.GetValueNames()) {
                                names.Add(sName);
                            }
                        }
                        try {
                            reg.Close();
                        } catch { /* ignore this exception if we couldn't close */ }
                    }
                }
            }

            return names;
        }

    }
}
