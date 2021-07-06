using  Acesso.Dominio.Pessoas.Tipo;
using System.Collections.Generic;

namespace Acesso.Interfaces
{
    public interface IRepositorioColaborador
    {
        Colaborador ConsulteColaborador(int codigo);

        IEnumerable<Colaborador> ConsulteTodosColaboradorAtivos();

        bool ColaboradorEstaAtivo(int IdColaborador);
    }
}
