﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RatGameEditor
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            ListRealms();
        }

        private void ListRealms()
        {

        }

        private void tsmiViewWorldRealm_Click(object sender, EventArgs e)
        {
            frmRealms frm = new frmRealms();
            frm.Show();
        }
    }
}
