using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.WebApi.Fachadas
{
    public class FachadaDeAtributosAdicionais
    {
        private readonly IRepositorioAtributosAdicionais _repositorioDeAtributosAdicionais = FabricaDeRepositorios.Instancia.CrieRepositorioAtributosAdicionais();

        public IEnumerable<AtributosAdicionais> ConsulteTodosAtributosAdcionais()
        {
            return _repositorioDeAtributosAdicionais.ConsulteTodosAtributosAdcionais();
        }
    }
}