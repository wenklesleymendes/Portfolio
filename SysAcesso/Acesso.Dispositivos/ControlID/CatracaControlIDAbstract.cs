using Acesso.Dominio;
using Acesso.Servicos;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Acesso.Dominio.Pessoas.Tipo;
using static Acesso.Control.ID.EstadoCatracaControlID;

namespace Acesso.Control.ID
{
    /// <summary>
    /// Classe Principal para o projeto Control iD
    /// </summary>
    public abstract class CatracaControlIDAbstract
	{
		/// <summary>
		/// Objeto fazer log
		/// </summary>
		protected LogCatraca Log { get; }

		protected Dispositivo Dispositivo { get; }

		/// <summary>
		/// CFG Regras de Acessso de ser aplicada
		/// </summary>
		protected RegrasAcesso _cfgRegraAcesso;

		private readonly TcpIpServidor _conexaoTcpServidor = new TcpIpServidor();

		private Pessoa _pessoa = null;

		private InformacaoConexao _cfgConexao;

		private TcpClient _conexaoTcpCliente { get; set; }

		private Thread _threadDeMonitoramento { get; set; }

		private Thread _threadPing { get; }

		private EnumDispositvo _enumDispositivo { get; set; }
		private SentidoGiro _giroIndefinido = SentidoGiro.Indefinido;
		private TipoAcesso _tipoAcessoIndefinido = TipoAcesso.Indefinido;

		private IServicoMonitorAcesso _monitor { get; }

		private string _indetificacaoDaPessoa;
		private string _sessao = null;

		private protected Dispositivo Dispositivo1 { get; }

		private protected IServicoMonitorAcesso MonitorAcesso { get; }

		protected abstract Pessoa ObtenhaPessoa(string codigo);
		protected abstract void RegistreAcessoPessoa(Pessoa pessoa, SentidoGiro sentidoDoGiro);

		/// <summary>
		/// Contrutor Control Id Abstract
		/// </summary>
		/// <param name="dispositivo"></param>
		/// <param name="IServicoMonitorAcesso"></param>
		/// <param name="threadPing"></param>
		protected CatracaControlIDAbstract(Dispositivo dispositivo, IServicoMonitorAcesso IServicoMonitorAcesso, Thread threadPing)
		{
			Log = new LogCatraca(dispositivo);
			Log.WriteLog($"Iniciando {nameof(CatracaControlIDAbstract)}({dispositivo},{IServicoMonitorAcesso})");

			CarregueCFG();

			Dispositivo = dispositivo;
			_monitor = IServicoMonitorAcesso;
			_threadPing = threadPing;

			if (Dispositivo.Codigo == 1)
			{
				var tipoServidor = "Servidor do Monitor de Acesso";
				_conexaoTcpServidor.IniciarServidorTcpIp(_cfgConexao.IP, _cfgConexao.PortaTcpIp, tipoServidor);
			}

			InicieMonitoramento();
		}

		protected CatracaControlIDAbstract(Dispositivo dispositivo, IServicoMonitorAcesso monitorAcesso)
		{
			this.Dispositivo1 = dispositivo;
			this.MonitorAcesso = monitorAcesso;
		}

		private void CarregueCFG()
		{
			Log.WriteLog($"Iniciando {nameof(CarregueCFG)}");

			_cfgRegraAcesso = MapeadorArquivoJson.CarreguerArquivoJson<RegrasAcesso>("Acesso.Acesso.cfg");
			_cfgConexao = MapeadorArquivoJson.CarreguerArquivoJson<InformacaoConexao>("Acesso.Servidor.cfg");
		}

		private void InicieMonitoramento()
		{
			IniciarTreadMonitoramento();

			_threadDeMonitoramento.IsBackground = true;
			_threadDeMonitoramento.Start();
		}

		private void IniciarTreadMonitoramento()
		{
			_threadDeMonitoramento = new Thread(() =>
			{
				Log.WriteLog($"Iniciando {nameof(IniciarTreadMonitoramento)}");

				while (true)
				{
					try
					{
						Autentique();

						RestClient clienteSet = ObtenhaRestClient("set_configuration.fcgi?session=");
						ConfigureEmModoOnline(clienteSet);
						ConfigureIndetificacaoLocal(clienteSet);

						var messagen = $"{DateTime.Now}- Sessão autenticado e catraca configurada";
						EnviaMensagemEmBytes(messagen);

						ConexaoAutenticada();
					}
					catch (ThreadAbortException trex)
					{
						_monitor.AdicioneEvento(EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, "Off-line"));
						_conexaoTcpServidor.EnviarMensagem(JsonConvert.SerializeObject(EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, "Off-line")));

						Log.WriteLogError($"A thread de monitoramento foi abortada", trex);
						_conexaoTcpCliente.Close();
					}
					catch (Exception trex)
					{
						_monitor.AdicioneEvento(EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, "Off-line"));
						_conexaoTcpServidor.EnviarMensagem(JsonConvert.SerializeObject(EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, "Off-line")));

						Log.WriteLogError($"Ocorreu uma falha na tread de monitoramento", trex);
						_conexaoTcpCliente.Close();
					}
				}
			});
		}

		private void Autentique()
		{
			Log.WriteLog($"Iniciando {nameof(Autentique)}");
			var contadorLog = 0;

			while (true)
			{
				AjustaStatusDoMonitorAcesso("Off-line");

				try
				{
					var portaCatraca = Convert.ToInt32(Dispositivo.PortaCatraca);

					_conexaoTcpCliente = new TcpClient(Dispositivo.IpCatraca, portaCatraca)
					{
						Client =
						{
							ReceiveTimeout = 3000,
							SendTimeout = 4000
						}
					};

					_enumDispositivo = EnumDispositvo.ConexaoMonitor;

					AjustaStatusDoMonitorAcesso("");

					AutentiqueSessaoDoDispositivoApi();

					break;
				}
				catch (SocketException)
				{
					contadorLog++;

					Log.WriteLog($"Falha na Conexão Reiniciando o Processo Tentativa:{contadorLog}");
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
			Log.WriteLog($"Iniciando {nameof(AjustaStatusDoMonitorAcesso)}({status})");

			_monitor.AdicioneEvento(EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, status));
			_conexaoTcpServidor.EnviarMensagem(JsonConvert.SerializeObject(EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, status)));
		}

		private RestClient ObtenhaRestClient(string cdm) =>
			new RestClient($"http://{Dispositivo.IpCatraca}/{cdm}{_sessao}")
			{
				Timeout = -1
			};

		private void AutentiqueSessaoDoDispositivoApi()
		{
			Log.WriteLog($"Iniciando {nameof(AutentiqueSessaoDoDispositivoApi)}");

			RestClient urlDispositivo = ObtenhaUrlDispositivo();
			IRestResponse requisicao = ObtenhaResposta(urlDispositivo);

			_sessao = null; // Invalida sessão anterior.
			_sessao = requisicao.Content.Split('"')[3];
			Log.WriteLog($"Api SESSÃO: {_sessao}");

		}

		private RestClient ObtenhaUrlDispositivo() => new RestClient($"http://{Dispositivo.IpCatraca}/login.fcgi");

		private void ConfigureEmModoOnline(RestClient cliente)
		{
			Log.WriteLog($"Api Ativar Modo Online");

			try
			{
				var url = "{\"general\": {\"online\":\"1\"}}";
				IRestResponse response = ObtenhaResposta(cliente, url);
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

		private void ConfigureIndetificacaoLocal(RestClient cliente)
		{
			Log.WriteLog($"Api configurando Indentificação Local");

			try
			{
				var url = "{\"general\": {\"local_identification\":\"1\"}}";
				IRestResponse response = ObtenhaResposta(cliente, url);
				ObtenhaLogStatusCode("Api Indetificação Local :", response.StatusCode);
			}
			catch (Exception erro)
			{
				Log.WriteLogError($"Erro", erro);
			}
		}


		private void ConexaoAutenticada()
		{
			DateTime tempoUltimaFalha = DateTime.Now;
			var falhas = 0;

			Log.WriteLog($"{_enumDispositivo} com sucesso");
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

				if (_enumDispositivo == EnumDispositvo.ValidandorAcesso)
				{
					ValideAcesso(dadosDaCatraca);
				}

				if (_enumDispositivo == EnumDispositvo.AguardandoGiro)
				{
					RegisteAcesso();
				}
			}
		}

		private string CarregueDadosCatraca()
		{
			const int TamanhoBuffer = 1024;
			var bytes = new byte[TamanhoBuffer];
			DateTime tempoParaVerificacao = DateTime.Now;

			while (true)
			{
				try
				{
					int length = _conexaoTcpCliente.GetStream().Read(bytes, 0, TamanhoBuffer);
					if (length > 0)
					{
						tempoParaVerificacao = DateTime.Now;
					}

					return Encoding.ASCII.GetString(bytes, 0, length);
				}
				catch (IOException ioex)
				{
					if (!(ioex.InnerException is SocketException socketException) || socketException.ErrorCode != 10060)
					{
						throw ioex;
					}

					if (DateTime.Now.Subtract(tempoParaVerificacao).TotalSeconds > 20)
					{
						EnviaMensagemEmBytes($"01+EH+00+{DateTime.Now:dd/MM/yy HH:mm:ss}]00/00/00]00/00/00");

						tempoParaVerificacao = DateTime.Now;
					}

					PingReply ping = new Ping().Send(Dispositivo.IpCatraca, 1000);
					if (ping.Status == IPStatus.Success)
					{
						continue;
					}

					throw;
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

				string[] comandos = dadosRecebidos.Split(']');
				if (comandos.Length < 2)
				{
					Log.WriteLog($"Não foi possível dividir a String para leitura, Dados: {dadosRecebidos}");
					return false;
				}

				switch (comandos[0].Substring(6))
				{
					case "REON+000+0" when comandos[1].Length < 2:
						Log.WriteLog($"O tamanho do Código de Acesso é inválido, Dados: {dadosRecebidos}");
						return false;
					case "REON+000+0":
						{
							_tipoAcessoIndefinido = TipoAcesso.Indefinido;

							if (comandos[5].Length >= 3 && comandos[5].Substring(0, comandos[5].Length - 2) == "4")
							{
								_tipoAcessoIndefinido = TipoAcesso.Teclado;
							}

							_enumDispositivo = EnumDispositvo.ValidandorAcesso;

							break;
						}
					case "REON+000+81":
						_giroIndefinido = (SentidoGiro)Int32.Parse(comandos[3]);
						_enumDispositivo = EnumDispositvo.AguardandoGiro;

						break;
					default:
						_enumDispositivo = EnumDispositvo.ConexaoMonitor;

						break;
				}
				return true;

			}
			catch (Exception ex)
			{
				Log.WriteLogError($"Falha ao tratar string enviada pela catraca", ex);
				return false;
			}
		}

		/// <summary>
		/// Valida pessoa que pode acessar usando o teclado
		/// </summary>
		/// <param name="pessoa"></param>
		/// <param name="tipoAcesso"></param>
		protected virtual void ValideTemAcessoPorTeclado(Pessoa pessoa, TipoAcesso tipoAcesso) { }

		/// <summary>
		/// Valida Pesssoa o cadostro da pessoa esta como ativa
		/// </summary>
		/// <param name="pessoa"></param>
		protected virtual void ValidePessoaEstaAtiva(Pessoa pessoa) { }

		/// <summary>
		/// Valida o tempo minimo para fazer um novo acesso
		/// </summary>
		/// <param name="pessoa"></param>
		protected virtual void ValideTempoMinimoParaNovoAcesso(Pessoa pessoa) { }

		/// <summary>
		/// Valida se Aluno pode sair antes do tempo configurado.
		/// </summary>
		/// <param name="aluno"></param>
		/// <returns></returns>
		protected virtual bool ValideAlunoPossuiLiberacao(Aluno aluno) => false;

		/// <summary>
		/// Valida se aluno possui alguns tipo de bloqueio
		/// </summary>
		/// <param name="aluno"></param>
		protected virtual void ValideAlunoPossuiBloqueio(Aluno aluno) { }

		/// <summary>
		/// Valida Aluno Possui INadiplencia
		/// </summary>
		/// <param name="aluno"></param>
		protected virtual void ValideAlunoPossuiInadimplencia(Aluno aluno) { }

		/// <summary>
		///  Valida Aluno Falta algun tipo de documento
		/// </summary>
		/// <param name="aluno"></param>
		protected virtual void ValideAlunoFaltaDocumentos(Aluno aluno) { }

		/// <summary>
		/// Valide Aluno Falta Materiais
		/// </summary>
		/// <param name="aluno"></param>
		protected virtual void ValideAlunoFaltaMateriais(Aluno aluno) { }

		/// <summary>
		/// Valide Aluno Pode Sair Sozinho
		/// </summary>
		/// <param name="aluno"></param>
		protected virtual void ValideAlunoPodeSairSozinho(Aluno aluno) { }

		/// <summary>
		///  Valide Aluno Possui Ocorrencia
		/// </summary>
		/// <param name="aluno"></param>
		protected virtual void ValideAlunoPossuiOcorrencia(Aluno aluno) { }

		/// <summary>
		/// Valide Autorizado Possui Inadimplencia
		/// </summary>
		/// <param name="autorizadoBuscarAluno"></param>
		protected virtual void ValideAutorizadoPossuiInadimplencia(AutorizadoBuscarAluno autorizadoBuscarAluno) { }

		/// <summary>
		/// Valide Autorizado Falta Documentos
		/// </summary>
		/// <param name="autorizadoBuscarAluno"></param>
		protected virtual void ValideAutorizadoFaltaDocumentos(AutorizadoBuscarAluno autorizadoBuscarAluno) { }

		/// <summary>
		/// Valide Autorizado Falta Materiais
		/// </summary>
		/// <param name="autorizadoBuscarAluno"></param>
		protected virtual void ValideAutorizadoFaltaMateriais(AutorizadoBuscarAluno autorizadoBuscarAluno) { }

		/// <summary>
		/// Valide Autorizado Possui Ocorrencia
		/// </summary>
		/// <param name="autorizadoBuscarAluno"></param>
		protected virtual void ValideAutorizadoPossuiOcorrencia(AutorizadoBuscarAluno autorizadoBuscarAluno) { }


		/// <summary>
		/// Valide Responsavel PossuiBloqueio
		/// </summary>
		/// <param name="responsavel"></param>
		protected virtual void ValideResponsavelPossuiBloqueio(Responsavel responsavel) { }

		/// <summary>
		/// Valide Responsavel Possui Inadimplencia
		/// </summary>
		/// <param name="responsavel"></param>
		protected virtual void ValideResponsavelPossuiInadimplencia(Responsavel responsavel) { }

		/// <summary>
		/// Valide Responsavel Falta Documentos
		/// </summary>
		/// <param name="responsavel"></param>
		protected virtual void ValideResponsavelFaltaDocumentos(Responsavel responsavel) { }

		/// <summary>
		/// Valide Responsavel Falta Materiais
		/// </summary>
		/// <param name="responsavel"></param>
		protected virtual void ValideResponsavelFaltaMateriais(Responsavel responsavel) { }

		/// <summary>
		/// Valide Responsavel Possui Ocorrencia
		/// </summary>
		/// <param name="responsavel"></param>
		protected virtual void ValideResponsavelPossuiOcorrencia(Responsavel responsavel) { }

		/// <summary>
		/// Valide Professor Possui Ocorrencia
		/// </summary>
		/// <param name="professor"></param>
		protected virtual void ValideProfessorPossuiOcorrencia(Professor professor) { }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="colaborador"></param>
		protected virtual void ValideColaboradorPossuiOcorrencias(Colaborador colaborador) { }

		/// <summary>
		/// Valide Colaborador Possui Ocorrencias
		/// </summary>
		/// <param name="pessoa"></param>
		protected virtual void ValideDentroDoHorarioDeAcesso(Pessoa pessoa) { }

		/// <summary>
		/// Monte Mensagem Restricao
		/// </summary>
		/// <param name="pessoa"></param>
		/// <returns></returns>
		protected virtual string MonteMensagemRestricao(Pessoa pessoa) => null;

		private void ValideAcesso(string dadosRecebidos)
		{
			string codigo = dadosRecebidos.Split(']')[1];

			int.TryParse(codigo, out int codigoRecebido);

			Log.WriteLog($"Iniciando validação de acesso código: {codigoRecebido}");

			EventoCatraca evento;

			try
			{
				Log.WriteLog($"Iniciando a identificação da pessoa.");

				_pessoa = ObtenhaPessoa(codigoRecebido.ToString());
				_indetificacaoDaPessoa = $"Pessoa:{_pessoa.RecuperaTipo()} Código:{_pessoa.Id} Nome:{_pessoa.Nome}";

				Log.WriteLog($"Foi indetificada {_indetificacaoDaPessoa}");
			}
			catch (Exception)
			{
				var pessoa = new Aluno
				{
					Nome = "Não encontrado!"
				};

				evento = EventoCatraca.CrieAcessoNegado(SentidoGiro.Indefinido, pessoa, Dispositivo);

				_monitor.AdicioneEvento(evento);
				_conexaoTcpServidor.EnviarMensagem(JsonConvert.SerializeObject(evento));

				EnviaMensagemEmBytes("01+REON+00+30]10]" + pessoa.Nome + "}Acesso Negado!]");

				Log.WriteLog($"Acesso Negado, Motivo:{pessoa.Nome}!");
				return;
			}

			try
			{
				CarregueCFG();

				ValidePessoaEstaAtiva(_pessoa);
				ValideTemAcessoPorTeclado(_pessoa, _tipoAcessoIndefinido);

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

				string msgRestricao = MonteMensagemRestricao(_pessoa);
				evento = !string.IsNullOrEmpty(msgRestricao) ? EventoCatraca.CrieAcessoLiberadoComRestricao(SentidoGiro.Indefinido, _pessoa, Dispositivo, msgRestricao)
					: EventoCatraca.CrieAcessoLiberado(SentidoGiro.Indefinido, _pessoa, Dispositivo);

				_monitor.AdicioneEvento(evento);
				_conexaoTcpServidor.EnviarMensagem(JsonConvert.SerializeObject(evento));

				EnviaMensagemEmBytes("01+REON+00+1]10]" + _pessoa.Nome + "}Acesso Liberado!]");

				Log.WriteLog($"Acesso Liberado.");

			}
			catch (AcessoNegadoException ex)
			{
				evento = EventoCatraca.CrieAcessoNegado(SentidoGiro.Indefinido, _pessoa, Dispositivo);
				evento.Mensagem2 = ex.Message;

				_monitor.AdicioneEvento(evento);
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
					RegistreAcessoPessoa(_pessoa, _giroIndefinido);
					Log.WriteLog($"{_indetificacaoDaPessoa} Sentido do Giro da Dispositivo:{_giroIndefinido}");
				}
				_pessoa = null;
			}
			catch (Exception ex)
			{
				Log.WriteLogError($"Falha ao registrar o acesso {_indetificacaoDaPessoa} \n", ex);
			}
		}

		/// <summary>
		/// Para as tread e WCF
		/// </summary>
		public void PareCatraca()
		{
			_conexaoTcpCliente?.Close();
			_conexaoTcpCliente?.Dispose();

			_threadPing?.Abort();

			_threadDeMonitoramento.Abort();

			Log.WriteLog($"Dispositivo: {Dispositivo.Codigo} - Parando");
		}

		private void EnviaMensagemEmBytes(string mensagem)
		{
			try
			{
				NetworkStream stream = _conexaoTcpCliente.GetStream();
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
			var intValue = (short)mensagemBytes.Length;
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

		private static byte CalculaCheckSum(IReadOnlyCollection<byte> bytes)
		{
			byte chkSumByte = bytes.Aggregate<byte, byte>(0x00, (current, t) => (byte)(current ^ t));

			chkSumByte ^= (byte)(bytes.Count % 256);
			chkSumByte ^= (byte)(bytes.Count / 256);

			var h16 = int.Parse((Math.Floor((decimal)chkSumByte / 16)).ToString()).ToString("X");
			var h1 = int.Parse((chkSumByte % 16).ToString()).ToString("X");

			return Convert.ToByte("0x" + h16 + h1, 16);
		}

		private static void AdicionaByteFinalArray(ref byte[] dst, byte src)
		{
			Array.Resize(ref dst, dst.Length + 1);
			dst[dst.Length - 1] = src;
		}

		private static byte[] AdicionaByteInicioArray(byte[] bArray, byte novoByte)
		{
			var newArray = new byte[bArray.Length + 1];
			bArray.CopyTo(newArray, 1);
			newArray[0] = novoByte;
			return newArray;
		}

		private void RegistreLogConfiguracaoEquipamento()
		{
			RestClient resetCliente = ObtenhaRestClient("get_configuration.fcgi?session=");
			string configuracao = ObtenhaConfiguracaoEquipamentoLog(resetCliente);
			string logConfiguracao = FormateLogConfiguracao(configuracao, Dispositivo.Descricao);
			AuditoriaLog.WriteLogCFG(logConfiguracao);
		}

		private static IRestResponse ObtenhaResponseConfiguracao(RestClient restClient, string parametros)
		{
			var request = new RestRequest(Method.POST);
			request.AddHeader("content-type", "application/json");
			request.AddParameter("application/json", parametros, ParameterType.RequestBody);
			return restClient.Execute(request);
		}

		private IRestResponse ObtenhaResposta(RestClient urlDispositivo, string url = "")
		{
			if (urlDispositivo == null)
			{
				throw new ArgumentNullException(nameof(urlDispositivo));
			}

			if (url is null)
			{
				var catracalogin = new CatracaLoginControlID
				{
					login = Dispositivo.Login,
					password = Dispositivo.Senha
				};

				url = JsonConvert.SerializeObject(catracalogin);
			}

			var requisicao = new RestRequest(Method.POST);
			requisicao.AddHeader("content-type", "application/json");
			requisicao.AddParameter("application/json", url, ParameterType.RequestBody);

			IRestResponse retorno = urlDispositivo.Execute(requisicao);
			Log.WriteLog($"Retorno {nameof(ObtenhaResposta)} = {retorno.StatusCode}");

			return retorno;
		}

		private static string ObtenhaConfiguracaoEquipamentoLog(RestClient cliente)
		{
			string paramentros = "{\n\t\"general\": " +
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

			IRestResponse response = ObtenhaResponseConfiguracao(cliente, paramentros);

			return response.Content;
		}

		private static string FormateLogConfiguracao(string message, string descricao)
		{
			string original = message;
			var delimitadores = new char[] { ',' };
			string[] strings = original.Split(delimitadores);
			var messageFormatada = $"Log da configuração da {descricao}";

			return strings.Aggregate(messageFormatada, (current, s) => current + $"\n\t\t\t\t{s}");
		}
	}
}
