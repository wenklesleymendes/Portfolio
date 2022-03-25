using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Services;
using EMCatraca.Server.Excecoes;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TcpIp;
using ValidacaoExternaCatraca.Terabyte.ValidacoesDeAcesso;

namespace EMCatraca.Neokoros
{
    public abstract class CatracaNeokorosAbstract
    {
        protected ControleAcesso ConfiguracaoAcesso;
        protected TraceCatraca TraceCatraca { get; set; }
        protected Catraca Catraca { get; private set; }

        protected abstract Pessoa ObtenhaPessoa(string codigo);
        protected abstract string ObtenhaMatriculaPeloRfid(string codigo);
        protected abstract void RegistreAcessoPessoa(Pessoa pessoa, EnumSentidoGiro sentidoDoGiro);

        //private TcpIpServidor ServidorMonitor = new TcpIp.TcpIpServidor();
        private InformacaoConexao ConfiguracaoServidor;
        private Pessoa _pessoa = null;
        private EnumSentidoGiro _sentidoDoGiro = EnumSentidoGiro.Indefinido;
        private EnumTipoAcesso _tipoAcesso = EnumTipoAcesso.Indefinido;
        private string _indetificacaoDaPessoa;

        private const string CaracterDeControleDeCartao = "#C";
        private const string CaracterDeControleDeTeclado = "#P";
        private const string CaracterDeControleDeNumeroDeSerieDoCartao = "#S";
        private const string CaracterDeControleDeTag = "#T";

        public CatracaNeokorosAbstract(Catraca catraca)
        {
            TraceCatraca = new TraceCatraca(catraca);
            Catraca = catraca;
            AtualizeConfiguracoes();
        }

        private void AtualizeConfiguracoes()
        {
            ConfiguracaoAcesso = MapeadorArquivoJson.CarreguerArquivoJson<ControleAcesso>("emcatraca.acesso.cfg");
            ConfiguracaoServidor = MapeadorArquivoJson.CarreguerArquivoJson<InformacaoConexao>("emcatraca.servidor.cfg");

            var tipoServidor = "Servidor do Monitor de Acesso";
            //ServidorMonitor.IniciarServidorTcpIp(ConfiguracaoServidor.IP, ConfiguracaoServidor.PortaTcpIp, tipoServidor);
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

        public Pessoa ConsultePessoa(string dadosRecebidos)
        {
            TraceCatraca.WriteLog($"{ObtenhaMetodoLog()}");
            TraceCatraca.WriteLog($"{ObtenhaLinhaLog()}Iniciando consulta -> código: {dadosRecebidos}");
            var pessoa = RecuperePessoa(dadosRecebidos);
            TraceCatraca.WriteLog($"{ObtenhaMetodoLog()}");
            TraceCatraca.WriteLog($"{ObtenhaLinhaLog()}Resultado da consulta -> pessoa: {pessoa.TipoPessoa.ToString()}, nome: {pessoa.Nome}, código: {pessoa.Id}");
            return pessoa;
        }

        private Pessoa RecuperePessoa(string dadosRecebidos)
        {
            var formaAcesso = string.Empty;
            var codigo = string.Empty;
            var retorno = new RetornoDeValidacaoDeAcesso();

            TraceCatraca.WriteLog($"{ObtenhaMetodoLog()}");
            TraceCatraca.WriteLog($"{ObtenhaLinhaLog()}Solicitação de validação -> código: {dadosRecebidos}");

            if (string.IsNullOrEmpty(dadosRecebidos))
            {
                return null;
            }

            var aDadosRecebidos = dadosRecebidos.Split('#');
            if (aDadosRecebidos.Length >= 0)
            {
                codigo = aDadosRecebidos[0];
            }

            _tipoAcesso = EnumTipoAcesso.Indefinido;
            if (aDadosRecebidos.Length > 1)
            {
                formaAcesso = $"#{aDadosRecebidos[1]}";
                switch (formaAcesso)
                {
                    case CaracterDeControleDeCartao:
                        _tipoAcesso = EnumTipoAcesso.Cartao;
                        break;
                    case CaracterDeControleDeTag:
                        _tipoAcesso = EnumTipoAcesso.RFID;
                        break;
                    case CaracterDeControleDeTeclado:
                        _tipoAcesso = EnumTipoAcesso.Teclado;
                        break;
                }
            }

            if (_tipoAcesso == EnumTipoAcesso.RFID || (_tipoAcesso == EnumTipoAcesso.Cartao && codigo.Length >= 12))
            {
                codigo = ObtenhaMatriculaPeloRfid(codigo);
            }

            try
            {
                TraceCatraca.WriteLog($"{ObtenhaLinhaLog()}Iniciando identificação -> dados recebidos: {dadosRecebidos}, código encontrado: {codigo}, forma de acesso: {_tipoAcesso.ToString()}");

                _pessoa = ObtenhaPessoa(codigo.ToString());

                TraceCatraca.WriteLog($"{ObtenhaLinhaLog()}Foi indetificada -> tipo: {_pessoa.RecuperaTipo()}, código: {_pessoa.Id}, nome: {_pessoa.Nome}");
                return _pessoa;
            }
            catch (Exception)
            {
                TraceCatraca.WriteLog($"{ObtenhaLinhaLog()}Registro não encontrado -> dados recebidos: {dadosRecebidos}");
                throw new AcessoNegadoException($"Falha ao identificar -> dados recebidos: {dadosRecebidos}");
            }
        }

        public RetornoDeValidacaoDeAcesso ValideAcesso(string dadosRecebidos)
        {
            TraceCatraca.WriteLog($"{ObtenhaMetodoLog()}");
            var retorno = new RetornoDeValidacaoDeAcesso();
            try
            {
                _pessoa = RecuperePessoa(dadosRecebidos);
                TraceCatraca.WriteLog($"{ObtenhaMetodoLog()}");
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

                EventoCatraca evento;

                string msgRestricao = MonteMensagemRestricao(_pessoa);
                if (!string.IsNullOrEmpty(msgRestricao))
                {
                    retorno.Mensagem1 = _pessoa.Nome;
                    retorno.Mensagem2 = "Acesso liberado!";
                    retorno.AcessoEsperado = EnumAcessoEsperado.EsperadoAguardarAmbas;
                    evento = EventoCatraca.CrieAcessoLiberadoComRestricao(EnumSentidoGiro.Indefinido, _pessoa, Catraca, msgRestricao);
                }
                else
                {
                    retorno.Mensagem1 = _pessoa.Nome;
                    retorno.Mensagem2 = "Acesso liberado!";
                    retorno.AcessoEsperado = EnumAcessoEsperado.EsperadoAguardarAmbas;
                    evento = EventoCatraca.CrieAcessoLiberado(EnumSentidoGiro.Indefinido, _pessoa, Catraca);
                }

                //ServidorMonitor.EnviarMensagem(JsonConvert.SerializeObject(evento));

                TraceCatraca.WriteLog($"{ObtenhaLinhaLog()}Acesso liberado -> tipo: {_pessoa.RecuperaTipo()}, código: {_pessoa?.Id}, nome: {_pessoa?.Nome}");
                return retorno;
            }
            catch (AcessoNegadoException ex)
            {
                if (_pessoa == null)
                {
                    _pessoa = new Pessoa() { Nome = "Não cadastrado!" };
                }
                var evento = EventoCatraca.CrieAcessoNegado(EnumSentidoGiro.Indefinido, _pessoa, Catraca);
                evento.Mensagem2 = ex.Message;
               
                //ServidorMonitor.EnviarMensagem(JsonConvert.SerializeObject(evento));

                TraceCatraca.WriteLog($"{ObtenhaLinhaLog()}Acesso negado -> método da validação: \"{ex.TargetSite.Name}\"");

                retorno.Mensagem1 = _pessoa.Nome;
                retorno.Mensagem2 = "Acesso negado!";
                retorno.AcessoEsperado = EnumAcessoEsperado.EsperadoProibido;
                return retorno;
            }
        }

        private EnumSentidoGiro ObtenhaDirecaoGiro(int direcaoGiro)
        {
            switch (direcaoGiro)
            {
                case 1:
                    return EnumSentidoGiro.Entrada;
                case 2:
                    return EnumSentidoGiro.Saida;
            }
            return EnumSentidoGiro.Indefinido;
        }

        public void RegisteAcesso(string codigo, DateTime dataHora, int numeroTerminal, int direcaoGiro)
        {
            TraceCatraca.WriteLog($"{ObtenhaMetodoLog()}");
            TraceCatraca.WriteLog($"{ObtenhaLinhaLog()}Registrando o Acesso -> código: {codigo}, data: {dataHora.ToString("dd/MM/yyyy HH:mm")}, terminal: {numeroTerminal}, direcao do giro: {direcaoGiro}");
            _sentidoDoGiro = ObtenhaDirecaoGiro(direcaoGiro);
            try
            {
                _pessoa = RecuperePessoa(codigo);
                TraceCatraca.WriteLog($"{ObtenhaMetodoLog()}");
                if (_pessoa != null)
                {
                    RegistreAcessoPessoa(_pessoa, _sentidoDoGiro);
                    TraceCatraca.WriteLog($"{ObtenhaLinhaLog()}Sentido do Giro -> giro: {_sentidoDoGiro.ToString()}");
                }
                _pessoa = null;
            }
            catch (Exception ex)
            {
                TraceCatraca.WriteLogError($"{ObtenhaLinhaLog()}Falha ao registrar o acesso -> tipo: {_pessoa.RecuperaTipo()}, código: {_pessoa?.Id}, nome: {_pessoa?.Nome}", ex);
            }
        }

        private static string ObtenhaMetodoLog()
        {
            StackFrame stackFrame = new StackFrame(1, true);
            var metodo = stackFrame.GetMethod().ToString();

            var stackTrace = new StackTrace();
            var methodBase = stackTrace.GetFrame(1).GetMethod();
            var Class = methodBase.ReflectedType;
            var Namespace = Class.Namespace;         //Added finding the namespace
            Console.WriteLine(Namespace + "." + Class.Name + "." + methodBase.Name);


            return $"Método -> {Namespace + "." + Class.Name + "." + methodBase.Name}";
        }

        private static string ObtenhaLinhaLog()
        {
            StackFrame stackFrame = new StackFrame(1, true);
            var linha = stackFrame.GetFileLineNumber();
            
            return $"Ln:{linha} -> ";
        }
    }
}

