using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Core.Services;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.Control.ID
{
    public abstract class ControleDeCatracaControlID : IControladorDeCatraca
	{
		private readonly List<CatracaControlIDAbstract> _catracasContolID = new List<CatracaControlIDAbstract>();

		public void Inicie(IServicoMonitorAcesso servicoMonitorAcesso)
		{
			IEnumerable<Dispositivo> catracas = servicoMonitorAcesso.ObtenhaCatracas();
			foreach (Dispositivo catraca in catracas)
			{
				LogAuditoria.Escreva($"Iniciando catraca: {catraca.Codigo}", nameof(ControleDeCatracaControlID));

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

		/// <summary>
		/// Cria objeto catraca
		/// </summary>
		/// <param name="catraca"></param>
		/// <param name="servicoMonitorAcesso"></param>
		/// <returns></returns>
		protected abstract CatracaControlIDAbstract CrieCatracaControlID(Dispositivo catraca, IServicoMonitorAcesso servicoMonitorAcesso);
	}
}
