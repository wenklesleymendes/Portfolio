using EMCatraca.Core.RemoteServices;
using EMCatraca.Server;
using EMCatraca.Server.Controladores;
using System;

namespace EMCatraca.ServiceHostDebugger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Executando no diretório: " + Environment.CurrentDirectory);
            Console.WriteLine($"\n{ObtenhaLinhaLog()} - Iniciando Service Host Debugger.\n");

            ServiceHostLoader.IntercepteExcecaoNaoTratada();
            ServiceHostLoader.CarregueServicos();

            ControladorCatracaLoader.CarregueControlador();

            Console.WriteLine("\nPronto! Clique em algo para encerrar!\n");
            Console.ReadKey();

            ControladorCatracaLoader.PareControlador();
        }

        private static string ObtenhaLinhaLog()
        {
            var rastreamentoDePilhas = new System.Diagnostics.StackTrace(1, true);
            System.Diagnostics.StackFrame[] quadroPilha = rastreamentoDePilhas.GetFrames();

            int linhaCorrententeDoChamado = quadroPilha[0].GetFileLineNumber();
            var nomeMetadoCorrente = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString();

            return $"Ln{linhaCorrententeDoChamado}/{nomeMetadoCorrente}";
        }
    }
}
