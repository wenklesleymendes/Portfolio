using EscolaPro.Core.Model.Enums;
using EscolaPro.Service.Dto.ControleUsuarioVO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Service.Interfaces
{
    public interface IPerfilUsuarioService
    {
        Task<DtoPerfilUsuario> Inserir(DtoPerfilUsuario dtoPerfilUsuario);
        Task<IEnumerable<DtoPerfilUsuario>> BuscarTodos();
        Task<DtoPerfilUsuario> BuscarPorId(int idPerfil);
        Task<DtoPerfilUsuario> DesativarOuAtivar(int idPerfil);
        Task<bool> Excluir(int idPerfil);
        Task<bool> ConsultarPerfilExistente(PerfilSistemaEnum perfilSistemaEnum);
        Task<IEnumerable<DtoPerfilUsuario>> BuscarTodosAtivos();
        int BuscaPerfilAdminId();
    }
}
