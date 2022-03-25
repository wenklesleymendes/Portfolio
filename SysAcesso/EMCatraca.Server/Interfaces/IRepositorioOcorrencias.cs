using EMCatraca.Core.Dominio;
using System.Collections.Generic;

namespace EMCatraca.Server.Interfaces
{
    public interface IRepositorioOcorrencias
    {
        bool ExisteOcorrencias(int idPessoa, int tipoPessoa, string ocorrencias);

        IEnumerable<Ocorrencia> ConsulteTodosOcorrencias();
    }
}
