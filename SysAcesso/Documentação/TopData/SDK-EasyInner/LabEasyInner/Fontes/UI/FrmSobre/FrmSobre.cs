using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EasyInnerSDK.UI.FrmSobre
{
    public partial class FrmSobre : Form
    {
        static bool aberto = false;
        public FrmSobre(Form pai)
        {
            if (!aberto)
            {
                InitializeComponent();
                MdiParent = pai;
                aberto = true;
                Show();
            }
            else
            {
                Dispose();
            }
        }

        private void tbnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSobre_FormClosed(object sender, FormClosedEventArgs e)
        {
            aberto = false;
        }

        private void lblData_Click(object sender, EventArgs e)
        {

        }
    }
}