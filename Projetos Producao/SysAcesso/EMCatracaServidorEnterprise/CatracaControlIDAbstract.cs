using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Services;
using EMCatraca.Henry;
using EMCatraca.Server.Excecoes;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TcpIp;
using static EMCatraca.Control.ID.EstadoCatracaControlID;

namespace EMCatraca.Control.ID
{
    public abstract class CatracaControlIDAbstract
    {
        protected DispositivoCatraca Catraca { get; private set; }
        protected LogCatraca Log { get; set; }
        protected RegrasDeAcesso RegrasAcesso;

        private TcpIpServidor _conexaoTcpServidor = new TcpIpServidor();
        private Pessoa _pessoa = null;
        private InformacaoConexao ConexaoServidor;
        private TcpClient _conexaoTcpCliente { get; set; }
        private Thread _threadDeMonitoramento { get; set; }
        private Thread _threadPing { get; set; }

        private EnumStatusCatracaControlID _statusCatraca { get; set; }
        private EnumSentidoGiro _sentidoDoGiro = EnumSentidoGiro.Indefinido;
        private EnumTipoAcesso _tipoAcesso = EnumTipoAcesso.Indefinido;

        private IServicoMonitorAcesso Monitor { get; set; }

        private string _indetificacaoDaPessoa;
        private string _sessao = null;

        // Entender para isso e como funciona
        protected abstract Pessoa ObtenhaPessoa(string codigo);       
        protected abstract void RegistreAcessoPessoa(Pessoa pessoa, EnumSentidoGiro sentidoDoGiro);

        public CatracaControlIDAbstract(DispositivoCatraca dispositivo, IServicoMonitorAcesso servicoMonitorAcesso)
        {
            Log = new LogCatraca(dispositivo);
            CarregueInformacoesCFG();
            Catraca = dispositivo;
            Monitor = servicoMonitorAcesso;

            // Para garantir que vai ser instanciado única vez na tread
            if (Catraca.Codigo == 1)
            {
                var tipoServidor = "Servidor do Monitor de Acesso";
                _conexaoTcpServidor.IniciarServidorTcpIp(ConexaoServidor.IP, ConexaoServidor.PortaTcpIp, tipoServidor);
            }

            InicieMonitoramento();
        }

        private void CarregueInformacoesCFG()
        {
            RegrasAcesso = MapeadorArquivoJson.CarreguerArquivoJson<RegrasDeAcesso>("emcatraca.acesso.cfg");
            ConexaoServidor = MapeadorArquivoJson.CarreguerArquivoJson<InformacaoConexao>("emcatraca.servidor.cfg");
        }

        private void InicieMonitoramento()
        {
            IniciarTread();
            _threadDeMonitoramento.IsBackground = true;
            _threadDeMonitoramento.Start();
        }

        private void IniciarTread()
        {
            _threadDeMonitoramento = new Thread(() =>
            {
                Log.WriteLog($"Iniciando tread de monitoramento\n");

                while (true)
                {
                    try
                    {
                        Autentique();

                        var clienteSet = ObtenhaRestClient("set_configuration.fcgi?session=");
                        ConfigureEmModoOnline(clienteSet);
                        ConfigureIndetificacaoLocal(clienteSet);

                        var messagen = $"{DateTime.Now}- Sessão autenticado e catraca configurada";
                        EnviaMensagemEmBytes(messagen);

                        ConexaoAutenticada();
                    }
                    catch (ThreadAbortException trex)
                    {
                        Monitor.AdicioneEvento(EventoCatraca.CrieAcessoMudancaEstado(Catraca, "Off-line"));
                        _conexaoTcpServidor.EnviarMensagem(JsonConvert.SerializeObject(EventoCatraca.CrieAcessoMudancaEstado(Catraca, "Off-line")));

                        Log.WriteLogError($"A thread de monitoramento foi abortada", trex);
                        _conexaoTcpCliente.Close();
                    }
                    catch (Exception trex)
                    {
                        Monitor.AdicioneEvento(EventoCatraca.CrieAcessoMudancaEstado(Catraca, "Off-line"));
                        _conexaoTcpServidor.EnviarMensagem(JsonConvert.SerializeObject(EventoCatraca.CrieAcessoMudancaEstado(Catraca, "Off-line")));

                        Log.WriteLogError($"Ocorreu uma falha na tread de monitoramento", trex);
                        _conexaoTcpCliente.Close();
                    }
                }
            });
        }

        private void Autentique()
        {
            Log.WriteLog($"Iniciando autenticação!");
            var contadorControladorDeLog = 0;

            while (true)
            {
                AjustaStatusDoMonitorAcesso("Off-line");

                try
                {
                    _conexaoTcpCliente = new TcpClient(Catraca.IpV4, Catraca.Porta);
                    _conexaoTcpCliente.Client.ReceiveTimeout = 3000;
                    _conexaoTcpCliente.Client.SendTimeout = 4000;

                    _statusCatraca = EnumStatusCatracaControlID.ConexaoAutenticada;

                    AjustaStatusDoMonitorAcesso("");
                    AutentiqueSessaoApi();

                    break;
                }
                catch (SocketException)
                {
                    contadorControladorDeLog++;

                    Log.WriteLog($"Falha na Conexão Reiniciando o Processo Tentativa:{contadorControladorDeLog}");
                    Autentique();

                    break;
                }
                catch (Exception ex)
                {
                    Log.WriteLogError($"Erro na Conexão ", ex);
                    Autentique();
                    break;
                }
            }
        }

        private void AjustaStatusDoMonitorAcesso(string status)
        {
            Monitor.AdicioneEvento(EventoCatraca.CrieAcessoMudancaEstado(Catraca, status));
            _conexaoTcpServidor.EnviarMensagem(JsonConvert.SerializeObject(EventoCatraca.CrieAcessoMudancaEstado(Catraca, status)));
        }

        private RestClient ObtenhaRestClient(string cdm)
        {
            return new RestClient($"http://{Catraca.IpV4}/{cdm}{_sessao}")
            {
                Timeout = -1
            };
        }

        private void AutentiqueSessaoApi()
        {
            var cliente = new RestClient($"http://{Catraca.IpV4}/login.fcgi");
            var response = ObtenhaResponse(cliente);

            _sessao = null; // Invalida sessão anterior.
            _sessao = response.Content.Split('"')[3];
            Log.WriteLog($"Api SESSÃO: {_sessao}");

        }

        private void ConfigureEmModoOnline(RestClient cliente)
        {
            Log.WriteLog($"Api Ativar Modo Online");

            try
            {
                var paramentros = "{\"general\": {\"online\":\"1\"}}";
                var response = ObtenhaResponse(cliente, paramentros);
                ObtenhaLogStatusCode("Api Modo Online StatusCode:", response.StatusCode);
            }
            catch (Exception erro)
            {
                Log.WriteLogError($"Erro", erro);
            }
        }

        private void ObtenhaLogStatusCode(string texto, HttpStatusCode statusCode)
        {
            Log.WriteLog($"{texto}{statusCode}\n");
        }

        public void ConfigureIndetificacaoLocal(RestClient cliente)
        {
            Log.WriteLog($"Api configurando Indentificação Local");

            try
            {
                var paramentros = "{\"general\": {\"local_identification\":\"1\"}}";
                var response = ObtenhaResponse(cliente, paramentros);
                ObtenhaLogStatusCode("Api Indetificação Local :", response.StatusCode);
            }
            catch (Exception erro)
            {
                Log.WriteLogError($"Erro", erro);
            }
        }


        private void ConexaoAutenticada()
        {
            var tempoUltimaFalha = DateTime.Now;
            var falhas = 0;

            Log.WriteLog($"{_statusCatraca} com sucesso");
            RegistreLogConfiguracaoEquipamento();

            while (true)
            {
                string dadosDaCatraca;
                try
                {
                    Log.WriteLog($"Aguardando uma solicitação da catraca!\n");
                    dadosDaCatraca = CarregueDadosCatraca();
                }
                catch (Exception ex)
                {
                    Log.WriteLog($"Erro ao carregar dados, de comunicação {ex}\n");
                    falhas++;

                    if (((TimeSpan)DateTime.Now.Subtract(tempoUltimaFalha)).TotalSeconds > 15)
                    {
                        tempoUltimaFalha = DateTime.Now;
                        falhas = 0;
                    }

                    if (falhas > 5)
                    {
                        Log.WriteLog($"Tentando Reiniciar conexão!");
                        return;
                    }

                    continue;
                }

                if (string.IsNullOrEmpty(dadosDaCatraca))
                {
                    Log.WriteLogError($"Falha ao ler dados string vazia", new ApplicationException("String vazia"));
                    return;
                }

                if (!DadosEstaoValidos(dadosDaCatraca))
                {
                    Log.WriteLog($"Dados inválidos: {dadosDaCatraca}");
                    continue;
                }

                if (_statusCatraca == EnumStatusCatracaControlID.ValidandorAcesso)
                {
                    ValideAcesso(dadosDaCatraca);
                }

                if (_statusCatraca == EnumStatusCatracaControlID.AguardandoGiro)
                {
                    RegisteAcesso();
                }
            }
        }

        private string CarregueDadosCatraca()
        {
            const int TamanhoBuffer = 1024;
            var bytes = new byte[TamanhoBuffer];
            var tempoParaVerificacao = DateTime.Now;

            while (true)
            {
                try
                {
                    var length = _conexaoTcpCliente.GetStream().Read(bytes, 0, TamanhoBuffer);
                    if (length > 0)
                    {
                        tempoParaVerificacao = DateTime.Now;
                    }

                    return Encoding.ASCII.GetString(bytes, 0, length);
                }
                catch (IOException ioex)
                {
                    if (ioex.InnerException is SocketException socketException && socketException.ErrorCode == 10060)
                    {
                        if (((TimeSpan)DateTime.Now.Subtract(tempoParaVerificacao)).TotalSeconds > 20)
                        {
                            EnviaMensagemEmBytes($"01+EH+00+{DateTime.Now.ToString("dd/MM/yy HH:mm:ss")}]00/00/00]00/00/00");

                            tempoParaVerificacao = DateTime.Now;
                        }

                        var ping = new Ping().Send(Catraca.IpV4, 1000);
                        if (ping.Status == IPStatus.Success)
                        {
                            continue;
                        }
                    }

                    throw ioex;
                }
            }
        }

        private bool DadosEstaoValidos(string dadosRecebidos)
        {
            try
            {
                if (dadosRecebidos.Length < 20)
                {
                    Log.WriteLog($"O tamanho da string está incorreto, Dados: {dadosRecebidos}");
                    return false;
                }

                var comandos = dadosRecebidos.Split(']');
                if (comandos.Length < 2)
                {
                    Log.WriteLog($"Não foi possível dividir a String para leitura, Dados: {dadosRecebidos}");
                    return false;
                }

                if (comandos[0].Substring(6).Equals("REON+000+0"))
                {
                    if (comandos[1].Length < 2)
                    {
                        Log.WriteLog($"O tamanho do Código de Acesso é inválido, Dados: {dadosRecebidos}");
                        return false;
                    }

                    _tipoAcesso = EnumTipoAcesso.Indefinido;

                    if (comandos[5].Length >= 3)
                    {
                        if (comandos[5].Substring(0, comandos[5].Length - 2) == "4")
                        {
                            _tipoAcesso = EnumTipoAcesso.Teclado;
                        }
                    }

                    _statusCatraca = EnumStatusCatracaControlID.ValidandorAcesso;
                }
                else if (comandos[0].Substring(6).Equals("REON+000+81"))
                {
                    _sentidoDoGiro = (EnumSentidoGiro)Int32.Parse(comandos[3]);
                    _statusCatraca = EnumStatusCatracaControlID.AguardandoGiro;
                }
                else
                {
                    _statusCatraca = EnumStatusCatracaControlID.ConexaoAutenticada;
                }
                return true;

            }
            catch (Exception ex)
            {
                Log.WriteLogError($"Falha ao tratar string enviada pela catraca", ex);
                return false;
            }
        }

        protected virtual void ValideTemAcessoPorTeclado(Pessoa pessoa, EnumTipoAcesso tipoAcesso) { }
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

        private void ValideAcesso(string dadosRecebidos)
        {
            var codigo = dadosRecebidos.Split(']')[1];

            int.TryParse(codigo, out var codigoRecebido);

            Log.WriteLog($"Iniciando validação de acesso código: {codigoRecebido}");

            try
            {
                Log.WriteLog($"Iniciando a identificação da pessoa.");

                _pessoa = ObtenhaPessoa(codigoRecebido.ToString());
                _indetificacaoDaPessoa = $"Pessoa:{_pessoa.RecuperaTipo()} Código:{_pessoa.Id} Nome:{_pessoa.Nome}";

                Log.WriteLog($"Foi indetificada {_indetificacaoDaPessoa}");
            }
            catch (Exception ex)
            {
                var pessoa = new Aluno
                {
                    Nome = "Não encontrado!"
                };

                var evento = EventoCatraca.CrieAcessoNegado(EnumSentidoGiro.Indefinido, pessoa, Catraca);

                Monitor.AdicioneEvento(evento);
                _conexaoTcpServidor.EnviarMensagem(JsonConvert.SerializeObject(evento));

                EnviaMensagemEmBytes("01+REON+00+30]10]" + pessoa.Nome + "}Acesso Negado!]");

                Log.WriteLog($"Acesso Negado, Motivo:{pessoa.Nome}!");
                return;
            }

            try
            {
                CarregueInformacoesCFG();

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

                string msgRestricao = MonteMensagemRestricao(_pessoa);
                if (!string.IsNullOrEmpty(msgRestricao))
                {
                    evento = EventoCatraca.CrieAcessoLiberadoComRestricao(EnumSentidoGiro.Indefinido, _pessoa, Catraca, msgRestricao);
                }
                else
                {
                    evento = EventoCatraca.CrieAcessoLiberado(EnumSentidoGiro.Indefinido, _pessoa, Catraca);
                }

                Monitor.AdicioneEvento(evento);
                _conexaoTcpServidor.EnviarMensagem(JsonConvert.SerializeObject(evento));

                EnviaMensagemEmBytes("01+REON+00+1]10]" + _pessoa.Nome + "}Acesso Liberado!]");

                Log.WriteLog($"Acesso Liberado.");

            }
            catch (AcessoNegadoException ex)
            {
                var evento = EventoCatraca.CrieAcessoNegado(EnumSentidoGiro.Indefinido, _pessoa, Catraca);
                evento.Mensagem2 = ex.Message;

                Monitor.AdicioneEvento(evento);
                _conexaoTcpServidor.EnviarMensagem(JsonConvert.SerializeObject(evento));

                EnviaMensagemEmBytes("01+REON+00+30]10]" + _pessoa.Nome + "}Acesso Negado!]");

                Log.WriteLog($"O acesso foi Negado, motivo:\"{ex.TargetSite.Name}\"");

                _pessoa = null;
            }
        }

        private void RegisteAcesso()
        {
            Log.WriteLog($"Registrando o Acesso");

            try
            {
                if (_pessoa != null)
                {
                    RegistreAcessoPessoa(_pessoa, _sentidoDoGiro);
                    Log.WriteLog($"{_indetificacaoDaPessoa} Sentido do Giro da Catraca:{_sentidoDoGiro}");
                }
                _pessoa = null;
            }
            catch (Exception ex)
            {
                Log.WriteLogError($"Falha ao registrar o acesso {_indetificacaoDaPessoa} \n", ex);
            }
        }

        public void PareCatraca()
        {
            _conexaoTcpCliente?.Close();
            _conexaoTcpCliente?.Dispose();

            if (_threadPing != null)
            {
                _threadPing.Abort();
            }

            _threadDeMonitoramento.Abort();

            Log.WriteLog($"Catraca: {Catraca.Codigo} - Parando");
        }

        private void EnviaMensagemEmBytes(string mensagem)
        {
            try
            {
                var stream = _conexaoTcpCliente.GetStream();
                byte[] byteMensagem = MontaProtocolo(mensagem);
                stream.Write(byteMensagem, 0, byteMensagem.Length);
            }
            catch (Exception ex)
            {
                Log.WriteLogError($"Erro ao enviar mensagem para simulador: ", ex);
            }
        }

        private byte[] MontaProtocolo(string mensagem)
        {
            byte[] mensagemBytes = Encoding.ASCII.GetBytes(mensagem);
            short intValue = (Int16)mensagemBytes.Length;
            byte[] intBytes = BitConverter.GetBytes(intValue);
            Array.Reverse(intBytes);

            byte checkSum = CalculaCheckSum(mensagemBytes);

            mensagemBytes = AdicionaByteInicioArray(mensagemBytes, intBytes[0]);
            mensagemBytes = AdicionaByteInicioArray(mensagemBytes, intBytes[1]);
            mensagemBytes = AdicionaByteInicioArray(mensagemBytes, 2);

            AdicionaByteFinalArray(ref mensagemBytes, checkSum);
            AdicionaByteFinalArray(ref mensagemBytes, 0x03);
            return mensagemBytes;
        }

        private static byte CalculaCheckSum(byte[] bytes)
        {
            Byte chkSumByte = 0x00;
            for (int i = 0; i < bytes.Length; i++)
            {
                chkSumByte ^= bytes[i];
            }

            chkSumByte ^= (Byte)(bytes.Length % 256);
            chkSumByte ^= (Byte)(bytes.Length / 256);

            string h16 = int.Parse((Math.Floor((Decimal)chkSumByte / 16)).ToString()).ToString("X");
            string h1 = int.Parse((chkSumByte % 16).ToString()).ToString("X");

            return Convert.ToByte("0x" + h16 + h1, 16);
        }

        private void AdicionaByteFinalArray(ref byte[] dst, byte src)
        {
            Array.Resize(ref dst, dst.Length + 1);
            dst[dst.Length - 1] = src;
        }

        private byte[] AdicionaByteInicioArray(byte[] bArray, byte novoByte)
        {
            byte[] newArray = new byte[bArray.Length + 1];
            bArray.CopyTo(newArray, 1);
            newArray[0] = novoByte;
            return newArray;
        }

        private void RegistreLogConfiguracaoEquipamento()
        {
            var resetCliente = ObtenhaRestClient("get_configuration.fcgi?session=");
            var configuracao = ObtenhaConfiguracaoEquipamentoLog(resetCliente);
            var logConfiguracao = FormateLogConfiguracao(configuracao, Catraca.Descricao);
            LogGeral.WriteLogConfiguracao(logConfiguracao);
        }

        private IRestResponse ObtenhaResponseConfiguracao(RestClient restClient, string parametros)
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", parametros, ParameterType.RequestBody);
            return restClient.Execute(request); ;
        }


        private IRestResponse ObtenhaResponse(RestClient restClient, string parametros = "")
        {
            var client = restClient;
            var request = new RestRequest(Method.POST);

            if (parametros.Equals(""))
            {
                var catracalogin = new CatracaLoginControlID
                {
                    login = Catraca.Login,
                    password = Catraca.Password
                };

                parametros = JsonConvert.SerializeObject(catracalogin);
            }

            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", parametros, ParameterType.RequestBody);

            return client.Execute(request); ;
        }

        private string ObtenhaConfiguracaoEquipamentoLog(RestClient cliente)
        {

            var paramentros = "{\n\t\"general\": " +
                                        "[\n\t\t\"online\"," +
                                        "\n\t\t\"beep_enabled\"," +
                                        "\n\t\t\"bell_enabled\"," +
                                        "\n\t\t\"bell_relay\"," +
                                        "\n\t\t\"catra_timeout\"," +
                                        "\n\t\t\"local_identification\"," +
                                        "\n\t\t\"exception_mode\"," +
                                        "\n\t\t\"language\"," +
                                        "\n\t\t\"daylight_savings_time_start\"," +
                                        "\n\t\t\"daylight_savings_time_end\"," +
                                        "\n\t\t\"auto_reboot\"\n\t]," +
                                        "\n\t\"w_in0\": [\"byte_order\"]," +
                                        "\n\t\"w_in1\": [\"byte_order\"]," +
                                        "\n\t\"alarm\": [\n\t\t\"siren_enabled\"" +
                                        ",\n\t\t\"siren_relay\"\n\t]," +
                                        "\n\t\"identifier\": [\n\t\t\"verbose_logging\"," +
                                        "\n\t\t\"log_type\"," +
                                        "\n\t\t\"multi_factor_authentication\"\n\t]," +
                                        "\n\t\"bio_id\": [\"similarity_threshold_1ton\"]," +
                                        "\n\t\"online_client\": [\n\t\t\"server_id\", " +
                                        "\n\t\t\"extract_template\"," +
                                        "\n\t\t\"max_request_attempts\"\n\t]," +
                                        "\n\t\"catra\": [\n\t\t\"anti_passback\"," +
                                        "\n\t\t\"daily_reset\",\n\t\t\"gateway\"," +
                                        "\n\t\t\"operation_mode\"\n\t]," +
                                        "\n\t\"bio_module\": [\"var_min\"]," +
                                        "\n\t\"monitor\": [\n\t\t\"path\"," +
                                        "\n\t\t\"hostname\",\n\t\t\"port\"," +
                                        "\n\t\t\"request_timeout\"\n\t]," +
                                        "\n\t\"push_server\": [\n\t\t\"push_request_timeout\"," +
                                        "\n\t\t\"push_request_period\"," +
                                        "\n\t\t\"push_remote_address\"\n\t]\n}";

            var response = ObtenhaResponseConfiguracao(cliente, paramentros);

            return response.Content;
        }

        private static string FormateLogConfiguracao(string message, string descricao)
        {
            var original = message;
            var delimitadores = new char[] { ',' };
            var strings = original.Split(delimitadores);
            var messageFormatada = $"Log da configuração da {descricao}";

            foreach (string s in strings)
            {
                messageFormatada += $"\n\t\t\t\t{s}";
            }

            return messageFormatada;
        }
    }
}
