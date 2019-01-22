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
                    log.Warn("Dialog result from frmSelect is not OK");
                    
                    return;

                }
            }
            LoadData(ID);
        }

        private void LoadData(string ID) {

            foreach(TextBox in this.)

            if(cu.LoadFromDB(ID)) {
                log.Info("Till data Successfully loaded from DB");
            } else {
                log.Error("couldnt load data from DB");
            }

            cu.Drop();

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
