using  Acesso.Dominio;
using System.Collections.Generic;

namespace Acesso.Interfaces.Repositorios.API
{
    public class RepositorioOperadorAPI : IRepositorioOperador
    {
        public Operador ConsulteOperador()
        {
            return APIHelper.Instancia.Get<Operador>("Operador", "ConsulteOperador");
        }

        public IEnumerable<Operador> ConsulteTodosOperadorAtivos()
        {
            return APIHelper.Instancia.Get<List<Operador>>("Operador", "ConsulteTodosOperadorAtivos");
        }

        public bool ValideOperador(int codigo, string senha)
        {
            return APIHelper.Instancia.Get<bool>("Operador", $"ValideOperador?codigo={codigo}&senha={senha}");

        }
    }
}
