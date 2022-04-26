using EscolaPro.Core.Model.ReguaContato;
using EscolaPro.Repository.Interfaces.ReguaContato;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository.ReguaContato
{
    public class ReguaContatoFilaRepository : DomainRepository<ReguaContatoFila>, IReguaContatoFilaRepository
    {
        public ReguaContatoFilaRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<ReguaContatoFila>> BuscarPorRegraId(int reguaContatoRegrasId)
        {
            try
            {
                IQueryable<ReguaContatoFila> query = await Task.FromResult(GenerateQuery(x => x.ReguaContatoRegrasId == reguaContatoRegrasId
                                                                                           && x.StatusFila != StatusFila.Enviando
                                                                                           && x.StatusFila != StatusFila.Enviando).AsNoTracking());

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ReguaContatoFila>> BuscarPorTipoMensage(TipoMensagem tipoMensagem)
        {
            try
            {
                IQueryable<ReguaContatoFila> query = await Task.FromResult(GenerateQuery(x => x.ReguaContatoRegras.TipoMensagem == tipoMensagem
                                                                                           && (x.StatusFila == StatusFila.NaoEnviado
                                                                                           || x.StatusFila == StatusFila.ErroNoEnvio))
                                                               .Include(x => x.ReguaContatoRegras)
                                                               .OrderBy(x => x.Prioridade)
                                                               .ThenBy(x => x.DataInclusao)
                                                               .AsNoTracking());

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReguaContatoFila> BuscarPorId(int id)
        {
            try
            {
                IQueryable<ReguaContatoFila> query = await Task.FromResult(GenerateQuery(x => x.Id == id)
                                                               .Include(x => x.ReguaContatoRegras)
                                                               .Include(x => x.Aluno)
                                                               .ThenInclude(x => x.Matriculas)
                                                               .ThenInclude(x => x.Curso)
                                                               .Include(x => x.Aluno)
                                                               .ThenInclude(x => x.Contato)
                                                               .Include(x => x.Unidade)
                                                               .ThenInclude(x => x.Contato)
                                                               .Include(x => x.Unidade)
                                                               .ThenInclude(x => x.Endereco)
                                                               .Include(x => x.Pagamento)
                                                               .AsNoTracking());

                return query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateStatus(List<ReguaContatoFila> lista, StatusFila statusFila)
        {
            IQueryable<ReguaContatoFila> query = await Task.FromResult(GenerateQuery(x => lista.Any(y=> y.Id ==x.Id )));

            query.ToList().ForEach(x => x.StatusFila = statusFila);
            await this.UpdateRangeAsync(query);
        }

        public async Task UpdateStatus(int id, StatusFila statusFila)
        {
            IQueryable<ReguaContatoFila> query = await Task.FromResult(GenerateQuery(x => x.Id == id));

            ReguaContatoFila reguaContatoFila = query.FirstOrDefault();
            reguaContatoFila.StatusFila = statusFila;
            await this.UpdateAsync(reguaContatoFila);
        }

    }
}
