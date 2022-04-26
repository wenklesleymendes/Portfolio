using System;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

using EasyInnerSDK.Entity;
using System.Threading;

using NITGEN.SDK.NBioBSP;
using ExemploOnline.Entity;
using EasyInnerSDK.DAO;
using Microsoft.VisualBasic;

namespace EasyInnerSDK.UI.FrmBIO
{
    public partial class FrmBIO : Form
    {

        #region Propriedades

        #region Timeout
        private int timeout;
        public int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        #endregion

        #region Templates
        List<UsuarioBio> Templates;
        #endregion

        private FrmBIOController BioController;
        private Inner InnerAtual;
        private UpdatePropriedadeTelaBio UpFrmBio;
        private NitgenController ControlLN;
        
        #endregion

        #region Métodos

        #region Métodos Internos da Form

        #region FrmBIO
        static bool aberto = false;
        public FrmBIO(Form pai)
        {
            if (!aberto)
            {
                InitializeComponent();
                InicializaCombos();

                MdiParent = pai;
                aberto = true;
                Show();
            }
            else
            {
                Dispose();
            }
        }
        #endregion

        #endregion

        #region InicializaCombos
        private void InicializaCombos()
        {
            this.cboTipoConexao.Items.Add("Serial");
            this.cboTipoConexao.Items.Add("TCP/IP porta variável");
            this.cboTipoConexao.Items.Add("TCP/IP porta fixa");
            this.cboTipoConexao.SelectedIndex = 2;

            this.cboTipoConexaoOnline.Items.Add("Serial");
            this.cboTipoConexaoOnline.Items.Add("TCP/IP porta variável");
            this.cboTipoConexaoOnline.Items.Add("TCP/IP porta fixa");
            this.cboTipoConexaoOnline.SelectedIndex = 2;

            this.cboPadraoCartao.Items.Add("Topdata");
            this.cboPadraoCartao.Items.Add("Livre");
            this.cboPadraoCartao.SelectedIndex = 1;

            this.cboPadraoCartaoOnline.Items.Add("Topdata");
            this.cboPadraoCartaoOnline.Items.Add("Livre");
            this.cboPadraoCartaoOnline.SelectedIndex = 1;

            this.cboTipoLeitor.Items.Add("Código de Barras");
            this.cboTipoLeitor.Items.Add("Magnético");
            this.cboTipoLeitor.Items.Add("Prox. Abatrack/Smart Card");
            this.cboTipoLeitor.Items.Add("Prox. Wiegand/Smart Card");
            this.cboTipoLeitor.Items.Add("Prox. Smart Card Serial");
            this.cboTipoLeitor.Items.Add("Código de barras serial");
            this.cboTipoLeitor.Items.Add("Prox. Wiegand FC Sem Zero");
            this.cboTipoLeitor.SelectedIndex = 0;
        }
        #endregion

        #endregion

        public delegate void AtualizarLblQualidadeImagem(int Quality);
        public void AtualizarLabelQualidadeImagem(int Quality)
        {
            lblValorQualidadeImagem.Text = Quality.ToString();
            Application.DoEvents();
        }

        public delegate void AtualizarInfoTelaManut(string msg);
        public void AtualizarInfoManutencao(string mensagem)
        {
            lstManutencao.Items.Add(mensagem);
            Application.DoEvents();
        }
        public delegate void LimparlstManut();
        public void LimparListManut()
        {
            lstManutencao.Items.Clear();
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

        public delegate void AtualizarCursorsMs(Cursor cs);
        public void AtualizarCursorsMouse(Cursor cursors)
        {
            this.Cursor = cursors;
            Application.DoEvents();
        }

        public delegate void AtualizarEstadobtnCaptura(bool est);
        public void AtualizarEstadoButtonCaptura(bool estado)
        {
            this.btnCapturar.Enabled = estado;
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

        public delegate void ControlarbtnManutencao(bool enable);
        public void ControlarBotoesManutencao(bool Habilitar)
        {
            btnAtualizarLista.Enabled = Habilitar;
            btnEnviarDigitais.Enabled = Habilitar;
            btnReceberDigitais.Enabled = Habilitar;
            btnExcluirBD.Enabled = Habilitar;
            btnExcluirInner.Enabled = Habilitar;
            btnReceberQtdeUsuarios.Enabled = Habilitar;
            Application.DoEvents();
        }

        public void PreencherGridUsuariosInner(List<UsuarioBio> ListaUsuariosBio)
        {
            dgvDigitaisInner.ColumnCount = 1;
            dgvDigitaisInner.Columns[0].Name = "Usuários";
            for (int index = 0; index < ListaUsuariosBio.Count; index++)
            {
                dgvDigitaisInner.Rows.Add(new string[] {ListaUsuariosBio[index].Usuario});
            }
            dgvDigitaisInner.Sort(dgvDigitaisInner.Columns[0], ListSortDirection.Ascending);
        }
        
        #region Eventos

        #region cmdCfgInner_Click
        private void cmdCfgInner_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController = new FrmBIOController(this, InnerAtual, null);
            BioController.ConfigurarInner();
        }
        #endregion

        #region cmdQuantidadeUsrBio_Click
        private void cmdQuantidadeUsrBio_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController = new FrmBIOController(this, InnerAtual, null);
            BioController.ReceberQtdUsuariosBIO();
        }

        #endregion

        #region cmdReceberVersao_Click
        private void cmdReceberVersao_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController = new FrmBIOController(this, InnerAtual, null);
            BioController.ReceberVersaoBIO();
        }
        #endregion

        #region btnEnviarListaSemDigital_Click
        private void btnEnviarListaSemDigital_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController = new FrmBIOController(this, InnerAtual, null);
            List<string> ListaUsuario = getUsuariosSemDigital();
            BioController.EnviarListaUsuariosSemDigitail(ListaUsuario);
        }

        private List<string> getUsuariosSemDigital()
        {
            List<string> Lista = new List<string>();
            for (int index = 0; index < dgvUsuariosSD.Rows.Count; index++)
            {
                string usuario = dgvUsuariosSD.Rows[index].Cells[0].Value.ToString();
                Lista.Add(usuario);
            }
            return Lista;
        }
        #endregion

        private void FrmBIO_Load(object sender, EventArgs e)
        {
            try
            {
                tcInnerBIO.SelectedIndex = 0;                
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            lblNumeroInner.Text = "Número: " + txtNumInner.Value.ToString();
            CarregarDigitaisBD();
            CarregarUsuariosSD();
            ControlLN = new NitgenController(this, null);
            if (ControlLN.ConectarDLLLN() == true)
            {
                cboDispositivos.Items.Add("Leitor LN");
            }
        }

        private void CarregarPropriedadesInner()
        {
            InnerAtual = new Inner();
            InnerAtual.Numero = int.Parse(txtNumInner.Value.ToString());
            InnerAtual.QtdDigitos = int.Parse(txtQtdeDigitos.Value.ToString());
            InnerAtual.TipoConexao = cboTipoConexao.SelectedIndex;
            InnerAtual.TipoLeitor = cboTipoLeitor.SelectedIndex;
            InnerAtual.PadraoCartao = cboPadraoCartao.SelectedIndex;
            InnerAtual.Porta = int.Parse(txtPorta.Value.ToString());
            InnerAtual.Verificacao = Convert.ToByte(chkHabIdentificacao.Checked);
            InnerAtual.Identificacao = Convert.ToByte(chkHabVerificacao.Checked);
            InnerAtual.TimeOutAjustes = Convert.ToInt32(updTimeoutIdent.Value);
            InnerAtual.NivelLFD = Convert.ToInt32(updNivelLFD.Value);
            InnerAtual.DuasDigitais = rdbDigital2.Checked;
            InnerAtual.Bio16Digitos = chkBio16Digitos.Checked;
        }

        #endregion

        private void cboTipoConexao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoConexao.SelectedIndex == 0)
            {
                txtPorta.Value = 1;
            }
            else
            {
                txtPorta.Value = 3570;
            }
        }

        private void cboTipoConexaoOnline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoConexaoOnline.SelectedIndex == 0)
            {
                txtPortaOnline.Value = 1;
            }
            else
            {
                txtPortaOnline.Value = 3570;
            }
        }

        public void CarregarDigitaisBD()
        {
            try
            {
                dgvDigitaisBD.Rows.Clear();
                dgvDigitaisBD.ColumnCount = 3;
                dgvDigitaisBD.Columns[0].Name = "Usuários";
                dgvDigitaisBD.Columns[1].Name = "Template01";
                dgvDigitaisBD.Columns[2].Name = "Template02";

                DAOUsuariosBio AcessoTemplates = new DAOUsuariosBio();
                Templates = AcessoTemplates.ConsultarUsuariosBio(0);

                dgvDigitaisBD.RowCount = Templates == null || Templates.Count == 0 ? 1 : Templates.Count;

                for (int index = 0; index < Templates.Count; index++)
                {
                    dgvDigitaisBD[0, index].Value = Templates[index].Usuario;
                    dgvDigitaisBD[1, index].Value = Templates[index].TemplateA;
                    dgvDigitaisBD[2, index].Value = Templates[index].TemplateB;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
            }

        }

        public void CarregarUsuariosSD()
        {
            try
            {
                dgvUsuariosSD.Rows.Clear();
                dgvUsuariosSD.ColumnCount = 1;
                dgvUsuariosSD.Columns[0].Name = "Usuários";

                DAOUsuariosBio AcessoSD = new DAOUsuariosBio();
                List<UsuarioSemDigital> UserSD = AcessoSD.ConsultarUsuariosSD();
                dgvUsuariosSD.RowCount = UserSD.Count;

                if (UserSD.Count == 0)
                {
                    MessageBox.Show("Não há usuários sem digital!");
                    return;
                }

                for (int index = 0; index < UserSD.Count; index++)
                {
                    dgvUsuariosSD[0, index].Value = UserSD[index].Usuario;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void cboDispositivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDispositivos.SelectedIndex >= 0)
                btnIniciar.Enabled = true;
            else
                btnIniciar.Enabled = false;
        }

        private void FrmBIO_FormClosed(object sender, FormClosedEventArgs e)
        {
            aberto = false;
        }

        private void cboDispositivos_Click(object sender, EventArgs e)
        {
            if (cboDispositivos.Items.Count == 0)
            {
               MessageBox.Show("Hamster não foi conectado ou o driver não foi instalado, para maiores detalhes acesse o arquivo \n leiame contido na instalação do SDK (Menu Iniciar/Programas/SDK EasyInner/Manuais)");
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (cboDispositivos.SelectedIndex == -1 && cboDispositivos.Items.Count != 0)
            {
                MessageBox.Show("Não foi selecionado nenhum dispositivo");
                return;
            }
            CarregarPropriedadesInner();
            CarregarPropriedadesDisplay();
            BioController = new FrmBIOController(this, InnerAtual, UpFrmBio);
            BioController.AbrirDispositivo();
        }

        private void btnConfAjustesBio_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController = new FrmBIOController(this, InnerAtual, null);
            BioController.ConfigurarAjustesBio();
        }

        private void txtNumInner_ValueChanged(object sender, EventArgs e)
        {
            lblNumeroInner.Text =  "Número: " + txtNumInner.Value.ToString();
        }

        private void btnEnviarDigitais_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController = new FrmBIOController(this, InnerAtual, null);
            List<UsuarioBio> ListaUsuariosEnviar = getUsuariosEnviar();
            BioController.EnviarDigitaisInner(ListaUsuariosEnviar);
        }

        private List<UsuarioBio> getUsuariosEnviar()
        {
            List<UsuarioBio> ListaUsuariosBio = new List<UsuarioBio>();
            for (int index = 0; index < dgvDigitaisBD.SelectedRows.Count; index++)
            {
                UsuarioBio usuariobio = new UsuarioBio();
                usuariobio.Usuario = dgvDigitaisBD.SelectedRows[index].Cells[0].Value.ToString();
                usuariobio.TemplateA = dgvDigitaisBD.SelectedRows[index].Cells[1].Value.ToString();
                usuariobio.TemplateB = dgvDigitaisBD.SelectedRows[index].Cells[2].Value.ToString();
                ListaUsuariosBio.Add(usuariobio);
            }
            return ListaUsuariosBio;
        }

        private void cmdReceberModelo_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController = new FrmBIOController(this, InnerAtual, null);
            BioController.ReceberModeloBio();
        }

        private void btnCapturar_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesDisplay();
            BioController.CapturaTemplate(UpFrmBio);
        }

        private void CarregarPropriedadesDisplay()
        {
            UpFrmBio = new UpdatePropriedadeTelaBio();
            UpFrmBio.txtCartaoCadInner = txtCartaoCadInner.Text;
            UpFrmBio.txtCartaoCaptura = txtCartaoCap.Text;
            UpFrmBio.ImagemDigital = pcbImagemDigital;
            UpFrmBio.EnviarDigitalInner = chkEnviarInner.Checked;
            UpFrmBio.CheckImagem = chkImagem.Checked;
            UpFrmBio.QualidadeDigital = tcbQualidadeDigital.Value;
            UpFrmBio.QualidadeImagem = tcbVerify.Value;
        }

        private void cmdReceberQtdeUsuarios_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController = new FrmBIOController(this, InnerAtual, null);
            BioController.ReceberQtdUsuariosBIO();
        }

        private void btnExcluirBD_Click(object sender, EventArgs e)
        {
            List<string> ListaUsuariosExcluir = getUsuariosExcluir();
            BioController = new FrmBIOController(this, null, null);
            BioController.ExcluirUsuarioBD(ListaUsuariosExcluir);
        }

        private List<string> getUsuariosExcluir()
        {
            List<string> ListaExcluir = new List<string>();
            for (int index = 0; index < dgvDigitaisBD.SelectedRows.Count; index++)
            {
                string usuario = dgvDigitaisBD.SelectedRows[index].Cells[0].Value.ToString();
                ListaExcluir.Add(usuario);
            }
            return ListaExcluir;
        }

        private void btnReceberDigitais_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController = new FrmBIOController(this, InnerAtual, null);
            List<UsuarioBio> ListaRec = getUsuariosReceber();
            BioController.ReceberDigitais(ListaRec);
        }

        private List<UsuarioBio> getUsuariosReceber()
        {
            List<UsuarioBio> ListaRec = new List<UsuarioBio>();
            for (int index = 0; index < dgvDigitaisInner.SelectedRows.Count; index++)
            {
                UsuarioBio usuarioBio = new UsuarioBio();
                usuarioBio.Usuario = dgvDigitaisInner.SelectedRows[index].Cells[0].Value.ToString();
                ListaRec.Add(usuarioBio);
            }
            return ListaRec;
        }

        private void btnExcluirInner_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController = new FrmBIOController(this, InnerAtual, null);
            List<string> ListaExcluir = getUsuariosExcluirInner();
            BioController.ExcluirSelecionadosInner(ListaExcluir);
        }

        private List<string> getUsuariosExcluirInner()
        {
            List<string> lista = new List<string>();
            for(int index = 0; index < dgvDigitaisInner.SelectedRows.Count; index++)
            {
                string usuario = dgvDigitaisInner.SelectedRows[index].Cells[0].Value.ToString();
                lista.Add(usuario);
            }
            return lista;
        }

        private void btnAtualizarLista_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            BioController = new FrmBIOController(this, InnerAtual, null);
            dgvDigitaisInner.Rows.Clear();
            BioController.AtualizarUsuariosBIO();
        }

        private void btnInserirUsuarioLista_Click(object sender, EventArgs e)
        {
            string usuario = Interaction.InputBox("Digite o usuário a incluir:", "Incluir usuário sem digital", "1", 0, 0);
            bool UsuarioCorreto = Utils.SomenteNumeros(usuario);
            while (UsuarioCorreto == false)
            {
                DialogResult result = MessageBox.Show("Usuário inválido\nDeseja tentar novamente?", "Erro incluir usuário SD", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    usuario = Interaction.InputBox("Digite o usuário a incluir:", "Incluir usuário sem digital", "1", 0, 0);
                    UsuarioCorreto = Utils.SomenteNumeros(usuario);
                }
                else
                {
                    UsuarioCorreto = false;
                    break;
                }
            }
            if (UsuarioCorreto == true)
            {
                DAOUsuariosBio AcessoBanco = new DAOUsuariosBio();
                UsuarioSemDigital usuarioSD = new UsuarioSemDigital();
                usuarioSD.Usuario = usuario;
                AcessoBanco.InserirUsuarioSemDigital(usuarioSD);
                CarregarUsuariosSD();
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

        private void btnCadastrarDigitalInner_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            CarregarPropriedadesDisplay();
            BioController = new FrmBIOController(this, InnerAtual, UpFrmBio);
            BioController.CadastrarUsuarioInner();
        }

        private void btnCadastrarBDInner_Click(object sender, EventArgs e)
        {
            CarregarPropriedadesInner();
            CarregarPropriedadesDisplay();
            BioController = new FrmBIOController(this, InnerAtual, UpFrmBio);
            BioController.SolicitarTemplateLeitorInner();
        }
    }
}