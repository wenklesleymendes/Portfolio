using Formularios.Dtos;
using Formularios.Aplicacoes;
using Formularios.Telas._3_Login;
using Formularios.Telas._4_Login;
using System;
using System.Windows.Forms;

namespace Formularios.Telas._2_Principal
{
    public partial class frmMainOperador : Form
    {
        public frmMainOperador()
        {
            InitializeComponent();
        }

        private void frmMainPaciente_Load(object sender, EventArgs e)
        {
            var dto = new DtoConfiguracaoOperador()
            {
                PnUm = pnMainOperadorUm,
                PnDois= pnMainOperadorDois
            };

            var utilitario = new UtilitarioOperador(dto);
            utilitario.AbrirFormPanel(new frmCardOperador(dto), dto.PnUm);
            utilitario.AbrirFormPanel(new frmListaOperadores(dto), dto.PnDois);
        }
    }
}
