using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Dtos;
using EMCatraca.Neokoros;
using EMCatraca.Server;
using EMCatraca.Server.Excecoes;
using EMCatraca.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMCatraca.RegrasAcesso.Neokoros.ColegioWrBioFacial
{
    internal class BioFacialNeokorosWr : BioFacialNeokorosAbstract
    {
        private readonly IRepositorioAluno _iRepositorioAluno = FabricaDeRepositorios.Instancia.CrieRepositorioAluno();
        private readonly IRepositorioProfessor _iRepositorioProfessor = FabricaDeRepositorios.Instancia.CrieRepositorioProfessor();
        private readonly IRepositorioColaborador _iRepositorioColaborador = FabricaDeRepositorios.Instancia.CrieRepositorioColaborador();
        private readonly IRepositorioResponsavel _iRepositorioResponsavel = FabricaDeRepositorios.Instancia.CrieRepositorioResponsavel();
        private readonly IRepositorioAutorizadoBuscarAluno _iRepositorioAutorizadoBuscarAluno = FabricaDeRepositorios.Instancia.CrieRepositorioAutorizadoBuscarAluno();
        private readonly IRepositorioAuditoria _iRepositorioAuditoria = FabricaDeRepositorios.Instancia.CrieRepositorioAuditoria();
        private readonly IRepositorioOcorrencias _iRepositorioOcorrencia = FabricaDeRepositorios.Instancia.CrieRepositorioOcorrencias();
        private readonly IRepositorioDeAcessoPessoa _iRepositorioAcesso = FabricaDeRepositorios.Instancia.CrieRepositorioAcesso();

        private TipoPessoa _tipoPessoaAutorizou;
        private int _codigoPessoaAutorizou = 0;

        private DateTime tempoInicialAutorizouAlunoSairSozinho;

        #region Menssagen Padrao negar acesso e para onde deve ser encaminha.

        const string messagenNegaAcessoEncaminhaSecretaria = "Encaminhe-se a Secretaria!";
        const string messagenNegarAcessoEncaminhaCordenacao = "Encaminhe-se a Coordenação!";
        const string messagenNegarAcessoEncaminhaDirecao = "Encaminhe-se a Direcao!";

        #endregion

        protected override string ObtenhaMatriculaPeloRfid(string codigoRfid)
            => $"1 {_iRepositorioAluno.ObtenhaMatriculaPorRFID(codigoRfid)}";

        protected override Pessoa ObtenhaPessoa(string codigo)
        {
            codigo = codigo.Split('#')[0];

            if (codigo.Length < 2)
            {
                throw new AcessoNegadoException(
                    $"Número de dígitos inválido: \"{codigo}\"");
            }

            var codigoTipoDePessoa = codigo?.Substring(0, 1);
            if (codigoTipoDePessoa == null)
            {
                throw new AcessoNegadoException(
                    $"Não foi possível recuperar o Código de Acesso informado: " +
                    $"\"{codigo}\"");
            }

            if (!int.TryParse(codigoTipoDePessoa, out int codigoTipoPessoa))
            {
                throw new AcessoNegadoException(
                    $"Não foi possível converter o Identificador de Tipo" +
                    $" \"{codigo.Substring(1)}\"");
            }

            var tipoPessoa = Enumeradores.ObtenhaTipoPessoaPadrao(
                Convert.ToInt32(codigoTipoDePessoa));

            if (TipoPessoa.NaoEncontrada.Equals(tipoPessoa))
            {
                throw new AcessoNegadoException(
                    $"Tipo de pessoa inválida no Código de Acesso:" +
                    $" \"{codigo}\"");
            }

            if (!int.TryParse(codigo.Substring(1), out int codigoAcesso))
            {
                throw new AcessoNegadoException(
                    $"Não foi possível converter o Código de Acesso " +
                    $"\"{codigo.Substring(1)}\"");
            }

            _tipoPessoaAutorizou = tipoPessoa;

            switch (tipoPessoa)
            {
                case TipoPessoa.Aluno:

                    return _iRepositorioAluno.ConsulteAluno(codigoAcesso);

                case TipoPessoa.AutorizadoBuscarAluno:

                    _codigoPessoaAutorizou = codigoAcesso;
                    return _iRepositorioAutorizadoBuscarAluno.ConsulteAutorizadoBuscarAluno(codigoAcesso);

                case TipoPessoa.Profissional:

                    if (_jsonConfig.RegrasAcesso.ColaboradoresPodeLiberarAcessoAluno.Split(',')
                        .Any(x => x == codigoAcesso.ToString()))
                    {
                        _codigoPessoaAutorizou = codigoAcesso;
                    }

                    return _iRepositorioColaborador.ConsulteColaborador(codigoAcesso);

                case TipoPessoa.Professor:

                    if (_jsonConfig.RegrasAcesso.ProfessoresPodemLiberarAcessoAluno.Split(',')
                        .Any(x => x == codigoAcesso.ToString()))
                    {
                        _codigoPessoaAutorizou = codigoAcesso;
                    }

                    return _iRepositorioProfessor.ConsulteProfessor(codigoAcesso);

                case TipoPessoa.Responsavel:

                    _codigoPessoaAutorizou = codigoAcesso;
                    return _iRepositorioResponsavel.ConsulteResponsavel(codigoAcesso);

                default:

                    throw new AcessoNegadoException("Tipo Pessoa não cadastrado!");
            }
        }

        protected override TurmaMontada ObtenhaTurmaMontada(string codigo)
        {
            var codigoPessoa = codigo.Substring(0, 1);

            var PessoaAluno = codigoPessoa.Equals("1")
                              ? TipoPessoa.Aluno
                              : TipoPessoa.NaoEncontrada;

            if (PessoaAluno.Equals(TipoPessoa.Aluno))
            {
                var matricula = codigo.Substring(1);
                return _iRepositorioAluno.ConsulteTurmaMontadaPorAluno(Convert.ToInt32(matricula));
            }

            throw new AcessoNegadoException($"Teste Mendes 1:{PessoaAluno}");
        }

        protected override bool EhGiroRepetido(Pessoa pessoa, DateTime dataHoraAcessoAtual)
        {
            var tipoPessoa = ((int)pessoa.TipoPessoa);

            var idPesso = pessoa.Id;

            double intevaloLimiteEmSegundos = 30;

            DateTime ultimoAcesso = _iRepositorioAcesso.ObtenhaUltimoAcessoDaPessoa(idPesso, tipoPessoa);

            TimeSpan intervaloEntreAcessos = dataHoraAcessoAtual.Subtract(ultimoAcesso);

            if (intervaloEntreAcessos.TotalSeconds > intevaloLimiteEmSegundos)
            {
                return true;
            }
            return false;
        }
        protected override void ValidePessoaEstaAtiva(Pessoa pessoa)
        {
            if (pessoa is Aluno aluno)
            {
                var ehAlunoAtivo = _iRepositorioAluno.AlunoEstaAtivo(aluno.Id);

                var existeConfiguracaoParaNegarAcesso = 
                    _jsonConfig.RegrasAcesso.BloquearAcessoAlunoSemMatricula == "SIM";

                if (!ehAlunoAtivo && existeConfiguracaoParaNegarAcesso)
                {
                    throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
                }
            }
            else if (pessoa is AutorizadoBuscarAluno autorizadoBuscarAluno)
            {
                var existePessoaAtiva = _iRepositorioAutorizadoBuscarAluno
                    .AutorizadoBuscarAlunoEstaAtivo(autorizadoBuscarAluno.Id);

                var existeConfiguracaoNegarAcesso = _jsonConfig
                    .RegrasAcesso.BloquearAcessoAutorizadoSemMatricula == "SIM";

                if (!existePessoaAtiva && existeConfiguracaoNegarAcesso)
                {
                    throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
                }
            }
            else if (pessoa is Colaborador colaborador)
            {
                var colaboradorEstaAtivo = _iRepositorioColaborador.ColaboradorEstaAtivo(colaborador.Id);

                var existeConfiguracaoNegarAcesso = _jsonConfig
                    .RegrasAcesso.BloquearAcessoColaboradorInativo == "SIM";

                if (!colaboradorEstaAtivo && existeConfiguracaoNegarAcesso)
                {
                    throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
                }
            }
            else if (pessoa is Professor professor)
            {
                var professorEstaAtivo = _iRepositorioProfessor.ProfessorEstaAtivo(professor.Id);

                var existeConfiguracaoNegarAcesso = _jsonConfig
                    .RegrasAcesso.BloquearAcessoProfessorInativo == "SIM";

                if (!professorEstaAtivo && existeConfiguracaoNegarAcesso)
                {
                    throw new AcessoNegadoException(messagenNegarAcessoEncaminhaCordenacao);
                }
            }
            else if (pessoa is Responsavel responsavel)
            {
                var responsavelEstaAtivo = _iRepositorioResponsavel.ResponsavelEstaAtivo(responsavel.Id);

                var existeConfiguracaoNegarAcesso = _jsonConfig
                    .RegrasAcesso.BloquearAcessoResponsavelSemMatricula == "SIM";

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
            if (_jsonConfig.RegrasAcesso.BloquearAcessoAlunoComPendenciaDocumentos == "SIM")
            {
                if (_iRepositorioAluno.AlunoEstaPendenteDeDocumentos(aluno.Id))
                {
                    throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
                }
            }
        }

        protected override void ValideAlunoFaltaMateriais(Aluno aluno)
        {
            if (_jsonConfig.RegrasAcesso.BloquearAcessoAlunoComPendenciaMaterial == "SIM")
            {
                if (_iRepositorioAluno.AlunoEstaPendenteDeMateriais(aluno.Id))
                {
                    throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
                }
            }
        }

        protected override bool ValideAlunoPossuiLiberacao(Aluno aluno)
        {
            if (_jsonConfig.RegrasAcesso.FormularioLiberaAcessoAluno != "SIM")
            {
                return false;
            }

            var dataAtual = DateTime.Now;

            return _jsonConfig.Liberacoes.Any(
                l => l.Aluno.Id == aluno.Id 
                && l.Acessou == false 
                && (dataAtual - l.DataHoraLiberou).TotalSeconds < (double)(60 * l.TempoParaAcessso));
        }

        protected override void ValideAlunoPossuiBloqueio(Aluno aluno)
        {
            var existeBloqueio = _iRepositorioAluno
                .AlunoEstaBloqueado(
                                     aluno.Id, 
                                     _jsonConfig.RegrasAcesso.AtributoBloqueado
                                   );

            if (existeBloqueio)
            {
                throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
            }
        }

        protected override void ValideAlunoPodeSairSozinho(Aluno aluno)
        {
            int tempoMinimo = _jsonConfig.RegrasAcesso.TempoParaAcessoLiberadoSegundos;
            if (tempoInicialAutorizouAlunoSairSozinho.GetHashCode() == 0
                || DateTime.Now.Subtract(tempoInicialAutorizouAlunoSairSozinho)
                .TotalSeconds > tempoMinimo)
            {
                _codigoPessoaAutorizou = 0;
            }

            var atributoPodeSairSozinho = _jsonConfig.RegrasAcesso.AtributoPodeSairSozinho;
            if (!_iRepositorioAluno.AlunoPodeSairSozinho(aluno.Id, atributoPodeSairSozinho))
            {
                if (!EhAutorizadoAcessoForaDoHorarioAcesso(aluno))
                {
                    throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
                }
            }
        }

        protected override void ValideAlunoPossuiOcorrencia(Aluno aluno)
        {
            var negarAcessoDeAlunosComOcorrecias = _jsonConfig.RegrasAcesso.BloquearAcessoAlunoComOcorrencias;
            if (_iRepositorioOcorrencia.ExisteOcorrencias(aluno.Id, (int)aluno.RecuperaTipo(), negarAcessoDeAlunosComOcorrecias))
            {
                throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
            }
        }

        protected override void ValideProfessorPossuiOcorrencia(Professor professor)
        {
            var negarAcessoProfessor = _jsonConfig.RegrasAcesso.BloquearAcessoProfessorComOcorrencias;
            if (_iRepositorioOcorrencia.ExisteOcorrencias(professor.Id, (int)professor.RecuperaTipo(), negarAcessoProfessor))
            {
                throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
            }
        }

        protected override void ValideColaboradorPossuiOcorrencias(Colaborador colaborador)
        {
            var negarAcessoColaborador = _jsonConfig.RegrasAcesso.BloquearAcessoColaboradorComOcorrencias;
            if (_iRepositorioOcorrencia.ExisteOcorrencias(colaborador.Id, (int)colaborador.RecuperaTipo(), negarAcessoColaborador))
            {
                throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
            }
        }

        protected override void ValideResponsavelPossuiBloqueio(Responsavel responsavel)
        {
            if (_iRepositorioResponsavel.ResponsavelEstaBloqueado(responsavel.Id))
            {
                throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
            }
        }

        protected override void ValideTemAcessoPorTeclado(Pessoa pessoa, TipoAcesso tipoAcesso)
        {
            _jsonConfig.RegrasAcesso = MapeadorArquivoJson.CarreguerArquivoJson<Core.Dominio.RegrasAcesso>("EmCatraca.Acesso.cfg");

            if (tipoAcesso == TipoAcesso.Teclado)
            {
                string selecionados = string.Empty;

                if (pessoa is Aluno)
                {
                    selecionados = _jsonConfig.RegrasAcesso.AlunosPodemDigitar;
                }

                if (pessoa is Professor)
                {
                    selecionados = _jsonConfig.RegrasAcesso.ProfessoresPodemDigitar;
                }

                if (pessoa is Colaborador)
                {
                    selecionados = _jsonConfig.RegrasAcesso.ColaboradoresPodemDigitar;
                }

                if (pessoa is Responsavel)
                {
                    selecionados = _jsonConfig.RegrasAcesso.ResponsaveisPodemDigitar;
                }

                if (pessoa is AutorizadoBuscarAluno)
                {
                    selecionados = _jsonConfig.RegrasAcesso.AutorizadosPodemDigitar;
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
                tempoMinimo = _jsonConfig.RegrasAcesso.TempoMinimoParaNovoAcessoSegundos;
            }

            var tempoDecorridoUltimoAcesso = DateTime.Now.Subtract(_iRepositorioAcesso.ObtenhaUltimoAcessoDaPessoa(pessoa.Id, (int)pessoa.RecuperaTipo()));
            if (tempoDecorridoUltimoAcesso.TotalSeconds < tempoMinimo)
            {
                throw new AcessoNegadoException($"Aguarde {tempoMinimo - (int)tempoDecorridoUltimoAcesso.TotalSeconds} segundos!");
            }
        }

        protected override string MonteMensagemRestricao(Pessoa pessoa)
        {
            if (pessoa is Aluno aluno)
            {
                var existeRestricaoParaAluno = _iRepositorioOcorrencia
                    .ExisteOcorrencias(pessoa.Id, (int)pessoa
                    .RecuperaTipo(), _jsonConfig.RegrasAcesso.MensagemAlunoComOcorrencias);

                existeRestricaoParaAluno = existeRestricaoParaAluno 
                    || (_iRepositorioAluno.AlunoEstaInadimplenteDuplicata(aluno.Id)
                    && _jsonConfig.RegrasAcesso.MensagemAlunoComInadimplenciaDeDuplicatas == "SIM");

                existeRestricaoParaAluno = existeRestricaoParaAluno 
                    || (_iRepositorioAluno.AlunoEstaInadimplenteDeCheques(aluno.Id)
                    && _jsonConfig.RegrasAcesso.MensagemAlunoComInadimplenciaDeCheques == "SIM");

                existeRestricaoParaAluno = existeRestricaoParaAluno 
                    || (_iRepositorioAluno.AlunoEstaPendenteDeDocumentos(aluno.Id)
                    && _jsonConfig.RegrasAcesso.MensagemAlunoComPendenciaDeDocumentos == "SIM");

                existeRestricaoParaAluno = existeRestricaoParaAluno 
                    || (_iRepositorioAluno.AlunoEstaPendenteDeMateriais(aluno.Id)
                    && _jsonConfig.RegrasAcesso.MensagemAlunoComPendenciaDeMateriais == "SIM");

                if (existeRestricaoParaAluno)
                {
                    return messagenNegaAcessoEncaminhaSecretaria;
                }
            }

            if (pessoa is Responsavel)
            {
                var existeRestricaoParaResponsavel = _iRepositorioOcorrencia
                    .ExisteOcorrencias(pessoa.Id, (int)pessoa
                    .RecuperaTipo(), _jsonConfig.RegrasAcesso.MensagemProfessorComOcorrencias);

                if (existeRestricaoParaResponsavel)
                {
                    return messagenNegarAcessoEncaminhaCordenacao;
                }
            }

            if (pessoa is Colaborador)
            {
                var existeRestricao = _iRepositorioOcorrencia
                    .ExisteOcorrencias(pessoa.Id, (int)pessoa
                    .RecuperaTipo(), _jsonConfig.RegrasAcesso.MensagemColaboradorComOcorrencias);

                if (existeRestricao)
                {
                    return messagenNegarAcessoEncaminhaDirecao;
                }
            }

            return string.Empty;
        }

        protected override void RegistreAcessoPessoa(Pessoa pessoa, SentidoGiro sentidoDoGiro)
        {
            if (pessoa is Aluno && _jsonConfig.RegrasAcesso.FormularioLiberaAcessoAluno == "SIM")
            {
                var liberacoes = MapeadorArquivoJson.CarreguerArquivoJson<List<Liberacao>>("EmCatraca.Liberacao.cfg");
                var liberacao = liberacoes.Find(l => l.Aluno.Id == pessoa.Id);
                if (liberacao != null)
                {
                    liberacao.Acessou = true;
                    liberacao.DataHoraAcessou = DateTime.Now;
                    MapeadorArquivoJson.Gravar<List<Liberacao>>("EmCatraca.Liberacao.cfg", liberacoes);
                }
            }

            _iRepositorioAcesso.RegistreAcesso(
                new RegistroAcesso
                {
                    IdPessoa = pessoa.Id,
                    TipoPessoaAutorizou = (int)_tipoPessoaAutorizou,
                    CodigoPessoaAutorizou = _codigoPessoaAutorizou,
                    SentidoDoGiro = (int)sentidoDoGiro
                });

            _iRepositorioAuditoria.RegistreAuditoriaDeAcesso(
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

            if (_jsonConfig.RegrasAcesso.IntervalosAcesso == null)
            {
                return true;
            }

            var intervalosAgrupado = from intervalo in _jsonConfig.RegrasAcesso.IntervalosAcesso
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
                    if (_jsonConfig.RegrasAcesso.IntervalosParaAcessoUnico == "SIM" && (acesso1 >= 0 && acesso2 <= 0))
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
                if (!_iRepositorioAluno.AlunoPodeSerLiberadoPeloResponsavel(aluno.Id, _codigoPessoaAutorizou) 
                    && _jsonConfig.RegrasAcesso.ResponsaveisPodemLiberarAcessoAluno == "SIM")
                {
                    return false;
                }
            }
            else if (_tipoPessoaAutorizou == TipoPessoa.AutorizadoBuscarAluno)
            {
                if (!_iRepositorioAluno.AlunoPodeSerLiberadoPeloAutorizado(aluno.Id, _codigoPessoaAutorizou) 
                    && _jsonConfig.RegrasAcesso.AutorizadosPodemLiberamAcessoAluno == "SIM")
                {
                    return false;
                }
            }
            else if (_tipoPessoaAutorizou == TipoPessoa.Professor)
            {
                if (!_jsonConfig.RegrasAcesso.ProfessoresPodemLiberarAcessoAluno.Split(',')
                    .Any(x => x == _codigoPessoaAutorizou.ToString()))
                {
                    return false;
                }
            }
            else if (_tipoPessoaAutorizou == TipoPessoa.Colaborador)
            {
                if (!_jsonConfig.RegrasAcesso.ColaboradoresPodeLiberarAcessoAluno.Split(',')
                    .Any(x => x == _codigoPessoaAutorizou.ToString()))
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

        protected override void ValideDentroDoHorarioDeAcesso(Pessoa pessoa)
        {
            if (!ControleDeHorarios(pessoa, _iRepositorioAcesso
                .ObtenhaUltimoAcessoDaPessoa(pessoa.Id, (int)pessoa.RecuperaTipo())))
            {
                throw new AcessoNegadoException(messagenNegaAcessoEncaminhaSecretaria);
            }
        }
    }
}