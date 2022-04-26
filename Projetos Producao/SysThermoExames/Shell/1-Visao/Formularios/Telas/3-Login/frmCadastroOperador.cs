using ModelPrincipal._1_Entidades;
using ModelPrincipal._3_Utilitarios;
using Processos.Operadores;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Formularios.Telas._4_Login
{
    public partial class frmCadastroOperador : Form
    {
        ProcessoOperadores _processoOperadores = new ProcessoOperadores();
        frmListaOperadores FormLista { get; set; }
        public frmCadastroOperador(frmListaOperadores form)
        {
            InitializeComponent();
            FormLista = form;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnOperadorCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOperadorSalva_Click(object sender, EventArgs e)
        {
            if (!ValideCadastro())
            {
                return;
            }

            var usuario = new Operador()
            {
                Nome = txtNome.Text,
                //CPF = txtCpf.Text,
                EhAdministrador = ckbEhAdministrador.Checked,
                Login = txtLogin.Text
            };

            usuario.Senha = _processoOperadores.CriptografeSenha(txtSenha.Text);

            try
            {
                _processoOperadores.CadastreNovoOperador(usuario);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu o seguinte erro ao criar um novo usuário:\n" + ex.Message);
                return;
            }

            MessageBox.Show("Usuário cadastrado com sucesso");
            FormLista.AtualizeGrid();
            this.Close();
        }

        private bool ValideCadastro()
        {
            if (txtNome.Text != "" &&
                txtNome.Text.Length > 2 &&
                txtCpf.Text.Length == 11 &&
                txtCpf.Text != "" &&
                txtSenha.Text != "" &&
                txtSenha.Text.Length > 4 &&
                textConfSenha.Text != "" &&
                textConfSenha.Text.Length >= 5 &&
                txtLogin.Text != "" &&
                txtLogin.Text.Length > 2)
            {
                return true;
            }

            string mensagem = string.Empty;

            if (txtNome.Text == "")
            {
                mensagem = string.Concat(mensagem, "O campo nome é obrigatório.\n");
            }

            if (txtNome.Text.Length < 2)
            {
                mensagem = string.Concat(mensagem, "O campo nome deve conter três caracteres ou mais.\n");
            }
            if (txtCpf.Text == "")
            {
                mensagem = string.Concat(mensagem, "O campo CPF é obrigatório.\n");
            }

            if (!CPF.ValideCPF(txtCpf.Text))
            {
                mensagem = string.Concat(mensagem, "O CPF informado não é válido.\n");
            }

            if (txtLogin.Text == "")
            {
                mensagem = string.Concat(mensagem, "O campo login é obrigatório.\n");
            }

            if (txtLogin.Text.Length < 2)
            {
                mensagem = string.Concat(mensagem, "O campo login deve conter três caracteres ou mais.\n");
            }

            if (txtSenha.Text == "")
            {
                mensagem = string.Concat(mensagem, "O campo senha é obrigatório.\n");
            }

            if (txtSenha.Text.Length < 5)
            {
                mensagem = string.Concat(mensagem, "O campo senha deve conter cinco caracteres ou mais.\n");
            }

            if (textConfSenha.Text != txtSenha.Text)
            {
                mensagem = string.Concat(mensagem, "As senhas informadas são diferentes.\n");
            }

            MessageBox.Show(mensagem);
            return false;
        }
    }
}
