using  Acesso.Dominio.Pessoas.Tipo;
using System.Collections.Generic;

namespace Acesso.Interfaces
{
    public interface IRepositorioAutorizadoBuscarAluno
    {
        AutorizadoBuscarAluno ConsulteAutorizadoBuscarAluno(int codigo);

        IEnumerable<AutorizadoBuscarAluno> ConsulteAutorizadoBuscarAlunoAtivos();

        bool AutorizadoBuscarAlunoEstaAtivo(int idAutorizado);

        bool AutorizadoBuscarAlunoEstaBloqueado(int idAutorizado);

        bool AutorizadoBuscarAlunoEstaInadimplenteDuplicata(int idAutorizado);

        bool AutorizadoBuscarAlunoEstaInadimplenteDeCheques(int idAutorizado);

        bool AutorizadoBuscarAlunoEstaPendenteDeDocumento(int idAutorizado);

        bool AutorizadoBuscarAlunoEstaPendenteDeMateriais(int idAutorizado);
    }
}
