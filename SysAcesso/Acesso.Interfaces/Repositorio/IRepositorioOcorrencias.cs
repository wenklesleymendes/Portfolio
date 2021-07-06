using  Acesso.Dominio;
using System.Collections.Generic;

namespace Acesso.Interfaces
{
    public interface IRepositorioOcorrencias
    {
        bool ExisteOcorrencias(int idPessoa, int tipoPessoa, string ocorrencias);

        IEnumerable<Ocorrencia> ConsulteTodosOcorrencias();
    }
}
