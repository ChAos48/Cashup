﻿using System;
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
            rtxtbChangelog.Text = File.ReadAllText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\ChangeLog.txt");
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void rectangleShape1_Click(object sender, EventArgs e) {

        }
    }
}
