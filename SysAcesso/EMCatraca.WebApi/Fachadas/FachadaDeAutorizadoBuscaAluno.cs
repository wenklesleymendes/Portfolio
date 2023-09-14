using EMCatraca.Core.Dominio;
using EMCatraca.Server;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.WebApi.Fachadas
{
    public class FachadaDeAutorizadoBuscaAluno
    {
        private readonly IRepositorioAutorizadoBuscarAluno _repositorioAutorizadoBuscarAluno = FabricaDeRepositorios.Instancia.CrieRepositorioAutorizadoBuscarAluno();

        public AutorizadoBuscarAluno ConsulteAutorizadoBuscarAluno(int idAutorizado)
        {
            return _repositorioAutorizadoBuscarAluno.ConsulteAutorizadoBuscarAluno(idAutorizado);
        }

        public IEnumerable<AutorizadoBuscarAluno> ConsulteAutorizadoBuscarAlunoAtivos()
        {
            return _repositorioAutorizadoBuscarAluno.ConsulteAutorizadoBuscarAlunoAtivos();
        }

        public bool AutorizadoBuscarAlunoEstaBloqueado(int idAutorizado)
        {
            return _repositorioAutorizadoBuscarAluno.AutorizadoBuscarAlunoEstaBloqueado(idAutorizado);
        }

        public bool AutorizadoBuscarAlunoEstaAtivo(int idAutorizado)
        {
            return _repositorioAutorizadoBuscarAluno.AutorizadoBuscarAlunoEstaAtivo(idAutorizado);
        }

        public bool AutorizadoBuscarAlunoEstaInadimplenteDeCheques(int idAutorizado)
        {
            return _repositorioAutorizadoBuscarAluno.AutorizadoBuscarAlunoEstaInadimplenteDeCheques(idAutorizado);
        }

        public bool AutorizadoBuscarAlunoEstaInadimplenteDuplicata(int idAutorizado)
        {
            return _repositorioAutorizadoBuscarAluno.AutorizadoBuscarAlunoEstaInadimplenteDuplicata(idAutorizado);
        }

        public bool AutorizadoBuscarAlunoEstaPendenteDeDocumentos(int idAutorizado)
        {
            return _repositorioAutorizadoBuscarAluno.AutorizadoBuscarAlunoEstaPendenteDeDocumento(idAutorizado);
        }

        public bool AutorizadoBuscarAlunoEstaPendenteDeMateriais(int idAutorizado)
        {
            return _repositorioAutorizadoBuscarAluno.AutorizadoBuscarAlunoEstaPendenteDeMateriais(idAutorizado);
        }

    }
}