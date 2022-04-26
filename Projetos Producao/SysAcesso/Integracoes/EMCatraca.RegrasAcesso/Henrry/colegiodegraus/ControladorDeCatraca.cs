using EMCatraca.Core.Dominio;
using EMCatraca.Core.Services;
using EMCatraca.Henry;
using EMCatraca.RegrasAcesso.Henrry.colegiodegraus;

namespace EMCatraca.RegrasAcesso.colegiodegraus
{
    public class ControladorDeCatraca : ControladorDeCatracaHenry
	{
		protected override CatracaHenryAbstract CrieCatracaHenry(Dispositivo catraca,
			IServicoMonitorAcesso servicoMonitorAcesso) => 
			new CatracaHenryColegioDegraus(catraca, servicoMonitorAcesso);
	}
}