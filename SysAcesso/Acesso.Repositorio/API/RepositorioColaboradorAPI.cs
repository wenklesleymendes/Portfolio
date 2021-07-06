using  Acesso.Dominio.Pessoas.Tipo;
using System.Collections.Generic;

namespace Acesso.Interfaces.Repositorios.API
{
    public class RepositorioColaboradorAPI : IRepositorioColaborador
    {
        public Colaborador ConsulteColaborador(int idColaborador)
        {
            return APIHelper.Instancia.Get<Colaborador>("Colaborador", $"ConsulteColaborador?idColaborador={idColaborador}");
        }

        public IEnumerable<Colaborador> ConsulteTodosColaboradorAtivos()
        {
            return APIHelper.Instancia.Get<List<Colaborador>>("Colaborador", "ConsulteTodosColaboradorAtivos");
        }

        public bool ColaboradorEstaAtivo(int idColaborador)
        {
            return APIHelper.Instancia.Get<bool>("Colaborador", $"ColaboradorEstaAtivo?idColaborador={idColaborador}");
        }
    }
}
