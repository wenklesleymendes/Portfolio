using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Services;
using EMCatraca.Server;
using System.Collections.Generic;

namespace EMCatraca.Control.ID
{
    public abstract class ControleDeCatracaControlID : IControladorDeCatraca
    {
        private readonly List<CatracaControlIDAbstract> _catracasContolID = new List<CatracaControlIDAbstract>();

        public void Inicie(IServicoMonitorAcesso servicoMonitorAcesso)
        {
            var catracas = servicoMonitorAcesso.ObtenhaCatracas();
            foreach (DispositivoCatraca catraca in catracas)
            {
                LogGeral.WriteLog($"Iniciando catraca: {catraca.Codigo}");
                _catracasContolID.Add(CrieCatracaControlID(catraca, servicoMonitorAcesso));
            }
        }

        public void Pare()
        {
            foreach (CatracaControlIDAbstract catracaControlID in _catracasContolID)
            {
                catracaControlID.PareCatraca();
            }
        }

        protected abstract CatracaControlIDAbstract CrieCatracaControlID(DispositivoCatraca catraca, IServicoMonitorAcesso servicoMonitorAcesso);
    }
}
