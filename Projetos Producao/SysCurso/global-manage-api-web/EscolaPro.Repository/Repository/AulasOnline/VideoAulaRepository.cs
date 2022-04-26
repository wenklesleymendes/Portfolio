using EscolaPro.Core.Model.AulasOnline;
using EscolaPro.Repository.Interfaces.AulasOnline;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.AulasOnline
{
    public class VideoAulaRepository : DomainRepository<VideoAula>, IVideoAulaRepository
    {
        public VideoAulaRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public Task<VideoAula> BuscarPorId(int videoAulaId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VideoAula>> BuscarPorMateria(int materiaOnlineId)
        {
            try
            {
                IQueryable<VideoAula> query = await Task.FromResult(GenerateQuery((x => x.MateriaId == materiaOnlineId), null));

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<VideoPausado> BuscarUltimaSessao(int matriculaId)
        {
            try
            {
                var videoPausadoLista = await dbContext.Set<VideoPausado>().Where(x => x.MatriculaId == matriculaId).ToListAsync();

                return videoPausadoLista.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<VideoPausado> SalvarUltimaSessao(VideoPausado videoPausado)
        {
            try
            {
                var videoPausadoLista = dbContext.Set<VideoPausado>().Where(x => x.MatriculaId == videoPausado.MatriculaId).ToList();

                foreach (var item in videoPausadoLista)
                {
                    dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                }

                await dbContext.SaveChangesAsync();

                videoPausado.DataUltimaVisualizacao = DateTime.Now;

                dbContext.Entry(videoPausado).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                await dbContext.SaveChangesAsync();

                return await BuscarUltimaSessao(videoPausado.MatriculaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
