using EMCatraca.Core;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Logs;
using EMCatraca.Core.Services;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.Henry
{
	public abstract class ControladorDeCatracaHenry : IControladorDeCatraca
	{
		private readonly List<CatracaHenryAbstract> _catracasHenry = new List<CatracaHenryAbstract>();

		public void Inicie(IServicoMonitorAcesso servicoMonitorAcesso)
		{
			IEnumerable<Dispositivo> catracas = servicoMonitorAcesso.ObtenhaCatracas();

			foreach (Dispositivo catraca in catracas)
			{
				LogAuditoria.Escreva($"Iniciando catraca: {catraca.Codigo}",nameof(ControladorDeCatracaHenry));

				_catracasHenry.Add(CrieCatracaHenry(catraca, servicoMonitorAcesso));
			}
		}

		public void Pare()
		{
			foreach (CatracaHenryAbstract catracaHenry in _catracasHenry)
			{
				catracaHenry.PareCatraca();
			}
		}

		protected abstract CatracaHenryAbstract CrieCatracaHenry(Dispositivo catraca,
																 IServicoMonitorAcesso servicoMonitorAcesso);

		private static string ObtenhaLinhaLog()
		{
			var rastreamentoDePilhas = new System.Diagnostics.StackTrace(1, true);
			System.Diagnostics.StackFrame[] quadroPilha = rastreamentoDePilhas.GetFrames();

			int linhaCorrententeDoChamado = quadroPilha[0]
				.GetFileLineNumber();

			var projetoMetodoCorrente = System.Reflection.MethodBase.GetCurrentMethod()
											  .DeclaringType.ToString();

			return $"Ln:{linhaCorrententeDoChamado}/{projetoMetodoCorrente}";
		}
	}
}