using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Interfaces
{
    public interface IPerfilUsuarioRepository : IDomainRepository<PerfilUsuario>
    {
        Task<PerfilUsuario> BuscarPorTipo(int perfilUsuarioId);
        Task<bool> ConsultarPerfilExistente(PerfilSistemaEnum perfilSistemaEnum);
        Task<PerfilUsuario> BuscarPorId(int idPerfil);
        int BuscarPerfilAdminId();
    }
}
