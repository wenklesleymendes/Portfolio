using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Core.Services;
using EMCatraca.Server.Excecoes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TcpIp;
using static System.Int32;

namespace EMCatraca.Henry
{
    public abstract class CatracaHenryAbstract
	{
		protected Dispositivo Dispositivo { get; }

		protected RegrasAcesso ConfiguracaoAcesso;

		protected abstract Pessoa ObtenhaPessoa(string codigo);

		protected abstract void RegistreAcessoPessoa(Pessoa pessoa, SentidoGiro sentidoDoGiro);

		private TcpClient ConexaoTcp { get; set; }

		private readonly TcpIpServidor ServidorMonitor = new TcpIp.TcpIpServidor();

		private Thread ThreadMonitoramento { get; set; }

		private Thread ThreadPing { get; }

		private EnumEstadoDaCatracaHenry EstadoDeConexaoDaCatraca { get; set; }

		private IServicoMonitorAcesso ServicoMonitorAcesso { get; set; }

		private InformacaoConexao ConfiguracaoServidor;
		private Pessoa _pessoa = null;
		private SentidoGiro _sentidoDoGiro = SentidoGiro.Indefinido;
		private TipoAcesso _tipoAcesso = TipoAcesso.Indefinido;
		private string _indetificacaoDaPessoa;

		public CatracaHenryAbstract(Dispositivo dispositivo, IServicoMonitorAcesso servicoMonitorAcesso)
		{
			LogAuditoria.Escreva(nameof(CatracaHenryAbstract),
				$"Codigo:{dispositivo.Codigo}," +
				$"Descrição{dispositivo.Descricao},");

			AtualizeConfiguracoes();

			Dispositivo = dispositivo;
			ServicoMonitorAcesso = servicoMonitorAcesso;

			if (dispositivo.Codigo == 1)
			{
				var tipoServidor = "Servidor do Monitor de Acesso";

				ServidorMonitor.IniciarServidorTcpIp(ConfiguracaoServidor.IP, ConfiguracaoServidor.PortaTcpIp,
					tipoServidor);
			}

			InicieThread();
		}

		private void AtualizeConfiguracoes()
		{
			ConfiguracaoAcesso = MapeadorArquivoJson.CarreguerArquivoJson<RegrasAcesso>("emcatraca.acesso.cfg");

			ConfiguracaoServidor =
				MapeadorArquivoJson.CarreguerArquivoJson<InformacaoConexao>("emcatraca.servidor.cfg");
		}

		private void InicieThread()
		{
			ThreadMonitoramento = new Thread(() =>
			{
				LogAuditoria.Escreva($"Iniciando a thread", nameof(CatracaHenryAbstract));

				while (true)
				{
					try
					{
						Conecte();

						var comando = $"01+EH+00+{DateTime.Now:dd/MM/yy HH:mm:ss}";
						EnviaMensagemEmBytes(comando);

						Conectado();
					}
					catch (ThreadAbortException trex)
					{
						ServicoMonitorAcesso.AdicioneEvento(
							EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, "Off-line"));

						ServidorMonitor.EnviarMensagem(
							JsonConvert.SerializeObject(
								EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, "Off-line")));

						AuditoriaLog.EscrevaErro(nameof(CatracaHenryAbstract),trex);
						ConexaoTcp.Close();
					}
					catch (Exception ex)
					{
						ServicoMonitorAcesso.AdicioneEvento(
							EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, "Off-line"));

						ServidorMonitor.EnviarMensagem(
							JsonConvert.SerializeObject(
								EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, "Off-line")));

						AuditoriaLog.EscrevaErro(nameof(CatracaHenryAbstract), ex);
						ConexaoTcp.Close();
					}
				}
			})
			{
				IsBackground = true
			};

			ThreadMonitoramento.Start();
		}

		private void Conecte()
		{
			LogAuditoria.Escreva($"Iniciando a Conexao.", nameof(CatracaHenryAbstract));

			EstadoDeConexaoDaCatraca = EnumEstadoDaCatracaHenry.Conectando;
			var contadorControladorDeAuditoriaLog = 0;

			while (true)
			{
				ServicoMonitorAcesso.AdicioneEvento(EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, "Off-line"));

				ServidorMonitor.EnviarMensagem(
					JsonConvert.SerializeObject(EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, "Off-line")));

				try
				{
					var portaCatraca = Convert.ToInt32(Dispositivo.PortaCatraca);

					ConexaoTcp = new TcpClient(Dispositivo.IpCatraca, portaCatraca)
					{
						Client =
						{
							ReceiveTimeout = 3000,
							SendTimeout = 4000
						}
					};

					EstadoDeConexaoDaCatraca = EnumEstadoDaCatracaHenry.Conectado;

					ServicoMonitorAcesso.AdicioneEvento(EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, ""));

					ServidorMonitor.EnviarMensagem(
						JsonConvert.SerializeObject(EventoCatraca.CrieAcessoMudancaEstado(Dispositivo, "")));

					break;
				}
				catch (SocketException)
				{
					contadorControladorDeAuditoriaLog++;

					LogAuditoria.Escreva(nameof(CatracaHenryAbstract),
						$"Falha na Conexão Reiniciando o Processo Tentativa:{contadorControladorDeAuditoriaLog}");

					Conecte();

					break;
				}
				catch (Exception ex)
				{
					AuditoriaLog.EscrevaErro($"Erro na Conexão ", ex);
					Conecte();

					break;
				}
			}
		}

		private void Conectado()
		{
			DateTime tempoUltimaFalha = DateTime.Now;
			var falhas = 0;

			LogAuditoria.Escreva($"{EstadoDeConexaoDaCatraca} com sucesso",
				nameof(CatracaHenryAbstract));

			while (true)
			{
				string dadosDaCatraca;

				try
				{
					LogAuditoria.Escreva("Aguardando uma solicitação da catraca!", 
						nameof(CatracaHenryAbstract));

					dadosDaCatraca = CarregueDadosCatraca();
				}
				catch (Exception ex)
				{
					AuditoriaLog.EscrevaErro(nameof(CatracaHenryAbstract), ex);
					falhas++;

					if (DateTime.Now.Subtract(tempoUltimaFalha)
								.TotalSeconds > 15)
					{
						tempoUltimaFalha = DateTime.Now;
						falhas = 0;
					}

					if (falhas > 5)
					{
						LogAuditoria.Escreva("Tentando Reiniciar conexão!", nameof(CatracaHenryAbstract));

						return;
					}

					continue;
				}

				if (string.IsNullOrEmpty(dadosDaCatraca))
				{
					LogAuditoria.Escreva(nameof(CatracaHenryAbstract),
						$"Falha ao ler dados string vazia");

					return;
				}

				if (!DadosEstaoValidos(dadosDaCatraca))
				{
					LogAuditoria.Escreva(nameof(CatracaHenryAbstract),$"Dados inválidos: {dadosDaCatraca}");

					continue;
				}

				if (EstadoDeConexaoDaCatraca == EnumEstadoDaCatracaHenry.ValidandoAcesso)
				{
					ValideAcesso(dadosDaCatraca);
				}

				if (EstadoDeConexaoDaCatraca == EnumEstadoDaCatracaHenry.GirouCatraca)
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
					int length = ConexaoTcp.GetStream()
										   .Read(bytes, 0, TamanhoBuffer);

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
						throw;
					}

					if (DateTime.Now.Subtract(tempoParaVerificacao)
								.TotalSeconds > 20)
					{
						EnviaMensagemEmBytes($"01+EH+00+{DateTime.Now:dd/MM/yy HH:mm:ss}]00/00/00]00/00/00");

						tempoParaVerificacao = DateTime.Now;
					}

					PingReply ping = new Ping().Send(Dispositivo.IpCatraca, 1000);

					if (ping.Status == IPStatus.Success)
					{
						continue;
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
					LogAuditoria.Escreva($"O tamanho da string está incorreto, Dados: {dadosRecebidos}", 
						nameof(CatracaHenryAbstract));

					return false;
				}

				string[] comandos = dadosRecebidos.Split(']');

				if (comandos.Length < 2)
				{
					LogAuditoria.Escreva($"Não foi possível dividir a String para leitura, Dados: {dadosRecebidos}", 
						nameof(CatracaHenryAbstract));

					return false;
				}

				switch (comandos[0]
					.Substring(6))
				{
					case "REON+000+0" when comandos[1]
						.Length < 2:
						LogAuditoria.Escreva($"O tamanho do Código de Acesso é inválido, Dados: {dadosRecebidos}",
							nameof(CatracaHenryAbstract));

						return false;
					case "REON+000+0":
					{
						_tipoAcesso = TipoAcesso.Indefinido;

						if (comandos[5]
							.Length >= 3)
						{
							if (comandos[5]
								.Substring(0, comandos[5]
									.Length - 2) != "4") {}
							else
							{
								_tipoAcesso = TipoAcesso.Teclado;
							}
						}

						EstadoDeConexaoDaCatraca = EnumEstadoDaCatracaHenry.ValidandoAcesso;

						break;
					}
					case "REON+000+81":
						_sentidoDoGiro = (SentidoGiro)Parse(comandos[3]);
						EstadoDeConexaoDaCatraca = EnumEstadoDaCatracaHenry.GirouCatraca;

						break;
					default:
						EstadoDeConexaoDaCatraca = EnumEstadoDaCatracaHenry.Conectado;

						break;
				}

				return true;

			}
			catch (Exception ex)
			{
				AuditoriaLog.EscrevaErro(nameof(CatracaHenryAbstract), ex);

				return false;
			}
		}

		protected virtual void ValideTemAcessoPorTeclado(Pessoa pessoa, TipoAcesso tipoAcesso) {}

		protected virtual void ValidePessoaEstaAtiva(Pessoa pessoa) {}

		protected virtual void ValideTempoMinimoParaNovoAcesso(Pessoa pessoa) {}

		protected virtual bool ValideAlunoPossuiLiberacao(Aluno aluno) => false;

		protected virtual void ValideAlunoPossuiBloqueio(Aluno aluno) {}

		protected virtual void ValideAlunoPossuiInadimplencia(Aluno aluno) {}

		protected virtual void ValideAlunoFaltaDocumentos(Aluno aluno) {}

		protected virtual void ValideAlunoFaltaMateriais(Aluno aluno) {}

		protected virtual void ValideAlunoPodeSairSozinho(Aluno aluno) {}

		protected virtual void ValideAlunoPossuiOcorrencia(Aluno aluno) {}

		protected virtual void ValideAutorizadoPossuiInadimplencia(AutorizadoBuscarAluno autorizadoBuscarAluno) {}

		protected virtual void ValideAutorizadoFaltaDocumentos(AutorizadoBuscarAluno autorizadoBuscarAluno) {}

		protected virtual void ValideAutorizadoFaltaMateriais(AutorizadoBuscarAluno autorizadoBuscarAluno) {}

		protected virtual void ValideAutorizadoPossuiOcorrencia(AutorizadoBuscarAluno autorizadoBuscarAluno) {}

		protected virtual void ValideResponsavelPossuiBloqueio(Responsavel responsavel) {}

		protected virtual void ValideResponsavelPossuiInadimplencia(Responsavel responsavel) {}

		protected virtual void ValideResponsavelFaltaDocumentos(Responsavel responsavel) {}

		protected virtual void ValideResponsavelFaltaMateriais(Responsavel responsavel) {}

		protected virtual void ValideResponsavelPossuiOcorrencia(Responsavel responsavel) {}

		protected virtual void ValideProfessorPossuiOcorrencia(Professor professor) {}

		protected virtual void ValideColaboradorPossuiOcorrencias(Colaborador colaborador) {}

		protected virtual void ValideDentroDoHorarioDeAcesso(Pessoa pessoa) {}

		protected virtual string MonteMensagemRestricao(Pessoa pessoa) => null;

		private void ValideAcesso(string dadosRecebidos)
		{
			string codigo = dadosRecebidos.Split(']')[1];

			TryParse(codigo, out int codigoRecebido);

			LogAuditoria.Escreva($"Iniciando validação de acesso código: {codigoRecebido}", 
				nameof(CatracaHenryAbstract));

			try
			{
				LogAuditoria.Escreva($"Iniciando a identificação da pessoa.", 
					nameof(CatracaHenryAbstract));

				_pessoa = ObtenhaPessoa(codigoRecebido.ToString());
				_indetificacaoDaPessoa = $"Pessoa:{_pessoa.RecuperaTipo()} Código:{_pessoa.Id} Nome:{_pessoa.Nome}";

				LogAuditoria.Escreva($"Foi indetificada {_indetificacaoDaPessoa}", 
					nameof(CatracaHenryAbstract));
			}
			catch (Exception)
			{
				var pessoa = new Aluno
				{
					Nome = "Não encontrado!"
				};

				var evento = EventoCatraca.CrieAcessoNegado(SentidoGiro.Indefinido, pessoa, Dispositivo);

				ServicoMonitorAcesso.AdicioneEvento(evento);
				ServidorMonitor.EnviarMensagem(JsonConvert.SerializeObject(evento));

				EnviaMensagemEmBytes("01+REON+00+30]10]" + pessoa.Nome + "}Acesso Negado!]");

				LogAuditoria.Escreva($"Acesso Negado, Motivo:{pessoa.Nome}!",
					nameof(CatracaHenryAbstract));

				return;
			}

			try
			{
				AtualizeConfiguracoes();

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

				string msgRestricao = MonteMensagemRestricao(_pessoa);

				EventoCatraca evento = !string.IsNullOrEmpty(msgRestricao) ?
					EventoCatraca.CrieAcessoLiberadoComRestricao(SentidoGiro.Indefinido, _pessoa, Dispositivo,
						msgRestricao) :
					EventoCatraca.CrieAcessoLiberado(SentidoGiro.Indefinido, _pessoa, Dispositivo);

				ServicoMonitorAcesso.AdicioneEvento(evento);
				ServidorMonitor.EnviarMensagem(JsonConvert.SerializeObject(evento));

				EnviaMensagemEmBytes("01+REON+00+1]10]" + _pessoa.Nome + "}Acesso Liberado!]");

				LogAuditoria.Escreva($"Acesso Liberado.", nameof(CatracaHenryAbstract));

			}
			catch (AcessoNegadoException ex)
			{
				var evento = EventoCatraca.CrieAcessoNegado(SentidoGiro.Indefinido, _pessoa, Dispositivo);
				evento.Mensagem2 = ex.Message;

				ServicoMonitorAcesso.AdicioneEvento(evento);
				ServidorMonitor.EnviarMensagem(JsonConvert.SerializeObject(evento));

				EnviaMensagemEmBytes("01+REON+00+30]10]" + _pessoa.Nome + "}Acesso Negado!]");

				AuditoriaLog.EscrevaErro(nameof(CatracaHenryAbstract), ex);

				_pessoa = null;
			}
		}

		private void RegisteAcesso()
		{
			LogAuditoria.Escreva("Registrando o Acesso", nameof(CatracaHenryAbstract));

			try
			{
				if (_pessoa != null)
				{
					RegistreAcessoPessoa(_pessoa, _sentidoDoGiro);
					LogAuditoria.Escreva($"{_indetificacaoDaPessoa} Sentido do Giro da Dispositivo:{_sentidoDoGiro}",
						nameof(CatracaHenryAbstract));
				}

				_pessoa = null;
			}
			catch (Exception ex)
			{
				AuditoriaLog.EscrevaErro(nameof(CatracaHenryAbstract),ex);
			}
		}

		public void PareCatraca()
		{
			ConexaoTcp?.Close();
			ConexaoTcp?.Dispose();

			ThreadPing?.Abort();

			ThreadMonitoramento.Abort();

			LogAuditoria.Escreva($"Dispositivo: {Dispositivo.Codigo} - Parando", 
				nameof(CatracaHenryAbstract));
		}

		private void EnviaMensagemEmBytes(string mensagem)
		{
			try
			{
				NetworkStream stream = ConexaoTcp.GetStream();
				byte[] byteMensagem = MontaProtocolo(mensagem);
				stream.Write(byteMensagem, 0, byteMensagem.Length);
			}
			catch (Exception ex)
			{
				AuditoriaLog.EscrevaErro(nameof(CatracaHenryAbstract), ex);
			}
		}

		private static byte[] MontaProtocolo(string mensagem)
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
			var chkSumByte = 0x00;

			chkSumByte ^= (byte)(bytes.Count % 256);
			chkSumByte ^= (byte)(bytes.Count / 256);

			var h16 = Parse((Math.Floor((decimal)chkSumByte / 16)).ToString())
				.ToString("X");

			var h1 = Parse((chkSumByte % 16).ToString())
				.ToString("X");

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
	}
}