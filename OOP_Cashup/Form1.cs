using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Threading;
using System.IO;
using Microsoft.Reporting.WinForms;
using System.Drawing.Imaging;

namespace OOP_Cashup
    {
    public partial class Form1: Form {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Variables

        #region amount variables

        Decimal cashFloat = 0.00M;
        Decimal R200Total = 0.00M;
        Decimal R100Total = 0.00M;
        Decimal R50Total = 0.00M;
        Decimal R20Total = 0.00M;
        Decimal R10Total = 0.00M;
        Decimal R5Total = 0.00M;
        Decimal R2Total = 0.00M;
        Decimal R1Total = 0.00M;
        Decimal c50Total = 0.00M;
        Decimal c20Total = 0.00M;
        Decimal c10Total = 0.0M;
        Decimal c5Total = 0.00M;
        Decimal subTotal = 0.00M;
        Decimal drop = 0.00M;
        Decimal cheques = 0.00M;
        Decimal skimmed = 0.00M;
        Decimal total = 0.00M;
        #endregion

        #region drop variables

        Decimal R200TotalDrop = 0.00M;
        Decimal R100TotalDrop = 0.00M;
        Decimal R50TotalDrop = 0.00M;
        Decimal R20TotalDrop = 0.00M;
        Decimal R10TotalDrop = 0.00M;
        Decimal R5TotalDrop = 0.00M;
        Decimal R2TotalDrop = 0.00M;
        Decimal R1TotalDrop = 0.00M;
        Decimal c50TotalDrop = 0.00M;
        Decimal c20TotalDrop = 0.00M;
        Decimal c10TotalDrop = 0.00M;
        Decimal c5TotalDrop = 0.00M;
        static Decimal droppedTotal = 0.00M;

        Decimal dropTemp = 0.00M;
        #endregion

        private PrintDocument printDocument1 = new PrintDocument();

        public DateTimePicker date;

        private Cashup cu;

        #endregion Variables

        public Form1() {
            InitializeComponent();
            cu = new Cashup();

            date = dateTimePicker1;

            CheckRegNumber();

        }

        /// <summary>
        /// The Update Method, Updates all UI elements to their correct values.
        /// </summary>
        public new void Update() {

            try {

                lblRegisterNum.Text = combxRegister.Text;
                lblRegisterNum1.Text = combxRegister.Text;
                txtbxFloat1.Text = txtbFloat.Text;
                lblName0.Text = txtbNameLeft.Text;
                lblName1.Text = txtbNameLeft.Text;
                txtbChequesValue.Text = txtbxCheques.Text;
                cu.NumChecks = int.Parse(this.txtbNumCheques.Text);
                cu.ChecksValue = Decimal.Parse(txtbChequesValue.Text);
               

                #region regster update


                cu.R200Amt = int.Parse(txtbR200.Text);
                cu.R100Amt = int.Parse(txtbR100.Text);
                cu.R50Amt = int.Parse(txtbR50.Text);
                cu.R20Amt = int.Parse(txtbR20.Text);
                cu.R10Amt = int.Parse(txtbR10.Text);
                cu.R5Amt = int.Parse(txtbR5.Text);
                cu.R2Amt = int.Parse(txtbR2.Text);
                cu.R1Amt = int.Parse(txtbR1.Text);
                cu.c50Amt = int.Parse(txtb50c.Text);
                cu.c20Amt = int.Parse(txtb20c.Text);
                cu.c10Amt = int.Parse(txtb10c.Text);
                cu.c5Amt = int.Parse(txtb5c.Text);

                Decimal.TryParse(txtbChequesValue.Text, out cheques);
                Decimal.TryParse(txtbxSkimmed.Text, out skimmed);



                this.R200Total = (cu.R200Amt * 200);
                this.R100Total = (cu.R100Amt * 100);
                this.R50Total = (cu.R50Amt * 50);
                this.R20Total = (cu.R20Amt * 20);
                this.R10Total = (cu.R10Amt * 10);
                this.R5Total = (cu.R5Amt * 5);
                this.R2Total = (cu.R2Amt * 2);
                this.R1Total = (cu.R1Amt * 1);
                this.c50Total = (cu.c50Amt * 0.5M);
                this.c20Total = (cu.c20Amt * 0.2M);
                this.c10Total = (cu.c10Amt * 0.1M);
                this.c5Total = (cu.c5Amt * 0.05M);

                txtbTotalR200.Text = this.R200Total.ToString();
                txtbTotalR100.Text = this.R100Total.ToString();
                txtbTotalR50.Text = this.R50Total.ToString();
                txtbTotalR20.Text = this.R20Total.ToString();
                txtbTotalR10.Text = this.R10Total.ToString();
                txtbTotalR5.Text = this.R5Total.ToString();
                txtbTotalR2.Text = this.R2Total.ToString();
                txtbTotalR1.Text = this.R1Total.ToString();
                txtbTotal50c.Text = this.c50Total.ToString();
                txtbTotal20c.Text = this.c20Total.ToString();
                txtbTotal10c.Text = this.c10Total.ToString();
                txtbTotal5c.Text = this.c5Total.ToString();
                #endregion

                cu.CashFloat = this.cashFloat;
                this.subTotal = cu.subTotal;
                this.drop = cu.drop;
                dropTemp = this.drop;
                this.total = cu.total;
                txtbxSubTotal.Text = this.subTotal.ToString();
                txtbxCashDrop.Text = this.drop.ToString();
                cu.Name = this.txtbNameLeft.Text;
                cu.TillNum = combxRegister.Text;


                #region drop update

                txtbxR200Drop.Text = cu.R200Drop.ToString();
                txtbxR100Drop.Text = cu.R100Drop.ToString();
                txtbxR50Drop.Text = cu.R50Drop.ToString();
                txtbxR20Drop.Text = cu.R20Drop.ToString();
                txtbxR10Drop.Text = cu.R10Drop.ToString();
                txtbxR5Drop.Text = cu.R5Drop.ToString();
                txtbxR2Drop.Text = cu.R2Drop.ToString();
                txtbxR1Drop.Text = cu.R1Drop.ToString();
                txtbxc50Drop.Text = cu.c50Drop.ToString();
                txtbxc20Drop.Text = cu.c20Drop.ToString();
                txtbxc10Drop.Text = cu.c10Drop.ToString();
                txtbxc5Drop.Text = cu.c5Drop.ToString();


                R200TotalDrop = cu.R200Drop * 200;
                R100TotalDrop = cu.R100Drop * 100;
                R50TotalDrop = cu.R50Drop * 50;
                R20TotalDrop = cu.R20Drop * 20;
                R10TotalDrop = cu.R10Drop * 10;
                R5TotalDrop = cu.R5Drop * 5;
                R2TotalDrop = cu.R2Drop * 2;
                R1TotalDrop = cu.R1Drop * 1;
                c50TotalDrop = cu.c50Drop * 0.5M;
                c20TotalDrop = cu.c20Drop * 0.2M;
                c10TotalDrop = cu.c10Drop * 0.1M;
                c5TotalDrop = cu.c5Drop * 0.05M;

                txtbxR200TotalDrop.Text = this.R200TotalDrop.ToString();
                txtbxR100TotalDrop.Text = this.R100TotalDrop.ToString();
                txtbxR50TotalDrop.Text = this.R50TotalDrop.ToString();
                txtbxR20TotalDrop.Text = this.R20TotalDrop.ToString();
                txtbxR10TotalDrop.Text = this.R10TotalDrop.ToString();
                txtbxR5TotalDrop.Text = this.R5TotalDrop.ToString();
                txtbxR1TotalDrop.Text = this.R1TotalDrop.ToString();
                txtbxR2TotalDrop.Text = this.R2TotalDrop.ToString();
                txtbxc50TotalDrop.Text = this.c50TotalDrop.ToString();
                txtbxc20TotalDrop.Text = this.c20TotalDrop.ToString();
                txtbxc10TotalDrop.Text = this.c10TotalDrop.ToString();
                txtbxc5TotalDrop.Text = this.c5TotalDrop.ToString();

                droppedTotal = R200TotalDrop + R100TotalDrop + R50TotalDrop + R20TotalDrop + R10TotalDrop
                   + R5TotalDrop + R2TotalDrop + R1TotalDrop + c50TotalDrop + c20TotalDrop +
                   c10TotalDrop + c5TotalDrop /**+ cheques**/;
                txtbDropTotal.Text = droppedTotal.ToString();
                cu.DroppedTotal = droppedTotal;

                txtbTotal_Drop.Text = (droppedTotal + cheques).ToString();
                cu.DropTotal = droppedTotal + cheques;

                #endregion

            } catch (FormatException) {
                foreach (Control x in this.Controls) {
                    TextBox txt = x as TextBox;
                    if (txt is TextBox) {
                        if (string.IsNullOrEmpty(txt.Text)) {
                          txt.Text = "0";
                          txt.Focus();
                          txt.SelectAll();
                        }
                    }
                }

            }


        }

        /// <summary>
        /// double checks that register and name have been changed and then totals and drops everything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTotal_Click(object sender, EventArgs e) {

            int numCheques = int.Parse(txtbNumCheques.Text);

            if (cheques > 0 && numCheques == 0) {

                MessageBox.Show("Please make sure you have entered the number of cheques.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            } else {

                if (combxRegister.Text == "Register#") {

                    MessageBox.Show("Please Change the Register number AND make sure the float is right.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;

                } else {

                    if (drop <= 0) {

                        //txtbFloat.Text = subTotal.ToString();

                    }
                    ClearDrop();

                    Update();

                    cu.Drop();

                    txtbTotal1.Text = this.total.ToString();
                }
            }

        }


        private void combxRegister_SelectedIndexChanged(object sender, EventArgs e) {

            CheckRegNumber();

        }

        private new void TextChanged(object sender, EventArgs e) {
            if (this.Text == null) {
                this.Text = "0";
            }

        }

        /// <summary>
        /// sets the Float by checking the register Numbers
        /// </summary>
        private void CheckRegNumber() {

            if (combxRegister.SelectedIndex == 4) {
                txtbFloat.Text = "5000";
                this.cashFloat = 5000.00M;
                log.Info("Till 5 float set to R5000");
                
            } else if (combxRegister.SelectedIndex == 8 || combxRegister.SelectedIndex == 7) {
                txtbFloat.Text = "3000";
                this.cashFloat = 3000.00M;
                log.Info("Night staff float set to R5000");
            } else if (combxRegister.SelectedIndex == 5) {
                txtbFloat.Text = "10000";
                this.cashFloat = 10000.00M;
                log.Info("Till 6 float set to R1000.00");
            } else {
                txtbFloat.Text = "3000";
                this.cashFloat = 3000.00M;
                log.Info("float set to R3000");

            }
            cu.CashFloat = this.cashFloat;

        }

        void btnPrint_Click(object sender, EventArgs e) {

            if (txtbNameLeft.Text == "" || txtbNameLeft.Text == "Name" || txtbNameLeft.Text == null) {

                MessageBox.Show("Please enter your name befor printing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            } else if (combxRegister.Text == "Register#") {

                MessageBox.Show("Please Change the Register number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            } else {
                log.Info("printing");

                cu.btnPrint_Click(sender, e);
                Clear();

            }
            
        }

        private void btnClear_Click(object sender, EventArgs e) {
            
            Clear();
            log.Info("data cleared");
        }

        /// <summary>
        /// Clears all fields and resets the Cashup Object.
        /// </summary>
        private void Clear() {

            log.Info("clearing till data.");
            txtbR200.Text = "0";
            txtbR100.Text = "0";
            txtbR50.Text = "0";
            txtbR20.Text = "0";
            txtbR10.Text = "0";
            txtbR5.Text = "0";
            txtbR2.Text = "0";
            txtbR1.Text = "0";
            txtb50c.Text = "0";
            txtb20c.Text = "0";
            txtb10c.Text = "0";
            txtb5c.Text = "0";
            txtbxCheques.Text = "0";
            txtbxSkimmed.Text = "0";
            txtbTotal1.Text = "0";

            ClearDrop();

            txtbNameLeft.Text = "Name";
            combxRegister.Text = "Register#";
            txtbNumCheques.Text = "0";

            if (!(this.date.Value == DateTime.Today)) {
                this.dateTimePicker1.Value = DateTime.Today;
                this.dateTimePicker1.Value = DateTime.Today;
                this.dateTimePicker1.Value = DateTime.Today;
            }

            cu = new Cashup();
        }

        private void ClearDrop() {

            log.Info("Clearing Drop data");
            Update();
            txtbxR200Drop.Text = "0";
            txtbxR100Drop.Text = "0";
            txtbxR50Drop.Text = "0";
            txtbxR20Drop.Text = "0";
            txtbxR10Drop.Text = "0";
            txtbxR2Drop.Text = "0";
            txtbxR1Drop.Text = "0";
            txtbxc50Drop.Text = "0";
            txtbxc20Drop.Text = "0";
            txtbxc10Drop.Text = "0";
            txtbxc5Drop.Text = "0";

            cu.ClearDrop();

            Update();
        }

        private void txtbFloat_TextChanged(object sender, EventArgs e) {
            try {
                this.cashFloat = Decimal.Parse(txtbFloat.Text);

            } catch (FormatException) {
                txtbFloat.Text = "0";
            }

        }

        private void txtbxName_TextChanged(object sender, EventArgs e) {

        }

        /// <summary>
        /// starts interval Timer for Update Loop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e) {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = (10); // 10 milisecs
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            log.Debug("update tick started");


        }

        /// <summary>
        /// Update loop caller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e) {
            Update();
        }

    }
}

