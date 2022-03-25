using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Core.Negocio.Enumeradores;
using EMCatraca.Server;
using EMCatraca.Server.Excecoes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ValidacaoExterna.Neokoros;
using static EMCatraca.Core.Dominio.EventoCatraca;

namespace EMCatraca.Neokoros
{
    public abstract class BioFacialNeokorosAbstract
    {
        private SentidoGiro _sentidoDoGiro = SentidoGiro.Indefinido;
        private TipoAcesso _tipoAcesso = TipoAcesso.Indefinido;

        private Pessoa _pessoa = null;
        private TurmaMontada TurmaMontada { get; set; } = null;

        protected abstract Pessoa ObtenhaPessoa(string codigo);
        protected abstract TurmaMontada ObtenhaTurmaMontada(string codigo);
        protected abstract string ObtenhaMatriculaPeloRfid(string codigo);
        protected abstract void RegistreAcessoPessoa(Pessoa pessoa, SentidoGiro sentidoDoGiro);

        protected ConfiguracoesDto _jsonConfig = new ConfiguracoesDto();

        private const string CaracterDeControleDeCartao = "#C";
        private const string CaracterDeControleDeTeclado = "#P";
        private const string CaracterDeControleDeNumeroDeSerieDoCartao = "#S";
        private const string CaracterDeControleDeTag = "#T";
        private string _messagenLog;
        private string _nomeLog = "Auditoria";

        public BioFacialNeokorosAbstract()
        {
            ObtenhaConfiguracoes();
            
            AuditoriaLog.Escreva(_nomeLog, $"Dispositivo:{_jsonConfig.Dispositivo}");
        }

        private void ObtenhaConfiguracoes()
        {
            _jsonConfig.EhDesenvolvedor = false;

            var conexao = EnumeradorTipoCFG.Conexao.Descricao;
            _jsonConfig.InformacaoConexao = MapeadorArquivoJson.CarreguerJson<InformacaoConexao>(conexao, _jsonConfig);
            _messagenLog = $"{nameof(EMCatraca.Neokoros)}.{nameof(BioFacialNeokorosAbstract)}.ObtenhaConfiguracoes.{conexao}";
            AuditoriaLog.Escreva(_nomeLog, _messagenLog);

            var loader = EnumeradorTipoCFG.Loader.Descricao;
            _jsonConfig.TipoIntegracao = MapeadorArquivoJson.CarreguerJson<CatracaLoader>(loader, _jsonConfig).TipoIntegracao;
            _messagenLog = $"{nameof(EMCatraca.Neokoros)}.{nameof(BioFacialNeokorosAbstract)}.ObtenhaConfiguracoes.{loader}";
            AuditoriaLog.Escreva(_nomeLog, _messagenLog);

            var acesso = EnumeradorTipoCFG.Acesso.Descricao;
            _jsonConfig.RegrasAcesso = MapeadorArquivoJson.CarreguerJson<RegrasAcesso>(acesso, _jsonConfig);
            _messagenLog = $"{nameof(EMCatraca.Neokoros)}.{nameof(BioFacialNeokorosAbstract)}.ObtenhaConfiguracoes.{acesso}";
            AuditoriaLog.Escreva(_nomeLog, _messagenLog);

            var dispositivos = EnumeradorTipoCFG.Dispositivo.Descricao;
            _jsonConfig.TodosDispositivos = MapeadorArquivoJson.CarreguerJson<List<Dispositivo>>(dispositivos, _jsonConfig);
            _messagenLog = $"{nameof(EMCatraca.Neokoros)}.{nameof(BioFacialNeokorosAbstract)}.ObtenhaConfiguracoes.{dispositivos}";
            AuditoriaLog.Escreva(_nomeLog, _messagenLog);

            var liberacoes = EnumeradorTipoCFG.Liberacoes.Descricao;
            _jsonConfig.Liberacoes = MapeadorArquivoJson.CarreguerJson<List<Liberacao>>(liberacoes, _jsonConfig);
            _messagenLog = $"{nameof(EMCatraca.Neokoros)}.{nameof(BioFacialNeokorosAbstract)}.ObtenhaConfiguracoes.{liberacoes}";
            AuditoriaLog.Escreva(_nomeLog, _messagenLog);

            if (_jsonConfig.TodosDispositivos.Any())
            {
                _jsonConfig.Dispositivo = _jsonConfig.TodosDispositivos.First();
            }
        }

        protected virtual bool EhGiroRepetido(Pessoa pessoa, DateTime dataHora) => false;

        protected virtual void ValideTemAcessoPorTeclado(Pessoa pessoa, TipoAcesso tipoAcesso) { }

        protected virtual void ValidePessoaEstaAtiva(Pessoa pessoa) { }

        protected virtual void ValideTempoMinimoParaNovoAcesso(Pessoa pessoa) { }

        protected virtual bool ValideAlunoPossuiLiberacao(Aluno aluno) => false;

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

        public Pessoa ConsultePessoa(string dadosRecebidos)
        {
            Pessoa pessoa = RecuperePessoa(dadosRecebidos);
            return pessoa;
        }

        public TurmaMontada ConsulteTurmaMontada(string codigo)
        {
            TurmaMontada turmaMontada = RecupereTurmaMontada(codigo);
            return turmaMontada;
        }

        private TurmaMontada RecupereTurmaMontada(string codigo)
        {

            if (string.IsNullOrEmpty(codigo))
            {
                return null;
            }

            try
            {
                TurmaMontada = ObtenhaTurmaMontada(codigo);
                return TurmaMontada;
            }
            catch (Exception ex)
            {
                _messagenLog = $"{nameof(EMCatraca.Neokoros)}.{nameof(BioFacialNeokorosAbstract)}.{nameof(RecupereTurmaMontada)}: {ex}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);
                return null;
            }
        }

        private Pessoa RecuperePessoa(string codigoRecebido)
        {
            if (string.IsNullOrEmpty(codigoRecebido))
            {
                return null;
            }

            string[] codigoRecebidoFormatado = codigoRecebido.Split('#');
            string codigo = string.Empty;

            if (codigoRecebidoFormatado.Length >= 0)
            {
                codigo = codigoRecebidoFormatado[0];
            }

            if (codigoRecebidoFormatado.Length > 1)
            {
                var formaAcesso = $"#{codigoRecebidoFormatado[1]}";

                switch (formaAcesso)
                {
                    case CaracterDeControleDeCartao:
                        _tipoAcesso = TipoAcesso.Cartao;

                        break;
                    case CaracterDeControleDeTag:
                        _tipoAcesso = TipoAcesso.RFID;

                        break;
                    case CaracterDeControleDeTeclado:
                        _tipoAcesso = TipoAcesso.Teclado;

                        break;
                }
            }

            if (_tipoAcesso == TipoAcesso.RFID || (_tipoAcesso == TipoAcesso.Cartao && codigo.Length >= 12))
            {
                codigo = ObtenhaMatriculaPeloRfid(codigo);
            }

            try
            {
                _pessoa = ObtenhaPessoa(codigo.ToString());
                return _pessoa;
            }
            catch (Exception ex)
            {
                _messagenLog = $"{nameof(EMCatraca.Neokoros)}.{nameof(BioFacialNeokorosAbstract)}.{nameof(RecuperePessoa)}: {ex}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                throw new AcessoNegadoException($"Falha ao identificar -> dados recebidos: {codigoRecebido}");
            }
        }

        public RetornoDeValidacaoDeAcesso ValideAcesso(string dadosRecebidos, DateTime dataHora, int numeroTerminal)
        {
            var retorno = new RetornoDeValidacaoDeAcesso();

            EventoCatraca evento;

            try
            {
                _pessoa = RecuperePessoa(dadosRecebidos);

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

                Dispositivo catraca = _jsonConfig.TodosDispositivos.Where(p => p.Codigo == numeroTerminal).FirstOrDefault();

                if (!string.IsNullOrEmpty(msgRestricao))
                {
                    retorno.Mensagem1 = _pessoa.Nome;
                    retorno.Mensagem2 = "Acesso liberado!";
                    retorno.AcessoEsperado = ObtenhaAcessoPorCatraca(catraca);
                    retorno.GiroId = ObtenhaGiroPorCatraca(catraca);

                    evento = CrieAcessoLiberadoComRestricao(SentidoGiro.Indefinido, _pessoa, _jsonConfig.Dispositivo, msgRestricao);

                    retorno.Sucesso = true;
                }
                else
                {
                    retorno.Mensagem1 = _pessoa.Nome;
                    retorno.Mensagem2 = "Acesso liberado!";
                    retorno.AcessoEsperado = ObtenhaAcessoPorCatraca(catraca);
                    retorno.GiroId = ObtenhaGiroPorCatraca(catraca);
                    
                    if(retorno.GiroId == -1)
                    {
                        _messagenLog = $"{nameof(EMCatraca.Neokoros)}.{nameof(BioFacialNeokorosAbstract)}.{nameof(ValideAcesso)}: " +
                                       $"Giro do dispositivo não informado";
                        AuditoriaLog.Escreva(_nomeLog, _messagenLog);
                    }

                    evento = CrieAcessoLiberado(SentidoGiro.Indefinido, _pessoa, _jsonConfig.Dispositivo);

                    retorno.Sucesso = true;
                }

                _messagenLog = $"{nameof(EMCatraca.Neokoros)}.{nameof(BioFacialNeokorosAbstract)}.{nameof(ValideAcesso)}: "+
                    $"Acesso liberado -> tipo: {_pessoa.RecuperaTipo()}, " +
                                    $"código: {_pessoa?.Id}, " +
                                    $"nome: {_pessoa?.Nome}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                return retorno;
            }
            catch (AcessoNegadoException ex)
            {
                if (_pessoa == null)
                {
                    _pessoa = new Pessoa()
                    {
                        Nome = "Não cadastrado!"
                    };
                }

                evento = CrieAcessoNegado(SentidoGiro.Indefinido, _pessoa, _jsonConfig.Dispositivo);
                evento.Mensagem2 = ex.Message;

                _messagenLog = $"{nameof(EMCatraca.Neokoros)}.{nameof(BioFacialNeokorosAbstract)}.{nameof(ValideAcesso)}: Acesso negado -> método da validação: \"{ex.TargetSite.Name}\"";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                retorno.Mensagem1 = _pessoa.Nome;
                retorno.Mensagem2 = "Acesso negado!";
                retorno.AcessoEsperado = AcessoEsperado.EsperadoProibido;

                return retorno;
            }
        }

        private int ObtenhaGiroPorCatraca(Dispositivo catraca)
        {
            if (catraca.EhGiroEntrada)
            {
                return SentidoGiro.Entrada.GetHashCode();
            }

            if (catraca.EhGiroSaida)
            {
                return SentidoGiro.Saida.GetHashCode();
            }

            if (catraca.EhGiroNormal)
            {
                return SentidoGiro.Indefinido.GetHashCode();
            }

            return -1;
        }

        private AcessoEsperado ObtenhaAcessoPorCatraca(Dispositivo catraca)
        {
            if (catraca.EhGiroEntrada)
            {
                return AcessoEsperado.SomenteEntradaAguardeGiro;
            }

            if (catraca.EhGiroSaida)
            {
                return AcessoEsperado.SomenteSaidaAguardeGiro;
            }

            return AcessoEsperado.EntradaSaidaAguardeGiro;
        }

        private SentidoGiro ObtenhaDirecaoGiro(int direcaoGiro)
        {
            switch (direcaoGiro)
            {
                case 0:
                    return SentidoGiro.Indefinido;

                case 1:
                    return SentidoGiro.Entrada;

                case 2:
                    return SentidoGiro.Saida;
            }

            return SentidoGiro.Indefinido;
        }

        public void RegistreAcesso(string codigo, DateTime dataHora, int numeroTerminal, int direcaoGiro)
        {
            _sentidoDoGiro = ObtenhaDirecaoGiro(direcaoGiro);

            try
            {
                _pessoa = RecuperePessoa(codigo);

                _messagenLog = $"{nameof(EMCatraca.Neokoros)}.{nameof(BioFacialNeokorosAbstract)}.{nameof(RegistreAcesso)}: "+
                    $"Registrando o Acesso->da pessoa: { _pessoa.TipoPessoa}, " +
                    $"código: {_pessoa.Id}, data: {dataHora:dd/MM/yyyy HH:mm}, " +
                    $"terminal: {numeroTerminal}, direcao do giro: {direcaoGiro}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                if (_pessoa != null && EhGiroRepetido(_pessoa, dataHora))
                {
                    RegistreAcessoPessoa(_pessoa, _sentidoDoGiro);

                    _messagenLog = $"{nameof(EMCatraca.Neokoros)}.{nameof(BioFacialNeokorosAbstract)}.{nameof(RegistreAcesso)}: "+
                                   $"Sentido do Giro -> giro: {_sentidoDoGiro}";
                    AuditoriaLog.Escreva(_nomeLog, _messagenLog);
                }
                else
                {
                    _messagenLog = $"{nameof(EMCatraca.Neokoros)}.{nameof(BioFacialNeokorosAbstract)}.{nameof(RegistreAcesso)}: " +
                                   $"Giro Repetido não registrado";
                    AuditoriaLog.Escreva(_nomeLog, _messagenLog);
                }
                
                _pessoa = null;
            }
            catch (Exception ex)
            {
                _messagenLog = $"{nameof(EMCatraca.Neokoros)}.{nameof(BioFacialNeokorosAbstract)}.{nameof(RegistreAcesso)}: {ex}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);
            }
        }

        private static string ObtenhaLinhaLog()
        {
            var stackFrame = new StackFrame(1, true);
            int linha = stackFrame.GetFileLineNumber();

            return $"Ln:{linha} -> ";
        }
    }
}