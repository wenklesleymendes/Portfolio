using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using ValidacaoExternaCatraca.Terabyte.ValidacoesDeAcesso;

namespace ValidacaoExterna
{
    [Guid("5A63F31D-085C-427B-8B3A-E52DA2166C00"),
     ClassInterface(ClassInterfaceType.None),
     ComSourceInterfaces(typeof(Validador_Events)),
     ComVisible(true)]
    public class Validador : IValidador
    {
        public bool ConsultarCodigo(string codigo, out string nome)
        {
            try
            {
                new TraceCatraca(new Catraca()).WriteLog($"{ObtenhaMetodoLog()}");
                var controlador = ControladorCatracaNeokorosLoader.CarregueControlador();
                var pessoa = controlador.ConsulteCodigo(codigo);
                new TraceCatraca(new Catraca()).WriteLog($"{ObtenhaMetodoLog()} (CONCLUÍDO)\n");
                if (pessoa != null)
                {
                    nome = pessoa.Nome;
                    return true;
                }
                else
                {
                    nome = $"{pessoa.TipoPessoa.ToString()} NÃO CADASTRADO";
                    return false;
                }
            }
            catch (Exception ex)
            {
                new TraceCatraca(new Catraca()).WriteLog($"{ObtenhaMetodoLog()}");
                new TraceCatraca(new Catraca()).WriteLog($"{ObtenhaLinhaLog()}Falha ao consultar o código solicitado {codigo}\n");
                nome = $"NÃO CADASTRADO";
                return false;
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

        /*
         *  ' ValidarAcesso(codigo, dataHora, numTerminal)->(mensagem1, mensagem2, acessoEsperado, result)
            ' Função chamada quando uma pessoa é reconhecida pela impressão digital ou teclado ou cartão.
            ' entrada: codigo = código da pessoa reconhecida
            '                   Se o código terminar com #C é porque foi uma entrada de  teclado ou cartão
            '          dataHora = data/hora obtida a partir do relógio de ponto
            '          numTerminal = número identificador do terminal
            ' saída:   retorno: True se a pessoa foi encontrada no banco, False se não foi
            '          (o sistema acionará os leds de acordo com isso)
            '          mensagem1 = mensagem para linha 1 (será truncada para 16 caracteres)
            '          mensagem2 = mensagem para linha 2 (será truncada para 16 caracteres)
            '          acessoEsperado = o que o sistema deve fazer
            '          = 3 não aciona nada (para coletores somente ponto)
            '          = 4 liberar para entrada
            '          = 5 liberar para saída
            '          = 6 liberar para ambos os lados
            ' (se acessoEsperado = 4, 5 ou 6 a função RegistrarGiro será chamada quando girar a catraca)
         */
        public bool ValidarAcesso(string codigo, DateTime dataHora, int numeroTerminal,
            out string mensagem1, out string mensagem2,
            out int acessoEsperado)
        {
            try
            {
                var catraca = new Catraca() { Codigo = numeroTerminal, Descricao = $"Catraca {numeroTerminal}" };
                new TraceCatraca(catraca).WriteLog($"{ObtenhaMetodoLog()}");
                var controlador = ControladorCatracaNeokorosLoader.CarregueControlador();
                var retornoDeValidacaoDeAcesso = controlador.ValideAcesso(codigo, dataHora, numeroTerminal);
                new TraceCatraca(catraca).WriteLog($"{ObtenhaMetodoLog()} (CONCLUÍDO)\n");

                mensagem1 = retornoDeValidacaoDeAcesso.Mensagem1;
                mensagem2 = retornoDeValidacaoDeAcesso.Mensagem2;
                acessoEsperado = (int)retornoDeValidacaoDeAcesso.AcessoEsperado;

                return retornoDeValidacaoDeAcesso.Sucesso;
            }
            catch (Exception)
            {
                var catraca = new Catraca() { Codigo = numeroTerminal, Descricao = $"Catraca {numeroTerminal}" };
                new TraceCatraca(catraca).WriteLog($"{ObtenhaMetodoLog()}");
                new TraceCatraca(catraca).WriteLog($"Falha ao validar acesso do código: {codigo}, no terminal: {numeroTerminal}\n");
                mensagem1 = "Não encontrado!";
                mensagem2 = "Acesso negado!";
                acessoEsperado = (int)EnumAcessoEsperado.EsperadoProibido;
                return false;
            }
        }

        private RetornoDeValidacaoDeAcesso ObtenhaRetornoValidacaoAcesso(string codigo, DateTime dataHora, int numTerminal)
        {
            var catraca = new EMCatraca.Core.Dominio.Catraca() { Codigo = numTerminal, Descricao = $"Catraca {numTerminal}" };
            new TraceCatraca(catraca).WriteLog($"{ObtenhaMetodoLog()}");
            new TraceCatraca(catraca).WriteLog($"Validação de acesso do código: {codigo}, no terminal: {numTerminal}");

            var retornoDeValidacaoDeAcesso = new RetornoDeValidacaoDeAcesso() { Sucesso = true, Mensagem1 = "NOME ALUNO", Mensagem2 = "LIBERADO OU NÂO" };
            new TraceCatraca(catraca).WriteLog($"Falha ao validar acesso do código: {codigo}, no terminal: {numTerminal}");
            return retornoDeValidacaoDeAcesso;
        }

        public void RegistrarGiro(string codigo, DateTime dataHora, int numeroTerminal, int direcaoGiro)
        {
            try
            {
                var catraca = new Catraca() { Codigo = numeroTerminal, Descricao = $"Catraca {numeroTerminal}" };
                new TraceCatraca(catraca).WriteLog($"{ObtenhaMetodoLog()}");
                new TraceCatraca(catraca).WriteLog($"Registro de giro do código: {codigo}, no terminal: {numeroTerminal}, direcao do giro: {direcaoGiro}");
                var controlador = ControladorCatracaNeokorosLoader.CarregueControlador();
                controlador.RegistreGiro(codigo, dataHora, numeroTerminal, direcaoGiro);
                new TraceCatraca(catraca).WriteLog($"{ObtenhaMetodoLog()} (CONCLUÍDO)\n");
            }
            catch (Exception ex)
            {
                var catraca = new Catraca() { Codigo = numeroTerminal, Descricao = $"Catraca {numeroTerminal}" };
                new TraceCatraca(catraca).WriteLog($"{ObtenhaMetodoLog()}");
                new TraceCatraca(catraca).WriteLogError($"Falha ao registrar acesso do código: {codigo}, no terminal: {numeroTerminal}, direcao do giro: {direcaoGiro}\n", ex);
            }
        }
    }
}
