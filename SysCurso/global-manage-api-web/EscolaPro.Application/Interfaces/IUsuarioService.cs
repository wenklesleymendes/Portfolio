using EscolaPro.Service.Dto.ControleUsuarioVO;
using EscolaPro.Service.Dto.UsuarioVO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<DtoUsuario> Inserir(DtoUsuarioRequest model, bool isAluno);
        Task<DtoUsuario> Login(string email, string senha);
        Task<DtoUsuario> BuscarPorId(int idUsuario);
        Task<IEnumerable<DtoUsuario>> BuscarTodos(int idUsuario);
        Task<bool> Excluir(int idUsuario);
        Task<DtoUsuario> DesativarOuAtivar(int idUsuario);
        Task<IEnumerable<DtoUsuario>> FiltrarUsuario(DtoFiltrarUsuario dtoFiltrarUsuario);
        Task<DtoUsuario> FiltrarUsuario(string userName);
        Task<DtoUsuario> BuscarPorAlunoId(int alunoId);
        Task<bool> EsqueciMinhaSenha(string email);
        Task<IEnumerable<DtoUsuario>> BuscarUsuarioAtendente();
        bool CheckUsuarioAdmin(int perfilId);
        Task<DtoUsuario> CriarUsuarioAdmin(DtoUsuarioRequest model);
    }
}
