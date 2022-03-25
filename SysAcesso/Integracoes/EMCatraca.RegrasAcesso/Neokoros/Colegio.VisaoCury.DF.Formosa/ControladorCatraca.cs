using EMCatraca.Core.Dominio;
using EMCatraca.Neokoros;
using EMCatraca.RegrasAcesso.Neokoros.Colegio.VisaoCury.DF.Formosa;

namespace EMCatraca.RegrasAcesso.Colegio.VisaoCury.DF.Formosa
{
	public class ControladorCatraca : ControladorDeCatracaNeokoros
	{
        protected override CatracaNeokorosAbstract CrieCatracaNeokoros(Dispositivo Catraca)
        {
            return new CatracaNeokorosVisaoCury(Catraca);
        }
    }
}