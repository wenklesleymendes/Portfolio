using Formularios.Telas.Login;
using MdPaciente.Aplicacoes;
using ModelPrincipal;
using ModelPrincipal.Entidades;
using System.Windows.Forms;

namespace Formularios.Telas._8_Controles
{
    public partial class ucOperadorCard : UserControl
    {
        private readonly Operador _operador = new Operador();

        private readonly UtilitarioShell _utilitario = new UtilitarioShell();

        private readonly DtoConfigShell _dto = new DtoConfigShell();

        public bool selecionado = false;

        public ucOperadorCard(Operador operador, DtoConfigShell dto)
        {
            InitializeComponent();

            _dto = dto;
            _operador = operador;

            CarregueCardOperador();
        }

        public void CarregueCardOperador()
        {
            lbCadNome.Text = $"Nome: {_operador.Nome}";
            lbLogin.Text = $"Login: {_operador.Login}";
            lbGrupo.Text = $"Grupo: {_operador.Grupo}";
        }

        private void ucOperadorCard_DoubleClick(object sender, System.EventArgs e)
        {
            _utilitario.AbrirFormPanel(new frmCadastroOperador(_operador, _dto), _dto.PnCentral);
        }

        public Operador GetOperador()
        {
            return _operador;
        }

        private void ucOperadorCard_Click(object sender, System.EventArgs e)
        {
          //  if (_dto.ExisteCardSelecionadoPaciente)
          //  {
          //      _dto.ExisteCardSelecionadoPaciente = false;
          //      _dto.CodigoOperador = 0;
          //
          //      chbSelecionarOperador.Checked = _dto.ExisteCardSelecionadoPaciente;
          //      chbSelecionarOperador.Text = string.Empty;
          //
          //      return;
          //  }
          //
          //  _dto.ExisteCardSelecionadoPaciente = true;
          //  _dto.CodigoOperador = _operador.Codigo;
          //
          //  chbSelecionarOperador.Checked = _dto.ExisteCardSelecionadoPaciente;
          //  chbSelecionarOperador.Text = "Este paciente foi Selecionado!";
        }

        private void chbSelecionarOperador_CheckedChanged(object sender, System.EventArgs e)
        {
            if(chbSelecionarOperador.Checked )
            {
                selecionado = true;
                _dto.ExisteCardSelecionadoPaciente = true;

            }
            else
            {
                selecionado = false;
                _dto.ExisteCardSelecionadoPaciente = false;
            }
        }
    }
}
