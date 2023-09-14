using EMCatraca.Core.Dominio;
using EMCatraca.Neokoros;
using EMCatraca.RegrasAcesso.Neokoros.EducandarioVilaBoa;

namespace EMCatraca.RegrasAcesso.EducandarioVilaBoa
{
	public class ControladorDeCatraca : ControladorDeCatracaNeokoros
	{
        protected override CatracaNeokorosAbstract CrieCatracaNeokoros(Dispositivo Catraca)
        {
            return new CatracaNeokorosEducandarioVilaBoa(Catraca);
        }
    }
}