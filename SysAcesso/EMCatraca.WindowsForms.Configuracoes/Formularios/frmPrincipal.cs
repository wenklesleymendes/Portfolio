using System;
using System.Windows.Forms;

namespace EMCatraca.WindowsForms.Configuracoes.Formularios
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void TlsSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TlsConfiguracao_Click(object sender, EventArgs e)
        {
            var frmShow = new FrmConfiguraAcesso();
            frmShow.ShowDialog();
        }

        private void TlsAcesso_Click(object sender, EventArgs e)
        {
            var frmShow = new frmLiberacaoAcesso();
            frmShow.ShowDialog();
        }
    }
}
