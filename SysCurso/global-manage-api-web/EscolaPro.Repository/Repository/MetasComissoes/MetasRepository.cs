using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Repository.Interfaces.MetasComissoes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.MetasComissoes
{
    public class MetasRepository : DomainRepository<Meta>, IMetasRepository
    {
        public MetasRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task AdicionarDetalhamentoMeta(int idMeta, List<DetalhamentoMeta> metaPeriodos)
        {
            foreach (var item in metaPeriodos)
            {
                item.MetaId = idMeta;
                dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<Meta> BuscarPorId(int idMeta)
        {
            var meta = await GetByIdAsync(idMeta);

            meta.DetalhamentoMeta = new List<DetalhamentoMeta>();

            meta.DetalhamentoMeta = dbContext.Set<DetalhamentoMeta>().Where(x => x.MetaId == idMeta && !x.IsDelete).ToList();

            return meta;
        }

        public async Task<IEnumerable<Meta>> Filtrar(int? UnidadeId, string nomeMeta)
        {
            if (UnidadeId.HasValue && !string.IsNullOrEmpty(nomeMeta))
            {
                IQueryable<Meta> query = await Task.FromResult(GenerateQuery((x => x.UnidadeId == UnidadeId && x.Descricao.ToUpper().Contains(nomeMeta.ToUpper()) && !x.IsDelete), null));

                return query;
            }
            else if (UnidadeId.HasValue)
            {
                IQueryable<Meta> query = await Task.FromResult(GenerateQuery((x => x.UnidadeId == UnidadeId && !x.IsDelete), null));

                return query;
            }
            else if (!string.IsNullOrEmpty(nomeMeta))
            {
                IQueryable<Meta> query = await Task.FromResult(GenerateQuery((x => x.Descricao.ToUpper().Contains(nomeMeta.ToUpper()) && !x.IsDelete), null));

                return query;
            }
            else
            {
                IQueryable<Meta> query = await Task.FromResult(GenerateQuery((x => !x.IsDelete), null));

                return query;
            }
        }

        public async Task<bool> MetaPeriodosDeletar(int idMeta)
        {
            var metaPeriodos = dbContext.Set<DetalhamentoMeta>().Where(x => x.MetaId == idMeta && !x.IsDelete).ToList();

            foreach (var item in metaPeriodos)
            {
                dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }

            int atualizado = await dbContext.SaveChangesAsync();
            return atualizado > 1 ? true : false;
        }

        public async Task<bool> MetaUnidadesDeletar(int idMeta)
        {
            var metaUnidades = dbContext.Set<MetaUnidade>().Where(x => x.MetaId == idMeta && !x.IsDelete).ToList();

            foreach (var item in metaUnidades)
            {
                dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }

            int atualizado = await dbContext.SaveChangesAsync();
            return atualizado > 1 ? true : false;
        }



    }
}
