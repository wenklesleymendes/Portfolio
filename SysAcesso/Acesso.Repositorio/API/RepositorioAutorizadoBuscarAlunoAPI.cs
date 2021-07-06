using Acesso.Dominio.Pessoas.Tipo;
using System.Collections.Generic;

namespace Acesso.Interfaces.Repositorios.API
{
    public class RepositorioAutorizadoBuscarAlunoAPI : IRepositorioAutorizadoBuscarAluno
    {
        public AutorizadoBuscarAluno ConsulteAutorizadoBuscarAluno(int idAutorizado)
        {
            return APIHelper.Instancia.Get<AutorizadoBuscarAluno>("AutorizadoBuscaAluno", $"ConsulteAutorizadoBuscarAluno?idAutorizado={idAutorizado}");
        }

        public IEnumerable<AutorizadoBuscarAluno> ConsulteAutorizadoBuscarAlunoAtivos()
        {
            return APIHelper.Instancia.Get<List<AutorizadoBuscarAluno>>("AutorizadoBuscarAluno", "ConsulteAutorizadoBuscarAlunoAtivos");
        }

        public bool AutorizadoBuscarAlunoEstaBloqueado(int idAutorizado)
        {
            return APIHelper.Instancia.Get<bool>("AutorizadoBuscarAluno", $"AutorizadoBuscarAlunoEstaBloqueado?idAutorizado={idAutorizado}");
        }

        public bool AutorizadoBuscarAlunoEstaAtivo(int idAutorizado)
        {
            return APIHelper.Instancia.Get<bool>("AutorizadoBuscarAluno", $"AutorizadoBuscarAlunoEstaAtivo?idAutorizado={idAutorizado}");
        }

        public bool AutorizadoBuscarAlunoEstaInadimplenteDeCheques(int idAutorizado)
        {
            return APIHelper.Instancia.Get<bool>("AutorizadoBuscarAluno", $"AutorizadoBuscarAlunoEstaInadimplenteDeCheques?idAutorizado={idAutorizado}");
        }

        public bool AutorizadoBuscarAlunoEstaInadimplenteDuplicata(int idAutorizado)
        {
            return APIHelper.Instancia.Get<bool>("AutorizadoBuscarAluno", $"AutorizadoBuscarAlunoEstaInadimplenteDuplicata?idAutorizado={idAutorizado}");
        }

        public bool AutorizadoBuscarAlunoEstaPendenteDeDocumento(int idAutorizado)
        {
            return APIHelper.Instancia.Get<bool>("AutorizadoBuscarAluno", $"AutorizadoBuscarAlunoEstaPendenteDeDocumento?idAutorizado={idAutorizado}");
        }

        public bool AutorizadoBuscarAlunoEstaPendenteDeMateriais(int idAutorizado)
        {
            return APIHelper.Instancia.Get<bool>("AutorizadoBuscarAluno", $"AutorizadoBuscarAlunoEstaPendenteDeMateriais?idAutorizado={idAutorizado}");
        }
    }
}
