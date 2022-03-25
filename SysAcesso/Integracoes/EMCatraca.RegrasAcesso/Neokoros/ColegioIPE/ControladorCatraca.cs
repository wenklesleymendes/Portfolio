using EMCatraca.Core.Dominio;
using EMCatraca.Neokoros;

namespace EMCatraca.RegrasAcesso.Neokoros.Colegio.IPE
{
    public class ControladorCatraca : ControladorDeCatracaNeokoros
	{
        protected override CatracaNeokorosAbstract CrieCatracaNeokoros(Dispositivo Catraca)
        {
            return new CatracaNeokorosIPE(Catraca);
        }
    }
}