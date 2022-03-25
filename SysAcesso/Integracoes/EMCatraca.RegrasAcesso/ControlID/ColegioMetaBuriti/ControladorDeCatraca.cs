using EMCatraca.Control.ID;
using EMCatraca.Core.Dominio;
using EMCatraca.Core.Services;
using EMCatraca.RegrasAcesso.ControlID.ColegioMetaBuriti;

namespace EMCatraca.RegrasAcesso.ColegioMetaBuriti
{
    public class ControladorDeCatraca : ControleDeCatracaControlID
	{
		protected override CatracaControlIDAbstract CrieCatracaControlID(
			Dispositivo catraca, IServicoMonitorAcesso servicoMonitorAcesso) =>
			new CatracaCotrolIDColegioMetaBuritiApi(catraca, servicoMonitorAcesso);
	}
}