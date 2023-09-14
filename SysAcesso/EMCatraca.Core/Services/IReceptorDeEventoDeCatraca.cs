using EMCatraca.Core.Dominio;

namespace EMCatraca.Core.Services
{
    public interface IReceptorDeEventoDeCatraca
    {
        void RecebaEvento(EventoCatraca evento);
    }
}
