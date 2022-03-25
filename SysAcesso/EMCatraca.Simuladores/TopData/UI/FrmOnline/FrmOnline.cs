using EasyInnerSDK;
using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Simuladores.DAO;
using EMCatraca.Simuladores.Entity;
using EMCatraca.TopData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EMCatraca.Simuladores.SimuladorTopData.UI.FrmOnline
{
    public partial class FrmOnline : Form
    {
        private List<Dispositivo> _catracas { get; set; }
        private Dictionary<int, Inner> _inners { get; set; }
        private bool Executando { get; set; }

        private DAOUsuarios AcessoLista;
        private List<Bilhete> ListaBilhetes;

        private bool _liberaEntrada;
        private bool _liberaEntradaInvertida;
        private bool _liberaSaida;
        private bool _liberaSaidaInvertida;
        private bool _ehParaAlterarInner;
        private int _retornoComSucesso = (int)EnumeradoresDllInner.Retorno.RetornoComandoOk;

        private byte VersaoAlta = 0;
        private byte InnerAcessoBio;

        public FrmOnline()
        {
            InitializeComponent();

            _inners = new Dictionary<int, Inner>();
            _catracas = ObtenhaListaCatracasJson();
        }

        private void FrmOnline_Load(object sender, EventArgs e)
        {
            AjustaComboBoxPadraoCartao();
            AjustaComboBoxTipoDeConexao();
            AjustaComboBoxTipoLeitor();
            AjustaComboBoxTipoEquipamento();

            btnPararMaquina.Enabled = false;

            if (_catracas.Any())
            {
                foreach (var catraca in _catracas)
                {
                    AdicionarInner(catraca);
                }
            }
        }

        private void AjustaComboBoxTipoEquipamento()
        {
            // Padrão em sera Dispositivo Entrada/Saída
            cboTipoEquipamento.Items.Clear();
            cboTipoEquipamento.Items.Add("Dispositivo Entrada/Saída");
            cboTipoEquipamento.Items.Add("Dispositivo Entrada");
            cboTipoEquipamento.Items.Add("Dispositivo Saída");
            cboTipoEquipamento.Items.Add("Dispositivo Saída Liberada");
            cboTipoEquipamento.Items.Add("Dispositivo Entrada Liberada");
            cboTipoEquipamento.Items.Add("Dispositivo Liberada 2 Sentidos");
            cboTipoEquipamento.Items.Add("Dispositivo Liberada 2 Sentidos(Sentido Giro)");
            cboTipoEquipamento.Items.Add("Dispositivo com Urna");
            cboTipoEquipamento.SelectedIndex = 0;
        }

        private void AjustaComboBoxTipoLeitor()
        {
            // Padrão Em Código de Barras
            cboTipoLeitor.Items.Clear();
            cboTipoLeitor.Items.Add("Código de Barras");
            cboTipoLeitor.Items.Add("Magnético");
            cboTipoLeitor.Items.Add("Proximidade Abatrack/Smart Card");
            cboTipoLeitor.Items.Add("Proximidade Wiegand/Smart Card");
            cboTipoLeitor.Items.Add("Proximidade Smart Card Serial");
            cboTipoLeitor.Items.Add("Código de barras serial");
            cboTipoLeitor.Items.Add("Prox. Wiegand FC Sem Separador");
            cboTipoLeitor.Items.Add("Prox. Wiegand FC Com Separador");
            cboTipoLeitor.Items.Add("Barras, Prox, QR Code c/ letras");
            cboTipoLeitor.SelectedIndex = 0;
        }

        private void AjustaComboBoxTipoDeConexao()
        {
            // Padrão Em sempre sera TCP/IP porta fixa
            cboTipoConexaoOnline.Items.Add("Nenhuma");
            cboTipoConexaoOnline.Items.Add("TCP/IP porta variável");
            cboTipoConexaoOnline.Items.Add("TCP/IP porta fixa");
            cboTipoConexaoOnline.SelectedIndex = 2;
        }

        private void AjustaComboBoxPadraoCartao()
        {
            // Padão Em sempre deverar ser livre
            cboPadraoCartao.Items.Add("Livre");
            cboPadraoCartao.SelectedIndex = 0;
        }

        public void ExibirMensagembox(string Mensagem, string Titulo)
        {
            MessageBox.Show(Mensagem, Titulo);
        }

        private void btnAdicionarCatracaInnerOnline_Click(object sender, EventArgs e)
        {
            AdcionaItemNaLista();
        }

        private void AdcionaItemNaLista()
        {
            AdicionarInner();
            btnIniciarMaquina.Enabled = true;
        }

        private void btnIniciarMaquina_Click(object sender, EventArgs e)
        {
            btnPararMaquina.Enabled = true;
            btnIniciarMaquina.Enabled = false;
            Application.DoEvents();
            IniciarMaquina();
        }

        private void btnPararMaquina_Click(object sender, EventArgs e)
        {
            PararMaquina();
            lstVersaoInners.Items.Clear();

            btnIniciarMaquina.Enabled = true;

            btnComandoEntrada.Enabled = false;
            btnPararMaquina.Enabled = false;
            cmdSair.Enabled = false;
        }

        private void btnRemoverInnerLista_Click(object sender, EventArgs e)
        {
            RemoveItemDalista();
        }

        private void RemoveItemDalista()
        {
            if (lstInnersCadastrados.Items.Count != 0)
            {
                if (lstInnersCadastrados.Items.Count == 1)
                {
                    lstInnersCadastrados.SetSelected(0, true);
                }

                if ((Inner)lstInnersCadastrados.SelectedItem != null)
                {
                    var InnerAtual = ((Inner)lstInnersCadastrados.SelectedItem);

                    RemoverInner();
                    _inners.Remove(InnerAtual.Numero);

                    if (lstInnersCadastrados.Items.Count == 0)
                    {
                        btnIniciarMaquina.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um Inner para remover!", "Remover da Lista");
                }
            }
        }

        private void cboPadraoCartao_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValideDoisLeitores();

            switch (cboTipoLeitor.SelectedIndex)
            {
                case 2: //Proximidade Abatrack/Smart Card
                    udQtdDigitosCartao.Value = 10;
                    break;

                case 3: //Proximidade Wiegand/Smart Card
                    udQtdDigitosCartao.Value = 6;
                    break;

                case 6: //Prox. Wiegand FC Sem Separador
                    udQtdDigitosCartao.Value = 8;
                    break;

                case 7: //Prox. Wiegand FC Com Separador
                    udQtdDigitosCartao.Value = 10;
                    break;

                default:
                    break;
            }
        }

        private void ValideDoisLeitores()
        {
            var ehTipoLeitorCodigoBarra = cboTipoLeitor.SelectedIndex ==
                                           (int)EnumeradoresDllInner.TipoLeitor.CodigoDeBarras;

            var ehTipoLeitorMagnetico = cboTipoLeitor.SelectedIndex ==
                                        (int)EnumeradoresDllInner.TipoLeitor.Magnetico;

            ckbDoisLeitores.Enabled = !ehTipoLeitorCodigoBarra && !ehTipoLeitorMagnetico;
            ckbDoisLeitores.Checked = ckbDoisLeitores.Enabled;
        }

        private void ckbHabililtaFucoesBiometrica_Click(object sender, EventArgs e)
        {
            var habilitarFuncao = ckbHabililtaBiometria.Checked;
            if (ckbHabililtaBiometria.Checked)
            {
                chkHabilitaVerificacao.Enabled = habilitarFuncao;
                chkHabilitaIdentificacao.Enabled = habilitarFuncao;
                chkHabilitaListaSemDigital.Enabled = habilitarFuncao;
                return;
            }

            chkHabilitaVerificacao.Enabled = habilitarFuncao;
            chkHabilitaIdentificacao.Enabled = habilitarFuncao;
            chkHabilitaListaSemDigital.Enabled = habilitarFuncao;
            chkHabilitaVerificacao.Checked = habilitarFuncao;
            chkHabilitaIdentificacao.Checked = habilitarFuncao;
            chkHabilitaListaSemDigital.Checked = habilitarFuncao;
        }

        private void cmdLimparBilhetes_Click(object sender, EventArgs e)
        {
            lstBilhetes.Items.Clear();
        }

        private void btnComandoEntrada_Click(object sender, EventArgs e)
        {
            LiberarGiroCatraca("Entrada");
        }

        private void btnComandoSair_Click(object sender, EventArgs e)
        {
            LiberarGiroCatraca("Saida");
        }

        private void LiberarGiroCatraca(string tipoDoGiroCatraca)
        {
            if (lstInnersCadastrados.Items.Count == 1)
            {
                lstInnersCadastrados.SetSelected(0, true);
            }

            if ((Inner)lstInnersCadastrados.SelectedItem != null)
            {
                var innerAtualSelecionado = ((Inner)lstInnersCadastrados.SelectedItem);

                HabilitaLadoGiroDaCatraca(tipoDoGiroCatraca, innerAtualSelecionado.CatracaInvertida);
                innerAtualSelecionado.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoLiberarCatraca;
            }
            else
            {
                MessageBox.Show("Selecione um Inner para liberar!", "Liberar Acesso");
            }
        }

        private void ckbListaBio_Click(object sender, EventArgs e)
        {
            if (chkHabilitaListaSemDigital.Checked)
            {
                chkHabilitaVerificacao.Checked = true;
            }
        }

        private void ckbVerificacao_Click(object sender, EventArgs e)
        {
            if (!chkHabilitaVerificacao.Checked)
            {
                chkHabilitaListaSemDigital.Checked = false;
            }
        }

        private void chkCartaoMaster_CheckedChanged(object sender, EventArgs e)
        {
            txtCartaoMaster.Enabled = chkHabilitaCartaoMaster.Checked;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (lstInnersCadastrados.SelectedItem != null)
            {
                _ehParaAlterarInner = true;
                RemoveItemDalista();
                AdcionaItemNaLista();
                MensagemBox("O Inner foi alterado com sucesso!", "Aviso");
                return;
            }

            //Campo obrigatório
            MensagemBox("É necessário selecionar um Inner para remover da memória!", "Atenção");
            _ehParaAlterarInner = false;
            return;
        }

        public void IniciarMaquina()
        {
            try
            {
                Executando = true;
                HabilitarBotoes(false);
                AcessoLista = new DAOUsuarios();
                ListaBilhetes = new List<Bilhete>();
                AtualizarEstadosInner();
                MaquinaOnline();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
            }
        }

        public void PararMaquina()
        {
            try
            {
                Executando = false;
                HabilitarBotoes(true);

                //Exibe no rodapé o Fim da execução..
                lblStatus.Text = "Maquina parada";

                RetornarEstadoInners(EnumeradoresDllInner.EstadosInner.EstadoConectar, EnumeradoresDllInner.EstadosTeclado.TecladoEmBranco);

                //Fecha a porta da Easy Inner.
                EasyInner.FecharPortaComunicacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
            }
        }

        public void AdicionarInner(Dispositivo catracaJson = null)
        {
            var CatraInner = new Inner
            {
                Numero = catracaJson == null ? (int)udNumeroInner.Value : catracaJson.Codigo,
                Porta = catracaJson == null ? (int)udPorta.Value : Convert.ToInt32(catracaJson.PortaCatraca),
                QtdDigitos = (int)udQtdDigitosCartao.Value,
                TipoConexao = cboTipoConexaoOnline.SelectedIndex,
                TipoLeitor = cboTipoLeitor.SelectedIndex,
                DoisLeitores = ckbDoisLeitores.Checked,
                PadraoCartao = cboPadraoCartao.SelectedIndex,
                TipoEquipamento = cboTipoEquipamento.SelectedIndex,
                HabilitaListaOffLine = chkHabilitaListaOffLine.Checked,
                HabilitaTeclado = chkHabilitaTeclado.Checked,
                HabilitaCartaoMaster = chkHabilitaCartaoMaster.Checked,
                CartaoMaster = txtCartaoMaster.Text,
                Biometrico = ckbHabililtaBiometria.Checked,
                HabilitaListaBioSemDigital = chkHabilitaListaSemDigital.Checked,
                HabilitaVerificacao = (byte)(chkHabilitaVerificacao.Checked ? EnumeradoresDllInner.Opcao.Sim : EnumeradoresDllInner.Opcao.Nao),
                HabilitaIdentificacao = (byte)(chkHabilitaIdentificacao.Checked ? EnumeradoresDllInner.Opcao.Sim : EnumeradoresDllInner.Opcao.Nao),
                CatracaInvertida = optGiroEsquerda.Checked,
                EstadoAtual = (byte)EnumeradoresDllInner.EstadosInner.EstadoConectar,
                EstadoTeclado = (byte)EnumeradoresDllInner.EstadosTeclado.TecladoEmBranco,
                VariacaoInner = 0
            };

            foreach (Inner InnerAtual in _inners.Values)
            {
                if (CatraInner.Numero == InnerAtual.Numero)
                {
                    MessageBox.Show("Inner já cadastrado!", "Cadastro Inner");
                    return;
                }
            }

            _inners.Add(CatraInner.Numero, CatraInner);
            lstInnersCadastrados.Items.Add(CatraInner);
        }

        private void AtualizarEstadosInner()
        {
            foreach (Inner inner in _inners.Values)
            {
                AtualizarEstadoInner(inner.Numero, EnumeradoresDllInner.EstadosInner.EstadoConectar);
            }
        }

        public void AtualizarEstadoInner(int NumeroInner, EnumeradoresDllInner.EstadosInner EstadoInner)
        {
            lblStatus.Text = $"Inner: {NumeroInner} Estado: {EstadoInner}";
        }

        private void RetornarEstadoInners(EnumeradoresDllInner.EstadosInner estadosInner, EnumeradoresDllInner.EstadosTeclado EstadoTeclado)
        {
            foreach (Inner inner in _inners.Values)
            {
                inner.EstadoAtual = estadosInner;
                inner.EstadoTeclado = EstadoTeclado;
            }
        }

        public List<Dispositivo> ObtenhaListaCatracasJson()
        {
            return MapeadorArquivoJson.CarreguerArquivoJson<List<Dispositivo>>("emcatraca.catracas.cfg");
        }

        public void MensagemBox(string Mensagem, string Titulo)
        {
            ExibirMensagembox(Mensagem, Titulo);
        }

        public void HabilitarBotoes(bool Habilitar)
        {
            btnAdicionarCatracaInner.Enabled = Habilitar;
            btnIniciarMaquina.Enabled = Habilitar;
            btnRemoverInnerLista.Enabled = Habilitar;
            btnPararMaquina.Enabled = !Habilitar;
        }

        public void AtualizarVersaoInner(Inner InnerAtual)
        {
            bool Encontrado = false;
            for (int index = 0; index < lstVersaoInners.Items.Count; index++)
            {
                Console.WriteLine(lstVersaoInners.Items[index].ToString());
                Console.WriteLine((lstVersaoInners.Items[index].ToString().Substring(7, 1)));
                int NumInner = int.Parse(lstVersaoInners.Items[index].ToString().Substring(7, 1));
                if (NumInner == InnerAtual.Numero)
                {
                    Encontrado = true;
                }
            }
            if (Encontrado == false)
            {
                lstVersaoInners.Items.Add($"Inner: {InnerAtual.Numero} " +
                                          $"| {InnerAtual.LinhaInner}: {InnerAtual.VersaoInner} " +
                                          $"| {InnerAtual.ModeloBioInner} " +
                                          $"| {InnerAtual.VersaoBio}");
            }
        }

        private void lstInnersCadastrados_SelectedIndexChanged(object sender, EventArgs e)
        {
            var innerEstaSelecionada = (Inner)lstInnersCadastrados.SelectedItem != null;
            if (innerEstaSelecionada && !_ehParaAlterarInner)
            {
                var inner = (Inner)lstInnersCadastrados.SelectedItem;
                udNumeroInner.Value = inner.Numero;
                udPorta.Value = inner.Porta;
                udQtdDigitosCartao.Value = inner.QtdDigitos;
                cboTipoConexaoOnline.SelectedIndex = inner.TipoConexao;
                cboTipoLeitor.SelectedIndex = inner.TipoLeitor;
                ckbDoisLeitores.Checked = inner.DoisLeitores;
                cboPadraoCartao.SelectedIndex = inner.PadraoCartao;
                cboTipoEquipamento.SelectedIndex = inner.TipoEquipamento;
                chkHabilitaListaOffLine.Checked = inner.HabilitaListaOffLine;
                chkHabilitaTeclado.Checked = inner.HabilitaTeclado;
                chkHabilitaCartaoMaster.Checked = !(inner.CartaoMaster == string.Empty);
                ckbHabililtaBiometria.Checked = inner.Biometrico;
                chkHabilitaListaOffLine.Checked = inner.HabilitaListaOffLine;
                chkHabilitaIdentificacao.Checked = inner.HabilitaIdentificacao == 1;
                optGiroEsquerda.Checked = inner.CatracaInvertida;

            }
        }

        public void FuncoesCatraca(bool Habilitar, bool Dispositivo)
        {
            btnComandoEntrada.Enabled = Habilitar;
            cmdSair.Enabled = Habilitar;
        }

        public void AtulizarLabelDados(string mensagem)
        {
            lblDados.Text = mensagem;
        }

        public void RemoverInner()
        {
            if (lstInnersCadastrados.SelectedItem != null)
            {
                foreach (Inner inner in lstInnersCadastrados.Items)
                {
                    if (inner.Numero == ((Inner)lstInnersCadastrados.SelectedItem).Numero)
                    {
                        //Remove
                        lstInnersCadastrados.Items.Remove(lstInnersCadastrados.SelectedItem);

                        //Atualiza lista
                        if (lstInnersCadastrados.Items.Count > 0)
                        {
                            lstInnersCadastrados.SelectedIndex = lstInnersCadastrados.Items.Count - 1;
                        }

                        if (!_ehParaAlterarInner)
                        {
                            MensagemBox("Inner removido da memória", "");
                        }

                        break;
                    }
                }
            }
            else
            {
                //Campo obrigatório
                MensagemBox("É necessário selecionar um Inner para remover da memória!", "");
            }
        }

        public void HabilitaLadoGiroDaCatraca(string lado, bool Esquerda)
        {
            if (lado == "Entrada")
            {
                //entrada
                if (Esquerda == false)
                {
                    _liberaEntrada = true;
                    _liberaEntradaInvertida = false;
                }
                else
                {
                    _liberaEntradaInvertida = true;
                    _liberaEntrada = false;
                }
            }

            if (lado == "Saida")
            {
                //saída
                if (Esquerda == false)
                {
                    _liberaSaida = true;
                    _liberaSaidaInvertida = false;
                }
                else
                {
                    _liberaSaidaInvertida = true;
                    _liberaSaida = false;
                }
            }
        }

        public void AtualizarBilhetes(List<Bilhete> ListaBilhetes)
        {
            for (int index = 0; index < ListaBilhetes.Count; index++)
            {
                lstBilhetes.Items.Add(ListaBilhetes[index].ToString());
            }
        }

        public void AtualizarBilheteOnline(Bilhete RegistroOnline)
        {
            lstBilhetes.Items.Add(RegistroOnline.ToString());
            lstBilhetes.SetSelected(lstBilhetes.Items.Count - 1, true);
            lstBilhetes.ClearSelected();
        }

        #region Metados Ser estudados

        /// <summary>
        /// Método responsável pela liberação de acesso. Somente usuarios listado
        /// serão liberados. Esta consulta deverá ser feita em sua base de dados.
        /// </summary>
        /// <param name="NumCartao"></param>
        /// <returns></returns>
        private bool LiberarAcesso(string NumCartao)
        {
            bool acesso = false;

            List<Usuarios> ListaCartao = AcessoLista.ConsultarUsuarios(0);
            Usuarios user = new Usuarios() { CodigoUsuario = 0, Faixa = 101, Usuario = "455275978416" };
            Usuarios user2 = new Usuarios() { CodigoUsuario = 1, Faixa = 101, Usuario = "1221" };
            Usuarios user3 = new Usuarios() { CodigoUsuario = 2, Faixa = 101, Usuario = "ENGENHARIASW" };

            ListaCartao.Add(user);
            ListaCartao.Add(user2);
            ListaCartao.Add(user3);

            for (int index = 0; index < ListaCartao.Count; index++)
            {
                if (Utils.RemZeroEsquerda(ListaCartao[index].Usuario) == Utils.RemZeroEsquerda(NumCartao))
                {
                    acesso = true;
                }
            }
            return acesso;
        }

        /// <summary>
        /// Monta as configurações necessária para o funcionamento do Inner. Esta
        /// função é utilizada on-line ou off-line. modo = 0 off line/modo = 1 on line
        /// </summary>
        /// <param name="innerAtual"></param>
        /// <param name="modo"></param>
        private void MontaConfiguracaoInner(Inner innerAtual, EnumeradoresDllInner.modoComunicacao modo)
        {
            //Antes de realizar a configuração precisa definir o Padrão do cartão 
            //Padrão EM PadraoLivre
            EasyInner.DefinirPadraoCartao((byte)EnumeradoresDllInner.PadraoCartao.PadraoLivre);

            //Define Modo de comunicação
            if (modo == EnumeradoresDllInner.modoComunicacao.ModoOFFLINE)
            {
                //Configurações para Modo Offline.
                //Prepara o Inner para trabalhar no modo Off-Line, porém essa função
                //ainda não envia essa informação para o equipamento.
                // Deve configurar offline primeiro de acordo cdocumentação;
                EasyInner.ConfigurarInnerOffLine();
            }
            else
            {
                //Configurações para Modo Online.
                //Prepara o Inner para trabalhar no modo On-Line, porém essa função
                //ainda não envia essa informação para o equipamento.
                //Padrão EM
                EasyInner.ConfigurarInnerOnLine();
            }

            //Verificar
            //Acionamentos 1 e 2
            //Configura como irá funcionar o acionamento(rele) 1 e 2 do Inner, e por
            //quanto tempo ele será acionado.
            switch (innerAtual.TipoEquipamento)
            {
                //Dispositivo
                case (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaEntradaEhSaida:
                    EasyInner.ConfigurarAcionamento1((int)EnumeradoresDllInner.FuncaoAcionamento.AcionaRegistroEntradaOuSaida, 5);
                    EasyInner.ConfigurarAcionamento2((int)EnumeradoresDllInner.FuncaoAcionamento.NaoUtilizado, 0);
                    EasyInner.ConfigurarLeitor1((byte)EnumeradoresDllInner.Operacao.EntradaEhSaida);
                    if (innerAtual.DoisLeitores)
                    {
                        EasyInner.ConfigurarLeitor2((byte)EnumeradoresDllInner.Operacao.EntradaEhSaida);
                    }
                    else
                    {
                        EasyInner.ConfigurarLeitor2((byte)EnumeradoresDllInner.Operacao.Desativado);
                    }
                    break;

                case (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaEntrada:
                    if (innerAtual.CatracaInvertida)
                    {
                        EasyInner.ConfigurarAcionamento1((int)EnumeradoresDllInner.FuncaoAcionamento.AcionaRegistroSaida, 5);
                        EasyInner.ConfigurarLeitor1((byte)EnumeradoresDllInner.Operacao.SomenteSaida);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)EnumeradoresDllInner.FuncaoAcionamento.AcionaRegistroEntrada, 5);
                        EasyInner.ConfigurarLeitor1((byte)EnumeradoresDllInner.Operacao.SomenteEntrada);
                    }
                    EasyInner.ConfigurarAcionamento2((int)EnumeradoresDllInner.FuncaoAcionamento.NaoUtilizado, 0);
                    EasyInner.ConfigurarLeitor2((byte)EnumeradoresDllInner.Operacao.Desativado);
                    break;

                case (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaSaida:
                    if (!innerAtual.CatracaInvertida)
                    {
                        EasyInner.ConfigurarAcionamento1((int)EnumeradoresDllInner.FuncaoAcionamento.AcionaRegistroSaida, 5);
                        EasyInner.ConfigurarLeitor1((byte)EnumeradoresDllInner.Operacao.SomenteSaida);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)EnumeradoresDllInner.FuncaoAcionamento.AcionaRegistroEntrada, 5);
                        EasyInner.ConfigurarLeitor1((byte)EnumeradoresDllInner.Operacao.SomenteEntrada);
                    }
                    EasyInner.ConfigurarAcionamento2((int)EnumeradoresDllInner.FuncaoAcionamento.NaoUtilizado, 0);
                    EasyInner.ConfigurarLeitor2((byte)EnumeradoresDllInner.Operacao.Desativado);
                    break;


                case (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaSaidaLiberada:
                    if (innerAtual.CatracaInvertida)
                    {
                        EasyInner.ConfigurarAcionamento1((int)EnumeradoresDllInner.FuncaoAcionamento.CatracaEntradaLiberada, 5);
                        EasyInner.ConfigurarLeitor1((byte)EnumeradoresDllInner.Operacao.SomenteSaida);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)EnumeradoresDllInner.FuncaoAcionamento.CatracaSaidaLiberada, 5);
                        EasyInner.ConfigurarLeitor1((byte)EnumeradoresDllInner.Operacao.SomenteEntrada);
                    }
                    EasyInner.ConfigurarLeitor2((byte)EnumeradoresDllInner.Operacao.Desativado);
                    break;


                case (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaEntradaLiberada:
                    if (innerAtual.CatracaInvertida)
                    {
                        EasyInner.ConfigurarAcionamento1((int)EnumeradoresDllInner.FuncaoAcionamento.CatracaSaidaLiberada, 5);
                        EasyInner.ConfigurarLeitor1((byte)EnumeradoresDllInner.Operacao.SomenteEntrada);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)EnumeradoresDllInner.FuncaoAcionamento.CatracaEntradaLiberada, 5);
                        EasyInner.ConfigurarLeitor1((byte)EnumeradoresDllInner.Operacao.SomenteSaida);
                    }
                    EasyInner.ConfigurarLeitor2((byte)EnumeradoresDllInner.Operacao.Desativado);
                    break;

                case (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaLiberadaDoisSentidos:
                    EasyInner.ConfigurarAcionamento1((int)EnumeradoresDllInner.FuncaoAcionamento.CatracaLiberadaDoisSentidos, 5);
                    EasyInner.ConfigurarAcionamento2((int)EnumeradoresDllInner.FuncaoAcionamento.NaoUtilizado, 0);
                    EasyInner.ConfigurarLeitor1((byte)EnumeradoresDllInner.Operacao.EntradaEhSaida);
                    if (innerAtual.DoisLeitores)
                    {
                        EasyInner.ConfigurarLeitor2((byte)EnumeradoresDllInner.Operacao.EntradaEhSaida);
                    }
                    else
                    {
                        EasyInner.ConfigurarLeitor2((byte)EnumeradoresDllInner.Operacao.Desativado);
                    }
                    break;

                case (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaSentidoGiro:
                    EasyInner.ConfigurarAcionamento1((int)EnumeradoresDllInner.FuncaoAcionamento.CatacaLiberadaDoisSantiddosMarcacaoRegistro, 5);
                    EasyInner.ConfigurarAcionamento2((int)EnumeradoresDllInner.FuncaoAcionamento.NaoUtilizado, 0);
                    if (innerAtual.DoisLeitores)
                    {
                        EasyInner.ConfigurarLeitor2((byte)EnumeradoresDllInner.Operacao.EntradaEhSaida);
                    }
                    else
                    {
                        EasyInner.ConfigurarLeitor2((byte)EnumeradoresDllInner.Operacao.Desativado);
                    }
                    break;

                case (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaUrna:
                    EasyInner.ConfigurarAcionamento1((int)EnumeradoresDllInner.FuncaoAcionamento.AcionaRegistroEntrada, 5);
                    EasyInner.ConfigurarAcionamento2((int)EnumeradoresDllInner.FuncaoAcionamento.AcionaRegistroSaida, 5);
                    EasyInner.ConfigurarLeitor1((byte)EnumeradoresDllInner.Operacao.SomenteEntrada);
                    EasyInner.ConfigurarLeitor2((byte)EnumeradoresDllInner.Operacao.SomenteSaida);
                    break;
            }

            // define os valores para configurar os leitores de acordo com o tipo de inner
            //DefineValoresParaConfigurarLeitores(innerAtual);
            //EasyInner.ConfigurarLeitor1(innerAtual.ValorLeitor1);
            //EasyInner.ConfigurarLeitor2(innerAtual.ValorLeitor2);

            //Configurar tipo do leitor
            ConfigurarTpLeitor(innerAtual);
            //Envia qtd de dígitos.
            EasyInner.DefinirQuantidadeDigitosCartao(Convert.ToByte(innerAtual.QtdDigitos));


            // Caso desejar configurar o Inner para ler cartoes 
            // que possam variar de 1 dígito até 16 dígitos
            // utilizar a funcao InserirQuantidadeDigitoVariavel
            //EasyInner.InserirQuantidadeDigitoVariavel(4);
            //EasyInner.InserirQuantidadeDigitoVariavel(6);
            //EasyInner.InserirQuantidadeDigitoVariavel(8);
            //EasyInner.InserirQuantidadeDigitoVariavel(10);
            //EasyInner.InserirQuantidadeDigitoVariavel(12);
            //EasyInner.InserirQuantidadeDigitoVariavel(14);
            //EasyInner.InserirQuantidadeDigitoVariavel(16);

            // Caso utilize o Inner Acesso 2
            //EasyInner.DesabilitarWebServer(2);
            //EasyInner.DefinirSensorPortaOffline(3);
            //EasyInner.DesabilitarBipColetor(2);
            //EasyInner.ConfigurarBotaoExternoOffline(3);

            if (innerAtual.HabilitaCartaoMaster)
            {
                EasyInner.DefinirNumeroCartaoMaster(innerAtual.CartaoMaster);
            }
            //Habilitar teclado
            EasyInner.HabilitarTeclado((byte)(innerAtual.HabilitaTeclado ? EnumeradoresDllInner.Opcao.Sim : EnumeradoresDllInner.Opcao.Nao), 0);

            //Configura equipamentos com dois leitores
            if (innerAtual.DoisLeitores)
            {
                // exibe mensagens do segundo leitor
                EasyInner.ConfigurarWiegandDoisLeitores(0, (byte)EnumeradoresDllInner.Opcao.Sim);
            }

            //Registra acesso negado
            EasyInner.RegistrarAcessoNegado(0);

            //Dispositivo
            //Define qual será o tipo do registro realizado pelo Inner ao aproximar um
            //cartão do tipo proximidade no leitor do Inner, sem que o usuário tenha
            //pressionado a tecla entrada, saída ou função.
            if ((innerAtual.TipoEquipamento == (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaEntradaEhSaida)
                || (innerAtual.TipoEquipamento == (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaLiberadaDoisSentidos)
                || (innerAtual.TipoEquipamento == (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaSentidoGiro))
            {
                //Configura o tipo de registro que será associado a uma marcação
                EasyInner.DefinirFuncaoDefaultLeitoresProximidade(12); // 12 – Libera a catraca nos dois sentidos e registra o bilhete conforme o sentido giro.

                if (innerAtual.Biometrico)
                {
                    EasyInner.DefinirFuncaoDefaultSensorBiometria(12);
                }
                else
                {
                    EasyInner.DefinirFuncaoDefaultSensorBiometria(0);
                }
            }
            else
            {
                if ((innerAtual.TipoEquipamento == (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaEntrada)
                    || (innerAtual.TipoEquipamento == (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaSaidaLiberada))
                {
                    if (innerAtual.CatracaInvertida == false)
                    {
                        //Configura o tipo de registro que será associado a uma marcação
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(10);  // 10 – Registrar sempre como entrada.

                        if (innerAtual.Biometrico)
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(10);
                        }
                        else
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(0);
                        }
                    }
                    else
                    {
                        //Configura o tipo de registro que será associado a uma marcação
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(11);  // Inverte o sentido de entrada.

                        if (innerAtual.Biometrico)
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(11);
                        }
                        else
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(0);
                        }
                    }

                }
                else
                {
                    if (innerAtual.CatracaInvertida == false)
                    {
                        //Configura o tipo de registro que será associado a uma marcação
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(10);  // 10 – Registrar sempre como entrada.

                        if (innerAtual.Biometrico)
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(11);
                        }
                        else
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(0);
                        }

                    }
                    else
                    {
                        if (innerAtual.Biometrico)
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(10);
                        }
                        else
                        {
                            EasyInner.DefinirFuncaoDefaultSensorBiometria(0);
                        }
                    }
                }
            }

            //Define Inner bio Variável(cartões biométricos com até 16 dígitos)
            if (VersaoAlta >= 5)
            {
                EasyInner.SetarBioVariavel(1);
                EasyInner.ConfigurarBioVariavel(1);
            }

            if (innerAtual.QtdDigitos <= 14)
            {
                //Configura para receber o horario dos dados quando Online.
                EasyInner.ReceberDataHoraDadosOnLine((byte)(EnumeradoresDllInner.Opcao.Sim));
            }
            //Define tipo lista off
            if (innerAtual.HabilitaListaOffLine)
            {
                EasyInner.DefinirTipoListaAcesso(1);
            }

        }

        private static void ConfigurarTpLeitor(Inner innerAtual)
        {
            switch (innerAtual.TipoLeitor)
            {
                case (byte)EnumeradoresDllInner.TipoLeitor.CodigoDeBarras:
                    EasyInner.ConfigurarTipoLeitor((byte)EnumeradoresDllInner.TipoLeitor.CodigoDeBarras);
                    break;
                case (byte)EnumeradoresDllInner.TipoLeitor.Magnetico:
                    EasyInner.ConfigurarTipoLeitor((byte)EnumeradoresDllInner.TipoLeitor.Magnetico);
                    break;
                case (byte)EnumeradoresDllInner.TipoLeitor.ProximidadeAbatrack2:
                    EasyInner.ConfigurarTipoLeitor((byte)EnumeradoresDllInner.TipoLeitor.ProximidadeAbatrack2);
                    break;
                case (byte)EnumeradoresDllInner.TipoLeitor.Wiegand:
                    EasyInner.ConfigurarTipoLeitor((byte)EnumeradoresDllInner.TipoLeitor.Wiegand);
                    break;
                case (byte)EnumeradoresDllInner.TipoLeitor.ProximidadeSmartCardSertial:
                    EasyInner.ConfigurarTipoLeitor((byte)EnumeradoresDllInner.TipoLeitor.ProximidadeSmartCardSertial);
                    break;
                case (byte)EnumeradoresDllInner.TipoLeitor.CodigoBarrasSerial:
                    EasyInner.ConfigurarTipoLeitor((byte)EnumeradoresDllInner.TipoLeitor.CodigoBarrasSerial);
                    break;
                case (byte)EnumeradoresDllInner.TipoLeitor.WiegandFcSemSeparador:
                    EasyInner.ConfigurarTipoLeitor((byte)EnumeradoresDllInner.TipoLeitor.WiegandFcSemSeparador);
                    break;
                case (byte)EnumeradoresDllInner.TipoLeitor.WiegandFcComSeparador:
                    EasyInner.ConfigurarTipoLeitor((byte)EnumeradoresDllInner.TipoLeitor.Wiegand);
                    break;
                case (byte)EnumeradoresDllInner.TipoLeitor.QRCodeLetras:
                    EasyInner.ConfigurarTipoLeitor((byte)EnumeradoresDllInner.TipoLeitor.BarrasProxQRCode);
                    break;
                default:
                    EasyInner.ConfigurarTipoLeitor((byte)EnumeradoresDllInner.TipoLeitor.CodigoDeBarras);
                    break;
            }
        }

        private string InverterString(string str)
        {
            int tamanho = str.Length;
            char[] caracteres = new char[tamanho];

            for (int i = 0; i < tamanho; i++)
            {
                caracteres[i] = str[tamanho - 1 - i];
            }

            return new string(caracteres);
        }

        private int BinarioParaDecimal(string valorBinario)
        {
            int expoente = 0;
            int numero;
            int soma = 0;
            string numeroInvertido = InverterString(valorBinario);

            for (int i = 0; i < numeroInvertido.Length; i++)
            {
                //pega dígito por dígito do número digitado
                numero = Convert.ToInt32(numeroInvertido.Substring(i, 1));
                //multiplica o dígito por 2 elevado ao expoente, e armazena o resultado em soma
                soma += numero * (int)Math.Pow(2, expoente);
                // incrementa          
                expoente++;
            }
            return soma;
        }

        /// <summary>
        /// Define Mudanças OnLine
        /// Função que configura BIT a BIT, Ver no manual Anexo III
        /// </summary>
        /// <param name="InnerAtual"></param>
        /// <returns></returns>
        private int ConfiguraEntradasMudancaOnLine(Inner InnerAtual)
        {
            string Configuracao;

            //Habilita Teclado
            Configuracao = (InnerAtual.HabilitaTeclado ? 1 : 0).ToString();

            var ehBiometrico = InnerAtual.Biometrico;
            if (!ehBiometrico)
            {
                var ehDoisLeitores = InnerAtual.DoisLeitores;
                var ehAcionamentoCatracaEntrada = InnerAtual.TipoEquipamento == (int)EnumeradoresDllInner.Acionamento.AcionamentoCatracaEntrada;
                var ehAcionamentoCatracaSaida = InnerAtual.TipoEquipamento == (int)EnumeradoresDllInner.Acionamento.AcionamentoCatracaSaida;
                if (ehDoisLeitores)//Dois leitores
                {
                    Configuracao = "010" + //Leitor 2 só saida
                                   "001" + //Leitor 1 só entrada
                                   Configuracao;
                }
                else if (ehAcionamentoCatracaEntrada) //Apenas um leitores
                {
                    if (InnerAtual.CatracaInvertida)
                    {
                        Configuracao = "000" + //Leitor 2 Desativado
                                       "010" +
                                       Configuracao;  //Leitor 1 configurado para Saída
                    }
                    else
                    {
                        Configuracao = "000" + //Leitor 2 Desativado
                                       "001" +
                                       Configuracao; //Leitor 1 configurado para Entrada
                    }
                }
                else if (ehAcionamentoCatracaSaida)
                {
                    if (InnerAtual.CatracaInvertida)
                    {
                        Configuracao = "000" + //Leitor 2 Desativado
                                       "001" +
                                       Configuracao; //Leitor 1 configurado para Entrada
                    }
                    else
                    {
                        Configuracao = "000" + //Leitor 2 Desativado
                                       "010" +
                                       Configuracao; //Leitor 1 configurado para Saída
                    }
                }
                else
                {
                    Configuracao = "000" + //Leitor 2 Desativado
                                   "011" +
                                   Configuracao; //Leitor 1 configurado para Entrada e Saída
                }

                //Configuracao += Configuracao;
                Configuracao = "1" + // Habilitado
                               Configuracao;
            }
            else //Com Biometria 
            {
                Configuracao = "0" + //Bit Fixo
                               "1" + //Habilitado
                               InnerAtual.HabilitaIdentificacao + //Identificação
                               InnerAtual.HabilitaVerificacao + //Verificação
                               "0" + //Bit fixo 
                               (InnerAtual.DoisLeitores ? "11" : "10") + // 11 -> habilita leitor 1 e 2, 10 -> habilita apenas leitor 1
                               Configuracao;
            }

            //Converte Binário para Decimal
            return BinarioParaDecimal(Configuracao);

        }

        /// <summary>
        /// Insere no buffer da dll um horário de acesso. O Inner possui uma tabela de
        /// 100 horários de acesso, para cada horário é possível definir 4 faixas de acesso
        /// para cada dia da semana.
        /// Tabela de horarios numero 1
        /// </summary>
        /// <param name="InnerAtual"></param>
        private void MontarHorarios(Inner InnerAtual)
        {
            List<Entity.Horarios> ListaHorarios = Entity.Horarios.MontarListaHorarios();
            for (int index = 0; index < ListaHorarios.Count; index++)
            {
                //Insere no buffer da DLL horario de acesso
                EasyInner.InserirHorarioAcesso(ListaHorarios[index].Horario, ListaHorarios[index].Dia, ListaHorarios[index].Faixa,
                                                ListaHorarios[index].Hora, ListaHorarios[index].Minuto); //(1 - nº da tabela horario, 1 - dia da semana, 1 - faixa de horario, 8 - hora, 0 - minuto)
            }

            EasyInner.EnviarHorariosAcesso(InnerAtual.Numero);

        }

        //***********************************************************************************
        //MONTAR LISTA LIVRE
        //Monta o buffer para enviar a lista nos inners da linha Inner, cartão padrão livre 14 dígitos
        //***********************************************************************************
        private void MontarListaLivre(Inner InnerAtual, List<Usuarios> Lista)
        {
            //Define qual padrao o Inner vai usar
            EasyInner.DefinirPadraoCartao((byte)EnumeradoresDllInner.PadraoCartao.PadraoLivre); //(1 - Padrao Livre(Default))

            //Quantidade de digitos que o cartao usará
            EasyInner.DefinirQuantidadeDigitosCartao((byte)InnerAtual.QtdDigitos); //(qtde de digitos)

            //Insere usuario da lista no buffer da DLL
            for (int index = 0; index < Lista.Count; index++)
            {
                EasyInner.InserirUsuarioListaAcesso(Lista[index].Usuario, 101); //(1 - depende do padrao do cartao, 1 - nº do horario ja cadastrado)
            }

            EasyInner.EnviarListaAcesso(InnerAtual.Numero);
        }

        //***********************************************************************************
        //Monta o buffer usuários sem digital
        //***********************************************************************************
        private void MontarBufferListaSemDigital(Inner InnerAtual, List<UsuarioSemDigital> ListaSD)
        {
            for (int index = 0; index < ListaSD.Count; index++)
            {
                if (InnerAtual.InnerNetAcesso)
                {
                    EasyInner.IncluirUsuarioSemDigitalBioInnerAcesso(ListaSD[index].Usuario);
                }
                else
                {
                    EasyInner.IncluirUsuarioSemDigitalBio(ListaSD[index].Usuario);
                }
            }
        }

        /// <summary>
        /// Metodo responsável por realizar um comando simples com o equipamento para detectar
        /// se esta conectado.
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        /// <returns></returns>
        private int TestaConexaoInner(Inner InnerAtual)
        {
            int retornoRelogio = -1;
            byte Dia = 0;
            byte Mes = 0;
            byte Ano = 0;
            byte Hora = 0;
            byte Minuto = 0;
            byte Segundo = 0;

            retornoRelogio = EasyInner.ReceberRelogio(InnerAtual.Numero,
                                                      ref Dia,
                                                      ref Mes,
                                                      ref Ano,
                                                      ref Hora,
                                                      ref Minuto,
                                                      ref Segundo);
            return retornoRelogio;

        }

        /// <summary>
        /// Efetua a coleta dos bilhetes no modo Off-line
        /// Próximo passo: ESTADO_ENVIAR_CFG_ONLINE
        /// </summary>
        /// <param name="uiOnline"></param>
        /// <param name="InnerAtual"></param>
        private void ColetarBilhetesInnerAcesso(Inner InnerAtual)
        {
            try
            {
                //Declaração de Variáveis...
                StringBuilder Cartao = new StringBuilder();
                byte Dia = 0;
                byte Mes = 0;
                byte Ano = 0;
                byte Hora = 0;
                byte Minuto = 0;
                string strCartao = string.Empty;
                int Ret = -1;
                int Count = 0;
                byte Tipo = 0;
                int tamCartao = 0;

                int nBilhetes;

                int[] receber = new int[2];

                Thread.BeginCriticalRegion();

                //Envia o Comando Receber Dados Online..

                //Zera a contagem de Ping Online.
                InnerAtual.CntDoEvents = 0;
                InnerAtual.CountPingFail = 0;
                InnerAtual.CountRepeatPingOnline = 0;

                nBilhetes = 0;
                //Envia o Comando de Coleta de Bilhetes..
                Ret = EasyInner.ColetarBilhete(InnerAtual.Numero, ref Tipo, ref Dia, ref Mes, ref Ano, ref Hora, ref Minuto, Cartao);

                //Caso exista bilhete a coletar..
                if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
                {
                    strCartao = "";

                    //Recebe hora atual para inicio do PingOnline
                    InnerAtual.TempoInicialPingOnLine = DateTime.Now;

                    // Antes de realizar a configuração precisa definir o Padrão do cartão 
                    if (InnerAtual.PadraoCartao == 0)
                    {
                        tamCartao = Cartao.Length;
                    }
                    else
                    {
                        tamCartao = Cartao.Length - 1;
                    }

                    //Atribui o nro do Cartão..
                    for (Count = 0; Count < tamCartao; Count++)
                    {
                        strCartao += System.Convert.ToString(System.Convert.ToChar(Cartao[Count]));
                    }
                    Bilhete registro = new Bilhete();
                    registro.Tipo = Tipo;
                    registro.Dia = Dia;
                    registro.Mes = Mes;
                    registro.Ano = Ano;
                    registro.Hora = Hora;
                    registro.Minuto = Minuto;
                    registro.Cartao.Append(strCartao);

                    ListaBilhetes.Add(registro);
                    nBilhetes++;
                    InnerAtual.BilhetesAReceber--;
                }
                if (InnerAtual.BilhetesAReceber == 0)
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReceberQTDBilhetesOFF;
                    if (ListaBilhetes.Count > 0)
                    {
                        AtualizarBilhetes(ListaBilhetes);
                    }
                }
                Cartao = null;
            }
            catch (Exception ex)
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Efetua a coleta dos bilhetes no modo Off-line
        /// Próximo passo: ESTADO_ENVIAR_CFG_ONLINE
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void ColetarBilhetesInnerNet(Inner InnerAtual)
        {
            try
            {

                //Declaração de Variáveis...
                StringBuilder Cartao = new StringBuilder();
                byte Dia = 0;
                byte Mes = 0;
                byte Ano = 0;
                byte Hora = 0;
                byte Minuto = 0;
                string strCartao = string.Empty;
                int Ret = -1;
                int Count = 0;
                byte Tipo = 0;
                int tamCartao = 0;

                Thread.BeginCriticalRegion();

                //Envia o Comando Receber Dados Online..

                //Zera a contagem de Ping Online.
                InnerAtual.CntDoEvents = 0;
                InnerAtual.CountPingFail = 0;
                InnerAtual.CountRepeatPingOnline = 0;

                //Envia o Comando de Coleta de Bilhetes..
                Ret = EasyInner.ColetarBilhete(InnerAtual.Numero, ref Tipo, ref Dia, ref Mes, ref Ano, ref Hora, ref Minuto, Cartao);

                //Caso exista bilhete a coletar..
                if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
                {
                    strCartao = "";

                    //Recebe hora atual para inicio do PingOnline
                    InnerAtual.TempoInicialPingOnLine = DateTime.Now;

                    // Antes de realizar a configuração precisa definir o Padrão do cartão 
                    if (InnerAtual.PadraoCartao == 0)
                    {
                        tamCartao = Cartao.Length;
                    }
                    else
                    {
                        tamCartao = Cartao.Length - 1;
                    }

                    //Atribui o nro do Cartão..
                    for (Count = 0; Count < tamCartao; Count++)
                    {
                        strCartao += System.Convert.ToString(System.Convert.ToChar(Cartao[Count]));
                    }

                    //Adiciona a lista de bilhetes o Número bilhete coletado..
                    Bilhete RegistroOff = new Bilhete();
                    RegistroOff.Origem = InnerAtual.BilheteOnline.Origem;
                    RegistroOff.Complemento = InnerAtual.BilheteOnline.Complemento;
                    RegistroOff.Dia = Dia;
                    RegistroOff.Mes = Mes;
                    RegistroOff.Ano = Ano;
                    RegistroOff.Hora = Hora;
                    RegistroOff.Minuto = Minuto;
                    RegistroOff.Tipo = Tipo;
                    RegistroOff.Cartao.Append(strCartao);
                    ListaBilhetes.Add(RegistroOff);

                    //Adiciona 3 segundos tempo de coleta
                    InnerAtual.TempoColeta = DateTime.Now;
                    InnerAtual.CountPingFail = 0;
                }
                else
                {
                    if (Math.Abs(DateTime.Now.Subtract(InnerAtual.TempoColeta).TotalSeconds) > 3)
                    {
                        AtualizarBilhetes(ListaBilhetes);
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGOffLineCatraca;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
            }
        }

        private void MontarBilheteRecebido(Inner InnerAtual)
        {
            Console.WriteLine(InnerAtual.BilheteOnline.Cartao.ToString());

            var Cartao = InnerAtual.BilheteOnline.Cartao.ToString();
            int tam = InnerAtual.QtdDigitos > Cartao.Length
                       ? Cartao.Length
                       : InnerAtual.QtdDigitos;

            var strCartao = InnerAtual.BilheteOnline.Origem == 12
                             ? MontarCartaoBio(Cartao)
                             : MontarCartao(Cartao, tam);

            //Se o cartão padrão for topdata, configura os dígitos do cartão como padrão topdata
            string NumCartao = "";
            if (InnerAtual.PadraoCartao == 0)
            {
                //Padrão Topdata --> Cartão Topdata deve ter sempre 14 dígitos.
                //5 dígitos
                NumCartao = strCartao.ToString().Substring(13, 1);
                NumCartao = NumCartao + strCartao.ToString().Substring(4, 4);
            }
            else
            {
                //Padrão Livre
                NumCartao = strCartao;
            }

            var CartaoPronto = new StringBuilder();
            CartaoPronto.Append(NumCartao);
            InnerAtual.BilheteOnline.Cartao = CartaoPronto;
            //Adiciona bilhete coletado na Lista
            AtualizarBilheteOnline(InnerAtual.BilheteOnline);
        }

        private string MontarCartao(string Cartao, int tam)
        {
            string strCartao = "";
            for (int Count = 0; Count < tam; Count++)
            {
                strCartao += System.Convert.ToString(System.Convert.ToChar(Cartao[Count]));
            }
            return strCartao;
        }
        private string MontarCartaoBio(string Cartao)
        {
            string strCartao = "";
            Cartao = Utils.ReturnNumeros(Cartao);

            strCartao = Utils.RemZeroEsquerda(Cartao);
            return strCartao;
        }

        private int ReceberModeloBioAVer5(Inner InnerAtual)
        {
            int Ret = -1;
            int Modelo = 0;
            //Solicita o modelo do Inner bio.
            Ret = EasyInner.SolicitarModeloBio(InnerAtual.Numero);

            do
            {
                Thread.Sleep(1);

                //Retorna o resultado do comando SolicitarModeloBio, o modelo
                //do Inner Bio é retornado por referência no parâmetro da função.
                Ret = EasyInner.ReceberModeloBio(InnerAtual.Numero, 0, ref Modelo);
                Application.DoEvents();
            }
            while (Ret == (int)EnumeradoresDllInner.Retorno.RetornoBioProcessando);

            //Define o modelo do Inner Bio
            switch (Modelo)
            {
                case 1:
                    InnerAtual.ModeloBioInner = "Modelo do bio: Light 100 usuários FIM10";
                    break;
                case 4:
                    InnerAtual.ModeloBioInner = "Modelo do bio: 1000/4000 usuários FIM01";
                    break;
                case 51:
                    InnerAtual.ModeloBioInner = "Modelo do bio: 1000/4000 usuários FIM2030";
                    break;
                case 52:
                    InnerAtual.ModeloBioInner = "Modelo do bio: 1000/4000 usuários FIM2040";
                    break;
                case 48:
                    InnerAtual.ModeloBioInner = "Modelo do bio: Light 100 usuários FIM3030";
                    break;
                case 64:
                    InnerAtual.ModeloBioInner = "Modelo do bio: Light 100 usuários FIM3040";
                    break;
                case 80:
                    InnerAtual.ModeloBioInner = "Modelo do bio: FIM5060";
                    break;
                case 82:
                    InnerAtual.ModeloBioInner = "Modelo do bio: FIM5260";
                    break;
                case 83:
                    InnerAtual.ModeloBioInner = "Modelo do bio: FIM5360";
                    break;
                case 96:
                    InnerAtual.ModeloBioInner = "Modelo do bio: FIM6060";
                    break;
                case 255:
                    InnerAtual.ModeloBioInner = "Modelo do bio: Desconhecido";
                    break;
            }
            return Ret;
        }

        private int ReceberVersaoBioAVer5(Inner InnerAtual)
        {
            int Ret = -1;
            int VersaoAltaBio = 0;
            int VersaoBaixaBio = 0;
            //Solicita a versão do Inner bio.
            Ret = EasyInner.SolicitarVersaoBio(InnerAtual.Numero);
            if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
            {
                //Retorna o resultado do comando SolicitarVersaoBio, a versão
                //do Inner Bio é retornado por referência nos parâmetros da
                //função.

                Ret = EasyInner.ReceberVersaoBio(InnerAtual.Numero, 0, ref VersaoAltaBio, ref VersaoBaixaBio);
                Application.DoEvents();
            }
            InnerAtual.VersaoBio = VersaoAltaBio + "." + VersaoBaixaBio;
            AtualizarVersaoInner(InnerAtual);
            return Ret;
        }

        /// <summary>
        /// FUNCIONAMENTO DA MÁQUINA DE ESTADOS
        /// MÉTODO RESPONSÁVEL EM EXECUTAR OS PROCEDIMENTOS DO MODO ONLINE
        /// A Máquina de Estados nada mais é do que uma rotina que fica em loop testando
        /// uma variável que chamamos de Estado. Dependendo do estado atual, executamos
        /// alguns procedimentos e em seguida alteramos o estado que será verificado pela
        /// máquina de estados novamente no próximo passo do loop.
        /// </summary>
        private void MaquinaOnline()
        {
            try
            {
                int Ret2 = -1;

                //Define o tipo de conexão conforme selecionado em Combo (padrão Porta Fixa)
                EasyInner.DefinirTipoConexao((byte)_inners.Values.First().TipoConexao);

                //Fecha as conexões caso esteja aberta..
                EasyInner.FecharPortaComunicacao();
                //Abre a porta de comunicação com o Inner..
                Ret2 = EasyInner.AbrirPortaComunicacao(_inners.Values.First().Porta);

                //Tenta realizar a conexão com o Inner..
                Application.DoEvents();
                if (Ret2 == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
                {
                    //Enquanto a variável Estiver Selecionada para prosseguir a maquina, executa o processo..
                    while (Executando)
                    {
                        //Para cada inner da Lista de Inners cadastrados na UI.
                        foreach (Inner InnerAtual in _inners.Values)
                        {
                            //Verifica o Estado do Inner Atual..
                            switch (InnerAtual.EstadoAtual)
                            {
                                case EnumeradoresDllInner.EstadosInner.EstadoConectar:
                                    PassoEstadoConectar(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoReceberFirmWare:
                                    PassoEstadoReceberFirmWare(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoReceberModeloBio:
                                    PASSO_ESTADO_RECEBER_MODELO_BIO(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoReceberVersaoBio:
                                    PASSO_ESTADO_RECEBER_VERSAO_BIO(InnerAtual);
                                    break;



                                case EnumeradoresDllInner.EstadosInner.EstadoErnviarCFGOffLine:
                                    PassoEstadoEnviarCFGOffLine(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoEnviarListaOffLine:
                                    PassoEstadoEnviarListaOffLine(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoColetarBilhetes:
                                    PassoEstadoColetarBilhetes(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoEnviarCFGOnline:
                                    PassoEstadoEnviarCFGOnline(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoEnviarDataHora:
                                    PassoEstadoEnviarDataHora(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGPadrao: //Apos vencer o tempo mensagem volta para esta estado
                                    PassoEstadoEnviarMSGPadrao(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoConfigurarEntradasOnline:
                                    PassoEstadoConfigurarEntradasOnline(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadosPolling:
                                    PassoEstadoPolling(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoAguardaTempoMensagem: // Se umas desta validaçoes for verdade                                                    
                                    PassoEstadoAguardaTempoMemsagem(InnerAtual);                   // ehTeclaFuncao || ehTeclaAnula || ohCartaoEstaVazio
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoValidarAcesso:
                                    PassoEstadoValidarAcesso(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoLiberarCatraca:
                                    PASSO_ESTADO_LIBERA_GIRO_CATRACA(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoMonitorarGiroCatraca:
                                    PASSO_ESTADO_MONITORA_GIRO_CATRACA(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoPingOnline:
                                    PASSO_ESTADO_ENVIAR_PING_ONLINE(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoReconectar:
                                    PASSO_ESTADO_RECONECTAR(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoDefinicaoTeclado:
                                    PASSO_ESTADO_DEFINICAO_TECLADO(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoAguardaDefinicaoTeclado:
                                    PASSO_ESTADO_AGUARDA_DEFINICAO_TECLADO(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGUrna:
                                    PASSO_ESTADO_ENVIAR_MSG_URNA(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoMonitoramentoUrna:
                                    PASSO_ESTADO_MONITORA_URNA(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGAcessoNegado:
                                    PASSO_ESTADO_ENVIAR_MSG_ACESSO_NEGADO(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGOffLineCatraca:
                                    PASSO_ESTADO_ENVIAR_MSG_OFF_CATRACA(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoEnviarCFGOnLineOffLine:
                                    PASSO_ESTADO_ENVIAR_CONFIGMUD_ONLINE_OFFLINE(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoReceberQTDBilhetesOFF:
                                    PASSO_ESTADO_RECEBER_QTD_BILHETES_OFF(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGUrnaCheia:
                                    PASSO_ESTADO_ENVIAR_MSG_URNA_CHEIA(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoValidaUrnaCheia:
                                    PASSO_ESTADO_VALIDA_URNA_CHEIA(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoEnviarListaSemDigital:
                                    PASSO_ESTADO_ENVIAR_LISTA_SEMDIGITAL(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoDesligaRele:
                                    PASSO_ESTADO_DESLIGA_RELE(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoAcionarRele1:
                                    PASSO_ESTADO_ACIONAR_RELE1(InnerAtual);
                                    break;

                                case EnumeradoresDllInner.EstadosInner.EstadoAcionarRele2:
                                    PASSO_ESTADO_ACIONAR_RELE2(InnerAtual);
                                    break;

                            }
                            Thread.Sleep(5);
                            AtualizarEstadoInner(InnerAtual.Numero, InnerAtual.EstadoAtual);
                            Application.DoEvents();
                        }
                    }
                    Thread.EndCriticalRegion();
                }
                else
                {
                    MessageBox.Show("Erro ao tentar abrir a porta de comunicação.", "Atenção");
                    HabilitarBotoes(true);
                }
                //Fecha Porta de Comunicação.
                EasyInner.FecharPortaComunicacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
            }
        }

        private void PASSO_ESTADO_DESLIGA_RELE(Inner InnerAtual)
        {

            //Após passar os 2 segundos volta para o passo enviar mensagem padrão e desliga o rele
            TimeSpan tempo = DateTime.Now - InnerAtual.TempoInicialMensagem;
            if (tempo.Seconds >= 2)
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConfigurarEntradasOnline;
                if (InnerAtual.BilheteOnline.Origem == (int)EnumeradoresDllInner.Origem.VIA_LEITOR2)
                    EasyInner.DesabilitarRele2(InnerAtual.Numero);
                else
                    EasyInner.DesabilitarRele1(InnerAtual.Numero);

            }

        }

        private void PASSO_ESTADO_VALIDA_URNA_CHEIA(Inner InnerAtual)
        {

            //Declaração de Variáveis..
            Bilhete Bilhetes = new Bilhete();
            Bilhetes.Origem = 0;
            Bilhetes.Complemento = 0;
            Bilhetes.Cartao = null;
            StringBuilder Cartao = new StringBuilder();
            Bilhetes.Dia = 0;
            Bilhetes.Mes = 0;
            Bilhetes.Ano = 0;
            Bilhetes.Hora = 0;
            Bilhetes.Minuto = 0;
            Bilhetes.Segundo = 0;
            string strCartao = string.Empty;
            int Ret = -1;

            //Monitora o giro da catraca..
            Ret = EasyInner.ReceberDadosOnLine(InnerAtual.Numero,
                ref Bilhetes.Origem, ref Bilhetes.Complemento, Cartao, ref Bilhetes.Dia, ref Bilhetes.Mes, ref Bilhetes.Ano, ref Bilhetes.Hora,
                ref Bilhetes.Minuto, ref Bilhetes.Segundo);

            //Testa o retorno do comando..
            if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
            {
                if (Bilhetes.Origem == (int)EnumeradoresDllInner.Origem.URNA_CHEIA)
                {
                    AtulizarLabelDados("URNA CHEIA");
                    EasyInner.AcionarBipLongo(InnerAtual.Numero);

                    //Vai para o estado de Urna Cheia
                    InnerAtual.TempoInicialMensagem = DateTime.Now;
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGUrnaCheia;
                }
            }
            else
            {
                // Urna Nao esta cheia, chama metodo para pedir o cartao.
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGUrna;
            }
        }

        private void PASSO_ESTADO_ENVIAR_MSG_URNA_CHEIA(Inner InnerAtual)
        {
            int ret = -1;

            ret = (byte)EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "   URNA CHEIA    ESVAZIAR URNA ");

            if (ret == (byte)EnumeradoresDllInner.Retorno.RetornoComandoOk)
            {
                EasyInner.AcionarBipLongo(InnerAtual.Numero);
                if (InnerAtual.InnerNetAcesso)
                    EasyInner.LigarLedVermelho(InnerAtual.Numero);

                InnerAtual.TempoInicialMensagem = DateTime.Now;
                InnerAtual.Tentativas = 0;
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoAguardaTempoMensagem;
            }
            else
            {
                if (InnerAtual.Tentativas >= 3)
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                }
                //Adiciona 1 ao contador de tentativas
                InnerAtual.Tentativas++;
            }
        }

        /// <summary>
        /// Inicia a conexão com o Inner
        /// Próximo passo: EstadoEnviarCFGOffLine ????
        /// </summary>
        /// <param name="InnerAtual"></param>
        private void PassoEstadoConectar(Inner InnerAtual)
        {
            int count = 0;
            int Ret = -1;
            try
            {
                //Seta tempo inicial ping online
                InnerAtual.TempoInicialPingOnLine = DateTime.Now;

                //Testa a conexão
                do
                {
                    Ret = TestaConexaoInner(InnerAtual);
                    Thread.Sleep(300);

                } while ((count++ < 10) && (Ret != _retornoComSucesso));


                if (Ret == _retornoComSucesso)
                {
                    //caso consiga o Inner vai para o Passo de Configuração OFFLINE, 
                    //posteriormente para coleta de Bilhetes.
                    InnerAtual.Tentativas = 0;
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReceberFirmWare;
                    return;
                }

                //caso ele não consiga, tentará enviar três vezes, 
                //se não conseguir volta para o passo Reconectar.
                if (InnerAtual.Tentativas >= 3)
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                }

                //Contar Tentativas
                InnerAtual.Tentativas++;

            }
            catch (Exception ex)
            {
                // Houve algun erro setar EstadosInner.EstadoConectar
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// Configura a mudança automática
        /// Habilita/Desabilita a mudança automática do modo OffLine do Inner para
        /// OnLine e vice-versa.
        /// Habilita a mudança Offline
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_ENVIAR_CONFIGMUD_ONLINE_OFFLINE(Inner InnerAtual)
        {
            int Ret = -1;
            try
            {
                //Configura a mudança automática do modo OffLine do Inner para OnLine e vice-versa.
                EasyInner.HabilitarMudancaOnLineOffLine(2, 10);     //(2 - Habilita com PingOnline , 10 segundos)

                //Configura o teclado para quando o Inner voltar para OnLine após uma queda
                //para OffLine.
                EasyInner.DefinirConfiguracaoTecladoOnLine((Byte)InnerAtual.QtdDigitos, 0, 5, 17);

                //Define Mudanças OnLine
                //Função que configura BIT a BIT, Ver no manual Anexo III
                EasyInner.DefinirEntradasMudancaOnLine((Byte)ConfiguraEntradasMudancaOnLine(InnerAtual));

                if (InnerAtual.Biometrico)
                {
                    // Configura entradas mudança OffLine com Biometria
                    EasyInner.DefinirEntradasMudancaOffLineComBiometria((byte)(InnerAtual.HabilitaTeclado ? EnumeradoresDllInner.Opcao.Sim : EnumeradoresDllInner.Opcao.Nao),
                        3, (byte)(InnerAtual.DoisLeitores ? 3 : 0),
                        InnerAtual.HabilitaVerificacao, InnerAtual.HabilitaIdentificacao);
                }
                else
                {

                    if (InnerAtual.TipoEquipamento == (int)EnumeradoresDllInner.Acionamento.AcionamentoCatracaEntrada)
                    {
                        if (InnerAtual.CatracaInvertida)
                        {
                            EasyInner.DefinirEntradasMudancaOffLine((byte)(InnerAtual.HabilitaTeclado ? EnumeradoresDllInner.Opcao.Sim : EnumeradoresDllInner.Opcao.Nao), 2, 0, 0);
                        }
                        else
                        {
                            EasyInner.DefinirEntradasMudancaOffLine((byte)(InnerAtual.HabilitaTeclado ? EnumeradoresDllInner.Opcao.Sim : EnumeradoresDllInner.Opcao.Nao), 1, 0, 0);
                        }
                    }
                    else if (InnerAtual.TipoEquipamento == (int)EnumeradoresDllInner.Acionamento.AcionamentoCatracaSaida)
                    {
                        if (InnerAtual.CatracaInvertida)
                        {
                            EasyInner.DefinirEntradasMudancaOffLine((byte)(InnerAtual.HabilitaTeclado ? EnumeradoresDllInner.Opcao.Sim : EnumeradoresDllInner.Opcao.Nao), 1, 1, 0);
                        }
                        else
                        {
                            EasyInner.DefinirEntradasMudancaOffLine((byte)(InnerAtual.HabilitaTeclado ? EnumeradoresDllInner.Opcao.Sim : EnumeradoresDllInner.Opcao.Nao), 2, 2, 0);
                        }
                    }
                    else
                    {
                        // Configura entradas mudança OffLine 
                        if (InnerAtual.CatracaInvertida)
                        {
                            EasyInner.DefinirEntradasMudancaOffLine((byte)(InnerAtual.HabilitaTeclado ? EnumeradoresDllInner.Opcao.Sim : EnumeradoresDllInner.Opcao.Nao),
                            (byte)(InnerAtual.DoisLeitores ? 1 : 4), (byte)(InnerAtual.DoisLeitores ? 2 : 0), 0);
                        }
                        else
                        {
                            EasyInner.DefinirEntradasMudancaOffLine((byte)(InnerAtual.HabilitaTeclado ? EnumeradoresDllInner.Opcao.Sim : EnumeradoresDllInner.Opcao.Nao),
                            (byte)(InnerAtual.DoisLeitores ? 1 : 3), (byte)(InnerAtual.DoisLeitores ? 2 : 0), 0);
                        }
                    }
                }

                //Define mensagem de Alteração Online -> Offline.
                EasyInner.DefinirMensagemPadraoMudancaOffLine(1, "    OFF LINE    ");

                //Define mensagem de Alteração OffLine -> OnLine.
                EasyInner.DefinirMensagemPadraoMudancaOnLine(1, "Modo Online");

                //Envia Configurações.
                Ret = EasyInner.EnviarConfiguracoesMudancaAutomaticaOnLineOffLine(InnerAtual.Numero);

                if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
                {
                    InnerAtual.Tentativas = 0;

                    //InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.ESTADO_ENVIAR_MSG_PADRAO;
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarCFGOnline;
                    InnerAtual.TempoColeta = DateTime.Now;
                    InnerAtual.TentativasColeta = 0;
                }
                else
                {
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                    }
                    InnerAtual.Tentativas++;
                }
            }
            catch (Exception)
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
            }
        }
        /// <summary>
        /// Passo responsável pelo envio das mensagens off-line para o Inner
        /// </summary>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_ENVIAR_MSG_OFF_CATRACA(Inner InnerAtual)
        {
            int Ret = -1;
            try
            {
                if (!InnerAtual.CatracaInvertida || (InnerAtual.TipoEquipamento == (int)EnumeradoresDllInner.Acionamento.AcionamentoCatracaEntradaEhSaida
                                                                                     && InnerAtual.CatracaInvertida))
                {
                    //Mensagem Entrada e Saida Offline Liberado!
                    EasyInner.DefinirMensagemEntradaOffLine(1, "ENTRADA LIBERADA.");
                    EasyInner.DefinirMensagemSaidaOffLine(1, "SAIDA LIBERADA.");

                }
                else
                {
                    //Mensagem Entrada e Saida Offline Liberado!
                    EasyInner.DefinirMensagemEntradaOffLine(1, "SAIDA LIBERADA.");
                    EasyInner.DefinirMensagemSaidaOffLine(1, "ENTRADA LIBERADA.");
                }

                EasyInner.DefinirMensagemPadraoOffLine(1, "    OFF LINE    ");

                Ret = EasyInner.EnviarMensagensOffLine(InnerAtual.Numero);

                if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
                {
                    InnerAtual.Tentativas = 0;
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarDataHora;
                }
                else
                {
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                    }
                    InnerAtual.Tentativas++;
                }
            }
            catch (Exception)
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
            }
        }
        /// <summary>
        /// Configura modo Offline
        /// Próximo passo: EstadoColetarBilhetes ????
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PassoEstadoEnviarCFGOffLine(Inner InnerAtual)
        {
            int ret = 0;
            try
            {
                Thread.BeginCriticalRegion();

                //Preenche os campos de configuração do Inner
                var offLine = EnumeradoresDllInner.modoComunicacao.ModoOFFLINE;
                MontaConfiguracaoInner(InnerAtual, offLine);

                ret = EasyInner.EnviarConfiguracoes(InnerAtual.Numero);

                //Envia o comando de configuração
                Application.DoEvents();
                InnerAtual.Tentativas++;

                if (ret == _retornoComSucesso)
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarListaOffLine;
                    InnerAtual.Tentativas = 0;
                    Thread.EndCriticalRegion();
                }
                else if (InnerAtual.Tentativas++ >= 3)
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                }
            }
            catch (Exception)
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
            }
        }

        private void PASSO_ESTADO_ENVIAR_LISTA_SEMDIGITAL(Inner InnerAtual)
        {
            try
            {
                int ret = 0;
                if (InnerAtual.HabilitaListaBioSemDigital)
                {

                    DAOUsuariosBio AcessoSD = new DAOUsuariosBio();
                    List<UsuarioSemDigital> ListaSD = AcessoSD.ConsultarUsuariosSD();
                    Application.DoEvents();
                    //Chama rotina que monta o buffer de cartoes que Nao irao precisar da digital
                    MontarBufferListaSemDigital(InnerAtual, ListaSD);
                    //Envia o buffer com a lista de usuarios sem digital
                    if (InnerAtual.InnerNetAcesso)
                    {
                        ret = EasyInner.EnviarListaUsuariosSemDigitalBioVariavel(InnerAtual.Numero, InnerAtual.QtdDigitos);
                    }
                    else
                    {
                        ret = EasyInner.EnviarListaUsuariosSemDigitalBio(InnerAtual.Numero);
                    }
                }
                if (ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
                {
                    if (InnerAtual.InnerNetAcesso)
                    {
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReceberQTDBilhetesOFF;
                    }
                    else
                    {
                        InnerAtual.TempoColeta = DateTime.Now;
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoColetarBilhetes;
                    }
                }
                else
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                }
            }
            catch (Exception)
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
            }
        }

        private void PassoEstadoEnviarListaOffLine(Inner InnerAtual)
        {
            int Ret = 0;

            //Define Lista e horários offline
            if (InnerAtual.HabilitaListaOffLine)
            {
                MontarHorarios(InnerAtual);

                List<Usuarios> ListaUsuarios = AcessoLista.ConsultarUsuarios(0);
                MontarListaLivre(InnerAtual, ListaUsuarios);

                //Define qual tipo de lista(controle) de acesso o Inner vai utilizar.
                //Utilizar lista branca (cartões fora da lista tem o acesso negado).
                Ret = EasyInner.DefinirTipoListaAcesso(1);
            }
            else
            {
                //Não utilizar a lista de acesso.
                EasyInner.DefinirTipoListaAcesso(0);
            }

            if (Ret == _retornoComSucesso)
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarListaSemDigital;
            }
            else
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
            }
        }

        /// <summary>
        /// Mantém a mensagem no display por 2 segundos.
        /// Próximo passo: ESTADO_ENVIAR_MSG_PADRAO
        /// </summary>
        /// <param name="innerAtual"></param>
        private void PassoEstadoAguardaTempoMemsagem(Inner innerAtual)
        {
            try
            {
                //Após passar os 2 segundos volta para o passo enviar mensagem padrão
                var tempo = DateTime.Now - innerAtual.TempoInicialMensagem;
                if (tempo.Seconds >= 2)
                {
                    if (innerAtual.InnerNetAcesso)
                    {
                        EasyInner.DesligarLedVermelho(innerAtual.Numero);
                    }

                    innerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGPadrao;
                }
            }
            catch (Exception)
            {
                innerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
            }
        }

        /// <summary>
        /// Efetua a coleta dos bilhetes no modo Off-line
        /// Próximo passo: EstadoEnviarCFGOnline
        /// </summary>
        /// <param name="InnerAtual"></param>
        private void PassoEstadoColetarBilhetes(Inner InnerAtual) // validar qual tipo bilhete vai ser usado aqui ????
        {
            if (InnerAtual.InnerNetAcesso)
            {
                ColetarBilhetesInnerAcesso(InnerAtual);
            }
            else
            {
                ColetarBilhetesInnerNet(InnerAtual);
            }
        }

        /// <summary>
        /// Configura modo On-line
        /// Próximo passo: EstadoEnviarDataHora
        /// </summary>
        /// <param name="InnerAtual"></param>
        private void PassoEstadoEnviarCFGOnline(Inner InnerAtual)
        {
            try
            {
                Thread.BeginCriticalRegion();

                //Monta configuração modo Online
                var onLine = EnumeradoresDllInner.modoComunicacao.ModoOnLine;
                MontaConfiguracaoInner(InnerAtual, onLine);

                //Envia as configurações ao Inner Atual.
                Application.DoEvents();
                int ret = EasyInner.EnviarConfiguracoes(InnerAtual.Numero);
                InnerAtual.Tentativas++;

                if (ret == _retornoComSucesso)
                {
                    //Caso consiga enviar as configurações, 
                    //passa para o passo Enviar Data Hora
                    InnerAtual.Tentativas = 0;
                    //InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarConfigMudOnLineOffLine;

                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGPadrao;
                    Thread.EndCriticalRegion();
                }
                else if (InnerAtual.Tentativas >= 3)//caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                }

            }
            catch (Exception)
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
            }

        }

        /// <summary>
        /// Envia ao Inner data e hora atual
        /// Próximo passo: EstadoEnviarMSGPadrao
        /// </summary>
        /// <param name="InnerAtual"></param>
        private void PassoEstadoEnviarDataHora(Inner InnerAtual)
        {
            try
            {
                //Declaração de Variáveis..
                int Ret = -1;
                System.DateTime Data;
                Data = System.DateTime.Now;

                //Envia Comando de Relógio ao Inner Atual..
                Ret = EasyInner.EnviarRelogio(
                    InnerAtual.Numero,
                    (byte)Data.Day,
                    (byte)Data.Month,
                    System.Convert.ToByte(Data.Year.ToString().Substring(2, 2)),
                    (byte)Data.Hour,
                    (byte)Data.Minute,
                    (byte)Data.Second);

                //Testa o Retorno do comando de Envio de Relógio..
                if (Ret == _retornoComSucesso)
                {
                    //Vai para o passo de Envio de Msg Padrão..
                    InnerAtual.Tentativas = 0;
                    //InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.ESTADO_ENVIAR_CFG_ONLINE;
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarCFGOnLineOffLine;
                }
                else
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                    }

                    //Adiciona 1 ao contador de tentativas
                    InnerAtual.Tentativas++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
            }
        }

        /// <summary>
        /// CONFIGURAR_ENTRADAS_ONLINE
        /// Preparação configuração online para entrar em modo Polling
        /// Próximo passo: EstadoPolling
        /// </summary>
        /// <param name="InnerAtual"></param>
        private void PassoEstadoConfigurarEntradasOnline(Inner InnerAtual)
        {
            try
            {
                //Declaração de variáveis..
                int Ret = -1;

                //Converte Binário para Decimal
                int ValorDecimal = ConfiguraEntradasMudancaOnLine(InnerAtual); //Ver no manual Anexo III


                var digitos = (byte)InnerAtual.QtdDigitos;
                var simEcoTeclado = (byte)1;
                var valor = (byte)ValorDecimal;
                var tempoTeclado = (byte)15;
                var posicaoCurso = (byte)17;
                Ret = EasyInner.EnviarFormasEntradasOnLine(InnerAtual.Numero,
                                                           digitos,           //Qtd Digitos Teclado..
                                                           simEcoTeclado,     //Eco do Teclado no Display..
                                                           valor,             //Valor decimal resultante da conversão Binário para Decimal
                                                           tempoTeclado,      //Tempo teclado..
                                                           posicaoCurso);     //Posição do Cursor no Teclado..

                //Testa o retorno do comando..
                if (Ret == _retornoComSucesso)
                {
                    //Vai para o Estado De Polling.
                    InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                    InnerAtual.Tentativas = 0;
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadosPolling;

                    FuncoesCatraca(true, true);
                }
                else
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                    }
                    //Adiciona 1 ao contador de tentativas
                    InnerAtual.Tentativas++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
            }
        }

        /// <summary>
        /// Mostra mensagem para que seja informado se é entrada ou saída
        /// Este estado configura a mensagem padrão que será exibida no dispositivo em seu
        /// funcionamento Online utilizando o método EnviarMensagemPadraoOnline.
        /// O passo posterior a este estado é o passo de configuração de entradas online,
        /// ou em caso de erro pode retornar para o estado de conexão após alcançar o
        /// número máximo de tentativas.
        /// Próximo passo: ESTADO_POLLING
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_DEFINICAO_TECLADO(Inner InnerAtual)
        {
            int Ret = -1;
            //Envia mensagem Padrão Online..
            Ret = EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "ENTRADA OU SAIDA?");
            Ret = EasyInner.EnviarFormasEntradasOnLine(InnerAtual.Numero,
                         0, //Quantidade de Digitos do Teclado.. (Não aceita digitação numérica)
                         0, //0 – não ecoa
                         (int)EnumeradoresDllInner.EnviarFormasEntradasOnLine.EntradasOnAceitaTeclado,
                         10, // Tempo de entrada do Teclado (10s).
                         32);//Posição do Cursor (32 fica fora..)

            //Se Retorno OK, vai para proximo estado..
            if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
            {
                InnerAtual.Tentativas = 0;
                InnerAtual.EstadoTeclado = EnumeradoresDllInner.EstadosTeclado.AguardadoTeclado;
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoAguardaDefinicaoTeclado;
            }
            else
            {
                //Caso o retorno não for OK, tenta novamente até 3x..
                if (InnerAtual.Tentativas++ > 3)
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
                }
            }
        }

        /// <summary>
        /// PASSO_ESTADO_AGUARDAR_DEFINICAO_TECLADO
        /// Aguarda a resposta do teclado (Entrada, Saida, anula ou confirma)
        /// Proximo estado: ESTADO_POLLING
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_AGUARDA_DEFINICAO_TECLADO(Inner InnerAtual)
        {
            byte Origem = 0;
            byte Complemento = 0;
            StringBuilder Cartao = new StringBuilder();
            byte Dia = 0;
            byte Mes = 0;
            byte Ano = 0;
            byte Hora = 0;
            byte Minuto = 0;
            byte Segundo = 0;
            int Ret = -1;

            //Atribui Temporizador
            InnerAtual.Temporizador = DateTime.Now;

            try
            {
                Thread.BeginCriticalRegion();

                //Envia o Comando Receber Dados Online..
                Ret = EasyInner.ReceberDadosOnLine(InnerAtual.Numero,
                    ref Origem, ref Complemento, Cartao, ref Dia, ref Mes, ref Ano, ref Hora,
                    ref Minuto, ref Segundo);

                //Testa o Retorno do Comando..
                if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
                {
                    //Se está aguardando retorno (entrada ou saída)
                    if (InnerAtual.EstadoTeclado == EnumeradoresDllInner.EstadosTeclado.AguardadoTeclado)
                    {
                        //****************************************************
                        //Entrada, saída liberada, confirma, anula ou função tratar mensagem
                        //66 - "Entrada" via teclado
                        //67 - "Saída" via teclado
                        //35 - "Confirma" via teclado
                        //42 - "Anula" via teclado
                        //65 - "Função" via teclado
                        if (Convert.ToInt16(Complemento.ToString()) == (int)EnumeradoresDllInner.Origem.TECLA_ENTRADA) //entrada
                        {
                            HabilitaLadoGiroDaCatraca("Entrada", InnerAtual.CatracaInvertida);
                            InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoLiberarCatraca;
                        }
                        else if (Convert.ToInt16(Complemento.ToString()) == (int)EnumeradoresDllInner.Origem.TECLA_SAIDA)   //saída
                        {
                            HabilitaLadoGiroDaCatraca("Saida", InnerAtual.CatracaInvertida);
                            InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoLiberarCatraca;
                        }
                        else if (Convert.ToInt16(Complemento.ToString()) == (int)EnumeradoresDllInner.Origem.TECLA_CONFIRMA) //confirma
                        {
                            InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoLiberarCatraca;
                        }
                        else if (Convert.ToInt16(Complemento.ToString()) == (int)EnumeradoresDllInner.Origem.TECLA_ANULA) //anula
                        {
                            EasyInner.LigarLedVerde(InnerAtual.Numero);
                            InnerAtual.TempoInicialMensagem = DateTime.Now;
                            InnerAtual.Tentativas = 0;
                            InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGPadrao;
                        }
                        else if (Convert.ToInt16(Complemento.ToString()) == (int)EnumeradoresDllInner.Origem.TECLA_FUNCAO) //função
                        {
                            InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoDefinicaoTeclado;
                        }
                        InnerAtual.EstadoTeclado = EnumeradoresDllInner.EstadosTeclado.TecladoEmBranco;
                    }
                }
                else
                {
                    //Se passar 3 segundos sem receber nada, passa para o estado enviar ping on line, para manter o equipamento em on line.
                    TimeSpan tempo = DateTime.Now - InnerAtual.TempoInicialPingOnLine;
                    if (tempo.Seconds >= 3)
                    {
                        InnerAtual.EstadoSolicitacaoPingOnLine = InnerAtual.EstadoAtual;
                        InnerAtual.Tentativas = 0;
                        InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoPingOnline;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
            }
        }

        /// <summary>
        /// É onde funciona todo o processo do modo online
        /// Passagem de cartão, catraca, urna, mensagens...
        /// </summary>
        /// <param name="InnerAtual"></param>
        private void PassoEstadoPolling(Inner InnerAtual)
        {
            try
            {
                //Declaração de Variáveis..
                byte Origem = 0;
                byte Complemento = 0;
                var Cartao = new StringBuilder();
                byte Dia = 0;
                byte Mes = 0;
                byte Ano = 0;
                byte Hora = 0;
                byte Minuto = 0;
                byte Segundo = 0;
                string strCartao = string.Empty;
                int Ret = -1;
                string NumCartao = string.Empty;

                Thread.BeginCriticalRegion();

                if (InnerAtual.TipoLeitor == (int)EnumeradoresDllInner.TipoLeitor.QRCodeLetras)
                {
                    Ret = EasyInner.ReceberDadosOnLine_ComLetras(InnerAtual.Numero,
                                                                 ref Origem,
                                                                 ref Complemento,
                                                                 Cartao,
                                                                 ref Dia,
                                                                 ref Mes,
                                                                 ref Ano,
                                                                 ref Hora,
                                                                 ref Minuto,
                                                                 ref Segundo);
                }
                else
                {
                    //Envia o Comando Receber Dados Online..
                    Ret = EasyInner.ReceberDadosOnLine(InnerAtual.Numero,
                                                       ref Origem,
                                                       ref Complemento,
                                                       Cartao,
                                                       ref Dia,
                                                       ref Mes,
                                                       ref Ano,
                                                       ref Hora,
                                                       ref Minuto,
                                                       ref Segundo);
                }

                //Atribui Temporizador
                InnerAtual.Temporizador = DateTime.Now;

                //Testa o Retorno do Comando..
                if (Ret == _retornoComSucesso)
                {
                    //Teste se a origem é Fim de Acionamento, Função, Anula ou Giro de Dispositivo..
                    //Caso seja alguma destas origens, retorna para a maquina de estados.
                    var ehTeclaFuncao = Complemento == (int)EnumeradoresDllInner.Origem.TECLA_FUNCAO;
                    var ehTeclaAnula = Complemento == (int)EnumeradoresDllInner.Origem.TECLA_ANULA;
                    var ohCartaoEstaVazio = ((Cartao.Length == 0)
                                            && !(InnerAtual.EstadoTeclado == EnumeradoresDllInner.EstadosTeclado.AguardadoTeclado));

                    if (ehTeclaFuncao || ehTeclaAnula || ohCartaoEstaVazio)
                    {
                        //Zera contador de tentativas
                        InnerAtual.Tentativas = 0;
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoAguardaTempoMensagem;
                        return;
                    }

                    InnerAtual.BilheteOnline.Origem = Origem;
                    InnerAtual.BilheteOnline.Complemento = Complemento;
                    InnerAtual.BilheteOnline.Cartao = Cartao;
                    InnerAtual.BilheteOnline.Dia = Dia;
                    InnerAtual.BilheteOnline.Mes = Mes;
                    InnerAtual.BilheteOnline.Ano = Ano;
                    InnerAtual.BilheteOnline.Hora = Hora;
                    InnerAtual.BilheteOnline.Minuto = Minuto;
                    InnerAtual.BilheteOnline.Segundo = Segundo;

                    MontarBilheteRecebido(InnerAtual);
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoValidarAcesso;
                    return;
                }

                //Se passar 3 segundos sem receber nada, 
                //passa para o estado enviar ping on line, 
                //para manter o equipamento em on line.
                var tempo = DateTime.Now - InnerAtual.TempoInicialPingOnLine;
                if (tempo.Seconds >= 5)
                {
                    InnerAtual.EstadoSolicitacaoPingOnLine = InnerAtual.EstadoAtual;
                    InnerAtual.Tentativas = 0;
                    InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoPingOnline;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
            }
        }

        /// <summary>
        /// Envia mensagem padrão modo Online
        /// Próximo passo: ESTADO_CONFIGURAR_ENTRADAS_ONLINE
        /// </summary>
        /// <param name="innerAtual"></param>
        private void PassoEstadoEnviarMSGPadrao(Inner innerAtual)
        {
            try
            {
                //Testa o Retorno do comando de Envio de Mensagem Padrão On Line
                var exibirDataHora = (byte)1;
                var mensagemEnviada = EasyInner.EnviarMensagemPadraoOnLine(innerAtual.Numero, exibirDataHora, "Modo Online");
                var podeContinuarProximoPasso = mensagemEnviada == _retornoComSucesso;
                if (podeContinuarProximoPasso)
                {
                    //Muda o passo para configuração de entradas Online.
                    innerAtual.Tentativas = 0;
                    innerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConfigurarEntradasOnline;
                }
                else
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    if (innerAtual.Tentativas >= 3)
                    {
                        innerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                    }

                    //Adiciona 1 ao contador de tentativas
                    innerAtual.Tentativas++;
                }
            }
            catch (Exception ex)
            {
                innerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// Método responsável pela liberação de acesso. Somente usuarios listado
        /// serão liberados. Esta consulta deverá ser feita em sua base de dados.
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_ENVIAR_MSG_ACESSO_NEGADO(Inner InnerAtual)
        {
            int ret = -1;

            ret = (byte)EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 1, " ACESSO NEGADO!");

            if (ret == (byte)EnumeradoresDllInner.Retorno.RetornoComandoOk)
            {
                EasyInner.AcionarBipLongo(InnerAtual.Numero);
                if (InnerAtual.InnerNetAcesso)
                    EasyInner.LigarLedVermelho(InnerAtual.Numero);

                InnerAtual.TempoInicialMensagem = DateTime.Now;
                InnerAtual.Tentativas = 0;
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoAguardaTempoMensagem;
            }
            else
            {
                if (InnerAtual.Tentativas >= 3)
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                }
                //Adiciona 1 ao contador de tentativas
                InnerAtual.Tentativas++;
            }
        }
        /// <summary>
        /// Envia mensagem padrão estado Urna
        /// Próximo passo: ESTADO_MONITORA_URNA
        /// </summary>
        /// <param name="innerAtual"></param>
        private void PASSO_ESTADO_ENVIAR_MSG_URNA(Inner innerAtual)
        {
            try
            {
                //Testa o Retorno do comando de Envio de Mensagem padrão Urna    
                if (EasyInner.EnviarMensagemPadraoOnLine(innerAtual.Numero, 0, " DEPOSITE O       CARTAO") == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
                {
                    innerAtual.Tentativas = 0;
                    innerAtual.TentativasUrna = 0;
                    innerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoMonitoramentoUrna;
                }
                else
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    if (innerAtual.Tentativas >= 3)
                    {
                        innerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                    }
                    //Adiciona 1 ao contador de tentativas
                    innerAtual.Tentativas++;
                }
            }
            catch (Exception)
            {
                innerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
            }
        }
        /// <summary>
        /// Libera a catraca de acordo com o lado informado
        /// Próximo Passo: ESTADO_MONITORA_GIRO_CATRACA
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_LIBERA_GIRO_CATRACA(Inner InnerAtual)
        {
            try
            {
                //Declaração de Variáveis..
                int Ret = -1;

                //Envia comando de liberar a catraca para Entrada.
                if (_liberaEntrada)
                {
                    EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "                ENTRADA LIBERADA");
                    _liberaEntrada = false;
                    Ret = EasyInner.LiberarCatracaEntrada(InnerAtual.Numero);
                }
                else
                {
                    if (_liberaEntradaInvertida)
                    {
                        EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "                ENTRADA LIBERADA");
                        _liberaEntradaInvertida = false;
                        Ret = EasyInner.LiberarCatracaEntradaInvertida(InnerAtual.Numero);
                    }
                    else
                    {
                        //Envia comando de liberar a catraca para Saída.
                        if (_liberaSaida)
                        {
                            EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "                 SAIDA LIBERADA");
                            _liberaSaida = false;
                            Ret = EasyInner.LiberarCatracaSaida(InnerAtual.Numero);
                        }
                        else
                        {
                            if (_liberaSaidaInvertida)
                            {
                                EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "                 SAIDA LIBERADA");
                                _liberaSaidaInvertida = false;
                                Ret = EasyInner.LiberarCatracaSaidaInvertida(InnerAtual.Numero);
                            }
                            else
                            {
                                EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "LIBERADO DOIS SENTIDOS");
                                EasyInner.AcionarBipCurto(InnerAtual.Numero);
                                Ret = EasyInner.LiberarCatracaDoisSentidos(InnerAtual.Numero);
                            }
                        }
                    }
                }

                //Testa Retorno do comando..
                if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
                {
                    InnerAtual.CountPingFail = 0;
                    InnerAtual.Tentativas = 0;
                    InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoMonitorarGiroCatraca;
                }
                else
                {
                    //Se o retorno for diferente de 0 tenta liberar a catraca 3 vezes, caso não consiga enviar o comando volta para o passo reconectar.
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.Tentativas = 0;
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                    }
                    //Adiciona 1 ao contador de tentativas
                    InnerAtual.Tentativas++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
            }
        }
        /// <summary>
        /// Verifica se a catraca foi girada ou não e caso Sim para qual lado.
        /// Próximo Passo: ESTADO_ENVIAR_MSG_PADRAO
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_MONITORA_GIRO_CATRACA(Inner InnerAtual)
        {
            try
            {
                //Declaração de Variáveis..
                Bilhete Bilhetes = new Bilhete();
                Bilhetes.Origem = 0;
                Bilhetes.Complemento = 0;
                Bilhetes.Cartao = null;
                StringBuilder Cartao = new StringBuilder();
                Bilhetes.Dia = 0;
                Bilhetes.Mes = 0;
                Bilhetes.Ano = 0;
                Bilhetes.Hora = 0;
                Bilhetes.Minuto = 0;
                Bilhetes.Segundo = 0;
                string strCartao = string.Empty;
                int Ret = -1;

                //Monitora o giro da catraca..
                Ret = EasyInner.ReceberDadosOnLine(InnerAtual.Numero,
                    ref Bilhetes.Origem, ref Bilhetes.Complemento, Cartao, ref Bilhetes.Dia, ref Bilhetes.Mes, ref Bilhetes.Ano, ref Bilhetes.Hora,
                    ref Bilhetes.Minuto, ref Bilhetes.Segundo);

                //Testa o retorno do comando..
                if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
                {
                    //Testa se girou o não a catraca..
                    if (Bilhetes.Origem == (int)EnumeradoresDllInner.Origem.FIM_TEMPO_ACIONAMENTO)
                    {
                        AtulizarLabelDados("Não girou a catraca!");
                    }
                    else if (Bilhetes.Origem == (int)EnumeradoresDllInner.Origem.GIRO_DA_CATRACA_TOPDATA)
                    {
                        AtulizarLabelDados("Girou a catraca para " + (Bilhetes.Complemento - Convert.ToInt16(InnerAtual.CatracaInvertida) == 0 ? "entrada." : "saída.").ToString());
                    }
                    FuncoesCatraca(true, true);

                    //Vai para o estado de Envio de Msg Padrão..
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGPadrao;
                }
                else
                {
                    //Caso o tempo que estiver monitorando o giro chegue a 3 segundos,
                    //deverá enviar o ping on line para manter o equipamento em modo on line
                    TimeSpan tempo = DateTime.Now - InnerAtual.TempoInicialPingOnLine;
                    if (tempo.Seconds >= 3)
                    {
                        InnerAtual.EstadoSolicitacaoPingOnLine = InnerAtual.EstadoAtual;
                        InnerAtual.Tentativas = 0;
                        InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoPingOnline;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);

            }
        }
        /// <summary>
        /// Monitora o depósito do cartão na Urna
        /// Próximo passo: ESTADO_LIBERAR_CATRACA
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_MONITORA_URNA(Inner InnerAtual)
        {
            try
            {
                //Declaração de Variáveis..
                Bilhete Bilhetes = new Bilhete();
                Bilhetes.Origem = 0;
                Bilhetes.Complemento = 0;
                Bilhetes.Cartao = null;
                StringBuilder Cartao = new StringBuilder();
                Bilhetes.Dia = 0;
                Bilhetes.Mes = 0;
                Bilhetes.Ano = 0;
                Bilhetes.Hora = 0;
                Bilhetes.Minuto = 0;
                Bilhetes.Segundo = 0;
                string strCartao = string.Empty;
                int Ret = -1;

                //Monitora o giro da catraca..
                Ret = EasyInner.ReceberDadosOnLine(InnerAtual.Numero,
                    ref Bilhetes.Origem, ref Bilhetes.Complemento, Cartao, ref Bilhetes.Dia, ref Bilhetes.Mes, ref Bilhetes.Ano, ref Bilhetes.Hora,
                    ref Bilhetes.Minuto, ref Bilhetes.Segundo);

                //Testa o retorno do comando..
                if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
                {
                    //Testa se a urna recolheu o cartão
                    if (Bilhetes.Origem == (int)EnumeradoresDllInner.Origem.URNA)
                    {
                        AtulizarLabelDados("URNA RECOLHEU CARTÃO");

                        //Vai para o estado de Envio de Msg Padrão..
                        _liberaSaida = true;
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoLiberarCatraca;
                    }
                    //Senão depositou o cartão mostra mensagem e bloqueia o acesso
                    else if (Bilhetes.Origem == (int)EnumeradoresDllInner.Origem.FIM_TEMPO_ACIONAMENTO)
                    {
                        AtulizarLabelDados("NÃO DEPOSITOU CARTÃO");
                        EasyInner.AcionarBipLongo(InnerAtual.Numero);

                        //Vai para o estado de Envio de Msg Padrão..
                        InnerAtual.TempoInicialMensagem = DateTime.Now;
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGAcessoNegado;
                    }
                }
                else
                {
                    //Caso o tempo que estiver monitorando o giro chegue a 3 segundos,
                    //deverá enviar o ping on line para manter o equipamento em modo on line
                    TimeSpan tempo = DateTime.Now - InnerAtual.TempoInicialPingOnLine;
                    if (tempo.Seconds >= 3)
                    {
                        InnerAtual.EstadoSolicitacaoPingOnLine = InnerAtual.EstadoAtual;
                        InnerAtual.Tentativas = 0;
                        InnerAtual.TentativasUrna += 1;
                        InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoPingOnline;
                    }

                    // Caso não teve retorno da urna
                    if (InnerAtual.TentativasUrna == 3)
                    {
                        AtulizarLabelDados("SEM RETORNO DA URNA");
                        EasyInner.AcionarBipLongo(InnerAtual.Numero);

                        //Vai para o estado de Envio de Msg Padrão..
                        InnerAtual.TempoInicialMensagem = DateTime.Now;
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGAcessoNegado;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);

            }
        }
        /// <summary>
        /// Testa comunicação com o Inner e mantém o Inner em OnLine quando a mudança
        ///automática está configurada. Especialmente indicada para a verificação da
        ///conexão em comunicação TCP/IP.
        ///Próximo Passo: RETORNA MÉTODO QUE O ACIONOU
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_ENVIAR_PING_ONLINE(Inner InnerAtual)
        {
            try
            {

                //Envia o comando de PING ON LINE, se o retorno for OK volta para o estado onde chamou o método
                int retorno = EasyInner.PingOnLine(InnerAtual.Numero);
                if (retorno == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
                {
                    InnerAtual.EstadoAtual = InnerAtual.EstadoSolicitacaoPingOnLine;
                    InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                }
                else
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                    }
                    //Adiciona 1 ao contador de tentativas
                    InnerAtual.Tentativas++;
                }
            }
            catch (Exception)
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
            }
        }
        /// <summary>
        /// Se a conexão cair tenta conectar novamente
        /// Próximo Passo: ESTADO_ENVIAR_CFG_OFFLINE
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_RECONECTAR(Inner InnerAtual)
        {
            try
            {
                int count = 0;
                int Ret = -1;

                //Verifica tempo
                TimeSpan tempo = DateTime.Now - InnerAtual.TempoInicialPingOnLine;
                if (tempo.Seconds < 10)
                {
                    return;
                }
                InnerAtual.TempoInicialPingOnLine = DateTime.Now;

                AtualizarEstadoInner(InnerAtual.Numero, InnerAtual.EstadoAtual);

                do
                {
                    Ret = TestaConexaoInner(InnerAtual);
                    Thread.Sleep(300);

                } while ((count++ < 10) && (Ret != (int)EnumeradoresDllInner.Retorno.RetornoComandoOk));

                if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
                {
                    //Zera as variáveis de controle da maquina de estados.
                    InnerAtual.Tentativas = 0;
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReceberFirmWare;
                }
                else
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                    }
                    InnerAtual.Tentativas++;
                }
                InnerAtual.CountRepeatPingOnline = 0;
            }
            catch (Exception ex)
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoConectar;
                Console.WriteLine(ex);
            }


        }

        private void PASSO_ESTADO_RECEBER_QTD_BILHETES_OFF(Inner InnerAtual)
        {
            int Ret = -1;
            int[] receber = new int[2];
            Ret = EasyInner.ReceberQuantidadeBilhetes(InnerAtual.Numero, receber);
            if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
            {
                InnerAtual.BilhetesAReceber = receber[0];
                if (InnerAtual.BilhetesAReceber > 0)
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoColetarBilhetes;
                }
                else
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGOffLineCatraca;
                }
            }
            else
            {
                if (InnerAtual.Tentativas++ > 3)
                {
                    InnerAtual.Tentativas = 0;
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                }
            }
        }

        private void PassoEstadoValidarAcesso(Inner InnerAtual)
        {
            if (LiberarAcesso(InnerAtual.BilheteOnline.Cartao.ToString()) == false)
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGAcessoNegado;

                //Se 1 leitor
                //E Urna ou entrada e saída ou liberada 2 sentidos ou sentido giro
                //E cartão = proximidade
            }
            else if (((InnerAtual.DoisLeitores == false) &&
                        ((InnerAtual.TipoEquipamento == (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaUrna)
                        || (InnerAtual.TipoEquipamento == (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaEntradaEhSaida)
                        || (InnerAtual.TipoEquipamento == (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaLiberadaDoisSentidos)
                        || (InnerAtual.TipoEquipamento == (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaSentidoGiro))
                        && ((InnerAtual.TipoLeitor == 2) || (InnerAtual.TipoLeitor == 3) || (InnerAtual.TipoLeitor == 4))))
            {
                if (InnerAtual.EstadoTeclado == EnumeradoresDllInner.EstadosTeclado.TecladoEmBranco)
                {
                    //Apresenta mensagem para informa se é entrada ou saída
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoDefinicaoTeclado;
                }

                //Se estamos trabalhando com Urna e 1 leitor
                if ((InnerAtual.TipoEquipamento == (byte)EnumeradoresDllInner.Acionamento.AcionamentoCatracaUrna))
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarMSGUrna;
                }
            }
            else if (InnerAtual.TipoEquipamento == (byte)EnumeradoresDllInner.Acionamento.AcionamentoColetor)
            {
                //aciona Rele
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoAcionarRele1;

                /*   Para Inner acionando duas portas
                if (InnerAtual.BilheteOnline.Origem == (int)EnumeradoresDllInner.Origem.VIA_LEITOR2)
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.ESTADO_ACIONAR_RELE2;
                }
                else
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.ESTADO_ACIONAR_RELE1;
                }   */

            }
            else
            {
                //somente entrada
                if (InnerAtual.TipoEquipamento == (int)EnumeradoresDllInner.Acionamento.AcionamentoCatracaEntrada)
                {
                    HabilitaLadoGiroDaCatraca("Entrada", InnerAtual.CatracaInvertida);
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoLiberarCatraca;
                }
                //somente saida
                else if (InnerAtual.TipoEquipamento == (int)EnumeradoresDllInner.Acionamento.AcionamentoCatracaSaida)
                {
                    HabilitaLadoGiroDaCatraca("Saida", InnerAtual.CatracaInvertida);
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoLiberarCatraca;
                }

                //saida liberada
                else if (InnerAtual.TipoEquipamento == (int)EnumeradoresDllInner.Acionamento.AcionamentoCatracaSaidaLiberada)
                {
                    HabilitaLadoGiroDaCatraca("Entrada", InnerAtual.CatracaInvertida);
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoLiberarCatraca;
                }

                //entrada liberada
                else if (InnerAtual.TipoEquipamento == (int)EnumeradoresDllInner.Acionamento.AcionamentoCatracaEntradaLiberada)
                {
                    HabilitaLadoGiroDaCatraca("Saida", InnerAtual.CatracaInvertida);
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoLiberarCatraca;
                }
                //Se Urna e 2 leitores
                else if ((InnerAtual.TipoEquipamento == (int)EnumeradoresDllInner.Acionamento.AcionamentoCatracaUrna)
                    && (InnerAtual.BilheteOnline.Origem == (int)EnumeradoresDllInner.Origem.VIA_LEITOR2))
                {
                    EasyInner.AcionarRele2(InnerAtual.Numero);
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoValidaUrnaCheia;
                }
                else
                {
                    EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "Acesso Liberado!");
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoLiberarCatraca;
                }
            }
        }

        private void PassoEstadoReceberFirmWare(Inner InnerAtual) // validar qual versão e do cliente 
        {
            byte Linha = 0;
            short Variacao = 0;
            byte VersaoBaixa = 0;
            byte VersaoSufixo = 0;
            byte TipoModBio = 0;
            //Para InnerAcesso 2
            //byte ComplementarVersaoSuperior = 0;
            //byte ComplementarVersaoInferior = 0;
            //byte TipoDeOperacao = 0;

            //Solicita a versão do firmware do Inner e dados como o Idioma, 
            //se é uma versão especial.  
            int Ret = EasyInner.ReceberVersaoFirmware6xx(InnerAtual.Numero, ref Linha, ref Variacao, ref VersaoAlta, ref VersaoBaixa, ref VersaoSufixo, ref InnerAcessoBio, ref TipoModBio);

            //Solicita a versão do firmware do InnerAcesso 2 Principal e Complementar.
            //int Ret = EasyInner.ReceberVersaoFirmware6xx_ComComplementar(InnerAtual.Numero, ref Linha, ref Variacao, ref VersaoAlta, ref VersaoBaixa, ref VersaoSufixo, ref InnerAcessoBio, ref TipoModBio, ref ComplementarVersaoSuperior, ref ComplementarVersaoInferior, ref TipoDeOperacao);

            Application.DoEvents();

            if (Ret == _retornoComSucesso)
            {
                //Define a linha do Inner
                switch (Linha)
                {
                    // Varificar qual a linhas catracas trabalha
                    case 1:
                        InnerAtual.LinhaInner = "Inner Plus";
                        break;

                    case 2:
                        InnerAtual.LinhaInner = "Inner Disk";
                        break;

                    case 3:
                        InnerAtual.LinhaInner = "Inner Verid";
                        break;

                    case 6:
                        InnerAtual.LinhaInner = "Inner Bio";
                        break;

                    case 7:
                        InnerAtual.LinhaInner = "Inner NET";
                        InnerAtual.InnerNetAcesso = false;
                        break;

                    case 14:
                        InnerAtual.LinhaInner = "Inner Acesso";
                        InnerAtual.InnerNetAcesso = true;
                        break;

                    case 18:
                        InnerAtual.LinhaInner = "Inner Acesso 2";
                        InnerAtual.InnerNetAcesso = true;
                        break;

                    default:
                        InnerAtual.LinhaInner = "Inner";
                        InnerAtual.InnerNetAcesso = true;
                        break;
                }
                InnerAtual.VariacaoInner = Variacao;
                InnerAtual.VersaoInner = $"{VersaoAlta}.{VersaoBaixa}.{VersaoSufixo}";
                InnerAtual.VersaoFW = VersaoAlta;
                InnerAtual.TipoComBio = TipoModBio;

                //Se selecionado Biometria, valida se o equipamento é compatível
                if (InnerAtual.Biometrico)
                {
                    if ((((Linha != 6) && (Linha != 14) && (Linha != 18)) || ((Linha == 14) && (InnerAcessoBio == 0))))
                    {
                        MessageBox.Show("Equipamento " + InnerAtual.Numero + " não compatível com Biometria.", "Atenção");
                    }
                }
                if (InnerAcessoBio == 1 || Linha == 6)
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReceberModeloBio;
                }
                else
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoErnviarCFGOffLine;
                }
            }
            else
            {
                if (InnerAtual.Tentativas++ >= 3)
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                }
            }
        }

        private void PASSO_ESTADO_RECEBER_MODELO_BIO(Inner InnerAtual)
        {
            int Ret = -1;
            if (InnerAtual.VersaoFW < 6)
            {
                Ret = ReceberModeloBioAVer5(InnerAtual);
            }
            else
            {
                Ret = ReceberModeloBio6xx(InnerAtual);
            }

            if (Ret == _retornoComSucesso)
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReceberVersaoBio;
            }
            else
            {
                if (Ret == (int)EnumeradoresDllInner.RetornoBIO.RetBioModuloIincorreto)
                {
                    PararMaquina();
                    MessageBox.Show("Inner " + InnerAtual.Numero + " com módulo bio incorreto!", "Online");
                }
                else if (InnerAtual.Tentativas++ > 5)
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                }
            }






            //int Ret = -1;
            //if (InnerAtual.VersaoFW < 6)
            //{
            //    Ret = ReceberModeloBioAVer5(InnerAtual);
            //}

            //if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
            //{
            //    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReceberVersaoBio;
            //}
            //else
            //{
            //    if (Ret == (int)EnumeradoresDllInner.RetornoBIO.RetBioModuloIincorreto)
            //    {
            //        PararMaquina();
            //        MessageBox.Show("Inner " + InnerAtual.Numero + " com módulo bio incorreto!", "Online");
            //    }
            //    else if (InnerAtual.Tentativas++ > 5)
            //    {
            //        InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
            //    }
            //}
        }

        private int ReceberModeloBio6xx(Inner innerAtual)
        {
            int Ret = -1;
            Ret = EasyInner.RequisitarModeloBio(innerAtual.Numero, innerAtual.TipoComBio);
            if (Ret == _retornoComSucesso)
            {
                byte[] ModeloBio = new byte[4];
                Ret = EasyInner.RespostaModeloBio(innerAtual.Numero, ModeloBio);
                if (Ret == _retornoComSucesso)
                {
                    innerAtual.ModeloBioInner = "Modelo bio " + Encoding.ASCII.GetString(ModeloBio);
                }
            }
            return Ret;
        }

        private void PASSO_ESTADO_RECEBER_VERSAO_BIO(Inner InnerAtual)
        {
            int Ret = -1;
            if (InnerAtual.VersaoFW < 6)
            {
                Ret = ReceberVersaoBioAVer5(InnerAtual);
            }

            if (Ret == _retornoComSucesso)
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoErnviarCFGOffLine;
            }
            else
            {
                if (Ret == _retornoComSucesso)
                {
                    PararMaquina();
                    MessageBox.Show("Módulo incorreto!", "Online");
                }
                else if (InnerAtual.Tentativas++ > 5)
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                }
            }
        }

        private void PASSO_ESTADO_ENVIAR_MSG_OFF_COLETOR(Inner InnerAtual)
        {
            int Ret = 0;
            //intercala entre os horarios de entrada e saida
            byte HorMudEntrada1 = 8;
            byte MinMudEntrada1 = 0;
            byte HorMudSaida1 = 11;
            byte MinMudSaida1 = 0;

            byte HorMudEntrada2 = 14;
            byte MinMudEntrada2 = 0;
            byte HorMudSaida2 = 15;
            byte MinMudSaida2 = 0;

            byte HorMudEntrada3 = 16;
            byte MinMudEntrada3 = 0;
            byte HorMudSaida3 = 18;
            byte MinMudSaida3 = 0;

            EasyInner.DefinirMensagemApresentacaoEntrada(1, "Coletor entrada");

            EasyInner.DefinirMensagemApresentacaoSaida(1, "Coletor saida");

            EasyInner.InserirHorarioMudancaEntrada(HorMudEntrada1, MinMudEntrada1, HorMudEntrada2, MinMudEntrada2, HorMudEntrada3, MinMudEntrada3);

            EasyInner.InserirHorarioMudancaSaida(HorMudSaida1, MinMudSaida1, HorMudSaida2, MinMudSaida2, HorMudSaida3, MinMudSaida3);

            Ret = EasyInner.EnviarBufferEventosMudancaAuto(InnerAtual.Numero);

            if (Ret == (int)EnumeradoresDllInner.Retorno.RetornoComandoOk)
            {
                InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoEnviarDataHora;
            }
            else
            {
                //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                if (InnerAtual.Tentativas++ >= 3)
                {
                    InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoReconectar;
                }
            }
        }

        private void PASSO_ESTADO_ACIONAR_RELE1(Inner InnerAtual)
        {
            //Aciona Bip Curto..
            EasyInner.AcionarBipCurto(InnerAtual.Numero);
            //Desliga Led Verde
            EasyInner.LigarLedVerde(InnerAtual.Numero);
            EasyInner.AcionarRele1(InnerAtual.Numero);
            InnerAtual.TempoInicialMensagem = DateTime.Now;
            InnerAtual.Tentativas = 0;
            EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "Acesso Liberado!");
            InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoAguardaTempoMensagem;
        }
        private void PASSO_ESTADO_ACIONAR_RELE2(Inner InnerAtual)
        {
            //Aciona Bip Curto..
            EasyInner.AcionarBipCurto(InnerAtual.Numero);
            //Desliga Led Verde
            EasyInner.LigarLedVerde(InnerAtual.Numero);
            EasyInner.AcionarRele2(InnerAtual.Numero);
            InnerAtual.TempoInicialMensagem = DateTime.Now;
            InnerAtual.Tentativas = 0;
            EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "Acesso Liberado!");
            InnerAtual.EstadoAtual = EnumeradoresDllInner.EstadosInner.EstadoAguardaTempoMensagem;
        }

        #endregion
    }
}