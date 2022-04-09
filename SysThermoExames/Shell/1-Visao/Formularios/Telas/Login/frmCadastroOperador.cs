using MdPaciente.Aplicacoes;
using ModelPrincipal;
using ModelPrincipal.Entidades;
using ModelPrincipal.Enumeradores;
using ModelPrincipal.Utilitarios;
using Processos.Operadores;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Formularios.Telas.Login
{
    public partial class frmCadastroOperador : Form
    {
        ProcessoOperadores _processoOperadores = new ProcessoOperadores();

        frmListaOperadores FormLista { get; set; }

        private readonly UtilitarioShell _utilitario = new UtilitarioShell();

        private readonly DtoConfigShell _dto = new DtoConfigShell();

        private readonly Operador _operador = new Operador();

        public frmCadastroOperador(Operador operador, DtoConfigShell dto)
        {
            InitializeComponent();
            _dto = dto;
            txtId.Text = operador.Codigo.ToString();
            txtNome.Text = operador.Nome.ToString();
            txtCpf.Text = operador.CPF.ToString();
            txtLogin.Text = operador.Login.ToString();
            if (EnumOperador.Administrador.Equals(operador.Grupo))
            {
                 ckbEhAdministrador.Checked = true;
            }
            else
            {
                ckbEhOperador.Checked = true;
            }
        }

        public frmCadastroOperador(DtoConfigShell dto)
        {
            InitializeComponent();
            _dto = dto;
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
            VoltarParaCard();
        }

        private void btnOperadorSalva_Click(object sender, EventArgs e)
        {
            if (!PodeContinua())
            {
                return;
            }

            var usuario = new Operador()
            {
                Nome = txtNome.Text,
                CPF = txtCpf.Text,
                Grupo = ObtenhaGrupoOperador(),
                Login = txtLogin.Text
            };

            if (txtId.Text != "")
            {
                usuario.Codigo = Int32.Parse(txtId.Text);
            }
            usuario.Senha = _processoOperadores.CriptografeSenha(txtSenha.Text);
            usuario.EhAtivo = true;

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
            VoltarParaCard();
        }

        private void VoltarParaCard()
        {
            LimpeFormulario();
            _utilitario.AbrirFormPanel(new frmCardOperador(_dto), _dto.PnCentral);
        }

        private void LimpeFormulario()
        {
            txtId.Clear();
            txtNome.Clear();
            txtLogin.Clear();
            txtSenha.Clear();
            txtCpf.Clear();
        }

        private EnumOperador ObtenhaGrupoOperador()
        {
            if (ckbEhAdministrador.Checked)
            {
                return EnumOperador.Administrador;
            }

            if (ckbEhOperador.Checked)
            {
                return EnumOperador.Operador;
            }

            return EnumOperador.SemGrupo;
        }

        private bool PodeContinua()
        {
            string mensagem = string.Empty;

            if (txtNome.Text == "")
            {
                mensagem = string.Concat(mensagem, "O campo nome é obrigatório.\n");
                MessageBox.Show(mensagem);
                return false;
            }

            if (txtNome.Text.Length < 2)
            {
                mensagem = string.Concat(mensagem, "O campo nome deve conter três caracteres ou mais.\n");
                MessageBox.Show(mensagem);
                return false;
            }
            if (txtCpf.Text == "")
            {
                mensagem = string.Concat(mensagem, "O campo CPF é obrigatório.\n");
                MessageBox.Show(mensagem);
                return false;
            }

            if (!CPF.ValideCPF(txtCpf.Text))
            {
                mensagem = string.Concat(mensagem, "O CPF informado não é válido.\n");
                MessageBox.Show(mensagem);
                return false;
            }

            if (txtLogin.Text == "")
            {
                mensagem = string.Concat(mensagem, "O campo login é obrigatório.\n");
                MessageBox.Show(mensagem);
                return false;
            }

            if (txtLogin.Text.Length < 2)
            {
                mensagem = string.Concat(mensagem, "O campo login deve conter três caracteres ou mais.\n");
                MessageBox.Show(mensagem);
                return false;
            }

            if (txtSenha.Text == "")
            {
                mensagem = string.Concat(mensagem, "O campo senha é obrigatório.\n");
                MessageBox.Show(mensagem);
                return false;
            }

            if (txtSenha.Text.Length < 5)
            {
                mensagem = string.Concat(mensagem, "O campo senha deve conter cinco caracteres ou mais.\n");
                MessageBox.Show(mensagem);
                return false;
            }

            if (textConfSenha.Text != txtSenha.Text)
            {
                mensagem = string.Concat(mensagem, "As senhas informadas são diferentes.\n");
                MessageBox.Show(mensagem);
                return false;
            }

            return true;
        }
    }
}
