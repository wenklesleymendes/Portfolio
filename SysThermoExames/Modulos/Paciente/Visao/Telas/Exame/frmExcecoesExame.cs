using MdPaciente.Aplicacoes;
using MdPaciente.Dtos;
using MdPaciente.Visao.Telas.Exame;
using System;
using System.Windows.Forms;

namespace MdPaciente.Visao.Telas.Exame
{
    public partial class frmExcecoesExame : Form
    {
        private readonly Utilitario _utilitario = new Utilitario();

        private readonly DtoConfiguracao _dto = new DtoConfiguracao();
        public frmExcecoesExame()
        {
            InitializeComponent();
        }

        public frmExcecoesExame(DtoConfiguracao dto, string message)
        {
            InitializeComponent();

            txtTextoInformacoes.Text = message;
            _dto = dto;
        }

        private void pbAdd_Click(object sender, EventArgs e)
        {
            _utilitario.AbrirFormPanel(new frmCadastroExame(_dto), _dto.PnUm);
        }
    }
}
