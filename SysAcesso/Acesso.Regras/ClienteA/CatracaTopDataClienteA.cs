using Acesso.Dominio;
using Acesso.Interfaces;
using Acesso.Interfaces.Excecoes;
using Acesso.TopData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Acesso.RegrasAcesso.delos
{
    public class CatracaTopDataClienteA : CatracaTopDataAbstract
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

        private DateTime _tempoInicialAutorizouAlunoSairSozinho;

        public CatracaTopDataClienteA(Dispositivo catraca, IServicoMonitorAcesso servicoMonitorAcesso) : base(catraca, servicoMonitorAcesso)
        {

        }

        protected override Pessoa ObtenhaPessoa(string codigo)
        {
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

            var tipoPessoa = Enumeradores.ObtenhaTipoPessoa(Convert.ToInt32(codigoTipoDePessoa));
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

                    if (ConfiguracaoAcesso.ColaboradoresPodeLiberarAcessoAluno.Split(',').Any(x => x == codigoAcesso.ToString()))
                    {
                        _codigoPessoaAutorizou = codigoAcesso;
                    }

                    return _repositorioColaborador.ConsulteColaborador(codigoAcesso);

                case TipoPessoa.Professor:

                    if (ConfiguracaoAcesso.ProfessoresPodemLiberarAcessoAluno.Split(',').Any(x => x == codigoAcesso.ToString()))
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
            if (pessoa is Aluno aluno)
            {
                if (!_repositorioAluno.AlunoEstaAtivo(aluno.Id) && ConfiguracaoAcesso.BloquearAcessoAlunoSemMatricula == "SIM")
                {
                    throw new AcessoNegadoException("Encaminhe-se à Secretaria!");
                }
            }
            else if (pessoa is AutorizadoBuscarAluno autorizadoBuscarAluno)
            {
                if (!_repositorioAutorizadoBuscarAluno.AutorizadoBuscarAlunoEstaAtivo(autorizadoBuscarAluno.Id) && ConfiguracaoAcesso.BloquearAcessoAutorizadoSemMatricula == "SIM")
                {
                    throw new AcessoNegadoException("Encaminhe-se à Secretaria!");
                }
            }
            else if (pessoa is Colaborador colaborador)
            {
                if (!_repositorioColaborador.ColaboradorEstaAtivo(colaborador.Id) && ConfiguracaoAcesso.BloquearAcessoColaboradorInativo == "SIM")
                {
                    throw new AcessoNegadoException("Encaminhe-se à Administração!");
                }
            }
            else if (pessoa is Professor professor)
            {
                if (!_repositorioProfessor.ProfessorEstaAtivo(professor.Id) && ConfiguracaoAcesso.BloquearAcessoProfessorInativo == "SIM")
                {
                    throw new AcessoNegadoException("Encaminhe-se à Coordenação!");
                }
            }
            else if (pessoa is Responsavel responsavel)
            {
                if (!_repositorioResponsavel.ResponsavelEstaAtivo(responsavel.Id) && ConfiguracaoAcesso.BloquearAcessoResponsavelSemMatricula == "SIM")
                {
                    throw new AcessoNegadoException("Encaminhe-se à Coordenação!");
                }
            }
            else
            {
                throw new AcessoNegadoException("Tipo de pessoa não foi identificado");
            }
        }

        protected override void ValideAlunoPossuiInadimplencia(Aluno aluno)
        {
            if (ConfiguracaoAcesso.BloquearAcessoAlunoInadimplente == "SIM")
            {
                var inadimplente = _repositorioAluno.AlunoEstaInadimplenteDuplicata(aluno.Id);
                inadimplente = inadimplente || _repositorioAluno.AlunoEstaInadimplenteDeCheques(aluno.Id);

                if (inadimplente)
                {
                    throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
                }
            }
        }

        protected override void ValideAlunoFaltaDocumentos(Aluno aluno)
        {
            if (ConfiguracaoAcesso.BloquearAcessoAlunoComPendenciaDocumentos == "SIM")
            {
                if (_repositorioAluno.AlunoEstaPendenteDeDocumentos(aluno.Id))
                {
                    throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
                }
            }
        }

        protected override void ValideAlunoFaltaMateriais(Aluno aluno)
        {
            if (ConfiguracaoAcesso.BloquearAcessoAlunoComPendenciaMaterial == "SIM")
            {
                if (_repositorioAluno.AlunoEstaPendenteDeMateriais(aluno.Id))
                {
                    throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
                }
            }
        }

        protected override bool ValideAlunoPossuiLiberacao(Aluno aluno)
        {
            if (ConfiguracaoAcesso.FormularioLiberaAcessoAluno != "SIM")
            {
                return false;
            }

            var dataAtual = DateTime.Now;
            List<Liberacao> liberacoes = MapeadorArquivoJson.CarreguerArquivoJson<List<Liberacao>>("Acesso.liberacao.cfg");
            return liberacoes.Any(l => l.Aluno.Id == aluno.Id &&
                            l.Acessou == false &&
                            (dataAtual - l.DataHoraLiberou).TotalSeconds < (double)(60 * l.TempoParaAcessso));
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
            if (_tempoInicialAutorizouAlunoSairSozinho.GetHashCode() == 0 || DateTime.Now.Subtract(_tempoInicialAutorizouAlunoSairSozinho).TotalSeconds > tempoMinimo)
            {
                _codigoPessoaAutorizou = 0;
            }

            if (!_repositorioAluno.AlunoPodeSairSozinho(aluno.Id, ConfiguracaoAcesso.AtributoPodeSairSozinho))
            {
                if (!EhAutorizadoAcessoForaDoHorarioAcesso(aluno))
                    throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
            }
        }

        protected override void ValideAlunoPossuiOcorrencia(Aluno aluno)
        {
            if (_repositorioOcorrencia.ExisteOcorrencias(aluno.Id, (int)aluno.RecuperaTipo(), ConfiguracaoAcesso.BloquearAcessoAlunoComOcorrencias))
            {
                throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
            }
        }

        protected override void ValideProfessorPossuiOcorrencia(Professor professor)
        {
            if (_repositorioOcorrencia.ExisteOcorrencias(professor.Id, (int)professor.RecuperaTipo(), ConfiguracaoAcesso.BloquearAcessoProfessorComOcorrencias))
            {
                throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
            }
        }

        protected override void ValideColaboradorPossuiOcorrencias(Colaborador colaborador)
        {
            if (_repositorioOcorrencia.ExisteOcorrencias(colaborador.Id, (int)colaborador.RecuperaTipo(), ConfiguracaoAcesso.BloquearAcessoProfessorComOcorrencias))
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
            if (tipoAcesso == TipoAcesso.Teclado)
            {
                string selecionados = "";
                if (pessoa is Aluno) selecionados = ConfiguracaoAcesso.AlunosPodemDigitar;
                if (pessoa is Professor) selecionados = ConfiguracaoAcesso.ProfessoresPodemDigitar;
                if (pessoa is Colaborador) selecionados = ConfiguracaoAcesso.ProfissionaisPodemDigitar;
                if (pessoa is Responsavel) selecionados = ConfiguracaoAcesso.ResponsaveisPodemDigitar;
                if (pessoa is AutorizadoBuscarAluno) selecionados = ConfiguracaoAcesso.AutorizadosPodemDigitar;

                var listaSelecionados = selecionados.Split(',').ToList();
                if (!listaSelecionados.Any(x => x == pessoa.Id.ToString()))
                {
                    throw new AcessoNegadoException($"Encaminhe-se a Secretaria!");
                }
            }
        }

        protected override void ValideTempoMinimoParaNovoAcesso(Pessoa pessoa)
        {
            int tempoMinimo = 0;
            if (pessoa is Aluno) tempoMinimo = ConfiguracaoAcesso.TempoMinimoParaNovoAcessoSegundos;

            TimeSpan tempoDecorridoUltimoAcesso = DateTime.Now.Subtract(_repositorioAcesso.ObtenhaUltimoAcessoDaPessoa(pessoa.Id, (int)pessoa.RecuperaTipo()));
            if (tempoDecorridoUltimoAcesso.TotalSeconds < tempoMinimo)
            {
                throw new AcessoNegadoException($"Aguarde {tempoMinimo - (int)tempoDecorridoUltimoAcesso.TotalSeconds} segundos!");
            }
        }

        protected override void ValideDentroDoHorarioDeAcesso(Pessoa pessoa)
        {
            if (!ControleDeHorarios(pessoa, _repositorioAcesso.ObtenhaUltimoAcessoDaPessoa(pessoa.Id, (int)pessoa.RecuperaTipo())))
            {
                throw new AcessoNegadoException("Encaminhe-se a Secretaria!");
            }
        }

        protected override string MonteMensagemRestricao(Pessoa pessoa)
        {
            if (pessoa is Aluno aluno)
            {
                bool temRestricao = _repositorioOcorrencia.ExisteOcorrencias(pessoa.Id, (int)pessoa.RecuperaTipo(), ConfiguracaoAcesso.MensagemAlunoComOcorrencias);
                temRestricao = temRestricao || (_repositorioAluno.AlunoEstaInadimplenteDuplicata(aluno.Id) && ConfiguracaoAcesso.MensagemAlunoComInadimplenciaDeDuplicatas == "SIM");
                temRestricao = temRestricao || (_repositorioAluno.AlunoEstaInadimplenteDeCheques(aluno.Id) && ConfiguracaoAcesso.MensagemAlunoComInadimplenciaDeCheques == "SIM");
                temRestricao = temRestricao || (_repositorioAluno.AlunoEstaPendenteDeDocumentos(aluno.Id) && ConfiguracaoAcesso.MensagemAlunoComPendenciaDeDocumentos == "SIM");
                temRestricao = temRestricao || (_repositorioAluno.AlunoEstaPendenteDeMateriais(aluno.Id) && ConfiguracaoAcesso.MensagemAlunoComPendenciaDeMateriais == "SIM");
                if (temRestricao) return "Encaminhe-se a Secretaria!";
            }

            if (pessoa is Responsavel)
            {
                bool temRestricao = _repositorioOcorrencia.ExisteOcorrencias(pessoa.Id, (int)pessoa.RecuperaTipo(), ConfiguracaoAcesso.MensagemProfessorComOcorrencias);
                if (temRestricao) return "Encaminhe-se a Coordenacao!";
            }

            if (pessoa is Colaborador)
            {
                bool temRestricao = _repositorioOcorrencia.ExisteOcorrencias(pessoa.Id, (int)pessoa.RecuperaTipo(), ConfiguracaoAcesso.MensagemColaboradorComOcorrencias);
                if (temRestricao) return "Encaminhe-se a Direcao!";
            }
            return "";
        }

        protected override void RegistreAcessoPessoa(Pessoa pessoa, SentidoGiro sentidoDoGiro)
        {
            try
            {
                if (pessoa is Aluno && ConfiguracaoAcesso.FormularioLiberaAcessoAluno == "SIM")
                {
                    List<Liberacao> liberacoes = MapeadorArquivoJson.CarreguerArquivoJson<List<Liberacao>>("Acesso.liberacao.cfg");
                    if (liberacoes.Any())
                    {
                        var liberacao = liberacoes.First(l => l.Aluno.Id == pessoa.Id);
                        if (liberacao != null)
                        {
                            liberacao.Acessou = true;
                            liberacao.DataHoraAcessou = DateTime.Now;
                            MapeadorArquivoJson.Gravar<List<Liberacao>>("Acesso.liberacao.cfg", liberacoes);
                        }
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
                    _tempoInicialAutorizouAlunoSairSozinho = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog("ERRO - 2 - Override\n\r" + ex.ToString());
            }
        }

        private bool ControleDeHorarios(Pessoa pessoa, DateTime ultimoAcesso)
        {
            if (!(pessoa is Aluno)) return true;
            if (ConfiguracaoAcesso.IntervalosParaAcesso == null) return true;

            var horarios = ConfiguracaoAcesso.IntervalosParaAcesso.Find(x => x.DiaSemana == (int)DateTime.Now.DayOfWeek).Intervalo;
            if (horarios == null) return true;

            foreach (Horarios horario in horarios)
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
                    if (ConfiguracaoAcesso.IntervalosParaAcessoUnico == "SIM" && (acesso1 >= 0 && acesso2 <= 0))
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
                if (!_repositorioAluno.AlunoPodeSerLiberadoPeloResponsavel(aluno.Id, _codigoPessoaAutorizou) && ConfiguracaoAcesso.ResponsaveisPodemLiberarAcessoAluno == "SIM")
                {
                    return false;
                }
            }
            else if (_tipoPessoaAutorizou == TipoPessoa.AutorizadoBuscarAluno)
            {
                if (!_repositorioAluno.AlunoPodeSerLiberadoPeloAutorizado(aluno.Id, _codigoPessoaAutorizou) && ConfiguracaoAcesso.AutorizadosPodemLiberamAcessoAluno == "SIM")
                {
                    return false;
                }
            }
            else if (_tipoPessoaAutorizou == TipoPessoa.Professor)
            {
                if (!ConfiguracaoAcesso.ProfessoresPodemLiberarAcessoAluno.Split(',').Any(x => x == _codigoPessoaAutorizou.ToString()))
                {
                    return false;
                }
            }
            else if (_tipoPessoaAutorizou == TipoPessoa.Profissional)
            {
                if (!ConfiguracaoAcesso.ColaboradoresPodeLiberarAcessoAluno.Split(',').Any(x => x == _codigoPessoaAutorizou.ToString()))
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

        protected override void RegistreAcessoPessoa( Acesso.Dominio.Pessoa pessoa,  Acesso.Dominio.SentidoGiro sentidoDoGiro)
        {
            throw new NotImplementedException();
        }
    }
}
