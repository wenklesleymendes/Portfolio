using EasyInnerSDK;
using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Core.Services;
using EMCatraca.Server.Excecoes;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using TcpIp;
using static EMCatraca.TopData.EnumeradoresDllInner;

namespace EMCatraca.TopData
{
    public abstract class CatracaTopDataAbstract
    {
        protected Dispositivo Dispositivo { get; private set; }
        protected RegrasAcesso ConfiguracaoAcesso;
        protected abstract Pessoa ObtenhaPessoa(string codigo);
        protected abstract void RegistreAcessoPessoa(Pessoa pessoa, SentidoGiro sentidoDoGiro);
        protected InformacaoConexao ConfiguracaoServidor;

        private TcpIpServidor ServidorMonitor = new TcpIp.TcpIpServidor();
        private Thread ThreadMonitoramento { get; set; }
        private EstadoEquipamento _estadoEquipamento { get; set; }
        private Retorno _retornoComando { get; set; }
        private IServicoMonitorAcesso ServicoMonitorAcesso { get; set; }

        private Pessoa _pessoa = null;
        private SentidoGiro _sentidoDoGiro = SentidoGiro.Indefinido;
        private TipoAcesso _tipoAcesso = TipoAcesso.Indefinido;
        private DateTime _tPingCatraca;
        private Firmware _firmware;
        private const int _qtdDigitoCartao = 10;

        private string _indetificacaoDaPessoa;

        public CatracaTopDataAbstract(Dispositivo catraca, IServicoMonitorAcesso servicoMonitorAcesso)
        {
            ConfiguracaoAcesso = MapeadorArquivoJson.CarreguerArquivoJson<RegrasAcesso>("emcatraca.acesso.cfg");

            ServicoMonitorAcesso = servicoMonitorAcesso;
            Dispositivo = catraca;
            _estadoEquipamento = EstadoEquipamento.Conectando;

            //Deve ser iniciado apenas uma vez!
            if (catraca.Codigo == 1)
            {
                InicieConexao();
                ConfiguracaoServidor = MapeadorArquivoJson.CarreguerArquivoJson<InformacaoConexao>("emcatraca.servidor.cfg");

                var tipoServidor = "Servidor do Monitor de Acesso";
                ServidorMonitor.IniciarServidorTcpIp(ConfiguracaoServidor.IP, ConfiguracaoServidor.PortaTcpIp, tipoServidor);
            }
            //*********************************

            InicieThread();
        }

        private void InicieConexao()
        {
            var retornoTipoDefinicao = EasyInner.DefinirTipoConexao((byte)TipoConexao.TcpIpComPortaFixa);
            RegistraLogRetornoComando(retornoTipoDefinicao, nameof(EasyInner.DefinirTipoConexao));

            EasyInner.FecharPortaComunicacao();
            var tentativas = 0;

            var retornoAbrirPorta = EasyInner.AbrirPortaComunicacao(Convert.ToInt32(Dispositivo.PortaCatraca));
            RegistraLogRetornoComando(retornoAbrirPorta, nameof(EasyInner.AbrirPortaComunicacao));

            while (!Retorno.RetornoComandoOk.Equals(_retornoComando))
            {
                if (tentativas > 25)
                {
                    AuditoriaLog.EscrevaErro($"Erro Abrir porta:{Dispositivo.PortaCatraca} tentativa{tentativas}",
                        new ApplicationException());

                    return;
                }

                Thread.Sleep(100);
                tentativas++;
            }
        }

        private void RegistraLogRetornoComando(int valorRetono, string comando)
        {
            switch (valorRetono)
            {
                case 0:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Ok", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoComandoOk;
                    break;

                case 1:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Erro",nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoComandoErro;
                    break;

                case 2:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Retorno Porta Nao Aberta", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoPortaNaoAberta;
                    break;

                case 3:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Retorno Porta Ja Aberta", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoPortaJaAberta;
                    break;

                case 4:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Retorno Dll Inner2K Nao Encontrada", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoDllInner2KNaoEncontrada;
                    break;

                case 5:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Retorno Dll Inner TCP Nao Encontrada", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoDllInnerTCPNaoEncontrada;
                    break;

                case 6:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Retorno Dll Inner TCP2 Nao Encontrada", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoDllInnerTCP2NaoEncontrada;
                    break;

                case 8:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Retorno Erro GPF", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoErroGpf;
                    break;

                case 9:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Retorno Tipo Conexao Invalida", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoTipoConexaoInvalida;
                    break;

                case 128:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Retorno Bio Processando", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoBioProcessando;
                    break;

                case 129:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Retorno Bio Falha Comunicacao", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoBioFalhaComunicacao;
                    break;

                case 131:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Retorno Bio Usr Ja Cadastrado", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoBioUsrJaCadastrado;
                    break;

                case 132:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Retorno Bio Base Cheia", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoBioBaseCheia;
                    break;

                case 133:
                    LogAuditoria.Escreva($"Retorno comando {comando}:Retorno Bio DIG Nao Confere", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoBioDIGNaoConfere;
                    break;

                case 134:
                    LogAuditoria.Escreva($"Retorno comando {comando}:Retorno Bio DIG Nao Confere", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoBioDIGNaoConfere;
                    break;

                case 135:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Retorno Bio Invalida", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoBioInvalida;
                    break;

                case 136:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Retorno Bio Templeante Invalido", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoBioTempleanteInvalido;
                    break;

                case 137:
                    LogAuditoria.Escreva($"Retorno comando {comando}: Retorno Bio Parametro Invalido", nameof(CatracaTopDataAbstract));
                    _retornoComando = Retorno.RetornoBioParametroInvalido;
                    break;

                default:
                    break;
            }
        }

        private void InicieThread()
        {
            ThreadMonitoramento = new Thread(() =>
            {
                LogAuditoria.Escreva($"Iniciando a thread de monitoramento", nameof(CatracaTopDataAbstract));

                try
                {
                    Conecte();

                    _firmware = ReceberFirmware();

                    EnviarCFGOffLine();
                    EnviarCFGOnLinne();
                    Conectado();
                }
                catch (ThreadAbortException ex )
                {
                    AuditoriaLog.EscrevaErro(nameof(CatracaTopDataAbstract),ex);
                }
                catch (Exception execaoErro)
                {
                    AuditoriaLog.EscrevaErro(nameof(CatracaTopDataAbstract), execaoErro);
                }
            });

            ThreadMonitoramento.IsBackground = true;
            ThreadMonitoramento.Start();
        }

        private void Conecte()
        {
            LogAuditoria.Escreva($"Iniciando a Conexao.", nameof(CatracaTopDataAbstract));

            _estadoEquipamento = EstadoEquipamento.Conectando;
            var contadorControladorDeLog = 0;

            while (true)
            {
                ServicoMonitorAcesso.AdicioneEvento(EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, "Off-line"));
                ServidorMonitor.EnviarMensagem(JsonConvert.SerializeObject(EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, "Off-line")));

                try
                {
                    if (TestaConexaoCatraca() == 0)
                    {
                        _estadoEquipamento = EstadoEquipamento.Conectado;

                        ServicoMonitorAcesso.AdicioneEvento(EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, ""));
                        ServidorMonitor.EnviarMensagem(JsonConvert.SerializeObject(EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, "")));

                        break;
                    }
                    else
                    {
                        contadorControladorDeLog++;

                        if (contadorControladorDeLog >= 100)
                        {
                            LogAuditoria.Escreva($"Falha na Conexão Reiniciando o Processo Tentativa:{contadorControladorDeLog}", nameof(CatracaTopDataAbstract));
                        }
                    }

                    Thread.Sleep(300);
                }
                catch (Exception ex)
                {
                    AuditoriaLog.EscrevaErro(nameof(CatracaTopDataAbstract), ex);
                }
            }
        }

        private void Conectado()
        {
            byte Origem = 0;
            byte Complemento = 0;
            var Cartao = new StringBuilder();
            byte Dia = 0;
            byte Mes = 0;
            byte Ano = 0;
            byte Hora = 0;
            byte Minuto = 0;
            byte Segundo = 0;
            int pingFalhou = 0;

            _tPingCatraca = DateTime.Now;
            string dadosLidos;
            var conexaoAtiva = true;

            LogAuditoria.Escreva($"{_estadoEquipamento} com sucesso", nameof(CatracaTopDataAbstract));

            while (true)
            {
                var retornoReceberDadosOnline = EasyInner.ReceberDadosOnLine(Dispositivo.Codigo,
                                                                             ref Origem,
                                                                             ref Complemento, Cartao,
                                                                             ref Dia,
                                                                             ref Mes,
                                                                             ref Ano,
                                                                             ref Hora,
                                                                             ref Minuto,
                                                                             ref Segundo);
                if (retornoReceberDadosOnline == 0)
                {
                    dadosLidos = Origem == 12
                                   ? ObtenhaMatriculaRecebidaDaBiometria(Cartao.ToString(), Origem)
                                   : Cartao.ToString();

                    if (Origem == 1)
                    {
                        _tipoAcesso = TipoAcesso.Teclado;
                    }

                    var ehCartaoOuBiometria = Origem == 2 || Origem == 12;
                    if (ehCartaoOuBiometria && dadosLidos.Length > 0)
                    {
                        _estadoEquipamento = EstadoEquipamento.ValidandoAcesso;
                    }

                    if (Origem == 6)
                    {
                        _estadoEquipamento = EstadoEquipamento.GirouCatraca;
                    }

                    if (Origem == 5)
                    {
                        _estadoEquipamento = EstadoEquipamento.ExcedeuTempoDeGiro;
                    }

                    if (Origem == 6)
                    {
                        _sentidoDoGiro = (SentidoGiro)(Complemento + 1);
                    }

                    LogAuditoria.Escreva($"Estado: {_estadoEquipamento}, Origem: {Origem}, " +
                                          $"Complemento: {Complemento}, Cartao: {dadosLidos}",
                                          nameof(CatracaTopDataAbstract));

                    if (_estadoEquipamento == GetValidandoAcesso())
                    {
                        ValideAcesso(dadosLidos);
                    }

                    if (_estadoEquipamento == EstadoEquipamento.GirouCatraca)
                    {
                        RegisteAcesso(_sentidoDoGiro);

                        ConfiguraFormasDeEntradaOnline();
                        _estadoEquipamento = EstadoEquipamento.Conectado;
                    }

                    if (_estadoEquipamento == EstadoEquipamento.ExcedeuTempoDeGiro)
                    {
                        LogAuditoria.Escreva($"Excedeu o tempo para giro", nameof(CatracaTopDataAbstract));

                        ConfiguraFormasDeEntradaOnline();
                        _estadoEquipamento = EstadoEquipamento.Conectado;
                    }
                }

                if (((TimeSpan)DateTime.Now.Subtract(_tPingCatraca)).TotalSeconds >= 3)
                {
                    Thread.Sleep(100);

                    if (EasyInner.PingOnLine(Dispositivo.Codigo) != 0)
                    {
                        pingFalhou++;
                        if (pingFalhou >= 3)
                        {
                            conexaoAtiva = false;
                            LogAuditoria.Escreva($"Falha de conexão com a catraca, verifique se a catraca " +
                                $"está ligada e se está conectada em rede.", nameof(CatracaTopDataAbstract));
                            pingFalhou = 0;
                            _firmware = ReceberFirmware();
                        }
                        Thread.Sleep(100);
                    }
                    else
                    {
                        pingFalhou = 0;
                        if (!conexaoAtiva)
                        {
                            LogAuditoria.Escreva($"Conexão reestabelecida com a catraca {Dispositivo.Codigo}", nameof(CatracaTopDataAbstract));
                            conexaoAtiva = true;
                        }
                    }
                    _tPingCatraca = DateTime.Now;
                }
            }
        }

        private string ObtenhaMatriculaRecebidaDaBiometria(string cartao, byte origem)
        {
            return origem == 12
                    ? ObtenhaCartaoBio(cartao)
                    : string.Empty;
        }

        private string ObtenhaCartaoBio(string cartao)
        {
            cartao = Utils.ReturnNumeros(cartao);
            return Utils.RemZeroEsquerda(cartao);
        }

        private void EnviarCFGOffLine()
        {
            LogAuditoria.Escreva($"{_estadoEquipamento} Enviar Configuração Offline", nameof(CatracaTopDataAbstract));

            //Configuração Offline
            EasyInner.DefinirMensagemPadraoOffLine(1, $"CFG OFFLINE.");

            MonteCFGOffLineEhOnLine();

            EasyInner.ConfigurarInnerOffLine();

            var tentativas = 0;
            while (EasyInner.EnviarConfiguracoes(Dispositivo.Codigo) != 0)
            {
                if (tentativas > 5)
                {
                    LogAuditoria.Escreva($"Falha em EasyInner.EnviarConfiguracoes", nameof(CatracaTopDataAbstract));
                    return;
                }

                Thread.Sleep(100);
                tentativas++;
            }

            EasyInner.DefinirTipoListaAcesso(0);

            int[] receber = new int[2];
            tentativas = 0;

            while (EasyInner.ReceberQuantidadeBilhetes(Dispositivo.Codigo, receber) != 0)
            {
                if (tentativas > 5)
                {
                    LogAuditoria.Escreva($"Falha em EasyInner.ReceberQuantidadeBilhetes", nameof(CatracaTopDataAbstract));

                    return;
                }

                Thread.Sleep(100);
                tentativas++;
            }

            EasyInner.DefinirMensagemEntradaOffLine(1, "ENTRADA LIBERADA.");
            EasyInner.DefinirMensagemSaidaOffLine(1, "SAIDA LIBERADA.");
            EasyInner.DefinirMensagemPadraoOffLine(1, "    OFF LINE    ");
            tentativas = 0;

            while (EasyInner.EnviarMensagensOffLine(Dispositivo.Codigo) != 0)
            {
                if (tentativas > 5)
                {
                    LogAuditoria.Escreva($"Falha em EasyInner.EnviarMensagensOffLine", nameof(CatracaTopDataAbstract));

                    return;
                }
                Thread.Sleep(100);
                tentativas++;
            }

            tentativas = 0;
            while (EasyInner.EnviarRelogio(Dispositivo.Codigo,
               (byte)DateTime.Now.Day,
               (byte)DateTime.Now.Month,
               Convert.ToByte(DateTime.Now.Year.ToString().Substring(2, 2)),
               (byte)DateTime.Now.Hour,
               (byte)DateTime.Now.Minute,
               (byte)DateTime.Now.Second) != 0)
            {
                if (tentativas > 5)
                {
                    LogAuditoria.Escreva("Falha em EasyInner.EnviarRelogio", nameof(CatracaTopDataAbstract));
                   
                    return;
                }
                Thread.Sleep(100);
                tentativas++;
            }
        }

        protected virtual void MonteCFGOffLineEhOnLine()
        {
            LogAuditoria.Escreva("Enviar Configuração Online", nameof(CatracaTopDataAbstract));

            //Configuração Online
            EasyInner.DefinirPadraoCartao((byte)PadraoCartao.PadraoLivre);

            var tempoAcionamento1 = (byte)5;
            EasyInner.ConfigurarAcionamento1((byte)FuncaoAcionamento.AcionaRegistroEntradaOuSaida, tempoAcionamento1);

            var tempoAcionamento2 = (byte)0;
            EasyInner.ConfigurarAcionamento2((byte)FuncaoAcionamento.NaoUtilizado, tempoAcionamento2);

            EasyInner.ConfigurarLeitor1((byte)Operacao.EntradaEhSaida);
            EasyInner.ConfigurarLeitor2((byte)Operacao.Desativado);
            EasyInner.ConfigurarTipoLeitor((byte)TipoLeitor.CodigoDeBarras);
            EasyInner.DefinirQuantidadeDigitosCartao((byte)_qtdDigitoCartao);
            EasyInner.HabilitarTeclado((byte)Opcao.Sim, (byte)Opcao.Nao);
            EasyInner.RegistrarAcessoNegado((byte)Opcao.Nao);
            EasyInner.DefinirFuncaoDefaultLeitoresProximidade(12);
            EasyInner.DefinirFuncaoDefaultSensorBiometria(0);
            EasyInner.SetarBioVariavel(1);
            EasyInner.ConfigurarBioVariavel(1);
            EasyInner.ReceberDataHoraDadosOnLine((byte)(Opcao.Sim));
        }

        private void EnviarCFGOnLinne()
        {
            LogAuditoria.Escreva("Iniciar Envio da Configuração Online",
                nameof(CatracaTopDataAbstract));

            //Configuração Online
            MonteCFGOffLineEhOnLine();
            EasyInner.ConfigurarInnerOnLine();

            var tentativas = 0;

            while (EasyInner.EnviarConfiguracoes(Dispositivo.Codigo) != 0)
            {
                if (tentativas > 5)
                {
                    LogAuditoria.Escreva($"Falha em EasyInner.EnviarConfiguracoes", 
                        nameof(CatracaTopDataAbstract));
             
                    return;
                }

                Thread.Sleep(100);
                tentativas++;
            }

            AjustaMundancaOnLine();
            ConfiguraFormasDeEntradaOnline();
        }

        protected virtual void AjustaMundancaOnLine()
        {
            EasyInner.HabilitarMudancaOnLineOffLine(1, 10);
            EasyInner.DefinirConfiguracaoTecladoOnLine(10, 0, 5, 17);
            EasyInner.DefinirEntradasMudancaOnLine((byte)Convert.ToInt32($"{(int)FormaDeEntradasComBiometria.Byte112}", 2));
            EasyInner.DefinirEntradasMudancaOffLine((byte)Opcao.Nao, 3, (byte)Opcao.Nao, (byte)Opcao.Nao);

            EasyInner.DefinirMensagemPadraoMudancaOffLine(1, "Passe o cartão.");
            EasyInner.DefinirMensagemPadraoMudancaOnLine(1, "Passe o cartão!");

            var tentativas = 0;
            while (EasyInner.EnviarConfiguracoesMudancaAutomaticaOnLineOffLine(Dispositivo.Codigo) != 0)
            {
                if (tentativas > 5)
                {
                    LogAuditoria.Escreva("Falha em EasyInner.EnviarConfiguracoesMudancaAutomaticaOnLineOffLine", 
                        nameof(CatracaTopDataAbstract));

                    return;
                }

                Thread.Sleep(100);
                tentativas++;
            }
        }

        private int TestaConexaoCatraca()
        {
            byte Dia = 0;
            byte Mes = 0;
            byte Ano = 0;
            byte Hora = 0;
            byte Minuto = 0;
            byte Segundo = 0;

            var retornReceberRelogio = EasyInner.ReceberRelogio(Dispositivo.Codigo,
                                                                         ref Dia,
                                                                         ref Mes,
                                                                         ref Ano,
                                                                         ref Hora,
                                                                         ref Minuto,
                                                                         ref Segundo);

            return retornReceberRelogio;
        }

        private Firmware ReceberFirmware()
        {
            LogAuditoria.Escreva($"{_estadoEquipamento} Receber Versao Firmware6", 
                nameof(CatracaTopDataAbstract));

            var firmware = new Firmware() { Linha = 0 };
            var linha = firmware.Linha;
            var variacao = firmware.Variacao;
            var versaoAlta = firmware.VersaoAlta;
            var versaoBaixa = firmware.VersaoBaixa;
            var versaoSufixo = firmware.VersaoSufixo;
            var innerAcessoBio = firmware.InnerAcessoBio;
            var tipoModBio = firmware.TipoModBio;

            var tentativas = 0;
            var retornoVesaoFirmware = EasyInner.ReceberVersaoFirmware6xx(Dispositivo.Codigo,
                                                                           ref linha,
                                                                           ref variacao,
                                                                           ref versaoAlta,
                                                                           ref versaoBaixa,
                                                                           ref versaoSufixo,
                                                                           ref innerAcessoBio,
                                                                           ref tipoModBio);

            RegistraLogRetornoComando(retornoVesaoFirmware, nameof(EasyInner.ReceberVersaoFirmware6xx));

            while (!Retorno.RetornoComandoOk.Equals(_retornoComando))
            {
                if (tentativas > 5)
                {
                    LogAuditoria.Escreva($"Falha em EasyInner.ReceberVersaoFirmware6",nameof(CatracaTopDataAbstract));
           
                    return null;
                }

                Thread.Sleep(100);
                tentativas++;
            }

            firmware.Linha = linha;
            firmware.Variacao = variacao;
            firmware.VersaoAlta = versaoAlta;
            firmware.VersaoBaixa = versaoBaixa;
            firmware.VersaoSufixo = versaoSufixo;
            firmware.InnerAcessoBio = innerAcessoBio;
            firmware.TipoModBio = tipoModBio;

            LogAuditoria.Escreva($"Versao Firmware " +
                                  $"Linha:{firmware.Linha}" +
                                  $"|Variacao:{firmware.Variacao}" +
                                  $"|Versao:{ firmware.VersaoAlta}.{firmware.VersaoBaixa}.{firmware.VersaoSufixo}" +
                                  $"|Inner Acesso Bio:{firmware.InnerAcessoBio}" +
                                  $"|Inner Tipo ModBio:{firmware.TipoModBio}", 
                                  nameof(CatracaTopDataAbstract));

            return firmware;
        }

        private static EstadoEquipamento GetValidandoAcesso()
        {
            return EstadoEquipamento.ValidandoAcesso;
        }

        protected virtual void ConfiguraFormasDeEntradaOnline()
        {
            LogAuditoria.Escreva($"{_estadoEquipamento} Enviar Formas Entrada OnLine", 
                nameof(CatracaTopDataAbstract));

            var tentativas = 0;
            var echoDoTecladoNoDisplay = 1;
            var tempoDeEsperaDoTeclado = 15;
            var posicaoDoCursorNoTeclado = 17;

            var retornoEnviarFormasEntradasOnLine = EasyInner.EnviarFormasEntradasOnLine(Dispositivo.Codigo,
                                                                                         (byte)_qtdDigitoCartao,
                                                                                         (byte)echoDoTecladoNoDisplay,
                                                                                         (byte)Convert.ToInt32($"{(int)FormaDeEntradasComBiometria.Byte112}", 2),
                                                                                         (byte)tempoDeEsperaDoTeclado,
                                                                                         (byte)posicaoDoCursorNoTeclado);

            while (retornoEnviarFormasEntradasOnLine != 0)
            {
                if (tentativas > 5)
                {
                    LogAuditoria.Escreva("Falha em EasyInner.EnviarFormasEntradasOnLine", 
                        nameof(CatracaTopDataAbstract));

                    return;
                }

                Thread.Sleep(100);
                tentativas++;
            }

            EasyInner.EnviarMensagemPadraoOnLine(Dispositivo.Codigo, 1, "Passe o cartão!");
        }

        private void ConfiguraCatracaAcessoNegado()
        {
            if (_pessoa == null)
            {
                EasyInner.EnviarMensagemPadraoOnLine(Dispositivo.Codigo, 0, $"{(" Nao encontrada ").Substring(0, 16)} Acesso Negado! ");
            }
            else
            {
                EasyInner.EnviarMensagemPadraoOnLine(Dispositivo.Codigo, 0, $"{(_pessoa.Nome + new String(' ', 16)).Substring(0, 16)} Acesso Negado! ");
            }

            EasyInner.AcionarBipLongo(Dispositivo.Codigo);
            EasyInner.LigarLedVermelho(Dispositivo.Codigo);
            Thread.Sleep(TimeSpan.FromSeconds(3));
            EasyInner.DesligarLedVermelho(Dispositivo.Codigo);

            ConfiguraFormasDeEntradaOnline();
        }

        protected virtual void ValideTemAcessoPorTeclado(Pessoa pessoa, TipoAcesso tipoAcesso) { }
        protected virtual void ValidePessoaEstaAtiva(Pessoa pessoa) { }
        protected virtual void ValideTempoMinimoParaNovoAcesso(Pessoa pessoa) { }

        protected virtual bool ValideAlunoPossuiLiberacao(Aluno aluno) { return false; }
        protected virtual void ValideAlunoPossuiBloqueio(Aluno aluno) { }
        protected virtual void ValideAlunoPossuiInadimplencia(Aluno aluno) { }
        protected virtual void ValideAlunoFaltaDocumentos(Aluno aluno) { }
        protected virtual void ValideAlunoFaltaMateriais(Aluno aluno) { }
        protected virtual void ValideAlunoPodeSairSozinho(Aluno aluno) { }
        protected virtual void ValideAlunoPossuiOcorrencia(Aluno aluno) { }

        protected virtual void ValideAutorizadoPossuiInadimplencia(AutorizadoBuscarAluno autorizadoBuscarAluno) { }
        protected virtual void ValideAutorizadoFaltaDocumentos(AutorizadoBuscarAluno autorizadoBuscarAluno) { }
        protected virtual void ValideAutorizadoFaltaMateriais(AutorizadoBuscarAluno autorizadoBuscarAluno) { }
        protected virtual void ValideAutorizadoPossuiOcorrencia(AutorizadoBuscarAluno autorizadoBuscarAluno) { }

        protected virtual void ValideResponsavelPossuiBloqueio(Responsavel responsavel) { }
        protected virtual void ValideResponsavelPossuiInadimplencia(Responsavel responsavel) { }
        protected virtual void ValideResponsavelFaltaDocumentos(Responsavel responsavel) { }
        protected virtual void ValideResponsavelFaltaMateriais(Responsavel responsavel) { }
        protected virtual void ValideResponsavelPossuiOcorrencia(Responsavel responsavel) { }

        protected virtual void ValideProfessorPossuiOcorrencia(Professor professor) { }
        protected virtual void ValideColaboradorPossuiOcorrencias(Colaborador colaborador) { }

        protected virtual void ValideDentroDoHorarioDeAcesso(Pessoa pessoa) { }

        protected virtual string MonteMensagemRestricao(Pessoa pessoa) => null;

        private void ValideAcesso(string codigoRecebido)
        {
            LogAuditoria.Escreva($"Iniciando validação de acesso código: {codigoRecebido}",
                nameof(CatracaTopDataAbstract));
            _pessoa = null;

            try
            {
                LogAuditoria.Escreva($"Iniciando a identificação da pessoa.",
                    nameof(ControladorDeCatracaTopData));

                _pessoa = ObtenhaPessoa(codigoRecebido);
                _indetificacaoDaPessoa = $"Pessoa:{_pessoa.RecuperaTipo()} Código:{_pessoa.Id} Nome:{_pessoa.Nome}";

                LogAuditoria.Escreva($"Foi indetificada {_indetificacaoDaPessoa}", 
                    nameof(CatracaTopDataAbstract));
            }
            catch
            {
                var pessoa = new Aluno
                {
                    Nome = "Não encontrado!"
                };

                var evento = EventoCatraca.CrieAcessoNegado(SentidoGiro.Indefinido, pessoa, Dispositivo);

                ServicoMonitorAcesso.AdicioneEvento(evento);
                ServidorMonitor.EnviarMensagem(JsonConvert.SerializeObject(evento));

                ConfiguraCatracaAcessoNegado();

                LogAuditoria.Escreva($"Acesso Negado, Motivo:{pessoa.Nome}!", 
                    nameof(CatracaTopDataAbstract));
                return;
            }

            try
            {
                ConfiguracaoAcesso = MapeadorArquivoJson.CarreguerArquivoJson<RegrasAcesso>("emcatraca.acesso.cfg");

                ValidePessoaEstaAtiva(_pessoa);
                ValideTemAcessoPorTeclado(_pessoa, _tipoAcesso);

                if (_pessoa is Aluno aluno)
                {
                    ValideAlunoPossuiOcorrencia(aluno);
                    ValideAlunoPossuiInadimplencia(aluno);
                    ValideAlunoFaltaDocumentos(aluno);
                    ValideAlunoFaltaMateriais(aluno);
                    if (!ValideAlunoPossuiLiberacao(aluno))
                    {
                        ValideAlunoPossuiBloqueio(aluno);
                        ValideAlunoPodeSairSozinho(aluno);
                        ValideDentroDoHorarioDeAcesso(aluno);
                        ValideTempoMinimoParaNovoAcesso(aluno);
                    }
                }

                if (_pessoa is AutorizadoBuscarAluno autorizadoBuscarAluno)
                {
                    ValideAutorizadoPossuiInadimplencia(autorizadoBuscarAluno);
                    ValideAutorizadoFaltaDocumentos(autorizadoBuscarAluno);
                    ValideAutorizadoFaltaMateriais(autorizadoBuscarAluno);
                }

                if (_pessoa is Responsavel responsavel)
                {
                    ValideResponsavelPossuiBloqueio(responsavel);
                    ValideResponsavelPossuiInadimplencia(responsavel);
                    ValideResponsavelFaltaDocumentos(responsavel);
                    ValideResponsavelFaltaMateriais(responsavel);
                }

                if (_pessoa is Professor professor)
                {
                    ValideProfessorPossuiOcorrencia(professor);
                }

                if (_pessoa is Colaborador colaborador)
                {
                    ValideColaboradorPossuiOcorrencias(colaborador);
                }

                EventoCatraca evento;

                var msgRestricao = MonteMensagemRestricao(_pessoa);

                if (!string.IsNullOrEmpty(msgRestricao))
                {
                    evento = EventoCatraca.CrieAcessoLiberadoComRestricao(SentidoGiro.Indefinido, _pessoa, Dispositivo, msgRestricao);
                }
                else
                {
                    evento = EventoCatraca.CrieAcessoLiberado(SentidoGiro.Indefinido, _pessoa, Dispositivo);
                }

                ServicoMonitorAcesso.AdicioneEvento(evento);
                ServidorMonitor.EnviarMensagem(JsonConvert.SerializeObject(evento));

                EasyInner.EnviarMensagemPadraoOnLine(Dispositivo.Codigo, 0, $"{(_pessoa.Nome + new String(' ', 16)).Substring(0, 16)}Acesso Liberado!");
                EasyInner.AcionarBipCurto(Dispositivo.Codigo);
                EasyInner.LiberarCatracaDoisSentidos(Dispositivo.Codigo);

                _estadoEquipamento = EstadoEquipamento.AguardandoGiro;

                LogAuditoria.Escreva("Acesso Liberado.", 
                    nameof(CatracaTopDataAbstract));
            }
            catch (AcessoNegadoException ex)
            {
                var evento = EventoCatraca.CrieAcessoNegado(SentidoGiro.Indefinido, _pessoa, Dispositivo);
                evento.Mensagem2 = ex.Message;

                ServicoMonitorAcesso.AdicioneEvento(evento);
                ServidorMonitor.EnviarMensagem(JsonConvert.SerializeObject(evento));

                ConfiguraCatracaAcessoNegado();

                AuditoriaLog.EscrevaErro(nameof(CatracaTopDataAbstract), ex);

                _pessoa = null;
            }
        }

        private void RegisteAcesso(SentidoGiro sentidoGiro)
        {
            LogAuditoria.Escreva($"Registrando o Acesso", nameof(CatracaTopDataAbstract));

            try
            {
                if (_pessoa != null)
                {
                    if (Dispositivo.EhGiroInvertido && _sentidoDoGiro == SentidoGiro.Entrada)
                    {
                        _sentidoDoGiro = SentidoGiro.Saida;
                    }
                    else if (Dispositivo.EhGiroInvertido && _sentidoDoGiro == SentidoGiro.Saida)
                    {
                        _sentidoDoGiro = SentidoGiro.Entrada;
                    }

                    RegistreAcessoPessoa(_pessoa, _sentidoDoGiro);
                    LogAuditoria.Escreva($"{_indetificacaoDaPessoa} Sentido do Giro da Dispositivo:{_sentidoDoGiro}", 
                        nameof(CatracaTopDataAbstract));
                }

                _pessoa = null;
            }
            catch (Exception ex)
            {
                AuditoriaLog.EscrevaErro(nameof(CatracaTopDataAbstract),ex);
            }
        }

        public void PareCatraca()
        {
            LogAuditoria.Escreva("Dispositivo: {Dispositivo.Codigo}-Parando", 
                nameof(CatracaTopDataAbstract));

            ThreadMonitoramento.Abort();
        }
    }
}
