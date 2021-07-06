using  Acesso.Dominio;
using System.Collections.Generic;

namespace Acesso.Interfaces
{
    public interface IRepositorioSerieTurma
    {
        List<SerieTurma> ConsulteTodasSeriesTurmas();
    }
}
