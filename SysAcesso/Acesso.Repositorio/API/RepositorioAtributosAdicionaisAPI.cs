using Acesso.Dominio;
using System.Collections.Generic;
using Acesso.Interfaces;

namespace Acesso.Interfaces.Repositorios.API
{
    public class RepositorioAtributosAdicionaisAPI : IRepositorioAtributosAdicionais
    {
        public IEnumerable<AtributosAdicionais> ConsulteTodosAtributosAdcionais()
        {
            return APIHelper.Instancia.Get<IEnumerable<AtributosAdicionais>>("AtributosAdicionais", "ConsulteTodosAtributosAdcionais");
        }
    }
}
