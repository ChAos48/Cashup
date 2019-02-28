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

            if (cu.LoadFromDB(ID)) {
                log.Info("Till data Successfully loaded from DB");
            } else {
                log.Error("couldnt load data from DB");
            }

            cu.Drop();
            setTextBoxes();

        }

        private void frmView_FormClosing(object sender, FormClosingEventArgs e) {
            log.Debug("Closing frmView");
            DialogResult = DialogResult.Cancel;
        }

        private void btnClose_click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
        }

        private void setTextBoxes() {
            #region till
            this.txtbR200.Text = cu.R200Amt.ToString();
            this.txtbR100.Text = cu.R100Amt.ToString();
            this.txtbR50.Text = cu.R50Amt.ToString();
            this.txtbR20.Text = cu.R20Amt.ToString();
            this.txtbR10.Text = cu.R10Amt.ToString();
            this.txtbR5.Text = cu.R5Amt.ToString();
            this.txtbR2.Text = cu.R2Amt.ToString();
            this.txtbR1.Text = cu.R1Amt.ToString();
            this.txtb50c.Text = cu.c50Amt.ToString();
            this.txtb20c.Text = cu.c20Amt.ToString();
            this.txtb10c.Text = cu.c10Amt.ToString();

            this.txtbTotalR200.Text = cu.R200.ToString();
            this.txtbTotalR100.Text = cu.R100.ToString();
            this.txtbTotalR50.Text = cu.R50.ToString();
            this.txtbTotalR20.Text = cu.R20.ToString();
            this.txtbTotalR10.Text = cu.R10.ToString();
            this.txtbTotalR5.Text = cu.R5.ToString();
            this.txtbTotalR2.Text = cu.R2.ToString();
            this.txtbTotalR1.Text = cu.R1.ToString();
            this.txtbTotal50c.Text = cu.c50.ToString();
            this.txtbTotal20c.Text = cu.c20.ToString();
            this.txtbTotal10c.Text = cu.c10.ToString();
            #endregion

            #region 
            this.txtbxR200Drop.Text = cu.R200Drop.ToString();
            this.txtbxR100Drop.Text = cu.R100Drop.ToString();
            this.txtbxR50Drop.Text = cu.R50Drop.ToString();
            this.txtbxR20Drop.Text = cu.R20Drop.ToString();
            this.txtbxR10Drop.Text = cu.R10Drop.ToString();
            this.txtbxR5Drop.Text = cu.R5Drop.ToString();
            this.txtbxR2Drop.Text = cu.R2Drop.ToString();
            this.txtbxR1Drop.Text = cu.R1Drop.ToString();
            this.txtbxc50Drop.Text = cu.c50Drop.ToString();
            this.txtbxc20Drop.Text = cu.c20Drop.ToString();
            this.txtbxc10Drop.Text = cu.c10Drop.ToString();

            this.txtbxR200TotalDrop.Text = cu.R200DropTotal.ToString();
            this.txtbxR100TotalDrop.Text = cu.R100DropTotal.ToString();
            this.txtbxR50TotalDrop.Text = cu.R50DropTotal.ToString();
            this.txtbxR20TotalDrop.Text = cu.R20DropTotal.ToString();
            this.txtbxR10TotalDrop.Text = cu.R10DropTotal.ToString();
            this.txtbxR5TotalDrop.Text = cu.R5DropTotal.ToString();
            this.txtbxR2TotalDrop.Text = cu.R2DropTotal.ToString();
            this.txtbxR1TotalDrop.Text = cu.R1DropTotal.ToString();
            this.txtbxc50TotalDrop.Text = cu.c50DropTotal.ToString();
            this.txtbxc20TotalDrop.Text = cu.c20DropTotal.ToString();
            this.txtbxc10TotalDrop.Text = cu.c10DropTotal.ToString();
            #endregion

            this.txtbFloatLeft.Text = cu.CashFloat.ToString();
            this.txtbFloatTop.Text = cu.CashFloat.ToString();
            this.txtbNameLeft.Text = cu.Name;
            this.txtbNameTop.Text = cu.Name;
            this.textBox1.Text = cu.Name;
            this.dtpLeft.Value = cu.Date;
            this.dtpTop.Value = cu.Date;
            this.dtpRight.Value = cu.Date;

            this.txtbRegisterLeft.Text = cu.TillNum.ToString();
            this.txtbRegisterRight.Text = cu.TillNum.ToString();

            this.txtbxSubTotal.Text = cu.subTotal.ToString();
            this.txtbDropTotal.Text = cu.DropTotal.ToString();
            this.txtbxCashDrop.Text = cu.drop.ToString();
            this.txtbxCheques.Text = cu.ChecksValue.ToString();
            this.txtbChequesValue.Text = cu.ChecksValue.ToString();
            this.txtbNumCheques.Text = cu.NumChecks.ToString();
            this.txtbxSkimmed.Text = cu.skimmed.ToString();
            this.txtbTotal_Drop.Text = (cu.drop + cu.NumChecks).ToString();
            log.Debug("finished loading");

        }

        private void button1_Click(object sender, EventArgs e) {
            cu.PrintFromView();
        }
    }
}
