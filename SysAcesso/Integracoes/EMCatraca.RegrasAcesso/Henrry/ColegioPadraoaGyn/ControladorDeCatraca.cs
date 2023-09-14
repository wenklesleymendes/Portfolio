using EMCatraca.Core.Dominio;
using EMCatraca.Core.Services;
using EMCatraca.Henry;
using EMCatraca.RegrasAcesso.Henrry.colegiopadraoapgyn;

namespace EMCatraca.RegrasAcesso.colegiopadraoapgyn
{
	public class ControladorDeCatraca : ControladorDeCatracaHenry
	{
		protected override CatracaHenryAbstract CrieCatracaHenry(Dispositivo catraca,
		  IServicoMonitorAcesso servicoMonitorAcesso) => 
		  new CatracaHenryColegioPadrao(catraca, servicoMonitorAcesso);
	}
}