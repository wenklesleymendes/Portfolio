using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.WebApi.Fachadas
{
    public class FachadaOcorrencias
    {
        private readonly IRepositorioOcorrencias _repositorioDeOcorrencias = FabricaDeRepositorios.Instancia.CrieRepositorioOcorrencias();

        public bool ExisteOcorrencias(int idPessoa, int tipoPessoa, string ocorrencias)
        {
            return _repositorioDeOcorrencias.ExisteOcorrencias(idPessoa, tipoPessoa, ocorrencias);
        }

        public IEnumerable<Ocorrencia> ConsulteTodosOcorrencias()
        {
            return _repositorioDeOcorrencias.ConsulteTodosOcorrencias();
        }

    }
}