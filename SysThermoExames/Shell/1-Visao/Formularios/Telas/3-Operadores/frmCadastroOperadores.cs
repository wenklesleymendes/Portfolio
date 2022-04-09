using Formularios.Telas._3_Componentes;
using MaterialSkin.Controls;
using ModelPrincipal._1_Entidades;
using ModelPrincipal._3_Utilitarios;
using Processos.Operadores;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Formularios.Telas._2_Operadores
{
    public partial class frmCadastroOperador : MaterialForm
    {
        private ProcessoOperadores cadastroDeOperadores = new ProcessoOperadores();
        private bool EhPrimeiroCadastro { get; set; }
        private bool EhNovoCadastro { get; set; }
        private Operador operadorModificado { get; set; }

        public frmCadastroOperador(Operador operador)
        {
            operadorModificado = operador;
            
            EhNovoCadastro = false;
            
            EhPrimeiroCadastro = false;

            InitializeComponent();

            Temas.InicializeTemaPadrao(this);

            AjusteControles();
        }

        public frmCadastroOperador(bool primeiroCadastro = false)
        {
            EhPrimeiroCadastro = primeiroCadastro;

            InitializeComponent();

            Temas.InicializeTemaPadrao(this);

            AjusteControles();            
        }

        private void AjusteControles()
        {
            if(EhPrimeiroCadastro)
            {
                EhNovoCadastro = true;
                chkboxAdministrador.Enabled = false;
                chkboxAdministrador.Checked = true;
            }

            if(operadorModificado != null)
            {
                MostreDadosOperador();
            }
        }

        private void MostreDadosOperador()
        {
            txtboxNome.Text = operadorModificado.Nome;
            //mskboxCPF.Text = operadorModificado.CPF;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if(!ValideCadastro())
            {
                return;
            }

            var usuario = new Operador() 
            {
                Nome = txtboxNome.Text,
                //CPF = mskboxCPF.Text,
                EhAdministrador = chkboxAdministrador.Checked,
                Login = txtboxLogin.Text
            };

            usuario.Senha = cadastroDeOperadores.CriptografeSenha(txtboxSenha.Text);

            try
            {
                cadastroDeOperadores.CadastreNovoOperador(usuario);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu o seguinte erro ao criar um novo usuário:\n" + ex.Message);
                return;
            }

            MessageBox.Show("Usuário cadastrado com sucesso");
        }

        private bool ValideCadastro()
        {            
            if (txtboxNome.Text != "" &&
                txtboxNome.Text.Length > 2 && 
                mskboxCPF.Text.Length == 11 &&
                txtboxLogin.Text != "" && 
                txtboxSenha.Text != "" && 
                txtboxSenha.Text.Length > 5 && 
                txtboxLogin.Text.Length > 2)
            {
                return true;
            }

            string mensagem = string.Empty;

            if (txtboxNome.Text == "")
            {
                mensagem = string.Concat(mensagem, "O campo nome é obrigatório.\n");
            }

            if (txtboxNome.Text.Length < 2)
            {
                mensagem = string.Concat(mensagem, "O campo nome deve conter três caracteres ou mais.\n");
            }
            if (mskboxCPF.Text == "")
            {
                mensagem = string.Concat(mensagem, "O campo CPF é obrigatório.\n");
            }

            if (!CPF.ValideCPF(mskboxCPF.Text))
            {
                mensagem = string.Concat(mensagem, "O CPF informado não é válido.\n");
            }

            if (txtboxLogin.Text == "")
            {
                mensagem = string.Concat(mensagem, "O campo login é obrigatório.\n");
            }

            if (txtboxLogin.Text.Length < 2)
            {
                mensagem = string.Concat(mensagem, "O campo login deve conter três caracteres ou mais\n");
            }

            if (txtboxSenha.Text == "")
            {
                mensagem = string.Concat(mensagem, "O campo senha é obrigatório \n");
            }

            if (txtboxSenha.Text.Length < 5)
            {
                mensagem = string.Concat(mensagem, "O campo senha deve conter cinco caracteres ou mais \n");
            }

            MessageBox.Show(mensagem);
            return false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!EhPrimeiroCadastro)
            {
                txtboxNome.Clear();
                mskboxCPF.Clear();
                txtboxSenha.Clear();
                this.Close();
                return;
            }

            DialogResult resultado = MessageBox.Show("Não é possível iniciar a aplicação sem cadastrar um novo administrador. Deseja continuar? A aplicação será fechada.", "Administrador não cadastrado", MessageBoxButtons.OKCancel);
            if(resultado.Equals(DialogResult.Cancel))
            {
                return;
            }

            Application.Exit();
        }

        private void frmCadastroOperador_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
