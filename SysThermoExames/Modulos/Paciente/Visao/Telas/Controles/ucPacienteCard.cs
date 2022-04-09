using MdPaciente.Aplicacoes;
using MdPaciente.Dominio;
using MdPaciente.Dtos;
using System.Windows.Forms;

namespace MdPaciente.Visao
{
    public partial class ucPacienteCard : UserControl
    {
        private readonly Paciente _paciente = new Paciente();

        private readonly Utilitario _utilitario = new Utilitario();

        private readonly DtoConfiguracao _dto = new DtoConfiguracao();

        public ucPacienteCard(Paciente paciente, DtoConfiguracao dto)
        {
            InitializeComponent();

            _paciente = paciente;
            _dto = dto;

            CarregueCardPaciente();
            BackColor = System.Drawing.Color.White;
        }

        public void CarregueCardPaciente()
        {
            lbCadNome.Text = $"Nome: {_paciente.Nome} " +
                             $"{_paciente.NomeMeio} " +
                             $"{_paciente.Sobrenome}";

            lbNascimento.Text = $"Nascimento: {_paciente.Nascimento}";
            lbSobrenome.Text = $"Whatshapp: {_paciente.Whatshapp}";
        }

        private void ucPacienteCard_DoubleClick(object sender, System.EventArgs e)
        {
            _utilitario.AbrirFormPanel(new frmCadastro(_paciente, _dto), _dto.PnUm);
        }

        private void ucPacienteCard_Click(object sender, System.EventArgs e)
        {
            if (_dto.ExisteCardSelecionadoPaciente)
            {
                _dto.ExisteCardSelecionadoPaciente = false;
                _dto.CodigoPaciente = 0;

                chbSelecionarPaciente.Checked = _dto.ExisteCardSelecionadoPaciente;
                chbSelecionarPaciente.Text = string.Empty;

                return;
            }
            
            _dto.ExisteCardSelecionadoPaciente = true;
            _dto.CodigoPaciente = _paciente.Codigo;

            chbSelecionarPaciente.Checked = _dto.ExisteCardSelecionadoPaciente;
            chbSelecionarPaciente.Text = "Este paciente foi Selecionado!";
        }
    }
}
