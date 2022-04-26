using EMCatraca.Core.Dominio;
using System.Collections.Generic;

namespace EMCatraca.Server.Interfaces
{
    public interface IRepositorioOperador
    {
        IEnumerable<Operador> ConsulteTodosOperadorAtivos();

        bool ValideOperador(int codigo, string senha);
    }
}
