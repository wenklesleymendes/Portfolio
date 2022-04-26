using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Core.Services;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace EMCatraca.TopData
{
    public abstract class ControladorDeCatracaTopData : IControladorDeCatraca
    {
        private readonly List<CatracaTopDataAbstract> _catracasTopData = new List<CatracaTopDataAbstract>();

        public void Inicie(IServicoMonitorAcesso servicoMonitorAcesso)
        {
            var catracas = servicoMonitorAcesso.ObtenhaCatracas();

            foreach (var catraca in catracas)
            {
                var dadosDaExecucao = $"{ObtenhaDadosCorrenteDaExecucao()}";
                var dadosCatraca = $"-CATRACA({ catraca.Codigo})-IP:{catraca.IpCatraca}-PORTA:{catraca.PortaCatraca}";

                LogAuditoria.Escreva($"{dadosDaExecucao}{dadosCatraca}", nameof(ControladorDeCatracaTopData));

                _catracasTopData.Add(CrieCatracaTopData(catraca, servicoMonitorAcesso));

                Thread.Sleep(500);
            }
        }

        public void Pare()
        {
            foreach (CatracaTopDataAbstract catracaTopData in _catracasTopData)
            {
                catracaTopData.PareCatraca();
            }
        }

        protected abstract CatracaTopDataAbstract CrieCatracaTopData(Dispositivo catraca, IServicoMonitorAcesso servicoMonitorAcesso);

        private static string ObtenhaDadosCorrenteDaExecucao()
        {
            var rastreamentoDePilhas = new System.Diagnostics.StackTrace(1, true);
            System.Diagnostics.StackFrame[] quadroPilha = rastreamentoDePilhas.GetFrames();

            int linhaCorrententeDoChamado = quadroPilha[0].GetFileLineNumber();
            var projetoMetodoCorrente = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString();

            return $"Ln{linhaCorrententeDoChamado}/{projetoMetodoCorrente}";
        }

    }
}
