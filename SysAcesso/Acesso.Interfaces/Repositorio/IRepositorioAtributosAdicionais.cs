using Acesso.Dominio;
using System.Collections.Generic;

namespace Acesso.Interfaces
{
    public interface IRepositorioAtributosAdicionais
    {
        IEnumerable<AtributosAdicionais> ConsulteTodosAtributosAdcionais();
    }
}
