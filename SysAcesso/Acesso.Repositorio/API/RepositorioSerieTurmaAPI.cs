using  Acesso.Dominio;
using System.Collections.Generic;

namespace Acesso.Interfaces.Repositorios.API
{
    public class RepositorioSerieTurmaAPI : IRepositorioSerieTurma
    {
        public List<SerieTurma> ConsulteTodasSeriesTurmas()
        {
            return APIHelper.Instancia.Get<List<SerieTurma>>("SerieTurma", $"ConsulteTodasSeriesTurmas");
        }
    }
}
