using EscolaPro.Core.Model;
using EscolaPro.Repository.Scripts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IUsuarioRepository : IDomainRepository<Usuario>
    {
        Task<Usuario> Login(string userName, string senha);
        Task<Usuario> BuscarUsuarioPorNome(string userName);
        Task<IEnumerable<Usuario>> FiltrarUsuario(int? funcionarioId, int? departamentoId, int? unidadeId);
        Task<Usuario> BuscarPorId(int usuarioId);
        Task<IEnumerable<Usuario>> BuscarTodos(int usuarioLogadoId);
        Task<Usuario> BuscarPorAlunoId(int alunoId);
        Task<IEnumerable<Usuario>> BuscarUsuarioAtendente();
        bool CheckUsuarioAdmin(int perfilId);
        Task<Usuario> BuscarPorFuncionarioId(int funcionarioId);
        Task<IEnumerable<Usuario>> BuscaUsuarioPorUnidade(int? funcionarioId, int? departamentoId, int? unidadeId);

    }
}
