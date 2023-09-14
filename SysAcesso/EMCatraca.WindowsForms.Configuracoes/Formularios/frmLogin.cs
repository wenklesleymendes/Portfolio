using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using EMCatraca.WindowsForms.Configuracoes.Utilidades;
using System;
using System.Windows.Forms;

namespace EMCatraca.WindowsForms.Configuracoes.Formularios
{
    public partial class frmLogin : Form
    {
        private readonly IRepositorioOperador repositorioOperador = FabricaDeRepositorios.Instancia.CrieRepositorioOperador();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void BtnFecharLogin_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            cboOperadores.ValueMember = "Codigo";
            cboOperadores.DisplayMember = "Nome";

            cboOperadores.DataSource = repositorioOperador.ConsulteTodosOperadorAtivos();
        }

        private void BtnOkLogin_Click(object sender, EventArgs e)
        {
            Autentique((Operador)cboOperadores.SelectedItem, txtSenha.Text);
        }

        public void Autentique(Operador operador, string senhaInformada)
        {
            if (!ValidacaoDeSenhaOperador.SenhaInformadaEhValida(operador, senhaInformada))
            {
                MessageBox.Show("Usuário ou Senha inválido!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSenha.Text = "";
                cboOperadores.Text = "";
                cboOperadores.Focus();
            }
            else
            {
                SessaoDoUsuario.Instancia.OperadorLogado = operador;
                Hide();
                var frmShow = new frmPrincipal();
                frmShow.Closed += (s, args) => this.Close();
                frmShow.Show();
            }
        }

        private void TxtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
                Autentique((Operador)cboOperadores.SelectedItem, txtSenha.Text);
        }

        private void CboOperadores_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }

        private void CboOperadores_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtSenha.Select();
        }
    }
}
