using EMCatraca.Core.Dominio;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.Server.Repositorios.API
{
    public class RepositorioColaboradorAPI : IRepositorioColaborador
    {
        private readonly IAPIConexao _apiConexao;

        public RepositorioColaboradorAPI(IAPIConexao apiConexao)
        {
            _apiConexao = apiConexao;
        }

        public Colaborador ConsulteColaborador(int idColaborador)
        {
            return _apiConexao.Get<Colaborador>("Colaborador", $"ConsulteColaborador?idColaborador={idColaborador}");
        }

        public IEnumerable<Colaborador> ConsulteTodosColaboradorAtivos()
        {
            return _apiConexao.Get<List<Colaborador>>("Colaborador", "ConsulteTodosColaboradorAtivos");
        }

        public bool ColaboradorEstaAtivo(int idColaborador)
        {
            return _apiConexao.Get<bool>("Colaborador", $"ColaboradorEstaAtivo?idColaborador={idColaborador}");
        }
    }
}
