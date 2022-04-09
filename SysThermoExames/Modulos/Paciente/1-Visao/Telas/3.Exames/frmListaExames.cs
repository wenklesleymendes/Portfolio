using MdPaciente._5_Dtos;
using System.Windows.Forms;

namespace MdPaciente._1_Visao
{
    public partial class frmListaExames : Form
    {
        private readonly DtoConfiguracaoPaciente _dto = new DtoConfiguracaoPaciente();
        public frmListaExames(DtoConfiguracaoPaciente dtoConfPaciente)
        {
            InitializeComponent();

            _dto = dtoConfPaciente;
        }
    }
}
