using  Acesso.Dominio;
using Acesso.Core.Services;
using Acesso.TopData;

namespace Acesso.RegrasAcesso.delos
{
	public class ControladorDeCatraca : ControladorDeCatracaTopData
	{
		protected override CatracaTopDataAbstract CrieCatracaTopData(Dispositivo catraca,
																	 IServicoMonitorAcesso servicoMonitorAcesso) =>
			new CatracaTopDataClienteA(catraca, servicoMonitorAcesso);
	}
}