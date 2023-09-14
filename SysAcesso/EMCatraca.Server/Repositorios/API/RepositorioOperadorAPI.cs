using EMCatraca.Core.Dominio;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.Server.Repositorios.API
{
    public class RepositorioOperadorAPI : IRepositorioOperador
    {
        private readonly IAPIConexao _apiConexao;

        public RepositorioOperadorAPI(IAPIConexao apiConexao)
        {
            _apiConexao = apiConexao;
        }

        public Operador ConsulteOperador()
        {
            return _apiConexao.Get<Operador>("Operador", "ConsulteOperador");
        }

        public IEnumerable<Operador> ConsulteTodosOperadorAtivos()
        {
            return _apiConexao.Get<List<Operador>>("Operador", "ConsulteTodosOperadorAtivos");
        }

        public bool ValideOperador(int codigo, string senha)
        {
            return _apiConexao.Get<bool>("Operador", $"ValideOperador?codigo={codigo}&senha={senha}");

        }
    }
}
