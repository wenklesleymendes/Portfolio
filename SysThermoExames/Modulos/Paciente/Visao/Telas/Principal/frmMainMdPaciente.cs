using MdPaciente.Aplicacoes;
using MdPaciente.Dtos;
using System;
using System.Windows.Forms;

namespace MdPaciente.Visao
{
    public partial class frmMainMdPaciente : Form
    {
        public frmMainMdPaciente()
        {
            InitializeComponent();
        }

        private void frmMainPaciente_Load(object sender, EventArgs e)
        {
            var dto = new DtoConfiguracao()
            {
                PnUm = pnMainPacienteUm,
                PnDois= pnMainPacienteDois
            };

            var utilitario = new Utilitario(dto);
            utilitario.AbrirFormPanel(new frmCard(dto), dto.PnUm);
            utilitario.AbrirFormPanel(new frmListaExames(dto), dto.PnDois);
        }
    }
}
