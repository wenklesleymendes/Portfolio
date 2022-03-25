using EMCatraca.Core.Dominio;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.Server.Repositorios.API
{
    public class RepositorioAtributosAdicionaisAPI : IRepositorioAtributosAdicionais
    {
        private readonly IAPIConexao _apiConexao;

        public RepositorioAtributosAdicionaisAPI(IAPIConexao apiConexao)
        {
            _apiConexao = apiConexao;
        }

        public IEnumerable<AtributosAdicionais> ConsulteTodosAtributosAdcionais()
        {
            return _apiConexao.Get<IEnumerable<AtributosAdicionais>>("AtributosAdicionais", "ConsulteTodosAtributosAdcionais");
        }
    }
}
