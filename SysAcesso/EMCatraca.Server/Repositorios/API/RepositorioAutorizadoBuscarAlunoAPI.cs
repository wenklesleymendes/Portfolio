using EMCatraca.Core.Dominio;
using EMCatraca.Server.Interfaces;
using System.Collections.Generic;

namespace EMCatraca.Server.Repositorios.API
{
    public class RepositorioAutorizadoBuscarAlunoAPI : IRepositorioAutorizadoBuscarAluno
    {
        private readonly IAPIConexao _apiConexao;

        public RepositorioAutorizadoBuscarAlunoAPI(IAPIConexao apiConexao)
        {
            _apiConexao = apiConexao;
        }

        public AutorizadoBuscarAluno ConsulteAutorizadoBuscarAluno(int idAutorizado)
        {
            return _apiConexao.Get<AutorizadoBuscarAluno>("AutorizadoBuscaAluno", $"ConsulteAutorizadoBuscarAluno?idAutorizado={idAutorizado}");
        }

        public IEnumerable<AutorizadoBuscarAluno> ConsulteAutorizadoBuscarAlunoAtivos()
        {
            return _apiConexao.Get<List<AutorizadoBuscarAluno>>("AutorizadoBuscarAluno", "ConsulteAutorizadoBuscarAlunoAtivos");
        }

        public bool AutorizadoBuscarAlunoEstaBloqueado(int idAutorizado)
        {
            return _apiConexao.Get<bool>("AutorizadoBuscarAluno", $"AutorizadoBuscarAlunoEstaBloqueado?idAutorizado={idAutorizado}");
        }

        public bool AutorizadoBuscarAlunoEstaAtivo(int idAutorizado)
        {
            return _apiConexao.Get<bool>("AutorizadoBuscarAluno", $"AutorizadoBuscarAlunoEstaAtivo?idAutorizado={idAutorizado}");
        }

        public bool AutorizadoBuscarAlunoEstaInadimplenteDeCheques(int idAutorizado)
        {
            return _apiConexao.Get<bool>("AutorizadoBuscarAluno", $"AutorizadoBuscarAlunoEstaInadimplenteDeCheques?idAutorizado={idAutorizado}");
        }

        public bool AutorizadoBuscarAlunoEstaInadimplenteDuplicata(int idAutorizado)
        {
            return _apiConexao.Get<bool>("AutorizadoBuscarAluno", $"AutorizadoBuscarAlunoEstaInadimplenteDuplicata?idAutorizado={idAutorizado}");
        }

        public bool AutorizadoBuscarAlunoEstaPendenteDeDocumento(int idAutorizado)
        {
            return _apiConexao.Get<bool>("AutorizadoBuscarAluno", $"AutorizadoBuscarAlunoEstaPendenteDeDocumento?idAutorizado={idAutorizado}");
        }

        public bool AutorizadoBuscarAlunoEstaPendenteDeMateriais(int idAutorizado)
        {
            return _apiConexao.Get<bool>("AutorizadoBuscarAluno", $"AutorizadoBuscarAlunoEstaPendenteDeMateriais?idAutorizado={idAutorizado}");
        }
    }
}
