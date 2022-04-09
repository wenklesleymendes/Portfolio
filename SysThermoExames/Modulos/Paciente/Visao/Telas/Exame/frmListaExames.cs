using MdPaciente.Visao.Telas.Exame;
using MdPaciente.Aplicacoes;
using MdPaciente.Dtos;
using System.Windows.Forms;

namespace MdPaciente.Visao
{
    public partial class frmListaExames : Form
    {
        private readonly DtoConfiguracao _dto = new DtoConfiguracao();
        private readonly Utilitario _utilitario = new Utilitario();
        //private readonly DtoConfiguracao _dto = new DtoConfiguracao();

        public frmListaExames(DtoConfiguracao dtoConf)
        {
            InitializeComponent();
            _dto = dtoConf;
        }

        private void pbAdd_Click(object sender, System.EventArgs e)
        {
            _utilitario.AbrirFormPanel(new frmCadastroExame(_dto), _dto.PnUm);
        }
    }
}
