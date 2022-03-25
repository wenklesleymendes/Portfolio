//******************************************************************************
//A Topdata Sistemas de Automação Ltda não se responsabiliza por qualquer
//tipo de dano que este software possa causar, este exemplo deve ser utilizado
//apenas para demonstrar a comunicação com os equipamentos da linha Inner.
//
//Exemplo Biometria
//Desenvolvido em C#.
//                                           Topdata Sistemas de Automação Ltda.
//******************************************************************************

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Cryptography;

//Referências Nitgen
using NBioBSPCOMLib;
using NITGEN.SDK.NBioBSP;

//Referências Projeto
using EasyInnerSDK.Entity;

using System.Data.OleDb;
using System.Drawing;
using ExemploOnline.Entity;
using EasyInnerSDK.DAO;
using System.Globalization;

//Referência EasyInner
namespace EasyInnerSDK.UI.FrmBIO
{

    public class FrmBIOController
    {
        #region Propriedades
        private byte InnerAcessoBio;

        private DAOUsuariosBio AcessoUsuariosBio;
        private bool InnerConectado = false;

        private List<UsuarioBio> ListaUsuariosBio;
        private FrmBIO frmBiometrico;
        private Inner InnerAtual;
        private UpdatePropriedadeTelaBio UpdateFrmBio;
        private NitgenController ntgController;

        #endregion

        public FrmBIOController(FrmBIO frmBio, Inner inner, UpdatePropriedadeTelaBio UpdateprpFrmBio)
        {
            UpdateFrmBio = UpdateprpFrmBio;
            InnerAtual = inner;
            frmBiometrico = frmBio;
            AcessoUsuariosBio = new DAOUsuariosBio();
        }

        private void AtualizarInformacoes(string mensagem)
        {
            frmBiometrico.Invoke(new FrmBIO.AtualizarInfoConfig(frmBiometrico.AtualizarInfoConfiguracao), new object[] {mensagem});
        }
        private void AtualizarInfoTelaManutencao(string mensagem)
        {
            frmBiometrico.Invoke(new FrmBIO.AtualizarInfoTelaManut(frmBiometrico.AtualizarInfoManutencao), new object[] { mensagem });
        }
        private void AtualizarMouseCursor(Cursor cursor)
        {
            frmBiometrico.Invoke(new FrmBIO.AtualizarCursorsMs(frmBiometrico.AtualizarCursorsMouse), new object[] {cursor });
        }
        private void AtualizarEstadoButtonCaptura(bool estado)
        {
            frmBiometrico.Invoke(new FrmBIO.AtualizarEstadobtnCaptura(frmBiometrico.AtualizarEstadoButtonCaptura), new object[] { estado });
        }
        private void AtualizarInfoTelaCadLeitorInner(string mensagem)
        {
            frmBiometrico.Invoke(new FrmBIO.AtualizarInfoCad(frmBiometrico.AtualizarInfoCadInner), new object[] { mensagem });
        }
        private void LimparlstCadastroInner()
        {
            frmBiometrico.Invoke(new FrmBIO.LimparlstCadInner(frmBiometrico.LimparListCadInner), null);
        }
        private void LimparListInfo()
        {
            frmBiometrico.Invoke(new FrmBIO.LimparlstInfo(frmBiometrico.LimparListInfo), null);
        }
        private void LimparListManutencao()
        {
            frmBiometrico.Invoke(new FrmBIO.LimparlstManut(frmBiometrico.LimparListManut), null);
        }
        private void AtualizarLabelStatusCap(string mensagem)
        {
            frmBiometrico.Invoke(new FrmBIO.AtualizarlblStatusCap(frmBiometrico.AtualizarLabelStatusCap), new object[] { mensagem });
        }

        private void AtualizarLabelQualidadeDigital(string mensagem)
        {
            frmBiometrico.Invoke(new FrmBIO.AtualizarlblQualidadeDigital(frmBiometrico.AtualizarLabelQualidadeDigital), new object[] { mensagem });
        }

        private void AtualizarLabelQualidadeImagem(string mensagem)
        {
            frmBiometrico.Invoke(new FrmBIO.AtualizarlblQualidadeImagem(frmBiometrico.AtualizarLabelQualidadeImagem), new object[] { mensagem });
        }

        private void ControlarBotoesManutencao(bool Habilitar)
        {
            frmBiometrico.Invoke(new FrmBIO.ControlarbtnManutencao(frmBiometrico.ControlarBotoesManutencao), new object[] { Habilitar});
        }


        #region Metodos de Configuração
        /// <summary>
        /// Solicita dados do Firmware
        /// Retorna o resultado
        /// </summary>
        public int CapturaVersaoPlaca()
        {
            int ret2 = 0;

            byte Linha = 0;
            short Variacao = 0;
            byte VersaoAlta = 0;
            byte VersaoBaixa = 0;
            byte VersaoSufixo = 0;

            //Solicita a versão do firmware do Inner e dados como o Idioma, se é uma versão especial.
            ret2 = EasyInner.ReceberVersaoFirmware(InnerAtual.Numero, ref Linha, ref Variacao, ref VersaoAlta, ref VersaoBaixa, ref VersaoSufixo, ref InnerAcessoBio);


            //Se selecionado Biometria, valida se o equipamento é compatível
            if (((Linha != 6) && (Linha != 14)) || ((Linha == 14) && (InnerAcessoBio == 0)))
            {
                MessageBox.Show("Equipamento não compatível com Biometria.", "Atenção");
            }
            return VersaoAlta;
        }

        //***********************************************************************************
        //MONTAR CONFIGURAÇÕES
        //Esta rotina monta o buffer para enviar a configuração do Inner
        //***********************************************************************************
        public int MontarConfiguracao()
        {
            //Atribuição de Variáveis
            int Ret = -1;

            //Mensagens de status
            AtualizarInformacoes("Montando configurações...");

            //Definição da EasyInner
            EasyInner.DefinirPadraoCartao((byte)InnerAtual.PadraoCartao);
            EasyInner.HabilitarTeclado((byte)Enumeradores.Habilita.HABILITA, (byte)Enumeradores.Ecoar.ECOA_DIGITADO);
            EasyInner.DefinirQuantidadeDigitosCartao((byte)InnerAtual.QtdDigitos);

            //Configurar tipo do leitor
            EasyInner.ConfigurarTipoLeitor((byte)InnerAtual.TipoLeitor);

            //Definição da EasyInner
            EasyInner.ConfigurarLeitor1((byte)Enumeradores.ConfiguracaoLeitor.ENTRADA_SAIDA);
            EasyInner.ConfigurarAcionamento1((byte)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA_OU_SAIDA, 5);

            EasyInner.SetarBioVariavel(1); 
            EasyInner.ConfigurarBioVariavel(InnerAtual.Bio16Digitos ? 1 : 0);

            Application.DoEvents();

            //Envia as configurações para o Inner..
            return Ret = EasyInner.EnviarConfiguracoes(InnerAtual.Numero);
        }

        //***********************************************************************************
        //Configuração do Inner
        //***********************************************************************************
        public void ConfigurarInner()
        {
            //Declaração de variáveis..
            int Ret = -1;

            //Campo obrigatório
            if (InnerAtual.TipoLeitor == -1)
            {
                MessageBox.Show("Favor selecionar um tipo de leitor !", "Atenção");
                return;
            }

            //Altera o Cursor para modo ampulheta..
            frmBiometrico.Cursor = Cursors.WaitCursor;

            //Mensagens de status
            AtualizarInformacoes("Conectar Inner...");

            //Tenta realizar a conexão com o Inner, caso tenha sucesso envia as configurações..
            if (Conectar())
            {
                //Configuração INNER
                Ret = MontarConfiguracao();

                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {         
                    //Configuração INNER BIO
                    Ret = ConfigurarValidacaoIdentificacao(Ret);
                }
                else
                {
                    MessageBox.Show("Erro ao enviar as configurações");
                    return;
                }
            }

            //Se Configuração ok
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                System.Windows.Forms.MessageBox.Show("Configuração enviada com sucesso!", "Atenção.");
                AtualizarInformacoes("Configuração enviada com sucesso!");
                Application.DoEvents();
            }
            else
            {
                //Se erro de Configuração
                if (Ret == 1)
                {
                    MessageBox.Show("Erro ao enviar a configuração!");
                    AtualizarInformacoes("Erro ao configurar o inner..");
                    Application.DoEvents();
                } //Se erro de Configuração da Biometria
                else
                {
                    System.Windows.Forms.MessageBox.Show("Erro ao configurar o Inner bio!");
                    AtualizarInformacoes("Erro ao configurar o Inner bio..");
                }
            }

            //Fecha a comunicação com o Inner..
            EasyInner.FecharPortaComunicacao();

            //Altera o Cursor para modo Default..
            frmBiometrico.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Configurar Validação Identificação
        /// Método que configura biometria e retorna se foi configurado com sucesso
        /// </summary>
        /// <param name="UiMainBIO"></param>
        /// <param name="Verificacao"></param>
        /// <param name="Identificacao"></param>
        /// <param name="Ret"></param>
        /// <returns></returns>
        public int ConfigurarValidacaoIdentificacao(int Ret)
        {
            //Seta o cursor para Ampulheta..
            AtualizarMouseCursor(Cursors.WaitCursor);

            //Mensagens de Status
            AtualizarInformacoes("Enviando Configuração Bio...");

            //Envia comando de Configuração..
            Ret = EasyInner.ConfigurarBio(InnerAtual.Numero, InnerAtual.Identificacao, InnerAtual.Verificacao);

            //Testa retorno do comando..
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                SetarTimeoutBio();

                //Espera resposta do comando..
                do
                {
                    Pausa(1);
                    Ret = EasyInner.ResultadoConfiguracaoBio(InnerAtual.Numero, 0);
                }
                while (EsperaRespostaBio(Ret));
            }

            //Testa resultado da resposta..
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                return 0;
            }
            else
            {
                return 2;
            }

            //Limpa Lista
            LimparListManutencao();

            //Seta o cursor para Default..
            AtualizarMouseCursor(Cursors.Default);

        }

        /// <summary>
        /// Receber Modelo Bio
        /// O modelo do Inner Bio é retornado.
        /// </summary>
        /// <param name="UiMainBIO"></param>
        public void ReceberModeloBio()
        {
            int Modelo = 0;
            int Ret = -1;

            //Seta o cursor para Ampulheta..
            frmBiometrico.Cursor = Cursors.WaitCursor;

            //Mensagem de Status
            LimparListInfo();
            AtualizarInformacoes("Recebendo Modelo Bio...");
            Application.DoEvents();

            //Conecta com o Inner..
            if (Conectar())
            {
                //Envia comando solicitando modelo do BIO...
                Ret = EasyInner.SolicitarModeloBio(InnerAtual.Numero);
                Application.DoEvents();
                //Testa retorno do Comando..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    SetarTimeoutBio();

                    do
                    {
                        Pausa(1);

                        //Envia solicitação de resposta..
                        Ret = EasyInner.ReceberModeloBio(InnerAtual.Numero, 0, ref Modelo);
                        Application.DoEvents();
                    }
                    while (EsperaRespostaBio(Ret));

                }

                //Testa solicitação de Resposta..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    switch (Modelo)
                    {
                        case 2:
                            MessageBox.Show("Modelo do bio: Light usuários (FIM10).");
                            break;
                        case 4:
                            MessageBox.Show("Modelo do bio: 1000/4000 usuários (FIM01).");
                            break;
                        case 51:
                            MessageBox.Show("Modelo do bio: 1000/4000 usuários (FIM2030).");
                            break;
                        case 52:
                            MessageBox.Show("Modelo do bio: 1000/4000 usuários (FIM2040).");
                            break;
                        case 48:
                            MessageBox.Show("Modelo do bio: Light 100 usuários (FIM3030).");
                            break;
                        case 64:
                            MessageBox.Show("Modelo do bio: Light 100 usuários (FIM3040).");
                            break;
                        case 80:
                            MessageBox.Show("Modelo do bio: FIM5060.");
                            break;
                        case 82:
                            MessageBox.Show("Modelo do bio: FIM5260.");
                            break;
                        case 83:
                            MessageBox.Show("Modelo do bio: FIM5360.");
                            break;
                        case 96:
                            MessageBox.Show("Modelo do bio: FIM6060.");
                            break;
                        case 255:
                            System.Windows.Forms.MessageBox.Show("Modelo do bio: Desconhecido");
                            break;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Erro ao solicitar o modelo do bio!");
                }
            }

            //Fecha Porta de Comunicação..
            EasyInner.FecharPortaComunicacao();
            LimparListInfo();
            AtualizarMouseCursor(Cursors.Default);
        }

        /// <summary>
        /// Receber Versão Bio
        /// A versão do Inner Bio é retornado.
        /// </summary>
        /// <param name="UiMainBIO"></param>
        public void ReceberVersaoBIO()
        {
            int Ret = -1;
            int VersaoAlta = 0;
            int VersaoBaixa = 0;

            //Seta o cursor para Ampulheta..
            AtualizarMouseCursor(Cursors.WaitCursor);

            //Mensagem de Status
            LimparListInfo();
            AtualizarInformacoes("Recebendo Versão Bio...");
            Application.DoEvents();

            //Conecta com o Inner..
            if (Conectar())
            {
                //Envia Comando solicitando versão..
                Ret = EasyInner.SolicitarVersaoBio(InnerAtual.Numero);
                Application.DoEvents();

                //Testa Retorno do comando..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    SetarTimeoutBio();

                    do
                    {
                        Pausa(1);
                        //Envia solicitação da resposta do comando..
                        Ret = EasyInner.ReceberVersaoBio(InnerAtual.Numero, 0, ref VersaoAlta, ref VersaoBaixa);
                        Application.DoEvents();
                    }
                    while (EsperaRespostaBio(Ret));
                }

                //Testa retorno da resposta..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    System.Windows.Forms.MessageBox.Show("A versão do inner bio é " + VersaoAlta + "." + VersaoBaixa);

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Erro ao receber a versão do Bio!");
                }
            }

            //Fecha a porta de comunicação com o InnerBIO..
            EasyInner.FecharPortaComunicacao();
            LimparListInfo();
            AtualizarMouseCursor(Cursors.Default);
        }

        public void ConfigurarAjustesBio()
        {
            int Ret = -1;
            byte Ganho = 2;
            byte Brilho = 40;
            byte Contraste = 20;
            byte Registro = 40;
            byte QualVerificacao = 30;
            byte SegIdentificacao = 8;
            byte SegVerificacao = 5;
            byte Capturar = 0;
            byte TotalCap = 5;
            byte TempoCap = 50;
            byte HabilitarFiltro = 0;
            if (InnerAtual.NivelLFD > 0)
            {
                MessageBox.Show("Nivel LFD só é válido a partir da FIM6060.", "Atenção");
            }
            frmBiometrico.Cursor = Cursors.WaitCursor;
            if (Conectar())
            {
                EasyInner.ConfigurarAjustesSensibilidadeBio(InnerAtual.Numero, Ganho, Brilho, Contraste);
                EasyInner.ConfigurarAjustesQualidadeBio(InnerAtual.Numero, Registro, QualVerificacao);
                EasyInner.ConfigurarAjustesSegurancaBio(InnerAtual.Numero, SegIdentificacao, SegVerificacao);
                EasyInner.ConfigurarCapturaAdaptativaBio(InnerAtual.Numero, Capturar, TotalCap, TempoCap);
                EasyInner.ConfigurarFiltroBio(InnerAtual.Numero, HabilitarFiltro);
                EasyInner.ConfigurarTimeoutIdentificacao((byte)InnerAtual.TimeOutAjustes);
                EasyInner.ConfigurarNivelLFD((byte)InnerAtual.NivelLFD);

                Ret = EasyInner.EnviarAjustesBio(InnerAtual.Numero);
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    LimparListInfo();
                    AtualizarInformacoes("Ajustes configurados com sucesso!");
                }
                EasyInner.FecharPortaComunicacao();
                frmBiometrico.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Métodos de Manutenção de Usuários

        #region CadastrarUsuarioInner
        /// <summary>
        /// Adicionar Usuário na Memória
        /// Cadastra a 1º e 2º digital do usuário na memória do Inner Bio.
        /// </summary>
        /// <param name="UiMainBIO"></param>
        public void CadastrarUsuarioInner()
        {
            //Mensagem de Status
            LimparlstCadastroInner();
            AtualizarInfoTelaCadLeitorInner("Cadastrando Usuário " + UpdateFrmBio.txtCartaoCadInner);
            Application.DoEvents();

            frmBiometrico.Cursor = Cursors.WaitCursor;

            //Se Conectar Bio
            if (Conectar())
            {

                //Define que o Inner utilizado no momento é um Inner bio light ao invés de 
                //um Inner bio 1000/4000.
                if (BioLight())
                {
                    EasyInner.SetarBioLight(1);
                }

                //Inserção da primeira digital
                MessageBox.Show("Posicione a primeira digital");
                if (inserirTemplateUsr(frmBiometrico, UpdateFrmBio.txtCartaoCadInner, 0) == false)
                {
                    AtualizarMouseCursor(Cursors.Default);
                    LimparlstCadastroInner();
                    MessageBox.Show("Erro ao capturar digital!");
                    return;
                }
                Thread.Sleep(20);

                //Inserção da segunda digital
                MessageBox.Show("Posicione a segunda digital");
                if (!inserirTemplateUsr(frmBiometrico, UpdateFrmBio.txtCartaoCadInner, 3))
                {
                    AtualizarMouseCursor(Cursors.Default);
                    LimparlstCadastroInner();
                    MessageBox.Show("Erro ao capturar digital!");
                    return;
                }

                //Mensagem Status
                MessageBox.Show("Usuário cadastrado!");
            }

            //Fecha a Porta de Comunicação com o Inner
            EasyInner.FecharPortaComunicacao();
            LimparlstCadastroInner();
            AtualizarMouseCursor(Cursors.Default);
        }

        #endregion


        /// <summary>
        /// Remover Usuário da Memória da placa FIM
        /// Verifica se o usuário existe, se sim, exclui e retorna.
        /// </summary>
        #region RemoverUsuarioDaMemoria
        public void ExcluirSelecionadosInner(List<string> ListaExcluirInner)
        {
            LimparListManutencao();
            AtualizarInfoTelaManutencao("Excluindo Usuarios");

            int erros = 0;
            int excluidos = 0;

            if (ListaExcluirInner.Count <= 0)
            {
                MessageBox.Show("Selecione um usuário para excluir");
                return;
            }
            AtualizarMouseCursor(Cursors.WaitCursor);
            ControlarBotoesManutencao(false);
            if (Conectar())
            {
                Boolean placalight = BioLight();
                int versao = CapturaVersaoPlaca();

                foreach (var usuario in ListaExcluirInner)
                {
                    int ret = ExcluirUsuarioPlacaFim(usuario, placalight, versao);

                    if (ret == 0)
                    {
                        excluidos++;
                    }
                    else
                    {
                        erros++;
                    }
                    LimparListManutencao();
                    AtualizarInfoTelaManutencao("Usuário excluído " + usuario);
                }
            }
            else
            {
                MessageBox.Show("Erro ao conectar");
            }

            EasyInner.FecharPortaComunicacao();
            AtualizarMouseCursor(Cursors.Default);
            ControlarBotoesManutencao(true);
            LimparListManutencao();
            AtualizarInfoTelaManutencao("Total excluido :" + excluidos);
            AtualizarInfoTelaManutencao("Total não excluido :" + erros);
        }

        private int ExcluirUsuarioPlacaFim(String Usuario, bool placalight, int versao)
        {
            int Ret = -1;
            int tentativas = 0;
            string usuarioPronto = Utils.RemZeroEsquerda(Usuario);
            //Mensagem de Status
            LimparListManutencao();
            AtualizarInfoTelaManutencao("Excluindo Usuário " + usuarioPronto);
            Application.DoEvents();

            
            if (usuarioPronto.Length < 11)
            {
                if (placalight)
                {
                    EasyInner.SetarBioLight(1);
                }
            }
            else
            {
                EasyInner.SetarBioVariavel(1);
            }

                Application.DoEvents();
            //Solicita para o Inner bio excluir o cadastro do usuário desejado.
            Ret = EasyInner.SolicitarExclusaoUsuario(InnerAtual.Numero, usuarioPronto);

            Thread.Sleep(20);
            if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                Thread.Sleep(10);
                do
                {
                    //Verifica se foi excluído
                    Application.DoEvents();
                    Ret = EasyInner.UsuarioFoiExcluido(InnerAtual.Numero, 0);
                    Thread.Sleep(10);
                } while ((Ret == (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO) && (tentativas++ < 10));
            }


            return Ret;
        }
        #endregion

        /// <summary>
        ///  Enviar Usuario do Servidor para o Inner
        /// </summary>
        #region ENVIAR USUÁRIO BIO

        public void EnviarSelecionado()
        {

            try
            {
                //Mensagem de Status
                LimparListManutencao();
                AtualizarInfoTelaManutencao("Conectar Inner...");
                AtualizarMouseCursor(Cursors.WaitCursor);
                ControlarBotoesManutencao(false);
                //Se conectar Bio
                if (Conectar())
                {
                    LimparListManutencao();
                    AtualizarInfoTelaManutencao("Verificando tipo da placa FIM...");

                    //Se a base estiver vazia retorna
                    if (ListaUsuariosBio.Count == 0)
                    {
                        MessageBox.Show("Não há usuários para enviar !");
                        return;
                    }
                    else
                    {
                        int iOk = 0;
                        int iJaCadast = 0;
                        int iFalhaCom = 0;
                        int iParametro = 0;
                        int iInvalido = 0;
                        for(int index = 0; index < ListaUsuariosBio.Count; index++)
                        {
                            if (CapturaVersaoPlaca() < 5)
                            {
                                AtualizaContadores(EnviarUsuarioBio(ListaUsuariosBio[index].Usuario, ListaUsuariosBio[index].TemplateA, 
                                                   ListaUsuariosBio[index].TemplateB, BioLight()), ref iOk, ref iJaCadast, ref iFalhaCom, ref iParametro, ref iInvalido);
                            }
                            else
                            {
                                AtualizaContadores(EnviarUsuarioBioVariavel(ListaUsuariosBio[index].Usuario, ListaUsuariosBio[index].TemplateA, 
                                                   ListaUsuariosBio[index].TemplateB), ref iOk, ref iJaCadast, ref iFalhaCom, ref iParametro, ref iInvalido);
                            }
                        }
                    }

                }
                AtualizarMouseCursor(Cursors.Default);
                ControlarBotoesManutencao(true);
            }
            catch (Exception ex)
            {
                LimparListManutencao();
                AtualizarInfoTelaManutencao("ERRO");
                Console.WriteLine(ex.Message);
                AtualizarMouseCursor(Cursors.Default);
            }
        }

        private int EnviarUsuarioBio(String cartao, String Digital1, String Digital2, Boolean placaLight)
        {
            //Senão consulta os usuários cadastrados
            int i = 0;
            int j = 0;
            int k = 0;
            int tentativas = 0;

            Nitgen objNitgen;
            int Retorno = -1;

            byte[] linha = new byte[844];
            byte[] tempConv = new byte[404];

            //Template A
            i = 1;
            if (!placaLight)
            {

                //Rotinas que interpretam a digital cadastrada

                if (cartao.Length > 10)
                {
                    cartao = cartao.Substring(cartao.Length - 10);
                }

                cartao = CompletaString(cartao, 10, "0");

                for (j = 0; j < cartao.Length; j++)
                {
                    linha[i] = Convert.ToByte(Convert.ToInt32(cartao.Substring(j, 1)) + 48);
                    i++;
                }

                i = 28;
                k = 0;

                for (j = 0; j < 807; j += 2)
                {
                    tempConv[k] = Convert.ToByte(Convert.ToUInt32(Digital1.Substring(j, 2), 16));
                    k++;
                }

                for (j = 0; j <= 403; j++)
                {
                    linha[i] = tempConv[j];
                    i++;
                }

            } //placa light
            else
            {
                //Rotinas que interpretam a digital cadastrada

                if (cartao.Length > 8)
                {
                    cartao = cartao.Substring(cartao.Length - 8);
                }

                cartao = CompletaString(cartao, 8, "0");

                for (j = 0; j < cartao.Length; j++)
                {
                    linha[i] = Convert.ToByte(Convert.ToInt32(cartao.Substring(j, 1)) + 48);
                    i++;
                }

                i = 27;
                k = 0;

                for (j = 0; j < 807; j += 2)
                {
                    tempConv[k] = Convert.ToByte(Convert.ToUInt32(Digital1.Substring(j, 2), 16));
                    k++;
                }

                objNitgen = new Nitgen();
                objNitgen.biFIR = tempConv;
                objNitgen.objFPData.Import(1, 0, 1, 7, 404, tempConv, null);
                objNitgen.objFPData.Export(objNitgen.objFPData.FIR, 6);

                foreach (byte b in (byte[])objNitgen.objFPData.get_FPData(objNitgen.objFPData.get_FingerID(0), 0))
                {
                    linha[i] = b;
                    i++;
                }

            }

            //Template B
            if (!placaLight)
            {
                i = 432;
                k = 0;

                //Rotinas que interpretam a digital cadastrada

                for (j = 0; j <= 403; j++)
                {
                    tempConv[j] = 0;
                }

                for (j = 0; j < 807; j += 2)
                {
                    tempConv[k] = Convert.ToByte(Convert.ToUInt32(Digital2.Substring(j, 2), 16));
                    k++;
                }

                for (j = 0; j <= 403; j++)
                {
                    linha[i] = tempConv[j];
                    i++;
                }

            } //placa light
            else
            {
                i = 427;
                k = 0;

                //Rotinas que interpretam a digital cadastrada
                for (j = 0; j <= tempConv.Length - 1; j++)
                {
                    tempConv[j] = 0;
                }

                for (j = 0; j < 807; j += 2)
                {
                    tempConv[k] = Convert.ToByte(Convert.ToUInt32(Digital2.Substring(j, 2), 16));
                    k++;
                }

                objNitgen = new Nitgen();
                objNitgen.objFPData.Import(1, 0, 1, 7, 404, tempConv, null);
                objNitgen.objFPData.Export(objNitgen.objFPData.FIR, 6);

                foreach (byte b in (byte[])objNitgen.objFPData.get_FPData(objNitgen.objFPData.get_FingerID(0), 0))
                {
                    linha[i] = b;
                    i++;
                }
            }

            if (placaLight == false)
            {
                int Conv = int.Parse((DateTime.Now.Year / 100).ToString(), NumberStyles.HexNumber);
                linha[836] = Convert.ToByte(Conv);
                Conv = int.Parse((DateTime.Now.Year % 100).ToString(), NumberStyles.HexNumber);
                linha[837] = Convert.ToByte(Conv);
                Conv = int.Parse((DateTime.Now.Month).ToString(), NumberStyles.HexNumber);
                linha[838] = Convert.ToByte(Conv);
                Conv = int.Parse((DateTime.Now.Day).ToString(), NumberStyles.HexNumber);
                linha[839] = Convert.ToByte(Conv);
                Conv = int.Parse((DateTime.Now.Hour).ToString(), NumberStyles.HexNumber);
                linha[840] = Convert.ToByte(Conv);
                Conv = int.Parse((DateTime.Now.Minute).ToString(), NumberStyles.HexNumber);
                linha[841] = Convert.ToByte(Conv);
                Conv = int.Parse((DateTime.Now.Second).ToString(), NumberStyles.HexNumber);
                linha[842] = Convert.ToByte(Conv);
                linha[843] = 0;
            }
            else
            {
                EasyInner.SetarBioLight(1);
            }
            //Envio do template para cadastro no Inner Bio
            Application.DoEvents();
            Retorno = (byte)EasyInner.EnviarUsuarioBio(InnerAtual.Numero, linha);
            Application.DoEvents();

            if (Retorno == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                do
                {
                    Application.DoEvents();
                    //Verifica se o template foi cadastrado com sucesso
                    Retorno = (byte)EasyInner.UsuarioFoiEnviado(InnerAtual.Numero, 0);
                    Application.DoEvents();
                    Thread.Sleep(30);
                } while ((Retorno == (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO) && (tentativas++ < 10));

            }

            return Retorno;

        }

        private int EnviarUsuarioBioVariavel(String Cartao, String Digital1, String Digital2)
        {
            int Retorno = -1;
            int tentativas = 0;
            int j = 0;
            int k = 0;
            byte[] bDigital1 = new byte[404];
            byte[] bDigital2 = new byte[404];

            try
            {
                for (j = 0; j < 807; j += 2)
                {
                    bDigital1[k] = Convert.ToByte(Convert.ToUInt32(Digital1.Substring(j, 2), 16));
                    bDigital2[k] = Convert.ToByte(Convert.ToUInt32(Digital2.Substring(j, 2), 16));
                    k++;
                }

                //Envio do template para cadastro no Inner Bio
                Thread.Sleep(10);
                Application.DoEvents();
                if (InnerAtual.DuasDigitais)
                {
                    Retorno = (byte)EasyInner.EnviarDigitalUsuario(InnerAtual.Numero, Cartao, bDigital1, bDigital2);
                }
                else
                {
                    Retorno = (byte)EasyInner.EnviarDigitalUsuario(InnerAtual.Numero, Cartao, bDigital1, null);
                }

                if (Retorno == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    do
                    {
                        // Application.DoEvents();
                        //Verifica se o template foi cadastrado com sucesso
                        Retorno = (byte)EasyInner.UsuarioFoiEnviado(InnerAtual.Numero, 0);
                        Thread.Sleep(40);

                    } while ((Retorno == (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO) && (tentativas++ < 10));

                }


            }
            catch (Exception ex)
            {
                LimparListManutencao();
                AtualizarInfoTelaManutencao("ERRO");
                Console.WriteLine(ex.Message);
                frmBiometrico.Cursor = Cursors.Default;
            }
            return Retorno;
        }

        private int AtualizaContadores(int Retorno, ref int iOk, ref int iJaCadast, ref int iFalhaCom, ref int iParametro, ref int iTempInvalido)
        {
            switch (Retorno)
            {
                case (int)Enumeradores.Retorno.RET_COMANDO_OK:
                    iOk++;
                    break;
                case (int)Enumeradores.Retorno.RET_BIO_USR_JA_CADASTRADO:
                    iJaCadast++;
                    break;
                case (int)Enumeradores.Retorno.RET_BIO_TEMPLATE_INVALIDO:
                    iTempInvalido++;
                    break;
                case (int)Enumeradores.Retorno.RET_BIO_PARAMETRO_INVALIDO:
                    iParametro++;
                    break;
                case (int)Enumeradores.Retorno.RET_BIO_BASE_CHEIA:
                    LimparListManutencao();
                    MessageBox.Show("Base bio cheia.");
                    AtualizarInfoTelaManutencao("ENVIADOS: " + iOk);
                    AtualizarInfoTelaManutencao("JÁ CADASTRADOS: " + iJaCadast);
                    AtualizarInfoTelaManutencao("FALHA ENVIO: " + (iFalhaCom + iParametro + iTempInvalido));
                    break;
                case (int)Enumeradores.Retorno.RET_BIO_FALHA_COMUNICACAO:
                    iFalhaCom++;
                    break;
            }
            
            LimparListManutencao();
            AtualizarInfoTelaManutencao("ENVIADOS: " + iOk);
            AtualizarInfoTelaManutencao("\nJÁ CADASTRADOS: " + iJaCadast);
            AtualizarInfoTelaManutencao("\nFALHA ENVIO: " + (iFalhaCom + iParametro + iTempInvalido));

            return Retorno;
        }

        #endregion

        /// <summary>
        /// Retorna a quantidade de usuários cadastrados.
        /// </summary>
        #region ReceberQtdUsuariosBIO

        public void ReceberQtdUsuariosBIO()
        {
            int Quantidade = 0;
            int Ret = -1;

            //Mensagem Status
            LimparListManutencao();
            AtualizarInfoTelaManutencao("Recebendo quantidade de usuários cadastrados");

            Application.DoEvents();

            AtualizarMouseCursor(Cursors.WaitCursor);

            if (Conectar())
            {
                //Solicita a quantidade de usuários cadastrados no Inner Bio.
                Application.DoEvents();
                Ret = EasyInner.SolicitarQuantidadeUsuariosBio(InnerAtual.Numero);
                Thread.Sleep(50);

                if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    Ret = (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO;
                    SetarTimeoutBio();
                    do
                    {
                        //Retorna a quantidade de usuários cadastrados no Inner Bio
                        Application.DoEvents();
                        Ret = EasyInner.ReceberQuantidadeUsuariosBio(InnerAtual.Numero, 0, ref Quantidade);

                        if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                        {
                            LimparListManutencao();
                            AtualizarInfoTelaManutencao("Quantidade total de usuários: " + Quantidade);
                            break;
                        }
                    } while (EsperaRespostaBio(Ret));
                }
            }

            //Fecha porta comunicação
            EasyInner.FecharPortaComunicacao();
            AtualizarMouseCursor(Cursors.Default);
            Application.DoEvents();

        }
        #endregion

        /// <summary>
        ///Receber Usuários cadastrados no Inner Bio
        ///Retorna todos os usuários cadastrados
        /// </summary>
        #region ReceberUsuariosBIO_PC

        public void AtualizarUsuariosBIO()
        {
            LimparListManutencao();
            AtualizarInfoTelaManutencao("Recebendo todos os Usuários...");
            Application.DoEvents();

            AtualizarMouseCursor(Cursors.WaitCursor);
            ControlarBotoesManutencao(false);
            if (Conectar())
            {
                //Inicia coleta usuários
                EasyInner.InicializarColetaListaUsuariosBio();
                ListaUsuariosBio = new List<UsuarioBio>();
                if (CapturaVersaoPlaca() < 5)
                {
                    ReceberUsuariosBio();
                }
                else
                {
                    ReceberUsuariosBioVariavel();
                }
                frmBiometrico.PreencherGridUsuariosInner(ListaUsuariosBio);
                LimparListManutencao();
                AtualizarInfoTelaManutencao("Recebeu " + ListaUsuariosBio.Count + " usuários");

                EasyInner.FecharPortaComunicacao();
            }

            AtualizarMouseCursor(Cursors.Default);
            ControlarBotoesManutencao(true);
        }

        private void ReceberUsuariosBio()
        {
            int nPacote = 0;
            int nUsuario = 0;
            int Ret = -1;

            while (EasyInner.TemProximoPacote() == 1)
            {
                do
                {
                    //Solicita uma parte(pacote) da lista de usuarios do bio
                    Application.DoEvents();
                    Ret = EasyInner.SolicitarListaUsuariosBio(InnerAtual.Numero);
                    LimparListManutencao();
                    AtualizarInfoTelaManutencao("Solicitando pacote...");

                } while (Ret == (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO);//se ainda estava

                if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Recebe uma parte da lista com os usuarios
                    //SetarTimeoutBio(UiMainBIO);
                    do
                    {
                        Thread.Sleep(20);
                        Application.DoEvents();
                        Ret = EasyInner.ReceberPacoteListaUsuariosBio(InnerAtual.Numero);
                        LimparListManutencao();
                        AtualizarInfoTelaManutencao("Recebendo pacote: " + Convert.ToInt32(nPacote + 1));

                    } while (Ret == (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO);

                    if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        nPacote++;
                        LimparListManutencao();
                        AtualizarInfoTelaManutencao("Recebeu pacote: " + Convert.ToInt32(nPacote));
                    }
                    else
                    {
                        continue;
                    }

                    Thread.Sleep(50);

                    //Verifica se existe um usuario
                    while (EasyInner.TemProximoUsuario() == 1)
                    {
                        StringBuilder Usuario = new StringBuilder();

                        //Pede um usuario da lista
                        Application.DoEvents();
                        Ret = EasyInner.ReceberUsuarioLista(InnerAtual.Numero, Usuario);

                        if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                        {
                            nUsuario++;
                            UsuarioBio usuarioBio = new UsuarioBio();
                            usuarioBio.Usuario = Usuario.ToString();
                            ListaUsuariosBio.Add(usuarioBio);
                        }
                    }

                }
                else
                {
                    TratarRetornoBio(Ret);
                }
            }
        }

        private void ReceberUsuariosBioVariavel()
        {
            int nPacote = 0;
            int nUsuario = 0;
            int Ret = -1;

            while (EasyInner.TemProximoPacote() == 1)
            {
                do
                {

                    //Solicita uma parte(pacote) da lista de usuarios do bio
                    Application.DoEvents();
                    Ret = EasyInner.SolicitarListaUsuariosBioVariavel(InnerAtual.Numero);
                    LimparListManutencao();
                    AtualizarInfoTelaManutencao("Solicitando pacote...");
                    Thread.Sleep(20);
                } while (Ret == (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO);//se ainda estava

                if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Recebe uma parte da lista com os usuarios
                    do
                    {
                        //tempo de processamento do Inner
                        Thread.Sleep(20);
                        Application.DoEvents();
                        Ret = EasyInner.SolicitarListaUsuariosComDigital(InnerAtual.Numero);
                        LimparListManutencao();
                        AtualizarInfoTelaManutencao("Recebendo pacote: " + System.Convert.ToInt32(nPacote + 1));


                    } while (Ret == (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO);

                    if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        nPacote++;
                        LimparListManutencao();
                        AtualizarInfoTelaManutencao("Recebeu pacote: " + System.Convert.ToInt32(nPacote));

                    }
                    else
                    {
                        continue;
                    }

                    Thread.Sleep(50);

                    //Verifica se existe um usuario
                    while (EasyInner.TemProximoUsuario() == 1)
                    {
                        byte[] Usuario = new byte[8];

                        //Pede um usuario da lista
                        Application.DoEvents();
                        Ret = EasyInner.ReceberUsuarioComDigital(Usuario);

                        if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                        {
                            nUsuario++;
                            String User = "";
                            for (int i = 0; i < Usuario.Length; i++)
                            {
                                User += Usuario[i] <= 9 ? "0" + Usuario[i].ToString() : Usuario[i].ToString();
                            }
                            //Insere o usuario na lista
                            UsuarioBio usuarioBio = new UsuarioBio();
                            usuarioBio.Usuario = User;
                            ListaUsuariosBio.Add(usuarioBio);
                        }
                    }
                }
                else
                {
                    TratarRetornoBio(Ret);
                }
            }
        }

        private void TratarRetornoBio(int Ret)
        {
            LimparListManutencao();

            //Em caso de erro
            switch (Ret)
            {
                case (byte)Enumeradores.Retorno.RET_COMANDO_ERRO:
                    AtualizarInfoTelaManutencao("Erro ao abrir porta de comunicação");
                    break;
                case (byte)Enumeradores.Retorno.RET_PORTA_NAOABERTA:
                    AtualizarInfoTelaManutencao("Porta não aberta");
                    break;
                case (byte)Enumeradores.Retorno.RET_PORTA_JAABERTA:
                    AtualizarInfoTelaManutencao("Porta já aberta");
                    break;
                case (byte)Enumeradores.Retorno.RET_DLL_INNER2K_NAO_ENCONTRADA:
                    AtualizarInfoTelaManutencao("DLL Inner2k não encontrada");
                    break;
                case (byte)Enumeradores.Retorno.RET_DLL_INNERTCP_NAO_ENCONTRADA:
                    AtualizarInfoTelaManutencao("DLL InnerTCP não encontrada");
                    break;
                case (byte)Enumeradores.Retorno.RET_DLL_INNERTCP2_NAO_ENCONTRADA:
                    AtualizarInfoTelaManutencao("DLL InnerTCP2 não encontrada");
                    break;
                case (byte)Enumeradores.Retorno.RET_ERRO_GPF:
                    AtualizarInfoTelaManutencao("Ocorreu um erro dentro da DLL");
                    break;
                case (byte)Enumeradores.Retorno.RET_TIPO_CONEXAO_INVALIDA:
                    AtualizarInfoTelaManutencao("Tipo de conexão inválida");
                    break;
                default:
                    AtualizarInfoTelaManutencao("Erro " + Ret.ToString());
                    break;
            }
        }
        #endregion

        /// <summary>
        /// Recebe Templates do Inner
        /// </summary>
        #region ReceberTemplateDeUsuarios

        public void ReceberDigitais(List<UsuarioBio> ListaReceber)
        {
            int index = 0;
            UsuarioBio temp = new UsuarioBio();
            int erro = 0;
            int Count = 0;
            int VersaoInner = 0;
            AtualizarMouseCursor(Cursors.WaitCursor);
            ControlarBotoesManutencao(false);
            if (Conectar())
            {
               VersaoInner  = CapturaVersaoPlaca();
            }
            if (VersaoInner > 0)
            {
                foreach (var usuarioBio in ListaReceber)
                {
                    string Usuario = Utils.RemZeroEsquerda(usuarioBio.Usuario);
                    if (AcessoUsuariosBio.ExisteUsuarioBio(Usuario, 0) == false)
                    {
                        Application.DoEvents();
                        if (VersaoInner < 5)
                        {
                            bool placaLight = BioLight();
                            temp = SolicitarUsuarioBio(Usuario, placaLight);
                            if (temp != null)
                            {
                                Count++;
                            }
                            else
                            {
                                erro++;
                            }
                        }
                        else
                        {
                            temp = SolicitarUsuarioBioVariavel(Usuario);
                            if (temp.Usuario != "")
                            {
                                Count++;
                            }
                            else
                            {
                                erro++;
                            }
                        }
                        if (temp.Usuario != null && temp.Usuario != "")
                        {
                            AcessoUsuariosBio.InserirTemplateBD(temp, 0);
                        }
                    }
                    index++;
                    LimparListManutencao();
                    AtualizarInfoTelaManutencao(Count + " templates gravados");
                    AtualizarInfoTelaManutencao(erro + " erros ao carregar.");
                }
            }

            frmBiometrico.CarregarDigitaisBD();
            frmBiometrico.Cursor = Cursors.Default;
            ControlarBotoesManutencao(true);
            Application.DoEvents();
        }

        private UsuarioBio SolicitarUsuarioBioVariavel(String cartao)
        {
            int tentativas = 0;
            int Retorno = -1;
            int y;
            int j;
            StringBuilder Digital1 = new StringBuilder();
            StringBuilder Digital2 = new StringBuilder();
            string UsuarioCompleto = "";
            if (cartao.Length > 10)
            {
                EasyInner.SetarBioVariavel(1);
                UsuarioCompleto = Utils.CompletarUsuario(cartao);
            }
            else
            {
                UsuarioCompleto = cartao;
            }
            //Conecta na base
            try
            {
                //Solicita os dados do usuário cadastrados no leitor
                Retorno = (byte)EasyInner.SolicitarDigitalUsuario(InnerAtual.Numero, UsuarioCompleto);
                Application.DoEvents();
                Thread.Sleep(50);
                if (Retorno == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    Retorno = -1;
                    byte[] Template = null;
                    byte[] DigitalTemp = new byte[404];
                    int TamResposta = 0;
                    do
                    {
                        Retorno = EasyInner.ReceberRespostaRequisicaoBio(InnerAtual.Numero, ref TamResposta);
                        Thread.Sleep(50);
                        Application.DoEvents();
                    } while ((Retorno == (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO) && (tentativas++ < 50));//se ainda estava
                    if (Retorno == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        Template = new byte[TamResposta];
                        //Recebe os dados do usuário cadastrados no leitor
                        Retorno = (byte)EasyInner.ReceberDigitalUsuario(InnerAtual.Numero, Template, TamResposta);
                        Application.DoEvents();
                    }

                    //Se retornado com sucesso

                    if (Retorno == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        //Inicia processo de gravação

                        y = 0;
                        for (j = 68; j < 472; j++)
                        {
                            DigitalTemp[y] = Template[j];
                            y++;
                        }

                        foreach (byte b in (byte[])DigitalTemp)
                        {
                            Digital1.Append(b.ToString("x").PadLeft(2, '0'));
                        }

                        for (y = 0; y <= 403; y++)
                        {
                            DigitalTemp[y] = 0;
                        }
                        //se for dois templates no Inner grava os dois se não grava os dois iguais
                        if (Template.Length == 876)
                        {
                            y = 0;
                            for (j = 472; j < 876; j++)
                            {
                                DigitalTemp[y] = Template[j];
                                y++;
                            }

                            foreach (byte b in (byte[])DigitalTemp)
                            {
                                Digital2.Append(b.ToString("x").PadLeft(2, '0'));
                            }
                        }
                        else
                        {
                            Digital2 = Digital1;
                        }
                    }
                    else
                    {
                        return new UsuarioBio();
                    }
                }
                else
                {
                    return new UsuarioBio();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            UsuarioBio UserBio = new UsuarioBio();
            UserBio.Usuario = UsuarioCompleto;
            UserBio.TemplateA = Digital1.ToString();
            UserBio.TemplateB = Digital2.ToString();
            return UserBio;
        }

        private UsuarioBio SolicitarUsuarioBio(String cartao, Boolean placaLight)
        {
            int tentativas = 0;
            int Retorno = -1;
            int y;
            int j;
            StringBuilder Digital1 = new StringBuilder();
            StringBuilder Digital2 = new StringBuilder();

            //Conecta na base
            try
            {
                if (placaLight)
                {
                    EasyInner.SetarBioLight(1);

                }
                //Solicita os dados do usuário cadastrados no leitor
                Retorno = (byte)EasyInner.SolicitarUsuarioCadastradoBio(InnerAtual.Numero, cartao);

                if (Retorno == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    Retorno = -1;
                    byte[] Template = new byte[844];
                    byte[] DigitalTemp = new byte[404];

                    do
                    {
                        //Recebe os dados do usuário cadastrados no leitor
                        Retorno = (byte)EasyInner.ReceberUsuarioCadastradoBio(InnerAtual.Numero, 0, Template);

                        //Se retornado com sucesso
                        if (Retorno == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                        {
                            //Inicia processo de gravação

                            if (placaLight)
                            {

                                j = 27;
                                for (y = 0; y <= 403; y++)
                                {
                                    DigitalTemp[y] = Template[j];
                                    j++;
                                }

                                Nitgen objNitgen = new Nitgen();
                                objNitgen.objFPData.Import(1, 0, 1, 6, 400, DigitalTemp, null);
                                objNitgen.objFPData.Export(objNitgen.objFPData.FIR, 7);

                                foreach (byte b in (byte[])objNitgen.objFPData.get_FPData(objNitgen.objFPData.get_FingerID(0), 0))
                                {
                                    Digital1.Append(b.ToString("x").PadLeft(2, '0'));
                                }

                                for (y = 0; y <= 403; y++)
                                {
                                    DigitalTemp[y] = 0;
                                }

                                j = 427;
                                for (y = 0; y <= 403; y++)
                                {
                                    DigitalTemp[y] = Template[j];
                                    j++;
                                }

                                objNitgen.objFPData.Import(1, 0, 1, 6, 400, DigitalTemp, null);
                                objNitgen.objFPData.Export(objNitgen.objFPData.FIR, 7);

                                foreach (byte b in (byte[])objNitgen.objFPData.get_FPData(objNitgen.objFPData.get_FingerID(0), 0))
                                {
                                    Digital2.Append(b.ToString("x").PadLeft(2, '0'));
                                }

                            }
                            else
                            {

                                j = 28;
                                for (y = 0; y <= 403; y++)
                                {
                                    DigitalTemp[y] = Template[j];
                                    j++;
                                }

                                foreach (byte b in (byte[])DigitalTemp)
                                {
                                    Digital1.Append(b.ToString("x").PadLeft(2, '0'));
                                }

                                for (y = 0; y <= 403; y++)
                                {
                                    DigitalTemp[y] = 0;
                                }

                                j = 432;
                                for (y = 0; y <= 403; y++)
                                {
                                    DigitalTemp[y] = Template[j];
                                    j++;
                                }

                                foreach (byte b in (byte[])DigitalTemp)
                                {
                                    Digital2.Append(b.ToString("x").PadLeft(2, '0'));
                                }
                            }
                        }

                    } while ((Retorno == (byte)Enumeradores.Retorno.RET_BIO_PROCESSANDO) && (tentativas++ < 50));//se ainda estava
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            UsuarioBio UserBio = new UsuarioBio();
            UserBio.Usuario = cartao;
            UserBio.TemplateA = Digital1.ToString();
            UserBio.TemplateB = Digital2.ToString();
            return UserBio;
        }

        #endregion

        #region receber templates do leitor biométrico Inner

        public void SolicitarTemplateLeitorInner()
        {
            byte[] digital = new byte[404];
            int retorno = -1;
            int tentativas = 0;
            StringBuilder DigitalCapturada = new StringBuilder();

            AtualizarMouseCursor(Cursors.WaitCursor);
            AtualizarInfoTelaCadLeitorInner("Conectando com o Inner");
            if (Conectar())
            {
                LimparlstCadastroInner();
                MessageBox.Show("coloque o dedo");
                
                retorno = EasyInner.SolicitarTemplateLeitor(InnerAtual.Numero);
                Thread.Sleep(50);

                if (retorno == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    retorno = -1;
                    do
                    {
                        LimparlstCadastroInner();
                        AtualizarInfoTelaCadLeitorInner("Recebendo digital capturada Inner");
                        Application.DoEvents();
                        retorno = EasyInner.ReceberTemplateLeitor(InnerAtual.Numero, 0, digital);
                        Thread.Sleep(100);
                    } while (retorno != (int)Enumeradores.Retorno.RET_COMANDO_OK && tentativas++ < 50);

                    if (retorno == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        foreach (byte b in (byte[])digital)
                        {
                            DigitalCapturada.Append(b.ToString("x").PadLeft(2, '0'));
                        }
                        LimparlstCadastroInner();
                        AtualizarInfoTelaCadLeitorInner("Gravar digital recebida na base de dados");
                        AcessoUsuariosBio = new DAOUsuariosBio();
                        UsuarioBio userBio = new UsuarioBio();
                        userBio.Usuario = UpdateFrmBio.txtCartaoCadInner;
                        userBio.TemplateA = DigitalCapturada.ToString();
                        userBio.TemplateB = userBio.TemplateA;
                        AcessoUsuariosBio.InserirTemplateBD(userBio, 0);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao receber digital");
                    }
                }
                else
                {
                    MessageBox.Show("Erro ao solicitar digital");
                }
            }
            else
            {
                MessageBox.Show("Erro ao conectar com o Inner");
            }
            EasyInner.FecharPortaComunicacao();
            LimparlstCadastroInner();
            AtualizarInfoTelaCadLeitorInner("Digital recebida e gravado com sucesso!");
            frmBiometrico.CarregarDigitaisBD();
            AtualizarMouseCursor(Cursors.Default);
        }

        #endregion

        #region ExcluirUsuario
        //***********************************************************************************
        //APAGA O CARTÃO 'Usuário'
        //***********************************************************************************
        public void ExcluirUsuarioBD(List<string> ListaUsuariosExcluir)
        {
            //Consulta se o usuário existe
            if (ListaUsuariosExcluir.Count == 0)
            {
                return;
            }
            try
            {
                AtualizarMouseCursor(Cursors.WaitCursor);
                ControlarBotoesManutencao(false);
                for (int index = 0; index < ListaUsuariosExcluir.Count; index++)
                {
                    //Usuário encontrado
                    AtualizarInfoTelaManutencao("Apagar " + ListaUsuariosExcluir[index]);
                    if (AcessoUsuariosBio.ExcluirTemplateBD(ListaUsuariosExcluir[index], 0))
                    {
                        LimparListManutencao();
                        AtualizarInfoTelaManutencao("Usuario: " + ListaUsuariosExcluir[index] + " apagado");
                    }
                }
                frmBiometrico.CarregarDigitaisBD();
                AtualizarMouseCursor(Cursors.Default);
                ControlarBotoesManutencao(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion

        #region AbrirDispositivo
        //***********************************************************************************
        //INICIO (Hamster)
        //***********************************************************************************
        public void AbrirDispositivo()
        {
            ntgController = new NitgenController(frmBiometrico, null);
            AtualizarEstadoButtonCaptura(true);
            AtualizarLabelStatusCap("Dispositivo pronto para capturar!");
        }

        #endregion
               

        #region CapturaTemplate
        //***********************************************************************************
        //CAPTURA TEMPLATE
        //Cadastra as digitais do novo usuário
        //***********************************************************************************
        public void CapturaTemplate(UpdatePropriedadeTelaBio upPropTelaBio)
        {
            UpdateFrmBio = upPropTelaBio;
            if (UpdateFrmBio.txtCartaoCaptura == "")
            {
                MessageBox.Show("Favor informar o número do cartão!");
                return;
            }

            try
            {
                AtualizarLabelQualidadeImagem("0");
                AtualizarLabelQualidadeDigital("0");
                //Verifica se usuário já existe na base de dados
                if (AcessoUsuariosBio.ExisteUsuarioBio(UpdateFrmBio.txtCartaoCaptura, 0))
                {
                    AtualizarLabelStatusCap("Usuário já cadastrado!");
                    return;
                }
                //Senão inicia preparação leitura dedos
                UsuarioBio UserBio = new UsuarioBio();
                object Fir = null;
                DialogResult MsgResult = MessageBox.Show("Prepare a primeira digital!", "Captura template hamster", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (MsgResult == DialogResult.Yes)
                {
                    Fir = ntgController.GetTemplateHamster(UpdateFrmBio.CheckImagem, UpdateFrmBio.QualidadeImagem, UpdateFrmBio.ImagemDigital);
                    if (ntgController.GetQualidadeDigital(Fir) >= UpdateFrmBio.QualidadeDigital)
                    {
                        AtualizarLabelQualidadeDigital(ntgController.GetQualidadeDigital(Fir).ToString());
                        UserBio.TemplateA = ntgController.ExportarTemplate(Fir);
                        MsgResult = MessageBox.Show("Prepare a segunda digital!", "Captura template hamster", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (MsgResult == DialogResult.Yes)
                        {
                            Fir = ntgController.GetTemplateHamster(UpdateFrmBio.CheckImagem, UpdateFrmBio.QualidadeImagem, UpdateFrmBio.ImagemDigital);
                            if (ntgController.GetQualidadeDigital(Fir) >= UpdateFrmBio.QualidadeDigital)
                            {
                                AtualizarLabelQualidadeDigital(ntgController.GetQualidadeDigital(Fir).ToString());
                                UserBio.TemplateB = ntgController.ExportarTemplate(Fir);
                            }
                            else
                            {
                                AtualizarLabelStatusCap("Qualidade digital baixa!");
                                AtualizarLabelQualidadeDigital(ntgController.GetQualidadeDigital(Fir).ToString());
                                return;
                            }
                        }
                    }
                    else
                    {
                        AtualizarLabelStatusCap("Qualidade digital baixa!");
                        AtualizarLabelQualidadeDigital(ntgController.GetQualidadeDigital(Fir).ToString());
                        return;
                    }
                }
                UserBio.Usuario = UpdateFrmBio.txtCartaoCaptura;
                if (AcessoUsuariosBio.InserirTemplateBD(UserBio, 0))
                {
                    EnviarDigitalCapturada(UserBio);
                    MessageBox.Show("Digital capturada com sucesso!!");
                }
                AtualizarMouseCursor(Cursors.Default);
                AtualizarLabelStatusCap("");
                frmBiometrico.CarregarDigitaisBD();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
            }
        }

        #endregion
        private void EnviarDigitalCapturada(UsuarioBio user)
        {
            if (UpdateFrmBio.EnviarDigitalInner == true)
            {
                AtualizarMouseCursor(Cursors.WaitCursor);
                AtualizarLabelStatusCap("Conectando no Inner para enviar");
                int Result = 0;
                if (InnerConectado == false)
                {
                    InnerConectado = Conectar();
                }
                if (InnerConectado)
                {
                    bool Light = BioLight();
                    int VersaoFW = CapturaVersaoPlaca();
                    LimparlstCadastroInner();
                    AtualizarLabelStatusCap("Enviar digital para o Inner");
                    if (VersaoFW < 5)
                    {
                        Result = EnviarUsuarioBio(user.Usuario, user.TemplateA, user.TemplateB, Light);
                    }
                    else
                    {
                        Result = EnviarUsuarioBioVariavel(user.Usuario, user.TemplateA, user.TemplateB);
                    }
                    if (Result == 0)
                    {
                        LimparlstCadastroInner();
                        AtualizarLabelStatusCap("Digital envidada com sucesso!");
                        AtualizarMouseCursor(Cursors.Default);
                    }
                }
            }
        }
        #region EnviarListaUsuariosSemDigital
        //***********************************************************************************
        //Envio Lista de Usuários sem digital
        //***********************************************************************************
        public void EnviarListaUsuariosSemDigitail(List<string> ListaUsuarioSemDigital)
        {
            int Ret = -1;
            AtualizarMouseCursor(Cursors.WaitCursor);
            for (int i = 0; i < ListaUsuarioSemDigital.Count; i++)
            {
                Application.DoEvents();
                EasyInner.IncluirUsuarioSemDigitalBio(ListaUsuarioSemDigital[i]);
            }
            if (Conectar())
            {
                Ret = EasyInner.EnviarListaUsuariosSemDigitalBio(InnerAtual.Numero);

                if (Ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    MessageBox.Show("Lista de usuários sem digital enviada com sucesso!");
                }
            }
            EasyInner.FecharPortaComunicacao();
            AtualizarMouseCursor(Cursors.Default);
            Application.DoEvents();
        }
        #endregion

        #endregion

        #region Métodos Auxiliares

        #region testaConexaoInner
        /// <summary>
        /// Metodo responsável por realizar um comando simples com o equipamento para detectar
        /// se esta conectado.
        /// </summary>
        /// <param name="UiBIO"></param>
        /// <returns></returns>
        private int testaConexaoInner()
        {
            int RetRelogio = -1;
            byte Dia = 0;
            byte Mes = 0;
            byte Ano = 0;
            byte Hora = 0;
            byte Minuto = 0;
            byte Segundo = 0;

            RetRelogio = EasyInner.ReceberRelogio(InnerAtual.Numero, ref Dia, ref Mes, ref Ano, ref Hora, ref Minuto, ref Segundo);
            return RetRelogio;
        }
        #endregion

        #region Conectar
        /// <summary>
        /// Rotina responsável por efetuar a conexão com o Inner
        /// </summary>
        /// <param name="UiMainBIO"></param>
        /// <returns></returns>
        /// 
        public bool Conectar()
        {
            int Fim;
            int Ret = -1;
            //Define o tipo de conexão selecionada no Combo..
            EasyInner.DefinirTipoConexao((byte)InnerAtual.TipoConexao);

            //Fecha as conexões caso esteja aberta..
            EasyInner.FecharPortaComunicacao();

            //Abre a porta de Conexão conforme a Porta Indicada..
            Ret = EasyInner.AbrirPortaComunicacao(InnerAtual.Porta);

            System.DateTime Data;
            Data = System.DateTime.Now;

            //Tenta Realizar a Conexão
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                //Registra o tempo fim de conexão (tempo atual +15)
                Fim = (int)EasyInner.RetornarSegundosSys() + 15;

                //Realiza loop enquanto o tempo fim for menor que o tempo atual, e o comando retornado diferente de OK.
                do
                {
                    Ret = testaConexaoInner();
                    Thread.Sleep(100);
                }
                while ((EasyInner.RetornarSegundosSys() <= Fim) && (Ret != (int)Enumeradores.Retorno.RET_COMANDO_OK));

                //Caso o retorno seja OK.. volta a função chamadora..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    LimparListManutencao();
                    AtualizarInfoTelaManutencao("Conectou ao Inner!");
                    Application.DoEvents();
                    return true;
                }
                else
                {
                    //Exibe mensagem de erro para o Usuário..
                    LimparListManutencao();
                    AtualizarInfoTelaManutencao("Erro ao conectar com o Inner!");
                    LimparListManutencao();
                    Application.DoEvents();
                    return false;
                }
            }
            else
            {
                LimparListManutencao();
                AtualizarInfoTelaManutencao("Não conectou ao Inner!");
                Application.DoEvents();
                return false;
            }
        }
        #endregion

        #region Pausa
        private static void Pausa(int Tempo)
        {
            System.Threading.Thread.Sleep(Tempo);
            Application.DoEvents();
        }
        #endregion

        #region SetarTimeoutBio
        private void SetarTimeoutBio()
        {
            frmBiometrico.Timeout = 0;
            frmBiometrico.Timeout = (int)EasyInner.RetornarSegundosSys() + 7;
        }
        #endregion

        #region EsperaRespostaBio
        private bool EsperaRespostaBio(int Ret)
        {
            Thread.Sleep(300);
            return ((Ret != (int)Enumeradores.Retorno.RET_COMANDO_OK) && ((int)EasyInner.RetornarSegundosSys() <= frmBiometrico.Timeout));
        }
        #endregion

        #region ValidaNumeroUsuario
        //***********************************************************************************
        //Preenche com 0 a esquerda caso não tenha 10 Números
        //***********************************************************************************
        private static bool ValidaNumeroUsuario(ref string NumUsuario)
        {
            int saida;

            //Testa se é um Número
            if (int.TryParse(NumUsuario, out saida))
            {
                while (NumUsuario.Length < 10)
                {
                    NumUsuario = "0" + NumUsuario;
                }
                return true;
            }
            else
                return false;
        }
        #endregion

        #region TratarRetornoErro
        //***********************************************************************************
        //Apresenta a mensagem de acordo com o erro retornado
        //***********************************************************************************
        private static void TratarRetornoErro(int Ret)
        {
            switch ((Enumeradores.RetornoBIO)Ret)
            {
                case Enumeradores.RetornoBIO.FALHA_NA_COMUNICACAO:
                    MessageBox.Show("Erro: Falha na comunicação com o Inner BIO.", "Erro");
                    break;
                case Enumeradores.RetornoBIO.PROCESSANDO_ULTIMO_COMANDO:
                    MessageBox.Show("Atenção: Ainda processando último Comando.", "Erro");
                    break;
                case Enumeradores.RetornoBIO.FALHA_NA_COMUNICACAO_COM_PLACA_BIO:
                    MessageBox.Show("Erro: Falha na comunicação com a placa BIO.", "Erro");
                    break;
                case Enumeradores.RetornoBIO.INNER_BIO_NAO_ESTA_EM_MODO_MASTER:
                    MessageBox.Show("Erro: Inner BIO não esta em moddo MASTER.", "Erro");
                    break;
                case Enumeradores.RetornoBIO.USUARIO_JA_CADASTRADO_NO_BANCO_DE_DADOS_INNER_BIO:
                    MessageBox.Show("Erro: Usuário ja cadastrado no banco de dados do Inner BIO.", "Erro");
                    break;
                case Enumeradores.RetornoBIO.USUARIO_NAO_CADASTRADO_NO_BANCO_DE_DADOS_INNER_BIO:
                    MessageBox.Show("Erro: Usuário não cadastrado no banco de dados Inner BIO.", "Erro");
                    break;
                case Enumeradores.RetornoBIO.BASE_DE_DADOS_DE_USUARIOS_ESTA_CHEIA:
                    MessageBox.Show("Erro: Base de dados de Usuários esta cheia.", "Erro");
                    break;
                case Enumeradores.RetornoBIO.ERRO_NO_SEGUNDO_DEDO_DO_USUARIO:
                    MessageBox.Show("Erro: Erro no segundo dedo do Usuário.", "Erro");
                    break;
                case Enumeradores.RetornoBIO.SOLICITACAO_PARA_INNER_BIO_INVALIDA:
                    MessageBox.Show("Erro: Solicitação para Inner BIO Inválida.", "Erro");
                    break;
                default:
                    MessageBox.Show("Erro: Mensagem Indefinida", "Erro");
                    break;
            }
        }

        #endregion

        #region BioLight
        /// <summary>
        /// Retorna o modelo BioLight
        /// </summary>
        /// <param name="UiMainBIO"></param>
        /// <returns></returns>
        public bool BioLight()
        {
            bool placaLight = false;
            int Ret1 = -1;
            int modelo = 0;

            //Solicita Modelo
            Ret1 = EasyInner.SolicitarModeloBio(InnerAtual.Numero);
            SetarTimeoutBio();
            LimparListManutencao();
            do
            {
                if (Ret1 == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    Thread.Sleep(5);

                    //Recebe Modelo
                    Ret1 = EasyInner.ReceberModeloBio(InnerAtual.Numero, 0, ref modelo);
                    if (Ret1 == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                    {
                        switch (modelo)
                        {
                            case 2:
                                AtualizarInfoTelaManutencao("Modelo do bio: Light usuários  := FIM10).");
                                placaLight = true;
                                break;
                            case 4: 
                                AtualizarInfoTelaManutencao("Modelo do bio: 1000/4000 usuários  := FIM01).");
                                break;
                            case 51: 
                                AtualizarInfoTelaManutencao("Modelo do bio: 1000/4000 usuários  := FIM2030).");
                                break;
                            case 52:
                                AtualizarInfoTelaManutencao("Modelo do bio: 1000/4000 usuários  := FIM2040).");
                                break;
                            case 48:
                                AtualizarInfoTelaManutencao("Modelo do bio: Light 100 usuários  := FIM3030).");
                                placaLight = true;
                                break;
                            case 64:
                                AtualizarInfoTelaManutencao("Modelo do bio: Light 100 usuários  := FIM3040).");
                                placaLight = true;
                                break;
                            case 80: 
                                AtualizarInfoTelaManutencao("Modelo do bio: 1000/4000 usuários FIM5060.");
                                break;
                            case 82: 
                                AtualizarInfoTelaManutencao("Modelo do bio: 1000/4000 usuários FIM5260.");
                                break;
                            case 83:
                                AtualizarInfoTelaManutencao("Modelo do bio: Light 100 usuários FIM5360.");
                                placaLight = true;
                                break;
                            case 255: 
                                AtualizarInfoTelaManutencao("Modelo do bio: Desconhecido");
                                break;
                        }
                    }
                }
            } while (EsperaRespostaBio(Ret1));
            return placaLight;
        }
        #endregion

        #region inserirTemplateUsr
        /// <summary>
        /// O usuário será cadastrado no Inner bio com o número do cartão
        /// Retorna o resultado
        /// </summary>
        /// <param name="UiMainBIO"></param>
        /// <param name="Usr"></param>
        /// <param name="numTpl"></param>
        /// <returns></returns>
        private bool inserirTemplateUsr(FrmBIO UiMainBIO, String Usr, int numTpl)
        {
            bool Retorno = false;
            int Ret = -1;

            //Solicita inserção
            Ret = EasyInner.InserirUsuarioLeitorBio(InnerAtual.Numero, (byte)numTpl, Usr);

            Thread.Sleep(2000);
            SetarTimeoutBio();

            do
            {
                //Retorna resultado inserção          
                Application.DoEvents();
                Ret = EasyInner.ResultadoInsercaoUsuarioLeitorBio(InnerAtual.Numero, 0);

            } while (EsperaRespostaBio(Ret));

            if (numTpl == 0)
            {
                numTpl++;
            }

            LimparlstCadastroInner();

            //Resultado do cadastro
            switch (Ret)
            {
                case (byte)Enumeradores.Retorno.RET_COMANDO_OK:
                    Retorno = true;
                    MessageBox.Show("Digital " + numTpl + " capturada com sucesso.");
                    break;
                case (byte)Enumeradores.Retorno.RET_BIO_USR_JA_CADASTRADO:
                    MessageBox.Show("Usuário já existe.");
                    AtualizarInfoTelaCadLeitorInner("Selecione um Comando");
                    break;
                case (byte)Enumeradores.Retorno.RET_BIO_BASE_CHEIA:
                    MessageBox.Show("Não é possível incluir novo usuário, a base está cheia.");
                    AtualizarInfoTelaCadLeitorInner("Selecione um Comando");
                    break;
                case (byte)Enumeradores.Retorno.RET_BIO_FALHA_COMUNICACAO:
                    MessageBox.Show("Houve falha de comunicação, favor repetir o comando.");
                    AtualizarInfoTelaCadLeitorInner("Selecione um Comando");
                    break;
                case (byte)Enumeradores.Retorno.RET_BIO_DIG_NAO_CONFERE:
                    MessageBox.Show("As digitais não conferem");
                    AtualizarInfoTelaCadLeitorInner("Selecione um Comando");
                    break;
            }

            return (Retorno);
        }
        #endregion

        #region CompletaString
        //***********************************************************************************
        //Completa string de acordo com os parâmetros enviados
        //***********************************************************************************
        private static String CompletaString(String var1, int Len, String complemento)
        {
            while (var1.ToString().Length < Len)
            {
                var1 = complemento + var1;
            }
            return (var1);
        }
        #endregion

        #endregion

        public void EnviarDigitaisInner(List<UsuarioBio> ListaEnviar)
        {
            ListaUsuariosBio = ListaEnviar;
            EnviarSelecionado();
        }
    }
}
