using EMCatraca.Core.Dominio;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.Server.Repositorios.API
{
    public class RepositorioOcorrenciasAPI : IRepositorioOcorrencias
    {
        private readonly IAPIConexao _apiConexao;

        public RepositorioOcorrenciasAPI(IAPIConexao apiConexao)
        {
            _apiConexao = apiConexao;
        }

        public bool ExisteOcorrencias(int idPessoa, int tipoPessoa, string ocorrencias)
        {

            return _apiConexao.Get<bool>("Ocorrencias", $"ExisteOcorrencias?idPessoa={idPessoa}&tipoPessoa={tipoPessoa}&ocorrencias={ocorrencias}");
        }

        public IEnumerable<Ocorrencia> ConsulteTodosOcorrencias()
        {
            return _apiConexao.Get<IEnumerable<Ocorrencia>>("Ocorrencias", "ConsulteTodosOcorrencias");
        }
    }
}
