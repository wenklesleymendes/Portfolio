using EMCatraca.Core.Dominio;
using EMCatraca.Core.RemoteServices;
using System.Collections.Generic;

namespace EMCatraca.Core.Services
{
    [SingletonService]
    public interface IServicoMonitorAcesso : IService
    {
        event EventoCatracaEvent AoDispararEvento;

        IEnumerable<Dispositivo> ObtenhaCatracas();

        void AdicioneEvento(EventoCatraca evento);
    }
}
