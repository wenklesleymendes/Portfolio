using MdPaciente.Aplicacoes;
using MdPaciente.Dtos;
using MdPaciente.Visao.Telas.Exame;
using System;
using System.Windows.Forms;

namespace MdPaciente.Visao.Telas._3.Exames
{
    public partial class frmCardExame : Form
    {
        private readonly Utilitario _utilitario = new Utilitario();

        private readonly DtoConfiguracao _dto = new DtoConfiguracao();
        public frmCardExame(DtoConfiguracao dto)
        {
            InitializeComponent();
        }

        private void pbAdd_Click(object sender, EventArgs e)
        {
            _utilitario.AbrirFormPanel(new frmCadastroExame(_dto), _dto.PnUm);
        }
    }
}
