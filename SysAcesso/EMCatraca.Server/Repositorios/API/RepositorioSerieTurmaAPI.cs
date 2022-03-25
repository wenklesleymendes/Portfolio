using EMCatraca.Core.Dominio;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.Server.Repositorios.API
{
    public class RepositorioSerieTurmaAPI : IRepositorioSerieTurma
    {
        private readonly IAPIConexao _apiConexao;

        public RepositorioSerieTurmaAPI(IAPIConexao apiConexao)
        {
            _apiConexao = apiConexao;
        }

        public List<SerieTurma> ConsulteTodasSeriesTurmas()
        {
            return _apiConexao.Get<List<SerieTurma>>("SerieTurma", $"ConsulteTodasSeriesTurmas");
        }
    }
}
