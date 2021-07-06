using Acesso.Dominio;
using Acesso.Interfaces;
using Acesso.Servicos;
using System.Collections.Generic;

namespace Acesso.Control.ID
{
    public abstract class ControleDeCatracaControlID : IControladorDeCatraca
    {
		private readonly List<CatracaControlIDAbstract> _catracasContolID = new List<CatracaControlIDAbstract>();

		public void Inicie(IServicoMonitorAcesso servicoMonitorAcesso)
		{
			IEnumerable<Dispositivo> catracas = servicoMonitorAcesso.ObtenhaCatracas();
			foreach (Dispositivo catraca in catracas)
			{
				AuditoriaLog.WriteLog($"Iniciando catraca: {catraca.Codigo}");
				_catracasContolID.Add(CrieCatracaControlID(catraca, servicoMonitorAcesso));
			}
		}

        public void Inicie(IServicoMonitorAcesso servicoMonitorAcesso)
        {
            throw new System.NotImplementedException();
        }

        public void Pare()
		{
			foreach (CatracaControlIDAbstract catracaControlID in _catracasContolID)
			{
				catracaControlID.PareCatraca();
			}
		}

		/// <summary>
		/// Cria objeto catraca
		/// </summary>
		/// <param name="catraca"></param>
		/// <param name="servicoMonitorAcesso"></param>
		/// <returns></returns>
		protected abstract CatracaControlIDAbstract CrieCatracaControlID(Dispositivo catraca, IServicoMonitorAcesso servicoMonitorAcesso);
	}
}
