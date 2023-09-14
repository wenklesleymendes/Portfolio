//******************************************************************************
//A Topdata Sistemas de Automação Ltda não se responsabiliza por qualquer
//tipo de dano que este software possa causar, este exemplo deve ser utilizado
//apenas para demonstrar a comunicação com os equipamentos da linha Inner.
//
//Exemplo On-Line
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
using System.Linq;

//Referências Nitgen
using NBioBSPCOMLib;
using NITGEN.SDK.NBioBSP;

//Referências Projeto
using EasyInnerSDK.Entity;
using System.Configuration;
using EasyInnerSDK.DAO;
using ExemploOnline.Entity;

//Referência EasyInner
namespace EasyInnerSDK.UI
{     
    public class FrmOnlineController
    {
        #region Propriedades
        //Catraca

        private byte VersaoAlta = 0;

        public bool LiberaEntrada = false;
        public bool LiberaSaida = false;
        public bool LiberaEntradaInvertida = false;
        public bool LiberaSaidaInvertida = false;

        public string ultCartao;

        public OnUpdateDisplay UpdateDisplay { get; set; }

        public Dictionary<int, Inner> ListInners { get; set; }

        private byte InnerAcessoBio;
        private int Porta { get; set; }
        private bool Executando { get; set; }

        private DAOUsuarios AcessoLista;

        private List<Bilhete> ListaBilhetes;

        #endregion

        #region Métodos Auxiliares

        public FrmOnlineController(FrmOnline frm)
        {
            UpdateDisplay = new OnUpdateDisplay(frm);
            ListInners = new Dictionary<int, Inner>();
        }

        public void RemoverInnerLista(Inner InnerAtual)
        {
            foreach (Inner inner in ListInners.Values)
            {
                if (inner.Numero == InnerAtual.Numero)
                {
                    ListInners.Remove(InnerAtual.Numero);
                    if (ListInners.Count == 0)
                    {
                        break;
                    }
                }
            }
        }

        public void AdicionarInner()
        {
            //Cria um novo Objeto Inner.
            Inner objInner = new Inner();
            if (UpdateDisplay.getInfoInner() == false)
            {
                return;
            }
            objInner.Biometrico = UpdateDisplay.Biometrico;

            //Se catraca
            objInner.Catraca = UpdateDisplay.Acionamento != (byte)Enumeradores.Acionamento.Acionamento_Coletor;

            //Seta nas configurações do Inner os dados informados em tela
            objInner.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            objInner.EstadoTeclado = Enumeradores.EstadosTeclado.TECLADO_EM_BRANCO;
            objInner.Numero = UpdateDisplay.NumInner;
            objInner.QtdDigitos = UpdateDisplay.QtdDigitos;
            objInner.Teclado = UpdateDisplay.Teclado;
            objInner.Lista = UpdateDisplay.ListaOff;
            objInner.ListaBioSemDigital = UpdateDisplay.ListaSemDigital;
            objInner.TipoLeitor = UpdateDisplay.TipoLeitor;
            objInner.Identificacao = (byte)(UpdateDisplay.Identificacao ? Enumeradores.Opcao.SIM : Enumeradores.Opcao.NAO);
            objInner.Verificacao = (byte)(UpdateDisplay.Verificacao ? Enumeradores.Opcao.SIM : Enumeradores.Opcao.NAO);
            objInner.DoisLeitores = UpdateDisplay.DoisLeitores;
            objInner.VariacaoInner = 0;
            objInner.Acionamento = UpdateDisplay.Acionamento;
            objInner.CatInvertida = UpdateDisplay.CatInvertida;
            objInner.PadraoCartao = UpdateDisplay.PadraoCartao;
            objInner.TipoConexao = UpdateDisplay.TipoConexao;
            objInner.Porta = UpdateDisplay.Porta;
            objInner.TipoComBio = UpdateDisplay.ModuloBio;

            foreach (Inner InnerAtual in ListInners.Values)
            {
                if (objInner.Numero == InnerAtual.Numero)
                {
                    MessageBox.Show("Inner já cadastrado!", "Cadastro Inner");
                    return;
                }
            }

            ListInners.Add(objInner.Numero, objInner);
            UpdateDisplay.AdicionarInnerLista(objInner);
        }

        public void IniciarMaquina()
        {
            try
            {
                Executando = true;
                UpdateDisplay.HabilitarBotoes(false);
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
                UpdateDisplay.HabilitarBotoes(true);
                //Exibe no rodapé o Fim da execução..
                UpdateDisplay.AtualizarStatus("Maquina parada");

                RetornarEstadoInners(Enumeradores.EstadosInner.ESTADO_CONECTAR, Enumeradores.EstadosTeclado.TECLADO_EM_BRANCO);

                //Fecha a porta da Easy Inner.
                EasyInner.FecharPortaComunicacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
            }
        }
        /// <summary>
        /// De acordo com o que foi informado (Esquerda ou Direita)
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="lado"></param>
        public void HABILITA_LADO_CATRACA(string lado, bool Esquerda)
        {
            if (lado == "Entrada")
            {
                //entrada
                if (Esquerda == false)
                {
                    LiberaEntrada = true;
                    LiberaEntradaInvertida = false;
                }
                else
                {
                    LiberaEntradaInvertida = true;
                    LiberaEntrada = false;
                }
            }

            if (lado == "Saida")
            {
                //saída
                if (Esquerda == false)
                {
                    LiberaSaida = true;
                    LiberaSaidaInvertida = false;
                }
                else
                {
                    LiberaSaidaInvertida = true;
                    LiberaSaida = false;
                }
            }
        }

        private void AtualizarEstadosInner()
        {
            foreach (Inner inner in ListInners.Values)
            {
                UpdateDisplay.AtualizarEstadoInner(inner.Numero, Enumeradores.EstadosInner.ESTADO_CONECTAR);
            }
        }

        private void RetornarEstadoInners(Enumeradores.EstadosInner estadosInner, Enumeradores.EstadosTeclado EstadoTeclado)
        {
            foreach (Inner inner in ListInners.Values)
            {
                inner.EstadoAtual = estadosInner;
                inner.EstadoTeclado = EstadoTeclado;
            }
        }

        /// <summary>
        /// Método responsável pela liberação de acesso. Somente usuarios listado
        /// serão liberados. Esta consulta deverá ser feita em sua base de dados.
        /// </summary>
        /// <param name="NumCartao"></param>
        /// <returns></returns>
        private bool LiberarAcesso(string NumCartao)
        {
            bool acesso = false;

            List<Usuarios> Cartao = AcessoLista.ConsultarUsuarios(0);

            for (int index = 0; index < Cartao.Count; index++)
            {
                if (Utils.RemZeroEsquerda(Cartao[index].Usuario) == Utils.RemZeroEsquerda(NumCartao))
                {
                    acesso = true;
                }
            }
            return acesso;
        }

        //***********************************************************************************
        //CONFIGURAÇÃO LEITORES
        //De acordo com o lado da catraca, coletor ou se é dois leitores
        //***********************************************************************************
        private void DefineValoresParaConfigurarLeitores(Inner innerAtual)
        {
            //Configuração Catraca Esquerda ou Direita
            //define os valores para configurar os leitores de acordo com o tipo de inner
            if (innerAtual.DoisLeitores)
            {
                if (innerAtual.CatInvertida == false)
                {
                    //Direita Selecionado
                    innerAtual.ValorLeitor1 = Convert.ToByte(Enumeradores.Operacao.SOMENTE_ENTRADA);
                    innerAtual.ValorLeitor2 = Convert.ToByte(Enumeradores.Operacao.SOMENTE_SAIDA);
                }
                else
                {
                    //Esquerda Selecionado
                    innerAtual.ValorLeitor1 = Convert.ToByte(Enumeradores.Operacao.SOMENTE_SAIDA);
                    innerAtual.ValorLeitor2 = Convert.ToByte(Enumeradores.Operacao.SOMENTE_ENTRADA);
                }
            }
            else
            {
                if (innerAtual.CatInvertida == false || innerAtual.Catraca == false)
                {
                    //Direita Selecionado
                    innerAtual.ValorLeitor1 = Convert.ToByte(Enumeradores.Operacao.ENTRADA_E_SAIDA);
                }
                else
                {
                    //Esquerda Selecionado
                    innerAtual.ValorLeitor1 = Convert.ToByte(Enumeradores.Operacao.ENTRADA_E_SAIDA_INVERTIDAS);
                }

                innerAtual.ValorLeitor2 = Convert.ToByte(Enumeradores.Operacao.DESATIVADO);

            }
        }

        /// <summary>
        /// Monta as configurações necessária para o funcionamento do Inner. Esta
        /// função é utilizada on-line ou off-line. modo = 0 off line/modo = 1 on line
        /// </summary>
        /// <param name="innerAtual"></param>
        /// <param name="modo"></param>
        private void MontaConfiguracaoInner(Inner innerAtual, Enumeradores.modoComunicacao modo)
        {
            //Antes de realizar a configuração precisa definir o Padrão do cartão 
            if (innerAtual.PadraoCartao == 0)
            {
                EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_TOPDATA);
            }
            else
            {
                EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_LIVRE);
            }
            
            //Define Modo de comunicação
            if (modo == Enumeradores.modoComunicacao.MODO_OFF_LINE)
            {
                //Configurações para Modo Offline.
                //Prepara o Inner para trabalhar no modo Off-Line, porém essa função
                //ainda não envia essa informação para o equipamento.
                EasyInner.ConfigurarInnerOffLine();
            }
            else
            {
                //Configurações para Modo Online.
                //Prepara o Inner para trabalhar no modo On-Line, porém essa função
                //ainda não envia essa informação para o equipamento.
                EasyInner.ConfigurarInnerOnLine();
            }

            //Verificar
            //Acionamentos 1 e 2
            //Configura como irá funcionar o acionamento(rele) 1 e 2 do Inner, e por
            //quanto tempo ele será acionado.
            switch (innerAtual.Acionamento)
            {
                //Coletor
                case (byte)Enumeradores.Acionamento.Acionamento_Coletor:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 5);

                    if (innerAtual.DoisLeitores)
                    {
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_ENTRADA);
                        EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.SOMENTE_SAIDA);
                    }
                    else
                    {
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.ENTRADA_E_SAIDA);
                    }
                    break;

                //Catraca
                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada_E_Saida:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA_OU_SAIDA, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
                    EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.ENTRADA_E_SAIDA);
                    if (innerAtual.DoisLeitores)
                    {
                        EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.ENTRADA_E_SAIDA);
                    }
                    else
                    {
                        EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.DESATIVADO);
                    }
                    break;

                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada:
                    if (innerAtual.CatInvertida)
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_ENTRADA);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_SAIDA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_SAIDA);
                    }
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
                    EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.DESATIVADO);
                    break;

                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Saida:
                    if (innerAtual.CatInvertida)
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_SAIDA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_SAIDA);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_ENTRADA);
                    }
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
                    EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.DESATIVADO);
                    break;


                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Saida_Liberada:
                    if (innerAtual.CatInvertida)
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_ENTRADA_LIBERADA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_SAIDA);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_SAIDA_LIBERADA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_ENTRADA);
                    }
                    EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.DESATIVADO);
                    break;


                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada_Liberada:
                    if (innerAtual.CatInvertida)
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_SAIDA_LIBERADA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_ENTRADA);
                    }
                    else
                    {
                        EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_ENTRADA_LIBERADA, 5);
                        EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_SAIDA);
                    }
                    EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.DESATIVADO);
                    break;

                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Liberada_2_Sentidos:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_LIBERADA_DOIS_SENTIDOS, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
                    EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.ENTRADA_E_SAIDA);
                    if (innerAtual.DoisLeitores)
                    {
                        EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.ENTRADA_E_SAIDA);
                    }
                    else
                    {
                        EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.DESATIVADO);
                    }
                    break;

                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Sentido_Giro:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.CATRACA_LIBERADA_DOIS_SENTIDOS_MARCACAO_REGISTRO, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.NAO_UTILIZADO, 0);
                    if (innerAtual.DoisLeitores)
                    {
                        EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.ENTRADA_E_SAIDA);
                    }
                    else
                    {
                        EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.DESATIVADO);
                    }
                    break;

                case (byte)Enumeradores.Acionamento.Acionamento_Catraca_Urna:
                    EasyInner.ConfigurarAcionamento1((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_ENTRADA, 5);
                    EasyInner.ConfigurarAcionamento2((int)Enumeradores.FuncaoAcionamento.ACIONA_REGISTRO_SAIDA, 5);
                    EasyInner.ConfigurarLeitor1((byte)Enumeradores.Operacao.SOMENTE_ENTRADA);
                    EasyInner.ConfigurarLeitor2((byte)Enumeradores.Operacao.SOMENTE_SAIDA);
                    break;
            }

            // define os valores para configurar os leitores de acordo com o tipo de inner
            DefineValoresParaConfigurarLeitores(innerAtual);
            EasyInner.ConfigurarLeitor1(innerAtual.ValorLeitor1);
            EasyInner.ConfigurarLeitor2(innerAtual.ValorLeitor2);

            //Configurar tipo do leitor
            switch (innerAtual.TipoLeitor)
            {
                case (byte)Enumeradores.TipoLeitor.CODIGO_DE_BARRAS:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.CODIGO_DE_BARRAS);
                    break;
                case (byte)Enumeradores.TipoLeitor.MAGNETICO:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.MAGNETICO);
                    break;
                case (byte)Enumeradores.TipoLeitor.PROXIMIDADE_ABATRACK2:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.PROXIMIDADE_ABATRACK2);
                    break;
                case (byte)Enumeradores.TipoLeitor.WIEGAND:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.WIEGAND);
                    break;
                case (byte) Enumeradores.TipoLeitor.PROXIMIDADE_SMART_CARD_SERIAL:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.PROXIMIDADE_SMART_CARD_SERIAL);
                    break;
                case (byte)Enumeradores.TipoLeitor.CODIGO_BARRAS_SERIAL:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.CODIGO_BARRAS_SERIAL);
                    break;
                case (byte)Enumeradores.TipoLeitor.WIEGAND_FC_SEM_ZERO:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.WIEGAND_FC_SEM_ZERO);
                    break;
                //case (byte)Enumeradores.TipoLeitor.QRCODE_LETRAS:
                //    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.QRCODE_LETRAS);
                //    break;
                default:
                    EasyInner.ConfigurarTipoLeitor((byte)Enumeradores.TipoLeitor.CODIGO_DE_BARRAS);
                    break;
            }
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

            if (innerAtual.CartMaster)
            {
                EasyInner.DefinirNumeroCartaoMaster(innerAtual.Master);
            }
            //Habilitar teclado
            EasyInner.HabilitarTeclado((byte)(innerAtual.Teclado ? Enumeradores.Opcao.SIM : Enumeradores.Opcao.NAO), 0);

            //Configura equipamentos com dois leitores
            if (innerAtual.DoisLeitores)
            {
                // exibe mensagens do segundo leitor
                EasyInner.ConfigurarWiegandDoisLeitores(0, (byte)Enumeradores.Opcao.SIM);
            }

            //Registra acesso negado
            EasyInner.RegistrarAcessoNegado(1);

            //Catraca
            //Define qual será o tipo do registro realizado pelo Inner ao aproximar um
            //cartão do tipo proximidade no leitor do Inner, sem que o usuário tenha
            //pressionado a tecla entrada, saída ou função.
            if ((innerAtual.Acionamento == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada_E_Saida)
                || (innerAtual.Acionamento == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Liberada_2_Sentidos)
                || (innerAtual.Acionamento == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Sentido_Giro))
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
                if ((innerAtual.Acionamento == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada)
                    || (innerAtual.Acionamento == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Saida_Liberada))
                {
                    if (innerAtual.CatInvertida == false)
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
                    if (innerAtual.CatInvertida == false)
                    {
                        //Configura o tipo de registro que será associado a uma marcação
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(11);  // 10 – Registrar sempre como entrada.

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
                        //Configura o tipo de registro que será associado a uma marcação
                        EasyInner.DefinirFuncaoDefaultLeitoresProximidade(10);  // Inverte o sentido de entrada.

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
            if (VersaoAlta>=5)
            {
                    EasyInner.SetarBioVariavel(1); 
                    EasyInner.ConfigurarBioVariavel(1);
            }                
            
            if (innerAtual.QtdDigitos <= 14)
            {
                //Configura para receber o horario dos dados quando Online.
                EasyInner.ReceberDataHoraDadosOnLine((byte)(Enumeradores.Opcao.SIM));
            }
            //Define tipo lista off
            if (innerAtual.Lista)
            {
                EasyInner.DefinirTipoListaAcesso(1);
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

            for(int i = 0; i < numeroInvertido.Length; i++)
            {
                //pega dígito por dígito do número digitado
                numero = Convert.ToInt32(numeroInvertido.Substring(i,1)); 
                //multiplica o dígito por 2 elevado ao expoente, e armazena o resultado em soma
                soma += numero * (int)Math.Pow(2,expoente);
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
            Configuracao = (InnerAtual.Teclado ? 1 : 0).ToString();

            if (!InnerAtual.Biometrico)
            {
                //Dois leitores
                if (InnerAtual.DoisLeitores)
                    Configuracao = "010" + //Leitor 2 só saida
                                   "001" + //Leitor 1 só entrada
                                   Configuracao;
                else //Apenas um leitores
                    Configuracao = "000" + //Leitor 2 Desativado
                                   "011" + //Leitor 1 configurado para Entrada e Saída
                                   Configuracao;

                Configuracao = "1" + // Habilitado
                               Configuracao;
            }
            else //Com Biometria 
            {
                Configuracao = "0" + //Bit Fixo
                               "1" + //Habilitado
                               InnerAtual.Identificacao + //Identificação
                               InnerAtual.Verificacao + //Verificação
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
            List<Horarios> ListaHorarios = Horarios.MontarListaHorarios();
            for (int index = 0; index < ListaHorarios.Count; index++)
            {
                //Insere no buffer da DLL horario de acesso
                EasyInner.InserirHorarioAcesso(ListaHorarios[index].Horario, ListaHorarios[index].Dia, ListaHorarios[index].Faixa,
                                                ListaHorarios[index].Hora, ListaHorarios[index].Minuto); //(1 - nº da tabela horario, 1 - dia da semana, 1 - faixa de horario, 8 - hora, 0 - minuto)
            }

            EasyInner.EnviarHorariosAcesso(InnerAtual.Numero);

        }

        //***********************************************************************************
        //MONTAR LISTA TOPDATA
        //Monta o buffer para enviar a lista nos inners da linha Inner, cartão padrão Topdata
        //***********************************************************************************
        private void MontarListaTopdata(Inner InnerAtual)
        {
            //Define qual padrao o Inner vai usar
            EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_TOPDATA);

            //Insere usuario da lista no buffer da DLL
            for (int i = 0; i < 5; i++)
            {
                //Insere usuário da lista no buffer da DLL
                EasyInner.InserirUsuarioListaAcesso(i.ToString(), 101);
            }

            EasyInner.EnviarListaAcesso(InnerAtual.Numero);
        }
        
        //***********************************************************************************
        //MONTAR LISTA LIVRE
        //Monta o buffer para enviar a lista nos inners da linha Inner, cartão padrão livre 14 dígitos
        //***********************************************************************************
        private void MontarListaLivre(Inner InnerAtual, List<Usuarios> Lista)
        {
            //Define qual padrao o Inner vai usar
            EasyInner.DefinirPadraoCartao((byte)Enumeradores.PadraoCartao.PADRAO_LIVRE); //(1 - Padrao Livre(Default))

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
        private int testaConexaoInner(Inner InnerAtual)
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

        #region ColetarBilhetes
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
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
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
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECEBER_QTD_BILHETES_OFF;
                    if (ListaBilhetes.Count > 0)
                    {
                        UpdateDisplay.AtualizarBilhetes(ListaBilhetes);
                    }
                }
                Cartao = null;
            }
            catch (Exception ex)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
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
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
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
                        UpdateDisplay.AtualizarBilhetes(ListaBilhetes);
                        if (InnerAtual.Catraca)
                        {
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_OFFLINE_CATRACA;
                        }
                        else
                        {
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_OFFLINE_COLETOR;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
            }
        }
        #endregion

        private void MontarBilheteRecebido(Inner InnerAtual)
        {
            string strCartao = "";
            string NumCartao = "";
            string Cartao = InnerAtual.BilheteOnline.Cartao.ToString();
            int tam = 0;
            if (InnerAtual.QtdDigitos > Cartao.Length)
            {
                tam = Cartao.Length;
            }
            else
            {
                tam = InnerAtual.QtdDigitos;
            }

            if (InnerAtual.BilheteOnline.Origem == 12)
            {
                strCartao = MontarCartaoBio(Cartao);
            }
            else
            {
                strCartao = MontarCartao(Cartao, tam);
            }

            //Se o cartão padrão for topdata, configura os dígitos do cartão como padrão topdata
            NumCartao = "";
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
            StringBuilder CartaoPronto = new StringBuilder();
            CartaoPronto.Append(NumCartao);
            InnerAtual.BilheteOnline.Cartao = CartaoPronto;
            //Adiciona bilhete coletado na Lista
            UpdateDisplay.AtualizarBilheteOnline(InnerAtual.BilheteOnline);
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

        private int ReceberModeloBio6xx(Inner InnerAtual)
        {
            int Ret = -1;
            Ret = EasyInner.RequisitarModeloBio(InnerAtual.Numero, InnerAtual.TipoComBio);
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                byte [] ModeloBio = new byte[4];
                Ret = EasyInner.RespostaModeloBio(InnerAtual.Numero, ModeloBio);
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    InnerAtual.ModeloBioInner = "Modelo bio " + Encoding.ASCII.GetString(ModeloBio);
                }
            }
            return Ret;
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
            while (Ret == (int)Enumeradores.Retorno.RET_BIO_PROCESSANDO);

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
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                //Retorna o resultado do comando SolicitarVersaoBio, a versão
                //do Inner Bio é retornado por referência nos parâmetros da
                //função.

                Ret = EasyInner.ReceberVersaoBio(InnerAtual.Numero, 0, ref VersaoAltaBio, ref VersaoBaixaBio);
                Application.DoEvents();
            }
            InnerAtual.VersaoBio = VersaoAltaBio + "." + VersaoBaixaBio;
            UpdateDisplay.AtualizarVersaoInner(InnerAtual);
            return Ret;
        }
        private int ReceberVersaoBio6xx(Inner InnerAtual)
        {
            int Ret = -1;
            Ret = EasyInner.RequisitarVersaoBio(InnerAtual.Numero, InnerAtual.TipoComBio);
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                byte[] VersaoBio = new byte[4];
                Ret = EasyInner.RespostaVersaoBio(InnerAtual.Numero, VersaoBio);
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    InnerAtual.VersaoInner = "Versão bio " + Encoding.ASCII.GetString(VersaoBio);
                }
            }
            return Ret;
        }

        #endregion

        #region Maquina de Estados

        #region MaquinaOnline
        /// <summary>
        /// FUNCIONAMENTO DA MÁQUINA DE ESTADOS
        /// MÉTODO RESPONSÁVEL EM EXECUTAR OS PROCEDIMENTOS DO MODO ONLINE
        /// A Máquina de Estados nada mais é do que uma rotina que fica em loop testando
        /// uma variável que chamamos de Estado. Dependendo do estado atual, executamos
        /// alguns procedimentos e em seguida alteramos o estado que será verificado pela
        /// máquina de estados novamente no próximo passo do loop.
        /// </summary>
        /// <param name="UiMainOnline"></param>
        private void MaquinaOnline()
        {
            try
            {
                int Ret2 = -1;

                //Define o tipo de conexão conforme selecionado em Combo (padrão Porta Fixa)
                EasyInner.DefinirTipoConexao((byte)ListInners.Values.First().TipoConexao);

                //Fecha as conexões caso esteja aberta..
                EasyInner.FecharPortaComunicacao();
                //Abre a porta de comunicação com o Inner..
                Ret2 = EasyInner.AbrirPortaComunicacao(ListInners.Values.First().Porta);

                //Tenta realizar a conexão com o Inner..
                Application.DoEvents();
                if (Ret2 == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Enquanto a variável Estiver Selecionada para prosseguir a maquina, executa o processo..
                    while (Executando)
                    {
                        //Para cada inner da Lista de Inners cadastrados na UI.
                        foreach (Inner InnerAtual in ListInners.Values)
                        {
                            //Verifica o Estado do Inner Atual..
                            switch (InnerAtual.EstadoAtual)
                            {
                                case Enumeradores.EstadosInner.ESTADO_CONECTAR:
                                    PASSO_ESTADO_CONECTAR(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_CFG_OFFLINE:
                                    PASSO_ESTADO_ENVIAR_CFG_OFFLINE(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_COLETAR_BILHETES:
                                    PASSO_ESTADO_COLETAR_BILHETES(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_CFG_ONLINE:
                                    PASSO_ESTADO_ENVIAR_CFG_ONLINE(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_DATA_HORA:
                                    PASSO_ESTADO_ENVIAR_DATA_HORA(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_PADRAO:
                                    PASSO_ESTADO_ENVIAR_MSG_PADRAO(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_CONFIGURAR_ENTRADAS_ONLINE:
                                    PASSO_ESTADO_CONFIGURAR_ENTRADAS_ONLINE(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_POLLING:
                                    PASSO_ESTADO_POLLING(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA:
                                    PASSO_ESTADO_LIBERA_GIRO_CATRACA(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_MONITORA_GIRO_CATRACA:
                                    PASSO_ESTADO_MONITORA_GIRO_CATRACA(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_PING_ONLINE:
                                    PASSO_ESTADO_ENVIAR_PING_ONLINE(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_RECONECTAR:
                                    PASSO_ESTADO_RECONECTAR(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_AGUARDA_TEMPO_MENSAGEM:
                                    PASSO_ESTADO_AGUARDA_TEMPO_MENSAGEM(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_DEFINICAO_TECLADO:
                                    PASSO_ESTADO_DEFINICAO_TECLADO(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_AGUARDA_DEFINICAO_TECLADO:
                                    PASSO_ESTADO_AGUARDA_DEFINICAO_TECLADO(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_URNA:
                                    PASSO_ESTADO_ENVIAR_MSG_URNA(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_MONITORA_URNA:
                                    PASSO_ESTADO_MONITORA_URNA(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_ACESSO_NEGADO:
                                    PASSO_ESTADO_ENVIAR_MSG_ACESSO_NEGADO(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_OFFLINE_CATRACA:
                                    PASSO_ESTADO_ENVIAR_MSG_OFF_CATRACA(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_CONFIGMUD_ONLINE_OFFLINE:
                                    PASSO_ESTADO_ENVIAR_CONFIGMUD_ONLINE_OFFLINE(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_VALIDAR_ACESSO:
                                    PASSO_ESTADO_VALIDAR_ACESSO(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_RECEBER_QTD_BILHETES_OFF:
                                    PASSO_ESTADO_RECEBER_QTD_BILHETES_OFF(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_RECEBER_FIRMWARE:
                                    PASSO_ESTADO_RECEBER_FIRMWARE(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_RECEBER_MODELO_BIO:
                                    PASSO_ESTADO_RECEBER_MODELO_BIO(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_URNA_CHEIA:
                                    PASSO_ESTADO_ENVIAR_MSG_URNA_CHEIA(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_VALIDA_URNA_CHEIA:
                                    PASSO_ESTADO_VALIDA_URNA_CHEIA(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_LISTA_OFFLINE:
                                    PASSO_ESTADO_ENVIAR_LISTA_OFFLINE(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_LISTA_SEMDIGITAL:
                                    PASSO_ESTADO_ENVIAR_LISTA_SEMDIGITAL(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_OFFLINE_COLETOR:
                                    PASSO_ESTADO_ENVIAR_MSG_OFF_COLETOR(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_DESLIGA_RELE:
                                    PASSO_ESTADO_DESLIGA_RELE(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_RECEBER_VERSAO_BIO:
                                    PASSO_ESTADO_RECEBER_VERSAO_BIO(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ACIONAR_RELE1:
                                    PASSO_ESTADO_ACIONAR_RELE1(InnerAtual);
                                    break;

                                case Enumeradores.EstadosInner.ESTADO_ACIONAR_RELE2:
                                    PASSO_ESTADO_ACIONAR_RELE2(InnerAtual);
                                    break;

                            }
                            Thread.Sleep(5);
                            UpdateDisplay.AtualizarEstadoInner(InnerAtual.Numero, InnerAtual.EstadoAtual);
                            Application.DoEvents();
                        }
                    }
                    Thread.EndCriticalRegion();
                }
                else
                {
                    MessageBox.Show("Erro ao tentar abrir a porta de comunicação.", "Atenção");
                    UpdateDisplay.HabilitarBotoes(true);
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
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONFIGURAR_ENTRADAS_ONLINE;
                if (InnerAtual.BilheteOnline.Origem == (int)Enumeradores.Origem.VIA_LEITOR2)
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
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                if (Bilhetes.Origem == (int)Enumeradores.Origem.URNA_CHEIA)
                {
                    UpdateDisplay.AtulizarLabelDados("URNA CHEIA");
                    EasyInner.AcionarBipLongo(InnerAtual.Numero);

                    //Vai para o estado de Urna Cheia
                    InnerAtual.TempoInicialMensagem = DateTime.Now;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_URNA_CHEIA;
                }
            }
            else{
                // Urna nao esta cheia, chama metodo para pedir o cartao.
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_URNA;
            }
        }

        private void PASSO_ESTADO_ENVIAR_MSG_URNA_CHEIA(Inner InnerAtual)
        {
            int ret = -1;

            ret = (byte)EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "   URNA CHEIA    ESVAZIAR URNA ");

            if (ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                EasyInner.AcionarBipLongo(InnerAtual.Numero);
                if (InnerAtual.InnerNetAcesso)
                    EasyInner.LigarLedVermelho(InnerAtual.Numero);

                InnerAtual.TempoInicialMensagem = DateTime.Now;
                InnerAtual.Tentativas = 0;
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_AGUARDA_TEMPO_MENSAGEM;
            }
            else
            {
                if (InnerAtual.Tentativas >= 3)
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                }
                //Adiciona 1 ao contador de tentativas
                InnerAtual.Tentativas++;
            }
        }
        #endregion

        #region Passos Da Maquina de Estados

        /// <summary>
        /// Inicia a conexão com o Inner
        /// Próximo passo: ESTADO_ENVIAR_CFG_OFFLINE
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_CONECTAR(Inner InnerAtual)
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
                    Ret = testaConexaoInner(InnerAtual);
                    Thread.Sleep(300);

                } while ((count++ < 10) && (Ret != (int)Enumeradores.Retorno.RET_COMANDO_OK));


                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //caso consiga o Inner vai para o Passo de Configuração OFFLINE, posteriormente para coleta de Bilhetes.
                    InnerAtual.Tentativas = 0;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECEBER_FIRMWARE;
                }
                else
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    
                    //Adiciona 1 contador de tentativas
                    InnerAtual.Tentativas++;
                }
            }
            catch (Exception ex)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
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
                String TipoComunicacao = InnerAtual.TipoConexao.ToString();

                //Configura a mudança automática
                //Habilita/Desabilita a mudança automática do modo OffLine do Inner para
                //OnLine e vice-versa.
                //Habilita a mudança Offline
                
                if (TipoComunicacao.IndexOf("TCP")!=-1)
                {
                    //Habilita a mudança Offline
                    
                    EasyInner.HabilitarMudancaOnLineOffLine(2, 10);
                }
                else
                {
                    //Habilita a mudança Offline
                    EasyInner.HabilitarMudancaOnLineOffLine(1, 10);
                }

                //Configura o teclado para quando o Inner voltar para OnLine após uma queda
                //para OffLine.
                EasyInner.DefinirConfiguracaoTecladoOnLine((Byte)InnerAtual.QtdDigitos, 0, 5, 17);

                //Define Mudanças OnLine
                //Função que configura BIT a BIT, Ver no manual Anexo III
                EasyInner.DefinirEntradasMudancaOnLine((Byte)ConfiguraEntradasMudancaOnLine(InnerAtual));

                if (InnerAtual.Biometrico)
                {
                    // Configura entradas mudança OffLine com Biometria
                    EasyInner.DefinirEntradasMudancaOffLineComBiometria((byte)(InnerAtual.Teclado ? Enumeradores.Opcao.SIM : Enumeradores.Opcao.NAO),
                        3, (byte)(InnerAtual.DoisLeitores ? 3 : 0),
                        InnerAtual.Verificacao, InnerAtual.Identificacao);
                }
                else
                {
                    // Configura entradas mudança OffLine
                    EasyInner.DefinirEntradasMudancaOffLine((byte)(InnerAtual.Teclado ? Enumeradores.Opcao.SIM : Enumeradores.Opcao.NAO),
                        (byte)(InnerAtual.DoisLeitores ? 1 : 3), (byte)(InnerAtual.DoisLeitores ? 2 : 0), 0);
                }

                //Define mensagem de Alteração Online -> Offline.
                EasyInner.DefinirMensagemPadraoMudancaOffLine(1, " Modo OffLine");

                //Define mensagem de Alteração OffLine -> OnLine.
                EasyInner.DefinirMensagemPadraoMudancaOnLine(1, "Modo Online");

                //Envia Configurações.
                Ret = EasyInner.EnviarConfiguracoesMudancaAutomaticaOnLineOffLine(InnerAtual.Numero);

                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    InnerAtual.Tentativas = 0;

                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONFIGURAR_ENTRADAS_ONLINE;
                    InnerAtual.TempoColeta = DateTime.Now;
                    InnerAtual.TentativasColeta = 0;
                }
                else
                {
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    InnerAtual.Tentativas++;
                }
            }
            catch (Exception)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
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
                if (!InnerAtual.CatInvertida)
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

                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    InnerAtual.Tentativas = 0;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_DATA_HORA;
                }
                else
                {
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    InnerAtual.Tentativas++;
                }
            }
            catch (Exception)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        /// <summary>
        /// Configura modo Offline
        /// Próximo passo: ESTADO_COLETAR_BILHETES
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_ENVIAR_CFG_OFFLINE(Inner InnerAtual)
        {
            int ret = 0;
            try
            {
                #region Realiza as configurações Offline do Inner Atual.
                Thread.BeginCriticalRegion();
                //Preenche os campos de configuração do Inner
                MontaConfiguracaoInner(InnerAtual, Enumeradores.modoComunicacao.MODO_OFF_LINE);

                ret = EasyInner.EnviarConfiguracoes(InnerAtual.Numero);

                //Envia o comando de configuração
                Application.DoEvents();
                InnerAtual.Tentativas++;
              
                if (ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_LISTA_OFFLINE;
                    InnerAtual.Tentativas = 0;
                    Thread.EndCriticalRegion();
                }
                else if (InnerAtual.Tentativas++ >= 3)
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                }
               
                #endregion
            }
            catch (Exception)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }

        private void PASSO_ESTADO_ENVIAR_LISTA_SEMDIGITAL(Inner InnerAtual)
        {
            try
            {
                int ret = 0;
                if (InnerAtual.ListaBioSemDigital)
                {

                    DAOUsuariosBio AcessoSD = new DAOUsuariosBio();
                    List<UsuarioSemDigital> ListaSD = AcessoSD.ConsultarUsuariosSD();
                    Application.DoEvents();
                    //Chama rotina que monta o buffer de cartoes que nao irao precisar da digital
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
                if (ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    if (InnerAtual.InnerNetAcesso)
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECEBER_QTD_BILHETES_OFF;
                    }
                    else
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_COLETAR_BILHETES;
                    }
                }
                else
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                }
            }
            catch (Exception)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }

        private void PASSO_ESTADO_ENVIAR_LISTA_OFFLINE(Inner InnerAtual)
        {
            int Ret = 0;
            //Define Lista e horários offline
            if (InnerAtual.Lista)
            {
                MontarHorarios(InnerAtual);

                //Define a Lista de verificação
                if (InnerAtual.PadraoCartao == 0)
                {
                    MontarListaTopdata(InnerAtual);
                }
                else
                {
                    List<Usuarios> ListaUsuarios = AcessoLista.ConsultarUsuarios(0);
                    MontarListaLivre(InnerAtual, ListaUsuarios);
                }

                //Define qual tipo de lista(controle) de acesso o Inner vai utilizar.
                //Utilizar lista branca (cartões fora da lista tem o acesso negado).
                Ret = EasyInner.DefinirTipoListaAcesso(1);
            }
            else
            {
                //Não utilizar a lista de acesso.
                EasyInner.DefinirTipoListaAcesso(0);
            }
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_LISTA_SEMDIGITAL;
            }
            else
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
            }
        }
        /// <summary>
        /// Mantém a mensagem no display por 2 segundos.
        /// Próximo passo: ESTADO_ENVIAR_MSG_PADRAO
        /// </summary>
        /// <param name="innerAtual"></param>
        private void PASSO_ESTADO_AGUARDA_TEMPO_MENSAGEM(Inner innerAtual)
        {
            try
            {
                //Após passar os 2 segundos volta para o passo enviar mensagem padrão
                TimeSpan tempo = DateTime.Now - innerAtual.TempoInicialMensagem;
                if (tempo.Seconds >= 2)
                {
                    if (innerAtual.InnerNetAcesso)
                    {
                        EasyInner.DesligarLedVermelho(innerAtual.Numero);
                    }
                    innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONFIGURAR_ENTRADAS_ONLINE;
                }
            }
            catch (Exception)
            {
                innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        /// <summary>
        /// Efetua a coleta dos bilhetes no modo Off-line
        /// Próximo passo: ESTADO_ENVIAR_CFG_ONLINE
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_COLETAR_BILHETES(Inner InnerAtual)
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
        /// Próximo passo: ESTADO_ENVIAR_DATA_HORA
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_ENVIAR_CFG_ONLINE(Inner InnerAtual)
        {
            try
            {
                Thread.BeginCriticalRegion();

                //Monta configuração modo Online

                MontaConfiguracaoInner(InnerAtual, Enumeradores.modoComunicacao.MODO_ON_LINE);
                
                //Envia as configurações ao Inner Atual.
                Application.DoEvents();
                int ret = EasyInner.EnviarConfiguracoes(InnerAtual.Numero);
                InnerAtual.Tentativas++;
                
                if (ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Caso consiga enviar as configurações, passa para o passo Enviar Data Hora
                    InnerAtual.Tentativas = 0;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_CONFIGMUD_ONLINE_OFFLINE;
                    Thread.EndCriticalRegion();
                }
                else if (InnerAtual.Tentativas >= 3)
                //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                }
                    
            }            
            catch (Exception)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }

        }

        /// <summary>
        /// Envia ao Inner data e hora atual
        /// Próximo passo: ESTADO_ENVIAR_MSG_PADRAO
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_ENVIAR_DATA_HORA(Inner InnerAtual)
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
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Vai para o passo de Envio de Msg Padrão..
                    InnerAtual.Tentativas = 0;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_CFG_ONLINE;
                }
                else
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    //Adiciona 1 ao contador de tentativas
                    InnerAtual.Tentativas++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        /// <summary>
        /// CONFIGURAR_ENTRADAS_ONLINE
        /// Preparação configuração online para entrar em modo Polling
        /// Próximo passo: ESTADO_POLLING
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_CONFIGURAR_ENTRADAS_ONLINE(Inner InnerAtual)
        {
            try
            {
                //Declaração de variáveis..
                int Ret = -1;

                //Converte Binário para Decimal
                int ValorDecimal = ConfiguraEntradasMudancaOnLine(InnerAtual); //Ver no manual Anexo III

                Ret = EasyInner.EnviarFormasEntradasOnLine(InnerAtual.Numero, (byte)InnerAtual.QtdDigitos,  //Qtd Digitos Teclado..
                                                           1,                      //Eco do Teclado no Display..
                                                           (byte)ValorDecimal,     //Valor decimal resultante da conversão Binário para Decimal
                                                           15,                     //Tempo teclado..
                                                           17);                    //Posição do Cursor no Teclado..
 
                //Testa o retorno do comando..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Vai para o Estado De Polling.
                    InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                    InnerAtual.Tentativas = 0;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_PADRAO;

                    if (InnerAtual.Catraca)
                    {
                        UpdateDisplay.FuncoesCatraca(true, true);
                    }
                    else
                    {
                        UpdateDisplay.FuncoesCatraca(true, false);
                    }
                }
                else
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    //Adiciona 1 ao contador de tentativas
                    InnerAtual.Tentativas++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
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
                        (int)Enumeradores.EnviarFormasEntradasOnLine.EntradasON_ACEITA_TECLADO,
                        10, // Tempo de entrada do Teclado (10s).
                        32);//Posição do Cursor (32 fica fora..)

           //Se Retorno OK, vai para proximo estado..
           if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
           {
             InnerAtual.Tentativas = 0;
             InnerAtual.EstadoTeclado = Enumeradores.EstadosTeclado.AGUARDANDO_TECLADO;
             InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_AGUARDA_DEFINICAO_TECLADO;
           }
           else
           {
             //Caso o retorno não for OK, tenta novamente até 3x..
             if (InnerAtual.Tentativas++ > 3)
             {
                 InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
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
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Se está aguardando retorno (entrada ou saída)
                    if (InnerAtual.EstadoTeclado == Enumeradores.EstadosTeclado.AGUARDANDO_TECLADO)
                    {
                        //****************************************************
                        //Entrada, saída liberada, confirma, anula ou função tratar mensagem
                        //66 - "Entrada" via teclado
                        //67 - "Saída" via teclado
                        //35 - "Confirma" via teclado
                        //42 - "Anula" via teclado
                        //65 - "Função" via teclado
                        if (Convert.ToInt16(Complemento.ToString()) == (int)Enumeradores.Origem.TECLA_ENTRADA) //entrada
                        {
                            HABILITA_LADO_CATRACA("Entrada", InnerAtual.CatInvertida);
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                        }
                        else if (Convert.ToInt16(Complemento.ToString()) == (int)Enumeradores.Origem.TECLA_SAIDA)   //saída
                        {
                            HABILITA_LADO_CATRACA("Saida", InnerAtual.CatInvertida);
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                        }
                        else if (Convert.ToInt16(Complemento.ToString()) == (int)Enumeradores.Origem.TECLA_CONFIRMA) //confirma
                        {
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                        }
                        else if (Convert.ToInt16(Complemento.ToString()) == (int)Enumeradores.Origem.TECLA_ANULA) //anula
                        {
                            EasyInner.LigarLedVerde(InnerAtual.Numero);
                            InnerAtual.TempoInicialMensagem = DateTime.Now;
                            InnerAtual.Tentativas = 0;
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_PADRAO;
                        }
                        else if (Convert.ToInt16(Complemento.ToString()) == (int)Enumeradores.Origem.TECLA_FUNCAO) //função
                        {
                            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_DEFINICAO_TECLADO;
                        }
                        InnerAtual.EstadoTeclado = Enumeradores.EstadosTeclado.TECLADO_EM_BRANCO;
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
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_PING_ONLINE;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        /// <summary>
        /// É onde funciona todo o processo do modo online
        /// Passagem de cartão, catraca, urna, mensagens...
        /// </summary>
        /// <param name="UiMainOnline"></param>
        /// <param name="InnerAtual"></param>
        private void PASSO_ESTADO_POLLING(Inner InnerAtual)
        {
            try
            {
                //Declaração de Variáveis..
                byte Origem = 0;
                byte Complemento = 0;
                StringBuilder Cartao = new StringBuilder();
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
                //Envia o Comando Receber Dados Online..
                Ret = EasyInner.ReceberDadosOnLine(InnerAtual.Numero,
                        ref Origem, ref Complemento, Cartao, ref Dia, ref Mes, ref Ano, ref Hora,
                        ref Minuto, ref Segundo);

                //Atribui Temporizador
                InnerAtual.Temporizador = DateTime.Now;

                //Testa o Retorno do Comando..
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Teste se a origem é Fim de Acionamento, Função, Anula ou Giro de Catraca..
                    //Caso seja alguma destas origens, retorna para a maquina de estados.
                    if (Complemento == (int)Enumeradores.Origem.TECLA_FUNCAO
                        || Complemento == (int)Enumeradores.Origem.TECLA_ANULA
                        || ((Cartao.Length == 0) && !(InnerAtual.EstadoTeclado == Enumeradores.EstadosTeclado.AGUARDANDO_TECLADO)))                   
                    {
                        //Zera contador de tentativas
                        InnerAtual.Tentativas = 0;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_AGUARDA_TEMPO_MENSAGEM;
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
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_VALIDAR_ACESSO;
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
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_PING_ONLINE;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        /// <summary>
        /// Envia mensagem padrão modo Online
        /// Próximo passo: ESTADO_CONFIGURAR_ENTRADAS_ONLINE
        /// </summary>
        /// <param name="innerAtual"></param>
        private void PASSO_ESTADO_ENVIAR_MSG_PADRAO(Inner innerAtual)
        {
            try
            {
                //Testa o Retorno do comando de Envio de Mensagem Padrão On Line
                if (EasyInner.EnviarMensagemPadraoOnLine(innerAtual.Numero, 1, "Modo Online") == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Muda o passo para configuração de entradas Online.
                    innerAtual.Tentativas = 0;
                    innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_POLLING;
                }
                else
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    if (innerAtual.Tentativas >= 3)
                    {
                        innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    //Adiciona 1 ao contador de tentativas
                    innerAtual.Tentativas++;
                }
            }
            catch (Exception ex)
            {
                innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
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

            if (ret == (byte)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                EasyInner.AcionarBipLongo(InnerAtual.Numero);
                if (InnerAtual.InnerNetAcesso)
                    EasyInner.LigarLedVermelho(InnerAtual.Numero);

                InnerAtual.TempoInicialMensagem = DateTime.Now;
                InnerAtual.Tentativas = 0;
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_AGUARDA_TEMPO_MENSAGEM;
            }
            else
            {
                if (InnerAtual.Tentativas >= 3)
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
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
                if (EasyInner.EnviarMensagemPadraoOnLine(innerAtual.Numero, 0, " DEPOSITE O       CARTAO") == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    innerAtual.Tentativas = 0;
                    innerAtual.TentativasUrna = 0;
                    innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_MONITORA_URNA;
                }
                else
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    if (innerAtual.Tentativas >= 3)
                    {
                        innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    //Adiciona 1 ao contador de tentativas
                    innerAtual.Tentativas++;
                }
            }
            catch (Exception)
            {
                innerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
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
                if (LiberaEntrada)
                {
                    EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "                ENTRADA LIBERADA");
                    LiberaEntrada = false;
                    Ret = EasyInner.LiberarCatracaEntrada(InnerAtual.Numero);
                }
                else
                {
                    if (LiberaEntradaInvertida)
                    {
                        EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "                ENTRADA LIBERADA");
                        LiberaEntradaInvertida = false;
                        Ret = EasyInner.LiberarCatracaEntradaInvertida(InnerAtual.Numero);
                    }
                    else
                    {
                        //Envia comando de liberar a catraca para Saída.
                        if (LiberaSaida)
                        {
                            EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "                 SAIDA LIBERADA");
                            LiberaSaida = false;
                            Ret = EasyInner.LiberarCatracaSaida(InnerAtual.Numero);
                        }
                        else
                        {
                            if (LiberaSaidaInvertida)
                            {
                                EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "                 SAIDA LIBERADA");
                                LiberaSaidaInvertida = false;
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
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    InnerAtual.CountPingFail = 0;
                    InnerAtual.Tentativas = 0;
                    InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_MONITORA_GIRO_CATRACA;
                }
                else
                {
                    //Se o retorno for diferente de 0 tenta liberar a catraca 3 vezes, caso não consiga enviar o comando volta para o passo reconectar.
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.Tentativas = 0;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    //Adiciona 1 ao contador de tentativas
                    InnerAtual.Tentativas++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro:" + ex.Message);
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
            }
        }
        /// <summary>
        /// Verifica se a catraca foi girada ou não e caso sim para qual lado.
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
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Testa se girou o não a catraca..
                    if (Bilhetes.Origem == (int)Enumeradores.Origem.FIM_TEMPO_ACIONAMENTO)
                    {
                        UpdateDisplay.AtulizarLabelDados("Não girou a catraca!");
                    }
                    else if (Bilhetes.Origem == (int)Enumeradores.Origem.GIRO_DA_CATRACA_TOPDATA)
                    {
                        UpdateDisplay.AtulizarLabelDados("Girou a catraca para " + (Bilhetes.Complemento - Convert.ToInt16(InnerAtual.CatInvertida) == 0 ? "entrada." : "saída.").ToString());
                    }
                    UpdateDisplay.FuncoesCatraca(true, true);    

                    //Vai para o estado de Envio de Msg Padrão..
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONFIGURAR_ENTRADAS_ONLINE;
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
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_PING_ONLINE;
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
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Testa se a urna recolheu o cartão
                    if (Bilhetes.Origem == (int)Enumeradores.Origem.URNA)
                    {
                       UpdateDisplay.AtulizarLabelDados("URNA RECOLHEU CARTÃO");
                        
                        //Vai para o estado de Envio de Msg Padrão..
                        LiberaSaida = true;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                    }
                    //Senão depositou o cartão mostra mensagem e bloqueia o acesso
                    else if (Bilhetes.Origem == (int)Enumeradores.Origem.FIM_TEMPO_ACIONAMENTO)
                    {
                        UpdateDisplay.AtulizarLabelDados("NÃO DEPOSITOU CARTÃO");
                        EasyInner.AcionarBipLongo(InnerAtual.Numero);
                        
                        //Vai para o estado de Envio de Msg Padrão..
                        InnerAtual.TempoInicialMensagem = DateTime.Now;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_ACESSO_NEGADO;
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
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_PING_ONLINE;
                    }

                    // Caso não teve retorno da urna
                    if (InnerAtual.TentativasUrna == 3)
                    {
                        UpdateDisplay.AtulizarLabelDados("SEM RETORNO DA URNA");
                        EasyInner.AcionarBipLongo(InnerAtual.Numero); 

                        //Vai para o estado de Envio de Msg Padrão..
                        InnerAtual.TempoInicialMensagem = DateTime.Now;
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_ACESSO_NEGADO;
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
                if (retorno == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    InnerAtual.EstadoAtual = InnerAtual.EstadoSolicitacaoPingOnLine;
                    InnerAtual.TempoInicialPingOnLine = DateTime.Now;
                }
                else
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    //Adiciona 1 ao contador de tentativas
                    InnerAtual.Tentativas++;
                }
            }
            catch (Exception)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
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

                UpdateDisplay.AtualizarEstadoInner(InnerAtual.Numero, InnerAtual.EstadoAtual);

                do
                {
                    Ret = testaConexaoInner(InnerAtual);
                    Thread.Sleep(300);

                } while ((count++ < 10) && (Ret != (int)Enumeradores.Retorno.RET_COMANDO_OK));
                
                if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
                {
                    //Zera as variáveis de controle da maquina de estados.
                    InnerAtual.Tentativas = 0;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECEBER_FIRMWARE;
                }
                else
                {
                    //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                    if (InnerAtual.Tentativas >= 3)
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                    }
                    InnerAtual.Tentativas++;
                }
                InnerAtual.CountRepeatPingOnline = 0;
            }
            catch (Exception ex)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_CONECTAR;
                Console.WriteLine(ex);
            }
        

        }

        private void PASSO_ESTADO_RECEBER_QTD_BILHETES_OFF(Inner InnerAtual)
        {
            int Ret = -1;
            int[] receber = new int[2];
            Ret = EasyInner.ReceberQuantidadeBilhetes(InnerAtual.Numero, receber);
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                InnerAtual.BilhetesAReceber = receber[0];
                if (InnerAtual.BilhetesAReceber > 0)
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_COLETAR_BILHETES;
                }
                else
                {
                    if (InnerAtual.Catraca)
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_OFFLINE_CATRACA;
                    }
                    else
                    {
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_OFFLINE_COLETOR;
                    }
                }
            }
            else
            {
                if (InnerAtual.Tentativas++ > 3)
                {
                    InnerAtual.Tentativas = 0;
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                }
            }
        }

        private void PASSO_ESTADO_VALIDAR_ACESSO(Inner InnerAtual)
        {
            if (LiberarAcesso(InnerAtual.BilheteOnline.Cartao.ToString()) == false)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_ACESSO_NEGADO;

                //Se 1 leitor
                //E Urna ou entrada e saída ou liberada 2 sentidos ou sentido giro
                //E cartão = proximidade
            }
            else if (((InnerAtual.DoisLeitores == false) &&
                        ((InnerAtual.Acionamento == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Urna)
                        || (InnerAtual.Acionamento == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Entrada_E_Saida)
                        || (InnerAtual.Acionamento == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Liberada_2_Sentidos)
                        || (InnerAtual.Acionamento == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Sentido_Giro))
                        && ((InnerAtual.TipoLeitor == 2) || (InnerAtual.TipoLeitor == 3) || (InnerAtual.TipoLeitor == 4))))
            {
                if (InnerAtual.EstadoTeclado == Enumeradores.EstadosTeclado.TECLADO_EM_BRANCO)
                {
                    //Apresenta mensagem para informa se é entrada ou saída
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_DEFINICAO_TECLADO;
                }

                //Se estamos trabalhando com Urna e 1 leitor
                if ((InnerAtual.Catraca) && (InnerAtual.Acionamento == (byte)Enumeradores.Acionamento.Acionamento_Catraca_Urna))
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_MSG_URNA;
                }
            }
            else if (InnerAtual.Acionamento == (byte)Enumeradores.Acionamento.Acionamento_Coletor)
            {
                //aciona Rele
                if (InnerAtual.BilheteOnline.Origem == (int)Enumeradores.Origem.VIA_LEITOR2)
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ACIONAR_RELE2;
                }
                else
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ACIONAR_RELE1;
                }   
            }
            else
            {
                if (InnerAtual.Catraca)
                {
                    //somente entrada
                    if (InnerAtual.Acionamento == (int)Enumeradores.Acionamento.Acionamento_Catraca_Entrada)
                    {
                        HABILITA_LADO_CATRACA("Entrada", InnerAtual.CatInvertida);
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                    }
                    //somente saida
                    else if (InnerAtual.Acionamento == (int)Enumeradores.Acionamento.Acionamento_Catraca_Saida)
                    {
                        HABILITA_LADO_CATRACA("Saida", InnerAtual.CatInvertida);
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                    }

                    //saida liberada
                    else if (InnerAtual.Acionamento == (int)Enumeradores.Acionamento.Acionamento_Catraca_Saida_Liberada)
                    {
                        HABILITA_LADO_CATRACA("Entrada", InnerAtual.CatInvertida);
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                    }

                    //entrada liberada
                    else if (InnerAtual.Acionamento == (int)Enumeradores.Acionamento.Acionamento_Catraca_Entrada_Liberada)
                    {
                        HABILITA_LADO_CATRACA("Saida", InnerAtual.CatInvertida);
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                    }
                    //Se Urna e 2 leitores
                    else if ((InnerAtual.Acionamento == (int)Enumeradores.Acionamento.Acionamento_Catraca_Urna)
                        && (InnerAtual.BilheteOnline.Origem == (int)Enumeradores.Origem.VIA_LEITOR2))
                    {
                        EasyInner.AcionarRele2(InnerAtual.Numero);
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_VALIDA_URNA_CHEIA;
                    }
                    else
                    {
                        EasyInner.EnviarMensagemPadraoOnLine(InnerAtual.Numero, 0, "Acesso Liberado!");
                        InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_LIBERAR_CATRACA;
                    }
                }
            }
        }

        private void PASSO_ESTADO_RECEBER_FIRMWARE(Inner InnerAtual)
        {
            byte Linha = 0;
            short Variacao = 0;            
            byte VersaoBaixa = 0;
            byte VersaoSufixo = 0;
            byte TipoModBio = 0;

            //Solicita a versão do firmware do Inner e dados como o Idioma, se é uma versão especial.  
            int Ret = EasyInner.ReceberVersaoFirmware6xx(InnerAtual.Numero, ref Linha, ref Variacao, ref VersaoAlta, ref VersaoBaixa, ref VersaoSufixo, ref InnerAcessoBio, ref TipoModBio);
            Application.DoEvents();

            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                //Define a linha do Inner
                switch (Linha)
                {
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
                }
                InnerAtual.VariacaoInner = Variacao;
                InnerAtual.VersaoInner = VersaoAlta.ToString() + '.' + VersaoBaixa + '.' + VersaoSufixo;
                InnerAtual.VersaoFW = VersaoAlta;
                InnerAtual.TipoComBio = TipoModBio;
                //Se selecionado Biometria, valida se o equipamento é compatível
                if (InnerAtual.Biometrico)
                {
                    if ((((Linha != 6) && (Linha != 14)) || ((Linha == 14) && (InnerAcessoBio == 0))))
                    {
                        MessageBox.Show("Equipamento " + InnerAtual.Numero + " não compatível com Biometria.", "Atenção");
                    }
                }
                if (InnerAcessoBio == 1 || Linha == 6)
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECEBER_MODELO_BIO;
                }
                else
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_CFG_OFFLINE;
                }
            }
            else
            {
                if (InnerAtual.Tentativas++ >= 3)
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
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
            
            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECEBER_VERSAO_BIO;
            }
            else
            {
                if (Ret == (int)Enumeradores.RetornoBIO.RET_BIO_MODULO_INCORRETO)
                {
                    PararMaquina();
                    MessageBox.Show("Módulo incorreto!", "Online");
                }
                else if (InnerAtual.Tentativas++ > 5)
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
                }
            }
        }
        
        private void PASSO_ESTADO_RECEBER_VERSAO_BIO(Inner InnerAtual)
        {
            int Ret = -1;
            if (InnerAtual.VersaoFW < 6)
            {
                Ret = ReceberVersaoBioAVer5(InnerAtual);
            }
            else
            {
                Ret = ReceberVersaoBio6xx(InnerAtual);
            }

            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_CFG_OFFLINE;
            }
            else
            {
                if (Ret == (int)Enumeradores.RetornoBIO.RET_BIO_MODULO_INCORRETO)
                {
                    PararMaquina();
                    MessageBox.Show("Módulo incorreto!", "Online");
                }
                else if (InnerAtual.Tentativas++ > 5)
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
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

            if (Ret == (int)Enumeradores.Retorno.RET_COMANDO_OK)
            {
                InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_ENVIAR_DATA_HORA;
            }
            else
            {
                //caso ele não consiga, tentará enviar três vezes, se não conseguir volta para o passo Reconectar
                if (InnerAtual.Tentativas++ >= 3)
                {
                    InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_RECONECTAR;
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
            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_AGUARDA_TEMPO_MENSAGEM;
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
            InnerAtual.EstadoAtual = Enumeradores.EstadosInner.ESTADO_AGUARDA_TEMPO_MENSAGEM;
        }

        #endregion

        #endregion
    }
}
