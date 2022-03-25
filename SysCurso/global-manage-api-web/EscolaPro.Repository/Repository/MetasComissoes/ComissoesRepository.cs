using EscolaPro.Core.Model.Enums;
using EscolaPro.Core.Model.MetasComissoes;
using EscolaPro.Repository.Interfaces.MetasComissoes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.MetasComissoes
{
    public class ComissoesRepository : DomainRepository<Comissoes>, IComissoesRepository
    {
        public ComissoesRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task AtualizarParcelas(int idComissao, List<ComissaoParcela> comissaoParcelas)
        {
            var comissaoParcelasOld = dbContext.Set<ComissaoParcela>().Where(x => x.ComissoesId == idComissao).ToList();

            foreach (var item in comissaoParcelasOld)
            {
                dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }

            await dbContext.SaveChangesAsync();

            foreach (var item in comissaoParcelas)
            {
                item.Id = 0;
                item.ComissoesId = idComissao;
                dbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<Comissoes> BuscarPorId(int idComissoes)
        {
            var comissoes = await GetByIdAsync(idComissoes);

            var comissaoParcelas = dbContext.Set<ComissaoParcela>().Where(x => x.ComissoesId == idComissoes && !x.IsDelete).ToList();

            foreach (var item in comissaoParcelas)
            {
                comissoes.ComissaoParcelas.Add(item);
            }

            return comissoes;
        }

        public async Task<IEnumerable<Comissoes>> Filtrar(int? UnidadeId, DateTime? dataInicio, DateTime? dataFim, TipoPagamentoEnum? tipoPagamentoEnum)
        {
            try
            {

                if (dataFim.HasValue)
                {
                    dataFim.Value.Date.AddDays(1);
                }

                if (!UnidadeId.HasValue && !tipoPagamentoEnum.HasValue && !dataInicio.HasValue && !dataFim.HasValue)
                {
                    IQueryable<Comissoes> query = await Task.FromResult(GenerateQuery((x => !x.IsDelete), null));

                    return query.ToList();
                }

                if (UnidadeId.HasValue && tipoPagamentoEnum.HasValue && dataInicio.HasValue && dataFim.HasValue)
                {
                    IQueryable<Comissoes> query = await Task.FromResult(GenerateQuery((x => x.UnidadeId == UnidadeId &&
                                                                                            x.TipoPagamento == tipoPagamentoEnum.Value &&
                                                                                             x.DataInicioVirgencia.Value.Date >= dataInicio &&
                                                                                             x.DataFimVirgencia.Value.Date <= dataFim &&
                                                                                             !x.IsDelete), null));

                    return query.ToList();
                }

                if (UnidadeId.HasValue && tipoPagamentoEnum.HasValue)
                {
                    IQueryable<Comissoes> query = await Task.FromResult(GenerateQuery((x => x.UnidadeId == UnidadeId &&
                                                                                             x.TipoPagamento == tipoPagamentoEnum
                                                                                             && !x.IsDelete), null));
                    return query.ToList();
                }

                if (UnidadeId.HasValue && !tipoPagamentoEnum.HasValue)
                {
                    IQueryable<Comissoes> query = await Task.FromResult(GenerateQuery((x => x.UnidadeId == UnidadeId &&
                                                                                             !x.IsDelete), null));
                    return query.ToList();
                }

                if (!UnidadeId.HasValue && tipoPagamentoEnum.HasValue)
                {
                    IQueryable<Comissoes> query = await Task.FromResult(GenerateQuery((x => x.TipoPagamento == tipoPagamentoEnum &&
                                                                                             !x.IsDelete), null));
                    return query.ToList();
                }
                else
                {
                    IQueryable<Comissoes> query = await Task.FromResult(GenerateQuery((x => !x.IsDelete), null));

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
}
    }
}
