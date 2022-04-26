using MdPaciente.Aplicacoes;
using MdPaciente.Dominio;
using MdPaciente.Dtos;
using ModelPrincipal.Enumeradores;
using System;
using System.Windows.Forms;

namespace MdPaciente.Visao
{
    public partial class frmCadastro : Form
    {
        private ProcessoPaciente MdPaciente = new ProcessoPaciente();

        private readonly Utilitario _utilitario = new Utilitario();

        private readonly DtoConfiguracao _dto = new DtoConfiguracao();

        private bool ModoEdicao = false;

        private Paciente PacienteAtual { get; set; }

        public frmCadastro(Paciente paciente = null)
        {
            InitializeComponent();

            if (paciente != null)
            {
                CarregaDadosPacienteTela(paciente);
                ModoEdicao = true;
                PacienteAtual = paciente;
            }
        }

        public frmCadastro(Paciente paciente, DtoConfiguracao dtoConfiguracao)
        {
            InitializeComponent();

            _dto = dtoConfiguracao;

            if (paciente != null)
            {
                CarregaDadosPacienteTela(paciente);
                ModoEdicao = true;
                PacienteAtual = paciente;
            }
        }

        public frmCadastro(DtoConfiguracao dtoConfiguracao)
        {
            InitializeComponent();

            _dto = dtoConfiguracao;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ModoEdicao)
            {
                EditePaciente();
            }
            else
            {
                AdicionePaciente();
            }

            VoltarParaCard();
        }

        private void VoltarParaCard()
        {
            LimpeFormulario();
            Close();

            _utilitario.AbrirFormPanel(new frmCard(_dto), _dto.PnUm);
        }

        private void AdicionePaciente()
        {
            try
            {
                var paciente = ObtenhaDadosTela();
                MdPaciente.NovoPaciente(paciente);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu o seguinte erro ao cadastrar paciente:\n" + ex.Message);
                return;
            }

            MessageBox.Show("Paciente Cadastrado com sucesso!");
        }

        private void EditePaciente()
        {
            try
            {
                var pacienteAtualizado = ObtenhaDadosTela();
                pacienteAtualizado.Codigo = PacienteAtual.Codigo;
                MdPaciente.Atualize(pacienteAtualizado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu o seguinte erro ao atualizar paciente:\n" + ex.Message);
                return;
            }

            MessageBox.Show("Paciente atualizado com sucesso!");
            ModoEdicao = false;
        }

        private Paciente ObtenhaDadosTela()
        {
            var enums = new ProcessadorEnum();
            var sexo = enums.ObtenhaIndexSexo(cbSexo.SelectedIndex);
            var etnia = enums.ObtenhaIndexEtnia(cbEtnia.SelectedIndex);

            Paciente paciente = new Paciente()
            {
                Nome = txtNome.Text,
                NomeMeio = txtNomeMeio.Text,
                Sobrenome = txtSobrenome.Text,
                Whatshapp = txtWhatshapp.Text,
                Nascimento = dtpDataNascimento.Text,
                CodigoSexo = sexo,
                CodigoEtnia = etnia
            };

            return paciente;
        }
        
        private void CarregaDadosPacienteTela(Paciente paciente)
        {
            txtNome.Text = paciente.Nome;
            txtNomeMeio.Text = paciente.NomeMeio;
            txtSobrenome.Text = paciente.Sobrenome;
            cbSexo.SelectedIndex = paciente.CodigoSexo;
            cbEtnia.SelectedIndex = paciente.CodigoEtnia;
            dtpDataNascimento.Text = paciente.Nascimento;
            txtWhatshapp.Text = paciente.Whatshapp;
        }

        private void LimpeFormulario()
        {
            txtNome.Clear();
            txtNome.Clear();
            txtNomeMeio.Clear();
            txtSobrenome.Clear();
            dtpDataNascimento.Text = DateTime.Now.ToShortDateString();
            txtWhatshapp.Clear();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpeFormulario();
            this.Close();

        }

        private void btnOperadorCancelar_Click(object sender, EventArgs e)
        {
            VoltarParaCard();
        }
    }
}
