using MdPaciente._2_Aplicacoes;
using MdPaciente._3_Dominio;
using MdPaciente._5_Dtos;
using System.Windows.Forms;

namespace MdPaciente._1_Visao
{
    public partial class ucPacienteCard : UserControl
    {
        private readonly Paciente _paciente = new Paciente();

        private readonly UtilitarioPaciente _utilitario = new UtilitarioPaciente();

        private readonly DtoConfiguracaoPaciente _dto = new DtoConfiguracaoPaciente();

        public ucPacienteCard(Paciente paciente, DtoConfiguracaoPaciente dto)
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
