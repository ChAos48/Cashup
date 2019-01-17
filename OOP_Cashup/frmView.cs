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

        private Cashup cu = new Cashup();

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
                    cu.R200Amt = int.Parse(rdr["cashup_R200Amount"].ToString());
                    cu.R100Amt = int.Parse(rdr["cashup_R100Amount"].ToString());
                    cu.R50Amt = int.Parse(rdr["cashup_R50Amount"].ToString());
                    cu.R20Amt = int.Parse(rdr["cashup_R20Amount"].ToString());
                    cu.R10Amt = int.Parse(rdr["cashup_R10Amount"].ToString());
                    cu.R5Amt = int.Parse(rdr["cashup_R5Amount"].ToString());
                    cu.R2Amt = int.Parse(rdr["cashup_R2Amount"].ToString());
                    cu.R1Amt = int.Parse(rdr["cashup_R1Amount"].ToString());
                    cu.c50Amt = int.Parse(rdr["cashup_50cAmount"].ToString());
                    cu.c20Amt = int.Parse(rdr["cashup_20cAmount"].ToString());
                    cu.c10Amt = int.Parse(rdr["cashup_10cAmount"].ToString());
                    cu.c5Amt = int.Parse(rdr["cashup_5cAmount"].ToString());

                    cu.CashFloat = decimal.Parse(rdr["cashup_float"].ToString());
                    cu.TillNum = "Register #" + rdr["cashup_TillNum"].ToString();
                    cu.Name = rdr["cashup_CashierName"].ToString();
                    dateTimePicker1.Value = DateTime.ParseExact(rdr["cashup_date"].ToString(),"yyyy-MM-dd HH:mm:ss", null);

                    cu.R200 = int.Parse(rdr["cashup_R200Value"].ToString());
                    cu.R100 = int.Parse(rdr["cashup_R100Value"].ToString());
                    cu.R50 = int.Parse(rdr["cashup_R50Value"].ToString());
                    cu.R20 = int.Parse(rdr["cashup_R20Value"].ToString());
                    cu.R10 = int.Parse(rdr["cashup_R10Value"].ToString());
                    cu.R5 = int.Parse(rdr["cashup_R5Value"].ToString());
                    cu.R2 = int.Parse(rdr["cashup_R2Value"].ToString());
                    cu.R1 = int.Parse(rdr["cashup_R1Value"].ToString());
                    cu.c50 = int.Parse(rdr["cashup_50cValue"].ToString());
                    cu.c20 = int.Parse(rdr["cashup_20cValue"].ToString());
                    cu.c10 = int.Parse(rdr["cashup_10cValue"].ToString());
                    cu.c5 = int.Parse(rdr["cashup_5cValue"].ToString());

                    cu.R200Drop = int.Parse(rdr["cashup_R200Value"].ToString());
                    cu.R100Drop = int.Parse(rdr["cashup_R100Value"].ToString());
                    cu.R50Drop = int.Parse(rdr["cashup_R50Value"].ToString());
                    cu.R20Drop = int.Parse(rdr["cashup_R20Value"].ToString());
                    cu.R10Drop = int.Parse(rdr["cashup_R10Value"].ToString());
                    cu.R5Drop = int.Parse(rdr["cashup_R5Value"].ToString());
                    cu.R2Drop = int.Parse(rdr["cashup_R2Value"].ToString());
                    cu.R1Drop = int.Parse(rdr["cashup_R1Value"].ToString());
                    cu.c50Drop = int.Parse(rdr["cashup_50cValue"].ToString());
                    cu.c20Drop = int.Parse(rdr["cashup_20cValue"].ToString());
                    cu.c10Drop = int.Parse(rdr["cashup_10cValue"].ToString());
                    cu.c5Drop = int.Parse(rdr["cashup_5cValue"].ToString());

                    cu.R200DropTotal = int.Parse(rdr["cashup_R200DropValue"].ToString());
                    cu.R100DropTotal = int.Parse(rdr["cashup_R100DropValue"].ToString());
                    cu.R50DropTotal = int.Parse(rdr["cashup_R50DropValue"].ToString());
                    cu.R20DropTotal = int.Parse(rdr["cashup_R20DropValue"].ToString());
                    cu.R10DropTotal = int.Parse(rdr["cashup_R10DropValue"].ToString());
                    cu.R5DropTotal = int.Parse(rdr["cashup_R5DropValue"].ToString());
                    cu.R2DropTotal = int.Parse(rdr["cashup_R2DropValue"].ToString());
                    cu.R1DropTotal = int.Parse(rdr["cashup_R1DropValue"].ToString());
                    cu.c50DropTotal = int.Parse(rdr["cashup_50cDropValue"].ToString());
                    cu.c20DropTotal = int.Parse(rdr["cashup_20cDropValue"].ToString());
                    cu.c10DropTotal = int.Parse(rdr["cashup_10cDropValue"].ToString());
                    cu.c5DropTotal = int.Parse(rdr["cashup_5cDropValue"].ToString());

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
