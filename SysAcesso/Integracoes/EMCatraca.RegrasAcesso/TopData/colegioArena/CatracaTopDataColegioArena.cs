using EasyInnerSDK;
using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Dtos;
using EMCatraca.Core.Logs;
using EMCatraca.Core.Services;
using EMCatraca.Server;
using EMCatraca.Server.Excecoes;
using EMCatraca.Server.Interfaces;
using EMCatraca.TopData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static EMCatraca.TopData.EnumeradoresDllInner;

namespace EMCatraca.RegrasAcesso.TopData.ColegioArena
{
	public class CatracaTopDataColegioArena : CatracaTopDataAbstract
	{
		private readonly IRepositorioAluno _repositorioAluno = FabricaDeRepositorios.Instancia.CrieRepositorioAluno();

		private readonly IRepositorioProfessor _repositorioProfessor =
			FabricaDeRepositorios.Instancia.CrieRepositorioProfessor();

		private readonly IRepositorioColaborador _repositorioColaborador =
			FabricaDeRepositorios.Instancia.CrieRepositorioColaborador();

		private readonly IRepositorioResponsavel _repositorioResponsavel =
			FabricaDeRepositorios.Instancia.CrieRepositorioResponsavel();

		private readonly IRepositorioAutorizadoBuscarAluno _repositorioAutorizadoBuscarAluno =
			FabricaDeRepositorios.Instancia.CrieRepositorioAutorizadoBuscarAluno();

		private readonly IRepositorioAuditoria _repositorioAuditoria =
			FabricaDeRepositorios.Instancia.CrieRepositorioAuditoria();

		private readonly IRepositorioOcorrencias _repositorioOcorrencia =
			FabricaDeRepositorios.Instancia.CrieRepositorioOcorrencias();

		private readonly IRepositorioDeAcessoPessoa _repositorioAcesso =
			FabricaDeRepositorios.Instancia.CrieRepositorioAcesso();

		private TipoPessoa _tipoPessoaAutorizou;
		private int _codigoPessoaAutorizou = 0;

		private DateTime _tempoInicialAutorizouAlunoSairSozinho;

		public CatracaTopDataColegioArena(Dispositivo catraca, IServicoMonitorAcesso servicoMonitorAcesso)
			: base(catraca, servicoMonitorAcesso) { }

		protected override Pessoa ObtenhaPessoa(string codigo)
		{
			if (codigo.Length < 2)
			{
				throw new AcessoNegadoException($"Número de dígitos inválido: \"{codigo}\"");
			}

			string codigoTipoDePessoa = codigo?.Substring(0, 1);

			if (codigoTipoDePessoa == null)
			{
				throw new AcessoNegadoException(
					$"Não foi possível recuperar o Código de Acesso informado: \"{codigo}\"");
			}

			if (!int.TryParse(codigoTipoDePessoa, out int codigoTipoPessoa))
			{
				throw new AcessoNegadoException(
					$"Não foi possível converter o Identificador de Tipo \"{codigo.Substring(1)}\"");
			}

			TipoPessoa tipoPessoa = Enumeradores.ObtenhaTipoPessoaPadrao(Convert.ToInt32(codigoTipoDePessoa));

			if (TipoPessoa.NaoEncontrada.Equals(tipoPessoa))
			{
				throw new AcessoNegadoException($"Tipo de pessoa inválida no Código de Acesso: \"{codigo}\"");
			}

			if (!int.TryParse(codigo.Substring(1), out int codigoAcesso))
			{
				throw new AcessoNegadoException(
					$"Não foi possível converter o Código de Acesso \"{codigo.Substring(1)}\"");
			}

			_tipoPessoaAutorizou = tipoPessoa;

			switch (tipoPessoa)
			{
				case TipoPessoa.Aluno:

					return _repositorioAluno.ConsulteAluno(codigoAcesso);

				case TipoPessoa.AutorizadoBuscarAluno:

					_codigoPessoaAutorizou = codigoAcesso;

					return _repositorioAutorizadoBuscarAluno.ConsulteAutorizadoBuscarAluno(codigoAcesso);

				case TipoPessoa.Profissional:

					if (ConfiguracaoAcesso.ColaboradoresPodeLiberarAcessoAluno.Split(',')
										  .Any(x => x == codigoAcesso.ToString()))
					{
						_codigoPessoaAutorizou = codigoAcesso;
					}

					return _repositorioColaborador.ConsulteColaborador(codigoAcesso);

				case TipoPessoa.Professor:

					if (ConfiguracaoAcesso.ProfessoresPodemLiberarAcessoAluno.Split(',')
										  .Any(x => x == codigoAcesso.ToString()))
					{
						_codigoPessoaAutorizou = codigoAcesso;
					}

					return _repositorioProfessor.ConsulteProfessor(codigoAcesso);

				case TipoPessoa.Responsavel:

					_codigoPessoaAutorizou = codigoAcesso;

					return _repositorioResponsavel.ConsulteResponsavel(codigoAcesso);

				default:

					throw new AcessoNegadoException("Tipo Pessoa não cadastrado!");
			}
		}

		protected override void ValidePessoaEstaAtiva(Pessoa pessoa)
		{
			switch (pessoa)
			{
				case Aluno aluno:
					{
						if (!_repositorioAluno.AlunoEstaAtivo(aluno.Id)
							&& ConfiguracaoAcesso.BloquearAcessoAlunoSemMatricula == "SIM")
						{
							throw new AcessoNegadoException("Encaminhe-se à Secretaria!");
						}

						break;
					}
				case AutorizadoBuscarAluno autorizadoBuscarAluno:
					{
						if (!_repositorioAutorizadoBuscarAluno.AutorizadoBuscarAlunoEstaAtivo(autorizadoBuscarAluno.Id)
							&& ConfiguracaoAcesso.BloquearAcessoAutorizadoSemMatricula == "SIM")
						{
							throw new AcessoNegadoException("Encaminhe-se à Secretaria!");
						}

						break;
					}
				case Colaborador colaborador:
					{
						if (!_repositorioColaborador.ColaboradorEstaAtivo(colaborador.Id)
							&& ConfiguracaoAcesso.BloquearAcessoColaboradorInativo == "SIM")
						{
							throw new AcessoNegadoException("Encaminhe-se à Administração!");
						}

						break;
					}
				case Professor professor:
					{
						if (!_repositorioProfessor.ProfessorEstaAtivo(professor.Id)
							&& ConfiguracaoAcesso.BloquearAcessoProfessorInativo == "SIM")
						{
							throw new AcessoNegadoException("Encaminhe-se à Coordenação!");
						}

						break;
					}
				case Responsavel responsavel:
					{
						if (!_repositorioResponsavel.ResponsavelEstaAtivo(responsavel.Id)
							&& ConfiguracaoAcesso.BloquearAcessoResponsavelSemMatricula == "SIM")
						{
							throw new AcessoNegadoException("Encaminhe-se à Coordenação!");
						}

						break;
					}
				default:
					throw new AcessoNegadoException("Tipo de pessoa não foi identificado");
			}
		}

		protected override void ValideAlunoPossuiInadimplencia(Aluno aluno)
		{
			if (ConfiguracaoAcesso.BloquearAcessoAlunoInadimplente != "SIM")
			{
				return;
			}

			bool inadimplente = _repositorioAluno.AlunoEstaInadimplenteDuplicata(aluno.Id);
			inadimplente = inadimplente || _repositorioAluno.AlunoEstaInadimplenteDeCheques(aluno.Id);

			if (inadimplente)
			{
				throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
			}
		}

		protected override void ValideAlunoFaltaDocumentos(Aluno aluno)
		{
			if (ConfiguracaoAcesso.BloquearAcessoAlunoComPendenciaDocumentos == "SIM"
				&& _repositorioAluno.AlunoEstaPendenteDeDocumentos(aluno.Id))
			{
				throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
			}
		}

		protected override void ValideAlunoFaltaMateriais(Aluno aluno)
		{
			if (ConfiguracaoAcesso.BloquearAcessoAlunoComPendenciaMaterial == "SIM"
				&& _repositorioAluno.AlunoEstaPendenteDeMateriais(aluno.Id))
			{
				throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
			}
		}

		protected override bool ValideAlunoPossuiLiberacao(Aluno aluno)
		{
			if (ConfiguracaoAcesso.FormularioLiberaAcessoAluno != "SIM")
			{
				return false;
			}

			DateTime dataAtual = DateTime.Now;

			List<Liberacao> liberacoes =
				MapeadorArquivoJson.CarreguerArquivoJson<List<Liberacao>>("emcatraca.liberacao.cfg");

			return liberacoes.Any(l =>
				l.Aluno.Id == aluno.Id && l.Acessou == false && (dataAtual - l.DataHoraLiberou).TotalSeconds
				< (double)(60 * l.TempoParaAcessso));
		}

		protected override void ValideAlunoPossuiBloqueio(Aluno aluno)
		{
			if (_repositorioAluno.AlunoEstaBloqueado(aluno.Id, ConfiguracaoAcesso.AtributoBloqueado))
			{
				throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
			}
		}

		protected override void ValideAlunoPodeSairSozinho(Aluno aluno)
		{
			int tempoMinimo = ConfiguracaoAcesso.TempoParaAcessoLiberadoSegundos;

			if (_tempoInicialAutorizouAlunoSairSozinho.GetHashCode() == 0 || DateTime.Now
				.Subtract(_tempoInicialAutorizouAlunoSairSozinho)
				.TotalSeconds > tempoMinimo)
			{
				_codigoPessoaAutorizou = 0;
			}

			if (!_repositorioAluno.AlunoPodeSairSozinho(aluno.Id, ConfiguracaoAcesso.AtributoPodeSairSozinho)
				&& !EhAutorizadoAcessoForaDoHorarioAcesso(aluno))
			{
				throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
			}
		}

		protected override void ValideAlunoPossuiOcorrencia(Aluno aluno)
		{
			if (_repositorioOcorrencia.ExisteOcorrencias(aluno.Id, (int)aluno.RecuperaTipo(),
				ConfiguracaoAcesso.BloquearAcessoAlunoComOcorrencias))
			{
				throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
			}
		}

		protected override void ValideProfessorPossuiOcorrencia(Professor professor)
		{
			if (_repositorioOcorrencia.ExisteOcorrencias(professor.Id, (int)professor.RecuperaTipo(),
				ConfiguracaoAcesso.BloquearAcessoProfessorComOcorrencias))
			{
				throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
			}
		}

		protected override void ValideColaboradorPossuiOcorrencias(Colaborador colaborador)
		{
			if (_repositorioOcorrencia.ExisteOcorrencias(colaborador.Id, (int)colaborador.RecuperaTipo(),
				ConfiguracaoAcesso.BloquearAcessoProfessorComOcorrencias))
			{
				throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
			}
		}

		protected override void ValideResponsavelPossuiBloqueio(Responsavel responsavel)
		{
			if (_repositorioResponsavel.ResponsavelEstaBloqueado(responsavel.Id))
			{
				throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
			}
		}

		protected override void ValideTemAcessoPorTeclado(Pessoa pessoa, TipoAcesso tipoAcesso)
		{
			if (tipoAcesso != TipoAcesso.Teclado)
			{
				return;
			}

			var selecionados = "";

			switch (pessoa)
			{
				case Aluno _:
					selecionados = ConfiguracaoAcesso.AlunosPodemDigitar;

					break;
				case Professor _:
					selecionados = ConfiguracaoAcesso.ProfessoresPodemDigitar;

					break;
				case Colaborador _:
					selecionados = ConfiguracaoAcesso.ProfissionaisPodemDigitar;

					break;
				case Responsavel _:
					selecionados = ConfiguracaoAcesso.ResponsaveisPodemDigitar;

					break;
				case AutorizadoBuscarAluno _:
					selecionados = ConfiguracaoAcesso.AutorizadosPodemDigitar;

					break;
			}

			var listaSelecionados = selecionados.Split(',')
												.ToList();

			if (listaSelecionados.All(x => x != pessoa.Id.ToString()))
			{
				throw new AcessoNegadoException($"Encaminhe-se a Secretaria!");
			}
		}

		protected override void ValideTempoMinimoParaNovoAcesso(Pessoa pessoa)
		{
			var tempoMinimo = 0;

			if (pessoa is Aluno)
			{
				tempoMinimo = ConfiguracaoAcesso.TempoMinimoParaNovoAcessoSegundos;
			}

			TimeSpan tempoDecorridoUltimoAcesso =
				DateTime.Now.Subtract(
					_repositorioAcesso.ObtenhaUltimoAcessoDaPessoa(pessoa.Id, (int)pessoa.RecuperaTipo()));

			if (tempoDecorridoUltimoAcesso.TotalSeconds < tempoMinimo)
			{
				throw new AcessoNegadoException(
					$"Aguarde {tempoMinimo - (int)tempoDecorridoUltimoAcesso.TotalSeconds} segundos!");
			}
		}

		protected override void ValideDentroDoHorarioDeAcesso(Pessoa pessoa)
		{
			if (!ControleDeHorarios(pessoa,
				_repositorioAcesso.ObtenhaUltimoAcessoDaPessoa(pessoa.Id, (int)pessoa.RecuperaTipo())))
			{
				throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
			}
		}

		protected override string MonteMensagemRestricao(Pessoa pessoa)
		{
			bool temRestricao;

			switch (pessoa)
			{
				case Aluno aluno:
					{
						temRestricao = _repositorioOcorrencia.ExisteOcorrencias(pessoa.Id, (int)pessoa.RecuperaTipo(),
							ConfiguracaoAcesso.MensagemAlunoComOcorrencias);

						temRestricao = temRestricao || (_repositorioAluno.AlunoEstaInadimplenteDuplicata(aluno.Id)
														&& ConfiguracaoAcesso.MensagemAlunoComInadimplenciaDeDuplicatas
														== "SIM");

						temRestricao = temRestricao || (_repositorioAluno.AlunoEstaInadimplenteDeCheques(aluno.Id)
														&& ConfiguracaoAcesso.MensagemAlunoComInadimplenciaDeCheques
														== "SIM");

						temRestricao = temRestricao || (_repositorioAluno.AlunoEstaPendenteDeDocumentos(aluno.Id)
														&& ConfiguracaoAcesso.MensagemAlunoComPendenciaDeDocumentos
														== "SIM");

						temRestricao = temRestricao || (_repositorioAluno.AlunoEstaPendenteDeMateriais(aluno.Id)
														&& ConfiguracaoAcesso.MensagemAlunoComPendenciaDeMateriais
														== "SIM");

						if (temRestricao)
						{
							return "Encaminhe-se a Secretaria!";
						}

						break;
					}
				case Responsavel _:
					{
						temRestricao = _repositorioOcorrencia.ExisteOcorrencias(pessoa.Id, (int)pessoa.RecuperaTipo(),
							ConfiguracaoAcesso.MensagemProfessorComOcorrencias);

						if (temRestricao)
						{
							return "Encaminhe-se a Coordenacao!";
						}

						break;
					}
			}

			if (!(pessoa is Colaborador))
			{
				return "";
			}

			temRestricao = _repositorioOcorrencia.ExisteOcorrencias(pessoa.Id, (int)pessoa.RecuperaTipo(),
				ConfiguracaoAcesso.MensagemColaboradorComOcorrencias);

			return temRestricao ?
				"Encaminhe-se a Direcao!" :
				"";

		}

		protected override void RegistreAcessoPessoa(Pessoa pessoa, SentidoGiro sentidoDoGiro)
		{
			try
			{
				if (pessoa is Aluno && ConfiguracaoAcesso.FormularioLiberaAcessoAluno == "SIM")
				{
					List<Liberacao> liberacoes =
						MapeadorArquivoJson.CarreguerArquivoJson<List<Liberacao>>("emcatraca.liberacao.cfg");

					if (liberacoes.Any())
					{
						Liberacao liberacao = liberacoes.First(l => l.Aluno.Id == pessoa.Id);

						if (liberacao != null)
						{
							liberacao.Acessou = true;
							liberacao.DataHoraAcessou = DateTime.Now;
							MapeadorArquivoJson.Gravar<List<Liberacao>>("emcatraca.liberacao.cfg", liberacoes);
						}
					}
				}

				_repositorioAcesso.RegistreAcesso(new RegistroAcesso
				{
					IdPessoa = pessoa.Id,
					TipoPessoaAutorizou = (int)_tipoPessoaAutorizou,
					CodigoPessoaAutorizou = _codigoPessoaAutorizou,
					SentidoDoGiro = (int)sentidoDoGiro
				});

				_repositorioAuditoria.RegistreAuditoriaDeAcesso(new AuditoriaAcesso
				{
					IdPessoa = pessoa.Id,
					NomePessoa = pessoa.Nome,
					TipoPessoa = (int)_tipoPessoaAutorizou,
					SentidoDoGiro = (int)sentidoDoGiro
				});

				if (pessoa is Aluno)
				{
					_codigoPessoaAutorizou = 0;
				}
				else
				{
					_tempoInicialAutorizouAlunoSairSozinho = DateTime.Now;
				}
			}
			catch (Exception ex)
			{
				AuditoriaLog.EscrevaErro(nameof(CatracaTopDataAbstract), ex);
			}
		}

		private bool ControleDeHorarios(Pessoa pessoa, DateTime ultimoAcesso)
		{
			if (!(pessoa is Aluno))
			{
				return true;
			}

			if (ConfiguracaoAcesso.IntervalosAcesso == null)
			{
				return true;
			}


			var intervalosAgrupado = from intervalo in ConfiguracaoAcesso.IntervalosAcesso
									 group intervalo by intervalo.NumeroDia into semana
									 orderby semana.Key ascending
									 select semana.ToList();

			var horarios = new List<DtoHorario>();
			foreach (DtoIntervalo item in intervalosAgrupado.SelectMany(intervalos => intervalos))
			{
				if (item.NumeroDia == (int)DateTime.Now.DayOfWeek)
				{
					var horario = new DtoHorario
					{
						HoraInicial = item.HoraInicial,
						HoraFinal = item.HoraFinal,
						Tipo = item.TipoAcesso
					};

					horarios.Add(horario);
				};
			}

			return horarios == null || (from horario in horarios
										let tempo1 = TimeSpan.Parse(horario.HoraInicial)
										let tempo2 = TimeSpan.Parse(horario.HoraFinal + ":59")
										let data1 = DateTime.Today.Add(tempo1)
										let data2 = DateTime.Today.Add(tempo2)
										let result1 = DateTime.Compare(DateTime.Now, data1)
										let result2 = DateTime.Compare(DateTime.Now, data2)
										let acesso1 = DateTime.Compare(ultimoAcesso, data1)
										let acesso2 = DateTime.Compare(ultimoAcesso, data2)
										where result1 >= 0 && result2 <= 0
										select ConfiguracaoAcesso.IntervalosParaAcessoUnico != "SIM"
											   || (acesso1 < 0 || acesso2 > 0)).FirstOrDefault();

		}

		private bool EhAutorizadoAcessoForaDoHorarioAcesso(Aluno aluno)
		{
			switch (_tipoPessoaAutorizou)
			{
				case TipoPessoa.Responsavel:
					{
						if (!_repositorioAluno.AlunoPodeSerLiberadoPeloResponsavel(aluno.Id, _codigoPessoaAutorizou)
							&& ConfiguracaoAcesso.ResponsaveisPodemLiberarAcessoAluno == "SIM")
						{
							return false;
						}

						break;
					}
				case TipoPessoa.AutorizadoBuscarAluno:
					{
						if (!_repositorioAluno.AlunoPodeSerLiberadoPeloAutorizado(aluno.Id, _codigoPessoaAutorizou)
							&& ConfiguracaoAcesso.AutorizadosPodemLiberamAcessoAluno == "SIM")
						{
							return false;
						}

						break;
					}
				case TipoPessoa.Professor:
					{
						if (ConfiguracaoAcesso.ProfessoresPodemLiberarAcessoAluno.Split(',')
											  .All(x => x != _codigoPessoaAutorizou.ToString()))
						{
							return false;
						}

						break;
					}
				case TipoPessoa.Profissional:
					{
						if (ConfiguracaoAcesso.ColaboradoresPodeLiberarAcessoAluno.Split(',')
											  .All(x => x != _codigoPessoaAutorizou.ToString()))
						{
							return false;
						}

						break;
					}
				default:
					return false;
			}

			return true;
		}

		protected override void MonteCFGOffLineEhOnLine()
		{
			EasyInner.DefinirPadraoCartao((byte)PadraoCartao.PadraoLivre);

			const byte tempoAcionamento1 = (byte)5;
			EasyInner.ConfigurarAcionamento1((byte)FuncaoAcionamento.AcionaRegistroEntradaOuSaida, tempoAcionamento1);

			const byte tempoAcionamento2 = (byte)0;
			EasyInner.ConfigurarAcionamento2((byte)FuncaoAcionamento.NaoUtilizado, tempoAcionamento2);

			EasyInner.ConfigurarLeitor1((byte)Operacao.EntradaEhSaida);
			EasyInner.ConfigurarLeitor2((byte)Operacao.Desativado);
			EasyInner.ConfigurarTipoLeitor((byte)TipoLeitor.CodigoDeBarras);
			EasyInner.DefinirQuantidadeDigitosCartao((byte)10);
			EasyInner.HabilitarTeclado((byte)Opcao.Sim, (byte)Opcao.Nao);
			EasyInner.RegistrarAcessoNegado((byte)Opcao.Nao);
			EasyInner.DefinirFuncaoDefaultLeitoresProximidade(12);
			EasyInner.DefinirFuncaoDefaultSensorBiometria(12);
			EasyInner.SetarBioVariavel(1);
			EasyInner.ConfigurarBioVariavel(1);
			EasyInner.ReceberDataHoraDadosOnLine((byte)(Opcao.Sim));
		}

		protected override void AjustaMundancaOnLine()
		{
			EasyInner.HabilitarMudancaOnLineOffLine(1, 10);
			EasyInner.DefinirConfiguracaoTecladoOnLine(10, 0, 5, 17);
			EasyInner.DefinirEntradasMudancaOnLine((byte)Convert.ToInt32("101100101", 2)); // biometria
			EasyInner.DefinirEntradasMudancaOffLine((byte)Opcao.Nao, 3, (byte)Opcao.Nao, (byte)Opcao.Nao);

			EasyInner.DefinirMensagemPadraoMudancaOffLine(1, "INDENTIFIQUE-SE.");
			EasyInner.DefinirMensagemPadraoMudancaOnLine(1, "INDENTIFIQUE-SE!");

			var tentativas = 0;

			while (EasyInner.EnviarConfiguracoesMudancaAutomaticaOnLineOffLine(Dispositivo.Codigo) != 0)
			{
				if (tentativas > 5)
				{
					LogAuditoria.Escreva($"Falha em EasyInner.EnviarConfiguracoesMudancaAutomaticaOnLineOffLine",
						nameof(CatracaTopDataColegioArena));

					return;
				}

				Thread.Sleep(100);
				tentativas++;
			}
		}

		protected override void ConfiguraFormasDeEntradaOnline()
		{
			LogAuditoria.Escreva("Enviar Formas Entrada OnLine",nameof(CatracaTopDataColegioArena));

			var tentativas = 0;
			const int echoDoTecladoNoDisplay = 1;
			const int tempoDeEsperaDoTeclado = 15;
			const int posicaoDoCursorNoTeclado = 17;

			byte retornoEnviarFormasEntradasOnLine = EasyInner.EnviarFormasEntradasOnLine(Dispositivo.Codigo, (byte)10,
				(byte)echoDoTecladoNoDisplay, (byte)Convert.ToInt32("101100101", 2), (byte)tempoDeEsperaDoTeclado,
				(byte)posicaoDoCursorNoTeclado);

			while (retornoEnviarFormasEntradasOnLine != 0)
			{
				if (tentativas > 5)
				{
					LogAuditoria.Escreva($"Erro: Estouro de tentativas de resposta", 
						nameof(CatracaTopDataColegioArena));

					return;
				}

				Thread.Sleep(100);
				tentativas++;
			}

			EasyInner.EnviarMensagemPadraoOnLine(Dispositivo.Codigo, 1, "INDENTIFIQUE-SE!");
		}
	}
}