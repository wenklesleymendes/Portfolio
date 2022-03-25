using EMCatraca.Core.Dominio;
using EMCatraca.Core.Services;
using EMCatraca.TopData;

namespace EMCatraca.RegrasAcesso.TopData.RedeObjetivo_AguasClaras
{
    public class ControladorDeCatraca : ControladorDeCatracaTopData
	{
		protected override CatracaTopDataAbstract
			CrieCatracaTopData(Dispositivo catraca, IServicoMonitorAcesso servicoMonitorAcesso) =>
			new CatracaTopDataColegioObjetivoAguasClarasApi(catraca, servicoMonitorAcesso);
	}
}