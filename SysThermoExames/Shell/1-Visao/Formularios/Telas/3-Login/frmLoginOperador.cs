using Formularios.Telas._1_Principal;
using Processos.Operadores;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Formularios.Telas._4_Login
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

        #region Drag Form/ Mover Arrastrar Formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        #region Placeholder or WaterMark
        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtLoginOperador.Text == "Usuario")
            {
                txtLoginOperador.Text = "";
                txtOperadorSenha.ForeColor = Color.LightGray;
            }
        }

        private void txtLoginOperador_Leave(object sender, EventArgs e)
        {
            if (txtLoginOperador.Text == "")
            {
                txtLoginOperador.Text = "Operador";
                txtLoginOperador.ForeColor = Color.Silver;
            }
        }

        private void txtOperadorSenha_Enter(object sender, EventArgs e)
        {
            if (txtOperadorSenha.Text == "Contraseña")
            {
                txtOperadorSenha.Text = "";
                txtOperadorSenha.ForeColor = Color.LightGray;
                txtOperadorSenha.UseSystemPasswordChar = true;
            }
        }

        private void txtpass_Leave(object sender, EventArgs e)
        {
            if (txtOperadorSenha.Text == "")
            {
                txtOperadorSenha.Text = "Contraseña";
                txtOperadorSenha.ForeColor = Color.Silver;
                txtOperadorSenha.UseSystemPasswordChar = false;
            }
        }

        #endregion 

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            var login = txtLoginOperador.Text;
            var senha = txtOperadorSenha.Text;
            if (!ValideLogin())
            {
                return;
            }

            try
            {
                var operadorLogado = processoDeLogin.RealizeLogin(login, senha);
                if (operadorLogado != null)
                {
                    formPrincipal.OperadorLogado = operadorLogado;
                    //formPrincipal.AtualizaDadosOperador();
                    this.Hide();
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

        private bool ValideLogin()
        {
            if (txtLoginOperador.Text != "" && txtOperadorSenha.Text != "" && txtOperadorSenha.Text.Length >= 5 && txtLoginOperador.Text.Length > 2)
            {
                return true;
            }

            if (EhAdministradorPrincipal())
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

        private bool EhAdministradorPrincipal()
        {
            return processoDeLogin.EhAdministradorPrincipal(txtLoginOperador.Text, txtOperadorSenha.Text);
        }

        private void txtLoginOperador_TextChanged(object sender, EventArgs e)
        {
            var txtLogin = (TextBox)sender;

            txtLogin.Text = "";
        }

    }
}
