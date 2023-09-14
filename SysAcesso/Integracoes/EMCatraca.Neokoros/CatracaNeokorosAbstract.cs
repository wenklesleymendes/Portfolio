using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Core.Negocio.Enumeradores;
using EMCatraca.Server;
using EMCatraca.Server.Excecoes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TcpIp;
using ValidacaoExterna.Neokoros;
using static EMCatraca.Core.Dominio.EventoCatraca;

namespace EMCatraca.Neokoros
{
    public abstract class CatracaNeokorosAbstract
    {
        private SentidoGiro _sentidoDoGiro = SentidoGiro.Indefinido;
        private TipoAcesso _tipoAcesso = TipoAcesso.Indefinido;

        private Pessoa _pessoa = null;
        private TurmaMontada TurmaMontada { get; set; } = null;

        protected abstract Pessoa ObtenhaPessoa(string codigo);
        protected abstract TurmaMontada ObtenhaTurmaMontada(string codigo);
        protected abstract string ObtenhaMatriculaPorRFID(string codigo);
        protected abstract void RegistreAcessoPessoa(Pessoa pessoa, SentidoGiro sentidoDoGiro);

        private readonly TcpIpServidor ServidorMonitor = new TcpIpServidor();
        protected readonly ConfiguracoesDto _config = new ConfiguracoesDto();

        private const string CaracterDeControleDeCartao = "#C";
        private const string CaracterDeControleDeTeclado = "#P";
        private const string CaracterDeControleDeNumeroDeSerieDoCartao = "#S";
        private const string CaracterDeControleDeTag = "#T";

        protected CatracaNeokorosAbstract(Dispositivo dispositivo)
        {
            if (dispositivo.Codigo == 0)
            {
                MapeaPrimeiroDispositivo();

                AuditoriaLog.Escreva(nameof(CatracaNeokorosAbstract),
                    $"Dispositivo{_config.Dispositivo}");

                return;
            }

            ObtenhaConfiguracoes();
            _config.Dispositivo = dispositivo;
        }

        private void ObtenhaConfiguracoes()
        {
            _config.EhDesenvolvedor = false;

            var conexao = EnumeradorTipoCFG.Conexao.Descricao;
            _config.InformacaoConexao = MapeadorArquivoJson
                .CarreguerJson<InformacaoConexao>(conexao, _config);

            var loader = EnumeradorTipoCFG.Loader.Descricao;
            _config.TipoIntegracao = MapeadorArquivoJson
                .CarreguerJson<CatracaLoader>(loader, _config).TipoIntegracao;

            var acesso = EnumeradorTipoCFG.Acesso.Descricao;
            _config.RegrasAcesso = MapeadorArquivoJson
                .CarreguerJson<RegrasAcesso>(acesso, _config);

            var dispositivos = EnumeradorTipoCFG.Dispositivo.Descricao;
            _config.TodosDispositivos = MapeadorArquivoJson
                .CarreguerJson<List<Dispositivo>>(dispositivos, _config);

            var liberacoes = EnumeradorTipoCFG.Liberacoes.Descricao;
            _config.Liberacoes = MapeadorArquivoJson
                .CarreguerJson<List<Liberacao>>(liberacoes, _config);

            if (_config.RegrasAcesso.ExisteConfiguracoesCustomizadaTipoPessoa.Equals("SIM"))
            {
                var customizacaoTipoPessoa = EnumeradorTipoCFG.CTipoPessoa.Descricao;

                _config.CutomizacaoTipoPessoa = MapeadorArquivoJson
                    .CarreguerJson<CustomizacaoTipoPessoa>(customizacaoTipoPessoa, _config);
            }
        }

        private Dispositivo MapeaPrimeiroDispositivo()
        {
            var dispositivos = EnumeradorTipoCFG.Dispositivo.Descricao;
            _config.TodosDispositivos = MapeadorArquivoJson.CarreguerJson<List<Dispositivo>>(dispositivos, _config);

            if (_config.TodosDispositivos.Any())
            {
                _config.Dispositivo = _config.Dispositivo = _config.TodosDispositivos.First();
            }

            return _config.Dispositivo;
        }

        protected virtual void ValideTemAcessoPorTeclado(Pessoa pessoa, TipoAcesso tipoAcesso) { }

        protected virtual void ValidePessoaEstaAtiva(Pessoa pessoa) { }

        protected virtual void ValideTempoMinimoParaNovoAcesso(Pessoa pessoa) { }

        protected virtual bool ValideAlunoPossuiLiberacao(Aluno aluno)
        {
            return false;
        }

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

        protected virtual string MonteMensagemRestricao(Pessoa pessoa)
        {
            return null;
        }

        public Pessoa ConsultePessoa(string codigo) => RecuperePessoa(codigo);

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
                AuditoriaLog.EscrevaErro(nameof(CatracaNeokorosAbstract), ex);
                return null;
            }
        }

        private Pessoa RecuperePessoa(string codigoRecebido)
        {
            LogAuditoria.Escreva($"{nameof(RecuperePessoa)}: " +
                $"Retorna Pessoa {nameof(codigoRecebido)}={codigoRecebido}",
                $"{nameof(CatracaNeokorosAbstract)}{codigoRecebido}");

            if (string.IsNullOrEmpty(codigoRecebido))
            {
                LogAuditoria.Escreva($"{nameof(RecuperePessoa)}: " +
                   $"Retorna Pessoa {nameof(codigoRecebido)}= IsNullOrEmpty",
                   $"{nameof(CatracaNeokorosAbstract)}{codigoRecebido}");

                return null;
            }

            string[] codigoRecebidoFormatado = codigoRecebido.Split('#');
            LogAuditoria.Escreva($"{nameof(RecuperePessoa)}: " +
                $"string[] {nameof(codigoRecebidoFormatado)}={codigoRecebidoFormatado}",
                $"{nameof(CatracaNeokorosAbstract)}{codigoRecebido}");

            string codigo = string.Empty;
            if (codigoRecebidoFormatado.Length >= 0)
            {
                codigo = codigoRecebidoFormatado[0];
                LogAuditoria.Escreva($"{nameof(RecuperePessoa)}: " +
                    $"string {nameof(codigo)}={codigo}",
                    $"{nameof(CatracaNeokorosAbstract)}{codigoRecebido}");
            }

            _tipoAcesso = TipoAcesso.Indefinido;
            LogAuditoria.Escreva($"{nameof(RecuperePessoa)}: " +
                $"Enumerador {nameof(_tipoAcesso)}={_tipoAcesso}",
                $"{nameof(CatracaNeokorosAbstract)}{codigoRecebido}");

            if (codigoRecebidoFormatado.Length > 1)
            {
                string formaAcesso = $"#{codigoRecebidoFormatado[1]}";

                LogAuditoria.Escreva($"{nameof(RecuperePessoa)}: " +
                    $"string {nameof(formaAcesso)}={formaAcesso}",
                    $"{nameof(CatracaNeokorosAbstract)}{codigoRecebido}");

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

                    default:
                        break;
                }
            }

            if (_tipoAcesso == TipoAcesso.RFID || (_tipoAcesso == TipoAcesso.Cartao && codigo.Length >= 12))
            {
                LogAuditoria.Escreva($"{nameof(RecuperePessoa)}: " +
                    $"Enumerador {nameof(_tipoAcesso)}={_tipoAcesso}",
                    $"{nameof(CatracaNeokorosAbstract)}{codigoRecebido}");

                LogAuditoria.Escreva($"{nameof(RecuperePessoa)}: " +
                    $"Codigo do RFID={nameof(codigo)}={codigo}",
                    $"{nameof(CatracaNeokorosAbstract)}{codigoRecebido}");

                codigo = ObtenhaMatriculaPorRFID(codigo);

                LogAuditoria.Escreva($"{nameof(ObtenhaMatriculaPorRFID)}: " +
                    $"MATRICULA={nameof(codigo)}={codigo}",
                    $"{nameof(CatracaNeokorosAbstract)}{codigoRecebido}");
            }

            try
            {
                _pessoa = ObtenhaPessoa(codigo.ToString());

                LogAuditoria.Escreva($"{nameof(RecuperePessoa)}: " +
                    $"Retorna Pessoa " +
                    $"Matricula={_pessoa.Id} " +
                    $"Tipo Pessoa={_pessoa.TipoPessoa} " +
                    $"Nome={_pessoa.Nome}",
                    $"{nameof(CatracaNeokorosAbstract)}{codigoRecebido}");

                return _pessoa;
            }
            catch (Exception ex)
            {
                AuditoriaLog.EscrevaErro(nameof(CatracaNeokorosAbstract), ex);

                throw new AcessoNegadoException($"Falha ao identificar -> dados recebidos: {codigoRecebido}");
            }
        }

        public RetornoDeValidacaoDeAcesso ValideAcesso(string dadosRecebidos)
        {
            AuditoriaLog.Escreva(nameof(CatracaNeokorosAbstract), $"Iniciando validação do acesso");

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

                if (!string.IsNullOrEmpty(msgRestricao))
                {
                    retorno.Mensagem1 = _pessoa.Nome;
                    retorno.Mensagem2 = "Acesso liberado!";
                    retorno.AcessoEsperado = ObtenhaGiroAcessoPorCatraca(_config.Dispositivo);

                    evento = CrieAcessoLiberadoComRestricao(SentidoGiro.Indefinido, _pessoa, _config.Dispositivo, msgRestricao);

                    retorno.Sucesso = true;
                }
                else
                {
                    retorno.Mensagem1 = _pessoa.Nome;
                    retorno.Mensagem2 = "Acesso liberado!";
                    retorno.AcessoEsperado = ObtenhaGiroAcessoPorCatraca(_config.Dispositivo);

                    evento = CrieAcessoLiberado(SentidoGiro.Indefinido, _pessoa, _config.Dispositivo);
                    retorno.Sucesso = true;
                }

                AuditoriaLog.Escreva(nameof(CatracaNeokorosAbstract),
                    $"Acesso liberado: {_pessoa.RecuperaTipo()}, " +
                    $"Código: {_pessoa?.Id}, " +
                    $"Nome: {_pessoa?.Nome}");

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

                evento = CrieAcessoNegado(SentidoGiro.Indefinido, _pessoa, _config.Dispositivo);
                evento.Mensagem2 = ex.Message;

                ServidorMonitor.EnviarMensagem(JsonConvert.SerializeObject(evento));

                AuditoriaLog.Escreva(nameof(CatracaNeokorosAbstract),
                    $"Acesso negado {ex.TargetSite.Name}");

                retorno.Mensagem1 = _pessoa.Nome;
                retorno.Mensagem2 = "Acesso negado!";
                retorno.AcessoEsperado = AcessoEsperado.EsperadoProibido;

                return retorno;
            }
        }

        private AcessoEsperado ObtenhaGiroAcessoPorCatraca(Dispositivo catraca)
        {
            return catraca.EhGiroEntrada
                ? AcessoEsperado.SomenteEntradaAguardeGiro
                : catraca.EhGiroSaida
                    ? AcessoEsperado.SomenteSaidaAguardeGiro
                    : AcessoEsperado.EntradaSaidaAguardeGiro;
        }

        private SentidoGiro ObtenhaDirecaoGiro(int direcaoGiro)
        {
            switch (direcaoGiro)
            {
                case 1:
                    return SentidoGiro.Entrada;

                case 2:
                    return SentidoGiro.Saida;

                default:
                    return SentidoGiro.Indefinido;
            }
        }

        public void RegisteAcesso(string codigo, DateTime dataHora, int numeroTerminal, int direcaoGiro)
        {
            _sentidoDoGiro = ObtenhaDirecaoGiro(direcaoGiro);

            try
            {
                _pessoa = RecuperePessoa(codigo);

                AuditoriaLog.Escreva(nameof(CatracaNeokorosAbstract),
                    $"Registrando o Acesso  Tipo Pessoa: {_pessoa.TipoPessoa}, " +
                    $"código: {_pessoa.Id}, data: {dataHora:dd/MM/yyyy HH:mm}, " +
                    $"terminal: {numeroTerminal}, direcao do giro: {direcaoGiro}");

                if (_pessoa != null)
                {
                    RegistreAcessoPessoa(_pessoa, _sentidoDoGiro);
                    AuditoriaLog.Escreva(nameof(CatracaNeokorosAbstract), $"Sentido giro: {_sentidoDoGiro}");
                }

                _pessoa = null;
            }
            catch (Exception ex)
            {
                AuditoriaLog.EscrevaErro(nameof(CatracaNeokorosAbstract), ex);
            }
        }
    }
}