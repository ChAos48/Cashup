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
    public partial class frmSelect : Form
    {
        public string ID { get; set; }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private DataTable dt = new DataTable();

        public frmSelect() {
            InitializeComponent();
            log.Debug("frmSelect Opened");
        }

        //private void btnSearch_Click(object sender, EventArgs e) {

        //    SearchDate();

        //}

        private void frmSelect_Load(object sender, EventArgs e) {

            GetAll();

        }

        /// <summary>
        /// Depreciated. searches the DB for the all values with the selected date. Format (YYY-mm-dd)
        /// </summary>
        //private void SearchDate() {

        //    string query = String.Format(@"SELECT * FROM {0}.Cashup_data WHERE" +
        //        " date_format(cashup_date, '%Y-%m-%d') = DATE_FORMAT('{1}', '%Y-%m-%d');",
        //        RuntimeSettings.dbName ,dateTimePicker1.Value.ToString("yyyy-MM-dd HH-mm-ss"));

        //    using (OdbcConnection con = new OdbcConnection(RuntimeSettings.conString)) {
        //        OdbcCommand cmd = new OdbcCommand(query, con);
        //        try {
        //            con.Open();
        //        }catch(Exception ex) {
        //            log.Error("Cannot connect to Database.", ex);
        //        }

        //        OdbcDataAdapter da = new OdbcDataAdapter(cmd);
        //        da.Fill(dt);
        //        da.Dispose();

        //    }

        //    dataGridView1.DataSource = dt;
        //    dataGridView1.Refresh();

        //}

        /// <summary>
        /// Gets all the data in the table.
        /// </summary>
        private void GetAll() {

            string query = String.Format(@"SELECT * FROM {0}.Cashup_data;", RuntimeSettings.dbName);

            using (OdbcConnection con = new OdbcConnection(RuntimeSettings.conString)) {
                OdbcCommand cmd = new OdbcCommand(query, con);
                try {
                    con.Open();
                } catch (Exception ex) {
                    log.Error("Cannot connect to Database.", ex);
                }

                OdbcDataAdapter da = new OdbcDataAdapter(cmd);
                da.Fill(dt);
                da.Dispose();

            }

            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();

        }

        private void btnSelect_Click(object sender, EventArgs e) {

            foreach (DataGridViewRow row in dataGridView1.SelectedRows) {

                ID = row.Cells[0].Value.ToString();
                log.Debug("ID: " + ID);

            }
            DialogResult = DialogResult.OK;
        }

        private void frmSelect_FormClosing(object sender, FormClosingEventArgs e) {
            log.Debug("closing frmSelect");
        }
    }
}
