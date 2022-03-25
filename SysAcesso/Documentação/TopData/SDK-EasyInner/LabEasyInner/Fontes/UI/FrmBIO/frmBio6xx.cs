using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyInnerSDK.DAO;
using EasyInnerSDK.Entity;
using EasyInnerSDK.Controle;

namespace EasyInnerSDK.UI.FrmBIO
{
    public partial class frmBio6xx : Form
    {
        private Inner InnerAtual;
        private FrmBioController6xx BioController6xx;
        private NitgenController ControlUSBLN;
        private LCControle ControlUSBLC;
        
        public frmBio6xx(frmMain pai)
        {
            InitializeComponent();
            this.MdiParent = pai;
            Show();
        }

        private void frmBio6xx_Load(object sender, EventArgs e)
        {
            cboModuloBio.SelectedIndex = 0;
            lblModuloBio.Text = cboModuloBio.SelectedIndex == 0 ? "Modulo bio LN" : "Modulo bio LC";
            lblNumeroInner.Text = "Número: " + txtNumInner.Value.ToString();
            CarregarDigitaisBD6xx();
            InicializarDispositivosUSB();
        }

        private void InicializarDispositivosUSB()
        {
            ControlUSBLC = new LCControle(this);
            ControlUSBLN = new NitgenController(null, this);
            if (ControlUSBLN.ConectarDLLLN())
            {
                cboDispositivos.Items.Add("Leitor LN");
                gpbCapturasLC.Visible = true;
                gpbConfHamster.Visible = false;
                btnCapturar.Enabled = true;
            }
            if (ControlUSBLC.InicializarDispositivo())
            {
                cboDispositivos.Items.Add("Leitor LC");
                gpbCapturasLC.Visible = false;
                gpbConfHamster.Visible = true;
                btnCapturar.Enabled = true;
            }
        }

        public delegate void AtualizarInfoTelaManut6xx(string msg);
        public void AtualizarInfoManutencao6xx(string mensagem)
        {
            lstManutencao6xx.Items.Add(mensagem);
            Application.DoEvents();
        }
        public delegate void LimparlstManut6xx();
        public void LimparListManut6xx()
        {
            lstManutencao6xx.Items.Clear();
            Application.DoEvents();
        }
        public delegate void ControlarbtnManutencao6xx(bool enable);
        public void ControlarBotoesManutencao6xx(bool Habilitar)
        {
            btnAtualizarLista6xx.Enabled = Habilitar;
            btnEnviarDigitais6xx.Enabled = Habilitar;
            btnReceberDigitais6xx.Enabled = Habilitar;
            btnExcluirDigitaisBD6xx.Enabled = Habilitar;
            btnExcluirDigitaisInner6xx.Enabled = Habilitar;
            btnReceberQuantidade6xx.Enabled = Habilitar;
            btnExcluirTodosUsuariosBio.Enabled = Habilitar;
            Application.DoEvents();
        }
        public delegate void AtualizarInfoCad(string msg);
        public void AtualizarInfoCadInner(string mensagem)
        {
            lstCadastroInner.Items.Add(mensagem);
            Application.DoEvents();
        }

        public delegate void LimparlstCadInner();
        public void LimparListCadInner()
        {
            lstCadastroInner.Items.Clear();
            Application.DoEvents();
        }
        public delegate void AtualizarCursorsMs(Cursor cs);
        public void AtualizarCursorsMouse(Cursor cursors)
        {
            this.Cursor = cursors;
            Application.DoEvents();
        }
        public delegate void LimparlstInfo();
        public void LimparListInfo()
        {
            lstInformacoes.Items.Clear();
            Application.DoEvents();
        }
        public delegate void AtualizarInfoConfig(string msg);
        public void AtualizarInfoConfiguracao(string mensagem)
        {
            lstInformacoes.Items.Add(mensagem);
            Application.DoEvents();
        }

        public delegate void AtualizarStCaptura(string msg);
        public void AtualizarStatusCaptura(string mensagem)
        {
            lblStatusCaptura.Text = mensagem;
        }

        public delegate void AtualizarlblStatusCap(string msg);
        public void AtualizarLabelStatusCap(string mensagem)
        {
            lblStatusCaptura.Text = mensagem;
            Application.DoEvents();
        }

        public delegate void AtualizarlblQualidadeDigital(string msg);
        public void AtualizarLabelQualidadeDigital(string mensagem)
        {
            lblValorQualidadeDigital.Text = mensagem;
            Application.DoEvents();
        }

        public delegate void AtualizarlblQualidadeImagem(string msg);
        public void AtualizarLabelQualidadeImagem(string mensagem)
        {
            lblValorQualidadeImagem.Text = mensagem;
            Application.DoEvents();
        }


        public void CarregarDigitaisBD6xx()
        {
            try
            {
                dgvDigitaisBD6xx.Rows.Clear();
                dgvDigitaisBD6xx.ColumnCount = 3;
                dgvDigitaisBD6xx.Columns[0].Name = "Usuários";
                dgvDigitaisBD6xx.Columns[1].Name = "Template01";
                dgvDigitaisBD6xx.Columns[2].Name = "Template02";

                DAOUsuariosBio AcessoTemplates = new DAOUsuariosBio();
                List<UsuarioBio> Templates = AcessoTemplates.ConsultarUsuariosBio(cboModuloBio.SelectedIndex);

                dgvDigitaisBD6xx.RowCount = Templates == null || Templates.Count == 0 ? 1 : Templates.Count;

                for (int index = 0; index < Templates.Count; index++)
                {
                    dgvDigitaisBD6xx[0, index].Value = Templates[index].Usuario;
                    dgvDigitaisBD6xx[1, index].Value = Templates[index].TemplateA;
                    dgvDigitaisBD6xx[2, index].Value = Templates[index].TemplateB;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
            }
        }
        public void PreencherGridUsuariosInner6xx(List<UsuarioBio> ListaUsuariosBio)
        {
            dgvDigitaisInner6xx.Rows.Clear();
            dgvDigitaisInner6xx.ColumnCount = 1;
            dgvDigitaisInner6xx.Columns[0].Name = "Usuários";
            for (int index = 0; index < ListaUsuariosBio.Count; index++)
            {
                dgvDigitaisInner6xx.Rows.Add(new string[] { ListaUsuariosBio[index].Usuario });
            }
            dgvDigitaisInner6xx.Sort(dgvDigitaisInner6xx.Columns[0], ListSortDirection.Ascending);
        }       

        private void CarregarPropriedadesInner()
        {
            InnerAtual = new Inner();
            InnerAtual.Numero = int.Parse(txtNumInner.Value.ToString());
            InnerAtual.QtdDigitos = 16;
            InnerAtual.PadraoCartao = 0;
            InnerAtual.TipoConexao = 2;
            InnerAtual.Porta = int.Parse(txtPorta.Value.ToString());
            InnerAtual.DuasDigitais = rdb2Digitais.Checked;
            InnerAtual.Bio16Digitos = true;
            InnerAtual.TipoComBio = cboModuloBio.SelectedIndex;
            InnerAtual.Verificacao = chkVerificacao.Checked == true ? (byte)1 : (byte)0;
            InnerAtual.Identificacao = chkIdentificacao.Checked == true ? (byte)1 : (byte)0;

            InnerAtual.TimeOutAjustes = Convert.ToInt32(updTimeoutIdent.Value);
            InnerAtual.NivelLFD = Convert.ToInt32(updNivel.Value);
            InnerAtual.DedoDuplicado = chkDedoDuplicado.Checked;
        }

        private List<string> getUsuarios(DataGridView dgv)
        {
            List<string> ListaExcluir = new List<string>();
            for (int index = 0; index < dgv.SelectedRows.Count; index++)
            {
                string usuario = dgv.SelectedRows[index].Cells[0].Value.ToString();
                ListaExcluir.Add(usuario);
            }
            return ListaExcluir;
        }
        private List<UsuarioBio> getUsuariosEnviar6xx()
        {
            List<UsuarioBio> ListaUsuariosBio = new List<UsuarioBio>();
            for (int index = 0; index < dgvDigitaisBD6xx.SelectedRows.Count; index++)
            {
                UsuarioBio usuariobio = new UsuarioBio();
                usuariobio.Usuario = dgvDigitaisBD6xx.SelectedRows[index].Cells[0].Value.ToString();
                usuariobio.TemplateA = dgvDigitaisBD6xx.SelectedRows[index].Cells[1].Value.ToString();
                usuariobio.TemplateB = dgvDigitaisBD6xx.SelectedRows[index].Cells[2].Value.ToString();
                ListaUsuariosBio.Add(usuariobio);
            }
            return ListaUsuariosBio;
        }

        private void btnReceberQuantidade6xx_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            BioController6xx.ReceberQuantidadeUsuarios();
        }

        private void btnAtualizarLista6xx_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            BioController6xx.ListarUsuariosBio();
        }

        private void btnExcluiDigitaisBD6xx_Click(object sender, EventArgs e)
        {
            List<string> ListaUsuariosExcluir = getUsuarios(dgvDigitaisBD6xx);
            BioController6xx = new FrmBioController6xx(this, null);
            BioController6xx.ExcluirUsuarioBD(ListaUsuariosExcluir, cboModuloBio.SelectedIndex);
        }

        private void btnEnviarDigitais6xx_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            List<UsuarioBio> ListaUsuariosEnviar = getUsuariosEnviar6xx();
            BioController6xx.EnviarDigitaisInner(ListaUsuariosEnviar);
        }

        private void cmdReceberModelo_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            BioController6xx.ReceberModeloBio();
        }

        private void cmdReceberVersao_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            BioController6xx.ReceberVersaoBio();
        }

        private void btnRecConfiguracoes_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            BioController6xx.ReceberConfiguracoesInner();
        }

        private void cboModuloBio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarDigitaisBD6xx();
            lblModuloBio.Text = cboModuloBio.SelectedIndex == 0 ? "Modulo bio LN" : "Modulo bio LC";
            if (cboModuloBio.SelectedIndex == 0)
            {
                updTimeoutIdent.Minimum = 10;
                updTimeoutIdent.Maximum = 255;
                updTimeoutIdent.Value = 70;
                lblNivel.Text = "Nível LFD";
                updNivel.Minimum = 0;
                updNivel.Maximum = 3;
                updNivel.Value = 0;
                chkDedoDuplicado.Visible = false;
            }
            else
            {
                updTimeoutIdent.Minimum = 0;
                updTimeoutIdent.Maximum = 60;
                updTimeoutIdent.Value = 5;
                lblNivel.Text = "Segurança Ident.:";
                updNivel.Minimum = 1;
                updNivel.Maximum = 5;
                updNivel.Value = 3;
                chkDedoDuplicado.Visible = true;
                chkDedoDuplicado.Checked = true;
            }
        }

        private void btnExcluirDigitaisInner6xx_Click(object sender, EventArgs e)
        {
            List<string> ListaUsuariosExcluir = getUsuarios(dgvDigitaisInner6xx);
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            BioController6xx.ExcluirUsuarioBioInner(ListaUsuariosExcluir);
        }

        private void btnExcluirTodosUsuariosBio_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            BioController6xx.ExcluirTodosUsuariosInner();
        }

        private void btnReceberDigitais6xx_Click(object sender, EventArgs e)
        {
            List<string> ListaUsuariosReceber = getUsuarios(dgvDigitaisInner6xx);
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            BioController6xx.ReceberDigitaisBio(ListaUsuariosReceber);
            CarregarDigitaisBD6xx();
        }

        private void txtNumInner_ValueChanged(object sender, EventArgs e)
        {
            lblNumeroInner.Text = "Número: " + txtNumInner.Value.ToString();
        }

        private void btnConfAjustesBio_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            BioController6xx.EnviarAjustesBiometricos();
        }

        private void btnReceberTemplateLeitor_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            BioController6xx.ReceberTemplateLeitor(txtCartaoCadInner.Text);
        }

        private void btnIdentificarUsuario_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            BioController6xx.IdentificarDigital();
        }

        private void btnVerificarDigitalBio_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            BioController6xx.VerificarDigital(txtCartaoCadInner.Text);
        }

        private void btnVerificarCadastroUsuario_Click(object sender, EventArgs e)
        {
            if (txtCartaoCadInner.Text == "")
            {
                MessageBox.Show("Digite o número cartão");
                txtCartaoCadInner.Focus();
                return;
            }
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            BioController6xx.VerificarCadastroBio(txtCartaoCadInner.Text);
        }

        private void btnConfigurarIdentificacaoVerificacao_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            BioController6xx.ConfigurarIdentificacaoVerificacao();
        }

        private void cboDispositivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDispositivos.Text == "Leitor LN")
            {
                gpbCapturasLC.Visible = false;
                gpbConfHamster.Visible = true;
                btnCapturar.Enabled = true;
                cboModuloBio.SelectedIndex = 0;
            }
            else
            {
                gpbCapturasLC.Visible = true;
                gpbConfHamster.Visible = false;
                cboModuloBio.SelectedIndex = 1;
            }
        }

        private void btnCapturar_Click(object sender, EventArgs e)
        {
            UsuarioBio UserBio = new UsuarioBio();
            BioController6xx = new FrmBioController6xx(this, InnerAtual);
            if (txtCartaoCap.Text == "")
            {
                lblStatusCaptura.Text = "Coloque o número do cartão";
                return;
            }
            DAOUsuariosBio AcessoTemplates = new DAOUsuariosBio();
            if (AcessoTemplates.ExisteUsuarioBio(txtCartaoCap.Text, cboModuloBio.SelectedIndex) == true)
            {
                lblStatusCaptura.Text = "Usuário já cadastrado";
                return;
            }
            
            if (cboDispositivos.Text == "Leitor LN")
            {
                MessageBox.Show("Capturar 1 digital", "Capturar digital pelo leitor USB");

                UserBio.TemplateA = BioController6xx.CapturarHasmter();
                if (UserBio.TemplateA != "")
                {
                    MessageBox.Show("Capturar 2 digital", "Capturar digital pelo leitor USB");
                    UserBio.TemplateB = BioController6xx.CapturarHasmter();
                    if (UserBio.TemplateB != "")
                    {
                        UserBio.Usuario = txtCartaoCap.Text;
                        AcessoTemplates.InserirTemplateBD(UserBio, cboModuloBio.SelectedIndex);
                        MessageBox.Show("Usuário cadastrado", "Capturar digital pelo leitor USB");
                    }
                }
            }
            else
            {
                MessageBox.Show("Capturar 1 digital", "Capturar digital pelo leitor USB");
                UserBio.TemplateA = ControlUSBLC.CapturarTemplate();
                if (UserBio.TemplateA != "")
                {
                    MessageBox.Show("Capturar 2 digital", "Capturar digital pelo leitor USB");
                    UserBio.TemplateB = ControlUSBLC.CapturarTemplate();
                    if (UserBio.TemplateB != "")
                    {
                        UserBio.Usuario = txtCartaoCap.Text;
                        AcessoTemplates.InserirTemplateBD(UserBio, cboModuloBio.SelectedIndex);
                        MessageBox.Show("Usuário cadastrado", "Capturar digital pelo leitor USB");
                    }
                }
            }
        }

        private void tcbVerify_Scroll(object sender, EventArgs e)
        {
            lblValorVerify.Text = tcbVerify.Value.ToString();
        }

        private void tcbQualidadeDigital_Scroll(object sender, EventArgs e)
        {
            lblValorDigital.Text = tcbQualidadeDigital.Value.ToString();
        }
    }
}
