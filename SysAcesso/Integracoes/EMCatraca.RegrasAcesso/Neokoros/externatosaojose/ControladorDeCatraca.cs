using EMCatraca.Core.Dominio;
using EMCatraca.Neokoros;

namespace EMCatraca.RegrasAcesso.Neokoros.externatosaojose
{
    public class ControladorDeCatraca : ControladorDeCatracaNeokoros
    {
        protected override CatracaNeokorosAbstract CrieCatracaNeokoros(Dispositivo catraca)
        {
            return new CatracaNeokorosExternatoSaoJose(catraca);
        }
    }
}
