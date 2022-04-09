using MdPaciente._2_Aplicacoes;
using MdPaciente._5_Dtos;
using System;
using System.Windows.Forms;

namespace MdPaciente._1_Visao
{
    public partial class frmMainPaciente : Form
    {
        public frmMainPaciente()
        {
            InitializeComponent();
        }

        private void frmMainPaciente_Load(object sender, EventArgs e)
        {
            var dto = new DtoConfiguracaoPaciente()
            {
                PnUm = pnMainPacienteUm,
                PnDois= pnMainPacienteDois
            };

            var utilitario = new UtilitarioPaciente(dto);
            utilitario.AbrirFormPanel(new frmCard(dto), dto.PnUm);
            utilitario.AbrirFormPanel(new frmListaExames(dto), dto.PnDois);
        }
    }
}
