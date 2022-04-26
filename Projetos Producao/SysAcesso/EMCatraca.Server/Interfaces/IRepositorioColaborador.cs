using EMCatraca.Core.Dominio;
using System.Collections.Generic;

namespace EMCatraca.Server.Interfaces
{
    public interface IRepositorioColaborador
    {
        Colaborador ConsulteColaborador(int codigo);

        IEnumerable<Colaborador> ConsulteTodosColaboradorAtivos();

        bool ColaboradorEstaAtivo(int IdColaborador);
    }
}
