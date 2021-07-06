using  Acesso.Dominio;
using System.Collections.Generic;

namespace Acesso.Interfaces
{
    public interface IRepositorioOperador
    {
        IEnumerable<Operador> ConsulteTodosOperadorAtivos();

        bool ValideOperador(int codigo, string senha);
    }
}
