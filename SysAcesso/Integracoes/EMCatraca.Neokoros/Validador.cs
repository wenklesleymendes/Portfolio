using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Server;
using EMCatraca.Server.Controladores;
using EMCatraca.Server.Interfaces;
using System;
using System.Runtime.InteropServices;
using ValidacaoExterna.Neokoros;

namespace ValidacaoExterna
{
    [Guid("5A63F31D-085C-427B-8B3A-E52DA2166C00"), ClassInterface(ClassInterfaceType.None),
     ComSourceInterfaces(typeof(Validador_Events)), ComVisible(true)]
    public class Validador : IValidador
    {
        private string _mensagemFalhou = "Matricula não encontrada";
        private string _messagenLog;
        private string _nomeLog = "Auditoria";

        public bool ConsultarCodigo(string codigo, out string nome)
        {
            try
            {
                _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(ConsultarCodigo)}:Paramentro Codigo={codigo}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                // Regra de validação neokoros
                if (Equals(codigo, "1"))
                {
                    nome = $"Comunicação com Dokeu Validada com sucesso!";

                    _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(ConsultarCodigo)}:"+
                                   $" STRING 2.l {nome}";
                    AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                    return true;
                }

                IControladorDeCatracaNeokoros iControlador = ControladorCatracaNeokorosLoader.CarregueControlador();

                Pessoa pessoa = iControlador.ConsulteCodigo(codigo);

                _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(ConsultarCodigo)}: "+
                               "Pessoa= Matricula:{pessoa.Id}, Tipo: {pessoa.TipoPessoa}, Nome: {pessoa.Nome}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                if (pessoa != null)
                {
                    nome = pessoa.Nome;

                    _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(ConsultarCodigo)}:"+
                                   " Retorno: true - Pessoa Nome: {pessoa.Nome}";
                    AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                    return true;
                }

                nome = $"{_mensagemFalhou} Tipo Pessoa:{pessoa.TipoPessoa}";

                _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(ConsultarCodigo)}: Erro: {nome} ";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);
                
                return false;
            }
            catch (Exception erro)
            {
                nome = _mensagemFalhou;

                AuditoriaLog.EscrevaErro(_nomeLog, erro);

                return false;
            }
        }

        public bool ConsultarCodigoNovo(string codigo, out string nome, out string salaTurma, out string outros)
        {
            _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(ConsultarCodigoNovo)}:Paramentro Codigo={codigo}";
            AuditoriaLog.Escreva(_nomeLog, _messagenLog);

            salaTurma = null;
            outros = null;

            try
            {
                // Regra de validação neokoros
                if (Equals(codigo, "1"))
                {
                    nome = $"Comunicação com Dokeu Validada com sucesso!";
                    _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(ConsultarCodigoNovo)}: STRING 2.l {nome}";
                    AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                    return true;
                }

                IControladorDeCatracaNeokoros iControlador = CarregadorBioFacialConfiguracoes.CarregueAssemblys();
                Pessoa pessoa = iControlador.ConsulteCodigo(codigo);

                TurmaMontada turmaMontada = iControlador.ConsulteTurmaMontada(codigo);

                if (pessoa != null)
                {
                    nome = pessoa.Nome;
                    salaTurma = $"Sala: {turmaMontada.SerieDescricao} Turma: {turmaMontada.TurmaDescricao}";
                    outros = "";

                    _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(ConsultarCodigoNovo)}:" +
                        $"PESSOA==> " +
                        $"Codigo={pessoa.Id}" +
                        $"Nome={nameof(pessoa.Nome)} " +
                        $"Sala Turma={salaTurma} " +
                        $"Outros={outros}";
                    AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                    return true;
                }

                nome = $"{_mensagemFalhou} Tipo Pessoa:{pessoa.TipoPessoa}";

                _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(ConsultarCodigoNovo)}: Erro: {nome} ";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                return false;
            }
            catch (Exception erro)
            {              
                nome = _mensagemFalhou;

                AuditoriaLog.EscrevaErro(_nomeLog, erro);

                return false;
            }
        }

        public bool ValidarAcesso(string codigo, DateTime dataHora, int numeroTerminal,
                                  out string mensagem1, out string mensagem2, out int acessoEsperado)
        {
            var catraca = new Dispositivo()
            {
                Codigo = numeroTerminal,
                Descricao = $"Catraca {numeroTerminal}"
            };

            try
            {
                if (Equals(codigo, "1"))
                {
                    mensagem1 = "1";
                    mensagem2 = $"Dokeu Validado Método:{nameof(ValidarAcesso)}";
                    acessoEsperado = 6;
                    return true;
                }

                IControladorDeCatracaNeokoros iControlador = CarregadorBioFacialConfiguracoes.CarregueAssemblys();
                RetornoDeValidacaoDeAcesso retornoAcesso = iControlador.ValideAcesso(codigo, dataHora, numeroTerminal);

                mensagem1 = retornoAcesso.Mensagem1;
                mensagem2 = retornoAcesso.Mensagem2;
                acessoEsperado = (int)retornoAcesso.AcessoEsperado;

                if (retornoAcesso.Sucesso)
                {
                    RegistrarGiro(codigo, dataHora, numeroTerminal, retornoAcesso.GiroId);
                    _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(ValidarAcesso)}: Giro registrado ao validar acesso";
                    AuditoriaLog.Escreva(_nomeLog, _messagenLog);
                }

                _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(ValidarAcesso)}:"+
                                $" Acesso Esperado: {acessoEsperado}-{retornoAcesso.AcessoEsperado}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                return retornoAcesso.Sucesso;
            }
            catch (Exception ex)
            {
                AuditoriaLog.EscrevaErro(_nomeLog, ex);

                mensagem1 = "Não encontrado!";
                mensagem2 = "Acesso negado!";
                acessoEsperado = (int)AcessoEsperado.EsperadoProibido;
                
                _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(ValidarAcesso)}:"+
                    $"Mensagem 1: {mensagem1}, " +
                    $"Mensagem 2: {mensagem2}, " +
                    $"Acesso Esperado: {acessoEsperado}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                return false;
            }
        }

        public bool ValidarAcessoeTemperatura(string codigo, string Temperatura, DateTime dataHora, int numeroTerminal,
                                              out string nome, out string mensagem, out int acessoEsperado)
        {
            var catraca = new Dispositivo()
            {
                Codigo = numeroTerminal,
                Descricao = $"Catraca {numeroTerminal}"
            };

            try
            {
                if (Equals(codigo, "1"))
                {
                    nome = "1";
                    mensagem = $"Dokeu Validado Método:{nameof(ValidarAcessoeTemperatura)}";
                    acessoEsperado = 6;
                    return true;
                }

                IControladorDeCatracaNeokoros controlador = ControladorCatracaNeokorosLoader.CarregueControlador();

                RetornoDeValidacaoDeAcesso retornoAcesso = controlador.ValideAcesso(codigo, dataHora, numeroTerminal);

                nome = retornoAcesso.Mensagem1;
                mensagem = retornoAcesso.Mensagem2;
                acessoEsperado = (int)retornoAcesso.AcessoEsperado;

                _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(ValidarAcessoeTemperatura)}:"+
                    $"Nome: {retornoAcesso.Mensagem1}" +
                    $"Mensagem: {retornoAcesso.Mensagem2}" +
                    $"Acesso Esperado: {acessoEsperado}-{retornoAcesso.AcessoEsperado}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                if (retornoAcesso.Sucesso)
                {
                    RegistrarGiro(codigo, dataHora, numeroTerminal, retornoAcesso.GiroId);
                }

                return retornoAcesso.Sucesso;
            }
            catch (Exception ex)
            {
                AuditoriaLog.EscrevaErro(_nomeLog, ex);

                nome = "Não encontrado!";
                mensagem = "Acesso negado!";
                acessoEsperado = (int)AcessoEsperado.EsperadoProibido;

                _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(ValidarAcessoeTemperatura)}:" +
                    $"Nome: {nome}" +
                    $"Mensagem: {mensagem}" +
                    $"Acesso Esperado: {acessoEsperado}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                return false;
            }
        }

        public void RegistrarGiro(string codigo, DateTime dataHora, int numeroTerminal, int direcaoGiro)
        {
            var catraca = new Dispositivo()
            {
                Codigo = numeroTerminal,
                Descricao = $"Catraca {numeroTerminal}"
            };

            try
            {
                _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(RegistrarGiro)}: Parametros: " +
                    $"Registro giro matricula: {codigo}, " +
                    $"Terminal: {numeroTerminal}, " +
                    $"Direção giro: {direcaoGiro}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

                IControladorDeCatracaNeokoros controlador = ControladorCatracaNeokorosLoader.CarregueControlador();

                controlador.RegistreGiro(codigo, dataHora, numeroTerminal, direcaoGiro);

                _messagenLog = $"{nameof(ValidacaoExterna)}.{nameof(Validador)}.{nameof(RegistrarGiro)}: Registrado: " +
                    $"Codigo: {codigo}, " +
                    $"Data/Hora: {dataHora}, " +
                    $"Numero Terminal: {numeroTerminal}," +
                    $"Direçao giro: {direcaoGiro}";
                AuditoriaLog.Escreva(_nomeLog, _messagenLog);

            }
            catch (Exception ex)
            {
                AuditoriaLog.EscrevaErro(_nomeLog, ex);
            }
        }
    }
}