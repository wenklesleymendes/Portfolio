﻿using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Dtos;
using EMCatraca.Core.Logs;
using EMCatraca.Neokoros;
using EMCatraca.Server;
using EMCatraca.Server.Excecoes;
using EMCatraca.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMCatraca.RegrasAcesso.Neokoros.Colegio.IPE
{
	internal class CatracaNeokorosIPE: CatracaNeokorosAbstract
	{
		private readonly IRepositorioAluno _repositorioAluno = FabricaDeRepositorios.Instancia.CrieRepositorioAluno();
		private readonly IRepositorioProfessor _repositorioProfessor = FabricaDeRepositorios.Instancia.CrieRepositorioProfessor();
		private readonly IRepositorioColaborador _repositorioColaborador = FabricaDeRepositorios.Instancia.CrieRepositorioColaborador();
		private readonly IRepositorioResponsavel _repositorioResponsavel = FabricaDeRepositorios.Instancia.CrieRepositorioResponsavel();
		private readonly IRepositorioAutorizadoBuscarAluno _repositorioAutorizadoBuscarAluno = FabricaDeRepositorios.Instancia.CrieRepositorioAutorizadoBuscarAluno();
		private readonly IRepositorioAuditoria _repositorioAuditoria = FabricaDeRepositorios.Instancia.CrieRepositorioAuditoria();
		private readonly IRepositorioOcorrencias _repositorioOcorrencia = FabricaDeRepositorios.Instancia.CrieRepositorioOcorrencias();
		private readonly IRepositorioDeAcessoPessoa _repositorioAcesso = FabricaDeRepositorios.Instancia.CrieRepositorioAcesso();

		private TipoPessoa _tipoPessoaAutorizou;
		private int _codigoPessoaAutorizou = 0;

		private DateTime tempoInicialAutorizouAlunoSairSozinho;

		#region Menssagen Padrao negar acesso e para onde deve ser encaminha.

		const string messagenNegaAcessoEncaminhaSecretaria = "Encaminhe-se a Secretaria!";
		const string messagenNegarAcessoEncaminhaCordenacao = "Encaminhe-se a Coordenação!";
		const string messagenNegarAcessoEncaminhaDirecao = "Encaminhe-se a Direcao!";

		#endregion

		public CatracaNeokorosIPE(Dispositivo Catraca) : base(Catraca)
		{

		}

		protected override string ObtenhaMatriculaPorRFID(string codigoRFID)
		{
			LogAuditoria.Escreva($"{nameof(ObtenhaMatriculaPorRFID)}: " +
				   $"{nameof(codigoRFID)}={codigoRFID}",
				   $"{nameof(CatracaNeokorosIPE)}");

			return "1" + _repositorioAluno.ObtenhaMatriculaPorRFID(codigoRFID);

		}

		protected override Pessoa ObtenhaPessoa(string codigo)
		{
			codigo = codigo.Split('#')[0];

			if (codigo.Length < 2)
			{
				throw new AcessoNegadoException($"Número de dígitos inválido: \"{codigo}\"");
			}

			var codigoTipoDePessoa = codigo?.Substring(0, 1);
			if (codigoTipoDePessoa == null)
			{
				throw new AcessoNegadoException($"Não foi possível recuperar o Código de Acesso informado: \"{codigo}\"");
			}

			if (!Int32.TryParse(codigoTipoDePessoa, out int codigoTipoPessoa))
			{
				throw new AcessoNegadoException($"Não foi possível converter o Identificador de Tipo \"{codigo.Substring(1)}\"");
			}

			var existeCustomizacaoTipoPessoa = Equals(_config.RegrasAcesso.ExisteConfiguracoesCustomizadaTipoPessoa, "SIM");

			TipoPessoa tipoPessoa = existeCustomizacaoTipoPessoa
									 ? ObtenhaTipoPessoaCustomizado(codigoTipoDePessoa)
									 : Enumeradores.ObtenhaTipoPessoaPadrao(Convert.ToInt32(codigoTipoDePessoa));
						
			if (TipoPessoa.NaoEncontrada.Equals(tipoPessoa))
			{
				throw new AcessoNegadoException($"Tipo de pessoa inválida no Código de Acesso: \"{codigo}\"");
			}

			if (!Int32.TryParse(codigo.Substring(1), out int codigoAcesso))
			{
				throw new AcessoNegadoException($"Não foi possível converter o Código de Acesso \"{codigo.Substring(1)}\"");
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

					if (_config.RegrasAcesso.ColaboradoresPodeLiberarAcessoAluno.Split(',').Any(x => x == codigoAcesso.ToString()))
					{
						_codigoPessoaAutorizou = codigoAcesso;
					}
					return _repositorioColaborador.ConsulteColaborador(codigoAcesso);

				case TipoPessoa.Professor:

					if (_config.RegrasAcesso.ProfessoresPodemLiberarAcessoAluno.Split(',').Any(x => x == codigoAcesso.ToString()))
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

		private TipoPessoa ObtenhaTipoPessoaCustomizado(string codigoTipoPessoa)
		{
			var tipoPessoaConvertido = Convert.ToInt32(codigoTipoPessoa);

			return tipoPessoaConvertido == _config.CutomizacaoTipoPessoa.Aluno
					? TipoPessoa.Aluno
					: tipoPessoaConvertido == _config.CutomizacaoTipoPessoa.Responsavel
					 ? TipoPessoa.Responsavel
					 : tipoPessoaConvertido == _config.CutomizacaoTipoPessoa.AutorizadoBuscarAluno
					   ? TipoPessoa.AutorizadoBuscarAluno
					   : tipoPessoaConvertido == _config.CutomizacaoTipoPessoa.Professor
						 ? TipoPessoa.Professor
						 : tipoPessoaConvertido == _config.CutomizacaoTipoPessoa.Profissional
						   ? TipoPessoa.Profissional
						   : TipoPessoa.NaoEncontrada;
		}

		protected override void ValidePessoaEstaAtiva(Pessoa pessoa)
		{
			if (pessoa is Aluno aluno)
			{
				var ehAlunoAtivo = _repositorioAluno.AlunoEstaAtivo(aluno.Id);
				var existeConfiguracaoParaNegarAcesso = _config.RegrasAcesso.BloquearAcessoAlunoSemMatricula == "SIM";

				if (!ehAlunoAtivo && existeConfiguracaoParaNegarAcesso)
				{
					throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
				}
			}
			else if (pessoa is AutorizadoBuscarAluno autorizadoBuscarAluno)
			{
				var existePessoaAtiva = _repositorioAutorizadoBuscarAluno.AutorizadoBuscarAlunoEstaAtivo(autorizadoBuscarAluno.Id);
				var existeConfiguracaoNegarAcesso = _config.RegrasAcesso.BloquearAcessoAutorizadoSemMatricula == "SIM";

				if (!existePessoaAtiva && existeConfiguracaoNegarAcesso)
				{
					throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
				}
			}
			else if (pessoa is Colaborador colaborador)
			{
				var colaboradorEstaAtivo = _repositorioColaborador.ColaboradorEstaAtivo(colaborador.Id);
				var existeConfiguracaoNegarAcesso = _config.RegrasAcesso.BloquearAcessoColaboradorInativo == "SIM";

				if (!colaboradorEstaAtivo && existeConfiguracaoNegarAcesso)
				{
					throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
				}
			}
			else if (pessoa is Professor professor)
			{
				var professorEstaAtivo = _repositorioProfessor.ProfessorEstaAtivo(professor.Id);
				var existeConfiguracaoNegarAcesso = _config.RegrasAcesso.BloquearAcessoProfessorInativo == "SIM";

				if (!professorEstaAtivo && existeConfiguracaoNegarAcesso)
				{
					throw new AcessoNegadoException(messagenNegarAcessoEncaminhaCordenacao);
				}
			}
			else if (pessoa is Responsavel responsavel)
			{
				var responsavelEstaAtivo = _repositorioResponsavel.ResponsavelEstaAtivo(responsavel.Id);
				var existeConfiguracaoNegarAcesso = _config.RegrasAcesso.BloquearAcessoResponsavelSemMatricula == "SIM";

				if (!responsavelEstaAtivo && existeConfiguracaoNegarAcesso)
				{
					throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
				}
			}
			else
			{
				throw new AcessoNegadoException("Tipo de pessoa não foi identificado");
			}
		}

		protected override void ValideAlunoFaltaDocumentos(Aluno aluno)
		{
			if (_config.RegrasAcesso.BloquearAcessoAlunoComPendenciaDocumentos == "SIM")
			{
				if (_repositorioAluno.AlunoEstaPendenteDeDocumentos(aluno.Id))
				{
					throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
				}
			}
		}

		protected override void ValideAlunoFaltaMateriais(Aluno aluno)
		{
			if (_config.RegrasAcesso.BloquearAcessoAlunoComPendenciaMaterial == "SIM")
			{
				if (_repositorioAluno.AlunoEstaPendenteDeMateriais(aluno.Id))
				{
					throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
				}
			}
		}

		protected override bool ValideAlunoPossuiLiberacao(Aluno aluno)
		{
			if (_config.RegrasAcesso.FormularioLiberaAcessoAluno != "SIM")
			{
				return false;
			}

			var dataAtual = DateTime.Now;
			List<Liberacao> liberacoes = MapeadorArquivoJson.CarreguerArquivoJson<List<Liberacao>>("emcatraca.liberacao.cfg");
			return liberacoes.Any(l => l.Aluno.Id == aluno.Id &&
							l.Acessou == false && (dataAtual - l.DataHoraLiberou).TotalSeconds < (double)(60 * l.TempoParaAcessso));
		}

		protected override void ValideAlunoPossuiBloqueio(Aluno aluno)
		{
			var existeBloqueio = _repositorioAluno.AlunoEstaBloqueado(aluno.Id, _config.RegrasAcesso.AtributoBloqueado);
			if (existeBloqueio)
			{
				throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
			}
		}

		protected override void ValideAlunoPodeSairSozinho(Aluno aluno)
		{
			int tempoMinimo = _config.RegrasAcesso.TempoParaAcessoLiberadoSegundos;
			if (tempoInicialAutorizouAlunoSairSozinho.GetHashCode() == 0
				|| DateTime.Now.Subtract(tempoInicialAutorizouAlunoSairSozinho)
				.TotalSeconds > tempoMinimo)
			{
				_codigoPessoaAutorizou = 0;
			}

			var atributoPodeSairSozinho = _config.RegrasAcesso.AtributoPodeSairSozinho;
			if (!_repositorioAluno.AlunoPodeSairSozinho(aluno.Id, atributoPodeSairSozinho))
			{
				if (!EhAutorizadoAcessoForaDoHorarioAcesso(aluno))
				{
					throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
				}
			}
		}

		protected override void ValideAlunoPossuiOcorrencia(Aluno aluno)
		{
			var negarAcessoDeAlunosComOcorrecias = _config.RegrasAcesso.BloquearAcessoAlunoComOcorrencias;
			if (_repositorioOcorrencia.ExisteOcorrencias(aluno.Id, (int)aluno.RecuperaTipo(), negarAcessoDeAlunosComOcorrecias))
			{
				throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
			}
		}

		protected override void ValideProfessorPossuiOcorrencia(Professor professor)
		{
			var negarAcessoProfessor = _config.RegrasAcesso.BloquearAcessoProfessorComOcorrencias;
			if (_repositorioOcorrencia.ExisteOcorrencias(professor.Id, (int)professor.RecuperaTipo(), negarAcessoProfessor))
			{
				throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
			}
		}

		protected override void ValideColaboradorPossuiOcorrencias(Colaborador colaborador)
		{
			var negarAcessoColaborador = _config.RegrasAcesso.BloquearAcessoProfessorComOcorrencias;
			if (_repositorioOcorrencia.ExisteOcorrencias(colaborador.Id, (int)colaborador.RecuperaTipo(), negarAcessoColaborador))
			{
				throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
			}
		}

		protected override void ValideResponsavelPossuiBloqueio(Responsavel responsavel)
		{
			if (_repositorioResponsavel.ResponsavelEstaBloqueado(responsavel.Id))
			{
				throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
			}
		}

		protected override void ValideTemAcessoPorTeclado(Pessoa pessoa, TipoAcesso tipoAcesso)
		{
            _config.RegrasAcesso = MapeadorArquivoJson.CarreguerArquivoJson<Core.Dominio.RegrasAcesso>("emcatraca.acesso.cfg");

			if (tipoAcesso == TipoAcesso.Teclado)
			{
				string selecionados = string.Empty;

				if (pessoa is Aluno)
				{
					selecionados = _config.RegrasAcesso.AlunosPodemDigitar;
				}

				if (pessoa is Professor)
				{
					selecionados = _config.RegrasAcesso.ProfessoresPodemDigitar;
				}

				if (pessoa is Colaborador)
				{
					selecionados = _config.RegrasAcesso.ProfissionaisPodemDigitar;
				}

				if (pessoa is Responsavel)
				{
					selecionados = _config.RegrasAcesso.ResponsaveisPodemDigitar;
				}

				if (pessoa is AutorizadoBuscarAluno)
				{
					selecionados = _config.RegrasAcesso.AutorizadosPodemDigitar;
				}

				var listaSelecionados = selecionados.Split(',').ToList();
				if (!listaSelecionados.Any(x => x == pessoa.Id.ToString()))
				{
					throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
				}
			}
		}

		protected override void ValideTempoMinimoParaNovoAcesso(Pessoa pessoa)
		{
			int tempoMinimo = 0;
			if (pessoa is Aluno)
			{
				tempoMinimo = _config.RegrasAcesso.TempoMinimoParaNovoAcessoSegundos;
			}

			var tempoDecorridoUltimoAcesso = DateTime.Now.Subtract(_repositorioAcesso.ObtenhaUltimoAcessoDaPessoa(pessoa.Id, (int)pessoa.RecuperaTipo()));
			if (tempoDecorridoUltimoAcesso.TotalSeconds < tempoMinimo)
			{
				throw new AcessoNegadoException($"Aguarde {tempoMinimo - (int)tempoDecorridoUltimoAcesso.TotalSeconds} segundos!");
			}
		}

		protected override void ValideDentroDoHorarioDeAcesso(Pessoa pessoa)
		{
			if (!ControleDeHorarios(pessoa, _repositorioAcesso.ObtenhaUltimoAcessoDaPessoa(pessoa.Id, (int)pessoa.RecuperaTipo())))
			{
				throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
			}
		}

		protected override string MonteMensagemRestricao(Pessoa pessoa)
		{
			if (pessoa is Aluno aluno)
			{
				var existeRestricaoParaAluno = _repositorioOcorrencia.ExisteOcorrencias(pessoa.Id, (int)pessoa.RecuperaTipo(), _config.RegrasAcesso.MensagemAlunoComOcorrencias);

				existeRestricaoParaAluno = existeRestricaoParaAluno || (_repositorioAluno.AlunoEstaInadimplenteDuplicata(aluno.Id)
																	&& _config.RegrasAcesso.MensagemAlunoComInadimplenciaDeDuplicatas == "SIM");

				existeRestricaoParaAluno = existeRestricaoParaAluno || (_repositorioAluno.AlunoEstaInadimplenteDeCheques(aluno.Id)
																	&& _config.RegrasAcesso.MensagemAlunoComInadimplenciaDeCheques == "SIM");

				existeRestricaoParaAluno = existeRestricaoParaAluno || (_repositorioAluno.AlunoEstaPendenteDeDocumentos(aluno.Id)
												  && _config.RegrasAcesso.MensagemAlunoComPendenciaDeDocumentos == "SIM");

				existeRestricaoParaAluno = existeRestricaoParaAluno || (_repositorioAluno.AlunoEstaPendenteDeMateriais(aluno.Id)
												  && _config.RegrasAcesso.MensagemAlunoComPendenciaDeMateriais == "SIM");

				if (existeRestricaoParaAluno)
				{
					return messagenNegaAcessoEncaminhaSecretaria;
				}
			}

			if (pessoa is Responsavel)
			{
				var existeRestricaoParaResponsavel = _repositorioOcorrencia.ExisteOcorrencias(pessoa.Id, (int)pessoa.RecuperaTipo(), _config.RegrasAcesso.MensagemProfessorComOcorrencias);
				if (existeRestricaoParaResponsavel)
				{
					return messagenNegarAcessoEncaminhaCordenacao;
				}
			}

			if (pessoa is Colaborador)
			{
				var existeRestricao = _repositorioOcorrencia.ExisteOcorrencias(pessoa.Id, (int)pessoa.RecuperaTipo(), _config.RegrasAcesso.MensagemColaboradorComOcorrencias);
				if (existeRestricao)
				{
					return messagenNegarAcessoEncaminhaDirecao;
				}
			}

			return string.Empty;
		}

		protected override void RegistreAcessoPessoa(Pessoa pessoa, SentidoGiro sentidoDoGiro)
		{
			if (pessoa is Aluno && _config.RegrasAcesso.FormularioLiberaAcessoAluno == "SIM")
			{
				var liberacoes = MapeadorArquivoJson.CarreguerArquivoJson<List<Liberacao>>("emcatraca.liberacao.cfg");
				var liberacao = liberacoes.Find(l => l.Aluno.Id == pessoa.Id);
				if (liberacao != null)
				{
					liberacao.Acessou = true;
					liberacao.DataHoraAcessou = DateTime.Now;
					MapeadorArquivoJson.Gravar<List<Liberacao>>("emcatraca.liberacao.cfg", liberacoes);
				}
			}

			_repositorioAcesso.RegistreAcesso(
				new RegistroAcesso
				{
					IdPessoa = pessoa.Id,
					TipoPessoaAutorizou = (int)_tipoPessoaAutorizou,
					CodigoPessoaAutorizou = _codigoPessoaAutorizou,
					SentidoDoGiro = (int)sentidoDoGiro
				});

			_repositorioAuditoria.RegistreAuditoriaDeAcesso(
				new AuditoriaAcesso
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
				tempoInicialAutorizouAlunoSairSozinho = DateTime.Now;
			}
		}


		private bool ControleDeHorarios(Pessoa pessoa, DateTime ultimoAcesso)
		{
			if (!(pessoa is Aluno))
			{
				return true;
			}

			if (_config.RegrasAcesso.IntervalosAcesso == null)
			{
				return true;
			}

			var intervalosAgrupado = from intervalo in _config.RegrasAcesso.IntervalosAcesso
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

			if (horarios == null)
			{
				return true;
			}

			foreach (DtoHorario horario in horarios)
			{
				var tempo1 = TimeSpan.Parse(horario.HoraInicial);
				var tempo2 = TimeSpan.Parse(horario.HoraFinal + ":59");
				var data1 = DateTime.Today.Add(tempo1);
				var data2 = DateTime.Today.Add(tempo2);
				int result1 = DateTime.Compare(DateTime.Now, data1);
				int result2 = DateTime.Compare(DateTime.Now, data2);
				int acesso1 = DateTime.Compare(ultimoAcesso, data1);
				int acesso2 = DateTime.Compare(ultimoAcesso, data2);

				if (result1 >= 0 && result2 <= 0)
				{
					if (_config.RegrasAcesso.IntervalosParaAcessoUnico == "SIM" && (acesso1 >= 0 && acesso2 <= 0))
					{
						return false;
					}
					else
					{
						return true;
					}
				}
			}

			return false;
		}

		private bool EhAutorizadoAcessoForaDoHorarioAcesso(Aluno aluno)
		{
			if (_tipoPessoaAutorizou == TipoPessoa.Responsavel)
			{
				if (!_repositorioAluno.AlunoPodeSerLiberadoPeloResponsavel(aluno.Id, _codigoPessoaAutorizou) && _config.RegrasAcesso.ResponsaveisPodemLiberarAcessoAluno == "SIM")
				{
					return false;
				}
			}
			else if (_tipoPessoaAutorizou == TipoPessoa.AutorizadoBuscarAluno)
			{
				if (!_repositorioAluno.AlunoPodeSerLiberadoPeloAutorizado(aluno.Id, _codigoPessoaAutorizou) && _config.RegrasAcesso.AutorizadosPodemLiberamAcessoAluno == "SIM")
				{
					return false;
				}
			}
			else if (_tipoPessoaAutorizou == TipoPessoa.Professor)
			{
				if (!_config.RegrasAcesso.ProfessoresPodemLiberarAcessoAluno.Split(',').Any(x => x == _codigoPessoaAutorizou.ToString()))
				{
					return false;
				}
			}
			else if (_tipoPessoaAutorizou == TipoPessoa.Profissional)
			{
				if (!_config.RegrasAcesso.ColaboradoresPodeLiberarAcessoAluno.Split(',').Any(x => x == _codigoPessoaAutorizou.ToString()))
				{
					return false;
				}
			}
			else
			{
				return false;
			}

			return true;
		}

		protected override TurmaMontada ObtenhaTurmaMontada(string codigo) => throw new NotImplementedException();
	}
}
