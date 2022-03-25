using EscolaPro.Core.Model;
using EscolaPro.Core.Model.Enums;
using EscolaPro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class PerfilUsuarioRepository : DomainRepository<PerfilUsuario>, IPerfilUsuarioRepository
    {
        public PerfilUsuarioRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<PerfilUsuario> BuscarPorId(int idPerfil)
        {
            try
            {
                IQueryable<PerfilUsuario> query = await Task.FromResult(GenerateQuery((x => x.Id == idPerfil), null));

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PerfilUsuario> BuscarPorTipo(int perfilUsuarioId)
        {
            try
            {
                IQueryable<PerfilUsuario> query = await Task.FromResult(GenerateQuery((x => x.PerfilSistemaEnum == (Core.Model.Enums.PerfilSistemaEnum)perfilUsuarioId), null));

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ConsultarPerfilExistente(PerfilSistemaEnum perfilSistemaEnum)
        {
            try
            {
                IQueryable<PerfilUsuario> query = await Task.FromResult(GenerateQuery((x => x.PerfilSistemaEnum == perfilSistemaEnum), null));

                return query.Count() > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int BuscarPerfilAdminId()
        {
            try
            {
                return dbContext.Set<PerfilUsuario>()
                    .Where(x => x.PerfilSistemaEnum == PerfilSistemaEnum.Administrador)
                    .Select(x => x.Id)
                    .FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
    }
}
