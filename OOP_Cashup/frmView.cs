using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using Hounds;

namespace OOP_Cashup
{
    public partial class frmView : Form
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string ID = "";

        public frmView() {
            InitializeComponent();
            log.Debug("frmView Opened");

            using (frmSelect selectFrm = new frmSelect()) {
                if (selectFrm.ShowDialog() == DialogResult.OK) {

                    ID = selectFrm.ID;

                } else {

                    log.Debug("user abort/non slelect");
                    DialogResult = DialogResult.Abort;
                    return;

                }
            }
            LoadData(ID);
        }

        private void LoadData(string ID) {

            OdbcDataReader rdr = null;

            using (OdbcConnection con = new OdbcConnection(RuntimeSettings.conString)) {

                try {
                    con.Open();
                } catch (Exception ex) {
                    log.Error("Cannot connect to DB", ex);
                    return;
                }

                string query = "SELECT * WHERE cashup_ID = \"" + ID + "\"";
                OdbcCommand cmd = new OdbcCommand(query, con);

                rdr = cmd.ExecuteReader();

                while (rdr.Read()) {
                    //    Cashup cu = new Cashup(decimal.Parse(rdr["cashup_R200Amount"].ToString()),
                    //        decimal.Parse(rdr["cashup_R100Amount"].ToString()),
                    //        decimal.Parse(rdr["cashup_R50Amount"].ToString()),
                    //        decimal.Parse(rdr["cashup_R20Amount"].ToString()),
                    //        decimal.Parse(rdr["cashup_R10Amount"].ToString()),
                    //        decimal.Parse(rdr["cashup_R5Amount"].ToString()),
                    //        decimal.Parse(rdr["cashup_R2Amount"].ToString()),
                    //        decimal.Parse(rdr["cashup_R1Amount"].ToString()),
                    //        decimal.Parse(rdr["cashup_50cAmount"].ToString()),
                    //        decimal.Parse(rdr["cashup_20cAmount"].ToString()),
                    //        decimal.Parse(rdr["cashup_10cAmount"].ToString())
                    //        decimal.Parse(rdr["cashup_5cAmount"].ToString()),
                    //        );
                }

            }

        }

        private void frmView_FormClosing(object sender, FormClosingEventArgs e) {
            log.Debug("Closing frmView");
            DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }
    }
}
