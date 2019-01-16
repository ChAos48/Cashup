using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OOP_Cashup
{
    public partial class frmAbout : Form
    {
        public frmAbout() {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e) {
            rtxtbChangelog.Text = File.ReadAllText(System.Reflection.Assembly.GetExecutingAssembly().Location + "ChangeLog.txt");
        }
    }
}
