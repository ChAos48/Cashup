using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OOP_Cashup {
    public partial class frmPrintPreview: Form {
        public frmPrintPreview() {
            InitializeComponent();
        }

        private void frmPrintPreview_Load(object sender, EventArgs e) {

            this.reportViewer1.RefreshReport();
        }
    }
}
