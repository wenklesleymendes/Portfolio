using EscolaPro.Core.Model.ControlePontoEletronico;
using EscolaPro.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaPro.Repository.Repository
{
    public class ControlePontoRepository : DomainRepository<PontoEletronico>, IControlePontoRepository
    {
        public ControlePontoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<PontoEletronico> Atualizar(PontoEletronico pontoEletronico)
        {
            try
            {
                await UpdateAsync(pontoEletronico);

                return await GetByIdAsync(pontoEletronico.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PontoEletronico> BuscarPontoEletronicoPorId(int idPontoEletronico)
        {
            IQueryable<PontoEletronico> query = await Task.FromResult(GenerateQuery((x => x.Id == idPontoEletronico && !x.IsDelete), null));

            return query.FirstOrDefault();
        }

        public async Task<IEnumerable<PontoEletronico>> BuscarPorFeriasId(int feriasId)
        {
            IQueryable<PontoEletronico> query = await Task.FromResult(GenerateQuery((x => x.FeriasId == feriasId && !x.IsDelete), null));

            return query.OrderBy(x => x.Entrada1).ToList(); 
        }

        public async Task<IEnumerable<PontoEletronico>> BuscarPorFolhaPagamentoId(int folhaPagamentoId)
        {
            IQueryable<PontoEletronico> query = await Task.FromResult(GenerateQuery((x => x.FolhaPagamentoId == folhaPagamentoId && !x.IsDelete), null));

            return query.OrderBy(x => x.Entrada1).ToList();
        }

        public async Task<IEnumerable<PontoEletronico>> BuscarPorFuncionario(int idFuncionario, DateTime dateInicio, DateTime dataFim)
        {
            IQueryable<PontoEletronico> query = await Task.FromResult(GenerateQuery((x => x.FuncionarioId == idFuncionario && 
                                                                                     x.Entrada1.Value.Date >= dateInicio.Date && x.Entrada1.Value.Date <= dataFim.Date 
                                                                                     && !x.IsDelete), null));


            List<PontoEletronico> pontoEletronicos =
                (from tab in query
                 orderby tab.Entrada1.Value.Date descending
                 select tab).ToList();


            return pontoEletronicos;
                //query.OrderByDescending(x => x.Entrada1.Value.TimeOfDay).ThenBy(x => x.Entrada1.Value.Date).ThenBy(x => x.Entrada1.Value.Year);
            ///query.OrderBy(x => x.Entrada1.Value.Date).ToList(); 
        }

        public async Task<IEnumerable<PontoEletronico>> BuscarPorNomeArquivo(string nomeArquivo)
        {
            IQueryable<PontoEletronico> query = await Task.FromResult(GenerateQuery((x => x.NomeArquivo == nomeArquivo && !x.IsDelete), null));

            return query.ToList();
        }
    }
}
