using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EasyInnerSDK.UI;
using EasyInnerSDK.UI.FrmSobre;
using EasyInnerSDK.Entity;
using System.Diagnostics;
using EasyInnerSDK.UI.FrmBIO;


namespace EasyInnerSDK
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void tmiInnerOnline_Click(object sender, EventArgs e)
        {
            FrmOnline frmOnline = new FrmOnline(this);
        }

        private void tmiInnerBio_Click(object sender, EventArgs e)
        {
            FrmBIO frmBio = new FrmBIO(this);
        }

        private void tmiSobre_Click(object sender, EventArgs e)
        {
            FrmSobre frmSobre = new FrmSobre(this);
        }
        private void tmiInnerOffLine_Click(object sender, EventArgs e)
        {
            FrmOffLine frmOffLine = new FrmOffLine(this);
        }

        private void tmiManualDesenvolvimento_Click(object sender, EventArgs e)
        {

        }

        private void tmiInnerBio6xx_Click(object sender, EventArgs e)
        {
            frmBio6xx Bio6xx = new frmBio6xx(this);
        }
    }
}