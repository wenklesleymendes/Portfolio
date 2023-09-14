using EMCatraca.Core.Dominio;
using System.Collections.Generic;

namespace EMCatraca.Server.Interfaces
{
    public interface IRepositorioSerieTurma
    {
        List<SerieTurma> ConsulteTodasSeriesTurmas();
    }
}
