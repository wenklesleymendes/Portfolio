using  Acesso.Dominio;
using System.Collections.Generic;

namespace Acesso.Interfaces.Repositorios.API
{
    public class RepositorioOcorrenciasAPI : IRepositorioOcorrencias
    {
        public bool ExisteOcorrencias(int idPessoa, int tipoPessoa, string ocorrencias)
        {

            return APIHelper.Instancia.Get<bool>("Ocorrencias", $"ExisteOcorrencias?idPessoa={idPessoa}&tipoPessoa={tipoPessoa}&ocorrencias={ocorrencias}");
        }

        public IEnumerable<Ocorrencia> ConsulteTodosOcorrencias()
        {
            return APIHelper.Instancia.Get<IEnumerable<Ocorrencia>>("Ocorrencias", "ConsulteTodosOcorrencias");
        }
    }
}
