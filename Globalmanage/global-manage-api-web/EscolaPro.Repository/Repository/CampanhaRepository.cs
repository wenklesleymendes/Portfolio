using EscolaPro.Core.Model;
using EscolaPro.Core.Model.BolsaConvenio;
using EscolaPro.Repository.Interfaces;
using EscolaPro.Repository.Scripts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class CampanhaRepository : DomainRepository<Campanha>, ICampanhaRepository
    {
        public CampanhaRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        [Obsolete]
        public async Task<IEnumerable<Campanha>> BuscarCampanhaVigente(int unidadeId, int cursoId, int tipoPagamento)
        {
            try
            {
                var sqlQuery = CampanhaScript.Filtrar(unidadeId, cursoId, tipoPagamento);

                var query = dbSet.FromSql(sqlQuery).Select(x => new Campanha
                {
                    Id = x.Id,
                }).ToList();

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Campanha> BuscarPorId(int idCampanha)
        {
            var campanha = await GetByIdAsync(idCampanha);

            var cursoDescricao = dbContext.Set<Curso>().Where(x => x.Id == campanha.CursoId).FirstOrDefault().Descricao;

            campanha.DescricaoCurso = cursoDescricao;

            var campanhaUnidade = dbContext.Set<CampanhaUnidade>();

            campanha.CampanhaUnidade = campanhaUnidade.Where(x => x.CampanhaId == campanha.Id).ToList();

            var tipoPagamento = dbContext.Set<CampanhaTipoPagamento>();

            campanha.CampanhaTipoPagamento = tipoPagamento.Where(x => x.CampanhaId == campanha.Id).ToList();

            return campanha;
        }

        public async Task<Campanha> Inserir(Campanha campanha, List<CampanhaUnidade> campanhaUnidades)
        {
            if (campanha.Id == 0)
            {
                var campanhaInserida = await AddAsync(campanha);

                return campanha;
            }
            else
            {
                var idCampanha = campanha.Id;

                await UpdateAsync(campanha);

                var campanhaTipoPagamentoExcluir = dbContext.Set<CampanhaTipoPagamento>();

                foreach (var item in campanhaTipoPagamentoExcluir.Where(x => x.CampanhaId == idCampanha))
                {
                    dbContext.Entry<CampanhaTipoPagamento>(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                }

                var campanhaUnidadeExcluir = dbContext.Set<CampanhaUnidade>();

                foreach (var item in campanhaUnidadeExcluir.Where(x => x.CampanhaId == idCampanha))
                {
                    dbContext.Entry<CampanhaUnidade>(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                }

                await dbContext.SaveChangesAsync();

                foreach (var item in campanha.CampanhaTipoPagamento.ToList())
                {
                    item.CampanhaId = idCampanha;
                    dbContext.Entry<CampanhaTipoPagamento>(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }

                foreach (var item in campanhaUnidades.ToList())
                {
                    item.CampanhaId = idCampanha;
                    dbContext.Entry<CampanhaUnidade>(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }

                await dbContext.SaveChangesAsync();

                campanha = await BuscarPorId(campanha.Id);

                return campanha;
            }
        }
    }
}
