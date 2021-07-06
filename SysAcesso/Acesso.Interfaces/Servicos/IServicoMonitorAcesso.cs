using Acesso.Dominio;
using Acesso.Interfaces.Servicos;
using System.Collections.Generic;

namespace Acesso.Servicos
{
    [SingletonService]
    public interface IServicoMonitorAcesso : IService
    {
        event EventoCatracaEvent AoDispararEvento;

        IEnumerable<Dispositivo> ObtenhaCatracas();

        void AdicioneEvento(EventoCatraca evento);
    }
}
