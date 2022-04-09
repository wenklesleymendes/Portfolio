using Formularios.Telas.Principal;
using Processos.Operadores;
using System;
using System.Windows.Forms;

namespace Formularios.Telas.Login
{
    public partial class frmLoginOperador : Form
    {
        frmMain formPrincipal { get; set; }
        private ProcessoLogin processoDeLogin = new ProcessoLogin();

        public frmLoginOperador(frmMain frm)
        {
            formPrincipal = frm;
            InitializeComponent();           
        }

        public frmLoginOperador()
        {
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            var login = txtLoginOperador.Text;
            var senha = txtOperadorSenha.Text;
            if (!PodeLogar(login, senha))
            {
                return;
            }

            try
            {
                var operadorLogado = processoDeLogin.RealizeLogin(login, senha);
                if (operadorLogado != null)
                {
                    formPrincipal.OperadorLogado = operadorLogado;
                    Hide();
                    formPrincipal.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro no processo de login: \n\n {ex.Message}");
            }

            MessageBox.Show("Login incorreto. Verifique as informações e tente novamente.");
            txtOperadorSenha.Clear();
        }

        private bool PodeLogar(string login, string senha)
        {
            var existeDadosLoginSenha = login != "" && senha != "";
            var dadosLogiSenhaEstaNoPadrao = senha.Length >= 5 && senha.Length > 2;
            var loginValido = existeDadosLoginSenha && dadosLogiSenhaEstaNoPadrao;
  
            if (EhAdministradorPrincipal || loginValido)
            {
                return true;
            }

            string mensagem = string.Empty;

            if (txtLoginOperador.Text == "")
            {
                mensagem = string.Concat(mensagem, "O campo login é obrigatório \n");
            }

            if (txtLoginOperador.Text.Length < 2)
            {
                mensagem = string.Concat(mensagem, "O campo login deve conter três caracteres ou mais\n");
            }

            if (txtOperadorSenha.Text == "")
            {
                mensagem = string.Concat(mensagem, "O campo senha é obrigatório \n");
            }

            if (txtOperadorSenha.Text.Length < 5)
            {
                mensagem = string.Concat(mensagem, "O campo senha deve conter cinco caracteres ou mais \n");
            }

            MessageBox.Show(mensagem);
            return false;
        }

        private bool EhAdministradorPrincipal => 
            processoDeLogin.EhAdministradorPrincipal(txtLoginOperador.Text, txtOperadorSenha.Text);

        private void txtLoginOperador_TextChanged(object sender, EventArgs e)
        {
            var txtLogin = (TextBox)sender;
            txtLogin.Text = "";
        }
    }
}
